using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Notification.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class added_link_ids_13_06_update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 16L,
                column: "Link",
                value: "/project-details/<<pipelinecode>>/<<jobcode>>?tab3");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 16L,
                column: "Link",
                value: "/project-details/pipelinecode/jobcode?tab3");
        }
    }
}
