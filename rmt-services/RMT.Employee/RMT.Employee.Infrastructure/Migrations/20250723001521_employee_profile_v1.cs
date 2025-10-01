using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace RMT.Employee.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class employee_profile_v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeProfile",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    employee_name = table.Column<string>(type: "text", nullable: false),
                    employee_email = table.Column<string>(type: "text", nullable: false),
                    employee_mid = table.Column<string>(type: "text", nullable: false),
                    employee_code = table.Column<string>(type: "text", nullable: false),
                    business_unit = table.Column<string>(type: "text", nullable: false),
                    competency = table.Column<string>(type: "text", nullable: false),
                    supercoach_email = table.Column<string>(type: "text", nullable: true),
                    supercoach_mid = table.Column<string>(type: "text", nullable: true),
                    supercoach_name = table.Column<string>(type: "text", nullable: true),
                    co_supercoach_email = table.Column<string>(type: "text", nullable: true),
                    co_supercoach_mid = table.Column<string>(type: "text", nullable: true),
                    co_supercoach_name = table.Column<string>(type: "text", nullable: true),
                    location = table.Column<string>(type: "text", nullable: true),
                    linkedin_url = table.Column<string>(type: "text", nullable: true),
                    employee_type = table.Column<string>(type: "text", nullable: true),
                    year_of_experience = table.Column<int>(type: "integer", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    modified_by = table.Column<string>(type: "text", nullable: true),
                    created_by = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeProfile", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeLanguage",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    employee_mid = table.Column<string>(type: "text", nullable: false),
                    language_name = table.Column<string>(type: "text", nullable: false),
                    read = table.Column<string>(type: "text", nullable: true),
                    write = table.Column<string>(type: "text", nullable: true),
                    speak = table.Column<string>(type: "text", nullable: true),
                    employee_profile_id = table.Column<long>(type: "bigint", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    modified_by = table.Column<string>(type: "text", nullable: true),
                    created_by = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeLanguage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeLanguage_EmployeeProfile_employee_profile_id",
                        column: x => x.employee_profile_id,
                        principalTable: "EmployeeProfile",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeQualification",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    qualification_type = table.Column<string>(type: "text", nullable: false),
                    qualification = table.Column<string>(type: "text", nullable: false),
                    institute_location_name = table.Column<string>(type: "text", nullable: false),
                    month_year_of_passing = table.Column<string>(type: "text", nullable: false),
                    area_of_specialisation = table.Column<string>(type: "text", nullable: false),
                    employee_mid = table.Column<string>(type: "text", nullable: false),
                    employee_profile_id = table.Column<long>(type: "bigint", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    modified_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    modified_by = table.Column<string>(type: "text", nullable: true),
                    created_by = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeQualification", x => x.id);
                    table.ForeignKey(
                        name: "FK_EmployeeQualification_EmployeeProfile_employee_profile_id",
                        column: x => x.employee_profile_id,
                        principalTable: "EmployeeProfile",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeLanguage_employee_profile_id",
                table: "EmployeeLanguage",
                column: "employee_profile_id");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeQualification_employee_profile_id",
                table: "EmployeeQualification",
                column: "employee_profile_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeLanguage");

            migrationBuilder.DropTable(
                name: "EmployeeQualification");

            migrationBuilder.DropTable(
                name: "EmployeeProfile");
        }
    }
}
