using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Notification.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class notificationdbschemav1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotificationTemplates_NotificationTemplateLinks_link_id",
                table: "NotificationTemplates");

            migrationBuilder.AlterColumn<string>(
                name: "sub_module",
                table: "NotificationTemplates",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "module",
                table: "NotificationTemplates",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<long>(
                name: "link_id",
                table: "NotificationTemplates",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationTemplates_NotificationTemplateLinks_link_id",
                table: "NotificationTemplates",
                column: "link_id",
                principalTable: "NotificationTemplateLinks",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NotificationTemplates_NotificationTemplateLinks_link_id",
                table: "NotificationTemplates");

            migrationBuilder.AlterColumn<string>(
                name: "sub_module",
                table: "NotificationTemplates",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "module",
                table: "NotificationTemplates",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<long>(
                name: "link_id",
                table: "NotificationTemplates",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_NotificationTemplates_NotificationTemplateLinks_link_id",
                table: "NotificationTemplates",
                column: "link_id",
                principalTable: "NotificationTemplateLinks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
