using Microsoft.EntityFrameworkCore.Migrations;
using RMT.Reports.Infrastructure.Helpers;

#nullable disable

namespace RMT.Reports.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class employee_working_daysv11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string ConnStr_WcgtDB = ConfigConstants.WcgtDbConnStr;

            string cmd = @$"

            DROP INDEX IF EXISTS employee_allocation_index;

            DROP MATERIALIZED VIEW IF EXISTS employee_allocation_timesheet;

            DROP INDEX IF EXISTS employee_working_days_index;

            DROP MATERIALIZED VIEW IF EXISTS employee_working_days;

            CREATE MATERIALIZED VIEW IF NOT EXISTS employee_working_days
            TABLESPACE pg_default
            AS
             SELECT ewd.employee_mid,
                ewd.employee_code,
                ewd.employee_name,
                ewd.email_id,
                ewd.department,
                ewd.joining_date,
                ewd.proposed_lwd,
                ewd.location,
                ewd.working_date,
                ewd.week_day_number,
                ewd.designation_name,
                ewd.grade,
                ewd.business_unit,
                ewd.competency,
                ewd.hourly_rate
               FROM dblink('{ConnStr_WcgtDB}','

                                        Select ""emp"".""employee_mid"" ""employee_mid"", ""emp"".""employee_code"" ""employee_code"", ""emp"".""name"" ""employee_name"",
                                                    ""emp"".""email_id"" ""email_id"", ""emp"".""joining_date"" ""joining_date"", ""emp"".""proposed_lwd"" ""proposed_lwd"", ""emp"".""department"" ""department"", 
                                                    ""lc"".""location_name"" ""location"", ""wd"".""working_date""::Date ""working_date"", extract(isodow FROM ""wd"".""working_date"") ""week_day_number"",
                                                    ""dg"".""designation_name"" ""designation_name"", ""dg"".""grade"" ""grade"", ""bu"".""bu"" ""business_unit"", ""competency"".""CompetencyName"" ""competency"",
                                                        COALESCE((Select ""RatePerHour"" from ""RateDesignationMaster"" ""rmd"" where ""rmd"".""grade"" =  ""dg"".""grade"" limit 1),0) as ""hourly_rate""

                                                    from ""Employees"" ""emp""

                                                    left join ""Locations"" ""lc"" on  ""lc"".""location_id"" = ""emp"".""location_id""

                                                    left join ""Designations"" ""dg"" on  ""dg"".""designation_id"" = ""emp"".""designation_id""

                                                    left join (Select distinct bu_id, bu from ""BUTreeMappings"" as TempBU) as ""bu"" on  ""bu"".""bu_id"" = ""emp"".""business_unit_id""
                                                                left join (Select distinct ""CompetencyId"", ""CompetencyName"" from ""Competencies"" as TempCOMPETENCY) ""competency"" on  ""competency"".""CompetencyId"" = ""emp"".""CompetencyId""
                            
                                                        CROSS JOIN generate_series(current_date - interval ''6 month'', current_date + interval ''6 months'', ''1 day'') AS wd(working_date)
                                                        WHERE ""emp"".""isactive""= true and
                                                        extract(isodow FROM wd.working_date) NOT IN (6, 7)
	 
	                                                '::text) ewd(employee_mid text, employee_code text, employee_name text, email_id text, joining_date date, proposed_lwd date, department text, location text, working_date date, week_day_number numeric, designation_name text, grade text, business_unit text, competency text, hourly_rate numeric)
            WITH DATA;
            ";

            migrationBuilder.Sql(cmd);

            migrationBuilder.Sql(
               @$"DROP INDEX IF EXISTS employee_working_days_index;
                CREATE UNIQUE INDEX employee_working_days_index ON employee_working_days 
                (""employee_mid"", ""employee_code"", ""email_id"", ""working_date"", ""designation_name"", ""grade"", ""business_unit"", ""competency"", ""location"", ""department"");
                "
               );

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP INDEX IF EXISTS employee_working_days_index;");
            migrationBuilder.Sql("DROP MATERIALIZED VIEW  IF EXISTS employee_working_days");
        }
    }
}
