using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace RMT.Projects.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddingtableforPublishFieldForMarketPlace : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_FieldForMarketPlace",
                table: "FieldForMarketPlace");

            migrationBuilder.RenameTable(
                name: "FieldForMarketPlace",
                newName: "FieldForMarketPlaces");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FieldForMarketPlaces",
                table: "FieldForMarketPlaces",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "PublishedFieldForMarketPlaces",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProjectCode = table.Column<string>(type: "text", nullable: false),
                    FieldName = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublishedFieldForMarketPlaces", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PublishedFieldForMarketPlaces");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FieldForMarketPlaces",
                table: "FieldForMarketPlaces");

            migrationBuilder.RenameTable(
                name: "FieldForMarketPlaces",
                newName: "FieldForMarketPlace");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FieldForMarketPlace",
                table: "FieldForMarketPlace",
                column: "Id");
        }
    }
}
