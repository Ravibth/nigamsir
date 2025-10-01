using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Notification.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class allocationdelegatenotificationaddition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "NotificationTemplates",
                columns: new[] { "Id", "cc", "is_active", "link_id", "module", "notification_type", "sub_module", "subject", "subscription_role", "template", "to", "type" },
                values: new object[] { 506L, null, true, null, "Notification", "EMAIL", "Notification", "Allocation Supercoach delegate assignment", null, "You have been assigned as a Delegate to <<supercoach_email>> in OptiWise to support Allocation Workflow Management.<br/>If you have any questions about your responsibilities, please feel free to reach out to <<supercoach_email>> for guidance.", "allocation_supercoach_email", "NOTIFICATION_FOR_ALLOCATION_SUPERCOACH_DELEGATE_CHANGE" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 506L);
        }
    }
}
