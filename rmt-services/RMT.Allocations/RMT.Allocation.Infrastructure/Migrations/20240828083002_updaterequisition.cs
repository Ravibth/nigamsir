using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Allocation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updaterequisition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SMEG",
                table: "Requisition",
                newName: "Solutions");

            migrationBuilder.RenameColumn(
                name: "Expertise",
                table: "Requisition",
                newName: "Offerings");

            migrationBuilder.AddColumn<string>(
                name: "Competency",
                table: "Requisition",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CompetencyId",
                table: "Requisition",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Competency",
                table: "Requisition");

            migrationBuilder.DropColumn(
                name: "CompetencyId",
                table: "Requisition");

            migrationBuilder.RenameColumn(
                name: "Solutions",
                table: "Requisition",
                newName: "SMEG");

            migrationBuilder.RenameColumn(
                name: "Offerings",
                table: "Requisition",
                newName: "Expertise");
        }
    }
}
