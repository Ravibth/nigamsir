using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Allocation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class sp_availability_system_suggestionupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sp = @"CREATE OR REPLACE PROCEDURE public.sp_availability_system_suggestion(
	IN users jsonb,
	IN required_hours integer,
	IN start_date date, 
	IN end_date date,
	IN job_code text,
	IN pipeline_code text,
	OUT var_resp json
)
LANGUAGE 'plpgsql'
AS $BODY$

DECLARE
    days_between INT;
    weekends int;
BEGIN
-- Create temporary table to store user information with leaves and holidays
    CREATE temp TABLE user_master_with_leaves_holidays(
        emp_name varchar,
        email_id varchar,
        designation varchar,
        grade varchar,
		competency varchar,
		competency_id varchar,
        location varchar,
		expertise varchar,
        smeg varchar,
		supercoach varchar,
        business_unit varchar,
        sub_industry varchar,
        industry varchar,
        holiday_hours int,
        leave_hours int
    );

-- Insert data into temporary table from JSONB input
    INSERT INTO user_master_with_leaves_holidays(
        emp_name ,
        email_id ,
        designation ,
        grade ,
		competency ,
		competency_id ,
        location ,
		expertise,
        smeg ,
		supercoach,
        business_unit ,
        sub_industry ,
        industry ,
        holiday_hours ,
        leave_hours 
    )
    SELECT 
        elem->>'emp_name',
        elem->>'email_id',
        elem->>'designation',
        elem->>'grade',
		elem->>'competency' ,
		elem->>'competency_id' ,
        elem->>'location',
        elem->>'expertise',
        elem->>'smeg',
        elem->>'supercoach',
        elem->>'business_unit',
        elem->>'sub_industry',
        elem->>'industry',
        COALESCE((elem->>'holiday_hours')::int,0),
        COALESCE((elem->>'leave_hours')::int,0)
    FROM jsonb_array_elements(users) AS elem;
	
	
-- Delete any user from above table if date overlaps in published table
	DELETE FROM user_master_with_leaves_holidays u_master
	WHERE 
		u_master.email_id IN (
			SELECT pub_details.""EmpEmail""
			FROM ""PublishedResAllocDetails"" pub_details
			JOIN ""Requisition"" requisition 
				ON pub_details.""RequisitionId"" = requisition.""Id""
			WHERE 
				pub_details.""StartDate"" <= end_date
				AND pub_details.""EndDate"" >= start_date
				AND trim(lower(requisition.""PipelineCode"")) =  trim(lower(pipeline_code))
				AND trim(lower(requisition.""JobCode"")) =  trim(lower(job_code))
				AND pub_details.""IsActive"" = true
		);
		
-- Delete any user from above table if date overlaps in published table
	DELETE FROM user_master_with_leaves_holidays u_master
	WHERE 
		u_master.email_id IN (
			SELECT unpub_details.""EmpEmail""
			FROM ""UnPublishedResAllocDetails"" unpub_details
			JOIN ""Requisition"" requisition 
				ON unpub_details.""RequisitionId"" = requisition.""Id""
			WHERE 
				unpub_details.""StartDate"" <= end_date
				AND unpub_details.""EndDate"" >= start_date
				AND trim(lower(requisition.""PipelineCode"")) =  trim(lower(pipeline_code))
				AND trim(lower(requisition.""JobCode"")) =  trim(lower(job_code))
				AND LOWER(TRIM(unpub_details.""AllocationStatus"")) != 'draft'
				AND (
						unpub_details.""IsActive"" = true 
						OR(
							LOWER(TRIM(unpub_details.""AllocationStatus"")) in 
								(
									'employee allocation rejected by reviewer'
									,'employee allocation rejected by rmployee'
									,'employee allocation rejected by employee pending for rr'
									,'employee allocation terminated by resource requestor'
								)
						)
				)
		);
	
    weekends := COALESCE((SELECT count_weekends_between_dates(start_date, end_date)) * 8, 0);

