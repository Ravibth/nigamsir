using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Projects.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class JustificationAllocation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "JustificationToAllocate",
                table: "Projects",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "PublishedToMarketPlaceDate",
                table: "Projects",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 201L,
                columns: new[] { "JustificationToAllocate", "PublishedToMarketPlaceDate" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 202L,
                columns: new[] { "JustificationToAllocate", "PublishedToMarketPlaceDate" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 203L,
                columns: new[] { "JustificationToAllocate", "PublishedToMarketPlaceDate" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 204L,
                columns: new[] { "JustificationToAllocate", "PublishedToMarketPlaceDate" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 205L,
                columns: new[] { "JustificationToAllocate", "PublishedToMarketPlaceDate" },
                values: new object[] { null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JustificationToAllocate",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "PublishedToMarketPlaceDate",
                table: "Projects");
        }
    }
}
