using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RMT.Projects.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "BudgetStatus", "ChargableType", "ClientName", "CreatedBy", "Description", "EndDate", "Expertise", "Industry", "IsActive", "Location", "MarketClosed", "ModifiedBy", "PipelineCode", "PipelineName", "PipelineStage", "ProjectAllocationStatus", "ProjectCode", "ProjectFulFilledDemands", "ProjectName", "ProjectType", "RevenueUnit", "Sme", "StartDate" },
                values: new object[,]
                {
                    { 101L, "PC101", "PC101", "PC101", "System data", "PC101", new DateTime(2023, 8, 18, 13, 53, 8, 476, DateTimeKind.Utc).AddTicks(2972), "PC101", "PC101", true, "PC101", true, "System Data", "PC101", "PC101", "PC101", "PC101", "PC101", 0, "PC101", "PC101", "PC101", "PC101", new DateTime(2023, 8, 8, 13, 53, 8, 476, DateTimeKind.Utc).AddTicks(2960) },
                    { 102L, "PC102", "PC102", "PC102", "System data", "PC102", new DateTime(2023, 8, 18, 13, 53, 8, 476, DateTimeKind.Utc).AddTicks(2977), "PC102", "PC102", true, "PC102", true, "System Data", "PC102", "PC102", "PC102", "PC102", "PC102", 0, "PC102", "PC102", "PC102", "PC102", new DateTime(2023, 8, 8, 13, 53, 8, 476, DateTimeKind.Utc).AddTicks(2977) },
                    { 103L, "PC103", "PC103", "PC103", "System data", "PC103", new DateTime(2023, 8, 18, 13, 53, 8, 476, DateTimeKind.Utc).AddTicks(2981), "PC103", "PC103", true, "PC103", true, "System Data", "PC103", "PC103", "PC103", "PC103", "PC103", 0, "PC103", "PC103", "PC103", "PC103", new DateTime(2023, 8, 8, 13, 53, 8, 476, DateTimeKind.Utc).AddTicks(2980) },
                    { 104L, "PC104", "PC104", "PC104", "System data", "PC104", new DateTime(2023, 8, 18, 13, 53, 8, 476, DateTimeKind.Utc).AddTicks(2984), "PC104", "PC104", true, "PC104", true, "System Data", "PC104", "PC104", "PC104", "PC104", "PC104", 0, "PC104", "PC104", "PC104", "PC104", new DateTime(2023, 8, 8, 13, 53, 8, 476, DateTimeKind.Utc).AddTicks(2983) }
                });

            migrationBuilder.InsertData(
                table: "ProjectDemands",
                columns: new[] { "Id", "CreatedBy", "Designation", "IsActive", "ModifiedBy", "NoOfResources", "ProjectId" },
                values: new object[,]
                {
                    { 111L, null, "Consultant", true, null, 2, 101L },
                    { 112L, null, "Sr Consultant", true, null, 3, 101L },
                    { 113L, null, "Project Consultant", true, null, 1, 101L },
                    { 114L, null, "Manager ", true, null, 1, 101L },
                    { 115L, null, "Consultant", true, null, 2, 102L },
                    { 116L, null, "Sr Consultant", true, null, 3, 102L },
                    { 117L, null, "Project Consultant", true, null, 1, 102L },
                    { 118L, null, "Manager ", true, null, 1, 102L },
                    { 119L, null, "Consultant", true, null, 2, 103L },
                    { 120L, null, "Sr Consultant", true, null, 3, 103L },
                    { 121L, null, "Project Consultant", true, null, 1, 103L },
                    { 122L, null, "Manager ", true, null, 1, 103L },
                    { 123L, null, "Consultant", true, null, 2, 104L },
                    { 124L, null, "Sr Consultant", true, null, 3, 104L },
                    { 125L, null, "Project Consultant", true, null, 1, 104L },
                    { 126L, null, "Manager ", true, null, 1, 104L }
                });

            migrationBuilder.InsertData(
                table: "ProjectJobCodes",
                columns: new[] { "Id", "CreatedBy", "IsActive", "JobCode", "JobName", "ModifiedBy", "PipelineCode", "ProjectCode", "ProjectId" },
                values: new object[,]
                {
                    { 111L, "System", true, "JC101", "Job Name101", "System", "PC101", "PC101", 101L },
                    { 112L, "System", true, "JC102", "Job Name102", "System", "PC102", "PC102", 102L },
                    { 113L, "System", true, "JC103", "Job Name103", "System", "PC103", "PC103", 103L },
                    { 114L, "System", true, "JC104", "Job Name104", "System", "PC104", "PC104", 104L }
                });

            migrationBuilder.InsertData(
                table: "ProjectRoles",
                columns: new[] { "Id", "CreatedBy", "Description", "IsActive", "ModifiedBy", "ProjectId", "Role", "User", "UserName" },
                values: new object[,]
                {
                    { 111L, "System", null, true, "System", 101L, "Requestor", "abc@gmail.com", "abc" },
                    { 112L, "System", null, true, "System", 101L, "DelageteUser", "xyz@gmail.com", "xyz" },
                    { 113L, "System", null, true, "System", 101L, "EL", "qqq@gmail.com", "qqq" },
                    { 114L, "System", null, true, "System", 102L, "Requestor", "abc@gmail.com", "abc" },
                    { 115L, "System", null, true, "System", 102L, "DelageteUser", "xyz@gmail.com", "xyz" },
                    { 116L, "System", null, true, "System", 102L, "EL", "qqq@gmail.com", "qqq" },
                    { 117L, "System", null, true, "System", 103L, "Requestor", "abc@gmail.com", "abc" },
                    { 118L, "System", null, true, "System", 103L, "DelageteUser", "xyz@gmail.com", "xyz" },
                    { 119L, "System", null, true, "System", 103L, "EL", "qqq@gmail.com", "qqq" },
                    { 120L, "System", null, true, "System", 104L, "Requestor", "abc@gmail.com", "abc" },
                    { 121L, "System", null, true, "System", 104L, "DelageteUser", "xyz@gmail.com", "xyz" },
                    { 122L, "System", null, true, "System", 104L, "EL", "qqq@gmail.com", "qqq" }
                });

            migrationBuilder.InsertData(
                table: "ProjectSkills",
                columns: new[] { "Id", "CreatedBy", "IsActive", "ModifiedBy", "ProjectId", "SkillName" },
                values: new object[,]
                {
                    { 111L, "System", true, "System", 101L, "Consulting" },
                    { 112L, "System", true, "System", 101L, "Auditing" },
                    { 113L, "System", true, "System", 101L, "Taxation" },
                    { 114L, "System", true, "System", 102L, "Consulting" },
                    { 115L, "System", true, "System", 102L, "Auditing" },
                    { 116L, "System", true, "System", 102L, "Taxation" },
                    { 117L, "System", true, "System", 103L, "Consulting" },
                    { 118L, "System", true, "System", 103L, "Auditing" },
                    { 119L, "System", true, "System", 103L, "Taxation" },
                    { 120L, "System", true, "System", 104L, "Consulting" },
                    { 121L, "System", true, "System", 104L, "Auditing" },
                    { 122L, "System", true, "System", 104L, "Taxation" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DeleteData(
                table: "ProjectDemands",
                keyColumn: "Id",
                keyValue: 111L);

            migrationBuilder.DeleteData(
                table: "ProjectDemands",
                keyColumn: "Id",
                keyValue: 112L);

            migrationBuilder.DeleteData(
                table: "ProjectDemands",
                keyColumn: "Id",
                keyValue: 113L);

            migrationBuilder.DeleteData(
                table: "ProjectDemands",
                keyColumn: "Id",
                keyValue: 114L);

            migrationBuilder.DeleteData(
                table: "ProjectDemands",
                keyColumn: "Id",
                keyValue: 115L);

            migrationBuilder.DeleteData(
                table: "ProjectDemands",
                keyColumn: "Id",
                keyValue: 116L);

            migrationBuilder.DeleteData(
                table: "ProjectDemands",
                keyColumn: "Id",
                keyValue: 117L);

            migrationBuilder.DeleteData(
                table: "ProjectDemands",
                keyColumn: "Id",
                keyValue: 118L);

            migrationBuilder.DeleteData(
                table: "ProjectDemands",
                keyColumn: "Id",
                keyValue: 119L);

            migrationBuilder.DeleteData(
                table: "ProjectDemands",
                keyColumn: "Id",
                keyValue: 120L);

            migrationBuilder.DeleteData(
                table: "ProjectDemands",
                keyColumn: "Id",
                keyValue: 121L);

            migrationBuilder.DeleteData(
                table: "ProjectDemands",
                keyColumn: "Id",
                keyValue: 122L);

            migrationBuilder.DeleteData(
                table: "ProjectDemands",
                keyColumn: "Id",
                keyValue: 123L);

            migrationBuilder.DeleteData(
                table: "ProjectDemands",
                keyColumn: "Id",
                keyValue: 124L);

            migrationBuilder.DeleteData(
                table: "ProjectDemands",
                keyColumn: "Id",
                keyValue: 125L);

            migrationBuilder.DeleteData(
                table: "ProjectDemands",
                keyColumn: "Id",
                keyValue: 126L);

            migrationBuilder.DeleteData(
                table: "ProjectJobCodes",
                keyColumn: "Id",
                keyValue: 111L);

            migrationBuilder.DeleteData(
                table: "ProjectJobCodes",
                keyColumn: "Id",
                keyValue: 112L);

            migrationBuilder.DeleteData(
                table: "ProjectJobCodes",
                keyColumn: "Id",
                keyValue: 113L);

            migrationBuilder.DeleteData(
                table: "ProjectJobCodes",
                keyColumn: "Id",
                keyValue: 114L);

            migrationBuilder.DeleteData(
                table: "ProjectRoles",
                keyColumn: "Id",
                keyValue: 111L);

            migrationBuilder.DeleteData(
                table: "ProjectRoles",
                keyColumn: "Id",
                keyValue: 112L);

            migrationBuilder.DeleteData(
                table: "ProjectRoles",
                keyColumn: "Id",
                keyValue: 113L);

            migrationBuilder.DeleteData(
                table: "ProjectRoles",
                keyColumn: "Id",
                keyValue: 114L);

            migrationBuilder.DeleteData(
                table: "ProjectRoles",
                keyColumn: "Id",
                keyValue: 115L);

            migrationBuilder.DeleteData(
                table: "ProjectRoles",
                keyColumn: "Id",
                keyValue: 116L);

            migrationBuilder.DeleteData(
                table: "ProjectRoles",
                keyColumn: "Id",
                keyValue: 117L);

            migrationBuilder.DeleteData(
                table: "ProjectRoles",
                keyColumn: "Id",
                keyValue: 118L);

            migrationBuilder.DeleteData(
                table: "ProjectRoles",
                keyColumn: "Id",
                keyValue: 119L);

            migrationBuilder.DeleteData(
                table: "ProjectRoles",
                keyColumn: "Id",
                keyValue: 120L);

            migrationBuilder.DeleteData(
                table: "ProjectRoles",
                keyColumn: "Id",
                keyValue: 121L);

            migrationBuilder.DeleteData(
                table: "ProjectRoles",
                keyColumn: "Id",
                keyValue: 122L);

            migrationBuilder.DeleteData(
                table: "ProjectSkills",
                keyColumn: "Id",
                keyValue: 111L);

            migrationBuilder.DeleteData(
                table: "ProjectSkills",
                keyColumn: "Id",
                keyValue: 112L);

            migrationBuilder.DeleteData(
                table: "ProjectSkills",
                keyColumn: "Id",
                keyValue: 113L);

            migrationBuilder.DeleteData(
                table: "ProjectSkills",
                keyColumn: "Id",
                keyValue: 114L);

            migrationBuilder.DeleteData(
                table: "ProjectSkills",
                keyColumn: "Id",
                keyValue: 115L);

            migrationBuilder.DeleteData(
                table: "ProjectSkills",
                keyColumn: "Id",
                keyValue: 116L);

            migrationBuilder.DeleteData(
                table: "ProjectSkills",
                keyColumn: "Id",
                keyValue: 117L);

            migrationBuilder.DeleteData(
                table: "ProjectSkills",
                keyColumn: "Id",
                keyValue: 118L);

            migrationBuilder.DeleteData(
                table: "ProjectSkills",
                keyColumn: "Id",
                keyValue: 119L);

            migrationBuilder.DeleteData(
                table: "ProjectSkills",
                keyColumn: "Id",
                keyValue: 120L);

            migrationBuilder.DeleteData(
                table: "ProjectSkills",
                keyColumn: "Id",
                keyValue: 121L);

            migrationBuilder.DeleteData(
                table: "ProjectSkills",
                keyColumn: "Id",
                keyValue: 122L);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 101L);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 102L);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 103L);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 104L);
        }
    }
}
