using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using RMT.Projects.Domain.Entities;
using RMT.Projects.Domain.Repositories;
using System;
using System.Data;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using static RMT.Projects.Domain.Constant;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RMT.Projects.Infrastructure.Data
{
    public class ProjectDbContext : DbContext
    {
        public ProjectDbContext(DbContextOptions<ProjectDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to postgres with connection string from app settings
            //options.UseNpgsql(Configuration.GetConnectionString("WebApiDatabase"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<ProjectRolesView>(eb =>
                {
                    //eb.HasIndex(a => a.Id);
                    //eb.HasKey(a => a.Id);
                    eb.ToView("vw_projectroles");
                });

            modelBuilder.Entity<FieldForMarketPlace>().HasData(
                new FieldForMarketPlace
                {
                    Id = 1,
                    InternalName = "projectName",
                    DisplayName = "Name of Project",
                    IsActive = true,
                    CreatedAt = new DateTime(2024, 2, 22, 15, 19, 45, 866, DateTimeKind.Utc),
                    CreatedBy = "System",
                    ModifiedAt = new DateTime(2024, 2, 22, 15, 19, 45, 866, DateTimeKind.Utc),
                    ModifiedBy = "System",
                },
                new FieldForMarketPlace
                {
                    Id = 2,
                    InternalName = "clientName",
                    DisplayName = "Client Name",
                    IsActive = true,
                    CreatedAt = new DateTime(2024, 2, 22, 15, 19, 45, 866, DateTimeKind.Utc),
                    CreatedBy = "System",
                    ModifiedAt = new DateTime(2024, 2, 22, 15, 19, 45, 866, DateTimeKind.Utc),
                    ModifiedBy = "System",
                },
                new FieldForMarketPlace
                {
                    Id = 3,
                    InternalName = "clientGroup",
                    DisplayName = "Client Group",
                    IsActive = true,
                    CreatedAt = new DateTime(2024, 2, 22, 15, 19, 45, 866, DateTimeKind.Utc),
                    CreatedBy = "System",
                    ModifiedAt = new DateTime(2024, 2, 22, 15, 19, 45, 866, DateTimeKind.Utc),
                    ModifiedBy = "System",
                },
                new FieldForMarketPlace
                {
                    Id = 4,
                    InternalName = "projectCode",
                    DisplayName = "Project ID",
                    IsActive = true,
                    CreatedAt = new DateTime(2024, 2, 22, 15, 19, 45, 866, DateTimeKind.Utc),
                    CreatedBy = "System",
                    ModifiedAt = new DateTime(2024, 2, 22, 15, 19, 45, 866, DateTimeKind.Utc),
                    ModifiedBy = "System",
                }
                );

            ////Configure domain classes using modelBuilder here
        }

        public DbSet<Project> Projects { get; set; }
        //public DbSet<ProjectJobCodes> ProjectJobCodes { get; set; }
        public DbSet<ProjectRoles> ProjectRoles { get; set; }

        public DbSet<ProjectRolesView> ProjectRolesView { get; set; }

        public DbSet<ProjectDemand> ProjectDemands { get; set; }
        public DbSet<ProjectSkills> ProjectSkills { get; set; }
        public DbSet<ProjectCompetency> ProjectCompetency { get; set; }
        public DbSet<ProjectDemandSkills> ProjectDemandSkills { get; set; }

        public DbSet<ProjectBudget> ProjectBudget { get; set; }
        public DbSet<FieldForMarketPlace> FieldForMarketPlaces { get; set; }
        public DbSet<PublishedFieldForMarketPlace> PublishedFieldForMarketPlaces { get; set; }
        public DbSet<ProjectRequisitionAllocation> ProjectRequisitionAllocation { get; set; }

    }
}
