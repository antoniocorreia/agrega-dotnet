using AgregaDotNet.Web;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Sparc.Platforms.Web;
using AgregaDotNet.Features;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<IConfiguration>(_ => builder.Configuration);

//builder.AddPublicApi<AgregaDotNetApi>(builder.Configuration["ApiUrl"]);

builder.AddB2CApi<AgregaDotNetApi>(
                "https://kodekitui.onmicrosoft.com/ccba7246-6276-4566-a964-12d7a2b48198/KodekitAPI.Access",
                builder.Configuration["ApiUrl"]);

//builder.Services.AddApiAuthorization();
await builder.Build().RunAsync();
