
CREATE OR REPLACE PROCEDURE public.sp_jobcode_update(
	IN pipeline_code text,
	IN job_code text,
	IN new_pipeline_code text,	
	IN new_job_code text,
	IN new_job_name text,
	IN modified_by text)
LANGUAGE 'plpgsql'
AS $$
declare 
	jobCode text;	
	resource_allocation_details_rows RECORD;
	new_requision_id integer;
	new_resource_allocation_details_id integer;
	new_resource_allocation_id integer;
	success BOOLEAN := FALSE;
	cdate Timestamp := CURRENT_DATE;
	yesterday timestamp := Current_Date -1;
	total_allocation integer := 0;
BEGIN
	BEGIN
    
	 RAISE NOTICE 'update ResourceAllocation';
	/************************** Start Loop ********************************/
	for resource_allocation_details_rows in 
			select * from public."ResourceAllocation" 
				where "PipelineCode" = pipeline_code and "JobCode" = job_code 
					and "IsActive" = true and "ConfirmedAllocationStartDate" < cdate  and "ConfirmedAllocationEndDate" >  cdate 
                  
	loop
	   RAISE NOTICE 'allocations_items.RequisitionId %', resource_allocation_details_rows."RequisitionId";
	  /************************* insert requisitions ********************************/
	  Insert into public."Requisition"("PipelineCode", "JobCode", "JobName", "PipelineName", "RequisitionDescription", "IsContinuousAllocation", "TotalHours", 
						"RequisitionStatus", "Expertise", "SME", "Designation", "Description", "IsActive", "CreatedBy", "ModifiedBy", "RequisitionDemand", "ClientName", 
						"EffortsPerDay", "EndDate", "StartDate", "BU", "SMEG", "isPerDayHourAllocation", "CreatedAt", "ModifiedAt", "SuspendedAt", "isPublish", "RequisitionTypeId", 
						"Industry", "SubIndustry")
		select new_pipeline_code, new_job_code, new_job_name, "PipelineName", "RequisitionDescription", "IsContinuousAllocation", "TotalHours", 
				"RequisitionStatus", "Expertise", "SME", "Designation", "Description", "IsActive", "CreatedBy", modified_by, "RequisitionDemand", "ClientName", 
				"EffortsPerDay", "EndDate", cdate, "BU", "SMEG", "isPerDayHourAllocation", "CreatedAt", "ModifiedAt", "SuspendedAt", "isPublish", "RequisitionTypeId", 
				"Industry", "SubIndustry" from public."Requisition" 
				where "RequisionId" = resource_allocation_details_rows."RequisitionId"
	   RETURNING "RequisionId" INTO new_requision_id;

	   /**************************** update Requistion Table ****************************/
	   update public."Requisition" set "EndDate" = cdate where "RequisionId" = resource_allocation_details_rows."RequisitionId";
	   
	   /************************** add new resourceAllocation Details *****************************/
	   	total_allocation:= 	(SELECT sum("ConfirmedPerDayHours")
			FROM public."ResourceAllocationDays" WHERE "RequisitionId" = resource_allocation_details_rows."RequisitionId" and  "ConfirmedAllocationStartDate" > yesterday);

	   Insert into public."ResourceAllocationDetails"("PipelineCode", "JobCode", "JobName", "PipelineName", "EmpEmail", "EmpName", 
	   					"RequisitionId", "RecordType", "IsContinuousAllocation", "Description", "TotalEffort", "AllocationStatus", "CreatedDate", 
						"ModifiedDate", "CreatedBy", "ModifiedBy", "IsActive", guid, "AllocationEndDate", "AllocationStartDate", "SuspendedAt", 
						"PreviousGuid", "ConfirmedAllocationDate")
			SELECT  new_pipeline_code, new_job_code, new_job_name, "PipelineName", "EmpEmail", "EmpName", new_requision_id, "RecordType", 
						"IsContinuousAllocation", "Description", total_allocation, "AllocationStatus", "CreatedDate", "ModifiedDate", "CreatedBy", "ModifiedBy", 
						"IsActive", guid, "AllocationEndDate", "AllocationStartDate", "SuspendedAt", "PreviousGuid", "ConfirmedAllocationDate"
			FROM public."ResourceAllocationDetails" where "Id" = resource_allocation_details_rows."ResAllocDetailsId"
			RETURNING "Id" INTO new_resource_allocation_details_id;

				
		RAISE NOTICE 'ResourceAllocation->total_allocation %', total_allocation;
	   /************************** add new record ResourceAllocation ******************************/
	   Insert into public."ResourceAllocation"("PipelineCode", "JobCode", "JobName", "EmpEmail", "EmpName", "ConfirmedAllocationStartDate", 
											   "ConfirmedAllocationEndDate", "ConfirmedPerDayHours", "RequisitionId", "AllocationStatus", 
											   "RecordType", "CreatedDate", "ModifiedDate", "CreatedBy", "ModifiedBy", "IsActive", "PipelineName", 
											   "ResAllocDetailsId", "TotalWorkingDays", "ClientName", "ResourceAllocationDetailsId", 
											   "isPerDayHourAllocation", "SuspendedAt", "isPublish", "ResourceAllocationDetailsHistoryId", "RatePerHour")
	   	select new_pipeline_code, new_job_code, new_job_name, "EmpEmail", "EmpName", cDate,  "ConfirmedAllocationEndDate", total_allocation, 
												new_requision_id, "AllocationStatus", "RecordType", "CreatedDate", "ModifiedDate", "CreatedBy", 
												modified_by, "IsActive", "PipelineName", new_resource_allocation_details_id, "TotalWorkingDays", "ClientName", 
												"ResourceAllocationDetailsId", "isPerDayHourAllocation", "SuspendedAt", "isPublish", 
												"ResourceAllocationDetailsHistoryId", "RatePerHour"
	   	from public."ResourceAllocation" where "Id" = resource_allocation_details_rows."Id"	

	    RETURNING "Id" INTO new_resource_allocation_id;		
		/************************** update ResourceAllocationDays ******************************/
		RAISE NOTICE 'resource_allocation_details_rows.RequisitionId %', resource_allocation_details_rows."Id";
		RAISE NOTICE 'yesterday %', yesterday;
		total_allocation:= 0;
	    Update public."ResourceAllocationDays" set "ResAlloctionId" = new_resource_allocation_id, "JobCode" = new_job_code, "PipelineCode" = new_pipeline_code,
	  			"RequisitionId" = new_requision_id
	   			where "ResAlloctionId" = resource_allocation_details_rows."Id" and "ConfirmedAllocationStartDate" > yesterday;
		/************************** update ResourceAllocation ******************************/
		RAISE NOTICE 'total_allocation:%',yesterday;
		
		total_allocation:= 	(SELECT sum("ConfirmedPerDayHours")
			FROM public."ResourceAllocationDays" WHERE "RequisitionId" =  resource_allocation_details_rows."RequisitionId" and  "ConfirmedAllocationStartDate" <= yesterday);
			
		RAISE NOTICE 'total_allocation-1: %', resource_allocation_details_rows."Id";
		
	    Update public."ResourceAllocation" set  "ConfirmedAllocationEndDate" = yesterday, "ConfirmedPerDayHours" = total_allocation 
		where "Id" = resource_allocation_details_rows."Id";
		
		Update public."ResourceAllocationDetails" set "TotalEffort"= total_allocation where "RequisitionId" = resource_allocation_details_rows."RequisitionId";

		/************************** update ResourceAllocation ******************************/
		 
	 END loop;
	 
	 Update "Requisition" set "EndDate" = cdate where "PipelineCode" = pipeline_code and "JobCode" = job_code and "IsActive" = true 
	 and "StartDate" < cdate  and "EndDate" >  cdate;
	 success := TRUE;
	 
	 UPDATE public."ResourceAllocationDetails"  
			set "PipelineCode"= new_pipeline_code,  "JobCode" = new_job_code, 
			 "JobName" = new_job_name,  
			 "ModifiedBy" = modified_by 
			FROM public."ResourceAllocation"
			WHERE public."ResourceAllocationDetails"."RequisitionId" = public."ResourceAllocation"."RequisitionId" and
			public."ResourceAllocation"."PipelineCode" = pipeline_code and public."ResourceAllocation"."JobCode" = job_code 
			and public."ResourceAllocation"."IsActive" = true 
			and public."ResourceAllocation"."ConfirmedAllocationStartDate" >= Current_date;		
	
	/********************* Update ResourceAllocation future date allocation ********************************/
    Update "ResourceAllocation" set "JobCode" = new_job_code, "JobName" = new_job_name where "IsActive" = true and "ConfirmedAllocationStartDate" > cdate 
        and "PipelineCode" = pipeline_code and "JobCode" = job_code; 
		
	Update  public."ResourceAllocationDays" set "PipelineCode"=new_pipeline_code, "JobCode" = new_job_code, "JobName" = new_job_name, "ModifiedBy" = modified_by  
	 				where "PipelineCode" = pipeline_code and "JobCode" = job_code
					and "IsActive" = true and "ConfirmedAllocationStartDate" > yesterday;
  
	 EXCEPTION
        WHEN others THEN
            -- If an error occurs, rollback the transaction and handle the error
            RAISE NOTICE 'An error occurred: %', SQLERRM;
         ROLLBACK;		 
    END;
	 IF success THEN
            -- If successful, commit the transaction
            COMMIT;
            RAISE NOTICE 'Transaction committed successfully.';
        ELSE
            -- If not successful, rollback the transaction
            ROLLBACK;
            RAISE NOTICE 'Transaction rolled back.';
        END IF;
End;
$$;
 
