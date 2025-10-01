using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Notification.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class notificationuatv1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 155L,
                column: "to",
                value: "Requestor,Delegate,Reviewer,AdditionalEl,AdditionalDelegate");

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 156L,
                column: "to",
                value: "Requestor,Delegate,Reviewer,AdditionalEl,AdditionalDelegate");

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 408L,
                column: "to",
                value: "ResourceRequestor,Delegate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 155L,
                column: "to",
                value: "Requestor,Reviewer,AdditionalEl,AdditionalDelegate");

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 156L,
                column: "to",
                value: "Requestor,Reviewer,AdditionalEl,AdditionalDelegate");

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 408L,
                column: "to",
                value: "ResourceRequestor");
        }
    }
}
