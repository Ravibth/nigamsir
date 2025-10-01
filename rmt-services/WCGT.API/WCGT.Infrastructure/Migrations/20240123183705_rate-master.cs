using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WCGT.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ratemaster : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<double>(
            //    name: "timesheetcost",
            //    table: "sp_resource_timesheet_view",
            //    type: "double precision",
            //    nullable: true);

            migrationBuilder.CreateTable(
                name: "RateDesignationMaster",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Designation = table.Column<string>(type: "text", nullable: false),
                    RatePerDay = table.Column<double>(type: "double precision", nullable: false),
                    isactive = table.Column<bool>(type: "boolean", nullable: true),
                    createdat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    modifiedat = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    createdby = table.Column<string>(type: "text", nullable: true),
                    modifiedby = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RateDesignationMaster", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "RateDesignationMaster",
                columns: new[] { "id", "Designation", "RatePerDay", "createdat", "createdby", "isactive", "modifiedat", "modifiedby" },
                values: new object[,]
                {
                    { 1L, "Consultant", 3000.0, new DateTime(2023, 1, 23, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 1, 23, 0, 0, 0, 0, DateTimeKind.Utc), "System" },
                    { 2L, "Engineer", 5000.0, new DateTime(2023, 1, 23, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 1, 23, 0, 0, 0, 0, DateTimeKind.Utc), "System" },
                    { 3L, "Manager", 8000.0, new DateTime(2023, 1, 23, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 1, 23, 0, 0, 0, 0, DateTimeKind.Utc), "System" },
                    { 4L, "Senior Manager", 10000.0, new DateTime(2023, 1, 23, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 1, 23, 0, 0, 0, 0, DateTimeKind.Utc), "System" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RateDesignationMaster");

            //migrationBuilder.DropColumn(
            //    name: "timesheetcost",
            //    table: "sp_resource_timesheet_view");
        }
    }
}
