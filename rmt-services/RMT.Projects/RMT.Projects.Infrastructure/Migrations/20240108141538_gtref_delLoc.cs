using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RMT.Projects.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class gtref_delLoc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "FieldForMarketPlaces",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "FieldForMarketPlaces",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "FieldForMarketPlaces",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "FieldForMarketPlaces",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "FieldForMarketPlaces",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "MasterValues",
                keyColumn: "Id",
                keyValue: 301L);

            migrationBuilder.DeleteData(
                table: "MasterValues",
                keyColumn: "Id",
                keyValue: 302L);

            migrationBuilder.DeleteData(
                table: "MasterValues",
                keyColumn: "Id",
                keyValue: 303L);

            migrationBuilder.DeleteData(
                table: "MasterValues",
                keyColumn: "Id",
                keyValue: 304L);

            migrationBuilder.DeleteData(
                table: "MasterValues",
                keyColumn: "Id",
                keyValue: 305L);

            migrationBuilder.DeleteData(
                table: "MasterValues",
                keyColumn: "Id",
                keyValue: 306L);

            migrationBuilder.DeleteData(
                table: "MasterValues",
                keyColumn: "Id",
                keyValue: 307L);

            migrationBuilder.DeleteData(
                table: "MasterValues",
                keyColumn: "Id",
                keyValue: 308L);

            migrationBuilder.DeleteData(
                table: "MasterValues",
                keyColumn: "Id",
                keyValue: 309L);

            migrationBuilder.DeleteData(
                table: "MasterValues",
                keyColumn: "Id",
                keyValue: 310L);

            migrationBuilder.DeleteData(
                table: "MasterValues",
                keyColumn: "Id",
                keyValue: 311L);

            migrationBuilder.DeleteData(
                table: "MasterValues",
                keyColumn: "Id",
                keyValue: 312L);

            migrationBuilder.DeleteData(
                table: "MasterValues",
                keyColumn: "Id",
                keyValue: 313L);

            migrationBuilder.DeleteData(
                table: "MasterValues",
                keyColumn: "Id",
                keyValue: 314L);

            migrationBuilder.DeleteData(
                table: "MasterValues",
                keyColumn: "Id",
                keyValue: 315L);

            migrationBuilder.DeleteData(
                table: "MasterValues",
                keyColumn: "Id",
                keyValue: 316L);

            migrationBuilder.DeleteData(
                table: "MasterValues",
                keyColumn: "Id",
                keyValue: 317L);

            migrationBuilder.DeleteData(
                table: "ProjectJobCodes",
                keyColumn: "Id",
                keyValue: 211L);

            migrationBuilder.DeleteData(
                table: "ProjectJobCodes",
                keyColumn: "Id",
                keyValue: 212L);

            migrationBuilder.DeleteData(
                table: "ProjectJobCodes",
                keyColumn: "Id",
                keyValue: 213L);

            migrationBuilder.DeleteData(
                table: "ProjectJobCodes",
                keyColumn: "Id",
                keyValue: 214L);

            migrationBuilder.DeleteData(
                table: "ProjectJobCodes",
                keyColumn: "Id",
                keyValue: 215L);

            migrationBuilder.DeleteData(
                table: "ProjectJobCodes",
                keyColumn: "Id",
                keyValue: 216L);

            migrationBuilder.DeleteData(
                table: "ProjectRoles",
                keyColumn: "Id",
                keyValue: 211L);

            migrationBuilder.DeleteData(
                table: "ProjectRoles",
                keyColumn: "Id",
                keyValue: 212L);

            migrationBuilder.DeleteData(
                table: "ProjectRoles",
                keyColumn: "Id",
                keyValue: 213L);

            migrationBuilder.DeleteData(
                table: "ProjectRoles",
                keyColumn: "Id",
                keyValue: 214L);

            migrationBuilder.DeleteData(
                table: "ProjectRoles",
                keyColumn: "Id",
                keyValue: 215L);

            migrationBuilder.DeleteData(
                table: "ProjectRoles",
                keyColumn: "Id",
                keyValue: 216L);

            migrationBuilder.DeleteData(
                table: "ProjectRoles",
                keyColumn: "Id",
                keyValue: 217L);

            migrationBuilder.DeleteData(
                table: "ProjectRoles",
                keyColumn: "Id",
                keyValue: 218L);

            migrationBuilder.DeleteData(
                table: "ProjectRoles",
                keyColumn: "Id",
                keyValue: 219L);

            migrationBuilder.DeleteData(
                table: "ProjectRoles",
                keyColumn: "Id",
                keyValue: 220L);

            migrationBuilder.DeleteData(
                table: "ProjectRoles",
                keyColumn: "Id",
                keyValue: 221L);

            migrationBuilder.DeleteData(
                table: "ProjectRoles",
                keyColumn: "Id",
                keyValue: 222L);

            migrationBuilder.DeleteData(
                table: "ProjectRoles",
                keyColumn: "Id",
                keyValue: 223L);

            migrationBuilder.DeleteData(
                table: "ProjectRoles",
                keyColumn: "Id",
                keyValue: 224L);

            migrationBuilder.DeleteData(
                table: "ProjectRoles",
                keyColumn: "Id",
                keyValue: 225L);

            migrationBuilder.DeleteData(
                table: "ProjectRoles",
                keyColumn: "Id",
                keyValue: 226L);

            migrationBuilder.DeleteData(
                table: "ProjectRoles",
                keyColumn: "Id",
                keyValue: 227L);

            migrationBuilder.DeleteData(
                table: "ProjectRoles",
                keyColumn: "Id",
                keyValue: 228L);

            migrationBuilder.DeleteData(
                table: "ProjectRoles",
                keyColumn: "Id",
                keyValue: 229L);

            migrationBuilder.DeleteData(
                table: "ProjectRoles",
                keyColumn: "Id",
                keyValue: 230L);

            migrationBuilder.DeleteData(
                table: "ProjectRoles",
                keyColumn: "Id",
                keyValue: 231L);

            migrationBuilder.DeleteData(
                table: "ProjectRoles",
                keyColumn: "Id",
                keyValue: 232L);

            migrationBuilder.DeleteData(
                table: "ProjectRoles",
                keyColumn: "Id",
                keyValue: 233L);

            migrationBuilder.DeleteData(
                table: "ProjectRoles",
                keyColumn: "Id",
                keyValue: 234L);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 201L);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 202L);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 203L);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 204L);

            migrationBuilder.DeleteData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 205L);

            migrationBuilder.AddColumn<string>(
                name: "DeliveryLocation",
                table: "Projects",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GtRefCo",
                table: "Projects",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "JobLocation",
                table: "Projects",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LegalEntity",
                table: "Projects",
                type: "text",
                nullable: true);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProjectBudget");

            migrationBuilder.DropColumn(
                name: "DeliveryLocation",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "GtRefCo",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "JobLocation",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "LegalEntity",
                table: "Projects");

            migrationBuilder.InsertData(
                table: "FieldForMarketPlaces",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "DisplayName", "InternalName", "IsActive", "ModifiedAt", "ModifiedBy" },
                values: new object[,]
                {
                    { 1L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "Project Name", "projectName", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System" },
                    { 2L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "Client Name", "clientName", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System" },
                    { 3L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "Client Group", "clientGroup", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System" },
                    { 4L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "Project ID", "projectID", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System" },
                    { 5L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "string1", "string1", false, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System" }
                });

            migrationBuilder.InsertData(
                table: "MasterValues",
                columns: new[] { "Id", "IsActive", "RecordDisplayName", "RecordInternalName", "RecordType" },
                values: new object[,]
                {
                    { 301L, true, "Rupesh Kumar", "Rupesh@email.com", "Delegate" },
                    { 302L, true, "Abhijit Singh", "Abhijit@email.com", "Delegate" },
                    { 303L, true, "Sakshi Joshi", "Sakshi@email.com", "Delegate" },
                    { 304L, true, "Consultant", "Consultant", "Designation" },
                    { 305L, true, "Sr Consultant", "SrConsultant", "Designation" },
                    { 306L, true, "Manager", "Manager", "Designation" },
                    { 307L, true, "Sakshi Joshi", "Sakshi@email.com", "EngagementLeader" },
                    { 308L, true, "Puja Sinha", "Puja@email.com", "EngagementLeader" },
                    { 309L, true, "Abhijit Singh", "Abhijit@email.com", "EngagementLeader" },
                    { 310L, true, "Expertise1", "Expertise1", "Expertise" },
                    { 311L, true, "Expertise2", "Expertise2", "Expertise" },
                    { 312L, true, "Expertise3", "Expertise3", "Expertise" },
                    { 313L, true, "Auditing", "Auditing", "Skill" },
                    { 314L, true, "Taxation", "Taxation", "Skill" },
                    { 315L, true, "Consulting", "Consulting", "Skill" },
                    { 316L, true, "SME1", "SME1", "SME" },
                    { 317L, true, "SME2", "SME2", "SME" }
                });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "BudgetStatus", "ChargableType", "ClientGroup", "ClientName", "CreatedAt", "CreatedBy", "CreatedDate", "Description", "EndDate", "Expertise", "Industry", "IsActive", "IsPublishedToMarketPlace", "IsRequisitionCreationallowed", "IsRollover", "IsSuspended", "Location", "ModifiedAt", "ModifiedBy", "PipelineCode", "PipelineName", "PipelineStage", "PipelineStatus", "ProjectAllocationStatus", "ProjectCode", "ProjectFulFilledDemands", "ProjectName", "ProjectType", "RevenueUnit", "RolloverDays", "Sme", "StartDate", "Subindustry", "SuspendedModifyAt", "bu", "sector" },
                values: new object[,]
                {
                    { 201L, "In Budget", "Chargable", "ClientGrpName1", "Adani Enterprises Ltd", new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", null, "Job Description: This project is for Global Audit delivery of GOA CRM Testing Client. This position is responsible for sales tax setup, preparation, and management as well as preparing quarterly and annual income tax estimate payment calculations. This is the ideal role for someone who prioritizes accuracy and efficiency, has advanced Excel skills, and values strong internal controls-updated, update by testing", new DateTime(2023, 12, 30, 11, 44, 42, 586, DateTimeKind.Local).AddTicks(6280), "Audit-PIE", "Auto and Auto Component", true, false, true, false, null, "Mumbai", new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "PC101", "ABS Inc US GAAP Audit", "Approved", null, "ALLOCATION_COMPLETED", "PC101", 0, "ABS Inc US GAAP Audit", "Non-recurring", "Tax Audit", 0, "Attest Service-PIE", new DateTime(2023, 10, 3, 11, 44, 42, 586, DateTimeKind.Local).AddTicks(6260), "Food distributorsWON", null, ",sector= ", null },
                    { 202L, "In Budget", "Chargable", "ClientGrpName1", "Tata consultancy services", new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", null, "Job Description: This project is for  Global Audit delivery of GOA CRM Testing Client. This position is responsible for sales tax setup, preparation, and management as well as preparing quarterly and annual income tax estimate payment calculations. This is the ideal role for someone who prioritizes accuracy and efficiency, has advanced Excel skills, and values strong internal controls.", new DateTime(2023, 10, 29, 11, 44, 42, 586, DateTimeKind.Local).AddTicks(6280), "Audit-PC", "Auto and Auto Component", true, false, true, false, false, "New delhi", new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "PC102", "GOA CRM Testing Client", "Pending", null, "ALLOCATION_COMPLETED", "PC102", 0, "GOA CRM Testing Client", "Recurring", "TaxAudit", 0, "Attest Services-PIE", new DateTime(2023, 8, 1, 0, 0, 0, 0, DateTimeKind.Local), "Airlines and miscellaneous non-rail transportationWON", new DateTime(2023, 11, 30, 0, 44, 0, 278, DateTimeKind.Local).AddTicks(4850), "Bu1", "Sector1" },
                    { 203L, "In Budget", "NonChargable", "ClientGrpName1", "Philips ", new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", null, "Job Description: This project is for  Global Audit delivery of NC Direct Tax Analysis. This position is responsible for sales tax setup, preparation, and management as well as preparing quarterly and annual income tax estimate payment calculations. This is the ideal role for someone who prioritizes accuracy and efficiency, has advanced Excel skills, and values strong internal controls.", new DateTime(2023, 12, 15, 11, 44, 42, 586, DateTimeKind.Local).AddTicks(6340), "Audit-PC", "CIP and Retail", true, false, true, false, null, "Noida", new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "PC103", "NC Direct Tax Analysis", "Pending", "WON", "Pending", "PC103", 0, "NC Direct Tax Analysis", "Recurring", "Certification ", 0, "Attest Services-PIE", new DateTime(2023, 10, 5, 11, 44, 42, 586, DateTimeKind.Local).AddTicks(6340), "Tyres & rubber manufacturers", null, "", "" },
                    { 204L, "In Budget", "NonChargable", "ClientGrpName1", "Adani Enterprises Ltd", new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", null, "Job Description: This project is for  Global Audit delivery of Global Audit - Adani. This position is responsible for sales tax setup, preparation, and management as well as preparing quarterly and annual income tax estimate payment calculations. This is the ideal role for someone who prioritizes accuracy and efficiency, has advanced Excel skills, and values strong internal controls.", new DateTime(2023, 12, 15, 11, 44, 42, 586, DateTimeKind.Local).AddTicks(6440), "Audit-GLB", "CIP and Retail", true, false, true, false, true, "Gurgaon", new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "PC104", "Global Audit - Adani", "In Progress", null, "ALLOCATION_COMPLETED", "PC104", 0, "Global Audit - Adani", "Non-recurring", "Loan Staffing", 0, "Attest Services-PIE", new DateTime(2023, 10, 5, 11, 44, 42, 586, DateTimeKind.Local).AddTicks(6430), "Tyres & rubber manufacturersSuspended", new DateTime(2023, 11, 30, 18, 7, 0, 340, DateTimeKind.Local).AddTicks(1370), "", "" },
                    { 205L, "In Budget", "Chargable", "ClientGrpName1", "Adani Enterprises Ltd", new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", null, "Job Description: This project is for Global Audit delivery of GOA CRM Testing Client. This position is responsible for sales tax setup, preparation, and management as well as preparing quarterly and annual income tax estimate payment calculations. This is the ideal role for someone who prioritizes accuracy and efficiency, has advanced Excel skills, and values strong internal controls-updated, update by testing", new DateTime(2023, 12, 30, 11, 44, 42, 586, DateTimeKind.Local).AddTicks(6280), "Audit-PIE", "Auto and Auto Component", true, false, true, true, false, "Mumbai", new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "PC106", "GT Audit", "Pending", null, "ALLOCATION_COMPLETED", "PC106", 0, "GT Audit", "Non-recurring", "Tax Audit", 30, "Attest Service-PIE", new DateTime(2023, 10, 5, 11, 44, 42, 586, DateTimeKind.Local).AddTicks(6260), "Food distributorsWON", null, ",sector= ", null }
                });

            migrationBuilder.InsertData(
                table: "ProjectJobCodes",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "IsActive", "JobCode", "JobName", "ModifiedAt", "ModifiedBy", "PipelineCode", "ProjectCode", "ProjectId" },
                values: new object[,]
                {
                    { 211L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", true, "JC101", "Job Name101", new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "PC101", "PC101", 201L },
                    { 212L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", true, "JC102", "Job Name102", new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "PC102", "PC102", 202L },
                    { 213L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", true, "JC103", "Job Name103", new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "PC102", "PC102", 202L },
                    { 214L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", true, "JC104", "Job Name104", new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "PC104", "PC104", 204L },
                    { 215L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", true, "JC104", "Job Name 104", new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "PC104", "PC104", 204L },
                    { 216L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", true, "JC106", "Job Name106", new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "PC106", "PC106", 205L }
                });

            migrationBuilder.InsertData(
                table: "ProjectRoles",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Description", "IsActive", "ModifiedAt", "ModifiedBy", "ProjectId", "Role", "User", "UserName" },
                values: new object[,]
                {
                    { 211L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", 201L, "engagementleader", "Aayush.Garg@expdiginetdev.onmicrosoft.com", "Ayush Garg" },
                    { 212L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", 201L, "Delegate", "puja@gmail.com", "Puja Sinha" },
                    { 213L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", 201L, "EngagementLeader", "rupesh@gmail.com", "Rupesh Kumar" },
                    { 214L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", 202L, "engagementleader", "Aayush.Garg@expdiginetdev.onmicrosoft.com", "Ayush Garg" },
                    { 215L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", 202L, "Delegate", "puja@gmail.com", "Puja Sinha" },
                    { 216L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", 202L, "EngagementLeader", "rupesh@gmail.com", "Rupesh Kumar" },
                    { 217L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", 203L, "engagementleader", "Aayush.Garg@expdiginetdev.onmicrosoft.com", "Ayush Garg" },
                    { 218L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", 203L, "Delegate", "puja@gmail.com", "Puja Sinha" },
                    { 219L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", 203L, "EO", "rupesh@gmail.com", "Rupesh Kumar" },
                    { 220L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", 204L, "engagementleader", "Aayush.Garg@expdiginetdev.onmicrosoft.com", "Ayush Garg" },
                    { 221L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", 204L, "Delegate", "puja@gmail.com", "Puja Sinha" },
                    { 222L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", 204L, "EO", "rupesh@gmail.com", "Rupesh Kumar" },
                    { 223L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", 203L, "EngagementLeader", "Sakshi@email.com", "Sakshi Joshi" },
                    { 224L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", 203L, "EngagementLeader", "Puja@email.com", "Puja Sinha" },
                    { 225L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", 201L, "EngagementLeader", "Sakshi@email.com", "Sakshi Joshi" },
                    { 226L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", 201L, "Delegate", "Rupesh@email.com", "Rupesh Kumar" },
                    { 227L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", 205L, "Requestor", "Aayush.Garg@expdiginetdev.onmicrosoft.com", "Ayush Garg" },
                    { 228L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", 205L, "Delegate", "Aayush.Garg@expdiginetdev.onmicrosoft.com", "Ayush Garg" },
                    { 229L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", 205L, "delegateuser", "Aayush.Garg@expdiginetdev.onmicrosoft.com", "Ayush Garg" },
                    { 230L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", 205L, "engagementleader", "Aayush.Garg@expdiginetdev.onmicrosoft.com", "Ayush Garg" },
                    { 231L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", 201L, "Delegate", "Aayush.Garg@expdiginetdev.onmicrosoft.com", "Ayush Garg" },
                    { 232L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", 203L, "Delegate", "Aayush.Garg@expdiginetdev.onmicrosoft.com", "Ayush Garg" },
                    { 233L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", 204L, "Delegate", "Aayush.Garg@expdiginetdev.onmicrosoft.com", "Ayush Garg" },
                    { 234L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", "", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "System", 202L, "Delegate", "Aayush.Garg@expdiginetdev.onmicrosoft.com", "Ayush Garg" }
                });
        }
    }
}
