using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WCGT.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class sp_getUserWithLeavesHolidays_halfDayLeave : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sp = @"CREATE OR REPLACE PROCEDURE public.sp_getuserwithleavesholidays(IN designationid text, IN start_date date, IN end_date date, OUT var_resp json)
			LANGUAGE 'plpgsql'
    
		AS $BODY$
		begin

			CREATE temp TABLE leaves_temp_table(
				emp_mid varchar,
				emp_email varchar,
				max_leave_start_date date,
				min_leave_end_date date,
				holiday_between_leaves int,
				total_leave int
			);
			create temp table temp_table_main(
				emp_name varchar,
				employee_mid varchar,
				email_id varchar,
				designation varchar,
				grade varchar,
				competency varchar,
				competency_id varchar,
				location varchar,
				business_unit varchar,
				sub_industry varchar,
				industry varchar,
				supercoach varchar,
				max_leave_start_date date,
				min_leave_end_date date,
				holiday_not_between_leaves int,
				leaves_total int
			);
	
			insert into leaves_temp_table
			(
				emp_mid,
				emp_email ,
				max_leave_start_date ,
				min_leave_end_date ,
				holiday_between_leaves,
				total_leave 
			)
				SELECT
				l.emp_mid,
				l.emp_email,
				g.leave_date as max_leave_start_date,
				g.leave_date as min_leave_end_date,
		-- 		g.leave_date :: DATE,
		-- 		l.leave_id,
		-- 		l.location_id,
		-- 		l.location_name,
		-- 		l.leave_start_date,
		-- 		l.leave_end_date,
		-- 		l.emp_name,
		-- 		employees.designation_id,
		-- 		l.end_date_flag,
		-- 		l.start_date_flag,
				CASE
					WHEN g.leave_date = h.holiday_date THEN 8
					ELSE  0
				END holiday_between_leaves,
				CASE
					WHEN g.leave_date = h.holiday_date THEN 0
					ELSE
						(
							CASE
							-- For the first day of the leave period
							WHEN g.leave_date = l.leave_start_date THEN 
								CASE 
									WHEN l.start_date_flag = 'N' AND l.end_date_flag = 'N' THEN 8
									WHEN l.start_date_flag = 'F' AND l.end_date_flag = 'N' THEN 4
									WHEN l.start_date_flag = 'S' AND l.end_date_flag = 'N' THEN 4
									WHEN l.start_date_flag = 'N' AND l.end_date_flag = 'F' THEN 8
									WHEN l.start_date_flag = 'F' AND l.end_date_flag = 'F' THEN 8
									WHEN l.start_date_flag = 'S' AND l.end_date_flag = 'F' THEN 4
									WHEN l.start_date_flag = 'N' AND l.end_date_flag = 'S' THEN 8
									WHEN l.start_date_flag = 'F' AND l.end_date_flag = 'S' THEN 8
									WHEN l.start_date_flag = 'S' AND l.end_date_flag = 'S' THEN 4
									ELSE 8
								END
							-- For the last day of the leave period
							WHEN g.leave_date = l.leave_end_date THEN 
								CASE 
									WHEN l.start_date_flag = 'N' AND l.end_date_flag = 'N' THEN 8
									WHEN l.start_date_flag = 'F' AND l.end_date_flag = 'N' THEN 8
									WHEN l.start_date_flag = 'S' AND l.end_date_flag = 'N' THEN 8
									WHEN l.start_date_flag = 'N' AND l.end_date_flag = 'F' THEN 4
									WHEN l.start_date_flag = 'F' AND l.end_date_flag = 'F' THEN 4
									WHEN l.start_date_flag = 'S' AND l.end_date_flag = 'F' THEN 4
									WHEN l.start_date_flag = 'N' AND l.end_date_flag = 'S' THEN 4
									WHEN l.start_date_flag = 'F' AND l.end_date_flag = 'S' THEN 8
									WHEN l.start_date_flag = 'S' AND l.end_date_flag = 'S' THEN 8
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
				""Holidays"" h
				ON h.holiday_date BETWEEN l.leave_start_date AND l.leave_end_date 
				AND h.location_id = employees.location_id
				AND h.isactive = true
			JOIN 
				generate_series(
					GREATEST(l.leave_start_date, end_date), 
					LEAST(l.leave_end_date, start_date),  
					'1 day'::interval
				) AS g(leave_date)
				ON TRUE
			WHERE 
				l.leave_start_date <= start_date
				AND l.leave_end_date >= end_date
				AND l.isactive = TRUE
				AND employees.designation_id = designationid
				AND EXTRACT(DOW FROM g.leave_date) NOT IN (0, 6);

			insert INTO temp_table_main(
				emp_name ,
				employee_mid ,
				email_id ,
				designation ,
				grade,
				competency ,
				competency_id,
				location ,
				business_unit ,
				sub_industry ,
				industry ,
				supercoach,
				max_leave_start_date ,
				min_leave_end_date ,
				holiday_not_between_leaves ,
				leaves_total 
			)
				SELECT 
					employees.name as emp_name ,
					employees.employee_mid as employee_mid ,
					( employees.employee_mid || '__' || employees.email_id) as email_id ,
					designation_table.designation_name as designation,
					designation_table.grade as grade,
					competency_master.""CompetencyName"" as competency,
					employees.""CompetencyId"" as competency_id,
					location_table.location_name as location,
					bu_tree_mapping_for_bu.bu as business_unit,
			
					-- User does not have any base industry/sub-industry
					'' as sub_industry,
					'' as industry,
			
					( employees_2.employee_mid || '__' || employees_2.email_id) as supercoach,
					leaves_temp_table.max_leave_start_date as max_leave_start_date,
					leaves_temp_table.min_leave_end_date as min_leave_end_date,
					(
						select count(*)*8
						from ""Holidays"" holidays
						where holidays.isactive = true
						and  (holidays.holiday_date not between  leaves_temp_table.max_leave_start_date and leaves_temp_table.min_leave_end_date)
						and holidays.holiday_date >= start_date 
						and holidays.holiday_date <= end_date 
						and holidays.location_id = employees.location_id
					) as holiday_not_between_leaves,
					(
						leaves_temp_table.total_leave - coalesce(leaves_temp_table.holiday_between_leaves,0)
					) as leaves_total
				FROM ""Employees"" employees
				left join leaves_temp_table
					on leaves_temp_table.emp_mid = employees.employee_mid
				left join ""Designations"" designation_table
					on designation_table.designation_id = employees.designation_id
				left join ""Locations"" location_table
					on location_table.location_id = employees.location_id
				left join ""BUTreeMappings"" bu_tree_mapping_for_bu
					on bu_tree_mapping_for_bu.bu_id = employees.business_unit_id
				left join ""Competencies"" competency_master
					on competency_master.""CompetencyId"" = employees.""CompetencyId""
				LEFT JOIN ""Employees"" employees_2 
					ON employees.supercoach_mid = employees_2.employee_mid
				where 
					employees.designation_id = designationid
					and employees.isactive = true
					and competency_master.""CompetencyId"" = employees.""CompetencyId""
					and (lower(employees.employee_status) != 'absconder')
					and 
					(
						(employees.proposed_lwd is null and employees.resignation_date is null ) 
						-- check for plus 1 logic
						or ( employees.proposed_lwd is not null and employees.proposed_lwd - current_date >= 30)
						or ( employees.proposed_lwd is null and employees.resignation_date is not null and employees.resignation_date - current_date >= 30)
					);	
		-- 	) t;

			SELECT json_agg(row_to_json(t))
			INTO var_resp
			FROM 
			(  
		-- 		select * 
		select 
				emp_name ,
				employee_mid ,
				email_id ,
				designation ,
				grade ,
				competency,
				competency_id,
				location ,
				business_unit ,
				sub_industry ,
				industry ,
				supercoach,
				max(holiday_not_between_leaves) as holiday_hours ,
				sum(leaves_total) as leave_hours
	 
			 from temp_table_main
			group by emp_name ,
				employee_mid ,
				email_id ,
				designation ,
				grade ,
				competency,
				competency_id,
				location ,
				business_unit ,
				sub_industry ,
				industry,
				supercoach
			) t;

		drop table leaves_temp_table;
		drop table temp_table_main;
		end;
		$BODY$;";

            migrationBuilder.Sql(sp);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sp = @"	DROP procedure sp_getUserWithLeavesHolidays;";
            migrationBuilder.Sql(sp);
        }
    }
}
