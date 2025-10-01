using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RMT.Skill.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class skillcategorydata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "SkillCategoryMaster",
                columns: new[] { "id", "CategoryName", "CreateDate", "CreatedBy", "IsActive", "ModifieDate", "ModifiedBy" },
                values: new object[,]
                {
                    { 1L, "Technical", new DateTime(2024, 2, 5, 10, 38, 24, 432, DateTimeKind.Utc).AddTicks(2338), "System", true, null, null },
                    { 2L, "Soft", new DateTime(2024, 2, 5, 10, 38, 24, 432, DateTimeKind.Utc).AddTicks(2345), "System", true, null, null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SkillCategoryMaster",
                keyColumn: "id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "SkillCategoryMaster",
                keyColumn: "id",
                keyValue: 2L);
        }
    }
}
