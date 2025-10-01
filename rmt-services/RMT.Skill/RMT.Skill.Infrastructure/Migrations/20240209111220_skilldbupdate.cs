using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Skill.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class skilldbupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<string>(
            //    name: "SkillCode",
            //    table: "Skills",
            //    type: "text",
            //    nullable: false,
            //    defaultValue: "");

            //migrationBuilder.UpdateData(
            //    table: "SkillCategoryMaster",
            //    keyColumn: "id",
            //    keyValue: 1L,
            //    column: "CreateDate",
            //    value: new DateTime(2024, 2, 9, 11, 12, 19, 187, DateTimeKind.Utc).AddTicks(2015));

            //migrationBuilder.UpdateData(
            //    table: "SkillCategoryMaster",
            //    keyColumn: "id",
            //    keyValue: 2L,
            //    column: "CreateDate",
            //    value: new DateTime(2024, 2, 9, 11, 12, 19, 187, DateTimeKind.Utc).AddTicks(2020));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "SkillCode",
            //    table: "Skills");

            //migrationBuilder.UpdateData(
            //    table: "SkillCategoryMaster",
            //    keyColumn: "id",
            //    keyValue: 1L,
            //    column: "CreateDate",
            //    value: new DateTime(2024, 2, 6, 13, 11, 10, 238, DateTimeKind.Utc).AddTicks(671));

            //migrationBuilder.UpdateData(
            //    table: "SkillCategoryMaster",
            //    keyColumn: "id",
            //    keyValue: 2L,
            //    column: "CreateDate",
            //    value: new DateTime(2024, 2, 6, 13, 11, 10, 238, DateTimeKind.Utc).AddTicks(682));
        }
    }
}
