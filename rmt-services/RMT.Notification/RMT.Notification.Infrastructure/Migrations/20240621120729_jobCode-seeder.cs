using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Notification.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class jobCodeseeder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 403L,
                column: "template",
                value: "Project: <<projectname>> allocations have been moved to new project code");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 403L,
                column: "template",
                value: "Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>> allocations have been moved to new project code");
        }
    }
}
