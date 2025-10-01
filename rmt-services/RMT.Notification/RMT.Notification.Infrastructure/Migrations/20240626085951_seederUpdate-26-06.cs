using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Notification.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class seederUpdate2606 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 296L,
                column: "to",
                value: "previousrolesemails");

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 297L,
                column: "to",
                value: "previousrolesemails");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 296L,
                column: "to",
                value: "ResourceRequestor,Reviewer,Delegate,AdditionalEl,AdditionalDelegate");

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 297L,
                column: "to",
                value: "ResourceRequestor,Reviewer,Delegate,AdditionalEl,AdditionalDelegate");
        }
    }
}
