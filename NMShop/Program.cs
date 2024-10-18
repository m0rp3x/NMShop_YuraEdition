using MudBlazor.Services;
using NMShop.Components;
using Microsoft.EntityFrameworkCore;
using NMShop.Shared.Scaffold;
using NMShop.Client.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient();

builder.Services.AddSingleton<ClientDataProvider>();
builder.Services.AddScoped<CartService>();

builder.Services.AddControllersWithViews()
    .AddDataAnnotationsLocalization();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());
});

builder.Services.AddControllers();
builder.Services.AddMudServices();

// Добавление компонентов Razor
builder.Services.AddRazorComponents()
    .AddInteractiveWebAssemblyComponents();



builder.Services.AddDbContext<NMShopContext>(options =>
{
    options.UseNpgsql(builder.Configuration.GetConnectionString("SpermaConnection"));

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
app.UseCoreAdminCustomUrl("adolfhitler");
app.UseCoreAdminCustomTitle("Я ебу собак");
app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(NMShop.Client._Imports).Assembly);

app.UseStaticFiles();
app.MapDefaultControllerRoute();

app.Run();