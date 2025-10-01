using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WCGT.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class modify_supercoach_name_sp_getuserwithleavesholidaysandavailability_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sp = @"
CREATE OR REPLACE PROCEDURE public.sp_getuserwithleavesholidaysandavailability_3(
	IN start_date date,
	IN end_date date,
	IN emp_mids text[],
	IN grade_filter text[],
	IN designation text[],
	IN super_coach_mid text[],
	IN co_super_coach_mid text[],
	IN bu_ids text[],
	IN competency_ids text[],
	INOUT var_resp json)
LANGUAGE 'plpgsql'
AS $BODY$



BEGIN

    CREATE TEMP TABLE employee_information (
        employee_mid varchar,
        email_id varchar,
        business_unit_id varchar,
        competencyId varchar,
		designation_name varchar,
        designation_id varchar,
		location_name varchar,
        super_coach_mid varchar,
        co_super_coach_mid varchar,
        grade_name varchar,
        name varchar,
        working_date date,
        available_hrs int,
        location_id varchar,
		supercoach_name varchar,        -- add this
        co_supercoach_name varchar  
    );
    
    -- Step 1: Limit and offset employees first
    WITH limited_employees AS (
         SELECT emp.employee_mid,
               emp.email_id,
               emp.business_unit_id,
               emp.""CompetencyId"",
			   desig.""designation_name"",
               emp.designation_id,
               emp.supercoach_mid,
               emp.reporting_partner_mid AS co_super_coach_mid,
               desig.grade AS grade_name,
               emp.name,
               emp.location_id,
			   location.""location_name"",
			   sup.name as supercoach_name,
			   cosup.name as co_supercoach_name
        FROM ""Employees"" emp
 		LEFT JOIN ""Employees"" sup ON emp.supercoach_mid = sup.employee_mid
		LEFT JOIN ""Employees"" cosup ON emp.reporting_partner_mid = cosup.employee_mid
        LEFT JOIN ""Designations"" desig ON emp.designation_id = desig.designation_id
        LEFT JOIN ""Locations"" location ON emp.location_id = location.location_id
        WHERE 
                (ARRAY_LENGTH(emp_mids , 1) IS NULL OR emp.employee_mid = ANY (emp_mids) )
                AND (ARRAY_LENGTH(designation , 1) IS NULL OR emp.designation_id = ANY (designation) )
                AND (ARRAY_LENGTH(super_coach_mid , 1) IS NULL OR emp.supercoach_mid = ANY (super_coach_mid) )
                AND (ARRAY_LENGTH(co_super_coach_mid , 1) IS NULL OR emp.reporting_partner_mid = ANY (co_super_coach_mid) )
                AND (ARRAY_LENGTH(grade_filter , 1) IS NULL OR desig.grade = ANY (grade_filter) )
                AND (ARRAY_LENGTH(bu_ids , 1) IS NULL OR emp.business_unit_id = ANY (bu_ids) )
                AND (ARRAY_LENGTH(competency_ids , 1) IS NULL OR emp.""CompetencyId"" = ANY (competency_ids) )
        ORDER BY emp.employee_mid
        -- LIMIT page_size
        -- OFFSET (page_number - 1) * page_size 
    ),
    -- Step 2: Generate working dates
    working_days AS (
        SELECT working_date::date
        FROM generate_series(start_date, end_date, interval '1 day') AS wd(working_date)
        WHERE EXTRACT(DOW FROM working_date) NOT IN (0, 6)  -- Exclude weekends
    )
    
    -- Step 3: Cross join limited employees with working days
    INSERT INTO employee_information (
        employee_mid,
        email_id,
        business_unit_id,
        competencyId,
		designation_name,
        designation_id,
        super_coach_mid,
        co_super_coach_mid,
        grade_name,
        name,
        working_date,
        available_hrs,
        location_id,
		location_name,
		supercoach_name,
	    co_supercoach_name
    )
    SELECT 
        emp.employee_mid,
        emp.email_id,
        emp.business_unit_id,
        emp.""CompetencyId"",
		emp.designation_name,
        emp.designation_id,
        emp.supercoach_mid,
        emp.co_super_coach_mid,
        emp.grade_name,
        emp.name,
        wd.working_date,
        8 AS available_hrs,
        emp.location_id,
		emp.""location_name"",
		emp.supercoach_name,
	    emp.co_supercoach_name
    FROM limited_employees emp
    CROSS JOIN working_days wd;

    SELECT json_agg(row_to_json(t))
        INTO var_resp FROM (
            SELECT DISTINCT emp_info.working_date, emp_info.employee_mid, emp_info.email_id, emp_info.competencyId ,emp_info.designation_name, emp_info.designation_id, 
			emp_info.business_unit_id ,emp_info.grade_name,emp_info.name, emp_info.available_hrs, emp_info.location_id, emp_info.location_name, emp_info.super_coach_mid,  emp_info.co_super_coach_mid,
			
            CASE
                WHEN h.holiday_date IS NULL THEN leaves_calculator(emp_info.working_date , l.leave_start_date , l.leave_end_date , l.start_date_half , l.end_date_half) 
                ELSE 0
            END
            as leave_hrs,
            CASE 
                WHEN h.holiday_date IS NULL THEN 0 
                ELSE 8
            END as holiday_hrs,
            CONCAT(emp_info.employee_mid, '__', emp_info.email_id) as email_id_uid, emp_info.supercoach_name,  emp_info.co_supercoach_name
            from employee_information as emp_info
            left join ""Leaves"" as l on emp_info.employee_mid = l.emp_mid AND emp_info.working_date >= l.leave_start_date AND emp_info.working_date <= l.leave_end_date
            left join ""Holidays"" as h on emp_info.location_id = h.location_id AND emp_info.working_date = h.holiday_date
            order by emp_info.working_date
        ) t;
        DROP TABLE employee_information;
        
END;
$BODY$;";
            migrationBuilder.Sql(sp);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
