using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCGT.Domain.Entities;

namespace WCGT.Infrastructure.Data
{
    public class WcgtDbContext : DbContext
    {
        public WcgtDbContext(DbContextOptions<WcgtDbContext> options) : base(options)
        {
        }

        public override ValueTask<EntityEntry<TEntity>> AddAsync<TEntity>(TEntity entity, CancellationToken cancellationToken = default)
        {
            TrimFieldValue();

            return base.AddAsync(entity, cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            TrimFieldValue();

            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            TrimFieldValue();

            return base.SaveChanges();
        }

        private void TrimFieldValue()
        {
            foreach (var entity in this.ChangeTracker.Entries())
            {
                var properties = entity.Properties.ToList().Where(o => o.Metadata.ClrType.Name.Equals("String") && o.CurrentValue is not null);
                foreach (PropertyEntry property in properties)
                {
                    var currentValue = Convert.ToString(property.CurrentValue);
                    if (!string.IsNullOrEmpty(currentValue))
                    {
                        property.CurrentValue = currentValue.Trim();
                    }
                }
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to postgres with connection string from app settings
            //options.UseNpgsql(Configuration.GetConnectionString("WebApiDatabase"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DesignationGradeView>()
                .ToView(nameof(designationgraderateview))
                .HasNoKey();

            modelBuilder.Entity<WcgtTimesheetGroup>()
                .ToFunction(nameof(sp_timesheet_view)).HasNoKey().ToView(null);
            //.HasKey(t => t.monthname);

            modelBuilder.Entity<WCGTTimesheet>().HasKey(t => t.id);

            modelBuilder.Entity<WcgtResoureTimesheetGroup>()
                .ToFunction(nameof(sp_resource_timesheet_view)) //.HasNoKey().ToView(null);
                .HasKey(t => t.empcode);

            modelBuilder.Entity<LeaveReport>()
                .ToFunction(nameof(generate_leave_report)) //.HasNoKey().ToView(null);
                .HasNoKey();

            modelBuilder.Entity<WorkingDays>()
                .HasIndex(u => u.working_date).IsUnique();

            modelBuilder.Entity<Budget>().HasKey(x => x.Id);

            modelBuilder.Entity<Qualifications>()
                        .HasOne(q => q.Employee)
                        .WithMany(e => e.Qualifications)
                        .HasForeignKey(q => q.employee_mid)
                        .HasPrincipalKey(e => e.employee_mid)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<PastEmploymentDetails>()
                        .HasOne(e => e.Employee)
                        .WithMany(emp => emp.PastEmploymentDetails)
                        .HasForeignKey(e => e.employee_mid)
                        .HasPrincipalKey(emp => emp.employee_mid)
                        .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<language>()
                        .HasOne(l => l.Employee)
                        .WithMany(emp => emp.Language)
                        .HasForeignKey(l => l.employee_mid)
                        .HasPrincipalKey(emp => emp.employee_mid)
                        .OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<WCGTDataLog> WCGTDataLogs { get; set; }

        public DbSet<Client> Clients { get; set; }

        public DbSet<Pipeline> Pipelines { get; set; }
        public DbSet<PipelineRole> PipelineRoles { get; set; }

        public DbSet<Job> Jobs { get; set; }
        public DbSet<JobRole> JobRoles { get; set; }

        //public DbSet<Project> Projects { get; set; }
        //public DbSet<ProjectJobCode> ProjectJobCodes { get; set; }
        //public DbSet<ProjectRole> ProjectRoles { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<LeaveReport> generate_leave_report { get; set; }

        public DbSet<Competency> Competencies { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<BUTreeMapping> BUTreeMappings { get; set; }
        public DbSet<Holiday> Holidays { get; set; }
        public DbSet<Leave> Leaves { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<ClientLegalEntity> ClientLegalEntitys { get; set; }
        public DbSet<SectorIndustry> SectorIndustrys { get; set; }
        public DbSet<WCGTTimesheet> Timesheet { get; set; }
        public DbSet<WcgtTimesheetGroup> sp_timesheet_view { get; set; }
        public DbSet<WcgtResoureTimesheetGroup> sp_resource_timesheet_view { get; set; }
        public DbSet<RateDesignationMaster> RateDesignationMaster { get; set; }
        public DbSet<WorkingDays> working_days { get; set; }
        public DbSet<Budget> Budget { get; set; }
        public DbSet<DesignationGradeView> designationgraderateview { get; set; }
        public DbSet<Qualifications> Qualifications { get; set; }
        public DbSet<PastEmploymentDetails> PastEmploymentDetails { get; set; }
        public DbSet<language> Language { get; set; }

    }
}
