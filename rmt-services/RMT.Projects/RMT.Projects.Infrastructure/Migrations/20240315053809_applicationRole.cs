using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Projects.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class applicationRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.UpdateData(
                table: "FieldForMarketPlaces",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 22, 15, 19, 45, 866, DateTimeKind.Utc), new DateTime(2024, 2, 22, 15, 19, 45, 866, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "FieldForMarketPlaces",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 22, 15, 19, 45, 866, DateTimeKind.Utc), new DateTime(2024, 2, 22, 15, 19, 45, 866, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "FieldForMarketPlaces",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 22, 15, 19, 45, 866, DateTimeKind.Utc), new DateTime(2024, 2, 22, 15, 19, 45, 866, DateTimeKind.Utc) });

            migrationBuilder.UpdateData(
                table: "FieldForMarketPlaces",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 22, 15, 19, 45, 866, DateTimeKind.Utc), new DateTime(2024, 2, 22, 15, 19, 45, 866, DateTimeKind.Utc) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.UpdateData(
                table: "FieldForMarketPlaces",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 22, 15, 19, 45, 866, DateTimeKind.Utc).AddTicks(6930), new DateTime(2024, 2, 22, 15, 19, 45, 866, DateTimeKind.Utc).AddTicks(6934) });

            migrationBuilder.UpdateData(
                table: "FieldForMarketPlaces",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 22, 15, 19, 45, 866, DateTimeKind.Utc).AddTicks(6938), new DateTime(2024, 2, 22, 15, 19, 45, 866, DateTimeKind.Utc).AddTicks(6938) });

            migrationBuilder.UpdateData(
                table: "FieldForMarketPlaces",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 22, 15, 19, 45, 866, DateTimeKind.Utc).AddTicks(6940), new DateTime(2024, 2, 22, 15, 19, 45, 866, DateTimeKind.Utc).AddTicks(6941) });

            migrationBuilder.UpdateData(
                table: "FieldForMarketPlaces",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 22, 15, 19, 45, 866, DateTimeKind.Utc).AddTicks(6942), new DateTime(2024, 2, 22, 15, 19, 45, 866, DateTimeKind.Utc).AddTicks(6943) });
        }
    }
}
