-- PROCEDURE: public.sp_timesheet_view(text, text, date, date, refcursor)

-- DROP PROCEDURE IF EXISTS public.sp_timesheet_view(text, text, date, date, refcursor);

CREATE OR REPLACE PROCEDURE public.sp_timesheet_view(
	IN timeoptionname text,
	IN injobcode text,
	IN startdate date,
	IN enddate date,
	INOUT _result_one refcursor DEFAULT 'rs_resultone'::refcursor)
LANGUAGE 'plpgsql'
AS $$
DECLARE
	timeoption text;
BEGIN
	IF timeoptionname = 'weekly' THEN
		timeoption := 'week';
	ELSIF timeoptionname = 'monthly'  THEN
		timeoption := 'month';
	ELSIF timeoptionname = 'daily'  THEN
		timeoption := 'day';
	END IF;
	
	OPEN _result_one FOR  
	SELECT 
		DATE_TRUNC(timeoption, pub_days."AllocationDate") AS monthname
		, COALESCE(SUM(pub_days."ActualEffort"::integer),0) AS totaltime
		, req."Designation" AS "Designation"
		, COALESCE(SUM((pub_days."ActualEffort"::integer) * pub_days."RatePerHour"::integer),0) AS timesheetcost
	FROM public."PublishedResAllocDays" pub_days
	JOIN public."Requisition" req
		ON req."Id" = pub_days."RequisitionId"
	WHERE 
		lower(pub_days."JobCode") = lower(injobcode) 
		AND pub_days."AllocationDate" >= startDate 
		AND pub_days."AllocationDate" <= endDate 
	GROUP BY req."Designation" , monthname;
END;               
$$;
-- ALTER PROCEDURE public.sp_timesheet_view(text, text, date, date, refcursor)
--     OWNER TO postgres;
