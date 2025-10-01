using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Skill.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class skillCodeadd : Migration
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

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "SkillCode",
            //    table: "Skills");

        }
    }
}
