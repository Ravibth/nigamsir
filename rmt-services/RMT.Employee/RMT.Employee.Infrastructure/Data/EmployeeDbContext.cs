using Microsoft.EntityFrameworkCore;
using RMT.Employee.Domain.Entities;
using static RMT.Employee.Domain.Constants;

namespace RMT.Employee.Infrastructure.Data
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to postgres with connection string from app settings
            //options.UseNpgsql(Configuration.GetConnectionString("WebApiDatabase"));
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PreferenceMaster>()
                .HasData(
                new PreferenceMaster
                {
                    PreferenceMasterId = 100,
                    Category = CategoryType.LOCATION.ToString().ToUpper(),
                    Description = "String",
                    Name = "Noida",
                    SortOrder = 10,
                    IsActive = true,
                    CreatedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    CreatedBy = "System",
                    ModifiedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    ModifiedBy = "System"
                },
                new PreferenceMaster
                {
                    PreferenceMasterId = 101,
                    Category = CategoryType.LOCATION.ToString().ToUpper(),
                    Description = "String",
                    Name = "Delhi",
                    SortOrder = 10,
                    IsActive = true,
                    CreatedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    CreatedBy = "System",
                    ModifiedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    ModifiedBy = "System"
                },
                new PreferenceMaster
                {
                    PreferenceMasterId = 102,
                    Category = CategoryType.LOCATION.ToString().ToUpper(),
                    Description = "String",
                    Name = "Pune",
                    SortOrder = 10,
                    IsActive = true,
                    CreatedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    CreatedBy = "System",
                    ModifiedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    ModifiedBy = "System"
                },
                new PreferenceMaster
                {
                    PreferenceMasterId = 103,
                    Category = CategoryType.LOCATION.ToString().ToUpper(),
                    Description = "String",
                    Name = "Lucknow",
                    SortOrder = 10,
                    IsActive = true,
                    CreatedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    CreatedBy = "System",
                    ModifiedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    ModifiedBy = "System"
                },
                new PreferenceMaster
                {
                    PreferenceMasterId = 104,
                    Category = CategoryType.LOCATION.ToString().ToUpper(),
                    Description = "String",
                    Name = "Hydrabad",
                    SortOrder = 10,
                    IsActive = true,
                    CreatedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    CreatedBy = "System",
                    ModifiedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    ModifiedBy = "System"
                },
                new PreferenceMaster
                {
                    PreferenceMasterId = 105,
                    Category = CategoryType.LOCATION.ToString().ToUpper(),
                    Description = "String",
                    Name = "Jaipur",
                    SortOrder = 10,
                    IsActive = true,
                    CreatedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    CreatedBy = "System",
                    ModifiedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    ModifiedBy = "System"
                },
                new PreferenceMaster
                {
                    PreferenceMasterId = 106,
                    Category = CategoryType.SME.ToString().ToUpper(),
                    Description = "String",
                    Name = "SME 1",
                    SortOrder = 10,
                    IsActive = true,
                    CreatedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    CreatedBy = "System",
                    ModifiedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    ModifiedBy = "System"
                },
                new PreferenceMaster
                {
                    PreferenceMasterId = 107,
                    Category = CategoryType.SME.ToString().ToUpper(),
                    Description = "String",
                    Name = "SME 2",
                    SortOrder = 10,
                    IsActive = true,
                    CreatedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    CreatedBy = "System",
                    ModifiedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    ModifiedBy = "System"
                },
                new PreferenceMaster
                {
                    PreferenceMasterId = 108,
                    Category = CategoryType.SME.ToString().ToUpper(),
                    Description = "String",
                    Name = "SME 3",
                    SortOrder = 10,
                    IsActive = true,
                    CreatedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    CreatedBy = "System",
                    ModifiedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    ModifiedBy = "System"
                },
                new PreferenceMaster
                {
                    PreferenceMasterId = 109,
                    Category = CategoryType.SME.ToString().ToUpper(),
                    Description = "String",
                    Name = "SME 4",
                    SortOrder = 10,
                    IsActive = true,
                    CreatedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    CreatedBy = "System",
                    ModifiedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    ModifiedBy = "System"
                },
                new PreferenceMaster
                {
                    PreferenceMasterId = 110,
                    Category = CategoryType.SME.ToString().ToUpper(),
                    Description = "String",
                    Name = "SME 5",
                    SortOrder = 10,
                    IsActive = true,
                    CreatedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    CreatedBy = "System",
                    ModifiedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    ModifiedBy = "System"
                },
                new PreferenceMaster
                {
                    PreferenceMasterId = 111,
                    Category = CategoryType.REVENUE_UNIT.ToString().ToUpper(),
                    Description = "String",
                    Name = "REVENUE UNIT 1",
                    SortOrder = 10,
                    IsActive = true,
                    CreatedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    CreatedBy = "System",
                    ModifiedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    ModifiedBy = "System"
                },
                new PreferenceMaster
                {
                    PreferenceMasterId = 112,
                    Category = CategoryType.REVENUE_UNIT.ToString().ToUpper(),
                    Description = "String",
                    Name = "REVENUE UNIT 2",
                    SortOrder = 10,
                    IsActive = true,
                    CreatedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    CreatedBy = "System",
                    ModifiedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    ModifiedBy = "System"
                },
                new PreferenceMaster
                {
                    PreferenceMasterId = 113,
                    Category = CategoryType.REVENUE_UNIT.ToString().ToUpper(),
                    Description = "String",
                    Name = "REVENUE UNIT 3",
                    SortOrder = 10,
                    IsActive = true,
                    CreatedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    CreatedBy = "System",
                    ModifiedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    ModifiedBy = "System"
                },
                new PreferenceMaster
                {
                    PreferenceMasterId = 114,
                    Category = CategoryType.REVENUE_UNIT.ToString().ToUpper(),
                    Description = "String",
                    Name = "REVENUE UNIT 4",
                    SortOrder = 10,
                    IsActive = true,
                    CreatedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    CreatedBy = "System",
                    ModifiedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    ModifiedBy = "System"
                },
                new PreferenceMaster
                {
                    PreferenceMasterId = 115,
                    Category = CategoryType.REVENUE_UNIT.ToString().ToUpper(),
                    Description = "String",
                    Name = "REVENUE UNIT 5",
                    SortOrder = 10,
                    IsActive = true,
                    CreatedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    CreatedBy = "System",
                    ModifiedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    ModifiedBy = "System"
                },
                new PreferenceMaster
                {
                    PreferenceMasterId = 116,
                    Category = CategoryType.EXPERTISE.ToString().ToUpper(),
                    Description = "String",
                    Name = "EXPERTISE 1",
                    SortOrder = 10,
                    IsActive = true,
                    CreatedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    CreatedBy = "System",
                    ModifiedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    ModifiedBy = "System"
                },
                new PreferenceMaster
                {
                    PreferenceMasterId = 117,
                    Category = CategoryType.EXPERTISE.ToString().ToUpper(),
                    Description = "String",
                    Name = "EXPERTISE 2",
                    SortOrder = 10,
                    IsActive = true,
                    CreatedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    CreatedBy = "System",
                    ModifiedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    ModifiedBy = "System"
                },
                new PreferenceMaster
                {
                    PreferenceMasterId = 118,
                    Category = CategoryType.EXPERTISE.ToString().ToUpper(),
                    Description = "String",
                    Name = "EXPERTISE 3",
                    SortOrder = 10,
                    IsActive = true,
                    CreatedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    CreatedBy = "System",
                    ModifiedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    ModifiedBy = "System"
                },
                new PreferenceMaster
                {
                    PreferenceMasterId = 119,
                    Category = CategoryType.EXPERTISE.ToString().ToUpper(),
                    Description = "String",
                    Name = "EXPERTISE 4",
                    SortOrder = 10,
                    IsActive = true,
                    CreatedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    CreatedBy = "System",
                    ModifiedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    ModifiedBy = "System"
                },
                new PreferenceMaster
                {
                    PreferenceMasterId = 120,
                    Category = CategoryType.EXPERTISE.ToString().ToUpper(),
                    Description = "String",
                    Name = "EXPERTISE 5",
                    SortOrder = 10,
                    IsActive = true,
                    CreatedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    CreatedBy = "System",
                    ModifiedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    ModifiedBy = "System"
                },
                new PreferenceMaster
                {
                    PreferenceMasterId = 121,
                    Category = CategoryType.ENGAGEMENT_LEADER.ToString().ToUpper(),
                    Description = "String",
                    Name = "Samarth Mathur",
                    SortOrder = 10,
                    IsActive = true,
                    CreatedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    CreatedBy = "System",
                    ModifiedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    ModifiedBy = "System"
                },
                new PreferenceMaster
                {
                    PreferenceMasterId = 122,
                    Category = CategoryType.ENGAGEMENT_LEADER.ToString().ToUpper(),
                    Description = "String",
                    Name = "Manish Karl",
                    SortOrder = 10,
                    IsActive = true,
                    CreatedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    CreatedBy = "System",
                    ModifiedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    ModifiedBy = "System"
                },
                new PreferenceMaster
                {
                    PreferenceMasterId = 123,
                    Category = CategoryType.ENGAGEMENT_LEADER.ToString().ToUpper(),
                    Description = "String",
                    Name = "Devang Sharma",
                    SortOrder = 10,
                    IsActive = true,
                    CreatedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    CreatedBy = "System",
                    ModifiedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    ModifiedBy = "System"
                },
                new PreferenceMaster
                {
                    PreferenceMasterId = 124,
                    Category = CategoryType.ENGAGEMENT_LEADER.ToString().ToUpper(),
                    Description = "String",
                    Name = "Abhishake Kumar",
                    SortOrder = 10,
                    IsActive = true,
                    CreatedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    CreatedBy = "System",
                    ModifiedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    ModifiedBy = "System"
                },
                new PreferenceMaster
                {
                    PreferenceMasterId = 125,
                    Category = CategoryType.ENGAGEMENT_LEADER.ToString().ToUpper(),
                    Description = "String",
                    Name = "Shristi",
                    SortOrder = 10,
                    IsActive = true,
                    CreatedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    CreatedBy = "System",
                    ModifiedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    ModifiedBy = "System"
                },
                new PreferenceMaster
                {
                    PreferenceMasterId = 126,
                    Category = CategoryType.BUISNESS_UNIT.ToString().ToUpper(),
                    Description = "String",
                    Name = "Business Unit 1",
                    SortOrder = 10,
                    IsActive = true,
                    CreatedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    CreatedBy = "System",
                    ModifiedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    ModifiedBy = "System"
                },
                new PreferenceMaster
                {
                    PreferenceMasterId = 127,
                    Category = CategoryType.BUISNESS_UNIT.ToString().ToUpper(),
                    Description = "String",
                    Name = "Business Unit 2",
                    SortOrder = 10,
                    IsActive = true,
                    CreatedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    CreatedBy = "System",
                    ModifiedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    ModifiedBy = "System"
                },
                new PreferenceMaster
                {
                    PreferenceMasterId = 128,
                    Category = CategoryType.BUISNESS_UNIT.ToString().ToUpper(),
                    Description = "String",
                    Name = "Business Unit 3",
                    SortOrder = 10,
                    IsActive = true,
                    CreatedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    CreatedBy = "System",
                    ModifiedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    ModifiedBy = "System"
                },
                new PreferenceMaster
                {
                    PreferenceMasterId = 129,
                    Category = CategoryType.BUISNESS_UNIT.ToString().ToUpper(),
                    Description = "String",
                    Name = "Business Unit 4",
                    SortOrder = 10,
                    IsActive = true,
                    CreatedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    CreatedBy = "System",
                    ModifiedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    ModifiedBy = "System"
                },
                new PreferenceMaster
                {
                    PreferenceMasterId = 130,
                    Category = CategoryType.BUISNESS_UNIT.ToString().ToUpper(),
                    Description = "String",
                    Name = "Business Unit 5",
                    SortOrder = 10,
                    IsActive = true,
                    CreatedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    CreatedBy = "System",
                    ModifiedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    ModifiedBy = "System"
                },
                new PreferenceMaster
                {
                    PreferenceMasterId = 131,
                    Category = CategoryType.INDUSTRY.ToString().ToUpper(),
                    Description = "String",
                    Name = "Industry 1",
                    SortOrder = 10,
                    IsActive = true,
                    CreatedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    CreatedBy = "System",
                    ModifiedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    ModifiedBy = "System"
                },
                new PreferenceMaster
                {
                    PreferenceMasterId = 132,
                    Category = CategoryType.INDUSTRY.ToString().ToUpper(),
                    Description = "String",
                    Name = "Industry 2",
                    SortOrder = 10,
                    IsActive = true,
                    CreatedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    CreatedBy = "System",
                    ModifiedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    ModifiedBy = "System"
                },
                new PreferenceMaster
                {
                    PreferenceMasterId = 133,
                    Category = CategoryType.INDUSTRY.ToString().ToUpper(),
                    Description = "String",
                    Name = "Industry 3",
                    SortOrder = 10,
                    IsActive = true,
                    CreatedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    CreatedBy = "System",
                    ModifiedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    ModifiedBy = "System"
                },
                new PreferenceMaster
                {
                    PreferenceMasterId = 134,
                    Category = CategoryType.INDUSTRY.ToString().ToUpper(),
                    Description = "String",
                    Name = "Industry 4",
                    SortOrder = 10,
                    IsActive = true,
                    CreatedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    CreatedBy = "System",
                    ModifiedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    ModifiedBy = "System"
                },
                new PreferenceMaster
                {
                    PreferenceMasterId = 135,
                    Category = CategoryType.INDUSTRY.ToString().ToUpper(),
                    Description = "String",
                    Name = "Industry 5",
                    SortOrder = 10,
                    IsActive = true,
                    CreatedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    CreatedBy = "System",
                    ModifiedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    ModifiedBy = "System"
                },
                new PreferenceMaster
                {
                    PreferenceMasterId = 136,
                    Category = CategoryType.SECTOR.ToString().ToUpper(),
                    Description = "String",
                    Name = "Sector 1",
                    SortOrder = 10,
                    IsActive = true,
                    CreatedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    CreatedBy = "System",
                    ModifiedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    ModifiedBy = "System"
                },
                new PreferenceMaster
                {
                    PreferenceMasterId = 137,
                    Category = CategoryType.SECTOR.ToString().ToUpper(),
                    Description = "String",
                    Name = "Sector 2",
                    SortOrder = 10,
                    IsActive = true,
                    CreatedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    CreatedBy = "System",
                    ModifiedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    ModifiedBy = "System"
                },
                new PreferenceMaster
                {
                    PreferenceMasterId = 138,
                    Category = CategoryType.SECTOR.ToString().ToUpper(),
                    Description = "String",
                    Name = "Sector 3",
                    SortOrder = 10,
                    IsActive = true,
                    CreatedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    CreatedBy = "System",
                    ModifiedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    ModifiedBy = "System"
                },
                new PreferenceMaster
                {
                    PreferenceMasterId = 139,
                    Category = CategoryType.SECTOR.ToString().ToUpper(),
                    Description = "String",
                    Name = "Sector 4",
                    SortOrder = 10,
                    IsActive = true,
                    CreatedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    CreatedBy = "System",
                    ModifiedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    ModifiedBy = "System"
                },
                new PreferenceMaster
                {
                    PreferenceMasterId = 140,
                    Category = CategoryType.SECTOR.ToString().ToUpper(),
                    Description = "String",
                    Name = "Sector 5",
                    SortOrder = 10,
                    IsActive = true,
                    CreatedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    CreatedBy = "System",
                    ModifiedAt = new DateTime(2024, 01, 01, 1, 0, 0, 0, DateTimeKind.Utc),
                    ModifiedBy = "System"
                }
                );
        }

        public DbSet<EmployeePreference> EmployeePreferences { get; set; }

        public DbSet<PreferenceMaster> PreferenceMasters { get; set; }

        public DbSet<EmployeeProjectMapping> EmployeeProjectMapping { get; set; }
        public DbSet<EmployeeProfile> EmployeeProfile { get; set; } 
        public DbSet<EmployeeQualification> EmployeeQualification { get; set; } 
        public DbSet<EmployeeLanguage> EmployeeLanguage { get; set; }
        public DbSet<EmployeeWorkExprerience> EmployeeWorkExperience { get; set; }
        public DbSet<ExperienceOutsideGT> EmployeeExperienceOutsideGT { get; set; }


    }
}
