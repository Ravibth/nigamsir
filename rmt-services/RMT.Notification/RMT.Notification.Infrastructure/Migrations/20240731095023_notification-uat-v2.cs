using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Notification.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class notificationuatv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 141L,
                column: "to",
                value: "resource_requestor_allocation_wf,Reviewer");

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 142L,
                column: "to",
                value: "resource_requestor_allocation_wf,Reviewer");

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 145L,
                column: "to",
                value: "resource_requestor_allocation_wf,Reviewer");

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 146L,
                column: "to",
                value: "resource_requestor_allocation_wf,Reviewer");

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 411L,
                column: "to",
                value: "resource_requestor_allocation_wf,Reviewer");

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 412L,
                column: "to",
                value: "resource_requestor_allocation_wf,Reviewer");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 141L,
                column: "to",
                value: "resource_requestor_allocation_wf");

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 142L,
                column: "to",
                value: "resource_requestor_allocation_wf");

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 145L,
                column: "to",
                value: "resource_requestor_allocation_wf");

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 146L,
                column: "to",
                value: "resource_requestor_allocation_wf");

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 411L,
                column: "to",
                value: "resource_requestor_allocation_wf");

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 412L,
                column: "to",
                value: "resource_requestor_allocation_wf");
        }
    }
}
