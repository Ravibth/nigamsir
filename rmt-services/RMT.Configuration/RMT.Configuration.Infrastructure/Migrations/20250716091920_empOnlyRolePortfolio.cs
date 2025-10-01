using System;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RMT.Configuration.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class empOnlyRolePortfolio : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 232L);

            migrationBuilder.DeleteData(
                table: "RoleMenu",
                keyColumn: "Id",
                keyValue: 236L);

            migrationBuilder.UpdateData(
                table: "ConfigurationMainBreakup",
                keyColumn: "Id",
                keyValue: new Guid("2137d048-0e08-46d9-b86b-05c15f11613f"),
                column: "MetaValue",
                value: System.Text.Json.JsonDocument.Parse("[{\"Key\":\"NUMBER_OF_DAYS_FOR_SKILL_APPROVAL_DUE_DATE\",\"DisplayKey\":\"Number Of Days For Skill Approval Duedate\",\"Value\":\"3\"}]", new System.Text.Json.JsonDocumentOptions()));

            migrationBuilder.UpdateData(
                table: "ConfigurationMainBreakup",
                keyColumn: "Id",
                keyValue: new Guid("303ac0a5-b366-4258-8bfb-ea342e38d2ba"),
                column: "MetaValue",
                value: System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Amber_condition_for_Budget_Consumption\",\"DisplayKey\":\"Amber condition for Budget Consumption\",\"Value\":\"80\"}]", new System.Text.Json.JsonDocumentOptions()));

            migrationBuilder.UpdateData(
                table: "ConfigurationMainBreakup",
                keyColumn: "Id",
                keyValue: new Guid("31db74b1-6a80-4164-ae02-d3863aab976f"),
                column: "MetaValue",
                value: System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Location\",\"DisplayKey\":\"Location\",\"Value\":\"8\"},{\"Key\":\"Same_client\",\"DisplayKey\":\"Experience working with the same client\",\"Value\":\"8\"},{\"Key\":\"competency\",\"DisplayKey\":\"Competency\",\"Value\":\"8\"},{\"Key\":\"offerings\",\"DisplayKey\":\"Offerings\",\"Value\":\"8\"},{\"Key\":\"solutions\",\"DisplayKey\":\"Solutions\",\"Value\":\"8\"},{\"Key\":\"Industry\",\"DisplayKey\":\"Industry\",\"Value\":\"8\"},{\"Key\":\"Sub_Industry\",\"DisplayKey\":\"Sub Industry\",\"Value\":\"8\"},{\"Key\":\"Skills\",\"DisplayKey\":\"Skills\",\"Value\":\"8\"}]", new System.Text.Json.JsonDocumentOptions()));

            migrationBuilder.UpdateData(
                table: "ConfigurationMainBreakup",
                keyColumn: "Id",
                keyValue: new Guid("38770e66-cc61-4ddb-8f39-a2c5bfbe2000"),
                column: "MetaValue",
                value: System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Number_of_days_for_employee_to_confirm_on_their_allocations\",\"DisplayKey\":\"Number of days for employee to confirm on their allocations\",\"Value\":\"1\"}]", new System.Text.Json.JsonDocumentOptions()));

            migrationBuilder.UpdateData(
                table: "ConfigurationMainBreakup",
                keyColumn: "Id",
                keyValue: new Guid("5bca1b3f-29a1-4b98-8f66-ebfa5c668df5"),
                column: "MetaValue",
                value: System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Employee_Response_Review_process_reviewed_by_Resource_Requestor\",\"DisplayKey\":\"Review process of Employee Allocation response\",\"Value\":\"1\"}]", new System.Text.Json.JsonDocumentOptions()));

            migrationBuilder.UpdateData(
                table: "ConfigurationMainBreakup",
                keyColumn: "Id",
                keyValue: new Guid("7713e0f5-7259-4610-bf94-97db59b49fa8"),
                column: "MetaValue",
                value: System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Permission_for_Additional_EL\",\"DisplayKey\":\"Permission for Additional EL\",\"Value\":\"-1\"}]", new System.Text.Json.JsonDocumentOptions()));

            migrationBuilder.UpdateData(
                table: "ConfigurationMainBreakup",
                keyColumn: "Id",
                keyValue: new Guid("850f9b34-307a-4c28-a737-882d505afd36"),
                column: "MetaValue",
                value: System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Alert_condition_for_Allocation_Cost\",\"DisplayKey\":\"Alert condition for Allocation Cost\",\"Value\":\"90\"}]", new System.Text.Json.JsonDocumentOptions()));

            migrationBuilder.UpdateData(
                table: "ConfigurationMainBreakup",
                keyColumn: "Id",
                keyValue: new Guid("868dbe58-2086-48b9-bb6b-ec70d01178a4"),
                column: "MetaValue",
                value: System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Resource_allocation_review\",\"DisplayKey\":\"Resource allocation review by Reviewer\",\"Value\":\"1\"}]", new System.Text.Json.JsonDocumentOptions()));

            migrationBuilder.UpdateData(
                table: "ConfigurationMainBreakup",
                keyColumn: "Id",
                keyValue: new Guid("b3d0f14d-fb5e-44e9-9abf-1617c2d6a565"),
                column: "MetaValue",
                value: System.Text.Json.JsonDocument.Parse("[{\"Key\":\"System_Suggestion_For_Requisition_Percentage_Match\",\"DisplayKey\":\"System Suggestion For Requisition Percentage Match\",\"Value\":\"60\"}]", new System.Text.Json.JsonDocumentOptions()));

            migrationBuilder.UpdateData(
                table: "ConfigurationMainBreakup",
                keyColumn: "Id",
                keyValue: new Guid("b5357e78-ca69-4dc2-a69a-cbaf2952cb47"),
                column: "MetaValue",
                value: System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Permission_for_Additional_Delegate\",\"DisplayKey\":\"Permission for Additional Delegate\",\"Value\":\"-1\"}]", new System.Text.Json.JsonDocumentOptions()));

            migrationBuilder.UpdateData(
                table: "ConfigurationMainBreakup",
                keyColumn: "Id",
                keyValue: new Guid("c2dc3ca6-80a2-4123-9910-e2cb1aeec180"),
                column: "MetaValue",
                value: System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Employee_Response_Review_process_reviewed_by_Supercoach\",\"DisplayKey\":\"Resource allocation review by Supercoach\",\"Value\":\"1\"}]", new System.Text.Json.JsonDocumentOptions()));

            migrationBuilder.UpdateData(
                table: "ConfigurationMainBreakup",
                keyColumn: "Id",
                keyValue: new Guid("d0989935-537b-4499-ab34-4ae4f02adfa5"),
                column: "MetaValue",
                value: System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Maximum_number_of_parameters_that_can_be_selected_by_Employee_in_Preference_screen\",\"DisplayKey\":\"Maximum number of parameters for selection in Preference screen\",\"Value\":\"5\"}]", new System.Text.Json.JsonDocumentOptions()));

            migrationBuilder.UpdateData(
                table: "ConfigurationMainBreakup",
                keyColumn: "Id",
                keyValue: new Guid("e3238e31-8ac2-4ead-aef4-5c39ff07d7b6"),
                column: "MetaValue",
                value: System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Alert_condition_for_Timesheet_Hours\",\"DisplayKey\":\"Alert condition for Timesheet Hours\",\"Value\":\"90\"}]", new System.Text.Json.JsonDocumentOptions()));

            migrationBuilder.UpdateData(
                table: "ConfigurationMaster",
                keyColumn: "Id",
                keyValue: new Guid("0672de0b-6dee-4d11-a608-df9cbc75fd3c"),
                column: "schema",
                value: System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Permission_for_Additional_Delegate\",\"KeyDisplay\":\"Permission for Additional Delegate\",\"Description\":\"Permission for Additional Delegate\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^-?1$\"}]", new System.Text.Json.JsonDocumentOptions()));

            migrationBuilder.UpdateData(
                table: "ConfigurationMaster",
                keyColumn: "Id",
                keyValue: new Guid("0748bb53-c8bb-4c03-8d9c-ba9318758aea"),
                column: "schema",
                value: System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Alert_condition_for_Allocation_Cost\",\"KeyDisplay\":\"Alert condition for Allocation Cost\",\"Description\":\"Alert condition for Allocation Cost\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^([1-9]|[1-9][0-9]|100)$\"}]", new System.Text.Json.JsonDocumentOptions()));

            migrationBuilder.UpdateData(
                table: "ConfigurationMaster",
                keyColumn: "Id",
                keyValue: new Guid("2f185e77-16d0-4151-ad38-0f7ddd93a22f"),
                column: "schema",
                value: System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Amber_condition_for_Budget_Consumption\",\"KeyDisplay\":\"Amber condition for Budget Consumption\",\"Description\":\"Amber condition for Budget Consumption\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^([1-9]|[1-9][0-9]|100)$\"}]", new System.Text.Json.JsonDocumentOptions()));

            migrationBuilder.UpdateData(
                table: "ConfigurationMaster",
                keyColumn: "Id",
                keyValue: new Guid("40b4d939-d7e4-4376-9c05-2023dacbec67"),
                column: "schema",
                value: System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Permission_for_Additional_EL\",\"KeyDisplay\":\"Permission for Additional EL\",\"Description\":\"Permission for Additional EL\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^-?1$\"}]", new System.Text.Json.JsonDocumentOptions()));

            migrationBuilder.UpdateData(
                table: "ConfigurationMaster",
                keyColumn: "Id",
                keyValue: new Guid("4c0b7855-ce34-4fe1-87bc-8b1b5fa3c30d"),
                column: "schema",
                value: System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Number_of_days_for_employee_to_confirm_on_their_allocations\",\"KeyDisplay\":\"Number of days for employee to confirm on their allocations\",\"Description\":\"Number of days for employee to confirm on their allocations\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(-1|[1-4])$\"}]", new System.Text.Json.JsonDocumentOptions()));

            migrationBuilder.UpdateData(
                table: "ConfigurationMaster",
                keyColumn: "Id",
                keyValue: new Guid("69ed0225-64db-445a-bfef-bd739835eb87"),
                column: "schema",
                value: System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Employee_Response_Review_process_reviewed_by_Supercoach\",\"KeyDisplay\":\"Resource allocation review by Supercoach\",\"Description\":\"Resource allocation review by Supercoach\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(-1|[1-4])$\"}]", new System.Text.Json.JsonDocumentOptions()));

            migrationBuilder.UpdateData(
                table: "ConfigurationMaster",
                keyColumn: "Id",
                keyValue: new Guid("6e2f6490-d03a-4ade-854e-fb7e1ad43cc5"),
                column: "schema",
                value: System.Text.Json.JsonDocument.Parse("[{\"Key\":\"System_Suggestion_For_Requisition_Percentage_Match\",\"KeyDisplay\":\"System Suggestion For Requisition Percentage Match\",\"Description\":\"System Suggestion For Requisition Percentage Match\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^([1-9]|[1-9][0-9]|100)$\"}]", new System.Text.Json.JsonDocumentOptions()));

            migrationBuilder.UpdateData(
                table: "ConfigurationMaster",
                keyColumn: "Id",
                keyValue: new Guid("9999ce5f-c6aa-404e-aeda-1334a69641d9"),
                column: "schema",
                value: System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Alert_condition_for_Timesheet_Hours\",\"KeyDisplay\":\"Alert condition for Timesheet Hours\",\"Description\":\"Alert condition for Timesheet Hours\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^([1-9]|[1-9][0-9]|100)$\"}]", new System.Text.Json.JsonDocumentOptions()));

            migrationBuilder.UpdateData(
                table: "ConfigurationMaster",
                keyColumn: "Id",
                keyValue: new Guid("bbf7f6e5-bd62-4e2c-9e82-c5e2313c23ba"),
                column: "schema",
                value: System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Resource_allocation_review\",\"KeyDisplay\":\"Resource allocation review by Reviewer\",\"Description\":\"Resource allocation review by Reviewer\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(-1|[1-4])$\"}]", new System.Text.Json.JsonDocumentOptions()));

            migrationBuilder.UpdateData(
                table: "ConfigurationMaster",
                keyColumn: "Id",
                keyValue: new Guid("bd59fd6d-fc57-4002-91c9-1f13f87a2cd1"),
                column: "schema",
                value: System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Maximum_number_of_parameters_that_can_be_selected_by_Employee_in_Preference_screen\",\"KeyDisplay\":\"Maximum number of parameters for selection in Preference screen\",\"Description\":\"Maximum number of parameters for selection in Preference screen\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^[1-5]$\"}]", new System.Text.Json.JsonDocumentOptions()));

            migrationBuilder.UpdateData(
                table: "ConfigurationMaster",
                keyColumn: "Id",
                keyValue: new Guid("d4b5458e-87e7-4dda-a54d-be427dc4d8d0"),
                column: "schema",
                value: System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Employee_Response_Review_process_reviewed_by_Resource_Requestor\",\"KeyDisplay\":\"Review process of Employee Allocation response\",\"Description\":\"Review process of Employee Allocation response\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(-1|[1-4])$\"}]", new System.Text.Json.JsonDocumentOptions()));

            migrationBuilder.UpdateData(
                table: "ConfigurationMaster",
                keyColumn: "Id",
                keyValue: new Guid("e8534542-87df-48fe-8b75-ac0afc8244d9"),
                column: "schema",
                value: System.Text.Json.JsonDocument.Parse("[{\"Key\":\"NUMBER_OF_DAYS_FOR_SKILL_APPROVAL_DUE_DATE\",\"KeyDisplay\":\"Number Of Days For Skill Approval Duedate\",\"Description\":\"Number Of Days For Skill Approval Duedate\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^[1-5]$\"}]", new System.Text.Json.JsonDocumentOptions()));

            migrationBuilder.UpdateData(
                table: "ConfigurationMaster",
                keyColumn: "Id",
                keyValue: new Guid("f9d28250-c3fd-4309-84d4-6fb3204a7283"),
                column: "schema",
                value: System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Location\",\"KeyDisplay\":\"Location\",\"Description\":\"Location\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(10|[0-9]|[0-9]\\\\.[0-9]\\u002B)$\"},{\"Key\":\"Same_client\",\"KeyDisplay\":\"Experience working with the same client\",\"Description\":\"Experience working with the same client\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(10|[0-9]|[0-9]\\\\.[0-9]\\u002B)$\"},{\"Key\":\"competency\",\"KeyDisplay\":\"Competency\",\"Description\":\"Competency\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(10|[0-9]|[0-9]\\\\.[0-9]\\u002B)$\"},{\"Key\":\"offerings\",\"KeyDisplay\":\"Offerings\",\"Description\":\"Offerings\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^[0-9]$\"},{\"Key\":\"solutions\",\"KeyDisplay\":\"Solutions\",\"Description\":\"Solutions\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^[0-9]$\"},{\"Key\":\"Industry\",\"KeyDisplay\":\"Industry\",\"Description\":\"Industry\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(10|[0-9]|[0-9]\\\\.[0-9]\\u002B)$\"},{\"Key\":\"Sub_Industry\",\"KeyDisplay\":\"Sub Industry\",\"Description\":\"Sub Industry\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(10|[0-9]|[0-9]\\\\.[0-9]\\u002B)$\"},{\"Key\":\"Skills\",\"KeyDisplay\":\"Skills\",\"Description\":\"Skills\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^[0-9]$\"}]", new System.Text.Json.JsonDocumentOptions()));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ConfigurationMainBreakup",
                keyColumn: "Id",
                keyValue: new Guid("2137d048-0e08-46d9-b86b-05c15f11613f"),
                column: "MetaValue",
                value: System.Text.Json.JsonDocument.Parse("[{\"Key\":\"NUMBER_OF_DAYS_FOR_SKILL_APPROVAL_DUE_DATE\",\"DisplayKey\":\"Number Of Days For Skill Approval Duedate\",\"Value\":\"3\"}]", new System.Text.Json.JsonDocumentOptions()));

            migrationBuilder.UpdateData(
                table: "ConfigurationMainBreakup",
                keyColumn: "Id",
                keyValue: new Guid("303ac0a5-b366-4258-8bfb-ea342e38d2ba"),
                column: "MetaValue",
                value: System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Amber_condition_for_Budget_Consumption\",\"DisplayKey\":\"Amber condition for Budget Consumption\",\"Value\":\"80\"}]", new System.Text.Json.JsonDocumentOptions()));

            migrationBuilder.UpdateData(
                table: "ConfigurationMainBreakup",
                keyColumn: "Id",
                keyValue: new Guid("31db74b1-6a80-4164-ae02-d3863aab976f"),
                column: "MetaValue",
                value: System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Location\",\"DisplayKey\":\"Location\",\"Value\":\"8\"},{\"Key\":\"Same_client\",\"DisplayKey\":\"Experience working with the same client\",\"Value\":\"8\"},{\"Key\":\"competency\",\"DisplayKey\":\"Competency\",\"Value\":\"8\"},{\"Key\":\"offerings\",\"DisplayKey\":\"Offerings\",\"Value\":\"8\"},{\"Key\":\"solutions\",\"DisplayKey\":\"Solutions\",\"Value\":\"8\"},{\"Key\":\"Industry\",\"DisplayKey\":\"Industry\",\"Value\":\"8\"},{\"Key\":\"Sub_Industry\",\"DisplayKey\":\"Sub Industry\",\"Value\":\"8\"},{\"Key\":\"Skills\",\"DisplayKey\":\"Skills\",\"Value\":\"8\"}]", new System.Text.Json.JsonDocumentOptions()));

            migrationBuilder.UpdateData(
                table: "ConfigurationMainBreakup",
                keyColumn: "Id",
                keyValue: new Guid("38770e66-cc61-4ddb-8f39-a2c5bfbe2000"),
                column: "MetaValue",
                value: System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Number_of_days_for_employee_to_confirm_on_their_allocations\",\"DisplayKey\":\"Number of days for employee to confirm on their allocations\",\"Value\":\"1\"}]", new System.Text.Json.JsonDocumentOptions()));

            migrationBuilder.UpdateData(
                table: "ConfigurationMainBreakup",
                keyColumn: "Id",
                keyValue: new Guid("5bca1b3f-29a1-4b98-8f66-ebfa5c668df5"),
                column: "MetaValue",
                value: System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Employee_Response_Review_process_reviewed_by_Resource_Requestor\",\"DisplayKey\":\"Review process of Employee Allocation response\",\"Value\":\"1\"}]", new System.Text.Json.JsonDocumentOptions()));

            migrationBuilder.UpdateData(
                table: "ConfigurationMainBreakup",
                keyColumn: "Id",
                keyValue: new Guid("7713e0f5-7259-4610-bf94-97db59b49fa8"),
                column: "MetaValue",
                value: System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Permission_for_Additional_EL\",\"DisplayKey\":\"Permission for Additional EL\",\"Value\":\"-1\"}]", new System.Text.Json.JsonDocumentOptions()));

            migrationBuilder.UpdateData(
                table: "ConfigurationMainBreakup",
                keyColumn: "Id",
                keyValue: new Guid("850f9b34-307a-4c28-a737-882d505afd36"),
                column: "MetaValue",
                value: System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Alert_condition_for_Allocation_Cost\",\"DisplayKey\":\"Alert condition for Allocation Cost\",\"Value\":\"90\"}]", new System.Text.Json.JsonDocumentOptions()));

            migrationBuilder.UpdateData(
                table: "ConfigurationMainBreakup",
                keyColumn: "Id",
                keyValue: new Guid("868dbe58-2086-48b9-bb6b-ec70d01178a4"),
                column: "MetaValue",
                value: System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Resource_allocation_review\",\"DisplayKey\":\"Resource allocation review by Reviewer\",\"Value\":\"1\"}]", new System.Text.Json.JsonDocumentOptions()));

            migrationBuilder.UpdateData(
                table: "ConfigurationMainBreakup",
                keyColumn: "Id",
                keyValue: new Guid("b3d0f14d-fb5e-44e9-9abf-1617c2d6a565"),
                column: "MetaValue",
                value: System.Text.Json.JsonDocument.Parse("[{\"Key\":\"System_Suggestion_For_Requisition_Percentage_Match\",\"DisplayKey\":\"System Suggestion For Requisition Percentage Match\",\"Value\":\"60\"}]", new System.Text.Json.JsonDocumentOptions()));

            migrationBuilder.UpdateData(
                table: "ConfigurationMainBreakup",
                keyColumn: "Id",
                keyValue: new Guid("b5357e78-ca69-4dc2-a69a-cbaf2952cb47"),
                column: "MetaValue",
                value: System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Permission_for_Additional_Delegate\",\"DisplayKey\":\"Permission for Additional Delegate\",\"Value\":\"-1\"}]", new System.Text.Json.JsonDocumentOptions()));

            migrationBuilder.UpdateData(
                table: "ConfigurationMainBreakup",
                keyColumn: "Id",
                keyValue: new Guid("c2dc3ca6-80a2-4123-9910-e2cb1aeec180"),
                column: "MetaValue",
                value: System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Employee_Response_Review_process_reviewed_by_Supercoach\",\"DisplayKey\":\"Resource allocation review by Supercoach\",\"Value\":\"1\"}]", new System.Text.Json.JsonDocumentOptions()));

            migrationBuilder.UpdateData(
                table: "ConfigurationMainBreakup",
                keyColumn: "Id",
                keyValue: new Guid("d0989935-537b-4499-ab34-4ae4f02adfa5"),
                column: "MetaValue",
                value: System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Maximum_number_of_parameters_that_can_be_selected_by_Employee_in_Preference_screen\",\"DisplayKey\":\"Maximum number of parameters for selection in Preference screen\",\"Value\":\"5\"}]", new System.Text.Json.JsonDocumentOptions()));

            migrationBuilder.UpdateData(
                table: "ConfigurationMainBreakup",
                keyColumn: "Id",
                keyValue: new Guid("e3238e31-8ac2-4ead-aef4-5c39ff07d7b6"),
                column: "MetaValue",
                value: System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Alert_condition_for_Timesheet_Hours\",\"DisplayKey\":\"Alert condition for Timesheet Hours\",\"Value\":\"90\"}]", new System.Text.Json.JsonDocumentOptions()));

            migrationBuilder.UpdateData(
                table: "ConfigurationMaster",
                keyColumn: "Id",
                keyValue: new Guid("0672de0b-6dee-4d11-a608-df9cbc75fd3c"),
                column: "schema",
                value: System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Permission_for_Additional_Delegate\",\"KeyDisplay\":\"Permission for Additional Delegate\",\"Description\":\"Permission for Additional Delegate\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^-?1$\"}]", new System.Text.Json.JsonDocumentOptions()));

            migrationBuilder.UpdateData(
                table: "ConfigurationMaster",
                keyColumn: "Id",
                keyValue: new Guid("0748bb53-c8bb-4c03-8d9c-ba9318758aea"),
                column: "schema",
                value: System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Alert_condition_for_Allocation_Cost\",\"KeyDisplay\":\"Alert condition for Allocation Cost\",\"Description\":\"Alert condition for Allocation Cost\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^([1-9]|[1-9][0-9]|100)$\"}]", new System.Text.Json.JsonDocumentOptions()));

            migrationBuilder.UpdateData(
                table: "ConfigurationMaster",
                keyColumn: "Id",
                keyValue: new Guid("2f185e77-16d0-4151-ad38-0f7ddd93a22f"),
                column: "schema",
                value: System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Amber_condition_for_Budget_Consumption\",\"KeyDisplay\":\"Amber condition for Budget Consumption\",\"Description\":\"Amber condition for Budget Consumption\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^([1-9]|[1-9][0-9]|100)$\"}]", new System.Text.Json.JsonDocumentOptions()));

            migrationBuilder.UpdateData(
                table: "ConfigurationMaster",
                keyColumn: "Id",
                keyValue: new Guid("40b4d939-d7e4-4376-9c05-2023dacbec67"),
                column: "schema",
                value: System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Permission_for_Additional_EL\",\"KeyDisplay\":\"Permission for Additional EL\",\"Description\":\"Permission for Additional EL\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^-?1$\"}]", new System.Text.Json.JsonDocumentOptions()));

            migrationBuilder.UpdateData(
                table: "ConfigurationMaster",
                keyColumn: "Id",
                keyValue: new Guid("4c0b7855-ce34-4fe1-87bc-8b1b5fa3c30d"),
                column: "schema",
                value: System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Number_of_days_for_employee_to_confirm_on_their_allocations\",\"KeyDisplay\":\"Number of days for employee to confirm on their allocations\",\"Description\":\"Number of days for employee to confirm on their allocations\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(-1|[1-4])$\"}]", new System.Text.Json.JsonDocumentOptions()));

            migrationBuilder.UpdateData(
                table: "ConfigurationMaster",
                keyColumn: "Id",
                keyValue: new Guid("69ed0225-64db-445a-bfef-bd739835eb87"),
                column: "schema",
                value: System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Employee_Response_Review_process_reviewed_by_Supercoach\",\"KeyDisplay\":\"Resource allocation review by Supercoach\",\"Description\":\"Resource allocation review by Supercoach\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(-1|[1-4])$\"}]", new System.Text.Json.JsonDocumentOptions()));

            migrationBuilder.UpdateData(
                table: "ConfigurationMaster",
                keyColumn: "Id",
                keyValue: new Guid("6e2f6490-d03a-4ade-854e-fb7e1ad43cc5"),
                column: "schema",
                value: System.Text.Json.JsonDocument.Parse("[{\"Key\":\"System_Suggestion_For_Requisition_Percentage_Match\",\"KeyDisplay\":\"System Suggestion For Requisition Percentage Match\",\"Description\":\"System Suggestion For Requisition Percentage Match\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^([1-9]|[1-9][0-9]|100)$\"}]", new System.Text.Json.JsonDocumentOptions()));

            migrationBuilder.UpdateData(
                table: "ConfigurationMaster",
                keyColumn: "Id",
                keyValue: new Guid("9999ce5f-c6aa-404e-aeda-1334a69641d9"),
                column: "schema",
                value: System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Alert_condition_for_Timesheet_Hours\",\"KeyDisplay\":\"Alert condition for Timesheet Hours\",\"Description\":\"Alert condition for Timesheet Hours\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^([1-9]|[1-9][0-9]|100)$\"}]", new System.Text.Json.JsonDocumentOptions()));

            migrationBuilder.UpdateData(
                table: "ConfigurationMaster",
                keyColumn: "Id",
                keyValue: new Guid("bbf7f6e5-bd62-4e2c-9e82-c5e2313c23ba"),
                column: "schema",
                value: System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Resource_allocation_review\",\"KeyDisplay\":\"Resource allocation review by Reviewer\",\"Description\":\"Resource allocation review by Reviewer\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(-1|[1-4])$\"}]", new System.Text.Json.JsonDocumentOptions()));

            migrationBuilder.UpdateData(
                table: "ConfigurationMaster",
                keyColumn: "Id",
                keyValue: new Guid("bd59fd6d-fc57-4002-91c9-1f13f87a2cd1"),
                column: "schema",
                value: System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Maximum_number_of_parameters_that_can_be_selected_by_Employee_in_Preference_screen\",\"KeyDisplay\":\"Maximum number of parameters for selection in Preference screen\",\"Description\":\"Maximum number of parameters for selection in Preference screen\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^[1-5]$\"}]", new System.Text.Json.JsonDocumentOptions()));

            migrationBuilder.UpdateData(
                table: "ConfigurationMaster",
                keyColumn: "Id",
                keyValue: new Guid("d4b5458e-87e7-4dda-a54d-be427dc4d8d0"),
                column: "schema",
                value: System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Employee_Response_Review_process_reviewed_by_Resource_Requestor\",\"KeyDisplay\":\"Review process of Employee Allocation response\",\"Description\":\"Review process of Employee Allocation response\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(-1|[1-4])$\"}]", new System.Text.Json.JsonDocumentOptions()));

            migrationBuilder.UpdateData(
                table: "ConfigurationMaster",
                keyColumn: "Id",
                keyValue: new Guid("e8534542-87df-48fe-8b75-ac0afc8244d9"),
                column: "schema",
                value: System.Text.Json.JsonDocument.Parse("[{\"Key\":\"NUMBER_OF_DAYS_FOR_SKILL_APPROVAL_DUE_DATE\",\"KeyDisplay\":\"Number Of Days For Skill Approval Duedate\",\"Description\":\"Number Of Days For Skill Approval Duedate\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^[1-5]$\"}]", new System.Text.Json.JsonDocumentOptions()));

            migrationBuilder.UpdateData(
                table: "ConfigurationMaster",
                keyColumn: "Id",
                keyValue: new Guid("f9d28250-c3fd-4309-84d4-6fb3204a7283"),
                column: "schema",
                value: System.Text.Json.JsonDocument.Parse("[{\"Key\":\"Location\",\"KeyDisplay\":\"Location\",\"Description\":\"Location\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(10|[0-9]|[0-9]\\\\.[0-9]\\u002B)$\"},{\"Key\":\"Same_client\",\"KeyDisplay\":\"Experience working with the same client\",\"Description\":\"Experience working with the same client\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(10|[0-9]|[0-9]\\\\.[0-9]\\u002B)$\"},{\"Key\":\"competency\",\"KeyDisplay\":\"Competency\",\"Description\":\"Competency\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(10|[0-9]|[0-9]\\\\.[0-9]\\u002B)$\"},{\"Key\":\"offerings\",\"KeyDisplay\":\"Offerings\",\"Description\":\"Offerings\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^[0-9]$\"},{\"Key\":\"solutions\",\"KeyDisplay\":\"Solutions\",\"Description\":\"Solutions\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^[0-9]$\"},{\"Key\":\"Industry\",\"KeyDisplay\":\"Industry\",\"Description\":\"Industry\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(10|[0-9]|[0-9]\\\\.[0-9]\\u002B)$\"},{\"Key\":\"Sub_Industry\",\"KeyDisplay\":\"Sub Industry\",\"Description\":\"Sub Industry\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^(10|[0-9]|[0-9]\\\\.[0-9]\\u002B)$\"},{\"Key\":\"Skills\",\"KeyDisplay\":\"Skills\",\"Description\":\"Skills\",\"ControlType\":\"Integer\",\"ValidationRegEx\":\"^[0-9]$\"}]", new System.Text.Json.JsonDocumentOptions()));

            migrationBuilder.InsertData(
                table: "RoleMenu",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "IsActive", "MenuId", "ModifiedAt", "ModifiedBy", "Role" },
                values: new object[,]
                {
                    { 232L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 23L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "SuperCoach" },
                    { 236L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", true, 23L, new DateTime(2023, 12, 31, 18, 30, 0, 0, DateTimeKind.Utc), "System", "Leaders" }
                });
        }
    }
}
