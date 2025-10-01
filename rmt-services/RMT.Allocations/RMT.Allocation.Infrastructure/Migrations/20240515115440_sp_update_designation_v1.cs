using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Allocation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class sp_update_designation_v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sp = @"create or replace procedure public.sp_update_designation_cost(
				IN emp_details jsonb,
				OUT response jsonb
			)
			language 'plpgsql'
			as $$
			declare
				NewRequisionId int := 0;
				PersentRequisionId int := 0;
				ResourceAllocationId int := 0;
				ResourceAllocationDetailsId int := 0;
				employees_availability_row Record;
				rec   Record;
				allocationRec   Record;
			begin
   	
				DROP table IF EXISTS temp_allocation;
				DROP table IF EXISTS temp_emp;
				DROP table IF EXISTS temp_requisition;
	

				create temp table temp_emp as
				select *
				from json_to_recordset(emp_details ::json)
				as x(""EmpEmail"" text, ""Designation"" text,""RatePerHour"" double precision,""UpdateDate"" Timestamp);
	
				create temp table temp_allocation as 
				SELECT ""e"".""UpdateDate"",""e"".""Designation"",""Id"", ""PipelineCode"", ""JobCode"", ""JobName"", ""allocation"".""EmpEmail"", ""EmpName"", ""ConfirmedAllocationStartDate"", ""ConfirmedAllocationEndDate"", ""ConfirmedPerDayHours"", ""RequisitionId"", ""AllocationStatus"", ""RecordType"", ""CreatedDate"", ""ModifiedDate"", ""CreatedBy"", ""ModifiedBy"", ""IsActive"", ""PipelineName"", ""ResAllocDetailsId"", ""TotalWorkingDays"", ""ClientName"", ""ResourceAllocationDetailsId"", ""isPerDayHourAllocation"", ""SuspendedAt"", ""isPublish"",""e"".""RatePerHour""
				from ""ResourceAllocation"" as allocation
				INNER JOIN temp_emp AS e ON 
				 allocation.""EmpEmail""=e.""EmpEmail"" and allocation.""IsActive"" = true ;
	
	
				create temp table temp_requisition as 
				SELECT  ""allocation"".""UpdateDate"",""allocation"".""Designation"", ""Requisition"".""RequisionId"", ""Requisition"".""PipelineCode"", ""Requisition"".""JobCode"",
				""Requisition"".""RequisitionDescription"", ""Requisition"".""IsContinuousAllocation"", ""Requisition"".""TotalHours"", ""Requisition"".""RequisitionStatus"", ""Requisition"".""Expertise"", 
				""Requisition"".""SME"", ""Requisition"".""Description"", ""Requisition"".""IsActive"", ""Requisition"".""CreatedBy"", ""Requisition"".""ModifiedBy"", ""Requisition"".""RequisitionDemand"", ""Requisition"".""ClientName"", ""Requisition"".""EffortsPerDay"", ""EndDate"", ""StartDate"", ""BU"", ""SMEG"", ""Requisition"".""isPerDayHourAllocation"", ""CreatedAt"", ""ModifiedAt"", ""Requisition"".""SuspendedAt"", ""Requisition"".""isPublish"", ""Requisition"".""RequisitionType"", ""Requisition"".""RequisitionTypeId""
				from ""Requisition"" 
				INNER JOIN temp_allocation AS allocation ON 
				 allocation.""RequisitionId"" = ""Requisition"".""RequisionId"" and ""Requisition"".""IsActive"" = true ;
	
				-- fetch new RequisionId
				select (MAX(""RequisionId"")+1) into NewRequisionId  from ""Requisition"";

    				 FOR rec IN
					 SELECT *
					 FROM   temp_requisition ORDER BY ""RequisionId""
					LOOP
					  PersentRequisionId := rec.""RequisionId"";
		 
		 				UPDATE ""Requisition""
						SET   ""EndDate"" = rec.""UpdateDate"",
		 				 ""Designation"" = rec.""Designation""
						WHERE  rec.""RequisionId"" = ""Requisition"".""RequisionId"" AND ""EndDate"" > rec.""UpdateDate"";
			
		
					 rec.""RequisionId"" = NewRequisionId;
		 
					 INSERT INTO ""Requisition"" (""RequisionId"",""PipelineCode"", ""JobCode"", ""RequisitionDescription"", ""IsContinuousAllocation"", ""TotalHours"", ""RequisitionStatus"", ""Expertise"",
										   ""SME"", ""Designation"", ""Description"", ""IsActive"", ""CreatedBy"", ""ModifiedBy"", ""RequisitionDemand"", ""ClientName"", ""EffortsPerDay"", ""EndDate"", ""StartDate"", ""BU"", ""SMEG"", ""isPerDayHourAllocation"", ""CreatedAt"", ""ModifiedAt"", ""SuspendedAt"", ""isPublish"", ""RequisitionType"", ""RequisitionTypeId"")
					 VALUES(rec.""RequisionId"",rec.""PipelineCode"", rec.""JobCode"", rec.""RequisitionDescription"", rec.""IsContinuousAllocation"", rec.""TotalHours"", rec.""RequisitionStatus"", rec.""Expertise"",
									   rec.""SME"", rec.""Designation"", rec.""Description"", rec.""IsActive"", rec.""CreatedBy"", rec.""ModifiedBy"", rec.""RequisitionDemand"", rec.""ClientName"", rec.""EffortsPerDay"", rec.""EndDate"", rec.""UpdateDate""+ interval '1' day, rec.""BU"", rec.""SMEG"", rec.""isPerDayHourAllocation"", rec.""CreatedAt"", rec.""ModifiedAt"", rec.""SuspendedAt"", rec.""isPublish"", rec.""RequisitionType"", rec.""RequisitionTypeId"");
		
		
				  FOR allocationRec IN
					 SELECT *
					 FROM   temp_allocation 
					 where  temp_allocation.""ConfirmedAllocationEndDate"" > temp_allocation.""UpdateDate""
					 ORDER BY ""Id""
					LOOP
       				select (MAX(""Id"")+1) into ResourceAllocationId  from ""ResourceAllocation"";
					  UPDATE ""ResourceAllocation""
						SET   ""ConfirmedAllocationEndDate"" = allocationRec.""UpdateDate""
						WHERE  ""ResourceAllocation"".""RequisitionId"" = PersentRequisionId and ""ConfirmedAllocationEndDate"" > allocationRec.""UpdateDate"";
			
					INSERT INTO  ""ResourceAllocation""
					(""Id"", ""PipelineCode"", ""JobCode"", ""JobName"", ""EmpEmail"", ""EmpName"", ""ConfirmedAllocationStartDate"", ""ConfirmedAllocationEndDate"", ""ConfirmedPerDayHours"", ""RequisitionId"", ""AllocationStatus"", ""RecordType"", ""CreatedDate"", ""ModifiedDate"", ""CreatedBy"", ""ModifiedBy"", ""IsActive"", ""PipelineName"", ""ResAllocDetailsId"", ""TotalWorkingDays"",
					 ""ClientName"", ""ResourceAllocationDetailsId"", ""isPerDayHourAllocation"", ""SuspendedAt"", ""isPublish"", ""RatePerHour"")
					 VALUES(ResourceAllocationId, allocationRec.""PipelineCode"", allocationRec.""JobCode"", allocationRec.""JobName"", allocationRec.""EmpEmail"", allocationRec.""EmpName"", allocationRec.""UpdateDate"", allocationRec.""ConfirmedAllocationEndDate"", allocationRec.""ConfirmedPerDayHours"", 
							NewRequisionId, allocationRec.""AllocationStatus"", allocationRec.""RecordType"", allocationRec.""CreatedDate"",allocationRec.""ModifiedDate"", allocationRec.""CreatedBy"", allocationRec.""ModifiedBy"", allocationRec.""IsActive"", allocationRec.""PipelineName"", allocationRec.""ResAllocDetailsId"", allocationRec.""TotalWorkingDays"",
					 allocationRec.""ClientName"", allocationRec.""ResourceAllocationDetailsId"", allocationRec.""isPerDayHourAllocation"", allocationRec.""SuspendedAt"", allocationRec.""isPublish"", allocationRec.""RatePerHour"");
		
		
					UPDATE ""ResourceAllocationDays""
					SET ""RequisitionId"" = NewRequisionId ,
						""ResAlloctionId"" = ResourceAllocationId
					WHERE ""ConfirmedAllocationStartDate"" > allocationRec.""UpdateDate"";
		
					 ResourceAllocationId := ResourceAllocationId + 1;
					END LOOP;
		
		
					NewRequisionId:= NewRequisionId+1; 
    				END LOOP;
		
				response := '{}';
				for employees_availability_row in
					select *
					from temp_allocation
				loop
					response := jsonb_set(response, ARRAY[employees_availability_row.""EmpEmail""]
										 ,to_jsonb(employees_availability_row.""RatePerHour"")
										 );
				end loop;
	
			end
			$$;";
            migrationBuilder.Sql(sp);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@" drop procedure sp_update_designation_cost;");
        }
    }
}
