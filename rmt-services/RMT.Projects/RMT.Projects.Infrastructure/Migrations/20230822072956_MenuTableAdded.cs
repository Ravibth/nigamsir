using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RMT.Projects.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class MenuTableAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Menu",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Parent = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Url = table.Column<string>(type: "character varying(400)", maxLength: 400, nullable: false),
                    DisplayIcon = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menu", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Menu",
                columns: new[] { "Id", "DisplayIcon", "Parent", "Role", "Title", "Type", "Url" },
                values: new object[,]
                {
                    { 1L, "", "#", "", "Projects", "TopMenu", "/" },
                    { 2L, "", "#", "", "Employee", "TopMenu", "/" },
                    { 3L, "", "#", "", "Reports", "TopMenu", "/" },
                    { 4L, "", "#", "", "Manage", "TopMenu", "/" },
                    { 5L, "", "#", "", "Marketplace", "TopMenu", "/" },
                    { 6L, "", "#", "", "View Details", "ProjectContextMenu", "/" },
                    { 7L, "", "#", "", "Create Requisition", "ProjectContextMenu", "/" },
                    { 8L, "", "#", "", "Upload Requisition Excel", "ProjectContextMenu", "/" },
                    { 9L, "", "#", "", "Show Calendar View", "EmployeeContextMenu", "/" },
                    { 10L, "", "#", "", "Update Allocation", "EmployeeContextMenu", "/" },
                    { 11L, "", "#", "", "Release Employee", "EmployeeContextMenu", "/" },
                    { 12L, "", "#", "", "Move to new Job code", "EmployeeContextMenu", "/" }
                });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Menu");

        }
    }
}
