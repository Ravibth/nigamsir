using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Allocation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update_designation_sp_update3107 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sp = @"CREATE OR REPLACE PROCEDURE public.sp_update_designation_cost(
	IN emp_details jsonb,
	OUT response jsonb
)
LANGUAGE 'plpgsql'
AS $BODY$
DECLARE
	NewRequisitionId UUID := uuid_generate_v4();
	PresentRequisitionId UUID := uuid_generate_v4();
	ResourceAllocationDetailsId UUID := uuid_generate_v4();
	RunningResourceAllocationDetailsId UUID := uuid_generate_v4();
	ResourceAllocationId UUID := uuid_generate_v4();
	employees_availability_row RECORD;
	rec RECORD;
	allocationRec RECORD;
	allocationDetailsRec RECORD;
BEGIN
-- 	Drop temporary tables if they exist
	DROP TABLE IF EXISTS temp_allocation;
	DROP TABLE IF EXISTS temp_emp;
	DROP TABLE IF EXISTS temp_requisition;

-- 	Create temp table for employee details
	CREATE TEMP TABLE temp_emp AS
	SELECT *
	FROM json_to_recordset(emp_details::json)
	AS x(""EmpEmail"" TEXT, ""Designation"" TEXT, ""Grade"" TEXT, ""RatePerHour"" DOUBLE PRECISION, ""UpdateDate"" TIMESTAMP);

-- 	Create temp table for allocations
	CREATE TEMP TABLE temp_allocation AS 
	SELECT 
		allocation.""Id"" as allocid
		, DATE(e.""UpdateDate"") as ""UpdateDate""
		, e.""Designation""
		, e.""Grade""
		, e.""RatePerHour"" as newrateperhour 
		, Req.""Id"" AS RequisitionId
		, Req.""PipelineCode""
		, Req.""PipelineName""
		, Req.""JobCode""
		, Req.""JobName""
		, ResAllocDetails.""EmpEmail""
		, ResAllocDetails.""EmpName""
		, allocation.""StartDate""
		, allocation.""EndDate""
		, ResAllocDetails.""TotalEffort""
		, ""AllocationStatus""
		, ResAllocDetails.""CreatedAt""
		, ResAllocDetails.""ModifiedAt""
		, ResAllocDetails.""CreatedBy""
		, ResAllocDetails.""ModifiedBy""
		, ResAllocDetails.""IsActive""
		, ResAllocDetails.""Id"" as detailsid
		, ""TotalWorkingDays""
		, Req.""ClientName""
		, allocation.""IsPerDayAllocation""
		, allocation.""RatePerHour""
		,allocation.""Efforts""
	FROM ""PublishedResAlloc"" AS allocation
	INNER JOIN ""Requisition"" AS Req 
		ON Req.""Id"" = allocation.""RequisitionId""
	INNER JOIN ""PublishedResAllocDetails"" AS ResAllocDetails 
		ON ResAllocDetails.""RequisitionId"" = Req.""Id""
	INNER JOIN temp_emp AS e 
		ON ResAllocDetails.""EmpEmail"" = e.""EmpEmail"" AND ResAllocDetails.""IsActive"" = TRUE
	WHERE 
		LOWER(TRIM(Req.""Grade"")) != LOWER(TRIM(e.""Grade""))
		AND allocation.""EndDate"" >= DATE(e.""UpdateDate"");
	
	RAISE NOTICE 'temp_allocation contents:-1'; 

-- 	Create temp table for requisitions
	CREATE TEMP TABLE temp_requisition AS 
	SELECT 
		DISTINCT(details.""Id"") as detailsid
		, allocation.""UpdateDate""
		, allocation.""Designation""
		, allocation.""Grade""
		, allocation.newrateperhour
		, Req.""Id""
		, Req.""PipelineCode""
		, Req.""PipelineName"" 
		, Req.""JobCode""
		, Req.""JobName""
		, Req.""Description""
		, Req.""TotalHours""
		, Req.""RequisitionStatus""
		, Req.""Expertise""
		, Req.""SMEG""
		, Req.""IsActive""
		, Req.""CreatedBy""
		, Req.""ModifiedBy""
		, Req.""RequisitionDemand""
		, Req.""EffortsPerDay""
		, details.""EndDate""
		, details.""StartDate""
		, Req.""BusinessUnit""
		, Req.""IsPerDayHourAllocation""
		, Req.""CreatedAt""
		, Req.""ModifiedAt""
		, Req.""RequisitionTypeId""
		,Req.""ClientName""
	FROM ""Requisition"" AS Req
	INNER JOIN ""PublishedResAllocDetails"" details
		on details.""RequisitionId"" = Req.""Id"" AND details.""IsActive"" = TRUE
	INNER JOIN temp_allocation AS allocation 
		ON allocation.""requisitionid"" = Req.""Id"" AND Req.""IsActive"" = TRUE
	WHERE 
		Req.""EndDate"" >= allocation.""UpdateDate"";
	
