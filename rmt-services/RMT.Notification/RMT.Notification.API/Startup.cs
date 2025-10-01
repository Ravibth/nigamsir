using Microsoft.EntityFrameworkCore;
using RMT.Notification.Infrastructure.Data;
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

        // Migrate latest database changes during startup
        using (var scope = app.ApplicationServices.CreateScope())
        {
            var dbContext = scope.ServiceProvider
                .GetRequiredService<NotificationDbContext>();

            // Here is the migration executed
            dbContext.Database.Migrate();
        }
    }

    public void ConfigureServices(IServiceCollection services)
    {

    }

}