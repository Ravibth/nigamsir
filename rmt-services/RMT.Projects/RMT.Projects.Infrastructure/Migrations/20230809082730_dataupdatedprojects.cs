using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Projects.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class dataupdatedprojects : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 101L,
                columns: new[] { "BudgetStatus", "ClientName", "Description", "EndDate", "Expertise", "Industry", "Location", "PipelineName", "PipelineStage", "ProjectType", "RevenueUnit", "Sme", "StartDate" },
                values: new object[] { "In Budget", "Adani Enterprises Ltd", "This position is responsible for sales tax setup, preparation, and management as well as preparing quarterly and annual income tax \r\nestimate payment calculations. This is the ideal role for someone who prioritizes accuracy and efficiency, has advanced Excel skills, and \r\nvalues strong internal controls.", new DateTime(2023, 8, 24, 8, 27, 30, 645, DateTimeKind.Utc).AddTicks(4117), "Audit-PIE", "Auto and Auto Component", "Mumbai", "Pipeline Name 1", "Approved", "Non-recurring", "Tax Audit", "Attest Service-PIE", new DateTime(2023, 8, 14, 8, 27, 30, 645, DateTimeKind.Utc).AddTicks(4111) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 102L,
                columns: new[] { "BudgetStatus", "ClientName", "Description", "EndDate", "Expertise", "Industry", "Location", "PipelineName", "PipelineStage", "ProjectType", "RevenueUnit", "Sme", "StartDate" },
                values: new object[] { "In Budget", "Tata consultancy services", "This position is responsible for sales tax setup, preparation, and management as well as preparing quarterly and annual income tax \r\nestimate payment calculations. This is the ideal role for someone who prioritizes accuracy and efficiency, has advanced Excel skills, and \r\nvalues strong internal controls.", new DateTime(2023, 8, 24, 8, 27, 30, 645, DateTimeKind.Utc).AddTicks(4124), "Audit-PC", "Auto and Auto Component", "New delhi", "Pipeline Name 2", "Pending", "Recurring", "TaxAudit", "Attest Services-PIE", new DateTime(2023, 8, 14, 8, 27, 30, 645, DateTimeKind.Utc).AddTicks(4124) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 103L,
                columns: new[] { "BudgetStatus", "ClientName", "Description", "EndDate", "Expertise", "Industry", "Location", "PipelineName", "PipelineStage", "ProjectType", "RevenueUnit", "Sme", "StartDate" },
                values: new object[] { "In Budget", "Philips ", "This position is responsible for sales tax setup, preparation, and management as well as preparing quarterly and annual income tax \r\nestimate payment calculations. This is the ideal role for someone who prioritizes accuracy and efficiency, has advanced Excel skills, and \r\nvalues strong internal controls.", new DateTime(2023, 8, 24, 8, 27, 30, 645, DateTimeKind.Utc).AddTicks(4129), "Audit-PC", "CIP and Retail", "Noida", "Pipeline Name 3", "Approved", "Recurring", "Certification ", "Attest Services-PIE", new DateTime(2023, 8, 14, 8, 27, 30, 645, DateTimeKind.Utc).AddTicks(4128) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 104L,
                columns: new[] { "BudgetStatus", "ClientName", "Description", "EndDate", "Expertise", "Industry", "Location", "PipelineName", "PipelineStage", "ProjectType", "RevenueUnit", "Sme", "StartDate" },
                values: new object[] { "In Budget", "Adani Enterprises Ltd", "This position is responsible for sales tax setup, preparation, and management as well as preparing quarterly and annual income tax \r\nestimate payment calculations. This is the ideal role for someone who prioritizes accuracy and efficiency, has advanced Excel skills, and \r\nvalues strong internal controls.", new DateTime(2023, 8, 24, 8, 27, 30, 645, DateTimeKind.Utc).AddTicks(4134), "Audit-GLB", "CIP and Retail", "Gurgaon", "Pipeline Name 4", "In Progress", "Non-recurring", "Loan Staffing", "Attest Services-PIE", new DateTime(2023, 8, 14, 8, 27, 30, 645, DateTimeKind.Utc).AddTicks(4133) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 101L,
                columns: new[] { "BudgetStatus", "ClientName", "Description", "EndDate", "Expertise", "Industry", "Location", "PipelineName", "PipelineStage", "ProjectType", "RevenueUnit", "Sme", "StartDate" },
                values: new object[] { "PC101", "PC101", "PC101", new DateTime(2023, 8, 24, 7, 28, 39, 984, DateTimeKind.Utc).AddTicks(2427), "PC101", "PC101", "PC101", "PC101", "PC101", "PC101", "PC101", "PC101", new DateTime(2023, 8, 14, 7, 28, 39, 984, DateTimeKind.Utc).AddTicks(2421) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 102L,
                columns: new[] { "BudgetStatus", "ClientName", "Description", "EndDate", "Expertise", "Industry", "Location", "PipelineName", "PipelineStage", "ProjectType", "RevenueUnit", "Sme", "StartDate" },
                values: new object[] { "PC102", "PC102", "PC102", new DateTime(2023, 8, 24, 7, 28, 39, 984, DateTimeKind.Utc).AddTicks(2435), "PC102", "PC102", "PC102", "PC102", "PC102", "PC102", "PC102", "PC102", new DateTime(2023, 8, 14, 7, 28, 39, 984, DateTimeKind.Utc).AddTicks(2435) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 103L,
                columns: new[] { "BudgetStatus", "ClientName", "Description", "EndDate", "Expertise", "Industry", "Location", "PipelineName", "PipelineStage", "ProjectType", "RevenueUnit", "Sme", "StartDate" },
                values: new object[] { "PC103", "PC103", "PC103", new DateTime(2023, 8, 24, 7, 28, 39, 984, DateTimeKind.Utc).AddTicks(2440), "PC103", "PC103", "PC103", "PC103", "PC103", "PC103", "PC103", "PC103", new DateTime(2023, 8, 14, 7, 28, 39, 984, DateTimeKind.Utc).AddTicks(2439) });

            migrationBuilder.UpdateData(
                table: "Projects",
                keyColumn: "Id",
                keyValue: 104L,
                columns: new[] { "BudgetStatus", "ClientName", "Description", "EndDate", "Expertise", "Industry", "Location", "PipelineName", "PipelineStage", "ProjectType", "RevenueUnit", "Sme", "StartDate" },
                values: new object[] { "PC104", "PC104", "PC104", new DateTime(2023, 8, 24, 7, 28, 39, 984, DateTimeKind.Utc).AddTicks(2443), "PC104", "PC104", "PC104", "PC104", "PC104", "PC104", "PC104", "PC104", new DateTime(2023, 8, 14, 7, 28, 39, 984, DateTimeKind.Utc).AddTicks(2442) });
        }
    }
}
