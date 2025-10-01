using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RMT.Configuration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class dataseeddocker : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "BusinessUnitMasters",
                columns: new[] { "Id", "BuId", "BusinessUnit", "CreatedAt", "CreatedBy", "IsActive", "ModifiedAt", "ModifiedBy" },
                values: new object[,]
                {
                    { 1L, "SG000010", "Deals", new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System" },
                    { 2L, "SG000013", "ESG & Risk Consulting", new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System" }
                });

            migrationBuilder.InsertData(
                table: "ConfigurationGroups",
                columns: new[] { "Id", "AllValue", "ConfigGroup", "ConfigGroupDisplay", "ConfigKey", "ConfigType", "CongigDisplayText", "CreatedAt", "CreatedBy", "IsActive", "IsAll", "ModifiedAt", "ModifiedBy", "ValueType" },
                values: new object[,]
                {
                    { 1L, "{\"activationStatus\":\"true\",\"noOfDays\":\"1\"}", "Resource_allocation_review", "Resource allocation review", "Resource_allocation_review", "EXPERTISE", "Resource allocation review", new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, false, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "{\"activationStatus\":\"Boolean\",\"noOfDays\":\"Integer\"}" },
                    { 2L, "{\"activationStatus\":\"false\",\"noOfDays\":\"1\"}", "Resource_allocation_review", "Resource allocation review", "Resource_allocation_review", "BUSINESS_UNIT", "Resource allocation review", new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, false, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "{\"activationStatus\":\"Boolean\",\"noOfDays\":\"Integer\"}" },
                    { 5L, "12", "No_of_days_where_project_is_available_in_Marketplace", "No of days where project is available in Marketplace", "No_of_days_where_project_is_available_in_Marketplace", "EXPERTISE", "No of days where project is available in Marketplace", new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, false, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 6L, "15", "No_of_days_where_project_is_available_in_Marketplace", "No of days where project is available in Marketplace", "No_of_days_where_project_is_available_in_Marketplace", "BUSINESS_UNIT", "No of days where project is available in Marketplace", new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, false, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 7L, "string", "string", "string", "string", "string", "string", new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "string" },
                    { 8L, "1", "Weightage_for_parameters_for_System_Suggested_Requisition", "Weightage for parameters for System Suggested Requisition", "Location", "EXPERTISE", "Location", new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 9L, "1", "Weightage_for_parameters_for_System_Suggested_Requisition", "Weightage for parameters for System Suggested Requisition", "Location", "BUSINESS_UNIT", "Location", new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 10L, "8", "Weightage_for_parameters_for_System_Suggested_Requisition", "Weightage for parameters for System Suggested Requisition", "Expertise", "EXPERTISE", "Expertise", new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 11L, "8", "Weightage_for_parameters_for_System_Suggested_Requisition", "Weightage for parameters for System Suggested Requisition", "Expertise", "BUSINESS_UNIT", "Expertise", new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 12L, "6", "Weightage_for_parameters_for_System_Suggested_Requisition", "Weightage for parameters for System Suggested Requisition", "SMEG", "EXPERTISE", "SMEG", new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 13L, "6", "Weightage_for_parameters_for_System_Suggested_Requisition", "Weightage for parameters for System Suggested Requisition", "SMEG", "BUSINESS_UNIT", "SMEG", new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 14L, "4", "Weightage_for_parameters_for_System_Suggested_Requisition", "Weightage for parameters for System Suggested Requisition", "Revenue_Unit", "EXPERTISE", "Revenue Unit", new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 15L, "4", "Weightage_for_parameters_for_System_Suggested_Requisition", "Weightage for parameters for System Suggested Requisition", "Revenue_Unit", "BUSINESS_UNIT", "Revenue Unit", new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 16L, "7", "Weightage_for_parameters_for_System_Suggested_Requisition", "Weightage for parameters for System Suggested Requisition", "Sector", "EXPERTISE", "Sector", new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, false, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 17L, "6", "Weightage_for_parameters_for_System_Suggested_Requisition", "Weightage for parameters for System Suggested Requisition", "Sector", "BUSINESS_UNIT", "Sector", new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, false, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 18L, "2", "Weightage_for_parameters_for_System_Suggested_Requisition", "Weightage for parameters for System Suggested Requisition", "Same_client", "EXPERTISE", "Experience working with same client", new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 19L, "2", "Weightage_for_parameters_for_System_Suggested_Requisition", "Weightage for parameters for System Suggested Requisition", "Same_client", "BUSINESS_UNIT", "Experience working with same client", new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 20L, "10", "Weightage_for_parameters_for_System_Suggested_Requisition", "Weightage for parameters for System Suggested Requisition", "Sub_Industry", "EXPERTISE", "Sub Industry", new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 21L, "10", "Weightage_for_parameters_for_System_Suggested_Requisition", "Weightage for parameters for System Suggested Requisition", "Sub_Industry", "BUSINESS_UNIT", "Sub Industry", new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 22L, "9", "Threshold_no_of_days_for_notification_to_be_sent_to_Resource_Requestor_if_Project_status_is_not_WON", "Threshold no of days for notification to be sent to Resource Requestor if Project status is not WON", "Threshold_no_of_days_for_notification_to_be_sent_to_Resource_Requestor_if_Project_status_is_not_WON", "EXPERTISE", "Threshold no of days for notification to be sent to Resource Requestor if Project status is not WON", new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, false, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 23L, "9", "Threshold_no_of_days_for_notification_to_be_sent_to_Resource_Requestor_if_Project_status_is_not_WON", "Threshold no of days for notification to be sent to Resource Requestor if Project status is not WON", "Threshold_no_of_days_for_notification_to_be_sent_to_Resource_Requestor_if_Project_status_is_not_WON", "BUSINESS_UNIT", "Threshold no of days for notification to be sent to Resource Requestor if Project status is not WON", new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, false, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 24L, "6", "Weightage_for_parameters_for_System_Suggested_Requisition", "Weightage for parameters for System Suggested Requisition", "business_unit", "BUSINESS_UNIT", "Business Unit", new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 25L, "9", "Weightage_for_parameters_for_System_Suggested_Requisition", "Weightage for parameters for System Suggested Requisition", "business_unit", "EXPERTISE", "Business Unit", new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 26L, "12", "Maximum_number_of_items_for_employee_preference", "Maximum number of items for employee preference", "Maximum_number_of_items_for_employee_preference", "EXPERTISE", "Maximum number of items for employee preference", new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, false, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 27L, "14", "Maximum_number_of_items_for_employee_preference", "Maximum number of items for employee preference", "Maximum_number_of_items_for_employee_preference", "BUSINESS_UNIT", "Maximum number of items for employee preference", new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, false, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "INTEGER" }
                });

            migrationBuilder.InsertData(
                table: "ContextMenuMaster",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Description", "DisplayName", "InternalName", "IsActive", "ModifiedAt", "ModifiedBy", "Order" },
                values: new object[,]
                {
                    { 1L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "", "View Details", "View Details", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", 10 },
                    { 2L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "", "Create Requisition", "Create Requisition", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", 20 },
                    { 3L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "", "Upload Requisition Excel", "Upload Requisition Excel", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", 30 },
                    { 4L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "", "Allocate Employee", "Allocate Employee", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", 40 },
                    { 5L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "", "Allocate Same Team", "Allocate Same Team", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", 50 },
                    { 6L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "", "Roll forward allocations", "Roll forward allocations", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", 60 },
                    { 7L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "", "Move To Marketplace", "Move To Marketplace", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", 70 }
                });

            migrationBuilder.InsertData(
                table: "ExpertiesMasters",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Experties", "ExpertiesId", "IsActive", "ModifiedAt", "ModifiedBy" },
                values: new object[,]
                {
                    { 1L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "NDO", "SL000060", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System" },
                    { 2L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Recovery & Reorganisation", "SL000015", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System" },
                    { 3L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Transaction Tax", "SL000043", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System" },
                    { 4L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Due Diligence", "SL000046", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System" },
                    { 5L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Governance Risk & Operations (GRO)", "SL000049", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System" },
                    { 6L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "NErcO", "SL000061", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System" },
                    { 7L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Cyber & IT Risk (Cyber)", "SL000022", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System" },
                    { 8L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "FS Risk", "SL000027", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System" },
                    { 9L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Forensic", "SL000021", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System" }
                });

            migrationBuilder.InsertData(
                table: "MenuMaster",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Description", "DisplayName", "InternalName", "IsActive", "Is_Expandable", "MenuType", "ModifiedAt", "ModifiedBy", "Order", "ParentId", "Path" },
                values: new object[,]
                {
                    { 1L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "", "Dashboard", "Dashboard", true, false, "", new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", 10, "", "/" },
                    { 2L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "", "Reports", "Reports", true, false, "", new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", 20, "", "/reports" },
                    { 3L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "", "Projects", "Projects Listing", true, false, "", new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", 30, "", "/" },
                    { 4L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "", "Pending Requests / My Tasks", "Pending Requests / My Tasks", true, false, "", new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", 40, "", "/mytask" },
                    { 5L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "", "Configurations", "Configurations", true, false, "", new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", 50, "", "/configurations" },
                    { 6L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "", "User Onboarding", "User Onboarding", true, false, "", new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", 60, "", "/roles-permission" },
                    { 7L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "", "Marketplace", "Marketplace", true, false, "", new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", 70, "", "/marketplace" },
                    { 8L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "", "Alert", "Alert", true, false, "", new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", 80, "", "/notification" },
                    { 9L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "", "My View", "My View", true, false, "", new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", 90, "", "/manage" },
                    { 10L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "", "Manage", "Manage", true, false, "", new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", 4, "", "/alert" },
                    { 11L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "", "Employees", "Employees", true, false, "", new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", 2, "", "/employee" }
                });

            migrationBuilder.InsertData(
                table: "RUMaster",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "IsActive", "ModifiedAt", "ModifiedBy", "RU", "RUId" },
                values: new object[,]
                {
                    { 1L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "NC NDO", "RV007793" },
                    { 2L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Asset tracing & recovery", "RV007630" },
                    { 3L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "NC Transaction Tax", "RV007599" },
                    { 4L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Tax Compliance", "RV007603" },
                    { 5L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Tax Litigation", "RV007604" },
                    { 6L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Tax Assessment", "RV007605" },
                    { 7L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Tax Attest", "RV007606" },
                    { 8L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Tax Regulatory", "RV007607" },
                    { 9L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Tax Advisory", "RV007608" },
                    { 10L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "NC Due Diligence", "RV007612" },
                    { 11L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "GRO - Analytics / CCM / RPA", "RV007749" },
                    { 12L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "(CSRS) - Energy Advisory", "RV007750" },
                    { 13L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "GRO - Internal Audit", "RV007751" },
                    { 14L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "GRO- Standard operating procedure", "RV007752" },
                    { 15L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "NC Governance Risk & Operations (GRO)", "RV007753" },
                    { 16L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "NC NErcO", "RV007794" },
                    { 17L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "NC Global Delivery - Cyber", "RV007801" },
                    { 18L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "NC Global Delivery - FS Risk", "RV007802" },
                    { 19L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "NC Forensic", "RV007808" },
                    { 20L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "NC GD UK JV", "RV007812" }
                });

            migrationBuilder.InsertData(
                table: "SMEGMaster",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "IsActive", "ModifiedAt", "ModifiedBy", "SMEG", "SMEGId" },
                values: new object[,]
                {
                    { 1L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "NC NDO", "IS001165" },
                    { 2L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Recovery & Reorganisation", "IS001124" },
                    { 3L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Transaction Tax", "IS001116" },
                    { 4L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Due Diligence", "IS001120" },
                    { 5L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "GRO", "IS001154" },
                    { 6L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "NC NErcO", "IS001166" },
                    { 7L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "GD - Cyber", "IS001148" },
                    { 8L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "GD - FS Risk", "IS001153" },
                    { 9L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Forensic", "IS001150" },
                    { 10L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "GD UK JV - FS Risk", "IS001152" }
                });

            migrationBuilder.InsertData(
                table: "Bu_Experties_Grps",
                columns: new[] { "Id", "BusinessUnitId", "BusinessUnitName", "CreatedAt", "CreatedBy", "ExpertiesId", "ExpertiesName", "IsActive", "ModifiedAt", "ModifiedBy", "RUId", "SMEGId" },
                values: new object[,]
                {
                    { 1L, 1L, null, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", 1L, null, true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", 1L, 1L },
                    { 2L, 1L, null, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", 2L, null, true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", 2L, 2L },
                    { 3L, 1L, null, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", 3L, null, true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", 3L, 3L },
                    { 4L, 1L, null, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", 3L, null, true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", 4L, 3L },
                    { 5L, 1L, null, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", 3L, null, true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", 5L, 3L },
                    { 6L, 1L, null, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", 3L, null, true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", 6L, 3L },
                    { 7L, 1L, null, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", 3L, null, true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", 7L, 3L },
                    { 8L, 1L, null, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", 3L, null, true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", 8L, 3L },
                    { 9L, 1L, null, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", 3L, null, true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", 9L, 3L },
                    { 10L, 1L, null, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", 4L, null, true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", 10L, 4L },
                    { 11L, 2L, null, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", 5L, null, true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", 11L, 5L },
                    { 12L, 2L, null, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", 5L, null, true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", 12L, 5L },
                    { 13L, 2L, null, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", 5L, null, true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", 13L, 5L },
                    { 14L, 2L, null, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", 5L, null, true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", 14L, 5L },
                    { 15L, 2L, null, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", 5L, null, true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", 15L, 5L },
                    { 16L, 2L, null, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", 6L, null, true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", 16L, 6L },
                    { 17L, 2L, null, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", 7L, null, true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", 17L, 7L },
                    { 18L, 2L, null, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", 8L, null, true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", 18L, 8L },
                    { 19L, 2L, null, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", 9L, null, true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", 19L, 9L },
                    { 20L, 2L, null, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", 8L, null, true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", 20L, 10L }
                });

            migrationBuilder.InsertData(
                table: "RoleContextMenu",
                columns: new[] { "Id", "ContextMenuId", "CreatedAt", "CreatedBy", "IsActive", "ModifiedAt", "ModifiedBy", "Role" },
                values: new object[,]
                {
                    { 1L, 1L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Resource Requestor" },
                    { 2L, 2L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Resource Requestor" },
                    { 3L, 3L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Resource Requestor" },
                    { 4L, 4L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Resource Requestor" },
                    { 5L, 5L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Resource Requestor" },
                    { 6L, 6L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Resource Requestor" },
                    { 7L, 7L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Resource Requestor" },
                    { 8L, 1L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Additional EL" },
                    { 9L, 2L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Additional EL" },
                    { 10L, 3L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Additional EL" },
                    { 11L, 4L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Additional EL" },
                    { 12L, 5L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Additional EL" },
                    { 13L, 6L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Additional EL" },
                    { 14L, 7L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Additional EL" },
                    { 15L, 1L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Delegate" },
                    { 16L, 2L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Delegate" },
                    { 17L, 3L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Delegate" },
                    { 18L, 4L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Delegate" },
                    { 19L, 5L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Delegate" },
                    { 20L, 6L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Delegate" },
                    { 21L, 7L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Delegate" },
                    { 22L, 1L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Admin" },
                    { 23L, 2L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Admin" },
                    { 24L, 3L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Admin" },
                    { 25L, 4L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Admin" },
                    { 26L, 5L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Admin" },
                    { 27L, 6L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Admin" },
                    { 28L, 7L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Admin" },
                    { 29L, 1L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Employee" },
                    { 30L, 2L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Employee" },
                    { 31L, 3L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Employee" },
                    { 32L, 4L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Employee" },
                    { 33L, 5L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Employee" },
                    { 34L, 6L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Employee" },
                    { 35L, 7L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Employee" },
                    { 36L, 1L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Request Reviewer" },
                    { 37L, 2L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Request Reviewer" },
                    { 38L, 3L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Request Reviewer" },
                    { 39L, 4L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Request Reviewer" },
                    { 40L, 5L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Request Reviewer" },
                    { 41L, 6L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Request Reviewer" },
                    { 42L, 7L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Request Reviewer" },
                    { 43L, 1L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Additional Delegate" },
                    { 44L, 2L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Additional Delegate" },
                    { 45L, 3L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Additional Delegate" },
                    { 46L, 4L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Additional Delegate" },
                    { 47L, 5L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Additional Delegate" },
                    { 48L, 6L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Additional Delegate" },
                    { 49L, 7L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Additional Delegate" },
                    { 50L, 1L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Leader" },
                    { 51L, 2L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Leader" },
                    { 52L, 3L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Leader" },
                    { 53L, 4L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Leader" },
                    { 54L, 5L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Leader" },
                    { 55L, 6L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Leader" },
                    { 56L, 7L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Leader" }
                });

            migrationBuilder.InsertData(
                table: "RoleMenu",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "IsActive", "MenuId", "ModifiedAt", "ModifiedBy", "Role" },
                values: new object[,]
                {
                    { 1L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, 1L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Employee" },
                    { 2L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, 2L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Employee" },
                    { 3L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, 3L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Employee" },
                    { 4L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, 4L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Employee" },
                    { 5L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, 5L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Employee" },
                    { 6L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, 6L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Employee" },
                    { 7L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, 7L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Employee" },
                    { 8L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, 8L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Employee" },
                    { 9L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, 9L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Employee" },
                    { 10L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, 1L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Resource Requestor" },
                    { 11L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, 2L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Resource Requestor" },
                    { 12L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, 3L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Resource Requestor" },
                    { 13L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, 4L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Resource Requestor" },
                    { 14L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, 5L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Resource Requestor" },
                    { 15L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", false, 6L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Resource Requestor" },
                    { 16L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", false, 7L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Resource Requestor" },
                    { 17L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", false, 8L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Resource Requestor" },
                    { 18L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", false, 9L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Resource Requestor" },
                    { 19L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, 1L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Delegate" },
                    { 20L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, 2L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Delegate" },
                    { 21L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, 3L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Delegate" },
                    { 22L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, 4L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Delegate" },
                    { 23L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", false, 5L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Delegate" },
                    { 24L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", false, 6L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Delegate" },
                    { 25L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", false, 7L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Delegate" },
                    { 26L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", false, 8L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Delegate" },
                    { 27L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", false, 9L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Delegate" },
                    { 28L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, 1L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Request Reviewer" },
                    { 29L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, 2L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Request Reviewer" },
                    { 30L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, 3L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Request Reviewer" },
                    { 31L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, 4L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Request Reviewer" },
                    { 32L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", false, 5L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Request Reviewer" },
                    { 33L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", false, 6L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Request Reviewer" },
                    { 34L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", false, 7L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Request Reviewer" },
                    { 35L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", false, 8L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Request Reviewer" },
                    { 36L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", false, 9L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Request Reviewer" },
                    { 37L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, 1L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Additional EL" },
                    { 38L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, 2L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Additional EL" },
                    { 39L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, 3L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Additional EL" },
                    { 40L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, 4L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Additional EL" },
                    { 41L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", false, 5L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Additional EL" },
                    { 42L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", false, 6L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Additional EL" },
                    { 43L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", false, 7L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Additional EL" },
                    { 44L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", false, 8L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Additional EL" },
                    { 45L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", false, 9L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Additional EL" },
                    { 46L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, 1L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Additional Delegate" },
                    { 47L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, 2L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Additional Delegate" },
                    { 48L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, 3L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Additional Delegate" },
                    { 49L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, 4L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Additional Delegate" },
                    { 50L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", false, 5L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Additional Delegate" },
                    { 51L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", false, 6L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Additional Delegate" },
                    { 52L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", false, 7L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Additional Delegate" },
                    { 53L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", false, 8L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Additional Delegate" },
                    { 54L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", false, 9L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Additional Delegate" },
                    { 55L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, 1L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Admin" },
                    { 56L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, 2L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Admin" },
                    { 57L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, 3L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Admin" },
                    { 58L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", false, 4L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Admin" },
                    { 59L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, 5L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Admin" },
                    { 60L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, 6L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Admin" },
                    { 61L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", false, 7L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Admin" },
                    { 62L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", false, 8L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Admin" },
                    { 63L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", false, 9L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Admin" },
                    { 64L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, 1L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Leader" },
                    { 65L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, 2L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Leader" },
                    { 66L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, 3L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Leader" },
                    { 67L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", false, 4L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Leader" },
                    { 68L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, 5L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Leader" },
                    { 69L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", true, 6L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Leader" },
                    { 70L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", false, 7L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Leader" },
                    { 71L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", false, 8L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Leader" },
                    { 72L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", false, 9L, new DateTime(2023, 12, 12, 0, 0, 0, 0, DateTimeKind.Utc), "System", "Leader" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Bu_Experties_Grps",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Bu_Experties_Grps",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Bu_Experties_Grps",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Bu_Experties_Grps",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Bu_Experties_Grps",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "Bu_Experties_Grps",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "Bu_Experties_Grps",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "Bu_Experties_Grps",
                keyColumn: "Id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "Bu_Experties_Grps",
                keyColumn: "Id",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                table: "Bu_Experties_Grps",
                keyColumn: "Id",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                table: "Bu_Experties_Grps",
                keyColumn: "Id",
                keyValue: 11L);

            migrationBuilder.DeleteData(
                table: "Bu_Experties_Grps",
                keyColumn: "Id",
                keyValue: 12L);

            migrationBuilder.DeleteData(
                table: "Bu_Experties_Grps",
                keyColumn: "Id",
                keyValue: 13L);

            migrationBuilder.DeleteData(
                table: "Bu_Experties_Grps",
                keyColumn: "Id",
                keyValue: 14L);

            migrationBuilder.DeleteData(
                table: "Bu_Experties_Grps",
                keyColumn: "Id",
                keyValue: 15L);

            migrationBuilder.DeleteData(
                table: "Bu_Experties_Grps",
                keyColumn: "Id",
                keyValue: 16L);

            migrationBuilder.DeleteData(
                table: "Bu_Experties_Grps",
                keyColumn: "Id",
                keyValue: 17L);

            migrationBuilder.DeleteData(
                table: "Bu_Experties_Grps",
                keyColumn: "Id",
                keyValue: 18L);

            migrationBuilder.DeleteData(
                table: "Bu_Experties_Grps",
                keyColumn: "Id",
                keyValue: 19L);

            migrationBuilder.DeleteData(
                table: "Bu_Experties_Grps",
                keyColumn: "Id",
                keyValue: 20L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 11L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 12L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 13L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 14L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 15L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 16L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 17L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 18L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 19L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 20L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 21L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 22L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 23L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 24L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 25L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 26L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 27L);

            migrationBuilder.DeleteData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 11L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 11L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 12L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 13L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 14L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 15L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 16L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 17L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 18L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 19L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 20L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 21L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 22L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 23L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 24L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 25L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 26L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 27L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 28L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 29L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 30L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 31L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 32L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 33L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 34L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 35L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 36L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 37L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 38L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 39L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 40L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 41L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 42L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 43L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 44L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 45L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 46L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 47L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 48L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 49L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 50L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 51L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 52L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 53L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 54L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 55L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 56L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 11L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 12L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 13L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 14L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 15L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 16L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 17L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 18L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 19L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 20L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 21L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 22L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 23L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 24L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 25L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 26L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 27L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 28L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 29L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 30L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 31L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 32L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 33L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 34L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 35L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 36L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 37L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 38L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 39L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 40L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 41L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 42L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 43L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 44L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 45L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 46L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 47L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 48L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 49L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 50L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 51L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 52L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 53L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 54L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 55L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 56L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 57L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 58L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 59L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 60L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 61L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 62L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 63L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 64L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 65L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 66L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 67L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 68L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 69L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 70L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 71L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 72L);

            migrationBuilder.DeleteData(
                table: "BusinessUnitMasters",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "BusinessUnitMasters",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "ContextMenuMaster",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "ContextMenuMaster",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "ContextMenuMaster",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "ContextMenuMaster",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "ContextMenuMaster",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "ContextMenuMaster",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "ContextMenuMaster",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "ExpertiesMasters",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "ExpertiesMasters",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "ExpertiesMasters",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "ExpertiesMasters",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "ExpertiesMasters",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "ExpertiesMasters",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "ExpertiesMasters",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "ExpertiesMasters",
                keyColumn: "Id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "ExpertiesMasters",
                keyColumn: "Id",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                table: "RUMaster",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "RUMaster",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "RUMaster",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "RUMaster",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "RUMaster",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "RUMaster",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "RUMaster",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "RUMaster",
                keyColumn: "Id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "RUMaster",
                keyColumn: "Id",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                table: "RUMaster",
                keyColumn: "Id",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                table: "RUMaster",
                keyColumn: "Id",
                keyValue: 11L);

            migrationBuilder.DeleteData(
                table: "RUMaster",
                keyColumn: "Id",
                keyValue: 12L);

            migrationBuilder.DeleteData(
                table: "RUMaster",
                keyColumn: "Id",
                keyValue: 13L);

            migrationBuilder.DeleteData(
                table: "RUMaster",
                keyColumn: "Id",
                keyValue: 14L);

            migrationBuilder.DeleteData(
                table: "RUMaster",
                keyColumn: "Id",
                keyValue: 15L);

            migrationBuilder.DeleteData(
                table: "RUMaster",
                keyColumn: "Id",
                keyValue: 16L);

            migrationBuilder.DeleteData(
                table: "RUMaster",
                keyColumn: "Id",
                keyValue: 17L);

            migrationBuilder.DeleteData(
                table: "RUMaster",
                keyColumn: "Id",
                keyValue: 18L);

            migrationBuilder.DeleteData(
                table: "RUMaster",
                keyColumn: "Id",
                keyValue: 19L);

            migrationBuilder.DeleteData(
                table: "RUMaster",
                keyColumn: "Id",
                keyValue: 20L);

            migrationBuilder.DeleteData(
                table: "SMEGMaster",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "SMEGMaster",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "SMEGMaster",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "SMEGMaster",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "SMEGMaster",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "SMEGMaster",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "SMEGMaster",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "SMEGMaster",
                keyColumn: "Id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "SMEGMaster",
                keyColumn: "Id",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                table: "SMEGMaster",
                keyColumn: "Id",
                keyValue: 10L);
        }
    }
}
