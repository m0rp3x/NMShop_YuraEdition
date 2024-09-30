using MudBlazor.Services;
using NMShop.Client.Data;
using NMShop.Components;

var builder = WebApplication.CreateBuilder(args);

// Регистрация сервисов
builder.Services.AddHttpClient();

builder.Services.AddScoped<TestDataProvider>();

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
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseAntiforgery();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    // другие endpoints
});



app.MapControllers();

app.MapRazorComponents<App>()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(NMShop.Client._Imports).Assembly);

app.Run();