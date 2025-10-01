using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace RMT.Configuration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class configGroupDefaultValues : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "ConfigGroup",
            //    table: "ConfigurationGroups");

            //migrationBuilder.DropColumn(
            //    name: "ConfigGroupDisplay",
            //    table: "ConfigurationGroups");

            //migrationBuilder.DropColumn(
            //    name: "ConfigKey",
            //    table: "ConfigurationGroups");

            //migrationBuilder.DropColumn(
            //    name: "CongigDisplayText",
            //    table: "ConfigurationGroups");

            //migrationBuilder.DropColumn(
            //    name: "ValueType",
            //    table: "ConfigurationGroups");

            migrationBuilder.AddColumn<long>(
                name: "ConfigurationGroupMasterId",
                table: "ConfigurationGroups",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ConfigurationGroupMasters",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ConfigGroup = table.Column<string>(type: "text", nullable: false),
                    ConfigGroupDisplay = table.Column<string>(type: "text", nullable: false),
                    ConfigKey = table.Column<string>(type: "text", nullable: false),
                    CongigDisplayText = table.Column<string>(type: "text", nullable: false),
                    ValueType = table.Column<string>(type: "text", nullable: false),
                    DefaultValue = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConfigurationGroupMasters", x => x.Id);
                });


            migrationBuilder.CreateIndex(
                name: "IX_ConfigurationGroups_ConfigurationGroupMasterId",
                table: "ConfigurationGroups",
                column: "ConfigurationGroupMasterId");

            migrationBuilder.AddForeignKey(
                name: "FK_ConfigurationGroups_ConfigurationGroupMasters_Configuration~",
                table: "ConfigurationGroups",
                column: "ConfigurationGroupMasterId",
                principalTable: "ConfigurationGroupMasters",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ConfigurationGroups_ConfigurationGroupMasters_Configuration~",
                table: "ConfigurationGroups");

            migrationBuilder.DropTable(
                name: "ConfigurationGroupMasters");

            migrationBuilder.DropIndex(
                name: "IX_ConfigurationGroups_ConfigurationGroupMasterId",
                table: "ConfigurationGroups");

            migrationBuilder.DropColumn(
                name: "ConfigurationGroupMasterId",
                table: "ConfigurationGroups");

            //migrationBuilder.AddColumn<string>(
            //    name: "ConfigGroup",
            //    table: "ConfigurationGroups",
            //    type: "text",
            //    nullable: false,
            //    defaultValue: "");

            //migrationBuilder.AddColumn<string>(
            //    name: "ConfigGroupDisplay",
            //    table: "ConfigurationGroups",
            //    type: "text",
            //    nullable: false,
            //    defaultValue: "");

            //migrationBuilder.AddColumn<string>(
            //    name: "ConfigKey",
            //    table: "ConfigurationGroups",
            //    type: "text",
            //    nullable: false,
            //    defaultValue: "");

            //migrationBuilder.AddColumn<string>(
            //    name: "CongigDisplayText",
            //    table: "ConfigurationGroups",
            //    type: "text",
            //    nullable: false,
            //    defaultValue: "");

            //migrationBuilder.AddColumn<string>(
            //    name: "ValueType",
            //    table: "ConfigurationGroups",
            //    type: "text",
            //    nullable: false,
            //    defaultValue: "");

        }
    }
}
