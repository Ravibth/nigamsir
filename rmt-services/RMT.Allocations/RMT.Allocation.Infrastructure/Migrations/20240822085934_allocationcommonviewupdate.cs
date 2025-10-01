using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Allocation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class allocationcommonviewupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sp = @"CREATE OR REPLACE VIEW public.allocation_common_view
 AS
  SELECT ""UnPublishedResAllocDays"".""Id"",
    ""UnPublishedResAllocDays"".""RequisitionId"",
    ""UnPublishedResAllocDays"".""UnPublishedResAllocId"",
    ""UnPublishedResAllocDays"".""Efforts"",
    ""UnPublishedResAllocDays"".""EmailId"",
    ""UnPublishedResAllocDays"".""PipelineCode"",
    ""UnPublishedResAllocDays"".""JobCode"",
    ""UnPublishedResAllocDays"".""RatePerHour"",
    ""UnPublishedResAllocDays"".""Currency"",
    ""UnPublishedResAllocDays"".""AllocationDate"",
    'u'::text AS ""Type"" ,
	""UnPublishedResAllocDetails"".""AllocationStatus""
	FROM ""UnPublishedResAllocDays""
	INNER JOIN ""UnPublishedResAlloc""
		ON ""UnPublishedResAllocDays"".""UnPublishedResAllocId"" = ""UnPublishedResAlloc"".""Id""
	INNER JOIN ""UnPublishedResAllocDetails"" 
		ON ""UnPublishedResAllocDetails"".""Id"" = ""UnPublishedResAlloc"".""UnPublishedResAllocDetailsId""
UNION
 SELECT ""PublishedResAllocDays"".""Id"",
    ""PublishedResAllocDays"".""RequisitionId"",
    ""PublishedResAllocDays"".""PublishedResAllocId"" AS ""UnPublishedResAllocId"",
    ""PublishedResAllocDays"".""Efforts"",
    ""PublishedResAllocDays"".""EmailId"",
    ""PublishedResAllocDays"".""PipelineCode"",
    ""PublishedResAllocDays"".""JobCode"",
    ""PublishedResAllocDays"".""RatePerHour"",
    ""PublishedResAllocDays"".""Currency"",
    ""PublishedResAllocDays"".""AllocationDate"",
    'p'::text AS ""Type"",
	""PublishedResAllocDetails"".""AllocationStatus""
	FROM ""PublishedResAllocDays""
	INNER JOIN ""PublishedResAlloc""
		ON ""PublishedResAllocDays"".""PublishedResAllocId"" = ""PublishedResAlloc"".""Id""
	INNER JOIN ""PublishedResAllocDetails"" 
		ON ""PublishedResAllocDetails"".""Id"" = ""PublishedResAlloc"".""PublishedResAllocDetailsId""
 	WHERE NOT (""PublishedResAllocDays"".""RequisitionId"" IN ( SELECT ""UnPublishedResAllocDays"".""RequisitionId""
           FROM ""UnPublishedResAllocDays""));
";
            migrationBuilder.Sql(sp);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"drop view allocation_common_view;");
        }
    }
}
