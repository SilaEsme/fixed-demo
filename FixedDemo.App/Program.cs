using Blazored.LocalStorage;
using FixedDemo.App;
using FixedDemo.App.Providers;
using FixedDemo.App.Services.Authentication;
using FixedDemo.App.Services.Data;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredLocalStorage();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7102") });
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IInventoryService, InventoryService>();
builder.Services.AddAuthorizationCore();
builder.Services.AddOptions();

await builder.Build().RunAsync();
