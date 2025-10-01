using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Allocation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class sp_resource_view_v1 : Migration
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
										SELECT res.""EmpEmail"",res.""EmpName"",  SUM(res.""ConfirmedPerDayHours"") as totaltime,
												SUM(allocation.""RatePerHour"" * (res.""ConfirmedPerDayHours"")) AS cost
  												FROM public.""ResourceAllocationDays"" as res
												INNER JOIN ""ResourceAllocation""   as allocation
												ON  allocation.""Id"" = res.""ResAlloctionId""
												INNER JOIN ""ResourceAllocationDetails""   as details
												ON  allocation.""ResAllocDetailsId"" = details.""Id""
												where res.""JobCode""=injobcode AND res.""PipelineCode""=inpipelinecode AND res.""ConfirmedAllocationStartDate"" >=startdate AND 
												res.""ConfirmedAllocationStartDate"" <= enddate AND details.""AllocationStatus"" IN ('Employee Allocation Accepted By Reviewer', 'Employee Allocation Accepted By Employee','Employee Allocation Reviewer Accepted Resource Requestor Rejected Employee Rejection')
												AND res.""IsActive""=true AND details.""IsActive""=true
  												GROUP BY res.""EmpEmail"",res.""EmpName"";
	
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
            migrationBuilder.Sql(@" drop procedure sp_allocationday_resource_view;");
        }
    }
}
