using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Notification.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class suspendchanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 302L,
                column: "to",
                value: "employee_name_suspend");

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 303L,
                column: "subject",
                value: "Project:<<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> - <<pipelineStatus:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>>");

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 304L,
                column: "subject",
                value: "Project:<<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> - <<pipelineStatus:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>>");

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 305L,
                columns: new[] { "subject", "to" },
                values: new object[] { "Project:<<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> - <<pipelineStatus:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>>", "employee_name_suspend" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 302L,
                column: "to",
                value: "employee_name_allocation");

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 303L,
                column: "subject",
                value: "Project:<<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> - Suspended");

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 304L,
                column: "subject",
                value: "Project:<<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> - Suspended");

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 305L,
                columns: new[] { "subject", "to" },
                values: new object[] { "Project:<<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> - <<pipelineStatus :GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>>", "employee_name_allocation" });
        }
    }
}
