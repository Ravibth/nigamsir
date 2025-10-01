using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WCGT.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class halfdayleaveflagsv1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "start_date_half",
                table: "Leaves",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "end_date_half",
                table: "Leaves",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "start_date_half",
                table: "Leaves");

            migrationBuilder.DropColumn(
                name: "end_date_half",
                table: "Leaves");

        }
    }
}
