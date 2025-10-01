using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WCGT.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class employee_add_EMPLOYMENT_Language : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_language_Employees_employee_mid",
                table: "language");

            migrationBuilder.DropForeignKey(
                name: "FK_qualifications_Employees_employee_mid",
                table: "qualifications");

            migrationBuilder.DropTable(
                name: "past_employment_details");

            migrationBuilder.DropPrimaryKey(
                name: "PK_qualifications",
                table: "qualifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_language",
                table: "language");

            migrationBuilder.RenameTable(
                name: "qualifications",
                newName: "Qualifications");

            migrationBuilder.RenameTable(
                name: "language",
                newName: "Language");

            migrationBuilder.RenameIndex(
                name: "IX_qualifications_employee_mid",
                table: "Qualifications",
                newName: "IX_Qualifications_employee_mid");

            migrationBuilder.RenameIndex(
                name: "IX_language_employee_mid",
                table: "Language",
                newName: "IX_Language_employee_mid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Qualifications",
                table: "Qualifications",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Language",
                table: "Language",
                column: "id");

            migrationBuilder.CreateTable(
                name: "PastEmploymentDetails",
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
                    table.PrimaryKey("PK_PastEmploymentDetails", x => x.id);
                    table.ForeignKey(
                        name: "FK_PastEmploymentDetails_Employees_employee_mid",
                        column: x => x.employee_mid,
                        principalTable: "Employees",
                        principalColumn: "employee_mid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PastEmploymentDetails_employee_mid",
                table: "PastEmploymentDetails",
                column: "employee_mid");

            migrationBuilder.AddForeignKey(
                name: "FK_Language_Employees_employee_mid",
                table: "Language",
                column: "employee_mid",
                principalTable: "Employees",
                principalColumn: "employee_mid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Qualifications_Employees_employee_mid",
                table: "Qualifications",
                column: "employee_mid",
                principalTable: "Employees",
                principalColumn: "employee_mid",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Language_Employees_employee_mid",
                table: "Language");

            migrationBuilder.DropForeignKey(
                name: "FK_Qualifications_Employees_employee_mid",
                table: "Qualifications");

            migrationBuilder.DropTable(
                name: "PastEmploymentDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Qualifications",
                table: "Qualifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Language",
                table: "Language");

            migrationBuilder.RenameTable(
                name: "Qualifications",
                newName: "qualifications");

            migrationBuilder.RenameTable(
                name: "Language",
                newName: "language");

            migrationBuilder.RenameIndex(
                name: "IX_Qualifications_employee_mid",
                table: "qualifications",
                newName: "IX_qualifications_employee_mid");

            migrationBuilder.RenameIndex(
                name: "IX_Language_employee_mid",
                table: "language",
                newName: "IX_language_employee_mid");

            migrationBuilder.AddPrimaryKey(
                name: "PK_qualifications",
                table: "qualifications",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_language",
                table: "language",
                column: "id");

            migrationBuilder.CreateTable(
                name: "past_employment_details",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    employee_mid = table.Column<string>(type: "text", nullable: false),
                    from_date = table.Column<string>(type: "text", nullable: false),
                    last_designation_held = table.Column<string>(type: "text", nullable: false),
                    name_of_employer = table.Column<string>(type: "text", nullable: false),
                    to_date = table.Column<string>(type: "text", nullable: false)
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

            migrationBuilder.CreateIndex(
                name: "IX_past_employment_details_employee_mid",
                table: "past_employment_details",
                column: "employee_mid");

            migrationBuilder.AddForeignKey(
                name: "FK_language_Employees_employee_mid",
                table: "language",
                column: "employee_mid",
                principalTable: "Employees",
                principalColumn: "employee_mid",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_qualifications_Employees_employee_mid",
                table: "qualifications",
                column: "employee_mid",
                principalTable: "Employees",
                principalColumn: "employee_mid",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
