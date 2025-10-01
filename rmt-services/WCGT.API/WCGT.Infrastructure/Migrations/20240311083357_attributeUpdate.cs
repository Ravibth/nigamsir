using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WCGT.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class attributeUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "JobFees",
                table: "Jobs",
                newName: "jobBudgetValue");

            migrationBuilder.DropColumn(
                name: "won_expected_recovery",
                table: "Pipelines");

            migrationBuilder.DropColumn(
                name: "finalproposedope",
                table: "Pipelines");

            migrationBuilder.DropColumn(
                name: "finalproposedfee",
                table: "Pipelines");

            migrationBuilder.AddColumn<string>(
               name: "won_expected_recovery",
               table: "Pipelines",
               type: "double precision",
               nullable: true);

            migrationBuilder.AddColumn<string>(
               name: "finalproposedope",
               table: "Pipelines",
               type: "double precision",
               nullable: true);

            migrationBuilder.AddColumn<string>(
               name: "finalproposedfee",
               table: "Pipelines",
               type: "double precision",
               nullable: true);

            //migrationBuilder.AlterColumn<double>(
            //    name: "won_expected_recovery",
            //    table: "Pipelines",
            //    type: "double precision",
            //    nullable: true,
            //    oldClrType: typeof(string),
            //    oldType: "text",
            //    oldNullable: true);

            //migrationBuilder.AlterColumn<double>(
            //    name: "finalproposedope",
            //    table: "Pipelines",
            //    type: "double precision",
            //    nullable: true,
            //    oldClrType: typeof(string),
            //    oldType: "text",
            //    oldNullable: true);

            //migrationBuilder.AlterColumn<double>(
            //    name: "finalproposedfee",
            //    table: "Pipelines",
            //    type: "double precision",
            //    nullable: true,
            //    oldClrType: typeof(string),
            //    oldType: "text",
            //    oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "bu_id",
                table: "Pipelines",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "end_date",
                table: "Pipelines",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "expertise_id",
                table: "Pipelines",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "start_date",
                table: "Pipelines",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sub_industry_id",
                table: "Pipelines",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "agreedJobFee",
                table: "Jobs",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "bu_id",
                table: "Jobs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "client_group_id",
                table: "Jobs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "industry_id",
                table: "Jobs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "pipeline_status",
                table: "Jobs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "recurring_type",
                table: "Jobs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sub_industry_id",
                table: "Jobs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "employee_status",
                table: "Employees",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "proposed_lwd",
                table: "Employees",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "resignation_date",
                table: "Employees",
                type: "date",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "supercoach_mid",
                table: "Employees",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "bu_id",
                table: "Pipelines");

            migrationBuilder.DropColumn(
                name: "end_date",
                table: "Pipelines");

            migrationBuilder.DropColumn(
                name: "expertise_id",
                table: "Pipelines");

            migrationBuilder.DropColumn(
                name: "start_date",
                table: "Pipelines");

            migrationBuilder.DropColumn(
                name: "sub_industry_id",
                table: "Pipelines");

            migrationBuilder.DropColumn(
                name: "agreedJobFee",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "bu_id",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "client_group_id",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "industry_id",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "pipeline_status",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "recurring_type",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "sub_industry_id",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "employee_status",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "proposed_lwd",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "resignation_date",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "supercoach_mid",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "jobBudgetValue",
                table: "Jobs",
                newName: "JobFees");

            migrationBuilder.AlterColumn<string>(
                name: "won_expected_recovery",
                table: "Pipelines",
                type: "text",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "finalproposedope",
                table: "Pipelines",
                type: "text",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "finalproposedfee",
                table: "Pipelines",
                type: "text",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldNullable: true);
        }
    }
}
