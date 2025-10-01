using Microsoft.EntityFrameworkCore;
using RMT.Skill.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Skill.Infrastructure.Data
{
    public class SkillDbContext : DbContext
    {
        public SkillDbContext(DbContextOptions<SkillDbContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to postgres with connection string from app settings
            //options.UseNpgsql(Configuration.GetConnectionString("WebApiDatabase"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Skills>().HasKey(t => t.Skill_Id);
            modelBuilder.Entity<SkillCategoryMaster>().HasData(

                new SkillCategoryMaster { id = 1, CategoryName = "Technical", IsActive = true, CreateDate = new DateTime(2024, 8, 28, 17, 9, 50, 75, DateTimeKind.Utc).AddTicks(2229), CreatedBy = "System" },
                 new SkillCategoryMaster { id = 2, CategoryName = "Soft", IsActive = true, CreateDate = new DateTime(2024, 8, 28, 17, 9, 50, 75, DateTimeKind.Utc).AddTicks(2233), CreatedBy = "System" }
                );
            modelBuilder.Entity<SkillMapping>().HasKey(t => t.id);
        }
        public DbSet<Skills> Skills { get; set; }
        public DbSet<UserSkills> UserSkills { get; set; }
        public DbSet<SkillCategoryMaster> SkillCategoryMaster { get; set; }
        public DbSet<SkillMapping> SkillMapping { get; set; }

    }
}
