using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WCGT.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class region : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<string>(
                name: "region_name",
                table: "Locations",
                type: "text",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "location_id",
                keyValue: "LC000001",
                column: "region_name",
                value: "North");

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "location_id",
                keyValue: "LC000002",
                column: "region_name",
                value: "West");

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "location_id",
                keyValue: "LC000004",
                column: "region_name",
                value: "South");

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "location_id",
                keyValue: "LC000005",
                column: "region_name",
                value: "South");

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "location_id",
                keyValue: "LC000006",
                column: "region_name",
                value: "North");

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "location_id",
                keyValue: "LC000007",
                column: "region_name",
                value: "West");

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "location_id",
                keyValue: "LC000009",
                column: "region_name",
                value: "North");

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "location_id",
                keyValue: "LC000010",
                column: "region_name",
                value: "South");

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "location_id",
                keyValue: "LC000011",
                column: "region_name",
                value: "North");

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "location_id",
                keyValue: "LC000012",
                column: "region_name",
                value: "West");

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "location_id",
                keyValue: "LC000013",
                column: "region_name",
                value: "North");

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "location_id",
                keyValue: "LC000014",
                column: "region_name",
                value: "West");

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "location_id",
                keyValue: "LC000015",
                column: "region_name",
                value: "South");

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "location_id",
                keyValue: "LC000016",
                column: "region_name",
                value: "Others");

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "location_id",
                keyValue: "LC000017",
                column: "region_name",
                value: "North");

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "location_id",
                keyValue: "LC000018",
                column: "region_name",
                value: "South");

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "location_id",
                keyValue: "LC000019",
                column: "region_name",
                value: "North");

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "location_id",
                keyValue: "LC000020",
                column: "region_name",
                value: "North");

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "location_id",
                keyValue: "LC000021",
                column: "region_name",
                value: "North");

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "location_id",
                keyValue: "LC000022",
                column: "region_name",
                value: "West");

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "location_id",
                keyValue: "LC000023",
                column: "region_name",
                value: "North");

            migrationBuilder.UpdateData(
                table: "Locations",
                keyColumn: "location_id",
                keyValue: "LC000024",
                column: "region_name",
                value: "South");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(
                name: "region_name",
                table: "Locations");
        }
    }
}
