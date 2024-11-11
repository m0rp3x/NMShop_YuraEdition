using MudBlazor.Services;
using NMShop.Components;
using Microsoft.EntityFrameworkCore;
using NMShop.Shared.Scaffold;
using NMShop.Client.Services;
using System.Text.Json.Serialization;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();

builder.Services.AddSingleton<ClientDataProvider>();
builder.Services.AddSingleton<CartService>();
builder.Services.AddSingleton<LayoutService>();

builder.Services.AddControllersWithViews()
    .AddDataAnnotationsLocalization();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
});

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
}); 

builder.Services.AddMudServices();

// Добавление компонентов Razor
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();



// Configuring DB Connection depending on environment
string connectionString = builder.Environment.IsDevelopment()
    ? builder.Configuration.GetConnectionString("RemoteConnection")
    : builder.Configuration.GetConnectionString("LocalConnection");

builder.Services.AddDbContext<NMShopContext>(options =>
{
    options.UseNpgsql(connectionString);
});

builder.Services.AddAutoMapper(typeof(AppMappingProfile));

builder.Services.AddCoreAdmin(); // после DbContext-а

// Добавляем поддержку аутентификации и cookie
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
})
    .AddCookie(options =>
    {
        options.LoginPath = "/login";  // Путь на страницу логина
        options.AccessDeniedPath = "/accessdenied"; // Путь на страницу отказа в доступе
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Время жизни cookie
    });

builder.Services.AddAuthorization();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();
// Настройка CORS
app.UseCors("CorsPolicy");

// Настройка среды разработки
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();
app.MapControllers();

var adminUrl = builder.Configuration["CoreAdmin:CustomUrl"];
app.UseCoreAdminCustomUrl(adminUrl);
app.UseCoreAdminCustomTitle("Я ебу собак");


app.UseCoreAdminCustomAuth((serviceProvider) =>
{
    var context = serviceProvider.GetService<IHttpContextAccessor>()?.HttpContext;
    return Task.FromResult(context?.User?.Identity?.IsAuthenticated == true && context.User.IsInRole("Admin"));
});

app.MapPost("/login", async (HttpContext context) =>
{
    var form = context.Request.Form;
    var username = form["username"];
    var password = form["password"];

    if (username == "goida" && password == "bober")
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, username),
            new Claim(ClaimTypes.Role, "Admin")
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

        context.Response.Redirect(adminUrl);
    }
    else
    {
        context.Response.Redirect("/login?error=invalid_credentials");
    }
});

app.MapGet("/logout", async (HttpContext context) =>
{
    await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    context.Response.Redirect("/");
});

app.MapGet("/login", async (HttpContext context) =>
{
    await context.Response.WriteAsync(@"
        <html>
        <body>
            <form method='post' action='/login'>
                <label for='username'>Username:</label>
                <input type='text' id='username' name='username'>
                <br>
                <label for='password'>Password:</label>
                <input type='password' id='password' name='password'>
                <br>
                <button type='submit'>Login</button>
            </form>
        </body>
        </html>");
});

// Добавляем endpoint для страницы отказа в доступе
app.MapGet("/accessdenied", async (HttpContext context) =>
{
    await context.Response.WriteAsync("Access Denied");
});

app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(NMShop.Client._Imports).Assembly);

app.UseStaticFiles();
app.MapDefaultControllerRoute();

app.Run();