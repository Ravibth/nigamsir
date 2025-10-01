using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Projects.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class budgetjobfee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "JobFee",
                table: "ProjectBudget",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JobFee",
                table: "ProjectBudget");

            migrationBuilder.UpdateData(
                table: "FieldForMarketPlaces",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 1, 16, 11, 22, 27, 913, DateTimeKind.Utc).AddTicks(6107), new DateTime(2024, 1, 16, 11, 22, 27, 913, DateTimeKind.Utc).AddTicks(6111) });

            migrationBuilder.UpdateData(
                table: "FieldForMarketPlaces",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 1, 16, 11, 22, 27, 913, DateTimeKind.Utc).AddTicks(6165), new DateTime(2024, 1, 16, 11, 22, 27, 913, DateTimeKind.Utc).AddTicks(6166) });

            migrationBuilder.UpdateData(
                table: "FieldForMarketPlaces",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 1, 16, 11, 22, 27, 913, DateTimeKind.Utc).AddTicks(6168), new DateTime(2024, 1, 16, 11, 22, 27, 913, DateTimeKind.Utc).AddTicks(6168) });

            migrationBuilder.UpdateData(
                table: "FieldForMarketPlaces",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 1, 16, 11, 22, 27, 913, DateTimeKind.Utc).AddTicks(6169), new DateTime(2024, 1, 16, 11, 22, 27, 913, DateTimeKind.Utc).AddTicks(6170) });
        }
    }
}
