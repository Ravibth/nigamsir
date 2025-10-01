using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Projects.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RequisitionCreateDefaultValue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsRequisitionCreationallowed",
                table: "Projects",
                type: "boolean",
                defaultValue: true,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
               name: "IsRequisitionCreationallowed",
               table: "Projects",
               type: "boolean",
               defaultValue: true,
               nullable: true);
        }
    }
}
