using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Notification.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class tabs_link_fix_13_06 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 16L,
                column: "Link",
                value: "/project-details/<<pipelinecode>>/<<jobcode>>?tab=3");

            migrationBuilder.UpdateData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 17L,
                column: "Link",
                value: "/project-details/<<pipelinecode>>/<<jobcode>>?tab=4");

            migrationBuilder.UpdateData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 18L,
                column: "Link",
                value: "/project-details/<<pipelinecode>>/<<jobcode>>?tab=5");

            migrationBuilder.UpdateData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 19L,
                column: "Link",
                value: "/project-details/<<newPipelineCode>>/<<newJobCode>>?tab=0");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 16L,
                column: "Link",
                value: "/project-details/<<pipelinecode>>/<<jobcode>>?tab3");

            migrationBuilder.UpdateData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 17L,
                column: "Link",
                value: "/project-details/<<pipelinecode>>/<<jobcode>>?tab4");

            migrationBuilder.UpdateData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 18L,
                column: "Link",
                value: "/project-details/<<pipelinecode>>/<<jobcode>>?tab5");

            migrationBuilder.UpdateData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 19L,
                column: "Link",
                value: "/project-details/<<newPipelineCode>>/<<newJobCode>>?tab0");
        }
    }
}
