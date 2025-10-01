using Microsoft.EntityFrameworkCore.Migrations;
using RMT.Reports.Infrastructure.Helpers;

#nullable disable

namespace RMT.Reports.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CreateEmployeeSkillView : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string ConnStr_SkillDB = ConfigConstants.SkillDbConnStr;
            string cmd = @$"
            DROP MATERIALIZED VIEW IF EXISTS employee_skill;

            CREATE MATERIALIZED VIEW IF NOT EXISTS employee_skill
            TABLESPACE pg_default
            AS
            SELECT 
                empid AS employee_mid,
                email AS email_id,
                string_agg(skillname || ' (' || proficiency || ')', ', ' ORDER BY skillname) AS skills
            FROM dblink(
                    '{ConnStr_SkillDB}',
                    '
                        SELECT 
                            ""EmpId"",
                            ""Email"",
                            ""SkillName"",
                            ""Proficiency"",
                            ""Status"",
                            ""IsActive""
                        FROM ""UserSkills""
                        WHERE ""IsActive"" = true 
                          AND ""Status"" = ''Approved''
                    '::text
                ) us(empid text, email text, skillname text, proficiency text, status text, isactive boolean)
            GROUP BY empid, email
            WITH DATA;
            ";
            migrationBuilder.Sql(cmd);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DROP MATERIALIZED VIEW  IF EXISTS employee_skill");
        }
    }
}
