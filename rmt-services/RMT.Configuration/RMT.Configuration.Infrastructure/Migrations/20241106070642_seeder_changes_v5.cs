using System;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RMT.Configuration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class seeder_changes_v5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ConfigurationMainBreakup",
                keyColumn: "Id",
                keyValue: new Guid("1e184704-6d35-4b19-b7fa-e2cfd411b237"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMainBreakup",
                keyColumn: "Id",
                keyValue: new Guid("6b320aa8-7490-4ad7-b0fe-fd286da40e95"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMainBreakup",
                keyColumn: "Id",
                keyValue: new Guid("a1f30742-710b-4939-83d2-ce3a6f0d5a0b"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMainBreakup",
                keyColumn: "Id",
                keyValue: new Guid("a8243a70-1598-4154-9170-ae944a51fa39"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMainBreakup",
                keyColumn: "Id",
                keyValue: new Guid("bc872dea-93f9-4973-aad0-9ac860f36d1c"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMainBreakup",
                keyColumn: "Id",
                keyValue: new Guid("c03388c6-9467-4ee4-b69d-97e49ca0f05a"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMainBreakup",
                keyColumn: "Id",
                keyValue: new Guid("c8af7c20-3c71-430e-ac5e-477cc5701c8a"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMainBreakup",
                keyColumn: "Id",
                keyValue: new Guid("d4a16e4c-be92-4767-9183-74b9502be153"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMainBreakup",
                keyColumn: "Id",
                keyValue: new Guid("db230359-f321-4221-9b61-02acc9772a31"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMainBreakup",
                keyColumn: "Id",
                keyValue: new Guid("e10d95e2-59af-4756-9972-c3933991f9ba"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMainBreakup",
                keyColumn: "Id",
                keyValue: new Guid("e63a4286-bf53-4fff-8b82-99894f5e372a"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMainBreakup",
                keyColumn: "Id",
                keyValue: new Guid("f8579e16-852a-41e2-9a36-14c91371f1fa"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMaster",
                keyColumn: "Id",
                keyValue: new Guid("16e9bec9-891e-4a22-8b10-39249a4b4666"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMaster",
                keyColumn: "Id",
                keyValue: new Guid("21f5c6a2-336f-4176-a1af-111e7cbde8ac"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMaster",
                keyColumn: "Id",
                keyValue: new Guid("38eccefa-1bd9-43ff-8d9e-20e279845761"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMaster",
                keyColumn: "Id",
                keyValue: new Guid("57a9fbb4-8ca0-40da-89ff-655167c6a5ec"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMaster",
                keyColumn: "Id",
                keyValue: new Guid("630af6fb-4ce7-4c06-a04f-daca4fa5e997"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMaster",
                keyColumn: "Id",
                keyValue: new Guid("732ead14-624e-46f9-80e4-4cd7fce31635"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMaster",
                keyColumn: "Id",
                keyValue: new Guid("8f729288-fc9b-4e9d-abb0-6ce8673b4cbd"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMaster",
                keyColumn: "Id",
                keyValue: new Guid("a68b331c-cc3c-4314-99d0-01aa830c1b32"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMaster",
                keyColumn: "Id",
                keyValue: new Guid("b4b8beac-596a-4274-b671-0396655caef3"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMaster",
                keyColumn: "Id",
                keyValue: new Guid("c6bfb6e5-2274-4a58-8b1a-b7e2cdd8c273"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMaster",
                keyColumn: "Id",
                keyValue: new Guid("e55ce822-2c77-4168-831a-9801c4e565f8"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMaster",
                keyColumn: "Id",
                keyValue: new Guid("fa483b93-5d4a-4707-81d6-23f7c45a39e8"));

            migrationBuilder.InsertData(
                table: "ConfigurationMaster",
                columns: new[] { "Id", "ConfigGroup", "ConfigGroupDisplay", "ConfigNote", "Description", "GlobalDefaultDisplay", "SelectorConfigType", "SelectorWiseDisplay", "schema" },
                values: new object[,]
                {
                    { new Guid("05d0c713-5762-44f1-a2be-4f7955884bdc"), "Resource_allocation_review", "Resource allocation review by Reviewer", "Turning on this toggle at the BU level (or Expertise Level) enables the Resource Allocation Approval Workflow.  Enter the maximum number of days given to the reviewer to approve the allocation. Post this period; the request will be auto-approved. Turning off; will disable the Reviewer workflow feature.", "", true, "Offerings", true, System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Resource_allocation_review\",\"KeyDisplay\":\"Resource allocation review by Reviewer\",\"Description\":\"Resource allocation review by Reviewer\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(-1|[1-4])$\"}]", new System.Text.Json.JsonDocumentOptions()) },
                    { new Guid("1023ae1a-f30c-475b-9d4a-d33eab1cd4fd"), "System_Suggestion_For_Requisition_Percentage_Match", "Match Range  for System Suggestions for a Requisition", "The results of the System Suggestions against a requisition are expressed as a match percentage.  By setting the minimum match range percentage below the system suggestions results will be limited to show results above the specified percentage value. E.g. 10 entered below; will show system suggestions results where the resource match score is above 10%.", "", true, "Offerings", true, System.Text.Json.JsonDocument.Parse("[{\"Key\":\"System_Suggestion_For_Requisition_Percentage_Match\",\"KeyDisplay\":\"System Suggestion For Requisition Percentage Match\",\"Description\":\"System Suggestion For Requisition Percentage Match\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^([1-9]|[1-9][0-9]|100)$\"}]", new System.Text.Json.JsonDocumentOptions()) },
                    { new Guid("19e50dfe-3593-4f7a-8aa6-05202c709156"), "Maximum_number_of_parameters_that_can_be_selected_by_Employee_in_Preference_screen", "Maximum number of parameters for selection in Preference screen", "Enter a number below for defining the maximum number of user inputs values allowed to an employee for each Preference Parameter in the My Preferences Screen. ", "", true, "Global", false, System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Maximum_number_of_parameters_that_can_be_selected_by_Employee_in_Preference_screen\",\"KeyDisplay\":\"Maximum number of parameters for selection in Preference screen\",\"Description\":\"Maximum number of parameters for selection in Preference screen\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^[0-5]$\"}]", new System.Text.Json.JsonDocumentOptions()) },
                    { new Guid("1bdf3a16-4d0c-4e2d-aad3-1727c65c3f31"), "Number_of_days_for_employee_to_confirm_on_their_allocations", "Number of days for employee to confirm on their allocations", "Enter the maximum number of days given to the employee to approve the allocation request of the Reviewer. Post this period; the request will be auto-approved.", "", true, "Offerings", true, System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Number_of_days_for_employee_to_confirm_on_their_allocations\",\"KeyDisplay\":\"Number of days for employee to confirm on their allocations\",\"Description\":\"Number of days for employee to confirm on their allocations\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(-1|[1-4])$\"}]", new System.Text.Json.JsonDocumentOptions()) },
                    { new Guid("4e3f94bf-1f6b-423f-b30c-f043653dbaa0"), "Employee_Response_Review_process_reviewed_by_Resource_Requestor", "Review process of Employee Allocation response", "Turning on this toggle the at the BU level (or Expertise Level); enables the Employee Allocation Review Process for the case when the employee has not accepted the allocation. Enter the maximum number of days given to the reviewer to approve the  request. Post this period; the request will be auto-approved. Turning off; will disable the Reviewer workflow feature for this scenario.", "", true, "Offerings", true, System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Employee_Response_Review_process_reviewed_by_Resource_Requestor\",\"KeyDisplay\":\"Review process of Employee Allocation response\",\"Description\":\"Review process of Employee Allocation response\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(-1|[1-4])$\"}]", new System.Text.Json.JsonDocumentOptions()) },
                    { new Guid("4f1c9322-8177-4560-b13d-26a0f06b42a2"), "Alert_condition_for_Timesheet_Hours", "Alert condition for Timesheet Hours", "Setting the Percentage Budget consumption limit below; sets warning for budget consumption of Actuals (Time Cost) . The system will send a notification and alert to the user when the Actuals (Time Cost) as a Percentage of Budget exceeds this value.", "", true, "Global", false, System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Alert_condition_for_Timesheet_Hours\",\"KeyDisplay\":\"Alert condition for Timesheet Hours\",\"Description\":\"Alert condition for Timesheet Hours\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^([1-9]|[1-9][0-9]|100)$\"}]", new System.Text.Json.JsonDocumentOptions()) },
                    { new Guid("4fbfd329-cc6f-4c47-8def-1eab16ccff75"), "Permission_for_Additional_Delegate", "Permission for Additional Delegate", "Entering value of 1 enables the Additional Delegate to view Allocation and Requisition details created by the Resource Requestor and their Delegate. Entering value of -1 will disable the feature.", "", true, "Global", false, System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Permission_for_Additional_Delegate\",\"KeyDisplay\":\"Permission for Additional Delegate\",\"Description\":\"Permission for Additional Delegate\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^-?1$\"}]", new System.Text.Json.JsonDocumentOptions()) },
                    { new Guid("524985c2-ebcc-4272-af6d-c4d8283849b0"), "NUMBER_OF_DAYS_FOR_SKILL_APPROVAL_DUE_DATE", "Number Of Days For Skill Approval Duedate", "Enter a number of days to define the Due Date for the task for the Skill Reviewer.", "", true, "Global", false, System.Text.Json.JsonDocument.Parse("[{\"Key\":\"NUMBER_OF_DAYS_FOR_SKILL_APPROVAL_DUE_DATE\",\"KeyDisplay\":\"Number Of Days For Skill Approval Duedate\",\"Description\":\"Number Of Days For Skill Approval Duedate\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^[0-5]$\"}]", new System.Text.Json.JsonDocumentOptions()) },
                    { new Guid("770bb0c2-d983-4fab-a3ac-f093ee2d119c"), "Alert_condition_for_Allocation_Cost", "Alert condition for Allocation Cost", "Setting the Percentage Budget consumption limit below; sets warning for budget consumption of Allocation Cost . The system will send a notification and alert to the user when the Allocation Cost as a Percentage of Budget exceeds this value.", "", true, "Global", false, System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Alert_condition_for_Allocation_Cost\",\"KeyDisplay\":\"Alert condition for Allocation Cost\",\"Description\":\"Alert condition for Allocation Cost\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^([1-9]|[1-9][0-9]|100)$\"}]", new System.Text.Json.JsonDocumentOptions()) },
                    { new Guid("9407b04a-4394-49ce-9c7a-f082e1d49b92"), "Permission_for_Additional_EL", "Permission for Additional EL", "Entering value of 1 enables the Additional EL to view Allocation and Requisition details created by the Resource Requestor and their Delegate. Entering value of -1 will disable the feature.", "", true, "Global", false, System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Permission_for_Additional_EL\",\"KeyDisplay\":\"Permission for Additional EL\",\"Description\":\"Permission for Additional EL\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^-?1$\"}]", new System.Text.Json.JsonDocumentOptions()) },
                    { new Guid("ab662c2c-a775-4510-9828-69197a7a4440"), "Requisition_form", "Requisition Form Parameters ", "The Optional Parameters available on the Requisition Form can be removed from the form by disabling the respective parameter using the toggle switch.", "", true, "Offerings", true, System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Location\",\"KeyDisplay\":\"Location\",\"Description\":\"Location\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(10|[0-9]|[0-9]\\\\.[0-9]\\u002B)$\"},{\"Key\":\"Same_client\",\"KeyDisplay\":\"Experience working with the same client\",\"Description\":\"Experience working with the same client\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(10|[0-9]|[0-9]\\\\.[0-9]\\u002B)$\"},{\"Key\":\"competency\",\"KeyDisplay\":\"Competency\",\"Description\":\"Competency\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(10|[0-9]|[0-9]\\\\.[0-9]\\u002B)$\"},{\"Key\":\"offerings\",\"KeyDisplay\":\"Offerings\",\"Description\":\"Offerings\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(10|[0-9]|[0-9]\\\\.[0-9]\\u002B)$\"},{\"Key\":\"solutions\",\"KeyDisplay\":\"Solutions\",\"Description\":\"Solutions\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(10|[0-9]|[0-9]\\\\.[0-9]\\u002B)$\"},{\"Key\":\"Industry\",\"KeyDisplay\":\"Industry\",\"Description\":\"Industry\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(10|[0-9]|[0-9]\\\\.[0-9]\\u002B)$\"},{\"Key\":\"Sub_Industry\",\"KeyDisplay\":\"Sub Industry\",\"Description\":\"Sub Industry\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(10|[0-9]|[0-9]\\\\.[0-9]\\u002B)$\"},{\"Key\":\"Skills\",\"KeyDisplay\":\"Skills\",\"Description\":\"Skills\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(10|[0-9]|[0-9]\\\\.[0-9]\\u002B)$\"}]", new System.Text.Json.JsonDocumentOptions()) },
                    { new Guid("c2e22f07-1e9f-49a6-8ed9-00f5740cb37d"), "Amber_condition_for_Budget_Consumption", "Amber condition for Budget Consumption", "Setting the Percentage Budget consumption limit below; sets the Amber indicator when the Allocation Cost as Percentage of Budget exceeds this value.", "", true, "Global", false, System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Amber_condition_for_Budget_Consumption\",\"KeyDisplay\":\"Amber condition for Budget Consumption\",\"Description\":\"Amber condition for Budget Consumption\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^([1-9]|[1-9][0-9]|100)$\"}]", new System.Text.Json.JsonDocumentOptions()) }
                });

            migrationBuilder.InsertData(
                table: "ConfigurationMainBreakup",
                columns: new[] { "Id", "ConfigurationMasterId", "CreatedAt", "CreatedBy", "KeySelector", "MetaValue", "ModifiedAt", "ModifiedBy" },
                values: new object[,]
                {
                    { new Guid("1036796d-554f-4636-b173-36f212a46161"), new Guid("ab662c2c-a775-4510-9828-69197a7a4440"), new DateTime(2024, 11, 6, 7, 6, 41, 479, DateTimeKind.Utc).AddTicks(5736), "system", "DEFAULT", System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Location\",\"DisplayKey\":\"Location\",\"Value\":\"8\"},{\"Key\":\"Same_client\",\"DisplayKey\":\"Experience working with the same client\",\"Value\":\"8\"},{\"Key\":\"competency\",\"DisplayKey\":\"Competency\",\"Value\":\"8\"},{\"Key\":\"offerings\",\"DisplayKey\":\"Offerings\",\"Value\":\"8\"},{\"Key\":\"solutions\",\"DisplayKey\":\"Solutions\",\"Value\":\"8\"},{\"Key\":\"Industry\",\"DisplayKey\":\"Industry\",\"Value\":\"8\"},{\"Key\":\"Sub_Industry\",\"DisplayKey\":\"Sub Industry\",\"Value\":\"8\"},{\"Key\":\"Skills\",\"DisplayKey\":\"Skills\",\"Value\":\"8\"}]", new System.Text.Json.JsonDocumentOptions()), new DateTime(2024, 11, 6, 7, 6, 41, 479, DateTimeKind.Utc).AddTicks(5994), "system" },
                    { new Guid("357cd3ff-0a7b-4962-9453-9f7143b619e0"), new Guid("770bb0c2-d983-4fab-a3ac-f093ee2d119c"), new DateTime(2024, 11, 6, 7, 6, 41, 483, DateTimeKind.Utc).AddTicks(578), "system", "DEFAULT", System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Alert_condition_for_Allocation_Cost\",\"DisplayKey\":\"Alert condition for Allocation Cost\",\"Value\":\"90\"}]", new System.Text.Json.JsonDocumentOptions()), new DateTime(2024, 11, 6, 7, 6, 41, 483, DateTimeKind.Utc).AddTicks(579), "system" },
                    { new Guid("38792be2-5953-440d-84f3-305c24caf621"), new Guid("4f1c9322-8177-4560-b13d-26a0f06b42a2"), new DateTime(2024, 11, 6, 7, 6, 41, 483, DateTimeKind.Utc).AddTicks(1226), "system", "DEFAULT", System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Alert_condition_for_Timesheet_Hours\",\"DisplayKey\":\"Alert condition for Timesheet Hours\",\"Value\":\"90\"}]", new System.Text.Json.JsonDocumentOptions()), new DateTime(2024, 11, 6, 7, 6, 41, 483, DateTimeKind.Utc).AddTicks(1227), "system" },
                    { new Guid("4b1c48ad-f513-463f-950c-ff042bd56293"), new Guid("4fbfd329-cc6f-4c47-8def-1eab16ccff75"), new DateTime(2024, 11, 6, 7, 6, 41, 483, DateTimeKind.Utc).AddTicks(2414), "system", "DEFAULT", System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Permission_for_Additional_Delegate\",\"DisplayKey\":\"Permission for Additional Delegate\",\"Value\":\"-1\"}]", new System.Text.Json.JsonDocumentOptions()), new DateTime(2024, 11, 6, 7, 6, 41, 483, DateTimeKind.Utc).AddTicks(2415), "system" },
                    { new Guid("5320b89d-ed0e-4962-8229-7858c068fb97"), new Guid("4e3f94bf-1f6b-423f-b30c-f043653dbaa0"), new DateTime(2024, 11, 6, 7, 6, 41, 482, DateTimeKind.Utc).AddTicks(9082), "system", "DEFAULT", System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Employee_Response_Review_process_reviewed_by_Resource_Requestor\",\"DisplayKey\":\"Review process of Employee Allocation response\",\"Value\":\"1\"}]", new System.Text.Json.JsonDocumentOptions()), new DateTime(2024, 11, 6, 7, 6, 41, 482, DateTimeKind.Utc).AddTicks(9083), "system" },
                    { new Guid("7479c4c5-4b6d-4909-84eb-b85ea7fd1c40"), new Guid("19e50dfe-3593-4f7a-8aa6-05202c709156"), new DateTime(2024, 11, 6, 7, 6, 41, 482, DateTimeKind.Utc).AddTicks(9645), "system", "DEFAULT", System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Maximum_number_of_parameters_that_can_be_selected_by_Employee_in_Preference_screen\",\"DisplayKey\":\"Maximum number of parameters for selection in Preference screen\",\"Value\":\"5\"}]", new System.Text.Json.JsonDocumentOptions()), new DateTime(2024, 11, 6, 7, 6, 41, 482, DateTimeKind.Utc).AddTicks(9646), "system" },
                    { new Guid("786ef499-4005-4f27-8d23-c580b735821e"), new Guid("c2e22f07-1e9f-49a6-8ed9-00f5740cb37d"), new DateTime(2024, 11, 6, 7, 6, 41, 483, DateTimeKind.Utc).AddTicks(132), "system", "DEFAULT", System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Amber_condition_for_Budget_Consumption\",\"DisplayKey\":\"Amber condition for Budget Consumption\",\"Value\":\"80\"}]", new System.Text.Json.JsonDocumentOptions()), new DateTime(2024, 11, 6, 7, 6, 41, 483, DateTimeKind.Utc).AddTicks(133), "system" },
                    { new Guid("991dcbab-b651-49a1-a76b-fa789d0091c6"), new Guid("05d0c713-5762-44f1-a2be-4f7955884bdc"), new DateTime(2024, 11, 6, 7, 6, 41, 482, DateTimeKind.Utc).AddTicks(8092), "system", "DEFAULT", System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Resource_allocation_review\",\"DisplayKey\":\"Resource allocation review by Reviewer\",\"Value\":\"1\"}]", new System.Text.Json.JsonDocumentOptions()), new DateTime(2024, 11, 6, 7, 6, 41, 482, DateTimeKind.Utc).AddTicks(8093), "system" },
                    { new Guid("f98e0999-3c81-4731-ae0d-6182c2c8ec80"), new Guid("1bdf3a16-4d0c-4e2d-aad3-1727c65c3f31"), new DateTime(2024, 11, 6, 7, 6, 41, 482, DateTimeKind.Utc).AddTicks(8610), "system", "DEFAULT", System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Number_of_days_for_employee_to_confirm_on_their_allocations\",\"DisplayKey\":\"Number of days for employee to confirm on their allocations\",\"Value\":\"1\"}]", new System.Text.Json.JsonDocumentOptions()), new DateTime(2024, 11, 6, 7, 6, 41, 482, DateTimeKind.Utc).AddTicks(8611), "system" },
                    { new Guid("f9bd67ef-309e-404b-a246-f43f26d28c67"), new Guid("9407b04a-4394-49ce-9c7a-f082e1d49b92"), new DateTime(2024, 11, 6, 7, 6, 41, 483, DateTimeKind.Utc).AddTicks(1977), "system", "DEFAULT", System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Permission_for_Additional_EL\",\"DisplayKey\":\"Permission for Additional EL\",\"Value\":\"-1\"}]", new System.Text.Json.JsonDocumentOptions()), new DateTime(2024, 11, 6, 7, 6, 41, 483, DateTimeKind.Utc).AddTicks(1982), "system" },
                    { new Guid("fdf54f09-c6fe-4246-812b-e0008809e9db"), new Guid("1023ae1a-f30c-475b-9d4a-d33eab1cd4fd"), new DateTime(2024, 11, 6, 7, 6, 41, 482, DateTimeKind.Utc).AddTicks(7333), "system", "DEFAULT", System.Text.Json.JsonDocument.Parse("[{\"Key\":\"System_Suggestion_For_Requisition_Percentage_Match\",\"DisplayKey\":\"System Suggestion For Requisition Percentage Match\",\"Value\":\"60\"}]", new System.Text.Json.JsonDocumentOptions()), new DateTime(2024, 11, 6, 7, 6, 41, 482, DateTimeKind.Utc).AddTicks(7335), "system" },
                    { new Guid("ff7cb7c7-c52f-4d93-9cb5-db58072d0d54"), new Guid("524985c2-ebcc-4272-af6d-c4d8283849b0"), new DateTime(2024, 11, 6, 7, 6, 41, 483, DateTimeKind.Utc).AddTicks(2852), "system", "DEFAULT", System.Text.Json.JsonDocument.Parse("[{\"Key\":\"NUMBER_OF_DAYS_FOR_SKILL_APPROVAL_DUE_DATE\",\"DisplayKey\":\"Number Of Days For Skill Approval Duedate\",\"Value\":\"false\"}]", new System.Text.Json.JsonDocumentOptions()), new DateTime(2024, 11, 6, 7, 6, 41, 483, DateTimeKind.Utc).AddTicks(2852), "system" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ConfigurationMainBreakup",
                keyColumn: "Id",
                keyValue: new Guid("1036796d-554f-4636-b173-36f212a46161"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMainBreakup",
                keyColumn: "Id",
                keyValue: new Guid("357cd3ff-0a7b-4962-9453-9f7143b619e0"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMainBreakup",
                keyColumn: "Id",
                keyValue: new Guid("38792be2-5953-440d-84f3-305c24caf621"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMainBreakup",
                keyColumn: "Id",
                keyValue: new Guid("4b1c48ad-f513-463f-950c-ff042bd56293"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMainBreakup",
                keyColumn: "Id",
                keyValue: new Guid("5320b89d-ed0e-4962-8229-7858c068fb97"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMainBreakup",
                keyColumn: "Id",
                keyValue: new Guid("7479c4c5-4b6d-4909-84eb-b85ea7fd1c40"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMainBreakup",
                keyColumn: "Id",
                keyValue: new Guid("786ef499-4005-4f27-8d23-c580b735821e"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMainBreakup",
                keyColumn: "Id",
                keyValue: new Guid("991dcbab-b651-49a1-a76b-fa789d0091c6"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMainBreakup",
                keyColumn: "Id",
                keyValue: new Guid("f98e0999-3c81-4731-ae0d-6182c2c8ec80"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMainBreakup",
                keyColumn: "Id",
                keyValue: new Guid("f9bd67ef-309e-404b-a246-f43f26d28c67"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMainBreakup",
                keyColumn: "Id",
                keyValue: new Guid("fdf54f09-c6fe-4246-812b-e0008809e9db"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMainBreakup",
                keyColumn: "Id",
                keyValue: new Guid("ff7cb7c7-c52f-4d93-9cb5-db58072d0d54"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMaster",
                keyColumn: "Id",
                keyValue: new Guid("05d0c713-5762-44f1-a2be-4f7955884bdc"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMaster",
                keyColumn: "Id",
                keyValue: new Guid("1023ae1a-f30c-475b-9d4a-d33eab1cd4fd"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMaster",
                keyColumn: "Id",
                keyValue: new Guid("19e50dfe-3593-4f7a-8aa6-05202c709156"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMaster",
                keyColumn: "Id",
                keyValue: new Guid("1bdf3a16-4d0c-4e2d-aad3-1727c65c3f31"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMaster",
                keyColumn: "Id",
                keyValue: new Guid("4e3f94bf-1f6b-423f-b30c-f043653dbaa0"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMaster",
                keyColumn: "Id",
                keyValue: new Guid("4f1c9322-8177-4560-b13d-26a0f06b42a2"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMaster",
                keyColumn: "Id",
                keyValue: new Guid("4fbfd329-cc6f-4c47-8def-1eab16ccff75"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMaster",
                keyColumn: "Id",
                keyValue: new Guid("524985c2-ebcc-4272-af6d-c4d8283849b0"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMaster",
                keyColumn: "Id",
                keyValue: new Guid("770bb0c2-d983-4fab-a3ac-f093ee2d119c"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMaster",
                keyColumn: "Id",
                keyValue: new Guid("9407b04a-4394-49ce-9c7a-f082e1d49b92"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMaster",
                keyColumn: "Id",
                keyValue: new Guid("ab662c2c-a775-4510-9828-69197a7a4440"));

            migrationBuilder.DeleteData(
                table: "ConfigurationMaster",
                keyColumn: "Id",
                keyValue: new Guid("c2e22f07-1e9f-49a6-8ed9-00f5740cb37d"));

            migrationBuilder.InsertData(
                table: "ConfigurationMaster",
                columns: new[] { "Id", "ConfigGroup", "ConfigGroupDisplay", "ConfigNote", "Description", "GlobalDefaultDisplay", "SelectorConfigType", "SelectorWiseDisplay", "schema" },
                values: new object[,]
                {
                    { new Guid("16e9bec9-891e-4a22-8b10-39249a4b4666"), "Number_of_days_for_employee_to_confirm_on_their_allocations", "Number of days for employee to confirm on their allocations", "Enter the maximum number of days given to the employee to approve the allocation request of the Reviewer. Post this period; the request will be auto-approved.", "", true, "Offerings", true, System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Number_of_days_for_employee_to_confirm_on_their_allocations\",\"KeyDisplay\":\"Number of days for employee to confirm on their allocations\",\"Description\":\"Number of days for employee to confirm on their allocations\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(-1|[1-4])$\"}]", new System.Text.Json.JsonDocumentOptions()) },
                    { new Guid("21f5c6a2-336f-4176-a1af-111e7cbde8ac"), "Requisition_form", "Requisition Form Parameters ", "The Optional Parameters available on the Requisition Form can be removed from the form by disabling the respective parameter using the toggle switch.", "", true, "Offerings", true, System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Location\",\"KeyDisplay\":\"Location\",\"Description\":\"Location\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(10|[0-9]|[0-9]\\\\.[0-9]\\u002B)$\"},{\"Key\":\"Same_client\",\"KeyDisplay\":\"Experience working with the same client\",\"Description\":\"Experience working with the same client\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(10|[0-9]|[0-9]\\\\.[0-9]\\u002B)$\"},{\"Key\":\"competency\",\"KeyDisplay\":\"Competency\",\"Description\":\"Competency\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(10|[0-9]|[0-9]\\\\.[0-9]\\u002B)$\"},{\"Key\":\"offerings\",\"KeyDisplay\":\"Offerings\",\"Description\":\"Offerings\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(10|[0-9]|[0-9]\\\\.[0-9]\\u002B)$\"},{\"Key\":\"solutions\",\"KeyDisplay\":\"Solutions\",\"Description\":\"Solutions\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(10|[0-9]|[0-9]\\\\.[0-9]\\u002B)$\"},{\"Key\":\"Industry\",\"KeyDisplay\":\"Industry\",\"Description\":\"Industry\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(10|[0-9]|[0-9]\\\\.[0-9]\\u002B)$\"},{\"Key\":\"Sub_Industry\",\"KeyDisplay\":\"Sub Industry\",\"Description\":\"Sub Industry\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(10|[0-9]|[0-9]\\\\.[0-9]\\u002B)$\"},{\"Key\":\"Skills\",\"KeyDisplay\":\"Skills\",\"Description\":\"Skills\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(10|[0-9]|[0-9]\\\\.[0-9]\\u002B)$\"}]", new System.Text.Json.JsonDocumentOptions()) },
                    { new Guid("38eccefa-1bd9-43ff-8d9e-20e279845761"), "NUMBER_OF_DAYS_FOR_SKILL_APPROVAL_DUE_DATE", "Number Of Days For Skill Approval Duedate", "Enter a number of days to define the Due Date for the task for the Skill Reviewer.", "", true, "Global", false, System.Text.Json.JsonDocument.Parse("[{\"Key\":\"NUMBER_OF_DAYS_FOR_SKILL_APPROVAL_DUE_DATE\",\"KeyDisplay\":\"Number Of Days For Skill Approval Duedate\",\"Description\":\"Number Of Days For Skill Approval Duedate\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^[0-5]$\"}]", new System.Text.Json.JsonDocumentOptions()) },
                    { new Guid("57a9fbb4-8ca0-40da-89ff-655167c6a5ec"), "Alert_condition_for_Allocation_Cost", "Alert condition for Allocation Cost", "Setting the Percentage Budget consumption limit below; sets warning for budget consumption of Allocation Cost . The system will send a notification and alert to the user when the Allocation Cost as a Percentage of Budget exceeds this value.", "", true, "Global", false, System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Alert_condition_for_Allocation_Cost\",\"KeyDisplay\":\"Alert condition for Allocation Cost\",\"Description\":\"Alert condition for Allocation Cost\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^([1-9]|[1-9][0-9]|100)$\"}]", new System.Text.Json.JsonDocumentOptions()) },
                    { new Guid("630af6fb-4ce7-4c06-a04f-daca4fa5e997"), "Resource_allocation_review", "Resource allocation review by Reviewer", "Turning on this toggle at the BU level (or Expertise Level) enables the Resource Allocation Approval Workflow.  Enter the maximum number of days given to the reviewer to approve the allocation. Post this period; the request will be auto-approved. Turning off; will disable the Reviewer workflow feature.", "", true, "Offerings", true, System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Resource_allocation_review\",\"KeyDisplay\":\"Resource allocation review by Reviewer\",\"Description\":\"Resource allocation review by Reviewer\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(-1|[1-4])$\"}]", new System.Text.Json.JsonDocumentOptions()) },
                    { new Guid("732ead14-624e-46f9-80e4-4cd7fce31635"), "Maximum_number_of_parameters_that_can_be_selected_by_Employee_in_Preference_screen", "Maximum number of parameters for selection in Preference screen", "Enter a number below for defining the maximum number of user inputs values allowed to an employee for each Preference Parameter in the My Preferences Screen. ", "", true, "Global", false, System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Maximum_number_of_parameters_that_can_be_selected_by_Employee_in_Preference_screen\",\"KeyDisplay\":\"Maximum number of parameters for selection in Preference screen\",\"Description\":\"Maximum number of parameters for selection in Preference screen\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^[0-5]$\"}]", new System.Text.Json.JsonDocumentOptions()) },
                    { new Guid("8f729288-fc9b-4e9d-abb0-6ce8673b4cbd"), "Amber_condition_for_Budget_Consumption", "Amber condition for Budget Consumption", "Setting the Percentage Budget consumption limit below; sets the Amber indicator when the Allocation Cost as Percentage of Budget exceeds this value.", "", true, "Global", false, System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Amber_condition_for_Budget_Consumption\",\"KeyDisplay\":\"Amber condition for Budget Consumption\",\"Description\":\"Amber condition for Budget Consumption\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^([1-9]|[1-9][0-9]|100)$\"}]", new System.Text.Json.JsonDocumentOptions()) },
                    { new Guid("a68b331c-cc3c-4314-99d0-01aa830c1b32"), "Employee_Response_Review_process_reviewed_by_Resource_Requestor", "Review process of Employee Allocation response", "Turning on this toggle the at the BU level (or Expertise Level); enables the Employee Allocation Review Process for the case when the employee has not accepted the allocation. Enter the maximum number of days given to the reviewer to approve the  request. Post this period; the request will be auto-approved. Turning off; will disable the Reviewer workflow feature for this scenario.", "", true, "Offerings", true, System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Employee_Response_Review_process_reviewed_by_Resource_Requestor\",\"KeyDisplay\":\"Review process of Employee Allocation response\",\"Description\":\"Review process of Employee Allocation response\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(-1|[1-4])$\"}]", new System.Text.Json.JsonDocumentOptions()) },
                    { new Guid("b4b8beac-596a-4274-b671-0396655caef3"), "Permission_for_Additional_Delegate", "Permission for Additional Delegate", "Turning on this toggle enables the Additional Delegate to view Allocation and Requisition details created by the Resource Requestor and their Delegate. Turning off will disable this feature. ", "", true, "Global", false, System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Permission_for_Additional_Delegate\",\"KeyDisplay\":\"Permission for Additional Delegate\",\"Description\":\"Permission for Additional Delegate\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^-?1$\"}]", new System.Text.Json.JsonDocumentOptions()) },
                    { new Guid("c6bfb6e5-2274-4a58-8b1a-b7e2cdd8c273"), "System_Suggestion_For_Requisition_Percentage_Match", "Match Range  for System Suggestions for a Requisition", "The results of the System Suggestions against a requisition are expressed as a match percentage.  By setting the minimum match range percentage below the system suggestions results will be limited to show results above the specified percentage value. E.g. 10 entered below; will show system suggestions results where the resource match score is above 10%.", "", true, "Offerings", true, System.Text.Json.JsonDocument.Parse("[{\"Key\":\"System_Suggestion_For_Requisition_Percentage_Match\",\"KeyDisplay\":\"System Suggestion For Requisition Percentage Match\",\"Description\":\"System Suggestion For Requisition Percentage Match\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^([1-9]|[1-9][0-9]|100)$\"}]", new System.Text.Json.JsonDocumentOptions()) },
                    { new Guid("e55ce822-2c77-4168-831a-9801c4e565f8"), "Permission_for_Additional_EL", "Permission for Additional EL", "Turning on this toggle enables the Additional EL to view Allocation and Requisition details created by the Resource Requestor and their Delegate.  Turning off will disable this feature. ", "", true, "Global", false, System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Permission_for_Additional_EL\",\"KeyDisplay\":\"Permission for Additional EL\",\"Description\":\"Permission for Additional EL\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^-?1$\"}]", new System.Text.Json.JsonDocumentOptions()) },
                    { new Guid("fa483b93-5d4a-4707-81d6-23f7c45a39e8"), "Alert_condition_for_Timesheet_Hours", "Alert condition for Timesheet Hours", "Setting the Percentage Budget consumption limit below; sets warning for budget consumption of Actuals (Time Cost) . The system will send a notification and alert to the user when the Actuals (Time Cost) as a Percentage of Budget exceeds this value.", "", true, "Global", false, System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Alert_condition_for_Timesheet_Hours\",\"KeyDisplay\":\"Alert condition for Timesheet Hours\",\"Description\":\"Alert condition for Timesheet Hours\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^([1-9]|[1-9][0-9]|100)$\"}]", new System.Text.Json.JsonDocumentOptions()) }
                });

            migrationBuilder.InsertData(
                table: "ConfigurationMainBreakup",
                columns: new[] { "Id", "ConfigurationMasterId", "CreatedAt", "CreatedBy", "KeySelector", "MetaValue", "ModifiedAt", "ModifiedBy" },
                values: new object[,]
                {
                    { new Guid("1e184704-6d35-4b19-b7fa-e2cfd411b237"), new Guid("e55ce822-2c77-4168-831a-9801c4e565f8"), new DateTime(2024, 11, 5, 5, 59, 53, 248, DateTimeKind.Utc).AddTicks(390), "system", "DEFAULT", System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Permission_for_Additional_EL\",\"DisplayKey\":\"Permission for Additional EL\",\"Value\":\"-1\"}]", new System.Text.Json.JsonDocumentOptions()), new DateTime(2024, 11, 5, 5, 59, 53, 248, DateTimeKind.Utc).AddTicks(391), "system" },
                    { new Guid("6b320aa8-7490-4ad7-b0fe-fd286da40e95"), new Guid("c6bfb6e5-2274-4a58-8b1a-b7e2cdd8c273"), new DateTime(2024, 11, 5, 5, 59, 53, 247, DateTimeKind.Utc).AddTicks(6617), "system", "DEFAULT", System.Text.Json.JsonDocument.Parse("[{\"Key\":\"System_Suggestion_For_Requisition_Percentage_Match\",\"DisplayKey\":\"System Suggestion For Requisition Percentage Match\",\"Value\":\"60\"}]", new System.Text.Json.JsonDocumentOptions()), new DateTime(2024, 11, 5, 5, 59, 53, 247, DateTimeKind.Utc).AddTicks(6618), "system" },
                    { new Guid("a1f30742-710b-4939-83d2-ce3a6f0d5a0b"), new Guid("732ead14-624e-46f9-80e4-4cd7fce31635"), new DateTime(2024, 11, 5, 5, 59, 53, 247, DateTimeKind.Utc).AddTicks(8806), "system", "DEFAULT", System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Maximum_number_of_parameters_that_can_be_selected_by_Employee_in_Preference_screen\",\"DisplayKey\":\"Maximum number of parameters for selection in Preference screen\",\"Value\":\"5\"}]", new System.Text.Json.JsonDocumentOptions()), new DateTime(2024, 11, 5, 5, 59, 53, 247, DateTimeKind.Utc).AddTicks(8807), "system" },
                    { new Guid("a8243a70-1598-4154-9170-ae944a51fa39"), new Guid("8f729288-fc9b-4e9d-abb0-6ce8673b4cbd"), new DateTime(2024, 11, 5, 5, 59, 53, 247, DateTimeKind.Utc).AddTicks(9264), "system", "DEFAULT", System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Amber_condition_for_Budget_Consumption\",\"DisplayKey\":\"Amber condition for Budget Consumption\",\"Value\":\"80\"}]", new System.Text.Json.JsonDocumentOptions()), new DateTime(2024, 11, 5, 5, 59, 53, 247, DateTimeKind.Utc).AddTicks(9265), "system" },
                    { new Guid("bc872dea-93f9-4973-aad0-9ac860f36d1c"), new Guid("16e9bec9-891e-4a22-8b10-39249a4b4666"), new DateTime(2024, 11, 5, 5, 59, 53, 247, DateTimeKind.Utc).AddTicks(7809), "system", "DEFAULT", System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Number_of_days_for_employee_to_confirm_on_their_allocations\",\"DisplayKey\":\"Number of days for employee to confirm on their allocations\",\"Value\":\"1\"}]", new System.Text.Json.JsonDocumentOptions()), new DateTime(2024, 11, 5, 5, 59, 53, 247, DateTimeKind.Utc).AddTicks(7810), "system" },
                    { new Guid("c03388c6-9467-4ee4-b69d-97e49ca0f05a"), new Guid("630af6fb-4ce7-4c06-a04f-daca4fa5e997"), new DateTime(2024, 11, 5, 5, 59, 53, 247, DateTimeKind.Utc).AddTicks(7292), "system", "DEFAULT", System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Resource_allocation_review\",\"DisplayKey\":\"Resource allocation review by Reviewer\",\"Value\":\"1\"}]", new System.Text.Json.JsonDocumentOptions()), new DateTime(2024, 11, 5, 5, 59, 53, 247, DateTimeKind.Utc).AddTicks(7293), "system" },
                    { new Guid("c8af7c20-3c71-430e-ac5e-477cc5701c8a"), new Guid("b4b8beac-596a-4274-b671-0396655caef3"), new DateTime(2024, 11, 5, 5, 59, 53, 248, DateTimeKind.Utc).AddTicks(709), "system", "DEFAULT", System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Permission_for_Additional_Delegate\",\"DisplayKey\":\"Permission for Additional Delegate\",\"Value\":\"-1\"}]", new System.Text.Json.JsonDocumentOptions()), new DateTime(2024, 11, 5, 5, 59, 53, 248, DateTimeKind.Utc).AddTicks(710), "system" },
                    { new Guid("d4a16e4c-be92-4767-9183-74b9502be153"), new Guid("21f5c6a2-336f-4176-a1af-111e7cbde8ac"), new DateTime(2024, 11, 5, 5, 59, 53, 246, DateTimeKind.Utc).AddTicks(4457), "system", "DEFAULT", System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Location\",\"DisplayKey\":\"Location\",\"Value\":\"8\"},{\"Key\":\"Same_client\",\"DisplayKey\":\"Experience working with the same client\",\"Value\":\"8\"},{\"Key\":\"competency\",\"DisplayKey\":\"Competency\",\"Value\":\"8\"},{\"Key\":\"offerings\",\"DisplayKey\":\"Offerings\",\"Value\":\"8\"},{\"Key\":\"solutions\",\"DisplayKey\":\"Solutions\",\"Value\":\"8\"},{\"Key\":\"Industry\",\"DisplayKey\":\"Industry\",\"Value\":\"8\"},{\"Key\":\"Sub_Industry\",\"DisplayKey\":\"Sub Industry\",\"Value\":\"8\"},{\"Key\":\"Skills\",\"DisplayKey\":\"Skills\",\"Value\":\"8\"}]", new System.Text.Json.JsonDocumentOptions()), new DateTime(2024, 11, 5, 5, 59, 53, 246, DateTimeKind.Utc).AddTicks(4642), "system" },
                    { new Guid("db230359-f321-4221-9b61-02acc9772a31"), new Guid("fa483b93-5d4a-4707-81d6-23f7c45a39e8"), new DateTime(2024, 11, 5, 5, 59, 53, 247, DateTimeKind.Utc).AddTicks(9990), "system", "DEFAULT", System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Alert_condition_for_Timesheet_Hours\",\"DisplayKey\":\"Alert condition for Timesheet Hours\",\"Value\":\"90\"}]", new System.Text.Json.JsonDocumentOptions()), new DateTime(2024, 11, 5, 5, 59, 53, 247, DateTimeKind.Utc).AddTicks(9991), "system" },
                    { new Guid("e10d95e2-59af-4756-9972-c3933991f9ba"), new Guid("57a9fbb4-8ca0-40da-89ff-655167c6a5ec"), new DateTime(2024, 11, 5, 5, 59, 53, 247, DateTimeKind.Utc).AddTicks(9624), "system", "DEFAULT", System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Alert_condition_for_Allocation_Cost\",\"DisplayKey\":\"Alert condition for Allocation Cost\",\"Value\":\"90\"}]", new System.Text.Json.JsonDocumentOptions()), new DateTime(2024, 11, 5, 5, 59, 53, 247, DateTimeKind.Utc).AddTicks(9625), "system" },
                    { new Guid("e63a4286-bf53-4fff-8b82-99894f5e372a"), new Guid("a68b331c-cc3c-4314-99d0-01aa830c1b32"), new DateTime(2024, 11, 5, 5, 59, 53, 247, DateTimeKind.Utc).AddTicks(8367), "system", "DEFAULT", System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Employee_Response_Review_process_reviewed_by_Resource_Requestor\",\"DisplayKey\":\"Review process of Employee Allocation response\",\"Value\":\"1\"}]", new System.Text.Json.JsonDocumentOptions()), new DateTime(2024, 11, 5, 5, 59, 53, 247, DateTimeKind.Utc).AddTicks(8367), "system" },
                    { new Guid("f8579e16-852a-41e2-9a36-14c91371f1fa"), new Guid("38eccefa-1bd9-43ff-8d9e-20e279845761"), new DateTime(2024, 11, 5, 5, 59, 53, 248, DateTimeKind.Utc).AddTicks(1032), "system", "DEFAULT", System.Text.Json.JsonDocument.Parse("[{\"Key\":\"NUMBER_OF_DAYS_FOR_SKILL_APPROVAL_DUE_DATE\",\"DisplayKey\":\"Number Of Days For Skill Approval Duedate\",\"Value\":\"false\"}]", new System.Text.Json.JsonDocumentOptions()), new DateTime(2024, 11, 5, 5, 59, 53, 248, DateTimeKind.Utc).AddTicks(1033), "system" }
                });
        }
    }
}
