using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RMT.Employee.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class dataseeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PreferenceMasters",
                columns: new[] { "PreferenceMasterId", "Category", "CreatedAt", "CreatedBy", "Description", "IsActive", "ModifiedAt", "ModifiedBy", "Name", "SortOrder" },
                values: new object[,]
                {
                    { 100L, "LOCATION", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6047), "System", "String", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6048), "System", "Noida", 10 },
                    { 101L, "LOCATION", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6053), "System", "String", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6054), "System", "Delhi", 10 },
                    { 102L, "LOCATION", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6058), "System", "String", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6059), "System", "Pune", 10 },
                    { 103L, "LOCATION", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6062), "System", "String", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6063), "System", "Lucknow", 10 },
                    { 104L, "LOCATION", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6066), "System", "String", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6067), "System", "Hydrabad", 10 },
                    { 105L, "LOCATION", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6071), "System", "String", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6071), "System", "Jaipur", 10 },
                    { 106L, "SME", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6075), "System", "String", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6076), "System", "SME 1", 10 },
                    { 107L, "SME", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6080), "System", "String", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6080), "System", "SME 2", 10 },
                    { 108L, "SME", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6084), "System", "String", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6084), "System", "SME 3", 10 },
                    { 109L, "SME", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6088), "System", "String", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6088), "System", "SME 4", 10 },
                    { 110L, "SME", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6092), "System", "String", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6092), "System", "SME 5", 10 },
                    { 111L, "REVENUE_UNIT", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6096), "System", "String", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6097), "System", "REVENUE UNIT 1", 10 },
                    { 112L, "REVENUE_UNIT", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6101), "System", "String", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6101), "System", "REVENUE UNIT 2", 10 },
                    { 113L, "REVENUE_UNIT", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6105), "System", "String", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6106), "System", "REVENUE UNIT 3", 10 },
                    { 114L, "REVENUE_UNIT", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6109), "System", "String", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6110), "System", "REVENUE UNIT 4", 10 },
                    { 115L, "REVENUE_UNIT", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6113), "System", "String", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6114), "System", "REVENUE UNIT 5", 10 },
                    { 116L, "EXPERTISE", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6117), "System", "String", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6118), "System", "EXPERTISE 1", 10 },
                    { 117L, "EXPERTISE", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6122), "System", "String", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6122), "System", "EXPERTISE 2", 10 },
                    { 118L, "EXPERTISE", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6126), "System", "String", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6127), "System", "EXPERTISE 3", 10 },
                    { 119L, "EXPERTISE", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6130), "System", "String", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6131), "System", "EXPERTISE 4", 10 },
                    { 120L, "EXPERTISE", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6134), "System", "String", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6135), "System", "EXPERTISE 5", 10 },
                    { 121L, "ENGAGEMENT_LEADER", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6139), "System", "String", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6140), "System", "Samarth Mathur", 10 },
                    { 122L, "ENGAGEMENT_LEADER", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6260), "System", "String", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6261), "System", "Manish Karl", 10 },
                    { 123L, "ENGAGEMENT_LEADER", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6265), "System", "String", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6266), "System", "Devang Sharma", 10 },
                    { 124L, "ENGAGEMENT_LEADER", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6270), "System", "String", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6270), "System", "Abhishake Kumar", 10 },
                    { 125L, "ENGAGEMENT_LEADER", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6274), "System", "String", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6275), "System", "Shristi", 10 },
                    { 126L, "BUISNESS_UNIT", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6279), "System", "String", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6279), "System", "Business Unit 1", 10 },
                    { 127L, "BUISNESS_UNIT", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6284), "System", "String", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6284), "System", "Business Unit 2", 10 },
                    { 128L, "BUISNESS_UNIT", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6288), "System", "String", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6289), "System", "Business Unit 3", 10 },
                    { 129L, "BUISNESS_UNIT", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6293), "System", "String", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6293), "System", "Business Unit 4", 10 },
                    { 130L, "BUISNESS_UNIT", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6298), "System", "String", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6299), "System", "Business Unit 5", 10 },
                    { 131L, "INDUSTRY", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6303), "System", "String", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6303), "System", "Industry 1", 10 },
                    { 132L, "INDUSTRY", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6307), "System", "String", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6307), "System", "Industry 2", 10 },
                    { 133L, "INDUSTRY", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6311), "System", "String", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6312), "System", "Industry 3", 10 },
                    { 134L, "INDUSTRY", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6315), "System", "String", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6316), "System", "Industry 4", 10 },
                    { 135L, "INDUSTRY", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6319), "System", "String", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6320), "System", "Industry 5", 10 },
                    { 136L, "SECTOR", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6323), "System", "String", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6324), "System", "Sector 1", 10 },
                    { 137L, "SECTOR", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6327), "System", "String", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6328), "System", "Sector 2", 10 },
                    { 138L, "SECTOR", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6331), "System", "String", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6332), "System", "Sector 3", 10 },
                    { 139L, "SECTOR", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6336), "System", "String", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6336), "System", "Sector 4", 10 },
                    { 140L, "SECTOR", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6339), "System", "String", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(6340), "System", "Sector 5", 10 }
                });

            migrationBuilder.InsertData(
                table: "EmployeePreferences",
                columns: new[] { "Id", "Category", "CreatedAt", "CreatedBy", "EmployeeEmail", "IsActive", "ModifiedAt", "ModifiedBy", "PreferedValue" },
                values: new object[,]
                {
                    { 500L, "LOCATION", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5179), "System", "john.smith@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5182), "System", 100L },
                    { 501L, "SME", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5189), "System", "john.smith@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5190), "System", 106L },
                    { 502L, "REVENUE_UNIT", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5194), "System", "john.smith@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5195), "System", 111L },
                    { 503L, "EXPERTISE", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5198), "System", "john.smith@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5200), "System", 116L },
                    { 504L, "ENGAGEMENT_LEADER", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5204), "System", "john.smith@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5205), "System", 121L },
                    { 505L, "BUISNESS_UNIT", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5210), "System", "john.smith@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5211), "System", 126L },
                    { 506L, "INDUSTRY", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5215), "System", "john.smith@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5215), "System", 131L },
                    { 507L, "SECTOR", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5219), "System", "john.smith@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5220), "System", 136L },
                    { 508L, "LOCATION", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5224), "System", "emily.johnson@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5224), "System", 100L },
                    { 509L, "SME", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5228), "System", "emily.johnson@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5229), "System", 106L },
                    { 510L, "REVENUE_UNIT", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5232), "System", "emily.johnson@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5233), "System", 111L },
                    { 511L, "EXPERTISE", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5237), "System", "emily.johnson@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5237), "System", 116L },
                    { 512L, "ENGAGEMENT_LEADER", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5241), "System", "emily.johnson@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5242), "System", 121L },
                    { 513L, "BUISNESS_UNIT", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5246), "System", "emily.johnson@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5248), "System", 126L },
                    { 514L, "INDUSTRY", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5252), "System", "emily.johnson@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5252), "System", 131L },
                    { 515L, "SECTOR", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5256), "System", "emily.johnson@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5256), "System", 136L },
                    { 516L, "LOCATION", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5260), "System", "michael.davis@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5261), "System", 100L },
                    { 517L, "SME", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5264), "System", "michael.davis@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5265), "System", 106L },
                    { 518L, "REVENUE_UNIT", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5268), "System", "michael.davis@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5269), "System", 111L },
                    { 519L, "EXPERTISE", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5273), "System", "michael.davis@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5274), "System", 116L },
                    { 520L, "ENGAGEMENT_LEADER", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5278), "System", "michael.davis@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5278), "System", 121L },
                    { 521L, "BUISNESS_UNIT", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5282), "System", "michael.davis@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5282), "System", 126L },
                    { 522L, "INDUSTRY", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5286), "System", "michael.davis@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5287), "System", 131L },
                    { 523L, "SECTOR", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5292), "System", "michael.davis@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5292), "System", 136L },
                    { 524L, "LOCATION", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5296), "System", "jessica.anderson@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5297), "System", 100L },
                    { 525L, "SME", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5300), "System", "jessica.anderson@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5301), "System", 106L },
                    { 526L, "REVENUE_UNIT", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5305), "System", "jessica.anderson@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5305), "System", 111L },
                    { 527L, "EXPERTISE", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5309), "System", "jessica.anderson@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5310), "System", 116L },
                    { 528L, "ENGAGEMENT_LEADER", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5313), "System", "jessica.anderson@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5314), "System", 121L },
                    { 529L, "BUISNESS_UNIT", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5317), "System", "jessica.anderson@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5319), "System", 126L },
                    { 530L, "INDUSTRY", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5322), "System", "jessica.anderson@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5323), "System", 131L },
                    { 531L, "SECTOR", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5326), "System", "jessica.anderson@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5327), "System", 136L },
                    { 532L, "LOCATION", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5330), "System", "david.wilson@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5331), "System", 100L },
                    { 533L, "SME", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5338), "System", "david.wilson@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5339), "System", 106L },
                    { 534L, "REVENUE_UNIT", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5344), "System", "david.wilson@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5345), "System", 111L },
                    { 535L, "EXPERTISE", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5349), "System", "david.wilson@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5350), "System", 116L },
                    { 536L, "ENGAGEMENT_LEADER", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5355), "System", "david.wilson@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5356), "System", 121L },
                    { 537L, "BUISNESS_UNIT", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5360), "System", "david.wilson@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5361), "System", 126L },
                    { 538L, "INDUSTRY", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5367), "System", "david.wilson@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5368), "System", 131L },
                    { 539L, "SECTOR", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5373), "System", "david.wilson@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5374), "System", 136L },
                    { 540L, "LOCATION", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5378), "System", "sarah.thompson@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5379), "System", 100L },
                    { 541L, "SME", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5384), "System", "sarah.thompson@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5385), "System", 106L },
                    { 542L, "REVENUE_UNIT", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5390), "System", "sarah.thompson@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5390), "System", 111L },
                    { 543L, "EXPERTISE", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5397), "System", "sarah.thompson@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5398), "System", 116L },
                    { 544L, "ENGAGEMENT_LEADER", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5404), "System", "sarah.thompson@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5404), "System", 121L },
                    { 545L, "BUISNESS_UNIT", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5409), "System", "sarah.thompson@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5410), "System", 126L },
                    { 546L, "INDUSTRY", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5414), "System", "sarah.thompson@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5415), "System", 131L },
                    { 547L, "SECTOR", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5419), "System", "sarah.thompson@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5420), "System", 136L },
                    { 548L, "LOCATION", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5424), "System", "robert.martinez@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5424), "System", 100L },
                    { 549L, "SME", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5428), "System", "robert.martinez@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5429), "System", 106L },
                    { 550L, "REVENUE_UNIT", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5433), "System", "robert.martinez@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5434), "System", 111L },
                    { 551L, "EXPERTISE", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5440), "System", "robert.martinez@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5441), "System", 116L },
                    { 552L, "ENGAGEMENT_LEADER", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5446), "System", "robert.martinez@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5447), "System", 121L },
                    { 553L, "BUISNESS_UNIT", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5453), "System", "robert.martinez@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5454), "System", 126L },
                    { 554L, "INDUSTRY", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5460), "System", "robert.martinez@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5461), "System", 131L },
                    { 555L, "SECTOR", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5674), "System", "robert.martinez@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5674), "System", 136L },
                    { 556L, "LOCATION", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5679), "System", "matthew.lee@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5680), "System", 100L },
                    { 557L, "SME", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5684), "System", "matthew.lee@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5684), "System", 106L },
                    { 558L, "REVENUE_UNIT", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5688), "System", "matthew.lee@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5688), "System", 111L },
                    { 559L, "EXPERTISE", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5692), "System", "matthew.lee@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5693), "System", 116L },
                    { 560L, "ENGAGEMENT_LEADER", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5697), "System", "matthew.lee@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5697), "System", 121L },
                    { 561L, "BUISNESS_UNIT", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5702), "System", "matthew.lee@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5703), "System", 126L },
                    { 562L, "INDUSTRY", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5707), "System", "matthew.lee@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5707), "System", 131L },
                    { 563L, "SECTOR", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5711), "System", "matthew.lee@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5712), "System", 136L },
                    { 564L, "LOCATION", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5716), "System", "emily.turner@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5717), "System", 100L },
                    { 565L, "SME", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5720), "System", "emily.turner@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5721), "System", 106L },
                    { 566L, "REVENUE_UNIT", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5724), "System", "emily.turner@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5725), "System", 111L },
                    { 567L, "EXPERTISE", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5729), "System", "emily.turner@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5729), "System", 116L },
                    { 568L, "ENGAGEMENT_LEADER", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5733), "System", "emily.turner@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5734), "System", 121L },
                    { 569L, "BUISNESS_UNIT", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5738), "System", "emily.turner@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5738), "System", 126L },
                    { 570L, "INDUSTRY", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5742), "System", "emily.turner@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5742), "System", 131L },
                    { 571L, "SECTOR", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5746), "System", "emily.turner@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5747), "System", 136L },
                    { 572L, "LOCATION", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5750), "System", "alexander.smith@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5751), "System", 100L },
                    { 573L, "SME", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5755), "System", "alexander.smith@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5755), "System", 106L },
                    { 574L, "REVENUE_UNIT", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5759), "System", "alexander.smith@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5760), "System", 111L },
                    { 575L, "EXPERTISE", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5763), "System", "alexander.smith@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5764), "System", 116L },
                    { 576L, "ENGAGEMENT_LEADER", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5768), "System", "alexander.smith@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5768), "System", 121L },
                    { 577L, "BUISNESS_UNIT", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5772), "System", "alexander.smith@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5773), "System", 126L },
                    { 578L, "INDUSTRY", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5776), "System", "alexander.smith@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5777), "System", 131L },
                    { 579L, "SECTOR", new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5781), "System", "alexander.smith@example.com", true, new DateTime(2023, 8, 30, 8, 50, 5, 972, DateTimeKind.Utc).AddTicks(5781), "System", 136L }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 500L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 501L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 502L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 503L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 504L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 505L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 506L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 507L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 508L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 509L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 510L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 511L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 512L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 513L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 514L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 515L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 516L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 517L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 518L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 519L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 520L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 521L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 522L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 523L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 524L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 525L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 526L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 527L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 528L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 529L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 530L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 531L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 532L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 533L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 534L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 535L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 536L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 537L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 538L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 539L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 540L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 541L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 542L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 543L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 544L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 545L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 546L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 547L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 548L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 549L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 550L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 551L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 552L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 553L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 554L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 555L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 556L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 557L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 558L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 559L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 560L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 561L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 562L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 563L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 564L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 565L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 566L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 567L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 568L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 569L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 570L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 571L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 572L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 573L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 574L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 575L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 576L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 577L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 578L);

            migrationBuilder.DeleteData(
                table: "EmployeePreferences",
                keyColumn: "Id",
                keyValue: 579L);

            migrationBuilder.DeleteData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 101L);

            migrationBuilder.DeleteData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 102L);

            migrationBuilder.DeleteData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 103L);

            migrationBuilder.DeleteData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 104L);

            migrationBuilder.DeleteData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 105L);

            migrationBuilder.DeleteData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 107L);

            migrationBuilder.DeleteData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 108L);

            migrationBuilder.DeleteData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 109L);

            migrationBuilder.DeleteData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 110L);

            migrationBuilder.DeleteData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 112L);

            migrationBuilder.DeleteData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 113L);

            migrationBuilder.DeleteData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 114L);

            migrationBuilder.DeleteData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 115L);

            migrationBuilder.DeleteData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 117L);

            migrationBuilder.DeleteData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 118L);

            migrationBuilder.DeleteData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 119L);

            migrationBuilder.DeleteData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 120L);

            migrationBuilder.DeleteData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 122L);

            migrationBuilder.DeleteData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 123L);

            migrationBuilder.DeleteData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 124L);

            migrationBuilder.DeleteData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 125L);

            migrationBuilder.DeleteData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 127L);

            migrationBuilder.DeleteData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 128L);

            migrationBuilder.DeleteData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 129L);

            migrationBuilder.DeleteData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 130L);

            migrationBuilder.DeleteData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 132L);

            migrationBuilder.DeleteData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 133L);

            migrationBuilder.DeleteData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 134L);

            migrationBuilder.DeleteData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 135L);

            migrationBuilder.DeleteData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 137L);

            migrationBuilder.DeleteData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 138L);

            migrationBuilder.DeleteData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 139L);

            migrationBuilder.DeleteData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 140L);

            migrationBuilder.DeleteData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 100L);

            migrationBuilder.DeleteData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 106L);

            migrationBuilder.DeleteData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 111L);

            migrationBuilder.DeleteData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 116L);

            migrationBuilder.DeleteData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 121L);

            migrationBuilder.DeleteData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 126L);

            migrationBuilder.DeleteData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 131L);

            migrationBuilder.DeleteData(
                table: "PreferenceMasters",
                keyColumn: "PreferenceMasterId",
                keyValue: 136L);
        }
    }
}
