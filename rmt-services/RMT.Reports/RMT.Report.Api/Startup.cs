using Microsoft.EntityFrameworkCore;
using RMT.Reports.Infrastructure.Data;
using RMT.Reports.Infrastructure.Helpers;

namespace RMT.Report.Api
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            ConfigConstants.AllocationDbConnStr = Configuration.GetSection("MicroserviceApiSettings").GetSection("AllocationDbConnStr").Value;
            ConfigConstants.WcgtDbConnStr = Configuration.GetSection("MicroserviceApiSettings").GetSection("WcgtDbConnStr").Value;
            ConfigConstants.ConfigDbConnStr = Configuration.GetSection("MicroserviceApiSettings").GetSection("ConfigDbConnStr").Value;
            ConfigConstants.ProjectDbConnStr = Configuration.GetSection("MicroserviceApiSettings").GetSection("ProjectDbConnStr").Value;
            ConfigConstants.SkillDbConnStr = Configuration.GetSection("MicroserviceApiSettings").GetSection("SkillDbConnStr").Value;

            using (var scope = app.ApplicationServices.CreateScope())
            {
                var dbContext = scope.ServiceProvider
                    .GetRequiredService<ReportsDBContext>();

                // Here is the migration executed
                dbContext.Database.Migrate();

                Console.WriteLine("DB Migration execution Completed.");

            }
        }
        public void ConfigureServices(IServiceCollection services)
        {
            // Migrate latest database changes during startup

        }
    }
}
