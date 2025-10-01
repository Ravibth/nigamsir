using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Notification.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class notificationgradev1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 413L,
                column: "template",
                value: "Allocation of <<empEmail:ResourceAllocationDetails:item_id>>, <<designation:ResourceAllocationDetails:item_id>> , <<grade:ResourceAllocationDetails:item_id>> on the Project: <<ProjectName:ResourceAllocationDetails:item_id>> has been updated. ");

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 414L,
                column: "template",
                value: "Allocation of <<empEmail:ResourceAllocationDetails:item_id>>, <<designation:ResourceAllocationDetails:item_id>> , <<grade:ResourceAllocationDetails:item_id>> on the Project: <<ProjectName:ResourceAllocationDetails:item_id>> has been updated. ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 413L,
                column: "template",
                value: "Allocation of <<empEmail:ResourceAllocationDetails:item_id>>, <<designation:ResourceAllocationDetails:item_id>> on the Project: <<ProjectName:ResourceAllocationDetails:item_id>> has been updated. ");

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 414L,
                column: "template",
                value: "Employee allocation on the Project: <<ProjectName:ResourceAllocationDetails:item_id>> has been updated. ");
        }
    }
}
