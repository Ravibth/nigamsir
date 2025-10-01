using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Allocation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class sc_workflow_changes_1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sp = @"CREATE OR REPLACE PROCEDURE public.sp_allocationday_resource_view(
	IN inpipelinecode text,
	IN injobcode text,
	IN startdate date,
	IN enddate date,
	OUT response jsonb)
LANGUAGE 'plpgsql'
AS $BODY$



							declare
								employees_availability_row Record;
							begin
									DROP table IF EXISTS temp_allocation;
   									create temp table temp_allocation(EmpEmail text, EmpName text, totaltime bigint,cost double precision );
	
									INSERT INTO temp_allocation
										SELECT details.""EmpEmail"",details.""EmpName"",  SUM(res.""Efforts"") as totaltime,
					SUM(res.""RatePerHour"" * (res.""Efforts"")) AS cost
					FROM public.""PublishedResAllocDays"" as res
					INNER JOIN ""PublishedResAlloc""   as allocation
					ON  allocation.""Id"" = res.""PublishedResAllocId""
					INNER JOIN ""PublishedResAllocDetails""   as details
					ON  allocation.""PublishedResAllocDetailsId"" = details.""Id""
					INNER JOIN ""Requisition""   as req 
					on req.""Id"" = res.""RequisitionId""
												where res.""JobCode""=injobcode AND res.""PipelineCode""=inpipelinecode AND res.""AllocationDate"" >=startdate AND 
												res.""AllocationDate"" <= enddate AND details.""AllocationStatus"" IN ('Employee Allocation Accepted By Reviewer', 'Employee Allocation Accepted By Supercoach' , 'Employee Allocation Accepted By Employee','Employee Allocation Supercoach Accepted Resource Requestor Rejected Employee Rejection')
												AND details.""IsActive""=true AND details.""IsActive""=true
  												GROUP BY details.""EmpEmail"",details.""EmpName"";
	
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
            migrationBuilder.Sql(@"drop procedure sp_allocationday_resource_view");
        }
    }
}
