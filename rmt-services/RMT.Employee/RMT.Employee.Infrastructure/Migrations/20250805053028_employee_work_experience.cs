using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace RMT.Employee.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class employee_work_experience : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmployeeWorkExperience",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name_of_employer = table.Column<string>(type: "text", nullable: false),
                    from = table.Column<string>(type: "text", nullable: false),
                    to = table.Column<string>(type: "text", nullable: false),
                    last_designation_held = table.Column<string>(type: "text", nullable: false),
                    employee_mid = table.Column<string>(type: "text", nullable: false),
                    employee_profile_id = table.Column<long>(type: "bigint", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    created_by = table.Column<string>(type: "text", nullable: false),
                    updated_by = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeWorkExperience", x => x.id);
                    table.ForeignKey(
                        name: "FK_EmployeeWorkExperience_EmployeeProfile_employee_profile_id",
                        column: x => x.employee_profile_id,
                        principalTable: "EmployeeProfile",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeWorkExperience_employee_profile_id",
                table: "EmployeeWorkExperience",
                column: "employee_profile_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmployeeWorkExperience");
        }
    }
}
