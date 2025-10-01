using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace RMT.Notification.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class subscriptionTable1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "subscription_role",
                table: "NotificationTemplates",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "NotificationSubscription",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    module = table.Column<string>(type: "text", nullable: true),
                    subscription_role = table.Column<string>(type: "text", nullable: true),
                    user_emailid = table.Column<string>(type: "text", nullable: true),
                    user_name = table.Column<string>(type: "text", nullable: true),
                    is_active = table.Column<bool>(type: "boolean", nullable: true),
                    createdDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modifiedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    createdBy = table.Column<string>(type: "text", nullable: true),
                    modifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationSubscription", x => x.id);
                });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotificationSubscription");

            migrationBuilder.DropColumn(
                name: "subscription_role",
                table: "NotificationTemplates");
        }
    }
}
