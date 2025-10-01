using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace RMT.Employee.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class expoutsidegt2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeProjectExperience_EmployeeProfile_EmployeeProfileid",
                table: "EmployeeProjectExperience");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeProjectExperience_EmployeeProfileid",
                table: "EmployeeProjectExperience");

            migrationBuilder.DropColumn(
                name: "Duration",
                table: "EmployeeProjectExperience");

            migrationBuilder.DropColumn(
                name: "EmployeeProfileid",
                table: "EmployeeProjectExperience");

            migrationBuilder.DropColumn(
                name: "Offering",
                table: "EmployeeProjectExperience");

            migrationBuilder.DropColumn(
                name: "Role",
                table: "EmployeeProjectExperience");

            migrationBuilder.DropColumn(
                name: "Solution",
                table: "EmployeeProjectExperience");

            migrationBuilder.DropColumn(
                name: "actual_hours",
                table: "EmployeeProjectExperience");

            migrationBuilder.DropColumn(
                name: "business_unit",
                table: "EmployeeProjectExperience");

            migrationBuilder.DropColumn(
                name: "client_group",
                table: "EmployeeProjectExperience");

            migrationBuilder.DropColumn(
                name: "csp",
                table: "EmployeeProjectExperience");

            migrationBuilder.DropColumn(
                name: "job_name",
                table: "EmployeeProjectExperience");

            migrationBuilder.DropColumn(
                name: "primary_el",
                table: "EmployeeProjectExperience");

            migrationBuilder.DropColumn(
                name: "project_type",
                table: "EmployeeProjectExperience");

            migrationBuilder.DropColumn(
                name: "skills_utilized",
                table: "EmployeeProjectExperience");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "EmployeeProjectExperience",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "task_description",
                table: "EmployeeProjectExperience",
                newName: "created_by");

            migrationBuilder.AlterColumn<string>(
                name: "sub_industry",
                table: "EmployeeProjectExperience",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "project_description",
                table: "EmployeeProjectExperience",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "job_start_date",
                table: "EmployeeProjectExperience",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "job_end_date",
                table: "EmployeeProjectExperience",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "industry",
                table: "EmployeeProjectExperience",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "client_name",
                table: "EmployeeProjectExperience",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<long>(
                name: "id",
                table: "EmployeeProjectExperience",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<DateTime>(
                name: "created_at",
                table: "EmployeeProjectExperience",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<long>(
                name: "employee_profile_id",
                table: "EmployeeProjectExperience",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<DateTime>(
                name: "modified_at",
                table: "EmployeeProjectExperience",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "modified_by",
                table: "EmployeeProjectExperience",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "project_location",
                table: "EmployeeProjectExperience",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "project_name",
                table: "EmployeeProjectExperience",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "tasks_performed",
                table: "EmployeeProjectExperience",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeProjectExperience_employee_profile_id",
                table: "EmployeeProjectExperience",
                column: "employee_profile_id");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeProjectExperience_EmployeeProfile_employee_profile_~",
                table: "EmployeeProjectExperience",
                column: "employee_profile_id",
                principalTable: "EmployeeProfile",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeProjectExperience_EmployeeProfile_employee_profile_~",
                table: "EmployeeProjectExperience");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeProjectExperience_employee_profile_id",
                table: "EmployeeProjectExperience");

            migrationBuilder.DropColumn(
                name: "created_at",
                table: "EmployeeProjectExperience");

            migrationBuilder.DropColumn(
                name: "employee_profile_id",
                table: "EmployeeProjectExperience");

            migrationBuilder.DropColumn(
                name: "modified_at",
                table: "EmployeeProjectExperience");

            migrationBuilder.DropColumn(
                name: "modified_by",
                table: "EmployeeProjectExperience");

            migrationBuilder.DropColumn(
                name: "project_location",
                table: "EmployeeProjectExperience");

            migrationBuilder.DropColumn(
                name: "project_name",
                table: "EmployeeProjectExperience");

            migrationBuilder.DropColumn(
                name: "tasks_performed",
                table: "EmployeeProjectExperience");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "EmployeeProjectExperience",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "created_by",
                table: "EmployeeProjectExperience",
                newName: "task_description");

            migrationBuilder.AlterColumn<string>(
                name: "sub_industry",
                table: "EmployeeProjectExperience",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "project_description",
                table: "EmployeeProjectExperience",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "job_start_date",
                table: "EmployeeProjectExperience",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "job_end_date",
                table: "EmployeeProjectExperience",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "industry",
                table: "EmployeeProjectExperience",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "client_name",
                table: "EmployeeProjectExperience",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "EmployeeProjectExperience",
                type: "integer",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddColumn<string>(
                name: "Duration",
                table: "EmployeeProjectExperience",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "EmployeeProfileid",
                table: "EmployeeProjectExperience",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Offering",
                table: "EmployeeProjectExperience",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Role",
                table: "EmployeeProjectExperience",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Solution",
                table: "EmployeeProjectExperience",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<double>(
                name: "actual_hours",
                table: "EmployeeProjectExperience",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<string>(
                name: "business_unit",
                table: "EmployeeProjectExperience",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "client_group",
                table: "EmployeeProjectExperience",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "csp",
                table: "EmployeeProjectExperience",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "job_name",
                table: "EmployeeProjectExperience",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "primary_el",
                table: "EmployeeProjectExperience",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "project_type",
                table: "EmployeeProjectExperience",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "skills_utilized",
                table: "EmployeeProjectExperience",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeProjectExperience_EmployeeProfileid",
                table: "EmployeeProjectExperience",
                column: "EmployeeProfileid");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeProjectExperience_EmployeeProfile_EmployeeProfileid",
                table: "EmployeeProjectExperience",
                column: "EmployeeProfileid",
                principalTable: "EmployeeProfile",
                principalColumn: "id");
        }
    }
}
