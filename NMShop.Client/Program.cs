using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MudBlazor.Services;
using NMShop.Client.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Регистрация HttpClient с базовым адресом для Blazor WebAssembly
builder.Services.AddSingleton(sp =>
    new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddSingleton<ClientDataProvider>();
builder.Services.AddSingleton<CartService>();
builder.Services.AddSingleton<LayoutService>();
builder.Services.AddLocalization();
// Регистрация MudBlazor
builder.Services.AddMudServices();

await builder.Build().RunAsync();
