using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace RMT.Employee.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EmployeeProjectMappingv1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeProjectMapping",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    EmpMID = table.Column<string>(type: "text", nullable: false),
                    Offering = table.Column<string>(type: "text", nullable: false),
                    OfferingId = table.Column<string>(type: "text", nullable: true),
                    Solution = table.Column<string>(type: "text", nullable: false),
                    SolutionId = table.Column<string>(type: "text", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    ModifiedBy = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeProjectMapping", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 100L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 101L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 102L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 103L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 104L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 105L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 106L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 107L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 108L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 109L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 110L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 111L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 112L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 113L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 114L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 115L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 116L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 117L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 118L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 119L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 120L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 121L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 122L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 123L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 124L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 125L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 126L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 127L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 128L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 129L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 130L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 131L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 132L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 133L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 134L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 135L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 136L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 137L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 138L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 139L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 140L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc), new DateTime(2024, 1, 1, 1, 0, 0, 0, DateTimeKind.Utc) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeProjectMapping");

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 100L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3424), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3430) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 101L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3437), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3437) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 102L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3447), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3447) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 103L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3449), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3450) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 104L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3452), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3452) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 105L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3454), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3454) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 106L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3457), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3457) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 107L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3459), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3459) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 108L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3461), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3461) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 109L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3463), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3464) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 110L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3465), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3466) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 111L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3470), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3470) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 112L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3472), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3473) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 113L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3474), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3475) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 114L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3477), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3477) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 115L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3480), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3480) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 116L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3482), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3483) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 117L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3484), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3485) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 118L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3487), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3487) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 119L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3489), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3489) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 120L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3491), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3491) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 121L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3493), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3494) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 122L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3496), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3496) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 123L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3498), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3499) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 124L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3501), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3502) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 125L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3552), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3552) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 126L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3555), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3555) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 127L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3557), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3558) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 128L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3559), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3560) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 129L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3562), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3562) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 130L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3564), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3564) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 131L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3566), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3567) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 132L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3569), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3569) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 133L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3571), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3571) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 134L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3573), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3574) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 135L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3576), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3576) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 136L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3578), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3578) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 137L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3580), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3581) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 138L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3583), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3583) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 139L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3586), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3586) });

            migrationBuilder.UpdateData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 140L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3588), new DateTime(2024, 9, 4, 5, 13, 28, 258, DateTimeKind.Utc).AddTicks(3588) });
        }
    }
}
