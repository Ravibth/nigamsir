using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RMT.Projects.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class marketplafieldadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "PublishedFieldForMarketPlaces",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "PublishedFieldForMarketPlaces",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "PublishedFieldForMarketPlaces",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "PublishedFieldForMarketPlaces",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "FieldForMarketPlaces",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "FieldForMarketPlaces",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "FieldForMarketPlaces",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "FieldForMarketPlaces",
                type: "text",
                nullable: true);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "PublishedFieldForMarketPlaces");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "PublishedFieldForMarketPlaces");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "PublishedFieldForMarketPlaces");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "PublishedFieldForMarketPlaces");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "FieldForMarketPlaces");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "FieldForMarketPlaces");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "FieldForMarketPlaces");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "FieldForMarketPlaces");
        }
    }
}
