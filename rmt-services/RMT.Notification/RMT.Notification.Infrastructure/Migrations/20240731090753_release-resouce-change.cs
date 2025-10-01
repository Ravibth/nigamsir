using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Notification.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class releaseresoucechange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 263L,
                column: "to",
                value: "employee_name_release_resource,ResourceRequestor,Reviewer,AdditionalEl,Delegate,AdditionalDelegate");

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 264L,
                columns: new[] { "cc", "to" },
                values: new object[] { "ResourceRequestor,Reviewer,AdditionalEl,Delegate,AdditionalDelegate", "employee_name_release_resource" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 263L,
                column: "to",
                value: "employee_name_release_resource");

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 264L,
                columns: new[] { "cc", "to" },
                values: new object[] { "", "employee_name_release_resource,ResourceRequestor,Reviewer,AdditionalEl,Delegate,AdditionalDelegate" });
        }
    }
}
