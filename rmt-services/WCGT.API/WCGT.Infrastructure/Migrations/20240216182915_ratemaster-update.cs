using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WCGT.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ratemasterupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RatePerDay",
                table: "RateDesignationMaster",
                newName: "RatePerHour");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "RatePerHour",
                table: "RateDesignationMaster",
                newName: "RatePerDay");
        }
    }
}
