using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WCGT.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class structurechangesv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectJobCodes");

            migrationBuilder.DropTable(
                name: "ProjectRoles");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.AddColumn<string>(
                name: "CompetencyId",
                table: "RateDesignationMaster",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "offering_id",
                table: "Pipelines",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "solution_id",
                table: "Pipelines",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "offering",
                table: "Jobs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "offering_id",
                table: "Jobs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "solution",
                table: "Jobs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "solution_id",
                table: "Jobs",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "offering",
                table: "BUTreeMappings",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "offering_id",
                table: "BUTreeMappings",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "offering_leader_mid",
                table: "BUTreeMappings",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "solution",
                table: "BUTreeMappings",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "solution_id",
                table: "BUTreeMappings",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "solution_leader_mid",
                table: "BUTreeMappings",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompetencyId",
                table: "RateDesignationMaster");

            migrationBuilder.DropColumn(
                name: "offering_id",
                table: "Pipelines");

            migrationBuilder.DropColumn(
                name: "solution_id",
                table: "Pipelines");

            migrationBuilder.DropColumn(
                name: "offering",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "offering_id",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "solution",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "solution_id",
                table: "Jobs");

            migrationBuilder.DropColumn(
                name: "offering",
                table: "BUTreeMappings");

            migrationBuilder.DropColumn(
                name: "offering_id",
                table: "BUTreeMappings");

            migrationBuilder.DropColumn(
                name: "offering_leader_mid",
                table: "BUTreeMappings");

            migrationBuilder.DropColumn(
                name: "solution",
                table: "BUTreeMappings");

            migrationBuilder.DropColumn(
                name: "solution_id",
                table: "BUTreeMappings");

            migrationBuilder.DropColumn(
                name: "solution_leader_mid",
                table: "BUTreeMappings");

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    client_id = table.Column<string>(type: "text", nullable: true),
                    ClientLegalEntitypar_aid = table.Column<string>(type: "text", nullable: true),
                    client_group_code = table.Column<string>(type: "text", nullable: true),
                    contact_name = table.Column<string>(type: "text", nullable: true),
                    createdat = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    createdby = table.Column<string>(type: "text", nullable: true),
                    creation_date = table.Column<DateOnly>(type: "date", nullable: true),
                    expected = table.Column<string>(type: "text", nullable: true),
                    finalproposedfee = table.Column<string>(type: "text", nullable: true),
                    finalproposedope = table.Column<string>(type: "text", nullable: true),
                    isactive = table.Column<bool>(type: "boolean", nullable: true),
                    location_id = table.Column<string>(type: "text", nullable: true),
                    modifiedat = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    modifiedby = table.Column<string>(type: "text", nullable: true),
                    nrccstatus = table.Column<string>(type: "text", nullable: true),
                    pipeline_code = table.Column<string>(type: "text", nullable: true),
                    pipeline_description = table.Column<string>(type: "text", nullable: true),
                    pipeline_id = table.Column<string>(type: "text", nullable: true),
                    pipeline_name = table.Column<string>(type: "text", nullable: true),
                    pipeline_status = table.Column<string>(type: "text", nullable: true),
                    project_code = table.Column<string>(type: "text", nullable: true),
                    project_name = table.Column<string>(type: "text", nullable: true),
                    recurring = table.Column<string>(type: "text", nullable: true),
                    revenue_id = table.Column<string>(type: "text", nullable: true),
                    sector_id = table.Column<string>(type: "text", nullable: true),
                    service_line_id = table.Column<string>(type: "text", nullable: true),
                    sme_group_id = table.Column<string>(type: "text", nullable: true),
                    sme_id = table.Column<string>(type: "text", nullable: true),
                    win_probablity = table.Column<string>(type: "text", nullable: true),
                    won_date = table.Column<DateOnly>(type: "date", nullable: true),
                    won_expected_recovery = table.Column<string>(type: "text", nullable: true),
                    won_reason = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.id);
                    table.ForeignKey(
                        name: "FK_Projects_ClientLegalEntitys_ClientLegalEntitypar_aid",
                        column: x => x.ClientLegalEntitypar_aid,
                        principalTable: "ClientLegalEntitys",
                        principalColumn: "par_aid");
                    table.ForeignKey(
                        name: "FK_Projects_Clients_client_id",
                        column: x => x.client_id,
                        principalTable: "Clients",
                        principalColumn: "client_id");
                });

            migrationBuilder.CreateTable(
                name: "ProjectJobCodes",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    project_id = table.Column<long>(type: "bigint", nullable: true),
                    createdat = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    createdby = table.Column<string>(type: "text", nullable: true),
                    isactive = table.Column<bool>(type: "boolean", nullable: true),
                    job_client = table.Column<string>(type: "text", nullable: true),
                    job_code = table.Column<string>(type: "text", nullable: true),
                    job_description = table.Column<string>(type: "text", nullable: false),
                    job_id = table.Column<string>(type: "text", nullable: true),
                    job_name = table.Column<string>(type: "text", nullable: true),
                    modifiedat = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    modifiedby = table.Column<string>(type: "text", nullable: true),
                    pipeline_code = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectJobCodes", x => x.id);
                    table.ForeignKey(
                        name: "FK_ProjectJobCodes_Projects_project_id",
                        column: x => x.project_id,
                        principalTable: "Projects",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "ProjectRoles",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    project_id = table.Column<long>(type: "bigint", nullable: true),
                    createdat = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    createdby = table.Column<string>(type: "text", nullable: true),
                    isactive = table.Column<bool>(type: "boolean", nullable: true),
                    modifiedat = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    modifiedby = table.Column<string>(type: "text", nullable: true),
                    user_email = table.Column<string>(type: "text", nullable: true),
                    user_role = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectRoles", x => x.id);
                    table.ForeignKey(
                        name: "FK_ProjectRoles_Projects_project_id",
                        column: x => x.project_id,
                        principalTable: "Projects",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectJobCodes_project_id",
                table: "ProjectJobCodes",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectRoles_project_id",
                table: "ProjectRoles",
                column: "project_id");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_client_id",
                table: "Projects",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ClientLegalEntitypar_aid",
                table: "Projects",
                column: "ClientLegalEntitypar_aid");
        }
    }
}
