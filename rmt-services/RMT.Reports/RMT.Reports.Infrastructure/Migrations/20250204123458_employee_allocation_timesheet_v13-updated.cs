using Microsoft.EntityFrameworkCore.Migrations;
using RMT.Reports.Infrastructure.Helpers;

#nullable disable

namespace RMT.Reports.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class employee_allocation_timesheet_v13updated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string ConnStr_AllocationDB = ConfigConstants.AllocationDbConnStr;
            string ConnStr_WcgtDB = ConfigConstants.WcgtDbConnStr;
            string ConnStr_ConfigDB = ConfigConstants.ConfigDbConnStr;

            string cmd = @$"
                DROP INDEX IF EXISTS employee_allocation_index;

                DROP MATERIALIZED VIEW IF EXISTS employee_allocation_timesheet;

         CREATE MATERIALIZED VIEW IF NOT EXISTS employee_allocation_timesheet TABLESPACE pg_default AS WITH allocation AS (
    SELECT allocation.email_id,
        allocation.allocation_date,
        allocation.allocation_hours,
        allocation.pipeline_code,
        allocation.job_code,
        allocation.competency_id,
        allocation.competency_name,
        allocation.allocated_cost,
        allocation.chargable
    FROM dblink('{ConnStr_AllocationDB}'::text, '

                                 SELECT radays.""EmailId"" as ""email_id"", radays.""AllocationDate""::date as ""allocation_date"", SUM(COALESCE(radays.""Efforts"",0)) as ""allocation_hours"",
                                 req.""PipelineCode"" as ""pipeline_code"", 
                                 req.""JobCode"" as ""job_code"", 
									            req.""CompetencyId"" as ""competency_id"", 
									            req.""Competency"" as ""competency_name"",											
                                 SUM(COALESCE(radays.""RatePerHour"",0) * COALESCE(radays.""Efforts"",0)) as ""allocated_cost"" 
                                 ,
                                 CASE
                                     WHEN lower(req.""PipelineCode"") = lower(req.""JobCode"") THEN ''N''
                                     ELSE ''Y''
                                 END as ""chargable""
                                 FROM ""PublishedResAllocDays"" as radays
                                 join ""Requisition"" as req
                                 on radays.""RequisitionId"" = req.""Id"" and req.""IsActive"" = true
                                 join ""PublishedResAlloc"" as ""ra""
                                 on ra.""Id"" = radays.""PublishedResAllocId"" and radays.""AllocationDate""::Date between ra.""StartDate""::date and ra.""EndDate""::date
                                             group by radays.""AllocationDate""::Date, req.""JobCode"",req.""PipelineCode"",req.""CompetencyId"",req.""Competency"", radays.""EmailId""

                                             '::text
        ) allocation(
            email_id text,
            allocation_date date,
            allocation_hours numeric,
            pipeline_code text,
            job_code text,
            competency_id text,
            competency_name text,
            allocated_cost numeric,
            chargable text
        )
),
timesheet AS (
    SELECT timesheet.employeecode,
        timesheet.datelog,
        timesheet.actual_log_hours,
        timesheet.actual_cost,
        timesheet.job_chargeable_cost,
        timesheet.job_non_chargeable_cost,
        timesheet.job_chargeable_hours,
        timesheet.job_non_chargeable_hours
     FROM dblink('{ConnStr_WcgtDB}'::text, '

                                 SELECT ""employeecode"", ""datelog"",
                                 SUM(COALESCE(""totaltime"", 0)) ""actual_log_hours"",
                                 SUM(COALESCE(""rate"", 0) * COALESCE(""totaltime"", 0)) ""actual_cost"",
                                 SUM(CASE
                                     WHEN ""chargeableflag"" = ''Chargeable'' then COALESCE(""totaltime"",0) * COALESCE(""rate"",0)
                                     ELSE 0 END) as ""job_chargeable_cost"",

                                     SUM(CASE
                                     WHEN ""chargeableflag"" != ''Chargeable'' then COALESCE(""totaltime"",0) * COALESCE(""rate"",0)
                                     ELSE 0 END) as ""job_non_chargeable_cost"",

                                                         SUM(CASE
                                     WHEN ""chargeableflag"" = ''Chargeable'' then COALESCE(""totaltime"",0)
                                     ELSE 0 END) as ""job_chargeable_hours"",

                                     SUM(CASE
                                     WHEN ""chargeableflag"" != ''Chargeable'' then COALESCE(""totaltime"",0)
                                     ELSE 0 END) as ""job_non_chargeable_hours""

                                 FROM ""Timesheet""

                                 group by ""employeecode"", ""datelog""

                                 '::text
        ) timesheet(
            employeecode text,
            datelog date,
            actual_log_hours numeric,
            actual_cost numeric,
            job_chargeable_cost numeric,
            job_non_chargeable_cost numeric,
            job_chargeable_hours numeric,
            job_non_chargeable_hours numeric
        )
),
holidays AS (
    SELECT holidays.holiday_date,
        holidays.location_name,
        holidays.holiday_type,
        holidays.holiday_location_id,
        holidays.holiday_location_name
     FROM dblink('{ConnStr_WcgtDB}'::text, '

                                 SELECT hdays.""holiday_date"", hdays.""location_name"", hdays.""holiday_type"", hdays.""location_id"" as holiday_location_id, loc.""location_name"" as holiday_location_name
                                 FROM ""Holidays"" as hdays
                                 left join ""Locations"" as loc
                                 ON hdays.location_id = loc.location_id
                                 Where hdays.isactive = true

                                 '::text
        ) holidays(
            holiday_date date,
            location_name text,
            holiday_type text,
            holiday_location_id text,
            holiday_location_name text
        )
),
leaves AS (
    SELECT leaves.emp_mid,
        leaves.leave_start_date,
        leaves.leave_end_date,
		leaves.start_date_half,
		leaves.end_date_half
     FROM dblink('{ConnStr_WcgtDB}'::text, '

                                 SELECT ""emp_mid"", ""leave_start_date"", ""leave_end_date"",""start_date_half"",""end_date_half""
                                 FROM ""Leaves""
                                 Where ""isactive"" = true

                                 '::text
        ) leaves(
            emp_mid text,
            leave_start_date date,
            leave_end_date date,
			start_date_half text,
			end_date_half text
        )
)
SELECT ets.employee_mid,
    ets.employee_code,
    ets.employee_name,
    ets.email_id,
    ets.department,
    ets.location,
    ets.working_date,
    ets.designation_name,
    ets.grade,
    ets.business_unit,
    ets.competency,
    ets.hourly_rate,
    "" all "".allocation_date,
    "" all "".allocation_hours,
    "" all "".pipeline_code,
    "" all "".job_code,
    "" all "".competency_id,
    "" all "".competency_name,
    "" all "".allocated_cost,
    "" all "".chargable,
    CASE
        WHEN lower("" all "".chargable) = lower('y'::text) THEN "" all "".allocation_hours
        ELSE 0::numeric
    END AS allocated_chargable_hr,
    CASE
        WHEN lower("" all "".chargable) = lower('y'::text) THEN "" all "".allocated_cost
        ELSE 0::numeric
    END AS allocated_chargable_cost,
    CASE
        WHEN lower("" all "".chargable) = lower('n'::text) THEN "" all "".allocation_hours
        ELSE 0::numeric
    END AS allocated_non_chargable_hr,
    CASE
        WHEN lower("" all "".chargable) = lower('n'::text) THEN "" all "".allocated_cost
        ELSE 0::numeric
    END AS allocated_non_chargable_cost,
    ts.actual_log_hours,
    ts.job_chargeable_cost,
    ts.job_chargeable_hours,
    ts.job_non_chargeable_cost,
    ts.job_non_chargeable_hours,
    ts.actual_cost,
    hd.holiday_date,
    hd.location_name,
    hd.holiday_type,
    lv.emp_mid,
    lv.leave_start_date,
    lv.leave_end_date,
   CASE
	   WHEN hd.holiday_date IS NOT NULL THEN 0
	   WHEN lv.leave_start_date IS NOT NULL THEN 0
	   WHEN ets.proposed_lwd IS NOT NULL 
			AND ets.proposed_lwd < (ets.working_date + COALESCE((SELECT value  FROM dblink('{ConnStr_ConfigDB}'::text, 
				'SELECT ""Value"" FROM ""ApplicationLevelSettings"" WHERE ""Key"" = ''ResignedUserAvailabilityThreshold'' AND ""IsActive"" = true'::text
			) AS applevelsettings(value text))::int, 0) * INTERVAL '1 day') THEN 0
	   WHEN ets.proposed_lwd IS NULL AND ets.resignation_date IS NOT NULL 
			AND ets.resignation_date < (ets.working_date + COALESCE((SELECT value  FROM dblink('{ConnStr_ConfigDB}'::text, 
				'SELECT ""Value"" FROM ""ApplicationLevelSettings"" WHERE ""Key"" = ''ResignedUserAvailabilityThreshold'' AND ""IsActive"" = true'::text
			) AS applevelsettings(value text))::int, 0) * INTERVAL '1 day') THEN 0
	   ELSE 8
   END AS capacity,
    CASE
        WHEN hd.holiday_date IS NOT NULL THEN 8
        WHEN lv.leave_start_date IS NOT NULL 
		THEN 
			leaves_calculator(
					ets.working_date
					, lv.leave_start_date
					, lv.leave_end_date
					, lv.start_date_half
					, lv.end_date_half
				)
        ELSE 0
    END AS leave_hrs,
	CASE
        WHEN hd.holiday_date IS NOT NULL THEN 0
        WHEN lv.leave_start_date IS NOT NULL 
			THEN 
				leaves_calculator(
					ets.working_date
					, lv.leave_start_date
					, lv.leave_end_date
					, lv.start_date_half
					, lv.end_date_half
				) -- call function
        ELSE 0
    END AS net_leaves
