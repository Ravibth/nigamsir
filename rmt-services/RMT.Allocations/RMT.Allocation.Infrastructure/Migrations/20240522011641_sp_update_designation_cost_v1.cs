using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Allocation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class sp_update_designation_cost_v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sp = @"
                CREATE OR REPLACE PROCEDURE public.sp_update_designation_cost(
				IN emp_details jsonb,
				OUT response jsonb)
			LANGUAGE 'plpgsql'
			AS $BODY$

			DECLARE
				NewRequisitionId UUID := uuid_generate_v4();
				PresentRequisitionId UUID := uuid_generate_v4();
				ResourceAllocationId UUID := uuid_generate_v4();
				employees_availability_row RECORD;
				rec RECORD;
				allocationRec RECORD;
			BEGIN
				-- Drop temporary tables if they exist
				DROP TABLE IF EXISTS temp_allocation;
				DROP TABLE IF EXISTS temp_emp;
				DROP TABLE IF EXISTS temp_requisition;

				-- Create temp table for employee details
				CREATE TEMP TABLE temp_emp AS
				SELECT *
				FROM json_to_recordset(emp_details::json)
				AS x(""EmpEmail"" TEXT, ""Designation"" TEXT, ""RatePerHour"" DOUBLE PRECISION, ""UpdateDate"" TIMESTAMP);

				-- Create temp table for allocations
				CREATE TEMP TABLE temp_allocation AS 
				SELECT e.""UpdateDate"", e.""Designation"", Req.""Id"" AS RequisitionId, Req.""PipelineCode"", Req.""JobCode"", Req.""JobName"", 
					   ResAllocDetails.""EmpEmail"", ResAllocDetails.""EmpName"", 
					   allocation.""StartDate"", allocation.""EndDate"", ResAllocDetails.""TotalEffort"", ""AllocationStatus"", 
					   ResAllocDetails.""CreatedAt"", ResAllocDetails.""ModifiedAt"", ResAllocDetails.""CreatedBy"", ResAllocDetails.""ModifiedBy"", 
					   ResAllocDetails.""IsActive"", Req.""PipelineName"", ResAllocDetails.""Id"", ""TotalWorkingDays"", ""ClientName"", 
					   allocation.""IsPerDayAllocation"", ResAllocDays.""RatePerHour""
				FROM ""PublishedResAlloc"" AS allocation
				INNER JOIN ""Requisition"" AS Req ON Req.""Id"" = allocation.""RequisitionId""
				INNER JOIN ""PublishedResAllocDetails"" AS ResAllocDetails ON ResAllocDetails.""RequisitionId"" = Req.""Id""
				INNER JOIN ""PublishedResAllocDays"" AS ResAllocDays ON ResAllocDays.""RequisitionId"" = Req.""Id""
				INNER JOIN temp_emp AS e ON ResAllocDetails.""EmpEmail"" = e.""EmpEmail"" AND ResAllocDetails.""IsActive"" = TRUE;
	
				RAISE NOTICE 'temp_allocation contents:-1'; 

				-- Create temp table for requisitions
				CREATE TEMP TABLE temp_requisition AS 
				SELECT allocation.""UpdateDate"", allocation.""Designation"", 
					   Req.""Id"", Req.""PipelineCode"", Req.""JobCode"", Req.""JobName"", Req.""Description"", Req.""TotalHours"", 
					   Req.""RequisitionStatus"", 
					   Req.""Expertise"", Req.""SMEG"", Req.""IsActive"", Req.""CreatedBy"", Req.""ModifiedBy"", Req.""RequisitionDemand"", 
					   Req.""EffortsPerDay"", allocation.""EndDate"", allocation.""StartDate"", Req.""BusinessUnit"", 
					   Req.""IsPerDayHourAllocation"", Req.""CreatedAt"", Req.""ModifiedAt"", Req.""RequisitionTypeId""
				FROM ""Requisition"" AS Req
				INNER JOIN temp_allocation AS allocation ON allocation.""requisitionid"" = Req.""Id"" AND Req.""IsActive"" = TRUE;
			RAISE NOTICE 'temp_allocation contents:-1.1';
				-- Fetch new RequisitionId
				NewRequisitionId := uuid_generate_v4();
				RAISE NOTICE 'temp_allocation contents:-2'; 

				FOR rec IN
					SELECT * FROM temp_requisition ORDER BY ""Id""
				LOOP
					PresentRequisitionId := rec.""Id"";
		
					UPDATE ""Requisition""
					SET ""EndDate"" = rec.""UpdateDate"",
						""Designation"" = rec.""Designation""
					WHERE ""Id"" = rec.""Id"" AND ""EndDate"" > rec.""UpdateDate"";

					rec.""RequisitionId"" = NewRequisitionId;
				RAISE NOTICE 'temp_allocation contents:-3';
					INSERT INTO ""Requisition"" (""Id"", ""PipelineCode"", ""JobCode"", ""Description"", ""TotalHours"", 
											   ""RequisitionStatus"", ""Expertise"", ""SMEG"", ""Designation"", ""IsActive"", ""CreatedBy"", 
											   ""ModifiedBy"", ""RequisitionDemand"", ""ClientName"", ""EffortsPerDay"", ""EndDate"", ""StartDate"", 
											   ""BusinessUnit"", ""IsPerDayHourAllocation"", ""CreatedAt"", ""ModifiedAt"", ""RequisitionTypeId"")
					VALUES (NewRequisitionId, rec.""PipelineCode"", rec.""JobCode"", rec.""Description"", rec.""TotalHours"", rec.""RequisitionStatus"", 
							rec.""Expertise"", rec.""SMEG"", rec.""Designation"", rec.""IsActive"", rec.""CreatedBy"", rec.""ModifiedBy"", 
							rec.""RequisitionDemand"", rec.""ClientName"", rec.""EffortsPerDay"", rec.""EndDate"", rec.""UpdateDate"" + INTERVAL '1' DAY, 
							rec.""BusinessUnit"", rec.""IsPerDayHourAllocation"", rec.""CreatedAt"", rec.""ModifiedAt"", rec.""RequisitionTypeId"");

					FOR allocationRec IN
						SELECT * FROM temp_allocation WHERE ""EndDate"" > ""UpdateDate"" ORDER BY ""Id""
					LOOP
						ResourceAllocationId := uuid_generate_v4();

						UPDATE ""PublishedResAlloc""
						SET ""EndDate"" = allocationRec.""UpdateDate""
						WHERE ""RequisitionId"" = PresentRequisitionId AND ""EndDate"" > allocationRec.""UpdateDate"";

						INSERT INTO ""PublishedResAlloc"" (""Id"", ""StartDate"", ""EndDate"", ""RequisitionId"", ""PublishedResAllocDetailsId"", 
														 ""TotalWorkingDays"", ""IsPerDayAllocation"", ""RatePerHour"")
						VALUES (ResourceAllocationId, allocationRec.""StartDate"", allocationRec.""UpdateDate"" + INTERVAL '1' DAY, 
								NewRequisitionId, allocationRec.""Id"", allocationRec.""TotalWorkingDays"", 
								allocationRec.""IsPerDayAllocation"", allocationRec.""RatePerHour"");

						UPDATE ""PublishedResAllocDays""
						SET ""RequisitionId"" = NewRequisitionId,
							""PublishedResAllocId"" = ResourceAllocationId
						WHERE ""AllocationDate"" > allocationRec.""UpdateDate"";
					END LOOP;
				RAISE NOTICE 'temp_allocation contents:-4';
					NewRequisitionId := uuid_generate_v4();
				END LOOP;

				response := '{}';
				RAISE NOTICE 'temp_allocation contents:-5';
				FOR employees_availability_row IN
					SELECT * FROM temp_allocation
				LOOP
					response := jsonb_set(response, ARRAY[employees_availability_row.""EmpEmail""], to_jsonb(employees_availability_row.""RatePerHour""));
				END LOOP;
			END;

			$BODY$;

";
            migrationBuilder.Sql(sp);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@" drop procedure sp_update_designation_cost;");

        }
    }
}
