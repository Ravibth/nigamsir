using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Employee.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class employeeprofiledoublechanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "year_of_experience",
                table: "EmployeeProfile",
                type: "double precision",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "year_of_experience",
                table: "EmployeeProfile",
                type: "integer",
                nullable: true,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldNullable: true);
        }
    }
}
