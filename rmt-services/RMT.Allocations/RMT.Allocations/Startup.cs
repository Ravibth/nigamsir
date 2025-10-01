using Microsoft.EntityFrameworkCore;
using RMT.Allocation.Domain;
using RMT.Allocation.Infrastructure;
using RMT.Allocation.Infrastructure.Data;

namespace RMT.Allocations.API
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
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
            string _WorkingHourPerDay = Configuration?.GetSection("MicroserviceApiSettings")?.GetSection("WorkingHourPerDay")?.Value;
            string _NonWorkingDaysOfWeek = Configuration?.GetSection("MicroserviceApiSettings")?.GetSection("NonWorkingDaysOfWeek")?.Value;

            Constants.WorkingHourPerDay = Int16.Parse(string.IsNullOrEmpty(_WorkingHourPerDay) ? "8" : _WorkingHourPerDay);

            Constants.NonWorkingDays = (string.IsNullOrEmpty(_NonWorkingDaysOfWeek) ? "0,6" : _NonWorkingDaysOfWeek)
                .Split(',')?
                .Where(x => short.TryParse(x, out _))
                .Select(short.Parse)?.ToList<short>(); ;


            // Migrate latest database changes during startup
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var dbContext = scope.ServiceProvider
                    .GetRequiredService<AllocationDbContext>();
                // Here is the migration executed
                dbContext.Database.Migrate();
            }
        }

        public void ConfigureServices(IServiceCollection services)
        {

        }
    }
}
