using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;
using System.Net.Http;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// Регистрация HttpClient с базовым адресом для Blazor WebAssembly
builder.Services.AddScoped(sp => 
    new HttpClient { BaseAddress = new Uri("https://localhost:7279/") });



// Регистрация MudBlazor
builder.Services.AddMudServices();

await builder.Build().RunAsync();