using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Notification.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class notificationuatv7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 155L,
                column: "to",
                value: "ResourceRequestor,Delegate,Reviewer,AdditionalEl,AdditionalDelegate");

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 156L,
                column: "to",
                value: "ResourceRequestor,Delegate,Reviewer,AdditionalEl,AdditionalDelegate");

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 306L,
                column: "to",
                value: "ResourceRequestor,Reviewer,AdditionalEl,AdditionalDelegate");

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 307L,
                column: "to",
                value: "ResourceRequestor,Reviewer");

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 308L,
                column: "to",
                value: "ResourceRequestor,Reviewer");

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 309L,
                column: "to",
                value: "ResourceRequestor,Reviewer,AdditionalEl,AdditionalDelegate");

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 310L,
                column: "to",
                value: "ResourceRequestor,Reviewer,AdditionalEl,AdditionalDelegate");

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 313L,
                column: "to",
                value: "ResourceRequestor,Reviewer,AdditionalEl,AdditionalDelegate");

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 314L,
                column: "to",
                value: "ResourceRequestor,Reviewer,AdditionalEl,AdditionalDelegate");

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 315L,
                column: "to",
                value: "ResourceRequestor,Reviewer,AdditionalEl,AdditionalDelegate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                keyValue: 306L,
                column: "to",
                value: "Requestor,Reviewer,AdditionalEl,AdditionalDelegate");

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 307L,
                column: "to",
                value: "Requestor,Reviewer");

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 308L,
                column: "to",
                value: "Requestor,Reviewer");

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 309L,
                column: "to",
                value: "Requestor,Reviewer,AdditionalEl,AdditionalDelegate");

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 310L,
                column: "to",
                value: "Requestor,Reviewer,AdditionalEl,AdditionalDelegate");

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 313L,
                column: "to",
                value: "Requestor,Reviewer,AdditionalEl,AdditionalDelegate");

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 314L,
                column: "to",
                value: "Requestor,Reviewer,AdditionalEl,AdditionalDelegate");

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 315L,
                column: "to",
                value: "Requestor,Reviewer,AdditionalEl,AdditionalDelegate");
        }
    }
}
