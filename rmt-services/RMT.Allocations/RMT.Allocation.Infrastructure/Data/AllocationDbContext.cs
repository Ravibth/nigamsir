using Microsoft.EntityFrameworkCore;
using RMT.Allocation.Domain.Entities;

namespace RMT.Allocation.Infrastructure.Data
{

    public class AllocationDbContext : DbContext
    {
        public AllocationDbContext(DbContextOptions<AllocationDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to postgres with connection string from app settings
            //options.UseNpgsql(Configuration.GetConnectionString("WebApiDatabase"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<resourceallocationrequistionview>()
                .ToView(nameof(resourceallocationrequistionview))
                 .HasKey(t => t.RequisitionId);

            modelBuilder.Entity<AllocationDayGroup>()
             .ToView(nameof(sp_allocationday_view))
               .HasKey(t => t.monthname);

            modelBuilder.Entity<AllocationCommonView>()
             .ToView(nameof(allocation_common_view))
               .HasKey(t => t.Id);

            modelBuilder.Entity<AllocationDayResourceGroup>()
            .ToView(nameof(sp_allocationday_resource_view))
              .HasKey(t => t.empemail);

            modelBuilder.Entity<ResourceAllocationDesignation>()
                .ToView(nameof(sp_allocation_designation_view))
                .HasKey(t => t.designation);

            modelBuilder.Entity<UpdateDesignationCost>()
               .ToView(nameof(sp_update_designation))
                .HasKey(t => t.EmpEmail);

            modelBuilder.Entity<RequisitionType>()
            .HasData(
                new RequisitionType
                {
                    Id = 1,
                    Type = RequisitionTypeData.NamedAllocation
                },
                new RequisitionType
                {
                    Id = 2,
                    Type = RequisitionTypeData.SameTeamAllocation
                },
                new RequisitionType
                {
                    Id = 3,
                    Type = RequisitionTypeData.CreateRequisition
                },
                new RequisitionType
                {
                    Id = 4,
                    Type = RequisitionTypeData.RollForwardAllocation
                },
                new RequisitionType
                {
                    Id = 5,
                    Type = RequisitionTypeData.BulkAllocation
                },
                new RequisitionType
                {
                    Id = 6,
                    Type = RequisitionTypeData.BulkRequisition
                }
                );

        }
        public DbSet<RequisitionDemand> RequisitionDemand { get; set; }
        public DbSet<RequisitionType> RequisitionType { get; set; }
        public DbSet<Requisition> Requisition { get; set; }
        public DbSet<RequisitionParameters> RequisitionParameters { get; set; }
        public DbSet<RequisitionSkill> RequisitionSkill { get; set; }
        public DbSet<RequisitionParameterValues> RequisitionParameterValues { get; set; }
        public DbSet<PublishedResAllocDetails> PublishedResAllocDetails { get; set; }
        public DbSet<UnPublishedResAllocDetails> UnPublishedResAllocDetails { get; set; }
        public DbSet<PublishedResAlloc> PublishedResAlloc { get; set; }
        public DbSet<UnPublishedResAlloc> UnPublishedResAlloc { get; set; }
        public DbSet<PublishedResAllocDays> PublishedResAllocDays { get; set; }
        public DbSet<UnPublishedResAllocDays> UnPublishedResAllocDays { get; set; }
        public DbSet<PublishedResAllocSkillEntity> PublishedResAllocSkillEntity { get; set; }
        public DbSet<UnPublishedResAllocSkillEntity> UnPublishedResAllocSkillEntity { get; set; }
        public DbSet<AllocationCommonView> allocation_common_view { get; set; }





        //public DbSet<ResourceAllocation> ResourceAllocation { get; set; }
        //public DbSet<ResourceAllocationDetails> ResourceAllocationDetails { get; set; }
        //public DbSet<ResAllocationDays> ResourceAllocationDays { get; set; }
        ////public DbSet<RequisitionLocation> RequisitionLocation { get; set; }
        //public DbSet<ResourceAllocationSkillEntity> ResourceAllocationSkillEntity { get; set; }
        //public DbSet<ResourceAllocationDaysHistory> ResourceAllocationDaysHistory { get; set; }


        public DbSet<resourceallocationrequistionview> resourceallocationrequistionview { get; set; }
        public DbSet<AllocationDayGroup> sp_allocationday_view { get; set; }
        public DbSet<AllocationDayResourceGroup> sp_allocationday_resource_view { get; set; }
        public DbSet<ResourceAllocationDesignation> sp_allocation_designation_view { get; set; }
        public DbSet<UpdateDesignationCost> sp_update_designation { get; set; }
    }
}