-- Calculate the number of days between start_date and end_date
    days_between := CASE 
                        WHEN end_date = start_date THEN 8
                        ELSE (end_date - start_date + 1) * 8
                    END;

-- 	Create temporary table to store allocated hours
    CREATE temp TABLE user_allocated_hours(
        email_id varchar,
        allocated_hours int
    );

-- 	Insert data into temporary table from Published and Unpublished allocation days
    INSERT INTO user_allocated_hours(
        email_id ,
        allocated_hours
    )
	SELECT 
		email_id,
		SUM(allocated_hours)
    FROM (
		SELECT 
			user_master.email_id,
			COALESCE(SUM(pub_days.""Efforts""), 0) AS allocated_hours,
			'p' as alloc_type

		FROM user_master_with_leaves_holidays user_master
		LEFT JOIN ""PublishedResAllocDays"" pub_days
			ON pub_days.""EmailId"" = user_master.email_id
		WHERE 
			pub_days.""AllocationDate"" between start_date and end_date
		GROUP BY user_master.email_id

		UNION

		SELECT 
			user_master.email_id,
			COALESCE(SUM(unpub_days.""Efforts""), 0) AS allocated_hours,
			'u' as alloc_type
		FROM user_master_with_leaves_holidays user_master
		LEFT JOIN ""UnPublishedResAllocDays"" unpub_days
			ON unpub_days.""EmailId"" = user_master.email_id
		JOIN ""UnPublishedResAlloc"" unpub_alloc
			ON unpub_alloc.""Id"" = unpub_days.""UnPublishedResAllocId""
		JOIN ""UnPublishedResAllocDetails"" unpub_details
			ON unpub_details.""Id"" = unpub_alloc.""UnPublishedResAllocDetailsId""
		WHERE 
			LOWER(TRIM(unpub_details.""AllocationStatus"")) != 'draft'
			AND unpub_days.""AllocationDate"" between start_date and end_date
		GROUP BY user_master.email_id
	) cte
	GROUP BY email_id
	;

-- Create temporary table to store the response
    CREATE TEMP TABLE user_response_table (
        emp_name varchar,
        email_id varchar,
        designation varchar,
        grade varchar,
		competency varchar,
		competency_id varchar,
        location varchar,
		expertise varchar,
        smeg varchar,
		supercoach varchar,
        business_unit varchar,
        sub_industry varchar,
        industry varchar,
        available bool
    );

-- Insert into response table based on availability logic
    INSERT INTO user_response_table
    SELECT 
        umwlh.emp_name,
        umwlh.email_id,
        umwlh.designation,
        umwlh.grade,
        umwlh.competency,
        umwlh.competency_id,
        umwlh.location,
		umwlh.expertise,
        umwlh.smeg,
		umwlh.supercoach,
        umwlh.business_unit,
        umwlh.sub_industry,
        umwlh.industry,
        CASE 
            WHEN (days_between - (COALESCE(weekends,0) + COALESCE(ua.allocated_hours,0) + COALESCE(umwlh.leave_hours,0) + COALESCE(umwlh.holiday_hours,0))) >= required_hours 
				THEN true
            ELSE 
				false
        END AS available
    FROM user_master_with_leaves_holidays umwlh
    LEFT JOIN user_allocated_hours ua
        ON ua.email_id = umwlh.email_id;

-- Aggregate the final response as JSON
    SELECT jsonb_agg(row_to_json(final_data))
    INTO var_resp
    FROM user_response_table AS final_data;

drop table user_master_with_leaves_holidays;
drop table user_allocated_hours;
drop table user_response_table;

END;
$BODY$;
";
            migrationBuilder.Sql(sp);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sp = @"	DROP procedure sp_availability_system_suggestion;";
            migrationBuilder.Sql(sp);
        }
    }
}
