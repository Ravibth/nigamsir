using System;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Configuration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class configschemaupdate2310 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ConfigurationMaster",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ConfigGroup = table.Column<string>(type: "text", nullable: false),
                    ConfigGroupDisplay = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ConfigNote = table.Column<string>(type: "text", nullable: false),
                    GlobalDefaultDisplay = table.Column<bool>(type: "boolean", nullable: false),
                    SelectorWiseDisplay = table.Column<bool>(type: "boolean", nullable: false),
                    SelectorConfigType = table.Column<string>(type: "text", nullable: false),
                    schema = table.Column<JsonDocument>(type: "jsonb", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigurationMaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConfigurationMainBreakup",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ConfigurationMasterId = table.Column<Guid>(type: "uuid", nullable: false),
                    KeySelector = table.Column<string>(type: "text", nullable: false),
                    MetaValue = table.Column<JsonDocument>(type: "jsonb", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    ModifiedBy = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigurationMainBreakup", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConfigurationMainBreakup_ConfigurationMaster_ConfigurationM~",
                        column: x => x.ConfigurationMasterId,
                        principalTable: "ConfigurationMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationMainBreakup_ConfigurationMasterId",
                table: "ConfigurationMainBreakup",
                column: "ConfigurationMasterId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConfigurationMainBreakup");

            migrationBuilder.DropTable(
                name: "ConfigurationMaster");
        }
    }
}
