-- View: public.employee_allocation_timesheet

-- DROP MATERIALIZED VIEW IF EXISTS public.employee_allocation_timesheet;

CREATE MATERIALIZED VIEW IF NOT EXISTS public.employee_allocation_timesheet
TABLESPACE pg_default
AS
 WITH allocation AS (
         SELECT allocation.email_id,
            allocation.allocation_date,
            allocation.allocation_hours,
            allocation.pipeline_code,
            allocation.job_code,
            allocation.allocated_cost
           FROM dblink(' host = localhost user = postgres password =qwert dbname =AllocationDB-Dev01'::text, '

                select "rad"."EmpEmail" "email_id", "rad"."ConfirmedAllocationStartDate"::Date "allocation_date", SUM(COALESCE("rad"."ConfirmedPerDayHours",0)) "allocation_hours",
                "rad"."PipelineCode" "pipeline_code",
                "rad"."JobCode" "job_code",
                SUM(COALESCE("ra"."RatePerHour",0) * COALESCE("rad"."ConfirmedPerDayHours",0)) "allocated_cost"

                from "ResourceAllocationDays" "rad"
                left join "ResourceAllocation" "ra" on "ra"."PipelineCode" = "rad"."PipelineCode" and   "ra"."JobCode" = "rad"."JobCode" and  "ra"."EmpEmail" = "rad"."EmpEmail" and "rad"."ConfirmedAllocationStartDate"::Date between "ra"."ConfirmedAllocationStartDate"::Date and "ra"."ConfirmedAllocationEndDate"::Date
                group by "rad"."ConfirmedAllocationStartDate"::Date, "rad"."JobCode","rad"."PipelineCode", "rad"."EmpEmail"
    
                '::text) allocation(email_id text, allocation_date date, allocation_hours numeric, pipeline_code text, job_code text, allocated_cost numeric)
        ), holidays AS (
         SELECT holidays.holiday_date,
            holidays.location_name,
            holidays.holiday_type
           FROM dblink('host=localhost user=postgres password=qwert dbname=WCGTDB-Dev01'::text, '
    
                select "holiday_date", "location_name", "holiday_type"
                from "Holidays"

                '::text) holidays(holiday_date date, location_name text, holiday_type text)
        ), leaves AS (
         SELECT leaves.emp_email,
            leaves.leave_start_date,
            leaves.leave_end_date
           FROM dblink('host=localhost user=postgres password=qwert dbname=WCGTDB-Dev01'::text, '

                select "emp_email", "leave_start_date", "leave_end_date"
                from "Leaves"

                '::text) leaves(emp_email text, leave_start_date date, leave_end_date date)
        ), timesheet AS (
         SELECT timesheet.employeecode,
            timesheet.datelog,
            timesheet.actual_log_hours,
            timesheet.actual_cost,
            timesheet.job_chargeable_cost,
            timesheet.job_non_chargeable_cost,
            timesheet.job_chargeable_hours,
            timesheet.job_non_chargeable_hours
           FROM dblink('host=localhost user=postgres password=qwert dbname=WCGTDB-Dev01'::text, '

                select "employeecode", "datelog",
                SUM(COALESCE("totaltime", 0)) "actual_log_hours",
                SUM(COALESCE("rate", 0) * COALESCE("totaltime", 0)) "actual_cost",
                SUM(CASE
                    WHEN "chargeableflag" = ''Chargeable'' then COALESCE("totaltime",0) * COALESCE("rate",0)
                    ELSE 0 END) as "job_chargeable_cost",

                    SUM(CASE
                    WHEN "chargeableflag" != ''Chargeable'' then COALESCE("totaltime",0) * COALESCE("rate",0)
                    ELSE 0 END) as "job_non_chargeable_cost",

                                        SUM(CASE
                    WHEN "chargeableflag" = ''Chargeable'' then COALESCE("totaltime",0)
                    ELSE 0 END) as "job_chargeable_hours",

                    SUM(CASE
                    WHEN "chargeableflag" != ''Chargeable'' then COALESCE("totaltime",0)
                    ELSE 0 END) as "job_non_chargeable_hours"

                from "Timesheet"

                group by "employeecode", "datelog"

                '::text) timesheet(employeecode text, datelog date, actual_log_hours numeric, actual_cost numeric, job_chargeable_cost numeric, job_non_chargeable_cost numeric, job_chargeable_hours numeric, job_non_chargeable_hours numeric)
        ), employee_with_timesheet AS (
         SELECT employee_time_sheet.employee_mid,
            employee_time_sheet.employee_code,
            employee_time_sheet.employee_name,
            employee_time_sheet.email_id,
            employee_time_sheet.department,
            employee_time_sheet.location,
            employee_time_sheet.working_date,
            employee_time_sheet.designation_name,
            employee_time_sheet.business_unit,
            employee_time_sheet.expertise,
            employee_time_sheet.sme_group_name,
            employee_time_sheet.hourly_rate
           FROM dblink('host=localhost user=postgres password=qwert dbname=WCGTDB-Dev01'::text, '

                select "emp"."employee_mid" "employee_mid", "emp"."employee_code" "employee_code", "emp"."name" "employee_name",
                "emp"."email_id" "email_id", "emp"."department" "department", "lc"."location_name" "location", "wd"."working_date" "working_date",
                "dg"."designation_name" "designation_name", "bu"."bu" "business_unit", "expertise"."expertise" "expertise", "smeg"."sme_group" "sme_group_name",
                    COALESCE((select "RatePerHour" from "RateDesignationMaster" "rmd" where "rmd"."grade" = "dg"."designation_name"),0) as "hourly_rate"

                from "view_employees_report" "emp"

                cross join "working_days" "wd"

                left join "Locations" "lc" on  "lc"."location_id" = "emp"."location_id"

                left join "Designations" "dg" on  "dg"."designation_id" = "emp"."designation_id"

                left join (Select distinct bu_id, bu from "BUTreeMappings" as TempBU) as "bu" on  "bu"."bu_id" = "emp"."business_unit_id"
                left join (Select distinct expertise_id, expertise from "BUTreeMappings" as TempEXPERTISE) "expertise" on  "expertise"."expertise_id" = "emp"."expertise_id"
                left join (Select distinct sme_group_id, sme_group from "BUTreeMappings" as TempSMEG) "smeg" on  "smeg"."sme_group_id" = "emp"."smeg_id"

                '::text) employee_time_sheet(employee_mid text, employee_code text, employee_name text, email_id text, department text, location text, working_date date, designation_name text, business_unit text, expertise text, sme_group_name text, hourly_rate numeric)
        )
 SELECT ets.employee_mid,
    ets.employee_code,
    ets.employee_name,
    ets.email_id,
    ets.department,
    ets.location,
    ets.working_date,
    ets.designation_name,
    ets.business_unit,
    ets.expertise,
    ets.sme_group_name,
    ets.hourly_rate,
    "all".allocation_date,
    "all".allocation_hours,
    "all".pipeline_code,
    "all".job_code,
    "all".allocated_cost,
    ts.actual_log_hours,
    ts.job_chargeable_cost,
    ts.job_chargeable_hours,
    ts.job_non_chargeable_cost,
    ts.job_non_chargeable_hours,
    ts.actual_cost,
        CASE
            WHEN (( SELECT count(*) AS count
               FROM holidays
              WHERE holidays.holiday_type = 'Fixed'::text AND holidays.holiday_date = ets.working_date)) > 0 THEN 0
            WHEN (( SELECT count(*) AS count
               FROM leaves
              WHERE ets.email_id = leaves.emp_email AND ets.working_date >= leaves.leave_start_date AND ets.working_date <= leaves.leave_end_date)) > 0 THEN 0
            ELSE 8
        END AS capacity
   FROM employee_with_timesheet ets
     LEFT JOIN allocation "all" ON "all".email_id = ets.email_id AND "all".allocation_date = ets.working_date
     LEFT JOIN timesheet ts ON ts.datelog = ets.working_date AND ts.employeecode = ets.employee_code
WITH DATA;
