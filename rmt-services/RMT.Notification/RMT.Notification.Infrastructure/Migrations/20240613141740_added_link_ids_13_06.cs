using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RMT.Notification.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class added_link_ids_13_06 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 137L);

            migrationBuilder.DeleteData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 138L);

            migrationBuilder.DeleteData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 266L);

            migrationBuilder.DeleteData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 294L);

            migrationBuilder.UpdateData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 16L,
                column: "Link",
                value: "/project-details/pipelinecode/jobcode?tab3");

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 42L,
                column: "link_id",
                value: 12L);

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 140L,
                column: "link_id",
                value: 16L);

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 142L,
                column: "link_id",
                value: 17L);

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 144L,
                column: "link_id",
                value: 17L);

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 146L,
                column: "link_id",
                value: 17L);

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 148L,
                column: "link_id",
                value: 12L);

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 150L,
                column: "link_id",
                value: 16L);

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 152L,
                column: "link_id",
                value: 16L);

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 156L,
                column: "link_id",
                value: 16L);

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 268L,
                column: "link_id",
                value: 16L);

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 269L,
                column: "link_id",
                value: 18L);

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 276L,
                columns: new[] { "link_id", "template" },
                values: new object[] { 16L, " Allocation rejection by <<empEmail:ResourceAllocationDetails:item_id>> for Project: <<ProjectName:ResourceAllocationDetails:item_id>>  have been approved by reviewer <<updated_by>>. Please do new allocations" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 284L,
                column: "link_id",
                value: 16L);

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 287L,
                column: "link_id",
                value: 16L);

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 296L,
                column: "link_id",
                value: 19L);

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 298L,
                column: "link_id",
                value: 13L);

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 300L,
                column: "link_id",
                value: 11L);

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 304L,
                column: "link_id",
                value: 13L);

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 308L,
                column: "link_id",
                value: 15L);

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 309L,
                column: "link_id",
                value: 16L);

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 312L,
                column: "link_id",
                value: 1L);

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 314L,
                column: "link_id",
                value: 16L);

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 315L,
                column: "link_id",
                value: 16L);

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 318L,
                column: "link_id",
                value: 13L);

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 320L,
                column: "link_id",
                value: 13L);

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 322L,
                column: "link_id",
                value: 13L);

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 323L,
                column: "link_id",
                value: 1L);

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 400L,
                column: "link_id",
                value: 13L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 16L,
                column: "Link",
                value: "/project-details/<<pipelinecode>>/<<jobcode>>?tab3");

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 42L,
                column: "link_id",
                value: null);

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 140L,
                column: "link_id",
                value: null);

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 142L,
                column: "link_id",
                value: null);

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 144L,
                column: "link_id",
                value: null);

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 146L,
                column: "link_id",
                value: null);

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 148L,
                column: "link_id",
                value: null);

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 150L,
                column: "link_id",
                value: null);

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 152L,
                column: "link_id",
                value: null);

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 156L,
                column: "link_id",
                value: null);

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 268L,
                column: "link_id",
                value: null);

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 269L,
                column: "link_id",
                value: null);

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 276L,
                columns: new[] { "link_id", "template" },
                values: new object[] { null, "Dear User,<br/><br/>\r\n            The Allocation request rejected by employee <<empEmail:ResourceAllocationDetails:item_id>> for Project: <<ProjectName:ResourceAllocationDetails:item_id>> has been cancelled by Reviewer <<updated_by>>.<br/> \r\n            Please do new allocations.<br/><br/>\r\n            Please <a href='<<BaseUrl:config>>project-details/<<pipelinecode>>/<<jobcode>>?tab=3'>click here</a> for the details.<br/><br/>\r\n            Regards,<br/>\r\n            RMS Team<br/><br/>\r\n            ** This is an autogenerated email. Kindly do not respond to this.**" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 284L,
                column: "link_id",
                value: null);

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 287L,
                column: "link_id",
                value: null);

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 296L,
                column: "link_id",
                value: null);

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 298L,
                column: "link_id",
                value: null);

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 300L,
                column: "link_id",
                value: null);

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 304L,
                column: "link_id",
                value: null);

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 308L,
                column: "link_id",
                value: null);

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 309L,
                column: "link_id",
                value: null);

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 312L,
                column: "link_id",
                value: null);

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 314L,
                column: "link_id",
                value: null);

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 315L,
                column: "link_id",
                value: null);

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 318L,
                column: "link_id",
                value: null);

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 320L,
                column: "link_id",
                value: null);

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 322L,
                column: "link_id",
                value: null);

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 323L,
                column: "link_id",
                value: null);

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 400L,
                column: "link_id",
                value: null);

            migrationBuilder.InsertData(
                table: "NotificationTemplates",
                columns: new[] { "Id", "cc", "is_active", "link_id", "module", "notification_type", "sub_module", "subject", "subscription_role", "template", "to", "type" },
                values: new object[,]
                {
                    { 137L, null, true, null, "Notification", "EMAIL", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", null, "Dear User,<br/><br/>\r\n            The Allocation request rejected by employee <<empEmail:ResourceAllocationDetails:item_id>> for Project: <<ProjectName:ResourceAllocationDetails:item_id>>  has been approved by Reviewer <<updated_by>>.<br/>\r\n            Please do new allocations.<br/><br/>\r\n            Please <a href='<<BaseUrl:config>>project-details/<<pipelinecode>>/<<jobcode>>?tab=4'>click here</a> for the details.<br/><br/>\r\n\r\n            Regards,<br/>\r\n            RMS Team<br/><br/>\r\n\r\n            **This is an autogenerated email.Kindly do not respond to this.**", "resource_requestor_allocation_wf", "REVIEWER_ACCEPTED_EMPLOYEE_REJECTION_RESPONSE" },
                    { 138L, null, true, null, "Notification", "PUSH", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", null, "Dear User,<br/><br/> Allocation requests rejected by employee <<ProjectName:ResourceAllocationDetails:item_id>> for Project: <<ProjectName:ResourceAllocationDetails:item_id>> has been cancelled by Reviewer <<updated_by>>.<br/> Please do new allocations. <br/><br/>Please <a href='<<BaseUrl:config>>project-details/<<pipelinecode>>/<<jobcode>>?tab=4'>click here</a> for the details. <br/><br/>Regards,<br/> RMS Team <br/><br/>** This is an autogenerated email. Kindly do not respond to this.**", "resource_requestor_allocation_wf", "REVIEWER_ACCEPTED_EMPLOYEE_REJECTION_RESPONSE" },
                    { 266L, null, true, null, "Notification", "EMAIL", "Notification", "Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>> Roll Forward Update", null, "Dear User,<br/><br/>\r\n            RMS Team<br/><br/>\r\n            Project <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>> dates have been rolled forward from <<pipelineoldstartdate>> to <<pipelinestartdate>>.<br/><br/> \r\n            The system has taken the following actions based on the new start date which are set out below:<br/><br/> \r\n            1. For allocated employees who are available on the revised dates,  new allocation workflow and relevant approvals will be  triggered.<br/>\r\n            2. For allocated employees who are not available on certain dates in the new project period, those allocations will be moved to DRAFT state to review and change as appropriate.<br/>\r\n            3. For allocated employees who are not available for the requested period,  they will be released from the project.Request you to please make appropriate allocations to meet your resourcing needs. <br/><br/>\r\n            Please <a href='<<BaseUrl:config>>project-details/<<pipelinecode>>/<<jobcode>>?tab=3'>click here</a> for the details.<br/><br/>\r\n            Regards,<br/>\r\n            RMS Team <br/><br/>\r\n            **This is an autogenerated email.Kindly do not respond to this.**", "ResourceRequestor,Delegate,AdditionalEl,AdditionalDelegate", "ROLL_OVER_ALLOCATED_EMPLOYEES_AVAILABLE_FOR_REVISED_DATES_ALLOCATION_SHIFTED_NEW_WF_TRIGGERED_NOTIFICATION_TO_REQUESTOR_AND_REVIEWER" },
                    { 294L, null, true, null, "Notification", "PUSH", "Notification", "Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>> Roll Forward Update", null, "Project <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>> has been rolled forward to <<pipelinestartdate>>.  Review Project details for change in allocations. ", "ResourceRequestor,Delegate,AdditionalEl,AdditionalDelegate", "ROLL_OVER_ALLOCATED_EMPLOYEES_AVAILABLE_FOR_REVISED_DATES_ALLOCATION_SHIFTED_NEW_WF_TRIGGERED_NOTIFICATION_TO_REQUESTOR_AND_REVIEWER" }
                });
        }
    }
}
