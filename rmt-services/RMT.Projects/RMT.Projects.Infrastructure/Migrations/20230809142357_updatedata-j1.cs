using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RMT.Projects.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatedataj1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MasterValues",
                keyColumn: "Id",
                keyValue: 101L);

            migrationBuilder.DeleteData(
                table: "MasterValues",
                keyColumn: "Id",
                keyValue: 102L);

            migrationBuilder.DeleteData(
                table: "MasterValues",
                keyColumn: "Id",
                keyValue: 103L);

            migrationBuilder.DeleteData(
                table: "MasterValues",
                keyColumn: "Id",
                keyValue: 104L);

            migrationBuilder.DeleteData(
                table: "MasterValues",
                keyColumn: "Id",
                keyValue: 105L);

            migrationBuilder.DeleteData(
                table: "MasterValues",
                keyColumn: "Id",
                keyValue: 106L);

            migrationBuilder.DeleteData(
                table: "MasterValues",
                keyColumn: "Id",
                keyValue: 107L);

            migrationBuilder.DeleteData(
                table: "MasterValues",
                keyColumn: "Id",
                keyValue: 108L);

            migrationBuilder.DeleteData(
                table: "MasterValues",
                keyColumn: "Id",
                keyValue: 109L);

            migrationBuilder.DeleteData(
                table: "MasterValues",
                keyColumn: "Id",
                keyValue: 110L);

            migrationBuilder.DeleteData(
                table: "MasterValues",
                keyColumn: "Id",
                keyValue: 111L);

            migrationBuilder.DeleteData(
                table: "MasterValues",
                keyColumn: "Id",
                keyValue: 112L);

            migrationBuilder.DeleteData(
                table: "MasterValues",
                keyColumn: "Id",
                keyValue: 113L);

            migrationBuilder.DeleteData(
                table: "MasterValues",
                keyColumn: "Id",
                keyValue: 114L);

            migrationBuilder.DeleteData(
                table: "MasterValues",
                keyColumn: "Id",
                keyValue: 115L);

            migrationBuilder.DeleteData(
                table: "MasterValues",
                keyColumn: "Id",
                keyValue: 116L);

            migrationBuilder.DeleteData(
                table: "MasterValues",
                keyColumn: "Id",
                keyValue: 117L);

            migrationBuilder.DeleteData(
                table: "MasterValues",
                keyColumn: "Id",
                keyValue: 118L);

            migrationBuilder.InsertData(
                table: "MasterValues",
                columns: new[] { "Id", "IsActive", "RecordDisplayName", "RecordInternalName", "RecordType" },
                values: new object[,]
                {
                    { 201L, true, "Rupesh Kumar", "Rupesh@email.com", "Delegate" },
                    { 202L, true, "Abhijit Singh", "Abhijit@email.com", "Delegate" },
                    { 203L, true, "Sakshi Joshi", "Sakshi@email.com", "Delegate" },
                    { 204L, true, "Consultant", "Consultant", "Designation" },
                    { 205L, true, "Sr Consultant", "SrConsultant", "Designation" },
                    { 206L, true, "Manager", "Manager", "Designation" },
                    { 207L, true, "Sakshi Joshi", "Sakshi@email.com", "EngagementLeader" },
                    { 208L, true, "Puja Sinha", "Puja@email.com", "EngagementLeader" },
                    { 209L, true, "Abhijit Singh", "Abhijit@email.com", "EngagementLeader" },
                    { 210L, true, "Expertise1", "Expertise1", "Expertise" },
                    { 211L, true, "Expertise2", "Expertise2", "Expertise" },
                    { 212L, true, "Expertise3", "Expertise3", "Expertise" },
                    { 213L, true, "Auditing", "Auditing", "Skill" },
                    { 214L, true, "Taxation", "Taxation", "Skill" },
                    { 215L, true, "Consulting", "Consulting", "Skill" },
                    { 216L, true, "SME1", "SME1", "SME" },
                    { 217L, true, "SME2", "SME2", "SME" }
                });

            migrationBuilder.UpdateData(
                table: "ProjectRoles",
                keyColumn: "Id",
                keyValue: 113L,
                column: "Role",
                value: "EngagementLeader");

            migrationBuilder.UpdateData(
                table: "ProjectRoles",
                keyColumn: "Id",
                keyValue: 116L,
                column: "Role",
                value: "EngagementLeader");

            migrationBuilder.UpdateData(
                table: "ProjectRoles",
                keyColumn: "Id",
                keyValue: 119L,
                column: "Role",
                value: "EngagementLeader");

            migrationBuilder.UpdateData(
                table: "ProjectRoles",
                keyColumn: "Id",
                keyValue: 122L,
                column: "Role",
                value: "EngagementLeader");

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 101L,
                columns: new[] { "CreatedDate", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 8, 14, 14, 23, 57, 492, DateTimeKind.Utc).AddTicks(207), new DateTime(2023, 8, 24, 14, 23, 57, 492, DateTimeKind.Utc).AddTicks(203), new DateTime(2023, 8, 14, 14, 23, 57, 492, DateTimeKind.Utc).AddTicks(196) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 102L,
                columns: new[] { "CreatedDate", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 8, 14, 14, 23, 57, 492, DateTimeKind.Utc).AddTicks(253), new DateTime(2023, 8, 24, 14, 23, 57, 492, DateTimeKind.Utc).AddTicks(249), new DateTime(2023, 8, 14, 14, 23, 57, 492, DateTimeKind.Utc).AddTicks(249) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 103L,
                columns: new[] { "CreatedDate", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 8, 14, 14, 23, 57, 492, DateTimeKind.Utc).AddTicks(258), new DateTime(2023, 8, 24, 14, 23, 57, 492, DateTimeKind.Utc).AddTicks(256), new DateTime(2023, 8, 14, 14, 23, 57, 492, DateTimeKind.Utc).AddTicks(256) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 104L,
                columns: new[] { "CreatedDate", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 8, 9, 14, 23, 57, 492, DateTimeKind.Utc).AddTicks(263), new DateTime(2023, 8, 24, 14, 23, 57, 492, DateTimeKind.Utc).AddTicks(261), new DateTime(2023, 8, 14, 14, 23, 57, 492, DateTimeKind.Utc).AddTicks(261) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MasterValues",
                keyColumn: "Id",
                keyValue: 201L);

            migrationBuilder.DeleteData(
                table: "MasterValues",
                keyColumn: "Id",
                keyValue: 202L);

            migrationBuilder.DeleteData(
                table: "MasterValues",
                keyColumn: "Id",
                keyValue: 203L);

            migrationBuilder.DeleteData(
                table: "MasterValues",
                keyColumn: "Id",
                keyValue: 204L);

            migrationBuilder.DeleteData(
                table: "MasterValues",
                keyColumn: "Id",
                keyValue: 205L);

            migrationBuilder.DeleteData(
                table: "MasterValues",
                keyColumn: "Id",
                keyValue: 206L);

            migrationBuilder.DeleteData(
                table: "MasterValues",
                keyColumn: "Id",
                keyValue: 207L);

            migrationBuilder.DeleteData(
                table: "MasterValues",
                keyColumn: "Id",
                keyValue: 208L);

            migrationBuilder.DeleteData(
                table: "MasterValues",
                keyColumn: "Id",
                keyValue: 209L);

            migrationBuilder.DeleteData(
                table: "MasterValues",
                keyColumn: "Id",
                keyValue: 210L);

            migrationBuilder.DeleteData(
                table: "MasterValues",
                keyColumn: "Id",
                keyValue: 211L);

            migrationBuilder.DeleteData(
                table: "MasterValues",
                keyColumn: "Id",
                keyValue: 212L);

            migrationBuilder.DeleteData(
                table: "MasterValues",
                keyColumn: "Id",
                keyValue: 213L);

            migrationBuilder.DeleteData(
                table: "MasterValues",
                keyColumn: "Id",
                keyValue: 214L);

            migrationBuilder.DeleteData(
                table: "MasterValues",
                keyColumn: "Id",
                keyValue: 215L);

            migrationBuilder.DeleteData(
                table: "MasterValues",
                keyColumn: "Id",
                keyValue: 216L);

            migrationBuilder.DeleteData(
                table: "MasterValues",
                keyColumn: "Id",
                keyValue: 217L);

            migrationBuilder.InsertData(
                table: "MasterValues",
                columns: new[] { "Id", "IsActive", "RecordDisplayName", "RecordInternalName", "RecordType" },
                values: new object[,]
                {
                    { 101L, true, "User1", "User1@email.com", "Delegate" },
                    { 102L, true, "User2", "User2@email.com", "Delegate" },
                    { 103L, true, "User3", "User3@email.com", "Delegate" },
                    { 104L, true, "Consultant", "Consultant", "Designation" },
                    { 105L, true, "Sr Consultant", "SrConsultant", "Designation" },
                    { 106L, true, "Manager", "Manager", "Designation" },
                    { 107L, true, "EL1", "EL1@email.com", "EngagementLeader" },
                    { 108L, true, "EL2", "EL2@email.com", "EngagementLeader" },
                    { 109L, true, "EL3", "EL3@email.com", "EngagementLeader" },
                    { 110L, true, "Expertise1", "Expertise1", "Expertise" },
                    { 111L, true, "Expertise2", "Expertise2", "Expertise" },
                    { 112L, true, "Expertise3", "Expertise3", "Expertise" },
                    { 113L, true, "Auditing", "Auditing", "Skill" },
                    { 114L, true, "Taxation", "Taxation", "Skill" },
                    { 115L, true, "Consulting", "Consulting", "Skill" },
                    { 116L, true, "SME1", "SME1", "SME" },
                    { 117L, true, "SME2", "SME2", "SME" },
                    { 118L, true, "SME3", "SME3", "SME" }
                });

            migrationBuilder.UpdateData(
                table: "ProjectRoles",
                keyColumn: "Id",
                keyValue: 113L,
                column: "Role",
                value: "EL");

            migrationBuilder.UpdateData(
                table: "ProjectRoles",
                keyColumn: "Id",
                keyValue: 116L,
                column: "Role",
                value: "EL");

            migrationBuilder.UpdateData(
                table: "ProjectRoles",
                keyColumn: "Id",
                keyValue: 119L,
                column: "Role",
                value: "EL");

            migrationBuilder.UpdateData(
                table: "ProjectRoles",
                keyColumn: "Id",
                keyValue: 122L,
                column: "Role",
                value: "EL");

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 101L,
                columns: new[] { "CreatedDate", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 8, 14, 8, 52, 39, 439, DateTimeKind.Utc).AddTicks(2373), new DateTime(2023, 8, 24, 8, 52, 39, 439, DateTimeKind.Utc).AddTicks(2369), new DateTime(2023, 8, 14, 8, 52, 39, 439, DateTimeKind.Utc).AddTicks(2364) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 102L,
                columns: new[] { "CreatedDate", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 8, 14, 8, 52, 39, 439, DateTimeKind.Utc).AddTicks(2379), new DateTime(2023, 8, 24, 8, 52, 39, 439, DateTimeKind.Utc).AddTicks(2377), new DateTime(2023, 8, 14, 8, 52, 39, 439, DateTimeKind.Utc).AddTicks(2377) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 103L,
                columns: new[] { "CreatedDate", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 8, 14, 8, 52, 39, 439, DateTimeKind.Utc).AddTicks(2383), new DateTime(2023, 8, 24, 8, 52, 39, 439, DateTimeKind.Utc).AddTicks(2382), new DateTime(2023, 8, 14, 8, 52, 39, 439, DateTimeKind.Utc).AddTicks(2381) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 104L,
                columns: new[] { "CreatedDate", "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 8, 14, 8, 52, 39, 439, DateTimeKind.Utc).AddTicks(2387), new DateTime(2023, 8, 24, 8, 52, 39, 439, DateTimeKind.Utc).AddTicks(2385), new DateTime(2023, 8, 14, 8, 52, 39, 439, DateTimeKind.Utc).AddTicks(2385) });
        }
    }
}
