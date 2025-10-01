using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WCGT.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class availablityleavesandhpliday_sp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sp = @"
CREATE OR REPLACE PROCEDURE public.sp_getuserwithleavesholidaysandavailability(
	IN start_date date,
	IN end_date date,
	IN emp_mids text[],
	IN grade_filter text[],
	IN designation text[],
	IN super_coach_mid text[],
	IN co_super_coach_mid text[],
	INOUT var_resp json)
LANGUAGE 'plpgsql'
AS $BODY$

BEGIN
	
	create temp table employee_information(
		employee_mid varchar,
		email_id varchar,
		business_unit_id varchar,
		competencyId varchar,
		designation_id varchar,
		super_coach_mid varchar,
		co_super_coach_mid varchar,
		grade_name varchar,
		name varchar,
		working_date date,
		available_hrs int
	);

	CREATE temp TABLE leaves_temp_table(
			emp_mid varchar,
			emp_email varchar,
			max_leave_start_date date,
			min_leave_end_date date,
			holiday_between_leaves int,
			total_leave int
		);
	-- Get Employees List and insert into the temp table	
	INSERT INTO employee_information(
		employee_mid,
		email_id,
		business_unit_id,
		competencyId,
		designation_id,
		super_coach_mid,
		co_super_coach_mid,
		grade_name,
		name,
		working_date,
		available_hrs
	)
	(SELECT emp.employee_mid , emp.email_id , emp.business_unit_id , emp.""CompetencyId"" , emp.designation_id as designation_id , emp.supercoach_mid , emp.reporting_partner_mid , desig.grade as grade_name , emp.name  , working_date::date   ,8 as available_hrs  
			FROM ""Employees"" as emp
		LEFT JOIN ""Designations"" as desig on emp.designation_id = desig.designation_id
	CROSS JOIN
	(SELECT working_date FROM generate_series(start_date , end_date , '1 day'::interval) as wd(working_date)
		WHERE EXTRACT(DOW FROM working_date) NOT IN (0, 6)) AS filtered_dates
		WHERE ((emp.employee_mid = ANY (emp_mids)))
				AND (ARRAY_LENGTH(designation , 1) IS NULL OR emp.designation_id = ANY (designation) )
				AND (ARRAY_LENGTH(super_coach_mid , 1) IS NULL OR emp.supercoach_mid = ANY (super_coach_mid) )
				AND (ARRAY_LENGTH(co_super_coach_mid , 1) IS NULL OR emp.reporting_partner_mid = ANY (co_super_coach_mid) )
				AND (ARRAY_LENGTH(grade_filter , 1) IS NULL OR desig.grade = ANY (grade_filter) )
				
	);

  INSERT INTO leaves_temp_table(
            emp_mid,
			emp_email,
			max_leave_start_date,
			min_leave_end_date,
			holiday_between_leaves,
			total_leave 
   )
    -- insert data of leaves of each day and employee into information
   SELECT * FROM (
			SELECT
			l.emp_mid,
			l.emp_email,
			g.leave_date as max_leave_start_date,
			g.leave_date as min_leave_end_date,
			CASE
				WHEN g.leave_date = h.holiday_date THEN 8
				ELSE  0
			END holiday_between_leaves,
			CASE
				WHEN g.leave_date = h.holiday_date THEN 0
				ELSE
					(
						CASE
						-- For same day leave
						WHEN (g.leave_date = l.leave_start_date and l.leave_start_date::date = l.leave_end_date::date) THEN 
							CASE 
								WHEN l.start_date_half = 'N' AND l.end_date_half = 'N' THEN 8
        						WHEN l.start_date_half = 'F' AND l.end_date_half = 'N' THEN 4
        						WHEN l.start_date_half = 'S' AND l.end_date_half = 'N' THEN 4
        						WHEN l.start_date_half = 'N' AND l.end_date_half = 'F' THEN 4
        						WHEN l.start_date_half = 'F' AND l.end_date_half = 'F' THEN 4
        						WHEN l.start_date_half = 'S' AND l.end_date_half = 'F' THEN 0
        						WHEN l.start_date_half = 'N' AND l.end_date_half = 'S' THEN 4
        						WHEN l.start_date_half = 'F' AND l.end_date_half = 'S' THEN 8
        						WHEN l.start_date_half = 'S' AND l.end_date_half = 'S' THEN 4
        						ELSE 8
							END
						-- For the first day of the leave period
						WHEN g.leave_date = l.leave_start_date THEN 
							CASE 
								WHEN l.start_date_half = 'N' AND l.end_date_half = 'N' THEN 8
								WHEN l.start_date_half = 'F' AND l.end_date_half = 'N' THEN 4
								WHEN l.start_date_half = 'S' AND l.end_date_half = 'N' THEN 4
								WHEN l.start_date_half = 'N' AND l.end_date_half = 'F' THEN 8
								WHEN l.start_date_half = 'F' AND l.end_date_half = 'F' THEN 8
								WHEN l.start_date_half = 'S' AND l.end_date_half = 'F' THEN 4
								WHEN l.start_date_half = 'N' AND l.end_date_half = 'S' THEN 8
								WHEN l.start_date_half = 'F' AND l.end_date_half = 'S' THEN 8
								WHEN l.start_date_half = 'S' AND l.end_date_half = 'S' THEN 4
								ELSE 8
							END
						-- For the last day of the leave period
						WHEN g.leave_date = l.leave_end_date THEN 
							CASE 
								WHEN l.start_date_half = 'N' AND l.end_date_half = 'N' THEN 8
								WHEN l.start_date_half = 'F' AND l.end_date_half = 'N' THEN 8
								WHEN l.start_date_half = 'S' AND l.end_date_half = 'N' THEN 8
								WHEN l.start_date_half = 'N' AND l.end_date_half = 'F' THEN 4
								WHEN l.start_date_half = 'F' AND l.end_date_half = 'F' THEN 4
								WHEN l.start_date_half = 'S' AND l.end_date_half = 'F' THEN 4
								WHEN l.start_date_half = 'N' AND l.end_date_half = 'S' THEN 4
								WHEN l.start_date_half = 'F' AND l.end_date_half = 'S' THEN 8
								WHEN l.start_date_half = 'S' AND l.end_date_half = 'S' THEN 8
								ELSE 8
							END
						-- For all the in-between days (if applicable)
						WHEN g.leave_date > l.leave_start_date AND g.leave_date < l.leave_end_date THEN 8
						ELSE 0 -- No leave if not within the date range
					END
					)
			END AS total_leave
	
		FROM 
			""Leaves"" l
		JOIN 
			""Employees"" employees 
			ON employees.employee_mid = l.emp_mid
		LEFT JOIN
			""Designations"" designations
			ON employees.designation_id = designations.designation_id
		LEFT JOIN 
			""Holidays"" h
			ON h.holiday_date BETWEEN l.leave_start_date AND l.leave_end_date 
			AND h.location_id = employees.location_id
			AND h.isactive = true
		JOIN 
			generate_series(
				LEAST(l.leave_end_date, start_date),  
				GREATEST(l.leave_start_date, end_date), 
				'1 day'::interval
			) AS g(leave_date)
			ON TRUE
		WHERE 
			g.leave_date between start_date and end_date
			AND l.isactive = TRUE
			AND ((employees.employee_mid = ANY (emp_mids)))
			AND (ARRAY_LENGTH(designation , 1) IS NULL OR employees.designation_id = ANY (designation))
			AND (ARRAY_LENGTH(super_coach_mid , 1) IS NULL OR employees.supercoach_mid = ANY (super_coach_mid) )
			AND (ARRAY_LENGTH(co_super_coach_mid , 1) IS NULL OR employees.reporting_partner_mid = ANY (co_super_coach_mid) )
			AND (ARRAY_LENGTH(grade_filter , 1) IS NULL OR designations.grade = ANY (grade_filter) )
			AND EXTRACT(DOW FROM g.leave_date) NOT IN (0, 6)
		) as t1 
		WHERE t1.holiday_between_leaves > 0 OR t1.total_leave > 0;
		-- order by t1.max_leave_start_date

		SELECT json_agg(row_to_json(t))
		INTO var_resp FROM (
           select distinct emp_info.working_date, emp_info.employee_mid, emp_info.email_id, emp_info.competencyId , emp_info.designation_id , emp_info.business_unit_id ,emp_info.name, emp_info.available_hrs,
		   CASE WHEN leave_info.holiday_between_leaves IS NULL THEN 0 ELSE leave_info.holiday_between_leaves END as holiday_hrs, 
		   CASE WHEN leave_info.total_leave IS NULL THEN 0 ELSE leave_info.total_leave END as leave_hrs,
		   CONCAT(emp_info.employee_mid, '__', emp_info.email_id) as email_id_uid
           from employee_information as emp_info
           left join leaves_temp_table as leave_info 
           on emp_info.employee_mid = leave_info.emp_mid AND
           emp_info.working_date = leave_info.max_leave_start_date
		   order by emp_info.working_date
		) t;

		DROP TABLE employee_information;
		DROP TABLE leaves_temp_table;
END;
$BODY$;
";
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
