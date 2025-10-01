using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Allocation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class sp_allocation_designation_view : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sp = @"CREATE OR REPLACE PROCEDURE public.sp_allocation_designation_view(
				IN injobcode text,
				IN inpipelinecode text,
				IN RatePerHour double precision,
				OUT response jsonb)
			LANGUAGE 'plpgsql'
			AS $BODY$
					declare
						employees_availability_row Record;
					begin
					DROP table IF EXISTS temp_allocation;
   					create temp table temp_allocation(PipelineCode text,JobCode text, Designation text, TotalEffort bigint,cost double precision );
	
					INSERT INTO temp_allocation
					select res.""JobCode"",res.""PipelineCode"",req.""Designation"",SUM(det.""TotalEffort"") AS TotalEffort, SUM((det.""TotalEffort"")*res.""RatePerHour"") AS cost
					from public.""Requisition""  as req
					INNER JOIN public.""ResourceAllocation"" as res
					ON req.""RequisionId"" = res.""RequisitionId""
					INNER JOIN public.""ResourceAllocationDetails"" as det
					ON det.""Id"" = res.""ResAllocDetailsId""
					WHERE res.""JobCode"" = injobcode AND res.""PipelineCode"" = inpipelinecode
					GROUP BY res.""JobCode"",res.""PipelineCode"",req.""Designation"";
	
					select to_jsonb(json_agg(temp_allocation.*)) 
							into response
							from temp_allocation;
	
					end
		
			$BODY$;	";
            migrationBuilder.Sql(sp);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@" drop procedure sp_allocation_designation_view;");
        }
    }
}
