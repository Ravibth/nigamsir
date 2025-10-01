using System;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RMT.Configuration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class seeder_data_changes_v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ConfigurationMainBreakup",
                keyColumn: "Id",
                keyValue: new Guid("b09dbb85-e0e2-4a5e-ae2c-c49542e407ee"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMainBreakup",
                keyColumn: "Id",
                keyValue: new Guid("efebdaad-4195-4bf6-a0a3-1f60ab537d24"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMaster",
                keyColumn: "Id",
                keyValue: new Guid("b976f3c3-423d-4244-9b51-84a3d137da31"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMaster",
                keyColumn: "Id",
                keyValue: new Guid("ed87108f-e252-4a73-bb9a-60784ef97b0b"));

            migrationBuilder.InsertData(
                table: "ConfigurationMaster",
                columns: new[] { "Id", "ConfigGroup", "ConfigGroupDisplay", "ConfigNote", "Description", "GlobalDefaultDisplay", "SelectorConfigType", "SelectorWiseDisplay", "schema" },
                values: new object[,]
                {
                    { new Guid("15fae466-10f3-43ac-a7d7-45321a2a840f"), "System_Suggestion_For_Requisition_Percentage_Match", "Match Range  for System Suggestions for a Requisition", "The results of the System Suggestions against a requisition are expressed as a match percentage.  By setting the minimum match range percentage below the system suggestions results will be limited to show results above the specified percentage value. E.g. 10 entered below; will show system suggestions results where the resource match score is above 10%.", "", true, "Offerings", true, System.Text.Json.JsonDocument.Parse("[{\"Key\":\"System_Suggestion_For_Requisition_Percentage_Match\",\"KeyDisplay\":\"System Suggestion For Requisition Percentage Match\",\"Description\":\"System Suggestion For Requisition Percentage Match\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^([1-9]|[1-9][0-9]|100)$\"}]", new System.Text.Json.JsonDocumentOptions()) },
                    { new Guid("3307ad48-622c-4a6a-9b3c-f2b3d43a1d88"), "Alert_condition_for_Timesheet_Hours", "Alert condition for Timesheet Hours", "Setting the Percentage Budget consumption limit below; sets warning for budget consumption of Actuals (Time Cost) . The system will send a notification and alert to the user when the Actuals (Time Cost) as a Percentage of Budget exceeds this value.", "", true, "Global", true, System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Alert_condition_for_Timesheet_Hours\",\"KeyDisplay\":\"Alert condition for Timesheet Hours\",\"Description\":\"Alert condition for Timesheet Hours\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^([1-9]|[1-9][0-9]|100)$\"}]", new System.Text.Json.JsonDocumentOptions()) },
                    { new Guid("4129a413-0ce5-4587-96e4-62f4caa70ef4"), "Employee_Response_Review_process_reviewed_by_Resource_Requestor", "Review process of Employee Allocation response", "Turning on this toggle the at the BU level (or Expertise Level); enables the Employee Allocation Review Process for the case when the employee has not accepted the allocation. Enter the maximum number of days given to the reviewer to approve the  request. Post this period; the request will be auto-approved. Turning off; will disable the Reviewer workflow feature for this scenario.", "", true, "Offerings", true, System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Employee_Response_Review_process_reviewed_by_Resource_Requestor\",\"KeyDisplay\":\"Review process of Employee Allocation response\",\"Description\":\"Review process of Employee Allocation response\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(-1|[1-4])$\"}]", new System.Text.Json.JsonDocumentOptions()) },
                    { new Guid("446282e6-7f94-4e17-a6b9-a0e05de0713e"), "Amber_condition_for_Budget_Consumption", "Amber condition for Budget Consumption", "Setting the Percentage Budget consumption limit below; sets the Amber indicator when the Allocation Cost as Percentage of Budget exceeds this value.", "", true, "Global", true, System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Amber_condition_for_Budget_Consumption\",\"KeyDisplay\":\"Amber condition for Budget Consumption\",\"Description\":\"Amber condition for Budget Consumption\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^([1-9]|[1-9][0-9]|100)$\"}]", new System.Text.Json.JsonDocumentOptions()) },
                    { new Guid("6cd9ad6a-8b1c-4c2b-a5f9-d3c4b2630da8"), "Number_of_days_for_employee_to_confirm_on_their_allocations", "Number of days for employee to confirm on their allocations", "Enter the maximum number of days given to the employee to approve the allocation request of the Reviewer. Post this period; the request will be auto-approved.", "", true, "Offerings", true, System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Number_of_days_for_employee_to_confirm_on_their_allocations\",\"KeyDisplay\":\"Number of days for employee to confirm on their allocations\",\"Description\":\"Number of days for employee to confirm on their allocations\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(-1|[1-4])$\"}]", new System.Text.Json.JsonDocumentOptions()) },
                    { new Guid("7ca7f667-8e9b-4b38-8cfc-81a43e6e51da"), "Permission_for_Additional_EL", "Permission for Additional EL", "Turning on this toggle enables the Additional EL to view Allocation and Requisition details created by the Resource Requestor and their Delegate.  Turning off will disable this feature. ", "", true, "Global", true, System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Permission_for_Additional_EL\",\"KeyDisplay\":\"Permission for Additional EL\",\"Description\":\"Permission for Additional EL\",\"ControlType\":\"boolean\",\"ValidationRegEx\":\"^([1-9]|[1-9][0-9]|100)$\"}]", new System.Text.Json.JsonDocumentOptions()) },
                    { new Guid("7e178084-35b8-49fb-8d3b-a5644dfe9f06"), "Alert_condition_for_Allocation_Cost", "Alert condition for Allocation Cost", "Setting the Percentage Budget consumption limit below; sets warning for budget consumption of Allocation Cost . The system will send a notification and alert to the user when the Allocation Cost as a Percentage of Budget exceeds this value.", "", true, "Global", true, System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Alert_condition_for_Allocation_Cost\",\"KeyDisplay\":\"Alert condition for Allocation Cost\",\"Description\":\"Alert condition for Allocation Cost\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^([1-9]|[1-9][0-9]|100)$\"}]", new System.Text.Json.JsonDocumentOptions()) },
                    { new Guid("c578b5e9-7f50-49f6-8f87-72e14f945f6a"), "Maximum_number_of_parameters_that_can_be_selected_by_Employee_in_Preference_screen", "Maximum number of parameters for selection in Preference screen", "Enter a number below for defining the maximum number of user inputs values allowed to an employee for each Preference Parameter in the My Preferences Screen. ", "", true, "Global", true, System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Maximum_number_of_parameters_that_can_be_selected_by_Employee_in_Preference_screen\",\"KeyDisplay\":\"Maximum number of parameters for selection in Preference screen\",\"Description\":\"Maximum number of parameters for selection in Preference screen\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^[0-5]$\"}]", new System.Text.Json.JsonDocumentOptions()) },
                    { new Guid("d0df6225-6b8e-4683-bd76-15207b9ffa46"), "Resource_allocation_review", "Resource allocation review by Reviewer", "Turning on this toggle at the BU level (or Expertise Level) enables the Resource Allocation Approval Workflow.  Enter the maximum number of days given to the reviewer to approve the allocation. Post this period; the request will be auto-approved. Turning off; will disable the Reviewer workflow feature.", "", true, "Offerings", true, System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Resource_allocation_review\",\"KeyDisplay\":\"Resource allocation review by Reviewer\",\"Description\":\"Resource allocation review by Reviewer\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(-1|[1-4])$\"}]", new System.Text.Json.JsonDocumentOptions()) },
                    { new Guid("d5daac9b-4751-4aae-b0cc-d326a88d38b8"), "Requisition_form", "Requisition Form Parameters ", "The Optional Parameters available on the Requisition Form can be removed from the form by disabling the respective parameter using the toggle switch.", "", true, "Offerings", true, System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Location\",\"KeyDisplay\":\"Location\",\"Description\":\"Location\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(10|[0-9]|[0-9]\\\\.[0-9]\\u002B)$\\r\\n\"},{\"Key\":\"Same_client\",\"KeyDisplay\":\"Experience working with the same client\",\"Description\":\"Experience working with the same client\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(10|[0-9]|[0-9]\\\\.[0-9]\\u002B)$\\r\\n\"},{\"Key\":\"Competency\",\"KeyDisplay\":\"Competency\",\"Description\":\"Competency\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(10|[0-9]|[0-9]\\\\.[0-9]\\u002B)$\\r\\n\"},{\"Key\":\"Offerings\",\"KeyDisplay\":\"Offerings\",\"Description\":\"Offerings\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(10|[0-9]|[0-9]\\\\.[0-9]\\u002B)$\\r\\n\"},{\"Key\":\"Solutions\",\"KeyDisplay\":\"Solutions\",\"Description\":\"Solutions\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(10|[0-9]|[0-9]\\\\.[0-9]\\u002B)$\\r\\n\"},{\"Key\":\"Industry\",\"KeyDisplay\":\"Industry\",\"Description\":\"Industry\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(10|[0-9]|[0-9]\\\\.[0-9]\\u002B)$\\r\\n\"},{\"Key\":\"Sub_Industry\",\"KeyDisplay\":\"Sub Industry\",\"Description\":\"Sub Industry\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(10|[0-9]|[0-9]\\\\.[0-9]\\u002B)$\\r\\n\"},{\"Key\":\"Skills\",\"KeyDisplay\":\"Skills\",\"Description\":\"Skills\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(10|[0-9]|[0-9]\\\\.[0-9]\\u002B)$\\r\\n\"}]", new System.Text.Json.JsonDocumentOptions()) },
                    { new Guid("ecfd22aa-d3a3-498f-a273-2332aeb050d6"), "Permission_for_Additional_Delegate", "Permission for Additional Delegate", "Turning on this toggle enables the Additional Delegate to view Allocation and Requisition details created by the Resource Requestor and their Delegate. Turning off will disable this feature. ", "", true, "Global", true, System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Permission_for_Additional_Delegate\",\"KeyDisplay\":\"Permission for Additional Delegate\",\"Description\":\"Permission for Additional Delegate\",\"ControlType\":\"boolean\",\"ValidationRegEx\":\"^([1-9]|[1-9][0-9]|100)$\"}]", new System.Text.Json.JsonDocumentOptions()) },
                    { new Guid("fba61aaa-acde-4c4c-a59c-9af7d822472b"), "NUMBER_OF_DAYS_FOR_SKILL_APPROVAL_DUE_DATE", "Number Of Days For Skill Approval Duedate", "Enter a number of days to define the Due Date for the task for the Skill Reviewer.", "", true, "Global", true, System.Text.Json.JsonDocument.Parse("[{\"Key\":\"NUMBER_OF_DAYS_FOR_SKILL_APPROVAL_DUE_DATE\",\"KeyDisplay\":\"Number Of Days For Skill Approval Duedate\",\"Description\":\"Number Of Days For Skill Approval Duedate\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^[0-5]$\"}]", new System.Text.Json.JsonDocumentOptions()) }
                });

            migrationBuilder.InsertData(
                table: "ConfigurationMainBreakup",
                columns: new[] { "Id", "ConfigurationMasterId", "CreatedAt", "CreatedBy", "KeySelector", "MetaValue", "ModifiedAt", "ModifiedBy" },
                values: new object[,]
                {
                    { new Guid("07f74a31-9f8b-43c0-b562-1ee7b0a9d1e8"), new Guid("7e178084-35b8-49fb-8d3b-a5644dfe9f06"), new DateTime(2024, 10, 28, 13, 36, 16, 312, DateTimeKind.Utc).AddTicks(5806), "system", "DEFAULT", System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Alert_condition_for_Allocation_Cost\",\"DisplayKey\":\"Alert condition for Allocation Cost\",\"Value\":\"90\"}]", new System.Text.Json.JsonDocumentOptions()), new DateTime(2024, 10, 28, 13, 36, 16, 312, DateTimeKind.Utc).AddTicks(5807), "system" },
                    { new Guid("121e6f42-4233-4c68-a8fe-4f3c0c784646"), new Guid("fba61aaa-acde-4c4c-a59c-9af7d822472b"), new DateTime(2024, 10, 28, 13, 36, 16, 312, DateTimeKind.Utc).AddTicks(7171), "system", "DEFAULT", System.Text.Json.JsonDocument.Parse("[{\"Key\":\"NUMBER_OF_DAYS_FOR_SKILL_APPROVAL_DUE_DATE\",\"DisplayKey\":\"Number Of Days For Skill Approval Duedate\",\"Value\":\"false\"}]", new System.Text.Json.JsonDocumentOptions()), new DateTime(2024, 10, 28, 13, 36, 16, 312, DateTimeKind.Utc).AddTicks(7172), "system" },
                    { new Guid("1a94ec84-9eef-49a6-8600-aa7b28960928"), new Guid("7ca7f667-8e9b-4b38-8cfc-81a43e6e51da"), new DateTime(2024, 10, 28, 13, 36, 16, 312, DateTimeKind.Utc).AddTicks(6461), "system", "DEFAULT", System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Permission_for_Additional_EL\",\"DisplayKey\":\"Permission for Additional EL\",\"Value\":\"false\"}]", new System.Text.Json.JsonDocumentOptions()), new DateTime(2024, 10, 28, 13, 36, 16, 312, DateTimeKind.Utc).AddTicks(6461), "system" },
                    { new Guid("2a020571-a556-4a3f-b169-7e22e8fd5302"), new Guid("d0df6225-6b8e-4683-bd76-15207b9ffa46"), new DateTime(2024, 10, 28, 13, 36, 16, 312, DateTimeKind.Utc).AddTicks(4078), "system", "DEFAULT", System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Resource_allocation_review\",\"DisplayKey\":\"Resource allocation review by Reviewer\",\"Value\":\"1\"}]", new System.Text.Json.JsonDocumentOptions()), new DateTime(2024, 10, 28, 13, 36, 16, 312, DateTimeKind.Utc).AddTicks(4079), "system" },
                    { new Guid("2d769d84-a958-4338-81e5-c4afcba8a716"), new Guid("d5daac9b-4751-4aae-b0cc-d326a88d38b8"), new DateTime(2024, 10, 28, 13, 36, 16, 310, DateTimeKind.Utc).AddTicks(8591), "system", "DEFAULT", System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Location\",\"DisplayKey\":\"Location\",\"Value\":\"8\"},{\"Key\":\"Same_client\",\"DisplayKey\":\"Experience working with the same client\",\"Value\":\"8\"},{\"Key\":\"Competency\",\"DisplayKey\":\"Competency\",\"Value\":\"8\"},{\"Key\":\"Offerings\",\"DisplayKey\":\"Offerings\",\"Value\":\"8\"},{\"Key\":\"Solutions\",\"DisplayKey\":\"Solutions\",\"Value\":\"8\"},{\"Key\":\"Industry\",\"DisplayKey\":\"Industry\",\"Value\":\"8\"},{\"Key\":\"Sub_Industry\",\"DisplayKey\":\"Sub Industry\",\"Value\":\"8\"},{\"Key\":\"Skills\",\"DisplayKey\":\"Skills\",\"Value\":\"8\"}]", new System.Text.Json.JsonDocumentOptions()), new DateTime(2024, 10, 28, 13, 36, 16, 310, DateTimeKind.Utc).AddTicks(8752), "system" },
                    { new Guid("540163a7-d630-44e7-8cca-6c924177bef8"), new Guid("c578b5e9-7f50-49f6-8f87-72e14f945f6a"), new DateTime(2024, 10, 28, 13, 36, 16, 312, DateTimeKind.Utc).AddTicks(5093), "system", "DEFAULT", System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Maximum_number_of_parameters_that_can_be_selected_by_Employee_in_Preference_screen\",\"DisplayKey\":\"Maximum number of parameters for selection in Preference screen\",\"Value\":\"5\"}]", new System.Text.Json.JsonDocumentOptions()), new DateTime(2024, 10, 28, 13, 36, 16, 312, DateTimeKind.Utc).AddTicks(5093), "system" },
                    { new Guid("5a9f30e8-494d-45e3-86e7-2369ed8a22bf"), new Guid("6cd9ad6a-8b1c-4c2b-a5f9-d3c4b2630da8"), new DateTime(2024, 10, 28, 13, 36, 16, 312, DateTimeKind.Utc).AddTicks(4423), "system", "DEFAULT", System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Number_of_days_for_employee_to_confirm_on_their_allocations\",\"DisplayKey\":\"Number of days for employee to confirm on their allocations\",\"Value\":\"1\"}]", new System.Text.Json.JsonDocumentOptions()), new DateTime(2024, 10, 28, 13, 36, 16, 312, DateTimeKind.Utc).AddTicks(4423), "system" },
                    { new Guid("69f4a157-89c3-4454-9139-e921677bfcba"), new Guid("4129a413-0ce5-4587-96e4-62f4caa70ef4"), new DateTime(2024, 10, 28, 13, 36, 16, 312, DateTimeKind.Utc).AddTicks(4743), "system", "DEFAULT", System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Employee_Response_Review_process_reviewed_by_Resource_Requestor\",\"DisplayKey\":\"Review process of Employee Allocation response\",\"Value\":\"1\"}]", new System.Text.Json.JsonDocumentOptions()), new DateTime(2024, 10, 28, 13, 36, 16, 312, DateTimeKind.Utc).AddTicks(4744), "system" },
                    { new Guid("75d856f8-a35d-4618-96ad-30f73702fb6e"), new Guid("15fae466-10f3-43ac-a7d7-45321a2a840f"), new DateTime(2024, 10, 28, 13, 36, 16, 312, DateTimeKind.Utc).AddTicks(3694), "system", "DEFAULT", System.Text.Json.JsonDocument.Parse("[{\"Key\":\"System_Suggestion_For_Requisition_Percentage_Match\",\"DisplayKey\":\"System Suggestion For Requisition Percentage Match\",\"Value\":\"60\"}]", new System.Text.Json.JsonDocumentOptions()), new DateTime(2024, 10, 28, 13, 36, 16, 312, DateTimeKind.Utc).AddTicks(3694), "system" },
                    { new Guid("8a51960d-62c7-4b75-8758-ee99d79d102a"), new Guid("446282e6-7f94-4e17-a6b9-a0e05de0713e"), new DateTime(2024, 10, 28, 13, 36, 16, 312, DateTimeKind.Utc).AddTicks(5475), "system", "DEFAULT", System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Amber_condition_for_Budget_Consumption\",\"DisplayKey\":\"Amber condition for Budget Consumption\",\"Value\":\"80\"}]", new System.Text.Json.JsonDocumentOptions()), new DateTime(2024, 10, 28, 13, 36, 16, 312, DateTimeKind.Utc).AddTicks(5475), "system" },
                    { new Guid("ba484a77-23a0-4399-84c5-16d5fad74700"), new Guid("3307ad48-622c-4a6a-9b3c-f2b3d43a1d88"), new DateTime(2024, 10, 28, 13, 36, 16, 312, DateTimeKind.Utc).AddTicks(6145), "system", "DEFAULT", System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Alert_condition_for_Timesheet_Hours\",\"DisplayKey\":\"Alert condition for Timesheet Hours\",\"Value\":\"90\"}]", new System.Text.Json.JsonDocumentOptions()), new DateTime(2024, 10, 28, 13, 36, 16, 312, DateTimeKind.Utc).AddTicks(6145), "system" },
                    { new Guid("f0df3782-de76-46a9-97c8-17365646e898"), new Guid("ecfd22aa-d3a3-498f-a273-2332aeb050d6"), new DateTime(2024, 10, 28, 13, 36, 16, 312, DateTimeKind.Utc).AddTicks(6755), "system", "DEFAULT", System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Permission_for_Additional_Delegate\",\"DisplayKey\":\"Permission for Additional Delegate\",\"Value\":\"false\"}]", new System.Text.Json.JsonDocumentOptions()), new DateTime(2024, 10, 28, 13, 36, 16, 312, DateTimeKind.Utc).AddTicks(6756), "system" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ConfigurationMainBreakup",
                keyColumn: "Id",
                keyValue: new Guid("07f74a31-9f8b-43c0-b562-1ee7b0a9d1e8"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMainBreakup",
                keyColumn: "Id",
                keyValue: new Guid("121e6f42-4233-4c68-a8fe-4f3c0c784646"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMainBreakup",
                keyColumn: "Id",
                keyValue: new Guid("1a94ec84-9eef-49a6-8600-aa7b28960928"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMainBreakup",
                keyColumn: "Id",
                keyValue: new Guid("2a020571-a556-4a3f-b169-7e22e8fd5302"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMainBreakup",
                keyColumn: "Id",
                keyValue: new Guid("2d769d84-a958-4338-81e5-c4afcba8a716"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMainBreakup",
                keyColumn: "Id",
                keyValue: new Guid("540163a7-d630-44e7-8cca-6c924177bef8"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMainBreakup",
                keyColumn: "Id",
                keyValue: new Guid("5a9f30e8-494d-45e3-86e7-2369ed8a22bf"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMainBreakup",
                keyColumn: "Id",
                keyValue: new Guid("69f4a157-89c3-4454-9139-e921677bfcba"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMainBreakup",
                keyColumn: "Id",
                keyValue: new Guid("75d856f8-a35d-4618-96ad-30f73702fb6e"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMainBreakup",
                keyColumn: "Id",
                keyValue: new Guid("8a51960d-62c7-4b75-8758-ee99d79d102a"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMainBreakup",
                keyColumn: "Id",
                keyValue: new Guid("ba484a77-23a0-4399-84c5-16d5fad74700"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMainBreakup",
                keyColumn: "Id",
                keyValue: new Guid("f0df3782-de76-46a9-97c8-17365646e898"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMaster",
                keyColumn: "Id",
                keyValue: new Guid("15fae466-10f3-43ac-a7d7-45321a2a840f"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMaster",
                keyColumn: "Id",
                keyValue: new Guid("3307ad48-622c-4a6a-9b3c-f2b3d43a1d88"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMaster",
                keyColumn: "Id",
                keyValue: new Guid("4129a413-0ce5-4587-96e4-62f4caa70ef4"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMaster",
                keyColumn: "Id",
                keyValue: new Guid("446282e6-7f94-4e17-a6b9-a0e05de0713e"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMaster",
                keyColumn: "Id",
                keyValue: new Guid("6cd9ad6a-8b1c-4c2b-a5f9-d3c4b2630da8"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMaster",
                keyColumn: "Id",
                keyValue: new Guid("7ca7f667-8e9b-4b38-8cfc-81a43e6e51da"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMaster",
                keyColumn: "Id",
                keyValue: new Guid("7e178084-35b8-49fb-8d3b-a5644dfe9f06"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMaster",
                keyColumn: "Id",
                keyValue: new Guid("c578b5e9-7f50-49f6-8f87-72e14f945f6a"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMaster",
                keyColumn: "Id",
                keyValue: new Guid("d0df6225-6b8e-4683-bd76-15207b9ffa46"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMaster",
                keyColumn: "Id",
                keyValue: new Guid("d5daac9b-4751-4aae-b0cc-d326a88d38b8"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMaster",
                keyColumn: "Id",
                keyValue: new Guid("ecfd22aa-d3a3-498f-a273-2332aeb050d6"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMaster",
                keyColumn: "Id",
                keyValue: new Guid("fba61aaa-acde-4c4c-a59c-9af7d822472b"));

            migrationBuilder.InsertData(
                table: "ConfigurationMaster",
                columns: new[] { "Id", "ConfigGroup", "ConfigGroupDisplay", "ConfigNote", "Description", "GlobalDefaultDisplay", "SelectorConfigType", "SelectorWiseDisplay", "schema" },
                values: new object[,]
                {
                    { new Guid("b976f3c3-423d-4244-9b51-84a3d137da31"), "Requisition_form", "Requisition Form Parameters ", "The Optional Parameters available on the Requisition Form can be removed from the form by disabling the respective parameter using the toggle switch.", "", true, "Offerings", true, System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Location\",\"KeyDisplay\":\"Location\",\"Description\":\"Location\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(10|[0-9]|[0-9]?\\\\.[0-9]\\u002B)$\"},{\"Key\":\"Same_client\",\"KeyDisplay\":\"Experience working with the same client\",\"Description\":\"Experience working with the same client\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(10|[0-9]|[0-9]?\\\\.[0-9]\\u002B)$\"},{\"Key\":\"Competency\",\"KeyDisplay\":\"Competency\",\"Description\":\"Competency\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(10|[0-9]|[0-9]?\\\\.[0-9]\\u002B)$\"},{\"Key\":\"Offerings\",\"KeyDisplay\":\"Offerings\",\"Description\":\"Offerings\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(10|[0-9]|[0-9]?\\\\.[0-9]\\u002B)$\"},{\"Key\":\"Solutions\",\"KeyDisplay\":\"Solutions\",\"Description\":\"Solutions\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(10|[0-9]|[0-9]?\\\\.[0-9]\\u002B)$\"},{\"Key\":\"Industry\",\"KeyDisplay\":\"Industry\",\"Description\":\"Industry\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(10|[0-9]|[0-9]?\\\\.[0-9]\\u002B)$\"},{\"Key\":\"Sub_Industry\",\"KeyDisplay\":\"Sub Industry\",\"Description\":\"Sub Industry\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(10|[0-9]|[0-9]?\\\\.[0-9]\\u002B)$\"},{\"Key\":\"Skills\",\"KeyDisplay\":\"Skills\",\"Description\":\"Skills\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(10|[0-9]|[0-9]?\\\\.[0-9]\\u002B)$\"}]", new System.Text.Json.JsonDocumentOptions()) },
                    { new Guid("ed87108f-e252-4a73-bb9a-60784ef97b0b"), "System_Suggestion_For_Requisition_Percentage_Match", "Match Range  for System Suggestions for a Requisition", "The results of the System Suggestions against a requisition are expressed as a match percentage.  By setting the minimum match range percentage below the system suggestions results will be limited to show results above the specified percentage value. E.g. 10 entered below; will show system suggestions results where the resource match score is above 10%.", "", true, "Offerings", true, System.Text.Json.JsonDocument.Parse("[{\"Key\":\"System_Suggestion_For_Requisition_Percentage_Match\",\"KeyDisplay\":\"System Suggestion For Requisition Percentage Match\",\"Description\":\"System Suggestion For Requisition Percentage Match\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(100|[1-9][0-9]|[1-9])$\"}]", new System.Text.Json.JsonDocumentOptions()) }
                });

            migrationBuilder.InsertData(
                table: "ConfigurationMainBreakup",
                columns: new[] { "Id", "ConfigurationMasterId", "CreatedAt", "CreatedBy", "KeySelector", "MetaValue", "ModifiedAt", "ModifiedBy" },
                values: new object[,]
                {
                    { new Guid("b09dbb85-e0e2-4a5e-ae2c-c49542e407ee"), new Guid("b976f3c3-423d-4244-9b51-84a3d137da31"), new DateTime(2024, 10, 23, 7, 56, 55, 842, DateTimeKind.Utc).AddTicks(9286), "system", "DEFAULT", System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Location\",\"DisplayKey\":\"Location\",\"Value\":\"8\"},{\"Key\":\"Same_client\",\"DisplayKey\":\"Experience working with the same client\",\"Value\":\"8\"},{\"Key\":\"Competency\",\"DisplayKey\":\"Competency\",\"Value\":\"8\"},{\"Key\":\"Offerings\",\"DisplayKey\":\"Offerings\",\"Value\":\"8\"},{\"Key\":\"Solutions\",\"DisplayKey\":\"Solutions\",\"Value\":\"8\"},{\"Key\":\"Industry\",\"DisplayKey\":\"Industry\",\"Value\":\"0\"},{\"Key\":\"Sub_Industry\",\"DisplayKey\":\"Sub Industry\",\"Value\":\"0\"},{\"Key\":\"Skills\",\"DisplayKey\":\"Skills\",\"Value\":\"8\"}]", new System.Text.Json.JsonDocumentOptions()), new DateTime(2024, 10, 23, 7, 56, 55, 842, DateTimeKind.Utc).AddTicks(9649), "system" },
                    { new Guid("efebdaad-4195-4bf6-a0a3-1f60ab537d24"), new Guid("ed87108f-e252-4a73-bb9a-60784ef97b0b"), new DateTime(2024, 10, 23, 7, 56, 55, 845, DateTimeKind.Utc).AddTicks(8090), "system", "DEFAULT", System.Text.Json.JsonDocument.Parse("[{\"Key\":\"System_Suggestion_For_Requisition_Percentage_Match\",\"DisplayKey\":\"System Suggestion For Requisition Percentage Match\",\"Value\":\"60\"}]", new System.Text.Json.JsonDocumentOptions()), new DateTime(2024, 10, 23, 7, 56, 55, 845, DateTimeKind.Utc).AddTicks(8093), "system" }
                });
        }
    }
}
