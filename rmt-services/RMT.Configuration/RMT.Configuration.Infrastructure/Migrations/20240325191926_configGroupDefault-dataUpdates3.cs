using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Configuration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class configGroupDefaultdataUpdates3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfigGroup",
                table: "ConfigurationGroups");

            migrationBuilder.DropColumn(
                name: "ConfigGroupDisplay",
                table: "ConfigurationGroups");

            migrationBuilder.DropColumn(
                name: "ConfigKey",
                table: "ConfigurationGroups");

            migrationBuilder.DropColumn(
                name: "CongigDisplayText",
                table: "ConfigurationGroups");

            migrationBuilder.DropColumn(
                name: "ValueType",
                table: "ConfigurationGroups");

            migrationBuilder.UpdateData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DefaultValue",
                value: "8");

            migrationBuilder.UpdateData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DefaultValue",
                value: "8");

            migrationBuilder.UpdateData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 12L,
                column: "DefaultValue",
                value: "8");

            migrationBuilder.UpdateData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DefaultValue",
                value: "8");

            migrationBuilder.UpdateData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DefaultValue",
                value: "8");

            migrationBuilder.UpdateData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 18L,
                column: "DefaultValue",
                value: "8");

            migrationBuilder.UpdateData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 20L,
                column: "DefaultValue",
                value: "8");

            migrationBuilder.UpdateData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 24L,
                column: "DefaultValue",
                value: "8");

            migrationBuilder.UpdateData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 26L,
                column: "DefaultValue",
                value: "5");

            migrationBuilder.UpdateData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 28L,
                column: "DefaultValue",
                value: "8");

            migrationBuilder.UpdateData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 29L,
                column: "DefaultValue",
                value: "8");

            migrationBuilder.UpdateData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 30L,
                column: "DefaultValue",
                value: "8");

            migrationBuilder.UpdateData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 31L,
                column: "DefaultValue",
                value: "8");

            migrationBuilder.UpdateData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 32L,
                column: "DefaultValue",
                value: "8");

            migrationBuilder.UpdateData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 33L,
                column: "DefaultValue",
                value: "8");

            migrationBuilder.UpdateData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 35L,
                column: "DefaultValue",
                value: "8");

            migrationBuilder.UpdateData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 36L,
                column: "DefaultValue",
                value: "8");

            migrationBuilder.UpdateData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 40L,
                column: "DefaultValue",
                value: "8");

            migrationBuilder.UpdateData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 41L,
                column: "DefaultValue",
                value: "8");

            migrationBuilder.UpdateData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 48L,
                column: "DefaultValue",
                value: "{\"activationStatus\":\"false\",\"noOfDays\":\"1\"}");

            migrationBuilder.UpdateData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 52L,
                column: "DefaultValue",
                value: "1");

            migrationBuilder.UpdateData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 60L,
                column: "DefaultValue",
                value: "90");

            migrationBuilder.UpdateData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 62L,
                column: "DefaultValue",
                value: "90");

            migrationBuilder.UpdateData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 64L,
                column: "DefaultValue",
                value: "3");

            migrationBuilder.UpdateData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 70L,
                column: "DefaultValue",
                value: "8");

            migrationBuilder.UpdateData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 72L,
                column: "DefaultValue",
                value: "8");

            migrationBuilder.UpdateData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 74L,
                column: "DefaultValue",
                value: "false");

            migrationBuilder.UpdateData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 76L,
                column: "DefaultValue",
                value: "false");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ConfigGroup",
                table: "ConfigurationGroups",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ConfigGroupDisplay",
                table: "ConfigurationGroups",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ConfigKey",
                table: "ConfigurationGroups",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CongigDisplayText",
                table: "ConfigurationGroups",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ValueType",
                table: "ConfigurationGroups",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 8L,
                column: "DefaultValue",
                value: "1");

            migrationBuilder.UpdateData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 10L,
                column: "DefaultValue",
                value: "4");

            migrationBuilder.UpdateData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 12L,
                column: "DefaultValue",
                value: "4");

            migrationBuilder.UpdateData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 14L,
                column: "DefaultValue",
                value: "4");

            migrationBuilder.UpdateData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 16L,
                column: "DefaultValue",
                value: "4");

            migrationBuilder.UpdateData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 18L,
                column: "DefaultValue",
                value: "4");

            migrationBuilder.UpdateData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 20L,
                column: "DefaultValue",
                value: "4");

            migrationBuilder.UpdateData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 24L,
                column: "DefaultValue",
                value: "4");

            migrationBuilder.UpdateData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 26L,
                column: "DefaultValue",
                value: "10");

            migrationBuilder.UpdateData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 28L,
                column: "DefaultValue",
                value: "4");

            migrationBuilder.UpdateData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 29L,
                column: "DefaultValue",
                value: "4");

            migrationBuilder.UpdateData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 30L,
                column: "DefaultValue",
                value: "4");

            migrationBuilder.UpdateData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 31L,
                column: "DefaultValue",
                value: "4");

            migrationBuilder.UpdateData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 32L,
                column: "DefaultValue",
                value: "4");

            migrationBuilder.UpdateData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 33L,
                column: "DefaultValue",
                value: "4");

            migrationBuilder.UpdateData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 35L,
                column: "DefaultValue",
                value: "4");

            migrationBuilder.UpdateData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 36L,
                column: "DefaultValue",
                value: "4");

            migrationBuilder.UpdateData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 40L,
                column: "DefaultValue",
                value: "4");

            migrationBuilder.UpdateData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 41L,
                column: "DefaultValue",
                value: "4");

            migrationBuilder.UpdateData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 48L,
                column: "DefaultValue",
                value: "{\"activationStatus\":\"true\",\"noOfDays\":\"1\"}");

            migrationBuilder.UpdateData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 52L,
                column: "DefaultValue",
                value: "10");

            migrationBuilder.UpdateData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 60L,
                column: "DefaultValue",
                value: "80");

            migrationBuilder.UpdateData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 62L,
                column: "DefaultValue",
                value: "80");

            migrationBuilder.UpdateData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 64L,
                column: "DefaultValue",
                value: "2");

            migrationBuilder.UpdateData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 70L,
                column: "DefaultValue",
                value: "10");

            migrationBuilder.UpdateData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 72L,
                column: "DefaultValue",
                value: "9");

            migrationBuilder.UpdateData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 74L,
                column: "DefaultValue",
                value: "true");

            migrationBuilder.UpdateData(
                table: "ConfigurationGroupMasters",
                keyColumn: "Id",
                keyValue: 76L,
                column: "DefaultValue",
                value: "true");

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
    }
}
