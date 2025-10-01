using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.MarketPlace.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addfieldtoprojectDetailTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ClientGroup",
                table: "MarketPlaceProjectDetail",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "MarketPlaceExpirationDate",
                table: "MarketPlaceProjectDetail",
                type: "timestamp with time zone",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientGroup",
                table: "MarketPlaceProjectDetail");

            migrationBuilder.DropColumn(
                name: "MarketPlaceExpirationDate",
                table: "MarketPlaceProjectDetail");
        }
    }
}
