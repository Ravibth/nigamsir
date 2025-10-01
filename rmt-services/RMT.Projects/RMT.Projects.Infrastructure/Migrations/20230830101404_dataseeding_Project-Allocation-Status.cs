using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Projects.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class dataseeding_ProjectAllocationStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 101L,
                columns: new[] { "CreatedDate", "EndDate", "ProjectAllocationStatus", "StartDate" },
                values: new object[] { new DateTime(2023, 9, 4, 10, 14, 4, 443, DateTimeKind.Utc).AddTicks(8574), new DateTime(2023, 9, 14, 10, 14, 4, 443, DateTimeKind.Utc).AddTicks(8548), "ALLOCATION_COMPLETED", new DateTime(2023, 9, 4, 10, 14, 4, 443, DateTimeKind.Utc).AddTicks(8541) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 102L,
                columns: new[] { "CreatedDate", "EndDate", "ProjectAllocationStatus", "StartDate" },
                values: new object[] { new DateTime(2023, 9, 4, 10, 14, 4, 443, DateTimeKind.Utc).AddTicks(8584), new DateTime(2023, 9, 14, 10, 14, 4, 443, DateTimeKind.Utc).AddTicks(8579), "ALLOCATION_COMPLETED", new DateTime(2023, 9, 4, 10, 14, 4, 443, DateTimeKind.Utc).AddTicks(8578) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 103L,
                columns: new[] { "CreatedDate", "EndDate", "ProjectAllocationStatus", "StartDate" },
                values: new object[] { new DateTime(2023, 9, 4, 10, 14, 4, 443, DateTimeKind.Utc).AddTicks(8592), new DateTime(2023, 9, 14, 10, 14, 4, 443, DateTimeKind.Utc).AddTicks(8589), "ALLOCATION_COMPLETED", new DateTime(2023, 9, 4, 10, 14, 4, 443, DateTimeKind.Utc).AddTicks(8588) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 104L,
                columns: new[] { "CreatedDate", "EndDate", "ProjectAllocationStatus", "StartDate" },
                values: new object[] { new DateTime(2023, 8, 30, 10, 14, 4, 443, DateTimeKind.Utc).AddTicks(8597), new DateTime(2023, 9, 14, 10, 14, 4, 443, DateTimeKind.Utc).AddTicks(8594), "ALLOCATION_COMPLETED", new DateTime(2023, 9, 4, 10, 14, 4, 443, DateTimeKind.Utc).AddTicks(8593) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 101L,
                columns: new[] { "CreatedDate", "EndDate", "ProjectAllocationStatus", "StartDate" },
                values: new object[] { new DateTime(2023, 8, 27, 7, 29, 56, 355, DateTimeKind.Utc).AddTicks(393), new DateTime(2023, 9, 6, 7, 29, 56, 355, DateTimeKind.Utc).AddTicks(385), "PC101", new DateTime(2023, 8, 27, 7, 29, 56, 355, DateTimeKind.Utc).AddTicks(379) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 102L,
                columns: new[] { "CreatedDate", "EndDate", "ProjectAllocationStatus", "StartDate" },
                values: new object[] { new DateTime(2023, 8, 27, 7, 29, 56, 355, DateTimeKind.Utc).AddTicks(400), new DateTime(2023, 9, 6, 7, 29, 56, 355, DateTimeKind.Utc).AddTicks(396), "PC102", new DateTime(2023, 8, 27, 7, 29, 56, 355, DateTimeKind.Utc).AddTicks(396) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 103L,
                columns: new[] { "CreatedDate", "EndDate", "ProjectAllocationStatus", "StartDate" },
                values: new object[] { new DateTime(2023, 8, 27, 7, 29, 56, 355, DateTimeKind.Utc).AddTicks(404), new DateTime(2023, 9, 6, 7, 29, 56, 355, DateTimeKind.Utc).AddTicks(402), "PC103", new DateTime(2023, 8, 27, 7, 29, 56, 355, DateTimeKind.Utc).AddTicks(402) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 104L,
                columns: new[] { "CreatedDate", "EndDate", "ProjectAllocationStatus", "StartDate" },
                values: new object[] { new DateTime(2023, 8, 22, 7, 29, 56, 355, DateTimeKind.Utc).AddTicks(408), new DateTime(2023, 9, 6, 7, 29, 56, 355, DateTimeKind.Utc).AddTicks(406), "PC104", new DateTime(2023, 8, 27, 7, 29, 56, 355, DateTimeKind.Utc).AddTicks(406) });
        }
    }
}
