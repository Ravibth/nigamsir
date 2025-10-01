using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WCGT.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CompetencyChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Designation_id",
                table: "Budget");

            migrationBuilder.AddColumn<string>(
                name: "CompetencyId",
                table: "Employees",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Competencies",
                columns: table => new
                {
                    CompetencyId = table.Column<string>(type: "text", nullable: false),
                    CompetencyMID = table.Column<string>(type: "text", nullable: false),
                    CompetencyName = table.Column<string>(type: "text", nullable: false),
                    CompetencyLeaderMID = table.Column<string>(type: "text", nullable: false),
                    BuId = table.Column<string>(type: "text", nullable: false),
                    isactive = table.Column<bool>(type: "boolean", nullable: false),
                    createdat = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    modifiedat = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    createdby = table.Column<string>(type: "text", nullable: false),
                    modifiedby = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Competencies", x => x.CompetencyId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Competencies");

            migrationBuilder.DropColumn(
                name: "CompetencyId",
                table: "Employees");

            migrationBuilder.AddColumn<string>(
                name: "Designation_id",
                table: "Budget",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
