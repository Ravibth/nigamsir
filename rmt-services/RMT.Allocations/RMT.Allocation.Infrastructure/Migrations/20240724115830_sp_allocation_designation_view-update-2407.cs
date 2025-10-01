using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Allocation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class sp_allocation_designation_viewupdate2407 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sp = @"CREATE OR REPLACE PROCEDURE public.sp_allocation_designation_view(
	IN injobcode text,
	IN inpipelinecode text,
	IN rateperhour double precision,
	OUT response jsonb)
LANGUAGE 'plpgsql'
AS $BODY$
declare
	employees_availability_row Record;
begin
	DROP TABLE IF EXISTS temp_allocation;
	CREATE TEMP TABLE temp_allocation(
		PipelineCode text
		, JobCode text
		, Designation text
		, Grade text
		, TotalEffort bigint
		, cost double precision
	);
	
	INSERT INTO temp_allocation
	SELECT 
		req.""JobCode""
		, req.""PipelineCode""
		, req.""Designation""
		, req.""Grade""
		, SUM(det.""TotalEffort"") AS TotalEffort
		, SUM((det.""TotalEffort"")*res.""RatePerHour"") AS cost
	FROM public.""Requisition"" as req
	INNER JOIN public.""PublishedResAlloc"" as res
		ON req.""Id"" = res.""RequisitionId""
	INNER JOIN public.""PublishedResAllocDetails"" as det
		ON det.""Id"" = res.""PublishedResAllocDetailsId""
	WHERE 
		Lower(req.""JobCode"") = Lower(injobcode) 
		AND Lower(req.""PipelineCode"") = Lower(inpipelinecode) 
		AND req.""IsActive"" = true 
		AND det.""IsActive"" = true
		AND Lower(det.""AllocationStatus"") IN (
			'employee allocation accepted by reviewer'
			, 'employee allocation accepted by employee'
			,'employee allocation reviewer accepted resource requestor rejected rmployee rejection'
		)
	GROUP BY 
		req.""Grade""
		, req.""JobCode""
		, req.""PipelineCode""
		, req.""Designation"";
	
	select to_jsonb(json_agg(temp_allocation.*)) 
	into response
	from temp_allocation;
	
end
$BODY$;";
            migrationBuilder.Sql(sp);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
            drop procedure sp_allocation_designation_view;");
        }
    }
}
