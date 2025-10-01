using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WCGT.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class employee_add_QUALIFICATIONS_PAST_EMPLOYMENT_DETAILS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "education_qualification",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    qualification = table.Column<string>(type: "text", nullable: false),
                    institution_name_location = table.Column<string>(type: "text", nullable: false),
                    month_year_of_passing = table.Column<string>(type: "text", nullable: false),
                    area_of_specialisation = table.Column<string>(type: "text", nullable: false),
                    employee_mid = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_education_qualification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_education_qualification_Employees_employee_mid",
                        column: x => x.employee_mid,
                        principalTable: "Employees",
                        principalColumn: "employee_mid");
                });

            migrationBuilder.CreateTable(
                name: "language",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    employee_mid = table.Column<string>(type: "text", nullable: false),
                    language_name = table.Column<string>(type: "text", nullable: false),
                    read_flag = table.Column<string>(type: "text", nullable: false),
                    write_flag = table.Column<string>(type: "text", nullable: false),
                    speak_flag = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_language", x => x.id);
                    table.ForeignKey(
                        name: "FK_language_Employees_employee_mid",
                        column: x => x.employee_mid,
                        principalTable: "Employees",
                        principalColumn: "employee_mid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "past_employment_details",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    employee_mid = table.Column<string>(type: "text", nullable: false),
                    name_of_employer = table.Column<string>(type: "text", nullable: false),
                    from_date = table.Column<string>(type: "text", nullable: false),
                    to_date = table.Column<string>(type: "text", nullable: false),
                    last_designation_held = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_past_employment_details", x => x.id);
                    table.ForeignKey(
                        name: "FK_past_employment_details_Employees_employee_mid",
                        column: x => x.employee_mid,
                        principalTable: "Employees",
                        principalColumn: "employee_mid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "professional_qualification",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    qualification = table.Column<string>(type: "text", nullable: false),
                    institution_name_location = table.Column<string>(type: "text", nullable: false),
                    month_year_of_passing = table.Column<string>(type: "text", nullable: false),
                    area_of_specialisation = table.Column<string>(type: "text", nullable: false),
                    employee_mid = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_professional_qualification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_professional_qualification_Employees_employee_mid",
                        column: x => x.employee_mid,
                        principalTable: "Employees",
                        principalColumn: "employee_mid");
                });

            migrationBuilder.CreateTable(
                name: "qualifications",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    qualification_type = table.Column<string>(type: "text", nullable: false),
                    qualification = table.Column<string>(type: "text", nullable: false),
                    institution_name_location = table.Column<string>(type: "text", nullable: false),
                    month_year_of_passing = table.Column<string>(type: "text", nullable: false),
                    area_of_specialisation = table.Column<string>(type: "text", nullable: false),
                    employee_mid = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_qualifications", x => x.id);
                    table.ForeignKey(
                        name: "FK_qualifications_Employees_employee_mid",
                        column: x => x.employee_mid,
                        principalTable: "Employees",
                        principalColumn: "employee_mid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_education_qualification_employee_mid",
                table: "education_qualification",
                column: "employee_mid");

            migrationBuilder.CreateIndex(
                name: "IX_language_employee_mid",
                table: "language",
                column: "employee_mid");

            migrationBuilder.CreateIndex(
                name: "IX_past_employment_details_employee_mid",
                table: "past_employment_details",
                column: "employee_mid");

            migrationBuilder.CreateIndex(
                name: "IX_professional_qualification_employee_mid",
                table: "professional_qualification",
                column: "employee_mid");

            migrationBuilder.CreateIndex(
                name: "IX_qualifications_employee_mid",
                table: "qualifications",
                column: "employee_mid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "education_qualification");

            migrationBuilder.DropTable(
                name: "language");

            migrationBuilder.DropTable(
                name: "past_employment_details");

            migrationBuilder.DropTable(
                name: "professional_qualification");

            migrationBuilder.DropTable(
                name: "qualifications");
        }
    }
}
