using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WCGT.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class leaves_calculator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sp = @"
                CREATE OR REPLACE FUNCTION public.leaves_calculator(
	working_date date,
	leave_start_date date,
	leave_end_date date,
	start_date_half text,
	end_date_half text)
    RETURNS integer
    LANGUAGE 'plpgsql'
    COST 100
    VOLATILE PARALLEL UNSAFE
AS $BODY$

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
$BODY$;
";
			migrationBuilder.Sql(sp);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql(@"DROP FUNCTION IF EXISTS public.leaves_calculator(date, date, date, text, text);");
        }
    }
}
