using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace RMT.Skill.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addUserSkill : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserSkills",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SkillName = table.Column<string>(type: "text", nullable: false),
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
                value: new DateTime(2024, 2, 6, 13, 11, 10, 238, DateTimeKind.Utc).AddTicks(671));

            migrationBuilder.UpdateData(
                table: "SkillCategoryMaster",
                keyColumn: "id",
                keyValue: 2L,
                column: "CreateDate",
                value: new DateTime(2024, 2, 6, 13, 11, 10, 238, DateTimeKind.Utc).AddTicks(682));
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
