using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WCGT.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class sp_timesheet_viewv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sp = @"
             CREATE OR REPLACE PROCEDURE public.sp_timesheet_view(
	            IN timeoptionname text,
	            IN injobcode text,
	            IN startdate date,
	            IN enddate date,
	            INOUT _result_one refcursor DEFAULT 'rs_resultone'::refcursor)
            LANGUAGE 'plpgsql'
            AS $$
                    BEGIN
                        IF timeoptionname = 'weekly' THEN
                            open _result_one for 
			                    SELECT DATE_TRUNC('week', datelog) AS monthname, SUM(""Timesheet"".totaltime) as totaltime ,""Designations"".designation_name as ""Designation"", SUM((""Timesheet"".totaltime ) * ""rate"") as timesheetcost
						    FROM public.""Timesheet""
						    JOIN public.""Designations""
						    ON ""Timesheet"".designation_id = ""Designations"".designation_id
		          		    where jobcode=injobcode AND datelog >= startDate AND datelog <= endDate 
				            GROUP BY ""Designations"".designation_name,monthname;
                    ELSIF timeoptionname = 'monthly'  THEN
                        open _result_one for  
		                SELECT DATE_TRUNC('month', datelog) AS monthname, SUM(""Timesheet"".totaltime) as totaltime,""Designations"".designation_name as ""Designation"", SUM((""Timesheet"".totaltime ) * ""rate"") as timesheetcost
				            FROM public.""Timesheet""
						    JOIN public.""Designations""
						    ON ""Timesheet"".designation_id = ""Designations"".designation_id
		          		    where jobcode=injobcode AND datelog >= startDate AND datelog <= endDate 
				            GROUP BY ""Designations"".designation_name,monthname;
                    ELSIF timeoptionname = 'daily'  THEN
                        open _result_one for  
		                SELECT DATE_TRUNC('day', datelog) AS monthname, SUM(""Timesheet"".totaltime) as totaltime,""Designations"".designation_name as ""Designation"", SUM((""Timesheet"".totaltime ) * ""rate"") as timesheetcost
				            FROM public.""Timesheet""
							    JOIN public.""Designations""
						    ON ""Timesheet"".designation_id = ""Designations"".designation_id
		          		    where jobcode=injobcode AND datelog >= startDate AND datelog <= endDate 
				            GROUP BY ""Designations"".designation_name,monthname;
                    END IF;
                    END;
                
                $$";
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
