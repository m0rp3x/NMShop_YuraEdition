using MudBlazor.Services;
using NMShop.Components;
using Microsoft.EntityFrameworkCore;
using NMShop.Shared.Scaffold;
using NMShop.Client.Services;
using System.Text.Json.Serialization;

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
app.UseCoreAdminCustomAuth((serviceProvider) => Task.FromResult(true));

app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(NMShop.Client._Imports).Assembly);

app.UseStaticFiles();
app.MapDefaultControllerRoute();

app.Run();