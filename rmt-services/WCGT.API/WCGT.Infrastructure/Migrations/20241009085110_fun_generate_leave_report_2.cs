using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WCGT.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fun_generate_leave_report_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP FUNCTION IF EXISTS generate_leave_report");
            string sp = @"CREATE OR REPLACE FUNCTION generate_leave_report(
    p_start_date DATE,  
    p_end_date DATE,   
    p_emp_mids TEXT[]  
)
RETURNS TABLE(
    leave_date DATE,
    employee_email TEXT,
    emp_mid TEXT,
    leave_hours INT,
    leave_type TEXT
) 
LANGUAGE plpgsql
AS
$$
BEGIN
    RETURN QUERY
    SELECT 
        g.leave_date::DATE,  -- Cast to DATE
        l.emp_email as employee_email,
        l.emp_mid,
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
        END AS leave_hours,
        
        CASE
            -- For the first day of the leave period
            WHEN g.leave_date = l.leave_start_date THEN 
                CASE 
                    WHEN l.start_date_flag = 'N' AND l.end_date_flag = 'N' THEN 'FULL_DAY_LEAVE'
                    WHEN l.start_date_flag = 'F' AND l.end_date_flag = 'N' THEN 'FIRST_HALF_LEAVE'
                    WHEN l.start_date_flag = 'S' AND l.end_date_flag = 'N' THEN 'SECOND_HALF_LEAVE'
                    WHEN l.start_date_flag = 'N' AND l.end_date_flag = 'F' THEN 'FULL_DAY_LEAVE'
                    WHEN l.start_date_flag = 'F' AND l.end_date_flag = 'F' THEN 'FULL_DAY_LEAVE'
                    WHEN l.start_date_flag = 'S' AND l.end_date_flag = 'F' THEN 'SECOND_HALF_LEAVE'
                    WHEN l.start_date_flag = 'N' AND l.end_date_flag = 'S' THEN 'FULL_DAY_LEAVE'
                    WHEN l.start_date_flag = 'F' AND l.end_date_flag = 'S' THEN 'FULL_DAY_LEAVE'
                    WHEN l.start_date_flag = 'S' AND l.end_date_flag = 'S' THEN 'SECOND_HALF_LEAVE'
                    ELSE 'FULL_DAY_LEAVE'
                END
            -- For the last day of the leave period
            WHEN g.leave_date = l.leave_end_date THEN 
                CASE 
                    WHEN l.start_date_flag = 'N' AND l.end_date_flag = 'N' THEN 'FULL_DAY_LEAVE'
                    WHEN l.start_date_flag = 'F' AND l.end_date_flag = 'N' THEN 'FULL_DAY_LEAVE'
                    WHEN l.start_date_flag = 'S' AND l.end_date_flag = 'N' THEN 'FULL_DAY_LEAVE'
                    WHEN l.start_date_flag = 'N' AND l.end_date_flag = 'F' THEN 'FIRST_HALF_LEAVE'
                    WHEN l.start_date_flag = 'F' AND l.end_date_flag = 'F' THEN 'FIRST_HALF_LEAVE'
                    WHEN l.start_date_flag = 'S' AND l.end_date_flag = 'F' THEN 'FIRST_HALF_LEAVE'
                    WHEN l.start_date_flag = 'N' AND l.end_date_flag = 'S' THEN 'SECOND_HALF_LEAVE'
                    WHEN l.start_date_flag = 'F' AND l.end_date_flag = 'S' THEN 'FULL_DAY_LEAVE'
                    WHEN l.start_date_flag = 'S' AND l.end_date_flag = 'S' THEN 'FULL_DAY_LEAVE'
                    ELSE 'FULL_DAY_LEAVE'
                END
            -- For all the in-between days (if applicable)
            WHEN g.leave_date > l.leave_start_date AND g.leave_date < l.leave_end_date THEN 'FULL_DAY_LEAVE'
            ELSE '' -- No leave type if not within the date range
        END AS leave_type
		
        
    FROM 
        generate_series(p_start_date, p_end_date, '1 day'::interval) AS g(leave_date)
    JOIN 
        ""Leaves"" l ON g.leave_date BETWEEN l.leave_start_date AND l.leave_end_date
	LEFT JOIN
		""Holidays"" h on g.leave_date = h.holiday_date AND h.location_id = l.location_id
    WHERE 
        (p_emp_mids is null OR l.emp_mid = ANY(p_emp_mids) )
        AND l.isactive = TRUE
		AND EXTRACT(DOW FROM g.leave_date) NOT IN (0, 6)
		AND h.holiday_date IS NULL 
	ORDER BY leave_date;
END;
$$;";
            migrationBuilder.Sql(sp);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP FUNCTION IF EXISTS generate_leave_report");
        }
    }
}
