using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace RMT.Employee.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class expoutsidegt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeProjectExperience",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    job_name = table.Column<string>(type: "text", nullable: false),
                    client_group = table.Column<string>(type: "text", nullable: false),
                    client_name = table.Column<string>(type: "text", nullable: false),
                    business_unit = table.Column<string>(type: "text", nullable: false),
                    Offering = table.Column<string>(type: "text", nullable: false),
                    Solution = table.Column<string>(type: "text", nullable: false),
                    industry = table.Column<string>(type: "text", nullable: false),
                    sub_industry = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false),
                    Duration = table.Column<string>(type: "text", nullable: false),
                    job_start_date = table.Column<string>(type: "text", nullable: false),
                    job_end_date = table.Column<string>(type: "text", nullable: false),
                    primary_el = table.Column<string>(type: "text", nullable: false),
                    csp = table.Column<string>(type: "text", nullable: false),
                    project_type = table.Column<string>(type: "text", nullable: false),
                    project_description = table.Column<string>(type: "text", nullable: false),
                    task_description = table.Column<string>(type: "text", nullable: false),
                    skills_utilized = table.Column<string>(type: "text", nullable: false),
                    actual_hours = table.Column<double>(type: "double precision", nullable: false),
                    EmployeeProfileid = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeProjectExperience", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeProjectExperience_EmployeeProfile_EmployeeProfileid",
                        column: x => x.EmployeeProfileid,
                        principalTable: "EmployeeProfile",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeProjectExperience_EmployeeProfileid",
                table: "EmployeeProjectExperience",
                column: "EmployeeProfileid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeProjectExperience");
        }
    }
}
