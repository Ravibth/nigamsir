using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RMT.Configuration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialDBValuesV1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "ApplicationLevelSettings",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "IsActive", "Key", "ModifiedAt", "ModifiedBy", "Value" },
                values: new object[] { 1L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, "Weekends", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "0,6" });

            migrationBuilder.InsertData(
                table: "BusinessUnitMasters",
                columns: new[] { "Id", "BuId", "BusinessUnit", "CreatedAt", "CreatedBy", "IsActive", "ModifiedAt", "ModifiedBy" },
                values: new object[,]
                {
                    { 1L, "SG000010", "Deals", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System" },
                    { 2L, "SG000013", "ESG & Risk Consulting", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System" }
                });

            migrationBuilder.InsertData(
                table: "ConfigurationGroups",
                columns: new[] { "Id", "AllValue", "ConfigGroup", "ConfigGroupDisplay", "ConfigKey", "ConfigType", "CongigDisplayText", "CreatedAt", "CreatedBy", "IsActive", "IsAll", "ModifiedAt", "ModifiedBy", "ValueType" },
                values: new object[,]
                {
                    { 1L, "{\"activationStatus\":\"true\",\"noOfDays\":\"1\"}", "Resource_allocation_review", "Resource allocation review", "Resource_allocation_review", "EXPERTISE", "Resource allocation review", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, false, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "{\"activationStatus\":\"Boolean\",\"noOfDays\":\"Integer\"}" },
                    { 2L, "{\"activationStatus\":\"false\",\"noOfDays\":\"1\"}", "Resource_allocation_review", "Resource allocation review", "Resource_allocation_review", "BUSINESS_UNIT", "Resource allocation review", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, false, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "{\"activationStatus\":\"Boolean\",\"noOfDays\":\"Integer\"}" },
                    { 5L, "12", "No_of_days_where_project_is_available_in_Marketplace", "No of days where project is available in Marketplace", "No_of_days_where_project_is_available_in_Marketplace", "EXPERTISE", "No of days where project is available in Marketplace", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, false, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 6L, "15", "No_of_days_where_project_is_available_in_Marketplace", "No of days where project is available in Marketplace", "No_of_days_where_project_is_available_in_Marketplace", "BUSINESS_UNIT", "No of days where project is available in Marketplace", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, false, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 7L, "string", "string", "string", "string", "string", "string", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "string" },
                    { 8L, "1", "Weightage_for_parameters_for_System_Suggested_Requisition", "Weightage for parameters for System Suggested Requisition", "Location", "EXPERTISE", "Location", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 9L, "1", "Weightage_for_parameters_for_System_Suggested_Requisition", "Weightage for parameters for System Suggested Requisition", "Location", "BUSINESS_UNIT", "Location", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 10L, "8", "Weightage_for_parameters_for_System_Suggested_Requisition", "Weightage for parameters for System Suggested Requisition", "Expertise", "EXPERTISE", "Expertise", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 11L, "8", "Weightage_for_parameters_for_System_Suggested_Requisition", "Weightage for parameters for System Suggested Requisition", "Expertise", "BUSINESS_UNIT", "Expertise", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 12L, "6", "Weightage_for_parameters_for_System_Suggested_Requisition", "Weightage for parameters for System Suggested Requisition", "SMEG", "EXPERTISE", "SMEG", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 13L, "6", "Weightage_for_parameters_for_System_Suggested_Requisition", "Weightage for parameters for System Suggested Requisition", "SMEG", "BUSINESS_UNIT", "SMEG", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 14L, "4", "Weightage_for_parameters_for_System_Suggested_Requisition", "Weightage for parameters for System Suggested Requisition", "Revenue_Unit", "EXPERTISE", "Revenue Unit", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 15L, "4", "Weightage_for_parameters_for_System_Suggested_Requisition", "Weightage for parameters for System Suggested Requisition", "Revenue_Unit", "BUSINESS_UNIT", "Revenue Unit", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 16L, "7", "Weightage_for_parameters_for_System_Suggested_Requisition", "Weightage for parameters for System Suggested Requisition", "Sector", "EXPERTISE", "Sector", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, false, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 17L, "6", "Weightage_for_parameters_for_System_Suggested_Requisition", "Weightage for parameters for System Suggested Requisition", "Sector", "BUSINESS_UNIT", "Sector", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, false, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 18L, "2", "Weightage_for_parameters_for_System_Suggested_Requisition", "Weightage for parameters for System Suggested Requisition", "Same_client", "EXPERTISE", "Experience working with same client", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 19L, "2", "Weightage_for_parameters_for_System_Suggested_Requisition", "Weightage for parameters for System Suggested Requisition", "Same_client", "BUSINESS_UNIT", "Experience working with same client", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 20L, "10", "Weightage_for_parameters_for_System_Suggested_Requisition", "Weightage for parameters for System Suggested Requisition", "Sub_Industry", "EXPERTISE", "Sub Industry", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 21L, "10", "Weightage_for_parameters_for_System_Suggested_Requisition", "Weightage for parameters for System Suggested Requisition", "Sub_Industry", "BUSINESS_UNIT", "Sub Industry", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 22L, "9", "Threshold_no_of_days_for_notification_to_be_sent_to_Resource_Requestor_if_Project_status_is_not_WON", "Threshold no of days for notification to be sent to Resource Requestor if Project status is not WON", "Threshold_no_of_days_for_notification_to_be_sent_to_Resource_Requestor_if_Project_status_is_not_WON", "EXPERTISE", "Threshold no of days for notification to be sent to Resource Requestor if Project status is not WON", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, false, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 23L, "9", "Threshold_no_of_days_for_notification_to_be_sent_to_Resource_Requestor_if_Project_status_is_not_WON", "Threshold no of days for notification to be sent to Resource Requestor if Project status is not WON", "Threshold_no_of_days_for_notification_to_be_sent_to_Resource_Requestor_if_Project_status_is_not_WON", "BUSINESS_UNIT", "Threshold no of days for notification to be sent to Resource Requestor if Project status is not WON", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, false, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 24L, "6", "Weightage_for_parameters_for_System_Suggested_Requisition", "Weightage for parameters for System Suggested Requisition", "business_unit", "BUSINESS_UNIT", "Business Unit", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 25L, "9", "Weightage_for_parameters_for_System_Suggested_Requisition", "Weightage for parameters for System Suggested Requisition", "business_unit", "EXPERTISE", "Business Unit", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 26L, "12", "Maximum_number_of_items_for_employee_preference", "Maximum number of items for employee preference", "Maximum_number_of_items_for_employee_preference", "EXPERTISE", "Maximum number of items for employee preference", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, false, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 27L, "14", "Maximum_number_of_items_for_employee_preference", "Maximum number of items for employee preference", "Maximum_number_of_items_for_employee_preference", "BUSINESS_UNIT", "Maximum number of items for employee preference", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, false, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 28L, "8", "Requisition_form", "Weightage for parameters for System Suggested Requisition", "Expertise", "BUSINESS_UNIT", "Expertise", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 29L, "6", "Requisition_form", "Weightage for parameters for System Suggested Requisition", "SMEG", "BUSINESS_UNIT", "SMEG", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 30L, "1", "Requisition_form", "Requisition form", "Location", "EXPERTISE", "Location", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 31L, "8", "Requisition_form", "Requisition form", "Expertise", "EXPERTISE", "Expertise", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 32L, "6", "Requisition_form", "Requisition form", "SMEG", "EXPERTISE", "SMEG", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 33L, "4", "Requisition_form", "Requisition form", "Revenue_Unit", "EXPERTISE", "Revenue Unit", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 34L, "4", "Requisition_form", "Requisition form", "Revenue_Unit", "BUSINESS_UNIT", "Revenue Unit", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 35L, "7", "Requisition_form", "Requisition form", "Sector", "EXPERTISE", "Sector", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, false, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 36L, "2", "Requisition_form", "Requisition form", "Same_client", "EXPERTISE", "Experience working with same client", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 37L, "1", "Requisition_form", "Requisition form", "Location", "BUSINESS_UNIT", "Location", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 38L, "6", "Requisition_form", "Requisition form", "Sector", "BUSINESS_UNIT", "Sector", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, false, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 39L, "2", "Requisition_form", "Requisition form", "Same_client", "BUSINESS_UNIT", "Experience working with same client", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 40L, "10", "Requisition_form", "Requisition form", "Sub_Industry", "BUSINESS_UNIT", "Sub Industry", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 41L, "6", "Requisition_form", "Requisition form", "business_unit", "BUSINESS_UNIT", "Business Unit", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 42L, "10", "Requisition_form", "Requisition form", "Sub_Industry", "EXPERTISE", "Sub Industry", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 43L, "9", "Requisition_form", "Requisition form", "business_unit", "EXPERTISE", "Business Unit", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 44L, "5", "System_Suggestion_For_Requisition_Percentage_Match", "System Suggestion For Requisition Percentage Match", "System_Suggestion_For_Requisition_Percentage_Match", "EXPERTISE", "System Suggestion For Requisition Percentage Match", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 45L, "5", "System_Suggestion_For_Requisition_Percentage_Match", "System Suggestion For Requisition Percentage Match", "System_Suggestion_For_Requisition_Percentage_Match", "BUSINESS_UNIT", "System Suggestion For Requisition Percentage Match", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 48L, "{\"activationStatus\":\"true\",\"noOfDays\":\"2\"}", "Employee_Response_Review_process_reviewed_by_Resource_Requestor", "Employee Response Review process reviewed by Resource Requestor", "Employee_Response_Review_process_reviewed_by_Resource_Requestor", "EXPERTISE", "Employee Response Review process reviewed by Resource Requestor", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "{\"activationStatus\":\"Boolean\",\"noOfDays\":\"Integer\"}" },
                    { 49L, "{\"activationStatus\":\"true\",\"noOfDays\":\"2\"}", "Employee_Response_Review_process_reviewed_by_Resource_Requestor", "Employee Response Review process reviewed by Resource Requestor", "Employee_Response_Review_process_reviewed_by_Resource_Requestor", "BUSINESS_UNIT", "Employee Response Review process reviewed by Resource Requestor", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "{\"activationStatus\":\"Boolean\",\"noOfDays\":\"Integer\"}" },
                    { 52L, "5", "Number_of_days_for_employee_to_confirm_on_their_allocations", "Number of days for employee to confirm on their allocations", "Number_of_days_for_employee_to_confirm_on_their_allocations", "EXPERTISE", "Number of days for employee to confirm on their allocations", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 53L, "5", "Number_of_days_for_employee_to_confirm_on_their_allocations", "Number of days for employee to confirm on their allocations", "Number_of_days_for_employee_to_confirm_on_their_allocations", "BUSINESS_UNIT", "Number of days for employee to confirm on their allocations", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 56L, "5", "Maximum_number_of_parameters_that_can_be_selected_by_Employee_in_Preference_screen", "Maximum number of parameters that can be selected by Employee in Preference screen", "Maximum_number_of_parameters_that_can_be_selected_by_Employee_in_Preference_screen", "EXPERTISE", "Maximum number of parameters that can be selected by Employee in Preference screen", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 57L, "5", "Maximum_number_of_parameters_that_can_be_selected_by_Employee_in_Preference_screen", "Maximum number of parameters that can be selected by Employee in Preference screen", "Maximum_number_of_parameters_that_can_be_selected_by_Employee_in_Preference_screen", "BUSINESS_UNIT", "Maximum number of parameters that can be selected by Employee in Preference screen", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 58L, "80", "Amber_condition_for_Budget_Consumption", "Amber condition for Budget Consumption", "Amber_condition_for_Budget_Consumption", "EXPERTISE", "Amber condition for Budget Consumption", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 59L, "80", "Amber_condition_for_Budget_Consumption", "Amber condition for Budget Consumption", "Amber_condition_for_Budget_Consumption", "BUSINESS_UNIT", "Amber condition for Budget Consumption", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 60L, "80", "Alert_condition_for_Allocation_Cost", "Alert condition for Allocation Cost", "Alert_condition_for_Allocation_Cost", "EXPERTISE", "Alert condition for Allocation Cost", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 61L, "80", "Alert_condition_for_Allocation_Cost", "Alert condition for Allocation Cost", "Alert_condition_for_Allocation_Cost", "BUSINESS_UNIT", "Alert condition for Allocation Cost", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 62L, "80", "Alert_condition_for_Timesheet_Hours", "Alert condition for Timesheet Hours", "Alert_condition_for_Timesheet_Hours", "EXPERTISE", "Alert condition for Timesheet Hours", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 63L, "80", "Alert_condition_for_Timesheet_Hours", "Alert condition for Timesheet Hours", "Alert_condition_for_Timesheet_Hours", "BUSINESS_UNIT", "Alert condition for Timesheet Hours", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 64L, "2", "Requisition_form", "Requisition", "Skill", "BUSINESS_UNIT", "Skills", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 65L, "2", "Requisition_form", "Requisition", "Skill", "EXPERTISE", "Skills", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" }
                });

            migrationBuilder.InsertData(
                table: "ContextMenuMaster",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Description", "DisplayName", "InternalName", "IsActive", "ModifiedAt", "ModifiedBy", "Order" },
                values: new object[,]
                {
                    { 1L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "", "View Details", "View Details", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 10 },
                    { 2L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "", "Create Requisition", "Create Requisition", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 20 },
                    { 3L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "", "Bulk Upload", "Bulk Upload", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 30 },
                    { 4L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "", "Allocate Employee", "Allocate Employee", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 40 },
                    { 5L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "", "Allocate Same Team", "Allocate Same Team", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 50 },
                    { 6L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "", "Roll forward allocations", "Roll forward allocations", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 60 },
                    { 7L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "", "Move To Marketplace", "Move To Marketplace", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 70 },
                    { 8L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "", "Calender View", "Calender View", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 80 },
                    { 9L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "", "Allocations", "Allocations", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 90 },
                    { 10L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "", "Requisitions", "Requisitions", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 100 },
                    { 11L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "", "Assign New Code", "Assign New Code", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 110 }
                });

            migrationBuilder.InsertData(
                table: "ExpertiesMasters",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Experties", "ExpertiesId", "IsActive", "ModifiedAt", "ModifiedBy" },
                values: new object[,]
                {
                    { 1L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "NDO", "SL000060", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System" },
                    { 2L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Recovery & Reorganisation", "SL000015", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System" },
                    { 3L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Transaction Tax", "SL000043", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System" },
                    { 4L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Due Diligence", "SL000046", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System" },
                    { 5L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Governance Risk & Operations (GRO)", "SL000049", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System" },
                    { 6L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "NErcO", "SL000061", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System" },
                    { 7L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Cyber & IT Risk (Cyber)", "SL000022", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System" },
                    { 8L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "FS Risk", "SL000027", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System" },
                    { 9L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Forensic", "SL000021", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System" }
                });

            migrationBuilder.InsertData(
                table: "MenuMaster",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Description", "DisplayName", "InternalName", "IsActive", "IsDisplay", "Is_Expandable", "MenuType", "ModifiedAt", "ModifiedBy", "Order", "ParentId", "Path" },
                values: new object[,]
                {
                    { 1L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "", "Dashboard", "Dashboard", true, true, false, "", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 10, "", "/" },
                    { 2L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "", "Reports", "Reports", true, true, false, "", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 20, "", "/reports" },
                    { 3L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "", "Projects", "Projects Listing", true, true, false, "", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 30, "", "/projects" },
                    { 4L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "", "Pending Requests / My Tasks", "Pending Requests / My Tasks", true, true, false, "", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 40, "", "/myapproval" },
                    { 5L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "", "Configurations", "Configurations", true, true, false, "", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 50, "", "/configurations" },
                    { 6L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "", "User Management", "User Management", true, true, false, "", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 60, "", "/roles-permission" },
                    { 7L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "", "Marketplace", "Marketplace", true, true, false, "", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 70, "", "/marketplace" },
                    { 8L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "", "Alert", "Alert", true, true, false, "", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 80, "", "/notification" },
                    { 9L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "", "My View", "My View", true, true, false, "", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 90, "", "/myapproval" },
                    { 10L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "", "Manage", "Manage", true, true, false, "", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 4, "", "/manage" },
                    { 11L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "", "Employees", "Employees", true, false, false, "", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 2, "", "/employee" },
                    { 12L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "", "Create Requisition", "Create Requisition", true, false, false, "", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 6, "", "/create-requisition" },
                    { 13L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "", "Project Detailsn", "Project Details", true, false, false, "", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 8, "", "/project-details" },
                    { 14L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "", "Bulk Upload", "Bulk Upload", true, false, false, "", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 7, "", "/bulk-upload" },
                    { 15L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "", "Skills", "Skills", true, true, false, "", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 91, "", "/skillmaster" },
                    { 16L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "", "My skills", "My Skills", true, true, false, "", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 92, "", "/myskill" }
                });

            migrationBuilder.InsertData(
                table: "RUMaster",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "IsActive", "ModifiedAt", "ModifiedBy", "RU", "RUId" },
                values: new object[,]
                {
                    { 1L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "NC NDO", "RV007793" },
                    { 2L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Asset tracing & recovery", "RV007630" },
                    { 3L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "NC Transaction Tax", "RV007599" },
                    { 4L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Tax Compliance", "RV007603" },
                    { 5L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Tax Litigation", "RV007604" },
                    { 6L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Tax Assessment", "RV007605" },
                    { 7L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Tax Attest", "RV007606" },
                    { 8L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Tax Regulatory", "RV007607" },
                    { 9L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Tax Advisory", "RV007608" },
                    { 10L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "NC Due Diligence", "RV007612" },
                    { 11L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "GRO - Analytics / CCM / RPA", "RV007749" },
                    { 12L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "(CSRS) - Energy Advisory", "RV007750" },
                    { 13L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "GRO - Internal Audit", "RV007751" },
                    { 14L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "GRO- Standard operating procedure", "RV007752" },
                    { 15L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "NC Governance Risk & Operations (GRO)", "RV007753" },
                    { 16L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "NC NErcO", "RV007794" },
                    { 17L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "NC Global Delivery - Cyber", "RV007801" },
                    { 18L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "NC Global Delivery - FS Risk", "RV007802" },
                    { 19L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "NC Forensic", "RV007808" },
                    { 20L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "NC GD UK JV", "RV007812" }
                });

            migrationBuilder.InsertData(
                table: "SMEGMaster",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "IsActive", "ModifiedAt", "ModifiedBy", "SMEG", "SMEGId" },
                values: new object[,]
                {
                    { 1L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "NC NDO", "IS001165" },
                    { 2L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Recovery & Reorganisation", "IS001124" },
                    { 3L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Transaction Tax", "IS001116" },
                    { 4L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Due Diligence", "IS001120" },
                    { 5L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "GRO", "IS001154" },
                    { 6L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "NC NErcO", "IS001166" },
                    { 7L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "GD - Cyber", "IS001148" },
                    { 8L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "GD - FS Risk", "IS001153" },
                    { 9L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Forensic", "IS001150" },
                    { 10L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "GD UK JV - FS Risk", "IS001152" }
                });

            migrationBuilder.InsertData(
                table: "Bu_Experties_Grps",
                columns: new[] { "Id", "BusinessUnitId", "BusinessUnitName", "CreatedAt", "CreatedBy", "ExpertiesId", "ExpertiesName", "IsActive", "ModifiedAt", "ModifiedBy", "RUId", "SMEGId" },
                values: new object[,]
                {
                    { 1L, 1L, "Deals", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 1L, "NDO", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 1L, 1L },
                    { 2L, 1L, "Deals", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 2L, "Recovery & Reorganisation", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 2L, 2L },
                    { 3L, 1L, "Deals", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 3L, "Transaction Tax", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 3L, 3L },
                    { 10L, 1L, "Deals", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 4L, "Due Diligence", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 10L, 4L },
                    { 11L, 2L, "ESG & Risk Consulting", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 5L, "Governance Risk & Operations (GRO)", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 11L, 5L },
                    { 16L, 2L, "ESG & Risk Consulting", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 6L, "NErcO", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 16L, 6L },
                    { 17L, 2L, "ESG & Risk Consulting", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 7L, "Cyber & IT Risk (Cyber)", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 17L, 7L },
                    { 18L, 2L, "ESG & Risk Consulting", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 8L, "FS Risk", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 18L, 8L },
                    { 19L, 2L, "ESG & Risk Consulting", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 9L, "Forensic", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", 19L, 9L }
                });

            migrationBuilder.InsertData(
                table: "RoleContextMenu",
                columns: new[] { "Id", "ContextMenuId", "CreatedAt", "CreatedBy", "IsActive", "ModifiedAt", "ModifiedBy", "Role" },
                values: new object[,]
                {
                    { 1L, 1L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Resource Requestor" },
                    { 2L, 2L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Resource Requestor" },
                    { 3L, 3L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Resource Requestor" },
                    { 4L, 4L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Resource Requestor" },
                    { 5L, 5L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Resource Requestor" },
                    { 6L, 6L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Resource Requestor" },
                    { 7L, 7L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Resource Requestor" },
                    { 8L, 1L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Additional EL" },
                    { 9L, 2L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Additional EL" },
                    { 10L, 3L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Additional EL" },
                    { 11L, 4L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Additional EL" },
                    { 12L, 5L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Additional EL" },
                    { 13L, 6L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Additional EL" },
                    { 14L, 7L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Additional EL" },
                    { 15L, 1L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Delegate" },
                    { 16L, 2L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Delegate" },
                    { 17L, 3L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Delegate" },
                    { 18L, 4L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Delegate" },
                    { 19L, 5L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Delegate" },
                    { 20L, 6L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Delegate" },
                    { 21L, 7L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Delegate" },
                    { 22L, 1L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Admin" },
                    { 23L, 2L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Admin" },
                    { 24L, 3L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Admin" },
                    { 25L, 4L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Admin" },
                    { 26L, 5L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Admin" },
                    { 27L, 6L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Admin" },
                    { 28L, 7L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Admin" },
                    { 29L, 1L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Employee" },
                    { 30L, 2L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Employee" },
                    { 31L, 3L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Employee" },
                    { 32L, 4L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Employee" },
                    { 33L, 5L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Employee" },
                    { 34L, 6L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Employee" },
                    { 35L, 7L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Employee" },
                    { 36L, 1L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Request Reviewer" },
                    { 37L, 2L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Request Reviewer" },
                    { 38L, 3L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Request Reviewer" },
                    { 39L, 4L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Request Reviewer" },
                    { 40L, 5L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Request Reviewer" },
                    { 41L, 6L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Request Reviewer" },
                    { 42L, 7L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Request Reviewer" },
                    { 43L, 1L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Additional Delegate" },
                    { 44L, 2L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Additional Delegate" },
                    { 45L, 3L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Additional Delegate" },
                    { 46L, 4L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Additional Delegate" },
                    { 47L, 5L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Additional Delegate" },
                    { 48L, 6L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Additional Delegate" },
                    { 49L, 7L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Additional Delegate" },
                    { 50L, 1L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Leader" },
                    { 51L, 2L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Leader" },
                    { 52L, 3L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Leader" },
                    { 53L, 4L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Leader" },
                    { 54L, 5L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Leader" },
                    { 55L, 6L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Leader" },
                    { 56L, 7L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Leader" },
                    { 57L, 8L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Resource Requestor" },
                    { 58L, 8L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Additional EL" },
                    { 59L, 8L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Delegate" },
                    { 60L, 8L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Admin" },
                    { 61L, 8L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Employee" },
                    { 62L, 8L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Request Reviewer" },
                    { 63L, 8L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Additional Delegate" },
                    { 64L, 9L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Resource Requestor" },
                    { 65L, 9L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Additional EL" },
                    { 66L, 9L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Delegate" },
                    { 67L, 9L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Request Reviewer" },
                    { 68L, 9L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Employee" },
                    { 69L, 9L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Admin" },
                    { 70L, 9L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Additional Delegate" },
                    { 71L, 10L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Resource Requestor" },
                    { 72L, 10L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Additional EL" },
                    { 73L, 10L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Delegate" },
                    { 74L, 10L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Request Reviewer" },
                    { 75L, 10L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Employee" },
                    { 76L, 10L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Admin" },
                    { 77L, 10L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Additional Delegate" },
                    { 78L, 11L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Resource Requestor" },
                    { 79L, 11L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Additional EL" },
                    { 80L, 11L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Delegate" },
                    { 81L, 11L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Request Reviewer" },
                    { 82L, 11L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Employee" },
                    { 83L, 11L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Admin" },
                    { 84L, 11L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Additional Delegate" }
                });

            migrationBuilder.InsertData(
                table: "RoleMenu",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "IsActive", "MenuId", "ModifiedAt", "ModifiedBy", "Role" },
                values: new object[,]
                {
                    { 1L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 1L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Employee" },
                    { 2L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 2L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Employee" },
                    { 3L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 3L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Employee" },
                    { 4L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 4L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Employee" },
                    { 5L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 5L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Employee" },
                    { 6L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 6L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Employee" },
                    { 7L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 7L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Employee" },
                    { 8L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 8L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Employee" },
                    { 9L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 9L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Employee" },
                    { 10L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 10L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Employee" },
                    { 11L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 11L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Employee" },
                    { 12L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 12L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Employee" },
                    { 13L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 13L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Employee" },
                    { 14L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 14L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Employee" },
                    { 15L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 1L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Resource Requestor" },
                    { 16L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 2L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Resource Requestor" },
                    { 17L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 3L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Resource Requestor" },
                    { 18L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 4L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Resource Requestor" },
                    { 19L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 5L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Resource Requestor" },
                    { 20L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 6L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Resource Requestor" },
                    { 21L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 7L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Resource Requestor" },
                    { 22L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 8L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Resource Requestor" },
                    { 23L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 9L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Resource Requestor" },
                    { 24L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 10L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Resource Requestor" },
                    { 25L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 11L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Resource Requestor" },
                    { 26L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 12L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Resource Requestor" },
                    { 27L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 13L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Resource Requestor" },
                    { 28L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 14L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Resource Requestor" },
                    { 29L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 1L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Delegate" },
                    { 30L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 2L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Delegate" },
                    { 31L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 3L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Delegate" },
                    { 32L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 4L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Delegate" },
                    { 33L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 5L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Delegate" },
                    { 34L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 6L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Delegate" },
                    { 35L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 7L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Delegate" },
                    { 36L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 8L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Delegate" },
                    { 37L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 9L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Delegate" },
                    { 38L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 10L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Delegate" },
                    { 39L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 11L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Delegate" },
                    { 40L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 12L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Delegate" },
                    { 41L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 13L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Delegate" },
                    { 42L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 14L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Delegate" },
                    { 43L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 1L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Request Reviewer" },
                    { 44L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 2L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Request Reviewer" },
                    { 45L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 3L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Request Reviewer" },
                    { 46L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 4L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Request Reviewer" },
                    { 47L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 5L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Request Reviewer" },
                    { 48L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 6L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Request Reviewer" },
                    { 49L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 7L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Request Reviewer" },
                    { 50L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 8L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Request Reviewer" },
                    { 51L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 9L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Request Reviewer" },
                    { 52L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 10L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Request Reviewer" },
                    { 53L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 11L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Request Reviewer" },
                    { 54L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 12L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Request Reviewer" },
                    { 55L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 13L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Request Reviewer" },
                    { 56L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 14L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Request Reviewer" },
                    { 57L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 1L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Additional EL" },
                    { 58L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 2L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Additional EL" },
                    { 59L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 3L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Additional EL" },
                    { 60L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 4L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Additional EL" },
                    { 61L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 5L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Additional EL" },
                    { 62L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 6L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Additional EL" },
                    { 63L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 7L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Additional EL" },
                    { 64L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 8L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Additional EL" },
                    { 65L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 9L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Additional EL" },
                    { 66L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 10L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Additional EL" },
                    { 67L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 11L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Additional EL" },
                    { 68L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 12L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Additional EL" },
                    { 69L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 13L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Additional EL" },
                    { 70L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 14L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Additional EL" },
                    { 71L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 1L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Additional Delegate" },
                    { 72L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 2L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Additional Delegate" },
                    { 73L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 3L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Additional Delegate" },
                    { 74L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 4L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Additional Delegate" },
                    { 75L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 5L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Additional Delegate" },
                    { 76L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 6L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Additional Delegate" },
                    { 77L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 7L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Additional Delegate" },
                    { 78L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 8L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Additional Delegate" },
                    { 79L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 9L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Additional Delegate" },
                    { 80L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 10L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Additional Delegate" },
                    { 81L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 11L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Additional Delegate" },
                    { 82L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 12L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Additional Delegate" },
                    { 83L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 13L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Additional Delegate" },
                    { 84L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 14L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Additional Delegate" },
                    { 85L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 1L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Admin" },
                    { 86L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 2L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Admin" },
                    { 87L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 3L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Admin" },
                    { 88L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 4L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Admin" },
                    { 89L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 5L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Admin" },
                    { 90L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 6L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Admin" },
                    { 91L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 7L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Admin" },
                    { 92L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 8L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Admin" },
                    { 93L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 9L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Admin" },
                    { 94L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 10L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Admin" },
                    { 95L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 11L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Admin" },
                    { 96L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 12L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Admin" },
                    { 97L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 13L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Admin" },
                    { 98L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 14L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Admin" },
                    { 99L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 1L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Leader" },
                    { 100L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 2L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Leader" },
                    { 101L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 3L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Leader" },
                    { 102L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 4L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Leader" },
                    { 103L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 5L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Leader" },
                    { 104L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 6L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Leader" },
                    { 105L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 7L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Leader" },
                    { 106L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 8L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Leader" },
                    { 107L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 9L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Leader" },
                    { 108L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 10L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Leader" },
                    { 109L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 11L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Leader" },
                    { 110L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 12L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Leader" },
                    { 111L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 13L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Leader" },
                    { 112L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 14L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Leader" },
                    { 113L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 15L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Leader" },
                    { 114L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 15L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Admin" },
                    { 115L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 15L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Additional Delegate" },
                    { 116L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", false, 15L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Additional EL" },
                    { 117L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 15L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Request Reviewer" },
                    { 118L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 15L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Delegate" },
                    { 119L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 15L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Employee" },
                    { 120L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 16L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Leader" },
                    { 121L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 16L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Admin" },
                    { 122L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 16L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Additional Delegate" },
                    { 123L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 16L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Additional EL" },
                    { 124L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 16L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Request Reviewer" },
                    { 125L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 16L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Delegate" },
                    { 126L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 16L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Employee" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ApplicationLevelSettings",
                keyColumn: "Id",
                keyValue: 1L);

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
                keyValue: 10L);

            migrationBuilder.DeleteData(
                table: "Bu_Experties_Grps",
                keyColumn: "Id",
                keyValue: 11L);

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
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 28L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 29L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 30L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 31L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 32L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 33L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 34L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 35L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 36L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 37L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 38L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 39L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 40L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 41L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 42L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 43L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 44L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 45L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 48L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 49L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 52L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 53L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 56L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 57L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 58L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 59L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 60L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 61L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 62L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 63L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 64L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 65L);

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
                keyValue: 20L);

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
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 57L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 58L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 59L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 60L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 61L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 62L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 63L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 64L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 65L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 66L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 67L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 68L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 69L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 70L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 71L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 72L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 73L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 74L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 75L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 76L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 77L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 78L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 79L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 80L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 81L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 82L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 83L);

            migrationBuilder.DeleteData(
                table: "RoleContextMenu",
                keyColumn: "Id",
                keyValue: 84L);

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
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 73L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 74L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 75L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 76L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 77L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 78L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 79L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 80L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 81L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 82L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 83L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 84L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 85L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 86L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 87L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 88L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 89L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 90L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 91L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 92L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 93L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 94L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 95L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 96L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 97L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 98L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 99L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 100L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 101L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 102L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 103L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 104L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 105L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 106L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 107L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 108L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 109L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 110L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 111L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 112L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 113L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 114L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 115L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 116L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 117L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 118L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 119L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 120L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 121L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 122L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 123L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 124L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 125L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 126L);

            migrationBuilder.DeleteData(
                table: "SMEGMaster",
                keyColumn: "Id",
                keyValue: 10L);

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
                table: "ContextMenuMaster",
                keyColumn: "Id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "ContextMenuMaster",
                keyColumn: "Id",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                table: "ContextMenuMaster",
                keyColumn: "Id",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                table: "ContextMenuMaster",
                keyColumn: "Id",
                keyValue: 11L);

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
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 11L);

            migrationBuilder.DeleteData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 12L);

            migrationBuilder.DeleteData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 13L);

            migrationBuilder.DeleteData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 14L);

            migrationBuilder.DeleteData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 15L);

            migrationBuilder.DeleteData(
                table: "MenuMaster",
                keyColumn: "Id",
                keyValue: 16L);

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
                keyValue: 10L);

            migrationBuilder.DeleteData(
                table: "RUMaster",
                keyColumn: "Id",
                keyValue: 11L);

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
        }
    }
}
