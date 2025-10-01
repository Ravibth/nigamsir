using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RMT.Notification.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class pushnotificationlinkadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "NotificationTemplateLinks",
                columns: new[] { "Id", "Link", "Module", "SubModule" },
                values: new object[,]
                {
                    { 1L, "/myskill", "Notification", "Skill" },
                    { 2L, "/skill-review", "Notification", "Skill" },
                    { 3L, "/searchskill", "Notification", "Skill" },
                    { 4L, "/skillmaster", "Notification", "Skill" },
                    { 5L, "/projects", "Notification", "Skill" },
                    { 6L, "/projects", "Notification", "Project" },
                    { 7L, "/marketplace", "Notification", "Project" },
                    { 8L, "/my-preference", "Notification", "Manage" },
                    { 9L, "/my-preference", "Notification", "Manage" },
                    { 10L, "/my-calender", "Notification", "Manage" },
                    { 11L, "/roles-permission", "Notification", "User" },
                    { 12L, "/myapproval", "Notification", "User" },
                    { 13L, "/project-details/<<pipelinecode>>/<<jobcode>>?tab=0", "Notification", "Details" },
                    { 14L, "/project-details/<<pipelinecode>>/<<jobcode>>?tab=1", "Notification", "Details" },
                    { 15L, "/project-details/<<pipelinecode>>/<<jobcode>>?tab=2", "Notification", "Details" },
                    { 16L, "/project-details/<<pipelinecode>>/<<jobcode>>?tab3", "Notification", "Details" },
                    { 17L, "/project-details/<<pipelinecode>>/<<jobcode>>?tab4", "Notification", "Details" },
                    { 18L, "/project-details/<<pipelinecode>>/<<jobcode>>?tab5", "Notification", "Details" },
                    { 19L, "/project-details/<<newPipelineCode>>/<<newJobCode>>?tab0", "Notification", "Details" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 11L);

            migrationBuilder.DeleteData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 12L);

            migrationBuilder.DeleteData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 13L);

            migrationBuilder.DeleteData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 14L);

            migrationBuilder.DeleteData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 15L);

            migrationBuilder.DeleteData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 16L);

            migrationBuilder.DeleteData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 17L);

            migrationBuilder.DeleteData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 18L);

            migrationBuilder.DeleteData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 19L);
        }
    }
}
