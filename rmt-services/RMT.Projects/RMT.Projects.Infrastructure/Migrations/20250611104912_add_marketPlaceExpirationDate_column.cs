using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Projects.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_marketPlaceExpirationDate_column : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {            
            migrationBuilder.AddColumn<DateTime>(
               name: "MarketPlaceExpirationDate",
               table: "Projects",
               type: "timestamp with time zone",
               rowVersion: true,
               nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
               name: "MarketPlaceExpirationDate",
               table: "Projects");
        }
    }
}
