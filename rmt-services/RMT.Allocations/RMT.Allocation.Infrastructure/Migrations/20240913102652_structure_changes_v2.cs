using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Allocation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class structure_changes_v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string fun = @"DROP VIEW IF EXISTS AllocationDaysReqView;
            CREATE OR REPLACE VIEW public.allocationdaysreqview
            AS
            SELECT req.""JobCode"",
            req.""JobName"",
            resdetails.""EmpEmail"" as ""EmailId"",
            resdetails.""EmpName"",
            res.""RequisitionId"",
            resalloc.""IsPerDayAllocation"",
            resdetails.""Description"",
            req.""IsPerDayHourAllocation"",
            req.""IsActive"",
            res.""AllocationDate"",
            req.""Designation"",
            req.""Grade"",
            res.""Id"" AS ""ResAlloctionId"",
            res.""Efforts""
            FROM ""PublishedResAllocDays"" res
                LEFT JOIN ""Requisition"" req ON req.""Id"" = res.""RequisitionId""
                LEFT JOIN ""PublishedResAlloc"" resalloc ON resalloc.""RequisitionId"" = req.""Id""
                LEFT JOIN ""PublishedResAllocDetails"" resdetails ON resdetails.""RequisitionId"" = req.""Id""
            Where resdetails.""IsActive"" = true;";
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
