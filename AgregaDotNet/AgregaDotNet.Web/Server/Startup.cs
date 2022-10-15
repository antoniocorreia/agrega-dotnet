using Sparc.Authentication.AzureADB2C;
using Sparc.Core;
using Sparc.Features;

namespace AgregaDotNet.Features;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        
        services.Sparcify<Startup>(Configuration["WebClientUrl"]);
        services.AddAzureADB2CAuthentication(Configuration);

        services.AddRazorPages();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        //app.UseCors(policy =>
        //    policy.WithOrigins("https://localhost:7019", "http://localhost:5019", 
        //        "https://kodekit-test.azurewebsites.net",
        //        "https://app.kodekit.io")
        //    .AllowAnyMethod()
        //    .AllowAnyHeader());

        app.Sparcify<Startup>(env);

        app.UseEndpoints(endpoints => {
            endpoints.MapControllers();
        });
        app.UseDeveloperExceptionPage();

    }
}