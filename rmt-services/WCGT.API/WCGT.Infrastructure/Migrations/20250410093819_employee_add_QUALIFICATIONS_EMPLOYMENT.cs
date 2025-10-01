using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WCGT.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class employee_add_QUALIFICATIONS_EMPLOYMENT : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "education_qualification");

            migrationBuilder.DropTable(
                name: "professional_qualification");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "education_qualification",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    area_of_specialisation = table.Column<string>(type: "text", nullable: false),
                    employee_mid = table.Column<string>(type: "text", nullable: true),
                    institution_name_location = table.Column<string>(type: "text", nullable: false),
                    month_year_of_passing = table.Column<string>(type: "text", nullable: false),
                    qualification = table.Column<string>(type: "text", nullable: false)
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
                name: "professional_qualification",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    area_of_specialisation = table.Column<string>(type: "text", nullable: false),
                    employee_mid = table.Column<string>(type: "text", nullable: true),
                    institution_name_location = table.Column<string>(type: "text", nullable: false),
                    month_year_of_passing = table.Column<string>(type: "text", nullable: false),
                    qualification = table.Column<string>(type: "text", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_education_qualification_employee_mid",
                table: "education_qualification",
                column: "employee_mid");

            migrationBuilder.CreateIndex(
                name: "IX_professional_qualification_employee_mid",
                table: "professional_qualification",
                column: "employee_mid");
        }
    }
}
