using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RMT.Notification.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class bellnotificationv1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "NotificationTemplates",
                columns: new[] { "Id", "cc", "is_active", "link_id", "module", "notification_type", "sub_module", "subject", "subscription_role", "template", "to", "type" },
                values: new object[,]
                {
                    { 409L, null, true, null, "Notification", "PUSH", "Notification", "PUSH_NOTIFICATION_FOR_ALLOCATION_OF_RESOURCE_TO_PROJECT_REVIEWER_CONFIG_OFF", null, "Resources have been assigned to the project <<ProjectName:ResourceAllocationDetails:item_id>> on <<created_at>>. ", "resource_requestor_allocation_wf,Reviewer", "PUSH_NOTIFICATION_FOR_ALLOCATION_OF_RESOURCE_TO_PROJECT_REVIEWER_CONFIG_OFF" },
                    { 410L, null, true, null, "Notification", "PUSH", "Notification", "PUSH_NOTIFICATION_TO_REQUESTOR_AFTER_REVIEWER_ACCEPTS", null, "Resource Allocation Request for Project: <<ProjectName:ResourceAllocationDetails:item_id>>  have been approved by reviewer <<updated_by>>. ", "resource_requestor_allocation_wf", "PUSH_NOTIFICATION_TO_REQUESTOR_AFTER_REVIEWER_ACCEPTS" },
                    { 411L, null, true, null, "Notification", "PUSH", "Notification", "PUSH_NOTIFICATION_TO_REQUESTOR_ONCE_APPROVED_BY_EMPLOYEE", null, "Resource Allocation Request for Project: <<ProjectName:ResourceAllocationDetails:item_id>> have been confirmed by employee <<empEmail:ResourceAllocationDetails:item_id>>. ", "resource_requestor_allocation_wf", "PUSH_NOTIFICATION_TO_REQUESTOR_ONCE_APPROVED_BY_EMPLOYEE" },
                    { 412L, null, true, null, "Notification", "PUSH", "Notification", "PUSH_NOTIFICATION_TO_REQUESTOR_ONCE_WITHDRAWAL_BY_EMPLOYEE", null, "Resource Allocation Request for Project: <<ProjectName:ResourceAllocationDetails:item_id>> have been confirmed by employee <<empEmail:ResourceAllocationDetails:item_id>>. ", "resource_requestor_allocation_wf", "PUSH_NOTIFICATION_TO_REQUESTOR_ONCE_WITHDRAWAL_BY_EMPLOYEE" },
                    { 413L, null, true, null, "Notification", "PUSH", "Notification", "PUSH_NOTIFICATION_TO_REQUESTOR_REVIEWER_ACCEPTS_EMPLOYEE_UPDATE", null, "Allocation of <<empEmail:ResourceAllocationDetails:item_id>>, <<designation:ResourceAllocationDetails:item_id>> on the Project: <<ProjectName:ResourceAllocationDetails:item_id>> has been updated. ", "resource_requestor_allocation_wf", "PUSH_NOTIFICATION_TO_REQUESTOR_REVIEWER_ACCEPTS_EMPLOYEE_UPDATE" },
                    { 414L, null, true, null, "Notification", "PUSH", "Notification", "PUSH_NOTIFICATION_TO_REQUESTOR_AND_REVIEWER_AFTER_EMPLOYEE_UPDATE", null, "Employee allocation on the Project: <<ProjectName:ResourceAllocationDetails:item_id>> has been updated. ", "resource_requestor_allocation_wf,Reviewer", "PUSH_NOTIFICATION_TO_REQUESTOR_AND_REVIEWER_AFTER_EMPLOYEE_UPDATE" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 409L);

            migrationBuilder.DeleteData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 410L);

            migrationBuilder.DeleteData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 411L);

            migrationBuilder.DeleteData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 412L);

            migrationBuilder.DeleteData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 413L);

            migrationBuilder.DeleteData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 414L);
        }
    }
}