RAISE NOTICE 'temp_allocation contents:-1.1';
	
-- 	Fetch new RequisitionId
	NewRequisitionId := uuid_generate_v4();
	RAISE NOTICE 'temp_allocation contents:-2'; 

	FOR rec IN
		SELECT * 
		FROM temp_requisition 
		ORDER BY ""Id""
	LOOP
		PresentRequisitionId := rec.""Id"";
		allocationDetailsRec := NULL;
		
		IF (SELECT COUNT(*) FROM ""Requisition""
			WHERE 
				""Id"" = rec.""Id"" 
				AND ""StartDate"" < rec.""UpdateDate""
				AND ""EndDate"" < rec.""UpdateDate"") > 0
		THEN 
			CONTINUE;
		
		ELSEIF (SELECT COUNT(*) FROM ""Requisition""
			WHERE 
				""Id"" = rec.""Id"" 
				AND ""StartDate"" >= rec.""UpdateDate""
				AND ""EndDate"" >= rec.""UpdateDate"") > 0
		THEN
			UPDATE ""Requisition""
			SET 
				""Grade"" = rec.""Grade""
				, ""Designation"" = rec.""Designation""
			WHERE 
				""Id"" = rec.""Id"" 
				AND ""StartDate"" >= rec.""UpdateDate""
				AND ""EndDate"" >= rec.""UpdateDate"";
				
			UPDATE ""RequisitionDemand""
			SET ""TotalDemands"" = ""TotalDemands"" - 1
			WHERE ""Id"" = rec.""RequisitionDemand"";
			
			
			UPDATE ""PublishedResAlloc""
			SET ""RatePerHour"" = rec.newrateperhour
			WHERE ""RequisitionId"" = rec.""Id"";
			
			UPDATE ""PublishedResAllocDays""
			SET ""RatePerHour"" = rec.newrateperhour
			WHERE ""RequisitionId"" = rec.""Id"";
			
		ELSE
		
		
			UPDATE ""Requisition""
			SET ""EndDate"" = rec.""UpdateDate"" - INTERVAL '1' DAY
			WHERE 
				""Id"" = rec.""Id"" 
				AND ""EndDate"" >= rec.""UpdateDate"";

			RAISE NOTICE 'temp_allocation contents:-3';
			INSERT INTO ""Requisition"" (
				""Id"", ""PipelineCode"", ""JobCode"", ""Description"", ""TotalHours""
				, ""RequisitionStatus"", ""Expertise"", ""SMEG"", ""Designation""
				, ""Grade"" ,""IsActive"", ""CreatedBy"", ""ModifiedBy""
				, ""RequisitionDemand"", ""ClientName"", ""EffortsPerDay""
				, ""EndDate"", ""StartDate"", ""BusinessUnit""
				, ""IsPerDayHourAllocation"", ""CreatedAt""
				, ""ModifiedAt"", ""RequisitionTypeId"",""PipelineName"",""JobName""
			)
			VALUES (
				NewRequisitionId, rec.""PipelineCode"", rec.""JobCode"", rec.""Description"", rec.""TotalHours""
				, rec.""RequisitionStatus"", rec.""Expertise"", rec.""SMEG"", rec.""Designation""
				, rec.""Grade"" ,rec.""IsActive"", rec.""CreatedBy"", rec.""ModifiedBy""
				, rec.""RequisitionDemand"", rec.""ClientName"", rec.""EffortsPerDay""
				, rec.""EndDate"", rec.""UpdateDate"" , rec.""BusinessUnit""
				, rec.""IsPerDayHourAllocation"", rec.""CreatedAt""
				, rec.""ModifiedAt"", rec.""RequisitionTypeId"",rec.""PipelineName"",rec.""JobName""
			);
			
			
			INSERT INTO ""RequisitionParameters"" (
    			""Category"",
				""Id"",
				""RequisitionId"",
				""RequisitionWeight"",
				""IsChecked""
			)
			SELECT
				reqparam.""Category"",
				uuid_generate_v4(),            
				NewRequisitionId,             
				reqparam.""RequisitionWeight"",
				reqparam.""IsChecked""
			FROM
				public.""RequisitionParameters"" reqparam
			WHERE
				reqparam.""RequisitionId"" = PresentRequisitionId;
			
			INSERT INTO ""RequisitionParameterValues""(
				""Id""
				,""RequisitionId""
				,""Parameter""
				,""Value""
			)
			SELECT 
				uuid_generate_v4(), NewRequisitionId, reqparamvalue.""Parameter"",reqparamvalue.""Value""
			FROM 
				public.""RequisitionParameterValues"" reqparamvalue
			WHERE 
				reqparamvalue.""RequisitionId"" = PresentRequisitionId;
			
			
			INSERT INTO ""RequisitionSkill""(
				""Id""
				,""RequisitionId""
				,""SkillName""
				,""SkillCode""
				,""Type""
			)
			SELECT 
				uuid_generate_v4(), NewRequisitionId, reqskill.""SkillName"",reqskill.""SkillCode"",reqskill.""Type""
			FROM 
				public.""RequisitionSkill"" reqskill
			WHERE 
				reqskill.""RequisitionId"" = PresentRequisitionId;

			SELECT * FROM ""PublishedResAllocDetails"" into allocationDetailsRec
			WHERE 
				""RequisitionId"" = PresentRequisitionId
				AND ""EndDate"" >= rec.""UpdateDate""
				AND ""IsActive"" = true;

			IF allocationDetailsRec IS NOT NULL
			THEN
				ResourceAllocationDetailsId := uuid_generate_v4();

				UPDATE ""PublishedResAllocDetails""
				SET 
					""EndDate"" = rec.""UpdateDate"" - INTERVAL '1' DAY
					, ""IsUpdated"" = true
				WHERE 
					""RequisitionId"" = PresentRequisitionId 
					AND ""EndDate"" >= rec.""UpdateDate"";

				INSERT INTO ""PublishedResAllocDetails"" (
					""Id"", ""RequisitionId"",""EmpEmail"",""EmpName"" 
					, ""Description"", ""TotalEffort"", ""AllocationStatus""
					, ""StartDate"", ""EndDate"", ""ConfirmedAllocationDate""
					, ""IsActive"", ""CreatedBy"", ""ModifiedBy"",""CreatedAt""
					, ""ModifiedAt"", ""AllocationVersion"", ""IsUpdated""
				)
				VALUES (
					ResourceAllocationDetailsId, NewRequisitionId , allocationDetailsRec.""EmpEmail"", allocationDetailsRec.""EmpName""
					, allocationDetailsRec.""Description"", allocationDetailsRec.""TotalEffort"", allocationDetailsRec.""AllocationStatus""
					, rec.""UpdateDate"", allocationDetailsRec.""EndDate"" , allocationDetailsRec.""ConfirmedAllocationDate""
					, TRUE, allocationDetailsRec.""CreatedBy"", allocationDetailsRec.""ModifiedBy"", allocationDetailsRec.""CreatedAt""
					, allocationDetailsRec.""ModifiedAt"", allocationDetailsRec.""AllocationVersion"", allocationDetailsRec.""IsUpdated""
				);
				
				INSERT INTO ""PublishedResAllocSkillEntity""(
						""Id""
						,""RequisitionId""
						,""PublishedResAllocDetailsId""
						,""SkillName""
						,""SkillCode""
					)
				SELECT 
					uuid_generate_v4(), NewRequisitionId, ResourceAllocationDetailsId,pubskill.""SkillName"",pubskill.""SkillCode""
				FROM 
					public.""PublishedResAllocSkillEntity"" pubskill
				WHERE 
					pubskill.""RequisitionId"" = PresentRequisitionId;

				FOR allocationRec IN
					SELECT * FROM temp_allocation 
					WHERE 
						""EndDate"" > ""UpdateDate"" 
						AND ""requisitionid"" = PresentRequisitionId
					ORDER BY detailsid
				LOOP
					ResourceAllocationId := uuid_generate_v4();

					UPDATE ""PublishedResAlloc""
					SET 
						""EndDate"" = allocationRec.""UpdateDate"" - INTERVAL '1' DAY
					WHERE 
						""RequisitionId"" = PresentRequisitionId 
						AND ""StartDate"" < allocationRec.""UpdateDate""
						AND ""EndDate"" >= allocationRec.""UpdateDate"";

					INSERT INTO ""PublishedResAlloc"" (
						""Id"", ""StartDate"", ""EndDate"", ""RequisitionId""
						, ""PublishedResAllocDetailsId"", ""TotalWorkingDays""
						, ""IsPerDayAllocation"", ""RatePerHour"",""Efforts""
					)
					VALUES (
						ResourceAllocationId
						, GREATEST(allocationRec.""StartDate"",allocationRec.""UpdateDate"")
						,allocationRec.""EndDate"", NewRequisitionId
						, ResourceAllocationDetailsId, allocationRec.""TotalWorkingDays""
						, allocationRec.""IsPerDayAllocation"", allocationRec.newrateperhour,allocationRec.""Efforts""
					);

					UPDATE ""PublishedResAllocDays""
					SET ""RequisitionId"" = NewRequisitionId,
						""PublishedResAllocId"" = ResourceAllocationId,
						""RatePerHour"" = allocationRec.newrateperhour
					WHERE ""AllocationDate"" >= allocationRec.""UpdateDate"";

					UPDATE ""PublishedResAlloc"" resalloc
					SET 
						""Efforts"" = COALESCE((
								SELECT SUM(daystable.""Efforts"") 
								FROM ""PublishedResAllocDays"" daystable
								WHERE daystable.""PublishedResAllocId"" = resalloc.""Id""
						),0)
					WHERE 
						resalloc.""IsPerDayAllocation"" != true
						AND	(
							resalloc.""RequisitionId"" = PresentRequisitionId 
							OR	resalloc.""RequisitionId"" = NewRequisitionId 
						);


					DELETE FROM ""PublishedResAlloc""
					WHERE 
						""Id"" = allocationRec.allocid
						AND ""RequisitionId"" = PresentRequisitionId 
						AND ""StartDate"" > allocationRec.""UpdateDate"";

				END LOOP;

				UPDATE ""PublishedResAllocDetails"" details
				SET 
					""TotalEffort"" = (COALESCE((
										SELECT SUM(alloctable.""Efforts"") 
										FROM ""PublishedResAlloc"" alloctable
										WHERE 
											alloctable.""PublishedResAllocDetailsId"" = details.""Id""
											AND alloctable.""IsPerDayAllocation"" = false
									),0)+
									 COALESCE((
										SELECT SUM(daystable.""Efforts"") 
										FROM ""PublishedResAlloc"" alloctable
										INNER JOIN ""PublishedResAllocDays"" daystable 
											ON daystable.""PublishedResAllocId"" = alloctable.""Id""
										WHERE 
											alloctable.""PublishedResAllocDetailsId"" = details.""Id""
											AND alloctable.""IsPerDayAllocation"" = true
									 ),0))
				WHERE
					details.""RequisitionId"" = PresentRequisitionId 
					OR	details.""RequisitionId"" = NewRequisitionId ;
					
				UPDATE ""Requisition"" requsitionitem
				SET ""TotalHours"" = COALESCE((
										SELECT details.""TotalEffort""
										FROM ""PublishedResAllocDetails"" details
										WHERE 
											details.""RequisitionId"" = requsitionitem.""Id""
											AND details.""IsActive"" = true
									),0)
				WHERE 
					""Id"" = PresentRequisitionId
					OR ""Id"" = NewRequisitionId;
				
			END IF;			
		END IF;			
			
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

$BODY$;";
            migrationBuilder.Sql(sp);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@" drop procedure sp_update_designation_cost;");
        }
    }
}
