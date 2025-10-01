using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WCGT.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class employee_view_report : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            string cmd = @$"
 
            CREATE OR REPLACE VIEW public.view_employees_report
            AS
            SELECT ""Employees"".employee_mid,
                ""Employees"".company_name,
                ""Employees"".employee_code,
                ""Employees"".first_name,
                ""Employees"".middle_name,
                ""Employees"".last_name,
                ""Employees"".name,
                ""Employees"".designation_id,
                ""Employees"".department,
                ""Employees"".location_id,
                ""Employees"".service_line_id,
                (""Employees"".employee_mid || '__'::text) || ""Employees"".email_id AS email_id,
                ""Employees"".joining_date,
                ""Employees"".reporting_partner_mid,
                ""Employees"".group_head_mid,
                ""Employees"".business_unit_id,
                ""Employees"".smeg_id,
                ""Employees"".sme_id,
                ""Employees"".expertise_id,
                ""Employees"".specical_day,
                ""Employees"".birthday,
                ""Employees"".isactive,
                ""Employees"".createdat,
                ""Employees"".modifiedat,
                ""Employees"".createdby,
                ""Employees"".modifiedby,
                ""Employees"".employee_status,
                ""Employees"".proposed_lwd,
                ""Employees"".resignation_date,
                ""Employees"".supercoach_mid
            FROM ""Employees""
            WHERE ""Employees"".email_id ~~* '%@GTBResourceplanner.onmicrosoft.com%'::text OR ""Employees"".email_id ~~* '%@nagarro.com%'::text
            UNION ALL
            SELECT ""Employees"".employee_mid,
                ""Employees"".company_name,
                ""Employees"".employee_code,
                ""Employees"".first_name,
                ""Employees"".middle_name,
                ""Employees"".last_name,
                ""Employees"".name,
                ""Employees"".designation_id,
                ""Employees"".department,
                ""Employees"".location_id,
                ""Employees"".service_line_id,
                (""Employees"".employee_mid || '__'::text) || ""Employees"".email_id AS email_id,
                ""Employees"".joining_date,
                ""Employees"".reporting_partner_mid,
                ""Employees"".group_head_mid,
                ""Employees"".business_unit_id,
                ""Employees"".smeg_id,
                ""Employees"".sme_id,
                ""Employees"".expertise_id,
                ""Employees"".specical_day,
                ""Employees"".birthday,
                ""Employees"".isactive,
                ""Employees"".createdat,
                ""Employees"".modifiedat,
                ""Employees"".createdby,
                ""Employees"".modifiedby,
                ""Employees"".employee_status,
                ""Employees"".proposed_lwd,
                ""Employees"".resignation_date,
                ""Employees"".supercoach_mid
            FROM ""Employees""
            WHERE ""Employees"".isactive = true AND (""Employees"".email_id !~~* '%@GTBResourceplanner.onmicrosoft.com%'::text OR ""Employees"".email_id ~~* '%@nagarro.com%'::text)
            LIMIT 100;

            ";

            migrationBuilder.Sql(cmd);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
