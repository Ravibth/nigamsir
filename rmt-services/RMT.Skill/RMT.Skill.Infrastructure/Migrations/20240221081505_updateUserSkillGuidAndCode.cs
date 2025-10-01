using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Skill.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateUserSkillGuidAndCode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserSkills");

            migrationBuilder.CreateTable(
                name: "UserSkills",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SkillName = table.Column<string>(type: "text", nullable: false),
                    SkillCode = table.Column<string>(type: "text", nullable: false),
                    Proficiency = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    EmpId = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CreatedBy = table.Column<string>(type: "text", nullable: false),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedBy = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSkills", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "SkillCategoryMaster",
                keyColumn: "id",
                keyValue: 1L,
                column: "CreateDate",
                value: new DateTime(2024, 2, 21, 8, 15, 5, 583, DateTimeKind.Utc).AddTicks(7816));

            migrationBuilder.UpdateData(
                table: "SkillCategoryMaster",
                keyColumn: "id",
                keyValue: 2L,
                column: "CreateDate",
                value: new DateTime(2024, 2, 21, 8, 15, 5, 583, DateTimeKind.Utc).AddTicks(7824));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserSkills");

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
    }
}
