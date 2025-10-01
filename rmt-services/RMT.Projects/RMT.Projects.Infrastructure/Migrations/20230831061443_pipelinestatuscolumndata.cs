using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Projects.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class pipelinestatuscolumndata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ProjectRoles",
                keyColumn: "Id",
                keyValue: 112L,
                column: "Role",
                value: "Delegate");

            migrationBuilder.UpdateData(
                table: "ProjectRoles",
                keyColumn: "Id",
                keyValue: 115L,
                column: "Role",
                value: "Delegate");

            migrationBuilder.UpdateData(
                table: "ProjectRoles",
                keyColumn: "Id",
                keyValue: 118L,
                column: "Role",
                value: "Delegate");

            migrationBuilder.UpdateData(
                table: "ProjectRoles",
                keyColumn: "Id",
                keyValue: 121L,
                column: "Role",
                value: "Delegate");

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 101L,
                columns: new[] { "CreatedDate", "EndDate", "PipelineStatus", "StartDate" },
                values: new object[] { new DateTime(2023, 9, 5, 6, 14, 42, 586, DateTimeKind.Utc).AddTicks(6295), new DateTime(2023, 9, 15, 6, 14, 42, 586, DateTimeKind.Utc).AddTicks(6285), "WON", new DateTime(2023, 9, 5, 6, 14, 42, 586, DateTimeKind.Utc).AddTicks(6269) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 102L,
                columns: new[] { "CreatedDate", "EndDate", "PipelineStatus", "StartDate" },
                values: new object[] { new DateTime(2023, 9, 5, 6, 14, 42, 586, DateTimeKind.Utc).AddTicks(6341), new DateTime(2023, 9, 15, 6, 14, 42, 586, DateTimeKind.Utc).AddTicks(6335), "WON", new DateTime(2023, 9, 5, 6, 14, 42, 586, DateTimeKind.Utc).AddTicks(6334) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 103L,
                columns: new[] { "CreatedDate", "EndDate", "PipelineStatus", "StartDate" },
                values: new object[] { new DateTime(2023, 9, 5, 6, 14, 42, 586, DateTimeKind.Utc).AddTicks(6351), new DateTime(2023, 9, 15, 6, 14, 42, 586, DateTimeKind.Utc).AddTicks(6348), "WON", new DateTime(2023, 9, 5, 6, 14, 42, 586, DateTimeKind.Utc).AddTicks(6347) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 104L,
                columns: new[] { "CreatedDate", "EndDate", "PipelineStatus", "StartDate" },
                values: new object[] { new DateTime(2023, 8, 31, 6, 14, 42, 586, DateTimeKind.Utc).AddTicks(6447), new DateTime(2023, 9, 15, 6, 14, 42, 586, DateTimeKind.Utc).AddTicks(6440), "WON", new DateTime(2023, 9, 5, 6, 14, 42, 586, DateTimeKind.Utc).AddTicks(6437) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ProjectRoles",
                keyColumn: "Id",
                keyValue: 112L,
                column: "Role",
                value: "DelegeteUser");

            migrationBuilder.UpdateData(
                table: "ProjectRoles",
                keyColumn: "Id",
                keyValue: 115L,
                column: "Role",
                value: "DelegeteUser");

            migrationBuilder.UpdateData(
                table: "ProjectRoles",
                keyColumn: "Id",
                keyValue: 118L,
                column: "Role",
                value: "DelageteUser");

            migrationBuilder.UpdateData(
                table: "ProjectRoles",
                keyColumn: "Id",
                keyValue: 121L,
                column: "Role",
                value: "DelageteUser");

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 101L,
                columns: new[] { "CreatedDate", "EndDate", "PipelineStatus", "StartDate" },
                values: new object[] { new DateTime(2023, 9, 3, 9, 10, 46, 402, DateTimeKind.Utc).AddTicks(201), new DateTime(2023, 9, 13, 9, 10, 46, 402, DateTimeKind.Utc).AddTicks(187), null, new DateTime(2023, 9, 3, 9, 10, 46, 402, DateTimeKind.Utc).AddTicks(169) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 102L,
                columns: new[] { "CreatedDate", "EndDate", "PipelineStatus", "StartDate" },
                values: new object[] { new DateTime(2023, 9, 3, 9, 10, 46, 402, DateTimeKind.Utc).AddTicks(222), new DateTime(2023, 9, 13, 9, 10, 46, 402, DateTimeKind.Utc).AddTicks(214), null, new DateTime(2023, 9, 3, 9, 10, 46, 402, DateTimeKind.Utc).AddTicks(212) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 103L,
                columns: new[] { "CreatedDate", "EndDate", "PipelineStatus", "StartDate" },
                values: new object[] { new DateTime(2023, 9, 3, 9, 10, 46, 402, DateTimeKind.Utc).AddTicks(235), new DateTime(2023, 9, 13, 9, 10, 46, 402, DateTimeKind.Utc).AddTicks(230), null, new DateTime(2023, 9, 3, 9, 10, 46, 402, DateTimeKind.Utc).AddTicks(228) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 104L,
                columns: new[] { "CreatedDate", "EndDate", "PipelineStatus", "StartDate" },
                values: new object[] { new DateTime(2023, 8, 29, 9, 10, 46, 402, DateTimeKind.Utc).AddTicks(247), new DateTime(2023, 9, 13, 9, 10, 46, 402, DateTimeKind.Utc).AddTicks(242), null, new DateTime(2023, 9, 3, 9, 10, 46, 402, DateTimeKind.Utc).AddTicks(240) });
        }
    }
}
