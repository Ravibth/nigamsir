using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WCGT.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateemployee_qualification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "to_date",
                table: "PastEmploymentDetails",
                newName: "to");

            migrationBuilder.RenameColumn(
                name: "from_date",
                table: "PastEmploymentDetails",
                newName: "from");

            migrationBuilder.RenameColumn(
                name: "write_flag",
                table: "Language",
                newName: "write");

            migrationBuilder.RenameColumn(
                name: "speak_flag",
                table: "Language",
                newName: "speak");

            migrationBuilder.RenameColumn(
                name: "read_flag",
                table: "Language",
                newName: "read");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "to",
                table: "PastEmploymentDetails",
                newName: "to_date");

            migrationBuilder.RenameColumn(
                name: "from",
                table: "PastEmploymentDetails",
                newName: "from_date");

            migrationBuilder.RenameColumn(
                name: "write",
                table: "Language",
                newName: "write_flag");

            migrationBuilder.RenameColumn(
                name: "speak",
                table: "Language",
                newName: "speak_flag");

            migrationBuilder.RenameColumn(
                name: "read",
                table: "Language",
                newName: "read_flag");
        }
    }
}
