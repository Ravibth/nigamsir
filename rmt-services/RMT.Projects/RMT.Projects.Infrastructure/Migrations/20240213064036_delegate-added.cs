using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Projects.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class delegateadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DelegateEmail",
                table: "ProjectRoles",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DelegateUserName",
                table: "ProjectRoles",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "FieldForMarketPlaces",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 13, 6, 40, 35, 878, DateTimeKind.Utc).AddTicks(6039), new DateTime(2024, 2, 13, 6, 40, 35, 878, DateTimeKind.Utc).AddTicks(6042) });

            migrationBuilder.UpdateData(
                table: "FieldForMarketPlaces",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 13, 6, 40, 35, 878, DateTimeKind.Utc).AddTicks(6044), new DateTime(2024, 2, 13, 6, 40, 35, 878, DateTimeKind.Utc).AddTicks(6045) });

            migrationBuilder.UpdateData(
                table: "FieldForMarketPlaces",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 13, 6, 40, 35, 878, DateTimeKind.Utc).AddTicks(6049), new DateTime(2024, 2, 13, 6, 40, 35, 878, DateTimeKind.Utc).AddTicks(6049) });

            migrationBuilder.UpdateData(
                table: "FieldForMarketPlaces",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "CreatedAt", "ModifiedAt" },
                values: new object[] { new DateTime(2024, 2, 13, 6, 40, 35, 878, DateTimeKind.Utc).AddTicks(6051), new DateTime(2024, 2, 13, 6, 40, 35, 878, DateTimeKind.Utc).AddTicks(6051) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DelegateEmail",
                table: "ProjectRoles");

            migrationBuilder.DropColumn(
                name: "DelegateUserName",
                table: "ProjectRoles");

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
    }
}
