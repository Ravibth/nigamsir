-- PROCEDURE: public.sp_getuserwithleavesholidays(text, date, date)

-- DROP PROCEDURE IF EXISTS public.sp_getuserwithleavesholidays(text, date, date);

CREATE OR REPLACE PROCEDURE public.sp_getuserwithleavesholidays(
	IN designationid text,
	IN start_date date,
	IN end_date date,
	OUT var_resp json)
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
				"Leaves" l
			JOIN 
				"Employees" employees 
				ON employees.employee_mid = l.emp_mid
			LEFT JOIN 
				"Holidays" h
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
				AND employees.designation_id = designationid
				AND EXTRACT(DOW FROM g.leave_date) NOT IN (0, 6)
			) as t1
			WHERE t1.holiday_between_leaves > 0 OR t1.total_leave > 0 ;

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
				SELECT DISTINCT
					employees.name as emp_name ,
					employees.employee_mid as employee_mid ,
					( employees.employee_mid || '__' || employees.email_id) as email_id ,
					designation_table.designation_name as designation,
					designation_table.grade as grade,
					competency_master."CompetencyName" as competency,
					employees."CompetencyId" as competency_id,
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
						from "Holidays" holidays
						where holidays.isactive = true
						and  (holidays.holiday_date not between  leaves_temp_table.max_leave_start_date and leaves_temp_table.min_leave_end_date)
						and holidays.holiday_date >= start_date 
						and holidays.holiday_date <= end_date 
						and holidays.location_id = employees.location_id
					) as holiday_not_between_leaves,
					(
						leaves_temp_table.total_leave - coalesce(leaves_temp_table.holiday_between_leaves,0)
					) as leaves_total
				FROM "Employees" employees
				left join leaves_temp_table
					on leaves_temp_table.emp_mid = employees.employee_mid
				left join "Designations" designation_table
					on designation_table.designation_id = employees.designation_id
				left join "Locations" location_table
					on location_table.location_id = employees.location_id
				left join "BUTreeMappings" bu_tree_mapping_for_bu
					on bu_tree_mapping_for_bu.bu_id = employees.business_unit_id
				left join "Competencies" competency_master
					on competency_master."CompetencyId" = employees."CompetencyId"
				LEFT JOIN "Employees" employees_2 
					ON employees.supercoach_mid = employees_2.employee_mid
				where 
					employees.designation_id = designationid
					and employees.isactive = true
					and competency_master."CompetencyId" = employees."CompetencyId"
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
		
$BODY$;
-- ALTER PROCEDURE public.sp_getuserwithleavesholidays(text, date, date)
--     OWNER TO rmsdevdba;
