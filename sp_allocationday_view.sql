CREATE OR REPLACE PROCEDURE public.sp_allocationday_view(
        IN timeoptionname text,
        IN inpipelinecode text,
        IN injobcode text,
        IN startdate date,
        IN enddate date,
        OUT response jsonb
) 
LANGUAGE 'plpgsql' AS $BODY$
declare 
    employees_availability_row Record;
    chartviewoption TEXT;
begin 
    DROP table IF EXISTS temp_allocation;
    create temp table temp_allocation(
        monthname timestamp without time zone,
        totaltime bigint,
        designation text,
                    grade text,
        cost double precision
    );
    BEGIN 
    IF timeoptionname = 'weekly' 
        THEN
            chartviewoption = 'week';
    ELSIF timeoptionname = 'monthly'
        THEN
            chartviewoption = 'month';
    ELSIF timeoptionname = 'daily'
        THEN
            chartviewoption = 'day';
        END IF;
    END;

	INSERT INTO temp_allocation
    SELECT 
        DATE_TRUNC(chartviewoption, v."AllocationDate") AS monthname,
        SUM(v."Efforts"),
        v."Designation",
        v."Grade",
        SUM(allocationdays."RatePerHour" * v."Efforts") AS cost
    FROM public."allocationdaysreqview" as v
    INNER JOIN "PublishedResAllocDays" as allocationdays 
        ON allocationdays."RequisitionId" = v."RequisitionId"
    INNER JOIN "PublishedResAlloc" as allocation 
        ON allocation."RequisitionId" = v."RequisitionId"
    INNER JOIN "PublishedResAllocDetails" as details 
        ON details."RequisitionId" = v."RequisitionId"
    JOIN "Requisition" req 
        ON req."Id" = v."RequisitionId"
    where 
        lower(req."JobCode") = lower(injobcode)
        AND lower(req."PipelineCode") = lower(inpipelinecode)
        AND v."AllocationDate" >= startdate
        AND v."AllocationDate" <= enddate
        AND v."IsActive" = true
        AND details."IsActive" = true
        AND lower(details."AllocationStatus") IN (
            'employee allocation accepted by reviewer',
            'employee allocation accepted by employee',
            'employee allocation reviewer accepted resource requestor rejected employee rejection'
        )
    GROUP BY 
        v."Designation",
        v."Grade",
        monthname;
    	
    select to_jsonb(json_agg(temp_allocation.*)) 
    into response
    from temp_allocation;
	
end 
$BODY$;