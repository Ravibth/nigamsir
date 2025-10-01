using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Notification.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class suspend_change_v1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 303L,
                columns: new[] { "cc", "to" },
                values: new object[] { "Reviewer,AdditionalEl,AdditionalDelegate", "ResourceRequestor" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 304L,
                column: "to",
                value: "ResourceRequestor,Reviewer,AdditionalEl,AdditionalDelegate");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 303L,
                columns: new[] { "cc", "to" },
                values: new object[] { null, "Requestor,Reviewer,AdditionalEl,AdditionalDelegate" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 304L,
                column: "to",
                value: "Requestor,Reviewer,AdditionalEl,AdditionalDelegate");
        }
    }
}
