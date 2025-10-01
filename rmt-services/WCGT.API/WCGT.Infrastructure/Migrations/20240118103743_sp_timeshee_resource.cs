using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WCGT.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class sp_timeshee_resource : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP FUNCTION IF EXISTS fun_resource_timesheet_view");

            string sp = @"CREATE OR REPLACE PROCEDURE sp_resource_timesheet_view(
     		    start_date date,
			    end_date date,
	             injobcode text,
                 INOUT _result_one refcursor = 'rs_resultone'
                 )
                LANGUAGE plpgsql
                AS
                $$
             BEGIN
             open _result_one for 
			    SELECT employeecode as empcode,employeename as empname,
					SUM(""Timesheet"".totaltime) as totaltime, SUM((""Timesheet"".totaltime ) * ""rate"") as timesheetcost
  					FROM public.""Timesheet""
					where ""Timesheet"".jobcode=injobcode AND datelog >= start_date AND datelog <= end_date
  					GROUP BY employeecode,employeename;
                END;

            $$;";
            migrationBuilder.Sql(sp);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sp = @"	DROP PROCEDURE IF EXISTS sp_timesheet_view( timeoptionname text,injobcode text,startDate date,endDate date,
                        INOUT _result_one refcursor );";
            migrationBuilder.Sql(sp);
        }
    }
}
