using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Allocation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AllocationReqView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@" create view ResourceAllocationRequistionView as
            SELECT  req.""JobCode"",  ""JobName"", ""EmpEmail""
        , ""EmpName"", ""RequisitionId""
, req.""Description"", 
        ""TotalEffort"", ""AllocationStatus"", res.""IsActive"",   res.""EndDate""
        , res.""StartDate"",  req.""Designation""
     FROM ""PublishedResAllocDetails"" as res
    Inner Join ""Requisition"" as req
    On  ""req"".""Id"" = ""res"".""Id"";
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            drop view ResourceAllocationRequistionView;");
        }
    }
}
