using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Allocation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AllocationDaysReqView_01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)

        {
            string fun = @" 
DROP VIEW IF EXISTS AllocationDaysReqView;
Create view AllocationDaysReqView as SELECT req.""JobCode"",           
            req.""JobName"",
            res.""EmailId"",
            resDetails.""EmpName"",
            res.""RequisitionId"",
            resAlloc.""IsPerDayAllocation"",
            req.""Description"",
            req.""IsPerDayHourAllocation"",
            req.""IsActive"",
            res.""AllocationDate"",            
            req.""Designation"",
            res.""Id"" as ""ResAlloctionId"",
			res.""Efforts"" 
            FROM ""PublishedResAllocDays"" res
            JOIN ""Requisition"" req ON req.""Id"" = res.""RequisitionId""
	        JOIN ""PublishedResAllocDetails"" resDetails on req.""Id"" = resDetails.""RequisitionId""
	        JOIN ""PublishedResAlloc"" resAlloc on req.""Id"" = resDetails.""RequisitionId""
	
	";
            migrationBuilder.Sql(fun);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sp = @"DROP VIEW AllocationDaysReqView;";
            migrationBuilder.Sql(sp);
        }
    }
}
