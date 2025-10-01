using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Configuration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updaterequistionparametervalue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                           table: "ProjectConfigurations",
                           keyColumn: "Id",
                           keyValue: 55L,
                           column: "AttributeValue",
                           value: "8");

            migrationBuilder.UpdateData(
                           table: "ProjectConfigurations",
                           keyColumn: "Id",
                           keyValue: 64L,
                           column: "AttributeValue",
                           value: "8");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                          table: "ProjectConfigurations",
                          keyColumn: "Id",
                          keyValue: 55L,
                          column: "AttributeValue",
                          value: 10);

            migrationBuilder.UpdateData(
                           table: "ProjectConfigurations",
                           keyColumn: "Id",
                           keyValue: 64L,
                           column: "AttributeValue",
                           value: 10);
        }
    }
    
}
