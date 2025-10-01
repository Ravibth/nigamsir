using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WCGT.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class sp_getUserWithLeavesHolidays : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sp = @"CREATE OR REPLACE PROCEDURE public.sp_getUserWithLeavesHolidays(
	IN designationid TEXT,
	IN start_date DATE,
	IN end_date DATE,
	OUT var_resp json
)
LANGUAGE 'plpgsql'
AS $$
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
		location varchar,
		expertise varchar,
		smeg varchar,
		business_unit varchar,
		sub_industry varchar,
		industry varchar,
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
   		(leaves.emp_mid) AS emp_mid,
   		(leaves.emp_email) AS emp_email,
   		(GREATEST(leaves.leave_start_date, start_date)) AS max_leave_start_date,
		(LEAST(leaves.leave_end_date, end_date)) AS min_leave_end_date,
		(
				select count(*)*8
				from ""Holidays"" holidays
				where holidays.isactive = true
				and holidays.holiday_date >= (GREATEST(leaves.leave_start_date, start_date))
				and holidays.holiday_date <= (LEAST(leaves.leave_end_date, end_date)) 
				and holidays.location_id = employees.location_id
		) as holiday_between_leaves,
		(case
		 	when (LEAST(leaves.leave_end_date, end_date) = GREATEST(leaves.leave_start_date, start_date)) 
		 	then 8
		 	else ((LEAST(leaves.leave_end_date, end_date) - GREATEST(leaves.leave_start_date, start_date)) * 8)
		 end
		)as total_leave
	FROM ""Leaves"" leaves
	JOIN ""Employees"" employees 
		ON employees.employee_mid = leaves.emp_mid
	WHERE 
		leaves.leave_start_date <= end_date
		AND leaves.leave_end_date >= start_date
		AND leaves.isactive = TRUE
		AND employees.designation_id = designationid	
	ORDER BY 
		(leaves.emp_mid);

	insert INTO temp_table_main(
		emp_name ,
		employee_mid ,
		email_id ,
		designation ,
		location ,
		expertise ,
		smeg ,
		business_unit ,
		sub_industry ,
		industry ,
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
			location_table.location_name as location,
			bu_tree_mapping.expertise as expertise,
			bu_tree_mapping.sme_group as smeg,
			bu_tree_mapping.bu as business_unit,
			
			--check for industry/sub-industry
			bu_tree_mapping.bu as sub_industry,
			bu_tree_mapping.bu as industry,
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
		left join ""BUTreeMappings"" bu_tree_mapping
			on bu_tree_mapping.sme_group_id = employees.smeg_id
		where 
			employees.designation_id = designationid
			and employees.isactive = true
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
		location ,
		expertise ,
		smeg ,
		business_unit ,
		sub_industry ,
		industry ,
		max(holiday_not_between_leaves) as holiday_hours ,
		sum(leaves_total) as leave_hours
	 
	 from temp_table_main
	group by emp_name ,
		employee_mid ,
		email_id ,
		designation ,
		location ,
		expertise ,
		smeg ,
		business_unit ,
		sub_industry ,
		industry
	) t;

truncate table leaves_temp_table;
truncate table temp_table_main;

drop table leaves_temp_table;
drop table temp_table_main;
end;
$$;";
            migrationBuilder.Sql(sp);
        }
        // / < inheritdoc / >
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sp = @"	DROP procedure sp_getUserWithLeavesHolidays;";
            migrationBuilder.Sql(sp);
        }
    }
}