using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Projects.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatedprojectsdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 101L,
                columns: new[] { "CreatedDate", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 8, 14, 8, 52, 24, 759, DateTimeKind.Utc).AddTicks(4782), new DateTime(2023, 8, 24, 8, 52, 24, 759, DateTimeKind.Utc).AddTicks(4777), new DateTime(2023, 8, 14, 8, 52, 24, 759, DateTimeKind.Utc).AddTicks(4770) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 102L,
                columns: new[] { "CreatedDate", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 8, 14, 8, 52, 24, 759, DateTimeKind.Utc).AddTicks(4789), new DateTime(2023, 8, 24, 8, 52, 24, 759, DateTimeKind.Utc).AddTicks(4786), new DateTime(2023, 8, 14, 8, 52, 24, 759, DateTimeKind.Utc).AddTicks(4786) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 103L,
                columns: new[] { "CreatedDate", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 8, 14, 8, 52, 24, 759, DateTimeKind.Utc).AddTicks(4794), new DateTime(2023, 8, 24, 8, 52, 24, 759, DateTimeKind.Utc).AddTicks(4792), new DateTime(2023, 8, 14, 8, 52, 24, 759, DateTimeKind.Utc).AddTicks(4791) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 104L,
                columns: new[] { "CreatedDate", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 8, 14, 8, 52, 24, 759, DateTimeKind.Utc).AddTicks(4798), new DateTime(2023, 8, 24, 8, 52, 24, 759, DateTimeKind.Utc).AddTicks(4797), new DateTime(2023, 8, 14, 8, 52, 24, 759, DateTimeKind.Utc).AddTicks(4796) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 101L,
                columns: new[] { "CreatedDate", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 8, 9, 8, 45, 39, 297, DateTimeKind.Utc).AddTicks(6432), new DateTime(2023, 8, 24, 8, 45, 39, 297, DateTimeKind.Utc).AddTicks(6427), new DateTime(2023, 8, 14, 8, 45, 39, 297, DateTimeKind.Utc).AddTicks(6421) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 102L,
                columns: new[] { "CreatedDate", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 8, 9, 8, 45, 39, 297, DateTimeKind.Utc).AddTicks(6438), new DateTime(2023, 8, 24, 8, 45, 39, 297, DateTimeKind.Utc).AddTicks(6435), new DateTime(2023, 8, 14, 8, 45, 39, 297, DateTimeKind.Utc).AddTicks(6435) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 103L,
                columns: new[] { "CreatedDate", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 8, 9, 8, 45, 39, 297, DateTimeKind.Utc).AddTicks(6442), new DateTime(2023, 8, 24, 8, 45, 39, 297, DateTimeKind.Utc).AddTicks(6441), new DateTime(2023, 8, 14, 8, 45, 39, 297, DateTimeKind.Utc).AddTicks(6441) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 104L,
                columns: new[] { "CreatedDate", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 8, 9, 8, 45, 39, 297, DateTimeKind.Utc).AddTicks(6448), new DateTime(2023, 8, 24, 8, 45, 39, 297, DateTimeKind.Utc).AddTicks(6446), new DateTime(2023, 8, 14, 8, 45, 39, 297, DateTimeKind.Utc).AddTicks(6445) });
        }
    }
}
