using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Employee.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class expoutsidegt3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeProjectExperience_EmployeeProfile_employee_profile_~",
                table: "EmployeeProjectExperience");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeProjectExperience",
                table: "EmployeeProjectExperience");

            migrationBuilder.RenameTable(
                name: "EmployeeProjectExperience",
                newName: "EmployeeExperienceOutsideGT");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeProjectExperience_employee_profile_id",
                table: "EmployeeExperienceOutsideGT",
                newName: "IX_EmployeeExperienceOutsideGT_employee_profile_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeExperienceOutsideGT",
                table: "EmployeeExperienceOutsideGT",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeExperienceOutsideGT_EmployeeProfile_employee_profil~",
                table: "EmployeeExperienceOutsideGT",
                column: "employee_profile_id",
                principalTable: "EmployeeProfile",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeExperienceOutsideGT_EmployeeProfile_employee_profil~",
                table: "EmployeeExperienceOutsideGT");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EmployeeExperienceOutsideGT",
                table: "EmployeeExperienceOutsideGT");

            migrationBuilder.RenameTable(
                name: "EmployeeExperienceOutsideGT",
                newName: "EmployeeProjectExperience");

            migrationBuilder.RenameIndex(
                name: "IX_EmployeeExperienceOutsideGT_employee_profile_id",
                table: "EmployeeProjectExperience",
                newName: "IX_EmployeeProjectExperience_employee_profile_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EmployeeProjectExperience",
                table: "EmployeeProjectExperience",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeProjectExperience_EmployeeProfile_employee_profile_~",
                table: "EmployeeProjectExperience",
                column: "employee_profile_id",
                principalTable: "EmployeeProfile",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
