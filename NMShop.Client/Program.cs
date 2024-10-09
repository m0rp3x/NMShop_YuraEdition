using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using NMShop.Client.Data;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Регистрация HttpClient с базовым адресом для Blazor WebAssembly
builder.Services.AddSingleton(sp =>
    new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<ClientTestDataProvider>();

// Регистрация MudBlazor
builder.Services.AddMudServices();

await builder.Build().RunAsync();
