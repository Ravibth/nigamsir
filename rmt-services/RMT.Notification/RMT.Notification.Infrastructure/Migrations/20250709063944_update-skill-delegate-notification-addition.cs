using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Notification.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateskilldelegatenotificationaddition : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 507L,
                column: "template",
                value: "You have been assigned as a Delegate to <<supercoach_email>> in OptiWise to support Skill Workflow Management.<br/>If you have any questions about your responsibilities, please feel free to reach out to <<supercoach_email>> for guidance.");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 507L,
                column: "template",
                value: "You have been assigned as a Delegate to <<supercoach_email>> in OptiWise to support Allocation Workflow Management.<br/>If you have any questions about your responsibilities, please feel free to reach out to <<supercoach_email>> for guidance.");
        }
    }
}
