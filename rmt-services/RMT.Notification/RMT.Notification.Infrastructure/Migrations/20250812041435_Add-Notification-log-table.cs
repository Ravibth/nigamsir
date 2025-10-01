using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Notification.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddNotificationlogtable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "NotificationLog",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    to = table.Column<string>(type: "text", nullable: false),
                    cc = table.Column<string>(type: "text", nullable: false),
                    body = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationLog", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotificationLog");
        }
    }
}
