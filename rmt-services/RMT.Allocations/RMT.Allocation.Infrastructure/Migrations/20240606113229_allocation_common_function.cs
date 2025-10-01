using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Allocation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class allocation_common_function : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sp = @"CREATE VIEW ALLOCATION_COMMON_VIEW AS
                        SELECT *, 'u' as ""Type"" FROM ""UnPublishedResAllocDays"" 
	                    UNION 
	                    select *, 'p' as ""Type"" from ""PublishedResAllocDays"" 
	                    where ""PublishedResAllocDays"".""RequisitionId"" NOT IN (
		                    select ""RequisitionId"" from ""UnPublishedResAllocDays"" 
	                    )";
            migrationBuilder.Sql(sp);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@" drop view ALLOCATION_COMMON_VIEW;");
        }

    }
}
