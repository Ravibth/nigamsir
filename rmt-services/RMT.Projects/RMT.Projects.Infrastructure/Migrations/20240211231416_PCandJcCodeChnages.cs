using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace RMT.Projects.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PCandJcCodeChnages : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectJobCodes");

            migrationBuilder.RenameColumn(
                name: "ProjectCode",
                table: "PublishedFieldForMarketPlaces",
                newName: "PipelineCode");

            migrationBuilder.RenameColumn(
                name: "ProjectName",
                table: "Projects",
                newName: "JobName");

            migrationBuilder.RenameColumn(
                name: "ProjectCode",
                table: "Projects",
                newName: "JobCode");

            migrationBuilder.RenameColumn(
                name: "ProjectCode",
                table: "ProjectRequisitionAllocation",
                newName: "PipelineCode");

            migrationBuilder.AddColumn<string>(
                name: "JobCode",
                table: "PublishedFieldForMarketPlaces",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "JobCode",
                table: "ProjectRequisitionAllocation",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PipelineCode",
                table: "ProjectBudget",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "FieldForMarketPlaces",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 11, 23, 14, 16, 120, DateTimeKind.Utc).AddTicks(2036), new DateTime(2024, 2, 11, 23, 14, 16, 120, DateTimeKind.Utc).AddTicks(2041) });

            migrationBuilder.UpdateData(
                table: "FieldForMarketPlaces",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 11, 23, 14, 16, 120, DateTimeKind.Utc).AddTicks(2043), new DateTime(2024, 2, 11, 23, 14, 16, 120, DateTimeKind.Utc).AddTicks(2044) });

            migrationBuilder.UpdateData(
                table: "FieldForMarketPlaces",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 11, 23, 14, 16, 120, DateTimeKind.Utc).AddTicks(2046), new DateTime(2024, 2, 11, 23, 14, 16, 120, DateTimeKind.Utc).AddTicks(2046) });

            migrationBuilder.UpdateData(
                table: "FieldForMarketPlaces",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 11, 23, 14, 16, 120, DateTimeKind.Utc).AddTicks(2048), new DateTime(2024, 2, 11, 23, 14, 16, 120, DateTimeKind.Utc).AddTicks(2048) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JobCode",
                table: "PublishedFieldForMarketPlaces");

            migrationBuilder.DropColumn(
                name: "JobCode",
                table: "ProjectRequisitionAllocation");

            migrationBuilder.DropColumn(
                name: "PipelineCode",
                table: "ProjectBudget");

            migrationBuilder.RenameColumn(
                name: "PipelineCode",
                table: "PublishedFieldForMarketPlaces",
                newName: "ProjectCode");

            migrationBuilder.RenameColumn(
                name: "JobName",
                table: "Projects",
                newName: "ProjectName");

            migrationBuilder.RenameColumn(
                name: "JobCode",
                table: "Projects",
                newName: "ProjectCode");

            migrationBuilder.RenameColumn(
                name: "PipelineCode",
                table: "ProjectRequisitionAllocation",
                newName: "ProjectCode");

            migrationBuilder.CreateTable(
                name: "ProjectJobCodes",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProjectId = table.Column<long>(type: "bigint", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: true),
                    JobCode = table.Column<string>(type: "text", nullable: true),
                    JobName = table.Column<string>(type: "text", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<string>(type: "text", nullable: true),
                    PipelineCode = table.Column<string>(type: "text", nullable: true),
                    ProjectCode = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectJobCodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProjectJobCodes_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "FieldForMarketPlaces",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 6, 5, 58, 30, 481, DateTimeKind.Utc).AddTicks(4256), new DateTime(2024, 2, 6, 5, 58, 30, 481, DateTimeKind.Utc).AddTicks(4260) });

            migrationBuilder.UpdateData(
                table: "FieldForMarketPlaces",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 6, 5, 58, 30, 481, DateTimeKind.Utc).AddTicks(4262), new DateTime(2024, 2, 6, 5, 58, 30, 481, DateTimeKind.Utc).AddTicks(4263) });

            migrationBuilder.UpdateData(
                table: "FieldForMarketPlaces",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 6, 5, 58, 30, 481, DateTimeKind.Utc).AddTicks(4264), new DateTime(2024, 2, 6, 5, 58, 30, 481, DateTimeKind.Utc).AddTicks(4265) });

            migrationBuilder.UpdateData(
                table: "FieldForMarketPlaces",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 6, 5, 58, 30, 481, DateTimeKind.Utc).AddTicks(4266), new DateTime(2024, 2, 6, 5, 58, 30, 481, DateTimeKind.Utc).AddTicks(4267) });

            migrationBuilder.CreateIndex(
                name: "IX_ProjectJobCodes_ProjectId",
                table: "ProjectJobCodes",
                column: "ProjectId");
        }
    }
}
