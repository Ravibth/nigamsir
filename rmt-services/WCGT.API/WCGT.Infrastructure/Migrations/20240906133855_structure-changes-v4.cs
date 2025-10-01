using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WCGT.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class structurechangesv4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Expertise_id",
                table: "RateDesignationMaster");

            migrationBuilder.DropColumn(
                name: "expertise_id",
                table: "Pipelines");

            migrationBuilder.DropColumn(
                name: "revenue_id",
                table: "Pipelines");

            migrationBuilder.DropColumn(
                name: "service_line_id",
                table: "Pipelines");

            migrationBuilder.DropColumn(
                name: "sme_group_id",
                table: "Pipelines");

            migrationBuilder.DropColumn(
                name: "sme_id",
                table: "Pipelines");

            migrationBuilder.DropColumn(
                name: "expertise_id",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "offering",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "revenue_id",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "sme_id",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "smeg_id",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "solution",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "Competency",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "expertise",
                table: "BUTreeMappings");

            migrationBuilder.DropColumn(
                name: "expertise_id",
                table: "BUTreeMappings");

            migrationBuilder.DropColumn(
                name: "expertise_leader_mid",
                table: "BUTreeMappings");

            migrationBuilder.DropColumn(
                name: "revenue_id",
                table: "BUTreeMappings");

            migrationBuilder.DropColumn(
                name: "ru_name",
                table: "BUTreeMappings");

            migrationBuilder.DropColumn(
                name: "sme_group",
                table: "BUTreeMappings");

            migrationBuilder.DropColumn(
                name: "sme_group_id",
                table: "BUTreeMappings");

            migrationBuilder.DropColumn(
                name: "sme_group_leader_mid",
                table: "BUTreeMappings");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Expertise_id",
                table: "RateDesignationMaster",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "expertise_id",
                table: "Pipelines",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "revenue_id",
                table: "Pipelines",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "service_line_id",
                table: "Pipelines",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sme_group_id",
                table: "Pipelines",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sme_id",
                table: "Pipelines",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "expertise_id",
                table: "Jobs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "offering",
                table: "Jobs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "revenue_id",
                table: "Jobs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sme_id",
                table: "Jobs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "smeg_id",
                table: "Jobs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "solution",
                table: "Jobs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Competency",
                table: "Employees",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "expertise",
                table: "BUTreeMappings",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "expertise_id",
                table: "BUTreeMappings",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "expertise_leader_mid",
                table: "BUTreeMappings",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "revenue_id",
                table: "BUTreeMappings",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ru_name",
                table: "BUTreeMappings",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sme_group",
                table: "BUTreeMappings",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sme_group_id",
                table: "BUTreeMappings",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "sme_group_leader_mid",
                table: "BUTreeMappings",
                type: "text",
                nullable: true);
        }
    }
}
