using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Skill.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class removebuexperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BusinessUnit",
                table: "SkillMapping");

            migrationBuilder.DropColumn(
                name: "Experties",
                table: "SkillMapping");

            migrationBuilder.UpdateData(
                table: "SkillCategoryMaster",
                keyColumn: "id",
                keyValue: 1L,
                column: "CreateDate",
                value: new DateTime(2024, 8, 28, 17, 9, 50, 75, DateTimeKind.Utc).AddTicks(2229));

            migrationBuilder.UpdateData(
                table: "SkillCategoryMaster",
                keyColumn: "id",
                keyValue: 2L,
                column: "CreateDate",
                value: new DateTime(2024, 8, 28, 17, 9, 50, 75, DateTimeKind.Utc).AddTicks(2233));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BusinessUnit",
                table: "SkillMapping",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Experties",
                table: "SkillMapping",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "SkillCategoryMaster",
                keyColumn: "id",
                keyValue: 1L,
                column: "CreateDate",
                value: new DateTime(2024, 8, 28, 9, 13, 12, 349, DateTimeKind.Utc).AddTicks(6314));

            migrationBuilder.UpdateData(
                table: "SkillCategoryMaster",
                keyColumn: "id",
                keyValue: 2L,
                column: "CreateDate",
                value: new DateTime(2024, 8, 28, 9, 13, 12, 349, DateTimeKind.Utc).AddTicks(6321));
        }
    }
}
