using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RMT.Projects.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class deepeshdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 101L,
                columns: new[] { "ChargableType", "EndDate", "StartDate" },
                values: new object[] { "Chargable", new DateTime(2023, 8, 19, 14, 30, 24, 271, DateTimeKind.Utc).AddTicks(8212), new DateTime(2023, 8, 9, 14, 30, 24, 271, DateTimeKind.Utc).AddTicks(8204) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 102L,
                columns: new[] { "ChargableType", "EndDate", "StartDate" },
                values: new object[] { "Chargable", new DateTime(2023, 8, 19, 14, 30, 24, 271, DateTimeKind.Utc).AddTicks(8219), new DateTime(2023, 8, 9, 14, 30, 24, 271, DateTimeKind.Utc).AddTicks(8218) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 103L,
                columns: new[] { "ChargableType", "EndDate", "StartDate" },
                values: new object[] { "NonChargable", new DateTime(2023, 8, 19, 14, 30, 24, 271, DateTimeKind.Utc).AddTicks(8223), new DateTime(2023, 8, 9, 14, 30, 24, 271, DateTimeKind.Utc).AddTicks(8222) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 104L,
                columns: new[] { "ChargableType", "EndDate", "StartDate" },
                values: new object[] { "NonChargable", new DateTime(2023, 8, 19, 14, 30, 24, 271, DateTimeKind.Utc).AddTicks(8225), new DateTime(2023, 8, 9, 14, 30, 24, 271, DateTimeKind.Utc).AddTicks(8225) });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "BudgetStatus", "ChargableType", "ClientName", "CreatedBy", "Description", "EndDate", "Expertise", "Industry", "IsActive", "Location", "MarketClosed", "ModifiedBy", "PipelineCode", "PipelineName", "PipelineStage", "ProjectAllocationStatus", "ProjectCode", "ProjectFulFilledDemands", "ProjectName", "ProjectType", "RevenueUnit", "Sme", "StartDate" },
                values: new object[,]
                {
                    { 1L, "In Budget", "Chargable", "client name 1", "System data", "project description 1", new DateTime(2023, 8, 19, 14, 30, 24, 271, DateTimeKind.Utc).AddTicks(8413), "expertise 1", "Industry 1", true, "Noida", true, "System Data", "pipe1", "pipeline name 1", "Pending", "PC101", "job3", 0, "Project Name 1", "ProjectType1", "Revenue Unit 1", "sme 1", new DateTime(2023, 7, 20, 14, 30, 24, 271, DateTimeKind.Utc).AddTicks(8412) },
                    { 2L, "In Budget", "NonChargable", "client name 2", "System data", "project description 2", new DateTime(2023, 8, 12, 14, 30, 24, 271, DateTimeKind.Utc).AddTicks(8417), "expertise 2", "Industry 2", true, "Delhi", true, "System Data", "pipe2", "pipeline name 2", "Pending", "PC101", "job2", 0, "Project Name 2", "ProjectType1", "Revenue Unit 2", "sme 2", new DateTime(2023, 7, 30, 14, 30, 24, 271, DateTimeKind.Utc).AddTicks(8417) },
                    { 3L, "In Budget", "NonChargable", "client name 3", "System data", "project description 3", new DateTime(2023, 8, 26, 14, 30, 24, 271, DateTimeKind.Utc).AddTicks(8421), "expertise 3", "Industry 3", true, "Pune", true, "System Data", "pipe3", "pipeline name 3", "Pending", "Pending", "job4", 0, "Project Name 3", "ProjectType1", "Revenue Unit 3", "sme 3", new DateTime(2023, 7, 27, 14, 30, 24, 271, DateTimeKind.Utc).AddTicks(8419) },
                    { 4L, "In Budget", "Chargable", "client name 4", "System data", "project description 5", new DateTime(2023, 8, 28, 14, 30, 24, 271, DateTimeKind.Utc).AddTicks(8424), "expertise 4", "Industry 4", true, "Mumbai", true, "System Data", "pipe4", "pipeline name 4", "Pending", "Pending", "pipe4", 0, "Project Name 4", "ProjectType2", "Revenue Unit 4", "sme 4", new DateTime(2023, 8, 2, 14, 30, 24, 271, DateTimeKind.Utc).AddTicks(8424) },
                    { 5L, "In Budget", "Chargable", "client name 5", "System data", "project description 5", new DateTime(2023, 8, 28, 14, 30, 24, 271, DateTimeKind.Utc).AddTicks(8427), "expertise 5", "Industry 5", true, "Agra", true, "System Data", "pipe5", "pipeline name 5", "Pending", "Pending", "pipe5", 0, "Project Name 5", "ProjectType2", "Revenue Unit 5", "sme 5", new DateTime(2023, 8, 2, 14, 30, 24, 271, DateTimeKind.Utc).AddTicks(8426) },
                    { 6L, "In Budget", "Chargable", "client name 6", "System data", "project description 6", new DateTime(2023, 8, 28, 14, 30, 24, 271, DateTimeKind.Utc).AddTicks(8431), "expertise 6", "Industry 6", true, "Agra", true, "System Data", "pipe6", "pipeline name 6", "Pending", "Pending", "pipe6", 0, "Project Name 6", "ProjectType2", "Revenue Unit 6", "sme 6", new DateTime(2023, 8, 2, 14, 30, 24, 271, DateTimeKind.Utc).AddTicks(8430) }
                });

            migrationBuilder.InsertData(
                table: "ProjectJobCodes",
                columns: new[] { "Id", "CreatedBy", "IsActive", "JobCode", "JobName", "ModifiedBy", "PipelineCode", "ProjectCode", "ProjectId" },
                values: new object[,]
                {
                    { 1L, "System", true, "job1", "Job name1", "System", "pipe1", "Pc1", 1L },
                    { 2L, "System", true, "job2", "Job name2", "System", "pipe2", "Pc2", 2L },
                    { 3L, "System", true, "job3", "Job name3", "System", "pipe1", "Pc1", 1L },
                    { 4L, "System", true, "job4", "Job name4", "System", "pipe3", "Pc3", 3L },
                    { 5L, "System", true, "job5", "Job name5", "System", "pipe6", "Pc6", 6L }
                });

            migrationBuilder.InsertData(
                table: "ProjectRoles",
                columns: new[] { "Id", "CreatedBy", "Description", "IsActive", "ModifiedBy", "ProjectId", "Role", "User", "UserName" },
                values: new object[,]
                {
                    { 1L, "System", null, null, "System", 1L, "EL", "abc@gmail.com", "abc" },
                    { 2L, "System", null, null, "System", 1L, "EO", "bcd@gmail.com", "bcd" },
                    { 3L, "System", null, null, "System", 2L, "EL", "abc@gmail.com", "abc" },
                    { 4L, "System", null, null, "System", 3L, "EL", "abc@gmail.com", "abc" },
                    { 5L, "System", null, null, "System", 4L, "EL", "abc@gmail.com", "abc" },
                    { 6L, "System", null, null, "System", 5L, "EL", "abc@gmail.com", "abc" },
                    { 7L, "System", null, null, "System", 5L, "EL", "bcd@gmail.com", "bcd" },
                    { 8L, "System", null, null, "System", 6L, "EL", "bcd@gmail.com", "bcd" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ProjectJobCodes",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "ProjectJobCodes",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "ProjectJobCodes",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "ProjectJobCodes",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "ProjectJobCodes",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "ProjectRoles",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "ProjectRoles",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "ProjectRoles",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "ProjectRoles",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "ProjectRoles",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "ProjectRoles",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "ProjectRoles",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "ProjectRoles",
                keyColumn: "Id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 101L,
                columns: new[] { "ChargableType", "EndDate", "StartDate" },
                values: new object[] { "PC101", new DateTime(2023, 8, 18, 13, 53, 8, 476, DateTimeKind.Utc).AddTicks(2972), new DateTime(2023, 8, 8, 13, 53, 8, 476, DateTimeKind.Utc).AddTicks(2960) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 102L,
                columns: new[] { "ChargableType", "EndDate", "StartDate" },
                values: new object[] { "PC102", new DateTime(2023, 8, 18, 13, 53, 8, 476, DateTimeKind.Utc).AddTicks(2977), new DateTime(2023, 8, 8, 13, 53, 8, 476, DateTimeKind.Utc).AddTicks(2977) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 103L,
                columns: new[] { "ChargableType", "EndDate", "StartDate" },
                values: new object[] { "PC103", new DateTime(2023, 8, 18, 13, 53, 8, 476, DateTimeKind.Utc).AddTicks(2981), new DateTime(2023, 8, 8, 13, 53, 8, 476, DateTimeKind.Utc).AddTicks(2980) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 104L,
                columns: new[] { "ChargableType", "EndDate", "StartDate" },
                values: new object[] { "PC104", new DateTime(2023, 8, 18, 13, 53, 8, 476, DateTimeKind.Utc).AddTicks(2984), new DateTime(2023, 8, 8, 13, 53, 8, 476, DateTimeKind.Utc).AddTicks(2983) });
        }
    }
}
