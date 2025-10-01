using Microsoft.EntityFrameworkCore;
using RMT.Reports.Domain.Entities;

namespace RMT.Reports.Infrastructure.Data
{
    public class ReportsDBContext : DbContext
    {
        public ReportsDBContext(DbContextOptions<ReportsDBContext> options) : base(options)
        {
        }

        public DbSet<EmployeeAllocationTimeSheetEntity> EmployeeAllocationTimeSheet { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Console.WriteLine("this method is calling in migration, ");
            Console.WriteLine("DB Migration execution in progress...");
            modelBuilder.Entity<EmployeeAllocationTimeSheetEntity>()
                        .HasNoKey()
                        .ToView("employee-allocation-timesheet");

            base.OnModelCreating(modelBuilder);
        }


    }
}
