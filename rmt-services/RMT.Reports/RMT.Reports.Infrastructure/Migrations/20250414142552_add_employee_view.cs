using Microsoft.EntityFrameworkCore.Migrations;
using RMT.Reports.Infrastructure.Helpers;

#nullable disable

namespace RMT.Reports.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_employee_view : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string ConnStr_WcgtDB = ConfigConstants.WcgtDbConnStr;

            string cmd = @$"

            DROP MATERIALIZED VIEW IF EXISTS employee_view;

            CREATE MATERIALIZED VIEW IF NOT EXISTS employee_view
            TABLESPACE pg_default
            AS
             SELECT employee_mid,
                employee_code,
                name,
                email_id,
                joining_date,
                csc_mid,
                group_head_mid,
                business_unit_id,
                sme_id,
                expertise_id,
                employee_status,
                supercoach_mid,
                competencyid,
                supercoach_name,
                csc_name
               FROM dblink('{ConnStr_WcgtDB}','
                         SELECT 
                    e.employee_mid,
                    e.employee_code,
                    e.name,
                    e.email_id,
                    e.joining_date,
                    e.reporting_partner_mid as csc_mid,
                    e.group_head_mid,
                    e.business_unit_id,
                    e.sme_id,
                    e.expertise_id,
                    e.employee_status,
                    e.supercoach_mid,
                    e.""CompetencyId"",    
                    (SELECT name FROM ""Employees"" WHERE employee_mid = e.supercoach_mid) AS supercoach_name,
                    (SELECT name FROM ""Employees"" WHERE employee_mid = e.reporting_partner_mid) AS csc_name
	                FROM ""Employees"" e 
                    '::text) employee_view(employee_mid text, employee_code text, name text, email_id text, 
                    joining_date date, csc_mid text, group_head_mid text, business_unit_id text, sme_id text, expertise_id text, 
                    employee_status text, supercoach_mid text, competencyid text, supercoach_name text, csc_name text)                           
                WITH DATA;
            ";
            migrationBuilder.Sql(cmd);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP MATERIALIZED VIEW  IF EXISTS employee_view");
        }
    }
}
