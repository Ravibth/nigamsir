using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.MarketPlace.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class structurechnagesv3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Expertise",
                table: "MarketPlaceProjectDetail");

            migrationBuilder.DropColumn(
                name: "RevenueUnit",
                table: "MarketPlaceProjectDetail");

            migrationBuilder.DropColumn(
                name: "Sme",
                table: "MarketPlaceProjectDetail");

            migrationBuilder.DropColumn(
                name: "Smeg",
                table: "MarketPlaceProjectDetail");

            migrationBuilder.RenameColumn(
                name: "EmpName",
                table: "EmpProjectInterestScore",
                newName: "RequisitionSolutions");

            migrationBuilder.AddColumn<string>(
                name: "RequisitionBU",
                table: "EmpProjectInterestScore",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RequisitionCompetency",
                table: "EmpProjectInterestScore",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RequisitionOfferings",
                table: "EmpProjectInterestScore",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequisitionBU",
                table: "EmpProjectInterestScore");

            migrationBuilder.DropColumn(
                name: "RequisitionCompetency",
                table: "EmpProjectInterestScore");

            migrationBuilder.DropColumn(
                name: "RequisitionOfferings",
                table: "EmpProjectInterestScore");

            migrationBuilder.RenameColumn(
                name: "RequisitionSolutions",
                table: "EmpProjectInterestScore",
                newName: "EmpName");

            migrationBuilder.AddColumn<string>(
                name: "Expertise",
                table: "MarketPlaceProjectDetail",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RevenueUnit",
                table: "MarketPlaceProjectDetail",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sme",
                table: "MarketPlaceProjectDetail",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Smeg",
                table: "MarketPlaceProjectDetail",
                type: "text",
                nullable: true);
        }
    }
}
