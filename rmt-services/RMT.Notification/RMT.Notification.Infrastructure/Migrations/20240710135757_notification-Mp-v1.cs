using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Notification.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class notificationMpv1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "NotificationTemplates",
                columns: new[] { "Id", "cc", "is_active", "link_id", "module", "notification_type", "sub_module", "subject", "subscription_role", "template", "to", "type" },
                values: new object[] { 408L, null, true, null, "Notification", "PUSH", "Notification", "Project: <<projectname>> Markeplace Interest Submitted", null, "You have received an interest for the Project: <<projectname>> in Marketplace. Total <<noofinterested>> likes received.", "ResourceRequestor", "NOTIFICATION_FOR_INTEREST_IN_MARKETPLACE_AGAINST_THEIR_PROJECT" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 408L);
        }
    }
}
