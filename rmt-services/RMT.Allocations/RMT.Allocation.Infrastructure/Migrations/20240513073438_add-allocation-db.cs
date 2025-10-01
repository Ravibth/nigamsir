using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RMT.Allocation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addallocationdb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "RequisitionDemand",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TotalDemands = table.Column<long>(type: "bigint", nullable: false),
                    PendingDemands = table.Column<long>(type: "bigint", nullable: false),
                    AllResourcesHaveSameDetails = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequisitionDemand", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RequisitionType",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequisitionType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Requisition",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RequisitionDemand = table.Column<Guid>(type: "uuid", nullable: false),
                    PipelineCode = table.Column<string>(type: "text", nullable: true),
                    JobCode = table.Column<string>(type: "text", nullable: true),
                    PipelineName = table.Column<string>(type: "text", nullable: true),
                    JobName = table.Column<string>(type: "text", nullable: true),
                    ClientName = table.Column<string>(type: "text", nullable: true),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EffortsPerDay = table.Column<long>(type: "bigint", nullable: false),
                    TotalHours = table.Column<long>(type: "bigint", nullable: false),
                    RequisitionStatus = table.Column<string>(type: "text", nullable: false),
                    Expertise = table.Column<string>(type: "text", nullable: false),
                    Designation = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    IsPerDayHourAllocation = table.Column<bool>(type: "boolean", nullable: false),
                    BusinessUnit = table.Column<string>(type: "text", nullable: false),
                    SMEG = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    ModifiedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    RequisitionTypeId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Requisition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Requisition_RequisitionDemand_RequisitionDemand",
                        column: x => x.RequisitionDemand,
                        principalTable: "RequisitionDemand",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Requisition_RequisitionType_RequisitionTypeId",
                        column: x => x.RequisitionTypeId,
                        principalTable: "RequisitionType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PublishedResAllocDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RequisitionId = table.Column<Guid>(type: "uuid", nullable: false),
                    EmpEmail = table.Column<string>(type: "text", nullable: false),
                    EmpName = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    TotalEffort = table.Column<long>(type: "bigint", nullable: false),
                    AllocationStatus = table.Column<string>(type: "text", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    ConfirmedAllocationDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    ModifiedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    AllocationVersion = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublishedResAllocDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PublishedResAllocDetails_Requisition_RequisitionId",
                        column: x => x.RequisitionId,
                        principalTable: "Requisition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequisitionParameters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RequisitionId = table.Column<Guid>(type: "uuid", nullable: false),
                    Category = table.Column<string>(type: "text", nullable: false),
                    RequisitionWeight = table.Column<long>(type: "bigint", nullable: false),
                    IsChecked = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequisitionParameters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequisitionParameters_Requisition_RequisitionId",
                        column: x => x.RequisitionId,
                        principalTable: "Requisition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequisitionParameterValues",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RequisitionId = table.Column<Guid>(type: "uuid", nullable: false),
                    Parameter = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequisitionParameterValues", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequisitionParameterValues_Requisition_RequisitionId",
                        column: x => x.RequisitionId,
                        principalTable: "Requisition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RequisitionSkill",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RequisitionId = table.Column<Guid>(type: "uuid", nullable: false),
                    SkillName = table.Column<string>(type: "text", nullable: false),
                    SkillCode = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequisitionSkill", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequisitionSkill_Requisition_RequisitionId",
                        column: x => x.RequisitionId,
                        principalTable: "Requisition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PublishedResAlloc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RequisitionId = table.Column<Guid>(type: "uuid", nullable: false),
                    PublishedResAllocDetailsId = table.Column<Guid>(type: "uuid", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Efforts = table.Column<long>(type: "bigint", nullable: false),
                    IsPerDayAllocation = table.Column<bool>(type: "boolean", nullable: false),
                    RatePerHour = table.Column<double>(type: "double precision", nullable: false),
                    TotalWorkingDays = table.Column<long>(type: "bigint", nullable: false),
                    Currency = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublishedResAlloc", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PublishedResAlloc_PublishedResAllocDetails_PublishedResAllo~",
                        column: x => x.PublishedResAllocDetailsId,
                        principalTable: "PublishedResAllocDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PublishedResAlloc_Requisition_RequisitionId",
                        column: x => x.RequisitionId,
                        principalTable: "Requisition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PublishedResAllocSkillEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RequisitionId = table.Column<Guid>(type: "uuid", nullable: false),
                    PublishedResAllocDetailsId = table.Column<Guid>(type: "uuid", nullable: false),
                    SkillName = table.Column<string>(type: "text", nullable: false),
                    SkillCode = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublishedResAllocSkillEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PublishedResAllocSkillEntity_PublishedResAllocDetails_Publi~",
                        column: x => x.PublishedResAllocDetailsId,
                        principalTable: "PublishedResAllocDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PublishedResAllocSkillEntity_Requisition_RequisitionId",
                        column: x => x.RequisitionId,
                        principalTable: "Requisition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UnPublishedResAllocDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ParentPublishedResAllocDetailsId = table.Column<Guid>(type: "uuid", nullable: true),
                    RequisitionId = table.Column<Guid>(type: "uuid", nullable: false),
                    EmpEmail = table.Column<string>(type: "text", nullable: false),
                    EmpName = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    TotalEffort = table.Column<long>(type: "bigint", nullable: false),
                    AllocationStatus = table.Column<string>(type: "text", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    ModifiedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    AllocationVersion = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnPublishedResAllocDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnPublishedResAllocDetails_PublishedResAllocDetails_ParentP~",
                        column: x => x.ParentPublishedResAllocDetailsId,
                        principalTable: "PublishedResAllocDetails",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UnPublishedResAllocDetails_Requisition_RequisitionId",
                        column: x => x.RequisitionId,
                        principalTable: "Requisition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PublishedResAllocDays",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RequisitionId = table.Column<Guid>(type: "uuid", nullable: false),
                    PublishedResAllocId = table.Column<Guid>(type: "uuid", nullable: false),
                    Efforts = table.Column<long>(type: "bigint", nullable: false),
                    EmailId = table.Column<string>(type: "text", nullable: false),
                    PipelineCode = table.Column<string>(type: "text", nullable: false),
                    JobCode = table.Column<string>(type: "text", nullable: true),
                    RatePerHour = table.Column<double>(type: "double precision", nullable: false),
                    Currency = table.Column<string>(type: "text", nullable: true),
                    AllocationDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublishedResAllocDays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PublishedResAllocDays_PublishedResAlloc_PublishedResAllocId",
                        column: x => x.PublishedResAllocId,
                        principalTable: "PublishedResAlloc",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PublishedResAllocDays_Requisition_RequisitionId",
                        column: x => x.RequisitionId,
                        principalTable: "Requisition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UnPublishedResAlloc",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RequisitionId = table.Column<Guid>(type: "uuid", nullable: false),
                    UnPublishedResAllocDetailsId = table.Column<Guid>(type: "uuid", nullable: false),
                    StartDate = table.Column<DateOnly>(type: "date", nullable: false),
                    EndDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Efforts = table.Column<long>(type: "bigint", nullable: false),
                    IsPerDayAllocation = table.Column<bool>(type: "boolean", nullable: false),
                    RatePerHour = table.Column<double>(type: "double precision", nullable: false),
                    TotalWorkingDays = table.Column<long>(type: "bigint", nullable: false),
                    Currency = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnPublishedResAlloc", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnPublishedResAlloc_Requisition_RequisitionId",
                        column: x => x.RequisitionId,
                        principalTable: "Requisition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UnPublishedResAlloc_UnPublishedResAllocDetails_UnPublishedR~",
                        column: x => x.UnPublishedResAllocDetailsId,
                        principalTable: "UnPublishedResAllocDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UnPublishedResAllocSkillEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RequisitionId = table.Column<Guid>(type: "uuid", nullable: false),
                    UnPublishedResAllocDetailsId = table.Column<Guid>(type: "uuid", nullable: false),
                    SkillName = table.Column<string>(type: "text", nullable: false),
                    SkillCode = table.Column<string>(type: "text", nullable: false),
                    UnPublishedResAllocSkillEntityId = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnPublishedResAllocSkillEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnPublishedResAllocSkillEntity_Requisition_RequisitionId",
                        column: x => x.RequisitionId,
                        principalTable: "Requisition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UnPublishedResAllocSkillEntity_UnPublishedResAllocDetails_U~",
                        column: x => x.UnPublishedResAllocDetailsId,
                        principalTable: "UnPublishedResAllocDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UnPublishedResAllocSkillEntity_UnPublishedResAllocSkillEnti~",
                        column: x => x.UnPublishedResAllocSkillEntityId,
                        principalTable: "UnPublishedResAllocSkillEntity",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UnPublishedResAllocDays",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RequisitionId = table.Column<Guid>(type: "uuid", nullable: false),
                    UnPublishedResAllocId = table.Column<Guid>(type: "uuid", nullable: false),
                    Efforts = table.Column<long>(type: "bigint", nullable: false),
                    EmailId = table.Column<string>(type: "text", nullable: false),
                    PipelineCode = table.Column<string>(type: "text", nullable: false),
                    JobCode = table.Column<string>(type: "text", nullable: true),
                    RatePerHour = table.Column<double>(type: "double precision", nullable: false),
                    Currency = table.Column<string>(type: "text", nullable: true),
                    AllocationDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnPublishedResAllocDays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UnPublishedResAllocDays_Requisition_RequisitionId",
                        column: x => x.RequisitionId,
                        principalTable: "Requisition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UnPublishedResAllocDays_UnPublishedResAlloc_UnPublishedResA~",
                        column: x => x.UnPublishedResAllocId,
                        principalTable: "UnPublishedResAlloc",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "RequisitionType",
                columns: new[] { "Id", "Type" },
                values: new object[,]
                {
                    { 1L, "Named Allocation" },
                    { 2L, "Same Team Allocation" },
                    { 3L, "Create Requisition" },
                    { 4L, "Roll Forward Allocation" },
                    { 5L, "Bulk Allocation" },
                    { 6L, "Bulk Requisition" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_PublishedResAlloc_PublishedResAllocDetailsId",
                table: "PublishedResAlloc",
                column: "PublishedResAllocDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_PublishedResAlloc_RequisitionId",
                table: "PublishedResAlloc",
                column: "RequisitionId");

            migrationBuilder.CreateIndex(
                name: "IX_PublishedResAllocDays_PublishedResAllocId",
                table: "PublishedResAllocDays",
                column: "PublishedResAllocId");

            migrationBuilder.CreateIndex(
                name: "IX_PublishedResAllocDays_RequisitionId",
                table: "PublishedResAllocDays",
                column: "RequisitionId");

            migrationBuilder.CreateIndex(
                name: "IX_PublishedResAllocDetails_RequisitionId",
                table: "PublishedResAllocDetails",
                column: "RequisitionId");

            migrationBuilder.CreateIndex(
                name: "IX_PublishedResAllocSkillEntity_PublishedResAllocDetailsId",
                table: "PublishedResAllocSkillEntity",
                column: "PublishedResAllocDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_PublishedResAllocSkillEntity_RequisitionId",
                table: "PublishedResAllocSkillEntity",
                column: "RequisitionId");

            migrationBuilder.CreateIndex(
                name: "IX_Requisition_RequisitionDemand",
                table: "Requisition",
                column: "RequisitionDemand");

            migrationBuilder.CreateIndex(
                name: "IX_Requisition_RequisitionTypeId",
                table: "Requisition",
                column: "RequisitionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_RequisitionParameters_RequisitionId",
                table: "RequisitionParameters",
                column: "RequisitionId");

            migrationBuilder.CreateIndex(
                name: "IX_RequisitionParameterValues_RequisitionId",
                table: "RequisitionParameterValues",
                column: "RequisitionId");

            migrationBuilder.CreateIndex(
                name: "IX_RequisitionSkill_RequisitionId",
                table: "RequisitionSkill",
                column: "RequisitionId");

            migrationBuilder.CreateIndex(
                name: "IX_UnPublishedResAlloc_RequisitionId",
                table: "UnPublishedResAlloc",
                column: "RequisitionId");

            migrationBuilder.CreateIndex(
                name: "IX_UnPublishedResAlloc_UnPublishedResAllocDetailsId",
                table: "UnPublishedResAlloc",
                column: "UnPublishedResAllocDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_UnPublishedResAllocDays_RequisitionId",
                table: "UnPublishedResAllocDays",
                column: "RequisitionId");

            migrationBuilder.CreateIndex(
                name: "IX_UnPublishedResAllocDays_UnPublishedResAllocId",
                table: "UnPublishedResAllocDays",
                column: "UnPublishedResAllocId");

            migrationBuilder.CreateIndex(
                name: "IX_UnPublishedResAllocDetails_ParentPublishedResAllocDetailsId",
                table: "UnPublishedResAllocDetails",
                column: "ParentPublishedResAllocDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_UnPublishedResAllocDetails_RequisitionId",
                table: "UnPublishedResAllocDetails",
                column: "RequisitionId");

            migrationBuilder.CreateIndex(
                name: "IX_UnPublishedResAllocSkillEntity_RequisitionId",
                table: "UnPublishedResAllocSkillEntity",
                column: "RequisitionId");

            migrationBuilder.CreateIndex(
                name: "IX_UnPublishedResAllocSkillEntity_UnPublishedResAllocDetailsId",
                table: "UnPublishedResAllocSkillEntity",
                column: "UnPublishedResAllocDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_UnPublishedResAllocSkillEntity_UnPublishedResAllocSkillEnti~",
                table: "UnPublishedResAllocSkillEntity",
                column: "UnPublishedResAllocSkillEntityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PublishedResAllocDays");

            migrationBuilder.DropTable(
                name: "PublishedResAllocSkillEntity");

            migrationBuilder.DropTable(
                name: "RequisitionParameters");

            migrationBuilder.DropTable(
                name: "RequisitionParameterValues");

            migrationBuilder.DropTable(
                name: "RequisitionSkill");

            migrationBuilder.DropTable(
                name: "UnPublishedResAllocDays");

            migrationBuilder.DropTable(
                name: "UnPublishedResAllocSkillEntity");

            migrationBuilder.DropTable(
                name: "PublishedResAlloc");

            migrationBuilder.DropTable(
                name: "UnPublishedResAlloc");

            migrationBuilder.DropTable(
                name: "UnPublishedResAllocDetails");

            migrationBuilder.DropTable(
                name: "PublishedResAllocDetails");

            migrationBuilder.DropTable(
                name: "Requisition");

            migrationBuilder.DropTable(
                name: "RequisitionDemand");

            migrationBuilder.DropTable(
                name: "RequisitionType");
        }
    }
}
