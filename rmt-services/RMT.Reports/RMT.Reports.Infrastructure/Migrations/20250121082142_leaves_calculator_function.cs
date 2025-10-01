using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Reports.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class leaves_calculator_function : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sql = @"CREATE OR REPLACE FUNCTION leaves_calculator
(
	working_date DATE
	,leave_start_date DATE
	,leave_end_date DATE
	,start_date_half TEXT
	, end_date_half TEXT
)
RETURNS INT
LANGUAGE plpgsql
AS
$$
DECLARE
-- variable declaration
leaves_count INTEGER;
BEGIN
 -- logic

		
				leaves_count:= 	CASE
					-- For same day leave
					WHEN (working_date = leave_start_date and leave_start_date::date = leave_end_date::date) THEN 
						CASE 
							WHEN start_date_half = 'N' AND end_date_half = 'N' THEN 8
							WHEN start_date_half = 'F' AND end_date_half = 'N' THEN 4
							WHEN start_date_half = 'S' AND end_date_half = 'N' THEN 4
							WHEN start_date_half = 'N' AND end_date_half = 'F' THEN 4
							WHEN start_date_half = 'F' AND end_date_half = 'F' THEN 4
							WHEN start_date_half = 'S' AND end_date_half = 'F' THEN 0
							WHEN start_date_half = 'N' AND end_date_half = 'S' THEN 4
							WHEN start_date_half = 'F' AND end_date_half = 'S' THEN 8
							WHEN start_date_half = 'S' AND end_date_half = 'S' THEN 4
							ELSE 8
						END
					-- For the first day of the leave period
					WHEN working_date = leave_start_date THEN 
						CASE 
							WHEN start_date_half = 'N' AND end_date_half = 'N' THEN 8
							WHEN start_date_half = 'F' AND end_date_half = 'N' THEN 4
							WHEN start_date_half = 'S' AND end_date_half = 'N' THEN 4
							WHEN start_date_half = 'N' AND end_date_half = 'F' THEN 8
							WHEN start_date_half = 'F' AND end_date_half = 'F' THEN 8
							WHEN start_date_half = 'S' AND end_date_half = 'F' THEN 4
							WHEN start_date_half = 'N' AND end_date_half = 'S' THEN 8
							WHEN start_date_half = 'F' AND end_date_half = 'S' THEN 8
							WHEN start_date_half = 'S' AND end_date_half = 'S' THEN 4
							ELSE 8
						END
					-- For the last day of the leave period
					WHEN working_date = leave_end_date THEN 
						CASE 
							WHEN start_date_half = 'N' AND end_date_half = 'N' THEN 8
							WHEN start_date_half = 'F' AND end_date_half = 'N' THEN 8
							WHEN start_date_half = 'S' AND end_date_half = 'N' THEN 8
							WHEN start_date_half = 'N' AND end_date_half = 'F' THEN 4
							WHEN start_date_half = 'F' AND end_date_half = 'F' THEN 4
							WHEN start_date_half = 'S' AND end_date_half = 'F' THEN 4
							WHEN start_date_half = 'N' AND end_date_half = 'S' THEN 4
							WHEN start_date_half = 'F' AND end_date_half = 'S' THEN 8
							WHEN start_date_half = 'S' AND end_date_half = 'S' THEN 8
							ELSE 8
						END
					-- For all the in-between days (if applicable)
					WHEN working_date > leave_start_date AND working_date < leave_end_date THEN 8
					ELSE 0 -- No leave if not within the date range
				END;
			-- INTO leaves_count;
 RETURN leaves_count;
END;
$$;";

			migrationBuilder.Sql(sql);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql("DROP FUNCTION IF EXISTS public.leaves_calculator(date, date, date, text, text);");
        }
    }
}
