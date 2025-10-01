using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WCGT.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class industry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SectorIndustrys",
                keyColumn: "id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "SectorIndustrys",
                keyColumn: "id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "SectorIndustrys",
                keyColumn: "id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "SectorIndustrys",
                keyColumn: "id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "SectorIndustrys",
                keyColumn: "id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "SectorIndustrys",
                keyColumn: "id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "SectorIndustrys",
                keyColumn: "id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "SectorIndustrys",
                keyColumn: "id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "SectorIndustrys",
                keyColumn: "id",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                table: "SectorIndustrys",
                keyColumn: "id",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                table: "SectorIndustrys",
                keyColumn: "id",
                keyValue: 11L);

            migrationBuilder.DeleteData(
                table: "SectorIndustrys",
                keyColumn: "id",
                keyValue: 12L);

            migrationBuilder.DeleteData(
                table: "SectorIndustrys",
                keyColumn: "id",
                keyValue: 13L);

            migrationBuilder.DeleteData(
                table: "SectorIndustrys",
                keyColumn: "id",
                keyValue: 14L);

            migrationBuilder.DeleteData(
                table: "SectorIndustrys",
                keyColumn: "id",
                keyValue: 15L);

            migrationBuilder.DeleteData(
                table: "SectorIndustrys",
                keyColumn: "id",
                keyValue: 16L);

            migrationBuilder.DeleteData(
                table: "SectorIndustrys",
                keyColumn: "id",
                keyValue: 17L);

            migrationBuilder.DeleteData(
                table: "SectorIndustrys",
                keyColumn: "id",
                keyValue: 18L);

            migrationBuilder.DeleteData(
                table: "SectorIndustrys",
                keyColumn: "id",
                keyValue: 19L);

            migrationBuilder.DeleteData(
                table: "SectorIndustrys",
                keyColumn: "id",
                keyValue: 20L);

            migrationBuilder.RenameColumn(
                name: "sub_sector_name",
                table: "SectorIndustrys",
                newName: "sub_industry_name");

            migrationBuilder.RenameColumn(
                name: "sub_sector_id",
                table: "SectorIndustrys",
                newName: "sub_industry_id");

            migrationBuilder.RenameColumn(
                name: "sector_name",
                table: "SectorIndustrys",
                newName: "industry_name");

            migrationBuilder.RenameColumn(
                name: "sector_id",
                table: "SectorIndustrys",
                newName: "industry_id");

            migrationBuilder.RenameColumn(
                name: "sector_id",
                table: "Pipelines",
                newName: "industry_id");

            migrationBuilder.CreateTable(
                name: "fun_resource_timesheet_view",
                columns: table => new
                {
                    empcode = table.Column<string>(type: "text", nullable: false),
                    empname = table.Column<string>(type: "text", nullable: false),
                    totaltime = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fun_resource_timesheet_view", x => x.empcode);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "fun_resource_timesheet_view");

            migrationBuilder.RenameColumn(
                name: "sub_industry_name",
                table: "SectorIndustrys",
                newName: "sub_sector_name");

            migrationBuilder.RenameColumn(
                name: "sub_industry_id",
                table: "SectorIndustrys",
                newName: "sub_sector_id");

            migrationBuilder.RenameColumn(
                name: "industry_name",
                table: "SectorIndustrys",
                newName: "sector_name");

            migrationBuilder.RenameColumn(
                name: "industry_id",
                table: "SectorIndustrys",
                newName: "sector_id");

            migrationBuilder.RenameColumn(
                name: "industry_id",
                table: "Pipelines",
                newName: "sector_id");

            migrationBuilder.InsertData(
                table: "SectorIndustrys",
                columns: new[] { "id", "createdat", "createdby", "isactive", "modifiedat", "modifiedby", "sector_id", "sector_name", "sub_sector_id", "sub_sector_name" },
                values: new object[,]
                {
                    { 1L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "SB000011", "Law Firms", "SI000191", "Law firms" },
                    { 2L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "SB000001", "Auto & Auto Components", "SI000004", "Tyres & rubber manufacturers" },
                    { 3L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "SB000002", "CIP & Retail", "SI000010", "Food distributors" },
                    { 4L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "SB000002", "CIP & Retail", "SI000044", "Tour operators" },
                    { 5L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "SB000002", "CIP & Retail", "SI000045", "Fitness facilities" },
                    { 6L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "SB000002", "CIP & Retail", "SI000048", "Vehicle rental and taxi companies" },
                    { 7L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "SB000003", "Financial Services", "SI000068", "Other financial institutions" },
                    { 8L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "SB000003", "Financial Services", "SI000084", "Consumer finance" },
                    { 9L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "SB000003", "Financial Services", "SI000085", "Brokerage" },
                    { 10L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "SB000004", "PE / VC", "SI000090", "Venture Capital & Venture Capital Trusts" },
                    { 11L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "SB000005", "Real Estate & Infra", "SI000093", "Construction/homebuilding" },
                    { 12L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "SB000005", "Real Estate & Infra", "SI000112", "Wind (cleantech)" },
                    { 13L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "SB000006", "TMT", "SI000132", "Software" },
                    { 14L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "SB000006", "TMT", "SI000134", "Technology services" },
                    { 15L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "SB000006", "TMT", "SI000158", "Diversified telecommunication services" },
                    { 16L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "SB000007", "Pharma & Healthcare Services", "SI000163", "Managed health care" },
                    { 17L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "SB000007", "Pharma & Healthcare Services", "SI000164", "Healthcare providers & services" },
                    { 18L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "SB000007", "Pharma & Healthcare Services", "SI000165", "Health care services" },
                    { 19L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "SB000008", "Govt", "SI000174", "Quasi-Government" },
                    { 20L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "SB000008", "Govt", "SI000179", "Social services organisations" }
                });
        }
    }
}
