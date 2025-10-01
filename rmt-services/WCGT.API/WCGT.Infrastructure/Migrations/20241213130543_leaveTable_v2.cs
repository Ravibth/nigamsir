using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WCGT.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class leaveTable_v2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "end_date_flag",
                table: "Leaves");

            migrationBuilder.DropColumn(
                name: "start_date_flag",
                table: "Leaves");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "end_date_flag",
                table: "Leaves",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "start_date_flag",
                table: "Leaves",
                type: "text",
                nullable: true);
        }
    }
}
