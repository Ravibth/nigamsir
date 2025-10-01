using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Allocation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class sp_sp_allocationday_view : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sp = @"CREATE OR REPLACE PROCEDURE public.sp_allocationday_view(
				IN timeoptionname text,
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
   					create temp table temp_allocation(monthname timestamp without time zone, totaltime bigint, designation text,cost double precision );
		
										 BEGIN
						 IF timeoptionname = 'weekly' THEN
			 					INSERT INTO temp_allocation
  								SELECT DATE_TRUNC('week',v.""AllocationDate"") AS monthname, SUM(v.""Efforts""),v.""Designation"",
								SUM(allocation.""RatePerHour"" * v.""Efforts"") AS cost
  								FROM public.""allocationdaysreqview"" as v
								INNER JOIN ""PublishedResAlloc""  as allocation
								ON  allocation.""Id"" = v.""ResAlloctionId""
								INNER JOIN ""PublishedResAllocDetails""  as details
								ON  allocation.""PublishedResAllocDetailsId"" = details.""Id""
								JOIN ""Requisition"" req ON req.""Id"" = details.""RequisitionId""
								where req.""JobCode""=injobcode AND req.""PipelineCode""=inpipelinecode 
								AND v.""AllocationDate"" >= startdate
								AND v.""AllocationDate"" <= enddate AND v.""IsActive"" = true AND details.""IsActive"" = true 
								AND details.""AllocationStatus"" IN ('Employee Allocation Accepted By Reviewer', 'Employee Allocation Accepted By Employee','Employee Allocation Reviewer Accepted Resource Requestor Rejected Employee Rejection')
  								GROUP BY v.""Designation"",monthname;
					
						 ELSIF timeoptionname = 'monthly'  THEN
                 				INSERT INTO temp_allocation
  								SELECT DATE_TRUNC('month',v.""AllocationDate"") AS monthname, SUM(v.""Efforts""),v.""Designation"",
								SUM(allocation.""RatePerHour"" * v.""Efforts"") AS cost
  								FROM public.""allocationdaysreqview"" as v
								INNER JOIN ""PublishedResAlloc""  as allocation
								ON  allocation.""Id"" = v.""ResAlloctionId""
								INNER JOIN ""PublishedResAllocDetails""  as details
								ON  allocation.""PublishedResAllocDetailsId"" = details.""Id""
								JOIN ""Requisition"" req ON req.""Id"" = details.""RequisitionId""
								where req.""JobCode""=injobcode AND req.""PipelineCode""=inpipelinecode 
								AND v.""AllocationDate"" >= startdate
								AND v.""AllocationDate"" <= enddate AND v.""IsActive"" = true AND details.""IsActive"" = true  
								AND details.""AllocationStatus"" IN ('Employee Allocation Accepted By Reviewer', 'Employee Allocation Accepted By Employee','Employee Allocation Reviewer Accepted Resource Requestor Rejected Employee Rejection')
GROUP BY v.""Designation"",monthname;	
					
						 ELSIF timeoptionname = 'daily'  THEN
			 					INSERT INTO temp_allocation
             					SELECT DATE_TRUNC('month',v.""AllocationDate"") AS monthname, SUM(v.""Efforts""),v.""Designation"",
								SUM(allocation.""RatePerHour"" * v.""Efforts"") AS cost
  								FROM public.""allocationdaysreqview"" as v
								INNER JOIN ""PublishedResAlloc""  as allocation
								ON  allocation.""Id"" = v.""ResAlloctionId""
								INNER JOIN ""PublishedResAllocDetails""  as details
								ON  allocation.""PublishedResAllocDetailsId"" = details.""Id""
								JOIN ""Requisition"" req ON req.""Id"" = details.""RequisitionId""
								where req.""JobCode""=injobcode AND req.""PipelineCode""=inpipelinecode 
								AND v.""AllocationDate"" >= startdate
								AND v.""AllocationDate"" <= enddate AND v.""IsActive"" = true AND details.""IsActive"" = true  
								AND details.""AllocationStatus"" IN ('Employee Allocation Accepted By Reviewer', 'Employee Allocation Accepted By Employee','Employee Allocation Reviewer Accepted Resource Requestor Rejected Employee Rejection')
  								GROUP BY v.""Designation"",monthname;
					
							END IF;
						END;
	
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
            drop procedure sp_allocationday_view;");
        }
    }
}
