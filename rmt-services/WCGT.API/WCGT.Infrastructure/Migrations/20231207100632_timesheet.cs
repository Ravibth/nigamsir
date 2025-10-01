using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace WCGT.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class timesheet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Timesheet",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    employeecode = table.Column<string>(type: "text", nullable: false),
                    employeename = table.Column<string>(type: "text", nullable: false),
                    datelog = table.Column<DateOnly>(type: "date", nullable: false),
                    totaltime = table.Column<double>(type: "double precision", nullable: false),
                    client = table.Column<string>(type: "text", nullable: false),
                    jobcode = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<string>(type: "text", nullable: false),
                    designation = table.Column<string>(type: "text", nullable: false),
                    gradename = table.Column<string>(type: "text", nullable: false),
                    chargeableflag = table.Column<string>(type: "text", nullable: false),
                    rate = table.Column<double>(type: "double precision", nullable: false),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modifiedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    createdby = table.Column<string>(type: "text", nullable: true),
                    modifiedby = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Timesheet", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Timesheet");
        }
    }
}
