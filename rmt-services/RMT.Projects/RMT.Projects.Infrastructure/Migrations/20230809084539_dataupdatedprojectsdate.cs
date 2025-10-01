using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Projects.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class dataupdatedprojectsdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Projects",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Subindustry",
                table: "Projects",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 101L,
                columns: new[] { "CreatedDate", "EndDate", "StartDate", "Subindustry" },
                values: new object[] { new DateTime(2023, 8, 9, 8, 45, 39, 297, DateTimeKind.Utc).AddTicks(6432), new DateTime(2023, 8, 24, 8, 45, 39, 297, DateTimeKind.Utc).AddTicks(6427), new DateTime(2023, 8, 14, 8, 45, 39, 297, DateTimeKind.Utc).AddTicks(6421), "Food distributors" });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 102L,
                columns: new[] { "CreatedDate", "EndDate", "StartDate", "Subindustry" },
                values: new object[] { new DateTime(2023, 8, 9, 8, 45, 39, 297, DateTimeKind.Utc).AddTicks(6438), new DateTime(2023, 8, 24, 8, 45, 39, 297, DateTimeKind.Utc).AddTicks(6435), new DateTime(2023, 8, 14, 8, 45, 39, 297, DateTimeKind.Utc).AddTicks(6435), "Airlines and miscellaneous non-rail transportation" });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 103L,
                columns: new[] { "CreatedDate", "EndDate", "StartDate", "Subindustry" },
                values: new object[] { new DateTime(2023, 8, 9, 8, 45, 39, 297, DateTimeKind.Utc).AddTicks(6442), new DateTime(2023, 8, 24, 8, 45, 39, 297, DateTimeKind.Utc).AddTicks(6441), new DateTime(2023, 8, 14, 8, 45, 39, 297, DateTimeKind.Utc).AddTicks(6441), "Tyres & rubber manufacturers" });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 104L,
                columns: new[] { "CreatedDate", "EndDate", "StartDate", "Subindustry" },
                values: new object[] { new DateTime(2023, 8, 9, 8, 45, 39, 297, DateTimeKind.Utc).AddTicks(6448), new DateTime(2023, 8, 24, 8, 45, 39, 297, DateTimeKind.Utc).AddTicks(6446), new DateTime(2023, 8, 14, 8, 45, 39, 297, DateTimeKind.Utc).AddTicks(6445), "Tyres & rubber manufacturers" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Subindustry",
                table: "Projects");

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 101L,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 8, 24, 8, 27, 30, 645, DateTimeKind.Utc).AddTicks(4117), new DateTime(2023, 8, 14, 8, 27, 30, 645, DateTimeKind.Utc).AddTicks(4111) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 102L,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 8, 24, 8, 27, 30, 645, DateTimeKind.Utc).AddTicks(4124), new DateTime(2023, 8, 14, 8, 27, 30, 645, DateTimeKind.Utc).AddTicks(4124) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 103L,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 8, 24, 8, 27, 30, 645, DateTimeKind.Utc).AddTicks(4129), new DateTime(2023, 8, 14, 8, 27, 30, 645, DateTimeKind.Utc).AddTicks(4128) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 104L,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 8, 24, 8, 27, 30, 645, DateTimeKind.Utc).AddTicks(4134), new DateTime(2023, 8, 14, 8, 27, 30, 645, DateTimeKind.Utc).AddTicks(4133) });
        }
    }
}
