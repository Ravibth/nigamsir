using Microsoft.EntityFrameworkCore;
using RMT.Projects.Infrastructure.Data;

public class Startup
{
    public IConfiguration Configuration { get; }
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940

        //var builder = new ConfigurationBuilder()
        //    .SetBasePath(env.ContentRootPath)
        //    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        //    .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
        //    .AddEnvironmentVariables();

        //builder.Build();

        // Migrate latest database changes during startup
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var dbContext = scope.ServiceProvider
                .GetRequiredService<ProjectDbContext>();

            // Here is the migration executed
            dbContext.Database.Migrate();
        }

    }

    public void ConfigureServices(IServiceCollection services)
    {

    }

}