FROM employee_working_days ets
    LEFT JOIN allocation "" all "" ON (
        lower("" all "".email_id) = lower(ets.email_id)
        OR lower("" all "".email_id) = lower((ets.employee_mid || '__'::text) || ets.email_id)
    )
    AND "" all "".allocation_date = ets.working_date
    LEFT JOIN timesheet ts ON ts.datelog = ets.working_date
    AND ts.employeecode = ets.employee_code
    LEFT JOIN holidays hd ON hd.holiday_date = ets.working_date
    AND hd.holiday_location_name = ets.location
    LEFT JOIN leaves lv ON ets.employee_mid = lv.emp_mid
    AND ets.working_date >= lv.leave_start_date
    AND ets.working_date <= lv.leave_end_date WITH DATA;
            ";


            migrationBuilder.Sql(cmd);

            migrationBuilder.Sql(
               @$"DROP INDEX IF EXISTS employee_allocation_index;
                CREATE UNIQUE INDEX employee_allocation_index ON employee_allocation_timesheet USING btree (
    working_date,
    employee_code,
    employee_mid,
    email_id,
    business_unit,
    location,
    designation_name,
    grade,
    pipeline_code,
    job_code,
    allocation_hours,
    actual_log_hours,
    allocated_cost
) "
               );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP INDEX IF EXISTS employee_allocation_index;");
            migrationBuilder.Sql("DROP MATERIALIZED VIEW IF EXISTS employee_allocation_timesheet;");
        }
    }
}