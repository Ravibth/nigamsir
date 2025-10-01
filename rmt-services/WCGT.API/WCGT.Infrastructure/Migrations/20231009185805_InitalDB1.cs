using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WCGT.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitalDB1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BUTreeMappings",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    bu = table.Column<string>(type: "text", nullable: false),
                    bu_id = table.Column<string>(type: "text", nullable: false),
                    expertise = table.Column<string>(type: "text", nullable: false),
                    expertise_id = table.Column<string>(type: "text", nullable: false),
                    sme_group = table.Column<string>(type: "text", nullable: false),
                    sme_group_id = table.Column<string>(type: "text", nullable: false),
                    ru_name = table.Column<string>(type: "text", nullable: false),
                    revenue_id = table.Column<string>(type: "text", nullable: false),
                    isactive = table.Column<bool>(type: "boolean", nullable: true),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modifiedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    createdby = table.Column<string>(type: "text", nullable: true),
                    modifiedby = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BUTreeMappings", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ClientLegalEntitys",
                columns: table => new
                {
                    par_aid = table.Column<string>(type: "text", nullable: false),
                    para_desc = table.Column<string>(type: "text", nullable: false),
                    isactive = table.Column<bool>(type: "boolean", nullable: true),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modifiedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    createdby = table.Column<string>(type: "text", nullable: true),
                    modifiedby = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientLegalEntitys", x => x.par_aid);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    client_id = table.Column<string>(type: "text", nullable: false),
                    client_group_code = table.Column<string>(type: "text", nullable: false),
                    job_client = table.Column<string>(type: "text", nullable: false),
                    client_group_name = table.Column<string>(type: "text", nullable: false),
                    legal_entity = table.Column<string>(type: "text", nullable: false),
                    isactive = table.Column<bool>(type: "boolean", nullable: true),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modifiedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    createdby = table.Column<string>(type: "text", nullable: true),
                    modifiedby = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.client_id);
                });

            migrationBuilder.CreateTable(
                name: "Designations",
                columns: table => new
                {
                    designation_id = table.Column<string>(type: "text", nullable: false),
                    designation_name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    isactive = table.Column<bool>(type: "boolean", nullable: true),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modifiedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    createdby = table.Column<string>(type: "text", nullable: true),
                    modifiedby = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Designations", x => x.designation_id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    employee_mid = table.Column<string>(type: "text", nullable: false),
                    company_name = table.Column<string>(type: "text", nullable: false),
                    employee_code = table.Column<string>(type: "text", nullable: false),
                    first_name = table.Column<string>(type: "text", nullable: false),
                    middle_name = table.Column<string>(type: "text", nullable: false),
                    last_name = table.Column<string>(type: "text", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    designation_id = table.Column<string>(type: "text", nullable: false),
                    department = table.Column<string>(type: "text", nullable: false),
                    location_id = table.Column<string>(type: "text", nullable: false),
                    service_line_id = table.Column<string>(type: "text", nullable: false),
                    email_id = table.Column<string>(type: "text", nullable: false),
                    joining_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    reporting_partner_mid = table.Column<string>(type: "text", nullable: false),
                    group_head_mid = table.Column<string>(type: "text", nullable: false),
                    business_unit_id = table.Column<string>(type: "text", nullable: false),
                    smeg_id = table.Column<string>(type: "text", nullable: false),
                    sme_id = table.Column<string>(type: "text", nullable: false),
                    expertise_id = table.Column<string>(type: "text", nullable: false),
                    specical_day = table.Column<DateOnly>(type: "date", nullable: false),
                    birthday = table.Column<DateOnly>(type: "date", nullable: false),
                    isactive = table.Column<bool>(type: "boolean", nullable: true),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modifiedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    createdby = table.Column<string>(type: "text", nullable: false),
                    modifiedby = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.employee_mid);
                });

            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    job_id = table.Column<string>(type: "text", nullable: false),
                    job_code = table.Column<string>(type: "text", nullable: true),
                    pipeline_id = table.Column<string>(type: "text", nullable: true),
                    pipeline_code = table.Column<string>(type: "text", nullable: true),
                    parent_job_id = table.Column<string>(type: "text", nullable: true),
                    job_name = table.Column<string>(type: "text", nullable: true),
                    job_description = table.Column<string>(type: "text", nullable: true),
                    asst_incharge = table.Column<string>(type: "text", nullable: true),
                    entity = table.Column<string>(type: "text", nullable: true),
                    job_client = table.Column<string>(type: "text", nullable: true),
                    expertise_id = table.Column<string>(type: "text", nullable: true),
                    smeg_id = table.Column<string>(type: "text", nullable: true),
                    revenue_id = table.Column<string>(type: "text", nullable: true),
                    sme_id = table.Column<string>(type: "text", nullable: true),
                    market = table.Column<string>(type: "text", nullable: true),
                    sub_market = table.Column<string>(type: "text", nullable: true),
                    job_location_id = table.Column<string>(type: "text", nullable: true),
                    billing_currency = table.Column<string>(type: "text", nullable: true),
                    is_chargeable = table.Column<bool>(type: "boolean", nullable: false),
                    remarks = table.Column<string>(type: "text", nullable: true),
                    start_date = table.Column<DateOnly>(type: "date", nullable: true),
                    end_date = table.Column<DateOnly>(type: "date", nullable: true),
                    active = table.Column<bool>(type: "boolean", nullable: false),
                    closed_job = table.Column<bool>(type: "boolean", nullable: false),
                    created_date = table.Column<DateOnly>(type: "date", nullable: true),
                    updated_date = table.Column<DateOnly>(type: "date", nullable: true),
                    isactive = table.Column<bool>(type: "boolean", nullable: true),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modifiedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    createdby = table.Column<string>(type: "text", nullable: true),
                    modifiedby = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.job_id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    location_id = table.Column<string>(type: "text", nullable: false),
                    location_mid = table.Column<string>(type: "text", nullable: false),
                    location_name = table.Column<string>(type: "text", nullable: false),
                    isactive = table.Column<bool>(type: "boolean", nullable: true),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modifiedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    createdby = table.Column<string>(type: "text", nullable: true),
                    modifiedby = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.location_id);
                });

            migrationBuilder.CreateTable(
                name: "Pipelines",
                columns: table => new
                {
                    pipeline_id = table.Column<string>(type: "text", nullable: false),
                    pipeline_code = table.Column<string>(type: "text", nullable: true),
                    pipeline_name = table.Column<string>(type: "text", nullable: true),
                    project_code = table.Column<string>(type: "text", nullable: true),
                    project_name = table.Column<string>(type: "text", nullable: true),
                    location_id = table.Column<string>(type: "text", nullable: true),
                    expected = table.Column<string>(type: "text", nullable: true),
                    win_probablity = table.Column<string>(type: "text", nullable: true),
                    won_reason = table.Column<string>(type: "text", nullable: true),
                    job_id = table.Column<string>(type: "text", nullable: true),
                    emp_mid = table.Column<string>(type: "text", nullable: true),
                    emp_name = table.Column<string>(type: "text", nullable: true),
                    emp_location_id = table.Column<string>(type: "text", nullable: true),
                    emp_location_name = table.Column<string>(type: "text", nullable: true),
                    creation_date = table.Column<DateOnly>(type: "date", nullable: true),
                    won_date = table.Column<DateOnly>(type: "date", nullable: true),
                    recurring = table.Column<string>(type: "text", nullable: true),
                    sector_id = table.Column<string>(type: "text", nullable: true),
                    pipeline_status = table.Column<string>(type: "text", nullable: true),
                    sme_id = table.Column<string>(type: "text", nullable: true),
                    pipeline_description = table.Column<string>(type: "text", nullable: true),
                    nrccstatus = table.Column<string>(type: "text", nullable: true),
                    finalproposedfee = table.Column<string>(type: "text", nullable: true),
                    finalproposedope = table.Column<string>(type: "text", nullable: true),
                    won_expected_recovery = table.Column<string>(type: "text", nullable: true),
                    contact_name = table.Column<string>(type: "text", nullable: true),
                    client_group_code = table.Column<string>(type: "text", nullable: true),
                    client_id = table.Column<string>(type: "text", nullable: true),
                    service_line_id = table.Column<string>(type: "text", nullable: true),
                    sme_group_id = table.Column<string>(type: "text", nullable: true),
                    revenue_id = table.Column<string>(type: "text", nullable: true),
                    isactive = table.Column<bool>(type: "boolean", nullable: true),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modifiedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    createdby = table.Column<string>(type: "text", nullable: true),
                    modifiedby = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pipelines", x => x.pipeline_id);
                });

            migrationBuilder.CreateTable(
                name: "SectorIndustrys",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    sector_id = table.Column<string>(type: "text", nullable: false),
                    sector_name = table.Column<string>(type: "text", nullable: false),
                    sub_sector_id = table.Column<string>(type: "text", nullable: false),
                    sub_sector_name = table.Column<string>(type: "text", nullable: false),
                    isactive = table.Column<bool>(type: "boolean", nullable: true),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modifiedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    createdby = table.Column<string>(type: "text", nullable: true),
                    modifiedby = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SectorIndustrys", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceLines",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    service_line_id = table.Column<string>(type: "text", nullable: false),
                    service_line = table.Column<string>(type: "text", nullable: false),
                    sme_group_id = table.Column<string>(type: "text", nullable: false),
                    sme_group = table.Column<string>(type: "text", nullable: false),
                    sme_id = table.Column<string>(type: "text", nullable: false),
                    sme = table.Column<string>(type: "text", nullable: false),
                    isactive = table.Column<bool>(type: "boolean", nullable: true),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modifiedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    createdby = table.Column<string>(type: "text", nullable: true),
                    modifiedby = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceLines", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "WCGTDataLogs",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    input_json = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false),
                    entity_type = table.Column<string>(type: "text", nullable: false),
                    error_message = table.Column<string>(type: "text", nullable: false),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modifiedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    createdby = table.Column<string>(type: "text", nullable: true),
                    modifiedby = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WCGTDataLogs", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    pipeline_id = table.Column<string>(type: "text", nullable: true),
                    pipeline_code = table.Column<string>(type: "text", nullable: true),
                    pipeline_name = table.Column<string>(type: "text", nullable: true),
                    project_code = table.Column<string>(type: "text", nullable: true),
                    project_name = table.Column<string>(type: "text", nullable: true),
                    location_id = table.Column<string>(type: "text", nullable: true),
                    expected = table.Column<string>(type: "text", nullable: true),
                    win_probablity = table.Column<string>(type: "text", nullable: true),
                    won_reason = table.Column<string>(type: "text", nullable: true),
                    creation_date = table.Column<DateOnly>(type: "date", nullable: true),
                    won_date = table.Column<DateOnly>(type: "date", nullable: true),
                    recurring = table.Column<string>(type: "text", nullable: true),
                    sector_id = table.Column<string>(type: "text", nullable: true),
                    pipeline_status = table.Column<string>(type: "text", nullable: true),
                    sme_id = table.Column<string>(type: "text", nullable: true),
                    pipeline_description = table.Column<string>(type: "text", nullable: true),
                    nrccstatus = table.Column<string>(type: "text", nullable: true),
                    finalproposedfee = table.Column<string>(type: "text", nullable: true),
                    finalproposedope = table.Column<string>(type: "text", nullable: true),
                    won_expected_recovery = table.Column<string>(type: "text", nullable: true),
                    contact_name = table.Column<string>(type: "text", nullable: true),
                    client_group_code = table.Column<string>(type: "text", nullable: true),
                    client_id = table.Column<string>(type: "text", nullable: true),
                    service_line_id = table.Column<string>(type: "text", nullable: true),
                    sme_group_id = table.Column<string>(type: "text", nullable: true),
                    revenue_id = table.Column<string>(type: "text", nullable: true),
                    isactive = table.Column<bool>(type: "boolean", nullable: true),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modifiedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    createdby = table.Column<string>(type: "text", nullable: true),
                    modifiedby = table.Column<string>(type: "text", nullable: true),
                    ClientLegalEntitypar_aid = table.Column<string>(type: "text", nullable: true)
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
                name: "JobRoles",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    job_id = table.Column<string>(type: "text", nullable: true),
                    user_mid = table.Column<string>(type: "text", nullable: true),
                    user_empname = table.Column<string>(type: "text", nullable: true),
                    user_emailid = table.Column<string>(type: "text", nullable: true),
                    user_role = table.Column<string>(type: "text", nullable: true),
                    isactive = table.Column<bool>(type: "boolean", nullable: true),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modifiedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    createdby = table.Column<string>(type: "text", nullable: true),
                    modifiedby = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobRoles", x => x.id);
                    table.ForeignKey(
                        name: "FK_JobRoles_Jobs_job_id",
                        column: x => x.job_id,
                        principalTable: "Jobs",
                        principalColumn: "job_id");
                });

            migrationBuilder.CreateTable(
                name: "Holidays",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    holiday_name = table.Column<string>(type: "text", nullable: false),
                    holiday_type = table.Column<string>(type: "text", nullable: false),
                    location_id = table.Column<string>(type: "text", nullable: false),
                    location_name = table.Column<string>(type: "text", nullable: false),
                    holiday_date = table.Column<DateOnly>(type: "date", nullable: false),
                    cr_date = table.Column<DateOnly>(type: "date", nullable: false),
                    isactive = table.Column<bool>(type: "boolean", nullable: true),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modifiedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    createdby = table.Column<string>(type: "text", nullable: true),
                    modifiedby = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Holidays", x => x.id);
                    table.ForeignKey(
                        name: "FK_Holidays_Locations_location_id",
                        column: x => x.location_id,
                        principalTable: "Locations",
                        principalColumn: "location_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Leaves",
                columns: table => new
                {
                    leave_id = table.Column<string>(type: "text", nullable: false),
                    location_id = table.Column<string>(type: "text", nullable: false),
                    location_name = table.Column<string>(type: "text", nullable: false),
                    leave_start_date = table.Column<DateOnly>(type: "date", nullable: false),
                    leave_end_date = table.Column<DateOnly>(type: "date", nullable: false),
                    applied_days = table.Column<double>(type: "double precision", nullable: false),
                    revoked_days = table.Column<double>(type: "double precision", nullable: false),
                    revoked_from_date = table.Column<DateOnly>(type: "date", nullable: false),
                    revoked_to_date = table.Column<DateOnly>(type: "date", nullable: false),
                    comp_name = table.Column<string>(type: "text", nullable: false),
                    leave_type_name = table.Column<string>(type: "text", nullable: false),
                    emp_mid = table.Column<string>(type: "text", nullable: false),
                    emp_name = table.Column<string>(type: "text", nullable: false),
                    emp_email = table.Column<string>(type: "text", nullable: false),
                    leave_status_name = table.Column<string>(type: "text", nullable: false),
                    isactive = table.Column<bool>(type: "boolean", nullable: true),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modifiedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    createdby = table.Column<string>(type: "text", nullable: true),
                    modifiedby = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leaves", x => x.leave_id);
                    table.ForeignKey(
                        name: "FK_Leaves_Employees_emp_mid",
                        column: x => x.emp_mid,
                        principalTable: "Employees",
                        principalColumn: "employee_mid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Leaves_Locations_location_id",
                        column: x => x.location_id,
                        principalTable: "Locations",
                        principalColumn: "location_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PipelineRoles",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    pipeline_id = table.Column<string>(type: "text", nullable: true),
                    user_mid = table.Column<string>(type: "text", nullable: true),
                    user_empname = table.Column<string>(type: "text", nullable: true),
                    user_emailid = table.Column<string>(type: "text", nullable: true),
                    user_role = table.Column<string>(type: "text", nullable: true),
                    isactive = table.Column<bool>(type: "boolean", nullable: true),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modifiedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    createdby = table.Column<string>(type: "text", nullable: true),
                    modifiedby = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PipelineRoles", x => x.id);
                    table.ForeignKey(
                        name: "FK_PipelineRoles_Pipelines_pipeline_id",
                        column: x => x.pipeline_id,
                        principalTable: "Pipelines",
                        principalColumn: "pipeline_id");
                });

            migrationBuilder.CreateTable(
                name: "ProjectJobCodes",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    project_id = table.Column<long>(type: "bigint", nullable: true),
                    job_id = table.Column<string>(type: "text", nullable: false),
                    job_code = table.Column<string>(type: "text", nullable: false),
                    pipeline_code = table.Column<string>(type: "text", nullable: false),
                    job_name = table.Column<string>(type: "text", nullable: false),
                    job_description = table.Column<string>(type: "text", nullable: false),
                    job_client = table.Column<string>(type: "text", nullable: false),
                    isactive = table.Column<bool>(type: "boolean", nullable: true),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modifiedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    createdby = table.Column<string>(type: "text", nullable: true),
                    modifiedby = table.Column<string>(type: "text", nullable: true)
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
                    user_email = table.Column<string>(type: "text", nullable: false),
                    user_role = table.Column<string>(type: "text", nullable: false),
                    isactive = table.Column<bool>(type: "boolean", nullable: true),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modifiedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    createdby = table.Column<string>(type: "text", nullable: true),
                    modifiedby = table.Column<string>(type: "text", nullable: true)
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
                name: "IX_Holidays_location_id",
                table: "Holidays",
                column: "location_id");

            migrationBuilder.CreateIndex(
                name: "IX_JobRoles_job_id",
                table: "JobRoles",
                column: "job_id");

            migrationBuilder.CreateIndex(
                name: "IX_Leaves_emp_mid",
                table: "Leaves",
                column: "emp_mid");

            migrationBuilder.CreateIndex(
                name: "IX_Leaves_location_id",
                table: "Leaves",
                column: "location_id");

            migrationBuilder.CreateIndex(
                name: "IX_PipelineRoles_pipeline_id",
                table: "PipelineRoles",
                column: "pipeline_id");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BUTreeMappings");

            migrationBuilder.DropTable(
                name: "Designations");

            migrationBuilder.DropTable(
                name: "Holidays");

            migrationBuilder.DropTable(
                name: "JobRoles");

            migrationBuilder.DropTable(
                name: "Leaves");

            migrationBuilder.DropTable(
                name: "PipelineRoles");

            migrationBuilder.DropTable(
                name: "ProjectJobCodes");

            migrationBuilder.DropTable(
                name: "ProjectRoles");

            migrationBuilder.DropTable(
                name: "SectorIndustrys");

            migrationBuilder.DropTable(
                name: "ServiceLines");

            migrationBuilder.DropTable(
                name: "WCGTDataLogs");

            migrationBuilder.DropTable(
                name: "Jobs");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "Pipelines");

            migrationBuilder.DropTable(
                name: "Projects");

            migrationBuilder.DropTable(
                name: "ClientLegalEntitys");

            migrationBuilder.DropTable(
                name: "Clients");
        }
    }
}
