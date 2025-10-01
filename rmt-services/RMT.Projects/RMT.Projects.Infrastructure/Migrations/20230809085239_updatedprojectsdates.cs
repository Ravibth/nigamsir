using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Projects.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatedprojectsdates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 101L,
                columns: new[] { "CreatedDate", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 8, 14, 8, 52, 39, 439, DateTimeKind.Utc).AddTicks(2373), new DateTime(2023, 8, 24, 8, 52, 39, 439, DateTimeKind.Utc).AddTicks(2369), new DateTime(2023, 8, 14, 8, 52, 39, 439, DateTimeKind.Utc).AddTicks(2364) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 102L,
                columns: new[] { "CreatedDate", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 8, 14, 8, 52, 39, 439, DateTimeKind.Utc).AddTicks(2379), new DateTime(2023, 8, 24, 8, 52, 39, 439, DateTimeKind.Utc).AddTicks(2377), new DateTime(2023, 8, 14, 8, 52, 39, 439, DateTimeKind.Utc).AddTicks(2377) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 103L,
                columns: new[] { "CreatedDate", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 8, 14, 8, 52, 39, 439, DateTimeKind.Utc).AddTicks(2383), new DateTime(2023, 8, 24, 8, 52, 39, 439, DateTimeKind.Utc).AddTicks(2382), new DateTime(2023, 8, 14, 8, 52, 39, 439, DateTimeKind.Utc).AddTicks(2381) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 104L,
                columns: new[] { "CreatedDate", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 8, 14, 8, 52, 39, 439, DateTimeKind.Utc).AddTicks(2387), new DateTime(2023, 8, 24, 8, 52, 39, 439, DateTimeKind.Utc).AddTicks(2385), new DateTime(2023, 8, 14, 8, 52, 39, 439, DateTimeKind.Utc).AddTicks(2385) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
