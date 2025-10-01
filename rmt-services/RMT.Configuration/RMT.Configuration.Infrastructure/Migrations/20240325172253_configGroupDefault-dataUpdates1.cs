using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RMT.Configuration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class configGroupDefaultdataUpdates1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<string>(
            //    name: "ConfigGroup",
            //    table: "ConfigurationGroups",
            //    type: "text",
            //    nullable: false,
            //    defaultValue: "");

            //migrationBuilder.AddColumn<string>(
            //    name: "ConfigGroupDisplay",
            //    table: "ConfigurationGroups",
            //    type: "text",
            //    nullable: false,
            //    defaultValue: "");

            //migrationBuilder.AddColumn<string>(
            //    name: "ConfigKey",
            //    table: "ConfigurationGroups",
            //    type: "text",
            //    nullable: false,
            //    defaultValue: "");

            //migrationBuilder.AddColumn<string>(
            //    name: "CongigDisplayText",
            //    table: "ConfigurationGroups",
            //    type: "text",
            //    nullable: false,
            //    defaultValue: "");

            //migrationBuilder.AddColumn<string>(
            //    name: "ValueType",
            //    table: "ConfigurationGroups",
            //    type: "text",
            //    nullable: false,
            //    defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ConfigurationGroupMasters",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "ConfigurationGroupMasters",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedAt",
                table: "ConfigurationGroupMasters",
                type: "timestamp without time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "ConfigurationGroupMasters",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "ConfigurationGroupMasters",
                columns: new[] { "Id", "ConfigGroup", "ConfigGroupDisplay", "ConfigKey", "CongigDisplayText", "CreatedAt", "CreatedBy", "DefaultValue", "IsActive", "ModifiedAt", "ModifiedBy", "ValueType" },
                values: new object[,]
                {
                    { 1L, "Resource_allocation_review", "Resource allocation review", "Resource_allocation_review", "Resource allocation review", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "{\"activationStatus\":\"true\",\"noOfDays\":\"1\"}", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "{\"activationStatus\":\"Boolean\",\"noOfDays\":\"Integer\"}" },
                    { 5L, "No_of_days_where_project_is_available_in_Marketplace", "No of days where project is available in Marketplace", "No_of_days_where_project_is_available_in_Marketplace", "No of days where project is available in Marketplace", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "15", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 8L, "Weightage_for_parameters_for_System_Suggested_Requisition", "Weightage for parameters for System Suggested Requisition", "Location", "Location", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "1", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 10L, "Weightage_for_parameters_for_System_Suggested_Requisition", "Weightage for parameters for System Suggested Requisition", "Expertise", "Expertise", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "4", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 12L, "Weightage_for_parameters_for_System_Suggested_Requisition", "Weightage for parameters for System Suggested Requisition", "SMEG", "SMEG", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "4", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 14L, "Weightage_for_parameters_for_System_Suggested_Requisition", "Weightage for parameters for System Suggested Requisition", "Revenue_Unit", "Revenue Unit", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "4", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 16L, "Weightage_for_parameters_for_System_Suggested_Requisition", "Weightage for parameters for System Suggested Requisition", "Sector", "Sector", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "4", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 18L, "Weightage_for_parameters_for_System_Suggested_Requisition", "Weightage for parameters for System Suggested Requisition", "Same_client", "Experience working with same client", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "4", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 20L, "Weightage_for_parameters_for_System_Suggested_Requisition", "Weightage for parameters for System Suggested Requisition", "Sub_Industry", "Sub Industry", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "4", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 22L, "Threshold_no_of_days_for_notification_to_be_sent_to_Resource_Requestor_if_Project_status_is_not_WON", "Threshold no of days for notification to be sent to Resource Requestor if Project status is not WON", "Threshold_no_of_days_for_notification_to_be_sent_to_Resource_Requestor_if_Project_status_is_not_WON", "Threshold no of days for notification to be sent to Resource Requestor if Project status is not WON", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "4", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 24L, "Weightage_for_parameters_for_System_Suggested_Requisition", "Weightage for parameters for System Suggested Requisition", "business_unit", "Business Unit", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "4", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 26L, "Maximum_number_of_items_for_employee_preference", "Maximum number of items for employee preference", "Maximum_number_of_items_for_employee_preference", "Maximum number of items for employee preference", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "10", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 28L, "Requisition_form", "Weightage for parameters for System Suggested Requisition", "Expertise", "Expertise", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "4", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 29L, "Requisition_form", "Weightage for parameters for System Suggested Requisition", "SMEG", "SMEG", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "4", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 30L, "Requisition_form", "Requisition form", "Location", "Location", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "4", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 31L, "Requisition_form", "Requisition form", "Expertise", "Expertise", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "4", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 32L, "Requisition_form", "Requisition form", "SMEG", "SMEG", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "4", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 33L, "Requisition_form", "Requisition form", "Revenue_Unit", "Revenue Unit", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "4", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 35L, "Requisition_form", "Requisition form", "Sector", "Sector", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "4", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 36L, "Requisition_form", "Requisition form", "Same_client", "Experience working with same client", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "4", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 40L, "Requisition_form", "Requisition form", "Sub_Industry", "Sub Industry", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "4", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 41L, "Requisition_form", "Requisition form", "business_unit", "Business Unit", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "4", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 44L, "System_Suggestion_For_Requisition_Percentage_Match", "System Suggestion For Requisition Percentage Match", "System_Suggestion_For_Requisition_Percentage_Match", "System Suggestion For Requisition Percentage Match", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "10", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 48L, "Employee_Response_Review_process_reviewed_by_Resource_Requestor", "Employee Response Review process reviewed by Resource Requestor", "Employee_Response_Review_process_reviewed_by_Resource_Requestor", "Employee Response Review process reviewed by Resource Requestor", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "{\"activationStatus\":\"true\",\"noOfDays\":\"1\"}", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "{\"activationStatus\":\"Boolean\",\"noOfDays\":\"Integer\"}" },
                    { 52L, "Number_of_days_for_employee_to_confirm_on_their_allocations", "Number of days for employee to confirm on their allocations", "Number_of_days_for_employee_to_confirm_on_their_allocations", "Number of days for employee to confirm on their allocations", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "10", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 56L, "Maximum_number_of_parameters_that_can_be_selected_by_Employee_in_Preference_screen", "Maximum number of parameters that can be selected by Employee in Preference screen", "Maximum_number_of_parameters_that_can_be_selected_by_Employee_in_Preference_screen", "Maximum number of parameters that can be selected by Employee in Preference screen", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "5", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 58L, "Amber_condition_for_Budget_Consumption", "Amber condition for Budget Consumption", "Amber_condition_for_Budget_Consumption", "Amber condition for Budget Consumption", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "80", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 60L, "Alert_condition_for_Allocation_Cost", "Alert condition for Allocation Cost", "Alert_condition_for_Allocation_Cost", "Alert condition for Allocation Cost", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "80", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 62L, "Alert_condition_for_Timesheet_Hours", "Alert condition for Timesheet Hours", "Alert_condition_for_Timesheet_Hours", "Alert condition for Timesheet Hours", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "80", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 64L, "Requisition_form", "Requisition", "Skill", "Skills", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "2", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 70L, "Requisition_form", "Requisition form", "Skills", "Skills", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "10", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 72L, "Weightage_for_parameters_for_System_Suggested_Requisition", "Weightage for parameters for System Suggested Requisition", "Skills", "Skills", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "9", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "INTEGER" },
                    { 74L, "Permission_for_Additional_EL", "Permission for Additional EL", "Permission_for_Additional_EL", "Permission for Additional EL", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "true", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "BOOLEAN" },
                    { 76L, "Permission_for_Additional_Delegate", "Permission for Additional Delegate", "Permission_for_Additional_Delegate", "Permission for Additional Delegate", new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "true", true, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "BOOLEAN" }
                });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "ConfigGroup", "ConfigGroupDisplay", "ConfigKey", "CongigDisplayText", "ValueType" },
                values: new object[] { "Resource_allocation_review", "Resource allocation review", "Resource_allocation_review", "Resource allocation review", "{\"activationStatus\":\"Boolean\",\"noOfDays\":\"Integer\"}" });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "ConfigGroup", "ConfigGroupDisplay", "ConfigKey", "CongigDisplayText", "ValueType" },
                values: new object[] { "Resource_allocation_review", "Resource allocation review", "Resource_allocation_review", "Resource allocation review", "{\"activationStatus\":\"Boolean\",\"noOfDays\":\"Integer\"}" });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "ConfigGroup", "ConfigGroupDisplay", "ConfigKey", "CongigDisplayText", "ValueType" },
                values: new object[] { "No_of_days_where_project_is_available_in_Marketplace", "No of days where project is available in Marketplace", "No_of_days_where_project_is_available_in_Marketplace", "No of days where project is available in Marketplace", "INTEGER" });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "ConfigGroup", "ConfigGroupDisplay", "ConfigKey", "CongigDisplayText", "ValueType" },
                values: new object[] { "No_of_days_where_project_is_available_in_Marketplace", "No of days where project is available in Marketplace", "No_of_days_where_project_is_available_in_Marketplace", "No of days where project is available in Marketplace", "INTEGER" });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "ConfigGroup", "ConfigGroupDisplay", "ConfigKey", "CongigDisplayText", "ValueType" },
                values: new object[] { "string", "string", "string", "string", "string" });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "ConfigGroup", "ConfigGroupDisplay", "ConfigKey", "CongigDisplayText", "ValueType" },
                values: new object[] { "Weightage_for_parameters_for_System_Suggested_Requisition", "Weightage for parameters for System Suggested Requisition", "Location", "Location", "INTEGER" });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "ConfigGroup", "ConfigGroupDisplay", "ConfigKey", "CongigDisplayText", "ValueType" },
                values: new object[] { "Weightage_for_parameters_for_System_Suggested_Requisition", "Weightage for parameters for System Suggested Requisition", "Location", "Location", "INTEGER" });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "ConfigGroup", "ConfigGroupDisplay", "ConfigKey", "CongigDisplayText", "ValueType" },
                values: new object[] { "Weightage_for_parameters_for_System_Suggested_Requisition", "Weightage for parameters for System Suggested Requisition", "Expertise", "Expertise", "INTEGER" });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 11L,
                columns: new[] { "ConfigGroup", "ConfigGroupDisplay", "ConfigKey", "CongigDisplayText", "ValueType" },
                values: new object[] { "Weightage_for_parameters_for_System_Suggested_Requisition", "Weightage for parameters for System Suggested Requisition", "Expertise", "Expertise", "INTEGER" });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 12L,
                columns: new[] { "ConfigGroup", "ConfigGroupDisplay", "ConfigKey", "CongigDisplayText", "ValueType" },
                values: new object[] { "Weightage_for_parameters_for_System_Suggested_Requisition", "Weightage for parameters for System Suggested Requisition", "SMEG", "SMEG", "INTEGER" });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 13L,
                columns: new[] { "ConfigGroup", "ConfigGroupDisplay", "ConfigKey", "CongigDisplayText", "ValueType" },
                values: new object[] { "Weightage_for_parameters_for_System_Suggested_Requisition", "Weightage for parameters for System Suggested Requisition", "SMEG", "SMEG", "INTEGER" });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 14L,
                columns: new[] { "ConfigGroup", "ConfigGroupDisplay", "ConfigKey", "CongigDisplayText", "ValueType" },
                values: new object[] { "Weightage_for_parameters_for_System_Suggested_Requisition", "Weightage for parameters for System Suggested Requisition", "Revenue_Unit", "Revenue Unit", "INTEGER" });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 15L,
                columns: new[] { "ConfigGroup", "ConfigGroupDisplay", "ConfigKey", "CongigDisplayText", "ValueType" },
                values: new object[] { "Weightage_for_parameters_for_System_Suggested_Requisition", "Weightage for parameters for System Suggested Requisition", "Revenue_Unit", "Revenue Unit", "INTEGER" });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 16L,
                columns: new[] { "ConfigGroup", "ConfigGroupDisplay", "ConfigKey", "CongigDisplayText", "ValueType" },
                values: new object[] { "Weightage_for_parameters_for_System_Suggested_Requisition", "Weightage for parameters for System Suggested Requisition", "Sector", "Sector", "INTEGER" });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 17L,
                columns: new[] { "ConfigGroup", "ConfigGroupDisplay", "ConfigKey", "CongigDisplayText", "ValueType" },
                values: new object[] { "Weightage_for_parameters_for_System_Suggested_Requisition", "Weightage for parameters for System Suggested Requisition", "Sector", "Sector", "INTEGER" });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 18L,
                columns: new[] { "ConfigGroup", "ConfigGroupDisplay", "ConfigKey", "CongigDisplayText", "ValueType" },
                values: new object[] { "Weightage_for_parameters_for_System_Suggested_Requisition", "Weightage for parameters for System Suggested Requisition", "Same_client", "Experience working with same client", "INTEGER" });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 19L,
                columns: new[] { "ConfigGroup", "ConfigGroupDisplay", "ConfigKey", "CongigDisplayText", "ValueType" },
                values: new object[] { "Weightage_for_parameters_for_System_Suggested_Requisition", "Weightage for parameters for System Suggested Requisition", "Same_client", "Experience working with same client", "INTEGER" });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 20L,
                columns: new[] { "ConfigGroup", "ConfigGroupDisplay", "ConfigKey", "CongigDisplayText", "ValueType" },
                values: new object[] { "Weightage_for_parameters_for_System_Suggested_Requisition", "Weightage for parameters for System Suggested Requisition", "Sub_Industry", "Sub Industry", "INTEGER" });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 21L,
                columns: new[] { "ConfigGroup", "ConfigGroupDisplay", "ConfigKey", "CongigDisplayText", "ValueType" },
                values: new object[] { "Weightage_for_parameters_for_System_Suggested_Requisition", "Weightage for parameters for System Suggested Requisition", "Sub_Industry", "Sub Industry", "INTEGER" });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 22L,
                columns: new[] { "ConfigGroup", "ConfigGroupDisplay", "ConfigKey", "CongigDisplayText", "ValueType" },
                values: new object[] { "Threshold_no_of_days_for_notification_to_be_sent_to_Resource_Requestor_if_Project_status_is_not_WON", "Threshold no of days for notification to be sent to Resource Requestor if Project status is not WON", "Threshold_no_of_days_for_notification_to_be_sent_to_Resource_Requestor_if_Project_status_is_not_WON", "Threshold no of days for notification to be sent to Resource Requestor if Project status is not WON", "INTEGER" });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 23L,
                columns: new[] { "ConfigGroup", "ConfigGroupDisplay", "ConfigKey", "CongigDisplayText", "ValueType" },
                values: new object[] { "Threshold_no_of_days_for_notification_to_be_sent_to_Resource_Requestor_if_Project_status_is_not_WON", "Threshold no of days for notification to be sent to Resource Requestor if Project status is not WON", "Threshold_no_of_days_for_notification_to_be_sent_to_Resource_Requestor_if_Project_status_is_not_WON", "Threshold no of days for notification to be sent to Resource Requestor if Project status is not WON", "INTEGER" });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 24L,
                columns: new[] { "ConfigGroup", "ConfigGroupDisplay", "ConfigKey", "CongigDisplayText", "ValueType" },
                values: new object[] { "Weightage_for_parameters_for_System_Suggested_Requisition", "Weightage for parameters for System Suggested Requisition", "business_unit", "Business Unit", "INTEGER" });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 25L,
                columns: new[] { "ConfigGroup", "ConfigGroupDisplay", "ConfigKey", "CongigDisplayText", "ValueType" },
                values: new object[] { "Weightage_for_parameters_for_System_Suggested_Requisition", "Weightage for parameters for System Suggested Requisition", "business_unit", "Business Unit", "INTEGER" });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 26L,
                columns: new[] { "ConfigGroup", "ConfigGroupDisplay", "ConfigKey", "CongigDisplayText", "ValueType" },
                values: new object[] { "Maximum_number_of_items_for_employee_preference", "Maximum number of items for employee preference", "Maximum_number_of_items_for_employee_preference", "Maximum number of items for employee preference", "INTEGER" });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 27L,
                columns: new[] { "ConfigGroup", "ConfigGroupDisplay", "ConfigKey", "CongigDisplayText", "ValueType" },
                values: new object[] { "Maximum_number_of_items_for_employee_preference", "Maximum number of items for employee preference", "Maximum_number_of_items_for_employee_preference", "Maximum number of items for employee preference", "INTEGER" });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 28L,
                columns: new[] { "ConfigGroup", "ConfigGroupDisplay", "ConfigKey", "CongigDisplayText", "ValueType" },
                values: new object[] { "Requisition_form", "Weightage for parameters for System Suggested Requisition", "Expertise", "Expertise", "INTEGER" });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 29L,
                columns: new[] { "ConfigGroup", "ConfigGroupDisplay", "ConfigKey", "CongigDisplayText", "ValueType" },
                values: new object[] { "Requisition_form", "Weightage for parameters for System Suggested Requisition", "SMEG", "SMEG", "INTEGER" });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 30L,
                columns: new[] { "ConfigGroup", "ConfigGroupDisplay", "ConfigKey", "CongigDisplayText", "ValueType" },
                values: new object[] { "Requisition_form", "Requisition form", "Location", "Location", "INTEGER" });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 31L,
                columns: new[] { "ConfigGroup", "ConfigGroupDisplay", "ConfigKey", "CongigDisplayText", "ValueType" },
                values: new object[] { "Requisition_form", "Requisition form", "Expertise", "Expertise", "INTEGER" });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 32L,
                columns: new[] { "ConfigGroup", "ConfigGroupDisplay", "ConfigKey", "CongigDisplayText", "ValueType" },
                values: new object[] { "Requisition_form", "Requisition form", "SMEG", "SMEG", "INTEGER" });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 33L,
                columns: new[] { "ConfigGroup", "ConfigGroupDisplay", "ConfigKey", "CongigDisplayText", "ValueType" },
                values: new object[] { "Requisition_form", "Requisition form", "Revenue_Unit", "Revenue Unit", "INTEGER" });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 34L,
                columns: new[] { "ConfigGroup", "ConfigGroupDisplay", "ConfigKey", "CongigDisplayText", "ValueType" },
                values: new object[] { "Requisition_form", "Requisition form", "Revenue_Unit", "Revenue Unit", "INTEGER" });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 35L,
                columns: new[] { "ConfigGroup", "ConfigGroupDisplay", "ConfigKey", "CongigDisplayText", "ValueType" },
                values: new object[] { "Requisition_form", "Requisition form", "Sector", "Sector", "INTEGER" });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 36L,
                columns: new[] { "ConfigGroup", "ConfigGroupDisplay", "ConfigKey", "CongigDisplayText", "ValueType" },
                values: new object[] { "Requisition_form", "Requisition form", "Same_client", "Experience working with same client", "INTEGER" });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 37L,
                columns: new[] { "ConfigGroup", "ConfigGroupDisplay", "ConfigKey", "CongigDisplayText", "ValueType" },
                values: new object[] { "Requisition_form", "Requisition form", "Location", "Location", "INTEGER" });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 38L,
                columns: new[] { "ConfigGroup", "ConfigGroupDisplay", "ConfigKey", "CongigDisplayText", "ValueType" },
                values: new object[] { "Requisition_form", "Requisition form", "Sector", "Sector", "INTEGER" });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 39L,
                columns: new[] { "ConfigGroup", "ConfigGroupDisplay", "ConfigKey", "CongigDisplayText", "ValueType" },
                values: new object[] { "Requisition_form", "Requisition form", "Same_client", "Experience working with same client", "INTEGER" });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 40L,
                columns: new[] { "ConfigGroup", "ConfigGroupDisplay", "ConfigKey", "CongigDisplayText", "ValueType" },
                values: new object[] { "Requisition_form", "Requisition form", "Sub_Industry", "Sub Industry", "INTEGER" });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 41L,
                columns: new[] { "ConfigGroup", "ConfigGroupDisplay", "ConfigKey", "CongigDisplayText", "ValueType" },
                values: new object[] { "Requisition_form", "Requisition form", "business_unit", "Business Unit", "INTEGER" });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 42L,
                columns: new[] { "ConfigGroup", "ConfigGroupDisplay", "ConfigKey", "CongigDisplayText", "ValueType" },
                values: new object[] { "Requisition_form", "Requisition form", "Sub_Industry", "Sub Industry", "INTEGER" });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 43L,
                columns: new[] { "ConfigGroup", "ConfigGroupDisplay", "ConfigKey", "CongigDisplayText", "ValueType" },
                values: new object[] { "Requisition_form", "Requisition form", "business_unit", "Business Unit", "INTEGER" });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 44L,
                columns: new[] { "ConfigGroup", "ConfigGroupDisplay", "ConfigKey", "CongigDisplayText", "ValueType" },
                values: new object[] { "System_Suggestion_For_Requisition_Percentage_Match", "System Suggestion For Requisition Percentage Match", "System_Suggestion_For_Requisition_Percentage_Match", "System Suggestion For Requisition Percentage Match", "INTEGER" });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 45L,
                columns: new[] { "ConfigGroup", "ConfigGroupDisplay", "ConfigKey", "CongigDisplayText", "ValueType" },
                values: new object[] { "System_Suggestion_For_Requisition_Percentage_Match", "System Suggestion For Requisition Percentage Match", "System_Suggestion_For_Requisition_Percentage_Match", "System Suggestion For Requisition Percentage Match", "INTEGER" });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 48L,
                columns: new[] { "ConfigGroup", "ConfigGroupDisplay", "ConfigKey", "CongigDisplayText", "ValueType" },
                values: new object[] { "Employee_Response_Review_process_reviewed_by_Resource_Requestor", "Employee Response Review process reviewed by Resource Requestor", "Employee_Response_Review_process_reviewed_by_Resource_Requestor", "Employee Response Review process reviewed by Resource Requestor", "{\"activationStatus\":\"Boolean\",\"noOfDays\":\"Integer\"}" });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 49L,
                columns: new[] { "ConfigGroup", "ConfigGroupDisplay", "ConfigKey", "CongigDisplayText", "ValueType" },
                values: new object[] { "Employee_Response_Review_process_reviewed_by_Resource_Requestor", "Employee Response Review process reviewed by Resource Requestor", "Employee_Response_Review_process_reviewed_by_Resource_Requestor", "Employee Response Review process reviewed by Resource Requestor", "{\"activationStatus\":\"Boolean\",\"noOfDays\":\"Integer\"}" });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 52L,
                columns: new[] { "ConfigGroup", "ConfigGroupDisplay", "ConfigKey", "CongigDisplayText", "ValueType" },
                values: new object[] { "Number_of_days_for_employee_to_confirm_on_their_allocations", "Number of days for employee to confirm on their allocations", "Number_of_days_for_employee_to_confirm_on_their_allocations", "Number of days for employee to confirm on their allocations", "INTEGER" });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 53L,
                columns: new[] { "ConfigGroup", "ConfigGroupDisplay", "ConfigKey", "CongigDisplayText", "ValueType" },
                values: new object[] { "Number_of_days_for_employee_to_confirm_on_their_allocations", "Number of days for employee to confirm on their allocations", "Number_of_days_for_employee_to_confirm_on_their_allocations", "Number of days for employee to confirm on their allocations", "INTEGER" });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 56L,
                columns: new[] { "ConfigGroup", "ConfigGroupDisplay", "ConfigKey", "CongigDisplayText", "ValueType" },
                values: new object[] { "Maximum_number_of_parameters_that_can_be_selected_by_Employee_in_Preference_screen", "Maximum number of parameters that can be selected by Employee in Preference screen", "Maximum_number_of_parameters_that_can_be_selected_by_Employee_in_Preference_screen", "Maximum number of parameters that can be selected by Employee in Preference screen", "INTEGER" });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 57L,
                columns: new[] { "ConfigGroup", "ConfigGroupDisplay", "ConfigKey", "CongigDisplayText", "ValueType" },
                values: new object[] { "Maximum_number_of_parameters_that_can_be_selected_by_Employee_in_Preference_screen", "Maximum number of parameters that can be selected by Employee in Preference screen", "Maximum_number_of_parameters_that_can_be_selected_by_Employee_in_Preference_screen", "Maximum number of parameters that can be selected by Employee in Preference screen", "INTEGER" });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 58L,
                columns: new[] { "ConfigGroup", "ConfigGroupDisplay", "ConfigKey", "CongigDisplayText", "ValueType" },
                values: new object[] { "Amber_condition_for_Budget_Consumption", "Amber condition for Budget Consumption", "Amber_condition_for_Budget_Consumption", "Amber condition for Budget Consumption", "INTEGER" });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 59L,
                columns: new[] { "ConfigGroup", "ConfigGroupDisplay", "ConfigKey", "CongigDisplayText", "ValueType" },
                values: new object[] { "Amber_condition_for_Budget_Consumption", "Amber condition for Budget Consumption", "Amber_condition_for_Budget_Consumption", "Amber condition for Budget Consumption", "INTEGER" });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 60L,
                columns: new[] { "ConfigGroup", "ConfigGroupDisplay", "ConfigKey", "CongigDisplayText", "ValueType" },
                values: new object[] { "Alert_condition_for_Allocation_Cost", "Alert condition for Allocation Cost", "Alert_condition_for_Allocation_Cost", "Alert condition for Allocation Cost", "INTEGER" });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 61L,
                columns: new[] { "ConfigGroup", "ConfigGroupDisplay", "ConfigKey", "CongigDisplayText", "ValueType" },
                values: new object[] { "Alert_condition_for_Allocation_Cost", "Alert condition for Allocation Cost", "Alert_condition_for_Allocation_Cost", "Alert condition for Allocation Cost", "INTEGER" });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 62L,
                columns: new[] { "ConfigGroup", "ConfigGroupDisplay", "ConfigKey", "CongigDisplayText", "ValueType" },
                values: new object[] { "Alert_condition_for_Timesheet_Hours", "Alert condition for Timesheet Hours", "Alert_condition_for_Timesheet_Hours", "Alert condition for Timesheet Hours", "INTEGER" });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 63L,
                columns: new[] { "ConfigGroup", "ConfigGroupDisplay", "ConfigKey", "CongigDisplayText", "ValueType" },
                values: new object[] { "Alert_condition_for_Timesheet_Hours", "Alert condition for Timesheet Hours", "Alert_condition_for_Timesheet_Hours", "Alert condition for Timesheet Hours", "INTEGER" });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 64L,
                columns: new[] { "ConfigGroup", "ConfigGroupDisplay", "ConfigKey", "CongigDisplayText", "ValueType" },
                values: new object[] { "Requisition_form", "Requisition", "Skill", "Skills", "INTEGER" });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 65L,
                columns: new[] { "ConfigGroup", "ConfigGroupDisplay", "ConfigKey", "CongigDisplayText", "ValueType" },
                values: new object[] { "Requisition_form", "Requisition", "Skill", "Skills", "INTEGER" });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 70L,
                columns: new[] { "ConfigGroup", "ConfigGroupDisplay", "ConfigKey", "CongigDisplayText", "ValueType" },
                values: new object[] { "Requisition_form", "Requisition form", "Skills", "Skills", "INTEGER" });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 71L,
                columns: new[] { "ConfigGroup", "ConfigGroupDisplay", "ConfigKey", "CongigDisplayText", "ValueType" },
                values: new object[] { "Requisition_form", "Requisition form", "Skills", "Skills", "INTEGER" });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 72L,
                columns: new[] { "ConfigGroup", "ConfigGroupDisplay", "ConfigKey", "CongigDisplayText", "ValueType" },
                values: new object[] { "Weightage_for_parameters_for_System_Suggested_Requisition", "Weightage for parameters for System Suggested Requisition", "Skills", "Skills", "INTEGER" });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 73L,
                columns: new[] { "ConfigGroup", "ConfigGroupDisplay", "ConfigKey", "CongigDisplayText", "ValueType" },
                values: new object[] { "Weightage_for_parameters_for_System_Suggested_Requisition", "Weightage for parameters for System Suggested Requisition", "Skills", "Skills", "INTEGER" });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 74L,
                columns: new[] { "ConfigGroup", "ConfigGroupDisplay", "ConfigKey", "CongigDisplayText", "ValueType" },
                values: new object[] { "Permission_for_Additional_EL", "Permission for Additional EL", "Permission_for_Additional_EL", "Permission for Additional EL", "BOOLEAN" });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 75L,
                columns: new[] { "ConfigGroup", "ConfigGroupDisplay", "ConfigKey", "CongigDisplayText", "ValueType" },
                values: new object[] { "Permission_for_Additional_EL", "Permission for Additional EL", "Permission_for_Additional_EL", "Permission for Additional EL", "BOOLEAN" });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 76L,
                columns: new[] { "ConfigGroup", "ConfigGroupDisplay", "ConfigKey", "CongigDisplayText", "ValueType" },
                values: new object[] { "Permission_for_Additional_Delegate", "Permission for Additional Delegate", "Permission_for_Additional_Delegate", "Permission for Additional Delegate", "BOOLEAN" });

            migrationBuilder.UpdateData(
                table: "ConfigurationGroups",
                keyColumn: "Id",
                keyValue: 77L,
                columns: new[] { "ConfigGroup", "ConfigGroupDisplay", "ConfigKey", "CongigDisplayText", "ValueType" },
                values: new object[] { "Permission_for_Additional_Delegate", "Permission for Additional Delegate", "Permission_for_Additional_Delegate", "Permission for Additional Delegate", "BOOLEAN" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 12L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 14L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 16L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 18L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 20L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 22L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 24L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 26L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 28L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 29L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 30L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 31L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 32L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 33L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 35L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 36L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 40L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 41L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 44L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 48L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 52L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 56L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 58L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 60L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 62L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 64L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 70L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 72L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 74L);

            migrationBuilder.DeleteData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 76L);

            //migrationBuilder.DropColumn(
            //    name: "ConfigGroup",
            //    table: "ConfigurationGroups");

            //migrationBuilder.DropColumn(
            //    name: "ConfigGroupDisplay",
            //    table: "ConfigurationGroups");

            //migrationBuilder.DropColumn(
            //    name: "ConfigKey",
            //    table: "ConfigurationGroups");

            //migrationBuilder.DropColumn(
            //    name: "CongigDisplayText",
            //    table: "ConfigurationGroups");

            //migrationBuilder.DropColumn(
            //    name: "ValueType",
            //    table: "ConfigurationGroups");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ConfigurationGroupMasters");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ConfigurationGroupMasters");

            migrationBuilder.DropColumn(
                name: "ModifiedAt",
                table: "ConfigurationGroupMasters");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "ConfigurationGroupMasters");
        }
    }
}
