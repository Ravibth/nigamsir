using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Skill.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddSkillCodecolum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SkillCode",
                table: "Skills",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "SkillCategoryMaster",
                keyColumn: "id",
                keyValue: 1L,
                column: "CreateDate",
                value: new DateTime(2024, 2, 8, 13, 4, 47, 688, DateTimeKind.Utc).AddTicks(1226));

            migrationBuilder.UpdateData(
                table: "SkillCategoryMaster",
                keyColumn: "id",
                keyValue: 2L,
                column: "CreateDate",
                value: new DateTime(2024, 2, 8, 13, 4, 47, 688, DateTimeKind.Utc).AddTicks(1232));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SkillCode",
                table: "Skills");

            migrationBuilder.UpdateData(
                table: "SkillCategoryMaster",
                keyColumn: "id",
                keyValue: 1L,
                column: "CreateDate",
                value: new DateTime(2024, 2, 5, 10, 38, 24, 432, DateTimeKind.Utc).AddTicks(2338));

            migrationBuilder.UpdateData(
                table: "SkillCategoryMaster",
                keyColumn: "id",
                keyValue: 2L,
                column: "CreateDate",
                value: new DateTime(2024, 2, 5, 10, 38, 24, 432, DateTimeKind.Utc).AddTicks(2345));
        }
    }
}
