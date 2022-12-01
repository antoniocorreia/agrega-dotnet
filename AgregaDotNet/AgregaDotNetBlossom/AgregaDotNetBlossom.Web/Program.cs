using AgregaDotNetBlossom.Features;
using AgregaDotNetBlossom.Web;
using Blazored.Modal;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Sparc.Blossom.Web;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddBlazoredModal();

builder.AddBlossom<AgregaDotNetBlossomApi>(builder.Configuration["ApiUrl"]);

await builder.Build().RunAsync();


