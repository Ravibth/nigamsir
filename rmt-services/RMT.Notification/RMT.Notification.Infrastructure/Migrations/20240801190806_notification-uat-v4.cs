using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Notification.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class notificationuatv4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Link", "Module", "SubModule" },
                values: new object[] { "/myskill", "Notification", "Skill" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Link", "Module", "SubModule" },
                values: new object[] { "/skill-review", "Notification", "Skill" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "Link", "Module", "SubModule" },
                values: new object[] { "/searchskill", "Notification", "Skill" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "Link", "Module", "SubModule" },
                values: new object[] { "/skillmaster", "Notification", "Skill" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "Link", "Module", "SubModule" },
                values: new object[] { "/projects", "Notification", "Skill" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "Link", "Module", "SubModule" },
                values: new object[] { "/projects", "Notification", "Project" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "Link", "Module", "SubModule" },
                values: new object[] { "/marketplace", "Notification", "Project" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "Link", "Module", "SubModule" },
                values: new object[] { "/my-preference", "Notification", "Manage" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "Link", "Module", "SubModule" },
                values: new object[] { "/my-preference", "Notification", "Manage" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "Link", "Module", "SubModule" },
                values: new object[] { "/my-calender", "Notification", "Manage" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 11L,
                columns: new[] { "Link", "Module", "SubModule" },
                values: new object[] { "/roles-permission", "Notification", "User" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 12L,
                columns: new[] { "Link", "Module", "SubModule" },
                values: new object[] { "/myapproval", "Notification", "User" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 13L,
                columns: new[] { "Link", "Module", "SubModule" },
                values: new object[] { "/project-details/<<pipelinecode##>>/<<jobcode##>>?tab=0", "Notification", "Details" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 14L,
                columns: new[] { "Link", "Module", "SubModule" },
                values: new object[] { "/project-details/<<pipelinecode##>>/<<jobcode##>>?tab=1", "Notification", "Details" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 15L,
                columns: new[] { "Link", "Module", "SubModule" },
                values: new object[] { "/project-details/<<pipelinecode##>>/<<jobcode##>>?tab=2", "Notification", "Details" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 16L,
                columns: new[] { "Link", "Module", "SubModule" },
                values: new object[] { "/project-details/<<pipelinecode##>>/<<jobcode##>>?tab=3", "Notification", "Details" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 17L,
                columns: new[] { "Link", "Module", "SubModule" },
                values: new object[] { "/project-details/<<pipelinecode##>>/<<jobcode##>>?tab=4", "Notification", "Details" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 18L,
                columns: new[] { "Link", "Module", "SubModule" },
                values: new object[] { "/project-details/<<pipelinecode##>>/<<jobcode##>>?tab=5", "Notification", "Details" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 19L,
                columns: new[] { "Link", "Module", "SubModule" },
                values: new object[] { "/project-details/<<newpipelinecode##>>/<<newjobcode##>>?tab=0", "Notification", "Details" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 42L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "You have been assigned to the Project <<ProjectName:ResourceAllocationDetails:item_id>> on <<created_at>>.", "employee_name_allocation_wf", "NOTIFICATION_TO_EMPLOYEE_FOR_HIS_ALLOCATION_TASK_TO_PROJECT" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 43L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "Dear User,<br/><br/> \r\nYou have been assigned to the Project: <<ProjectName:ResourceAllocationDetails:item_id>> on <<created_at>>.<br/> \r\nThe Project Allocation Period is:<br/> \r\nStart Date: <<startdate:ResourceAllocationDetails:item_id>><br/> End Date: <<enddate:ResourceAllocationDetails:item_id>><br/> Hours: <<totaleffort:ResourceAllocationDetails:item_id>>.<br/><br/> \r\nPlease <a href='<<BaseUrl:config>>myapproval'>click here</a> to view further details of the allocation.<br/><br/> \r\nPlease note, if you are unable to review the request for approval, it will be auto - approved by <<due_date>>.<br/><br/> \r\nRegards,<br/>RMS Team<br/><br/>**This is an autogenerated email.Kindly do not respond to this.**", "employee_name_allocation_wf", "NOTIFICATION_TO_EMPLOYEE_FOR_HIS_ALLOCATION_TASK_TO_PROJECT" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 139L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "Dear User,<br/><br/> \r\nYour assignment on the Project: <<ProjectName:ResourceAllocationDetails:item_id>> has been updated on <<modifiedat>>.<br/><br/> \r\nYou revised allocation dates are:<br/> \r\nStart Date: <<startdate:ResourceAllocationDetails:item_id>><br/> End Date: <<enddate:ResourceAllocationDetails:item_id>><br/> Hours: <<totaleffort:ResourceAllocationDetails:item_id>>.<br/><br/> \r\nPlease <a href='<<BaseUrl:config>>project-details/<<pipelinecode##>>/<<jobcode##>>?tab=3'>click here</a> or the details.<br/><br/> \r\nRegards,<br/> \r\nRMS Team<br/><br/>**This is an autogenerated email.Kindly do not respond to this.**", "employee_name_allocation_wf", "NOTIFICATION_FOR_ALLOCATION_UPDATE_TO_EMPLOYEE" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 140L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "Your assignment on the project <<ProjectName:ResourceAllocationDetails:item_id>> has been updated on <<modifiedat>>. ", "employee_name_allocation_wf", "NOTIFICATION_FOR_ALLOCATION_UPDATE_TO_EMPLOYEE" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 141L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "Dear User,<br/><br/> \r\nResource allocation request has been rejected by reviewer: <<updated_by:GetUserInfo:updated_by>> for employee: <<empEmail:ResourceAllocationDetails:item_id>> \r\nin the project: <<ProjectName:ResourceAllocationDetails:item_id>>.<br/> \r\nRejection reason: <<ReasonForRejectionProvidedByReviewer>><br/><br/> \r\nRequest you to please allocate another resource.<br/><br/> \r\nPlease <a href='<<BaseUrl:config>>project-details/<<pipelinecode##>>/<<jobcode##>>?tab=4'>click here</a> for the details.<br/><br/> \r\nRegards,<br/>RMS Team<br/><br/>**This is an autogenerated email.Kindly do not respond to this.**", "resource_requestor_allocation_wf,Reviewer", "EMPLOYEE_ALLOCATION_REJECTED_BY_REVIEWER_NOTIFICATION_TO_RESOURCE_REQUESTOR" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 142L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "Resource allocation requests have been rejected for Project: <<ProjectName:ResourceAllocationDetails:item_id>> by reviewer <<updated_by>>. Please do new allocations", "resource_requestor_allocation_wf,Reviewer", "EMPLOYEE_ALLOCATION_REJECTED_BY_REVIEWER_NOTIFICATION_TO_RESOURCE_REQUESTOR" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 143L,
                columns: new[] { "cc", "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "", "Notification", "EMAIL", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "Dear User,<br/><br/> \r\nResource allocation request has been rejected by Employee: <<empEmail:ResourceAllocationDetails:item_id>> for Project: <<ProjectName:ResourceAllocationDetails:item_id>>.<br/> \r\nYou can terminate the workflow by <<due_date>> to make new allocations provided the allocated employee does not withdraw their rejection.<br/> \r\nRejection reason: <<ReasonForRejectionProvidedByEmployee>><br/><br/> \r\nPlease <a href='<<BaseUrl:config>>project-details/<<pipelinecode##>>/<<jobcode##>>?tab=4'>click here</a> for the details.<br/><br/> \r\nRegards,<br/>RMS Team<br/><br/>**This is an autogenerated email.Kindly do not respond to this.**", "resource_requestor_allocation_wf,Reviewer", "NOTIFICATION_TO_RESOURCE_REQUESTOR_AND_REVIEWER_ON_EMPLOYEE_REJECTION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 144L,
                columns: new[] { "cc", "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "", "Notification", "PUSH", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "Resource allocation requests have been rejected for Project: <<ProjectName:ResourceAllocationDetails:item_id>> by employee <<empEmail:ResourceAllocationDetails:item_id>>. Please make new allocations.", "resource_requestor_allocation_wf,Reviewer", "NOTIFICATION_TO_RESOURCE_REQUESTOR_AND_REVIEWER_ON_EMPLOYEE_REJECTION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 145L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "Dear User,<br/><br/> \r\nResource allocation request has been rejected by employee <<empEmail:ResourceAllocationDetails:item_id>> for Project: <<ProjectName:ResourceAllocationDetails:item_id>><br/> \r\nRejection reason: <<ReasonForRejectionProvidedByEmployee>><br/><br/> \r\nPlease <a href='<<BaseUrl:config>>project-details/<<pipelinecode##>>/<<jobcode##>>?tab=4'>click here</a> to view details and review employee action.<br/><br/> \r\nRegards,<br/>RMS Team<br/><br/>**This is an autogenerated email.Kindly do not respond to this.**", "resource_requestor_allocation_wf,Reviewer", "NOTIFICATION_TO_RESOURCE_REQUESTOR_AND_REVIEWER_ON_EMPLOYEE_REJECTION_REQUESTOR_CONFIG_IS_ON" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 146L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "Resource allocation requests have been rejected for Project: <<ProjectName:ResourceAllocationDetails:item_id>> by employee <<empEmail:ResourceAllocationDetails:item_id>>. Please review employee action", "resource_requestor_allocation_wf,Reviewer", "NOTIFICATION_TO_RESOURCE_REQUESTOR_AND_REVIEWER_ON_EMPLOYEE_REJECTION_REQUESTOR_CONFIG_IS_ON" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 147L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "Dear User,<br/><br/> \r\nYour request for non-acceptance of allocation has been rejected by the Reviewer <<updated_by>>. You have now been allocated to the Project: <<ProjectName:ResourceAllocationDetails:item_id>><br/><br/>The Project Allocation Period is:<br/> \r\nStart Date: <<startdate:ResourceAllocationDetails:item_id>><br/> End Date: <<enddate:ResourceAllocationDetails:item_id>><br/> Hours: <<totaleffort:ResourceAllocationDetails:item_id>><br/><br/> \r\nFor any queries please reach out to the above mentioned Reviewer.<br/><br/> \r\nPlease <a href='<<BaseUrl:config>>project-details/<<pipelinecode##>>/<<jobcode##>>?tab=3'>click here</a> review project and allocation the details..<br/><br/> \r\nRegards,<br/>RMS Team<br/><br/>**This is an autogenerated email.Kindly do not respond to this.**", "employee_name_allocation_wf", "NOTIFICATION_TO_EMPLOYEE_AFTER_REVIEWER_ACCEPTS" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 148L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "You have been allocated to the project <<ProjectName:ResourceAllocationDetails:item_id>> on <<startdate:ResourceAllocationDetails:item_id>> as your rejection has not been accepted.", "employee_name_allocation_wf", "NOTIFICATION_TO_EMPLOYEE_AFTER_REVIEWER_ACCEPTS" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 149L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "Dear User,<br/><br/> \r\nYou have been auto-allocated to the Project: <<ProjectName:ResourceAllocationDetails:item_id>>.<br/> \r\nStart Date: <<startdate:ResourceAllocationDetails:item_id>><br/> End Date: <<enddate:ResourceAllocationDetails:item_id>><br/> Hours: <<totaleffort:ResourceAllocationDetails:item_id>>.<br/><br/> \r\nPlease <a href='<<BaseUrl:config>>project-details/<<pipelinecode##>>/<<jobcode##>>?tab=3'>click here</a> to view allocation details.<br/><br/> \r\nRegards,<br/>RMS Team<br/><br/>**This is an autogenerated email.Kindly do not respond to this.**", "employee_name_allocation_wf", "NOTIFICATION_TO_EMPLOYEE_AUTO_APPROVED_AFTER_EMPLOYEE_DUE_DATE_CROSS" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 150L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "Your allocation has been auto-approved for the Project <<ProjectName:ResourceAllocationDetails:item_id>> on <<startdate:ResourceAllocationDetails:item_id>>", "employee_name_allocation_wf", "NOTIFICATION_TO_EMPLOYEE_AUTO_APPROVED_AFTER_EMPLOYEE_DUE_DATE_CROSS" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 151L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "Dear User,<br/><br/>As you have accepted the allocation, you have now been allocated to the Project: <<ProjectName:ResourceAllocationDetails:item_id>> on <<startdate:ResourceAllocationDetails:item_id>>. \r\n<br/><br/> \r\nPlease <a href='<<BaseUrl:config>>project-details/<<pipelinecode##>>/<<jobcode##>>?tab=3'>click here</a> to view further details.<br/><br/> \r\n<br/><br/> \r\nRegards,<br/>RMS Team<br/><br/>**This is an autogenerated email.Kindly do not respond to this.**", "employee_name_allocation_wf", "NOTIFICATION_TO_EMPLOYEE_WITHDRAWS_REJECTION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 152L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "You have successfully been allocated to the project <<ProjectName:ResourceAllocationDetails:item_id>> on <<startdate:ResourceAllocationDetails:item_id>>", "employee_name_allocation_wf", "NOTIFICATION_TO_EMPLOYEE_WITHDRAWS_REJECTION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 153L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "Dear User,<br/><br/> \r\nYour allocation to the Project: <<ProjectName:ResourceAllocationDetails:item_id>> has been cancelled, as you have not agreed to accept the allocation.<br/><br/> \r\nYour  request for not being allocated was approved by  <<updated_by>>.<br/><br/> \r\nRegards,<br/>RMS Team<br/><br/>** This is an autogenerated email. Kindly do not respond to this.**", "employee_name_allocation_wf", "NOTIFICATION_TO_EMPLOYEE_REQUESTOR_APPROVED_REJECTION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 154L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", " \r\nYour allocation to the project <<ProjectName:ResourceAllocationDetails:item_id>> has been cancelled. Allocation Rejection request has been approved by Resoruce Requestor <<updated_by>>.", "employee_name_allocation_wf", "NOTIFICATION_TO_EMPLOYEE_REQUESTOR_APPROVED_REJECTION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 155L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> Roll Forward Update", "Dear User,<br/><br/> \r\nProject <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> dates have been rolled forward from <<pipelineoldstartdate>> to <<pipelinestartdate>>. The system has taken the following actions based on the new start date which are set out below:<br/><br/> \r\n1. For allocated employees who are available on the revised dates, new allocation workflow and relevant approvals will be triggered.<br/> \r\n2. For allocated employees who are not available on certain dates in the new project period, those allocations will be moved to DRAFT state to review and change as appropriate.<br/> \r\n3. For allocated employees who are not available for the requested period, they will be released from the project.<br/><br/> \r\nPlease <a href='<<BaseUrl:config>>project-details/<<pipelinecode##>>/<<jobcode##>>?tab=3'>click here</a> for the details.<br/><br/> \r\nRegards,<br/>RMS Team<br/><br/>**This is an autogenerated email.Kindly do not respond to this.**", "Requestor,Delegate,Reviewer,AdditionalEl,AdditionalDelegate", "ROLL_OVER_ALLOCATED_EMPLOYEES_AVAILABLE_FOR_REVISED_DATES_ALLOCATION_SHIFTED_NEW_WF_TRIGGERED_NOTIFICATION_TO_REQUESTOR_AND_REVIEWER" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 156L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> Roll Forward Update", "Project <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> has been rolled forward to <<pipelinestartdate>>. Review Project details for change in allocations.", "Requestor,Delegate,Reviewer,AdditionalEl,AdditionalDelegate", "ROLL_OVER_ALLOCATED_EMPLOYEES_AVAILABLE_FOR_REVISED_DATES_ALLOCATION_SHIFTED_NEW_WF_TRIGGERED_NOTIFICATION_TO_REQUESTOR_AND_REVIEWER" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 169L,
                columns: new[] { "cc", "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "", "Notification", "EMAIL", "Notification", "Project Marketplace Updates", "Dear User,<br/><br/> \r\nBelow listed projects have been moved in the Marketplace.<br/><br/> \r\n<projectmovedtomarketplacetable><br/><br/> \r\nRegards,<br/>RMS Team<br/><br/>** This is an autogenerated email. Kindly do not respond to this.**", "Reviewer,ResourceRequestor,Delegate", "PROJECT_MOVED_TO_MARKETPLACE_SUMMARY_EMAIL" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 170L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "New Projects in the Marketplace", "Dear User,<br/><br/> \r\nNew projects have been added to the Marketplace in the RMS. These projects offer exciting opportunities for you to contribute your skills and expertise. A summary of the new projects provided in the table below:<br/><br/> \r\n<<projectlistinginmarketplacenotificationtoemployee>> \r\n<br/><br/> \r\nPlease <a href='<<BaseUrl:config>>marketplace'>click here</a> to view details in the Marketplace and submit your interest.<br/><br/> \r\nRegards,<br/>RMS Team<br/><br/>** This is an autogenerated email. Kindly do not respond to this.**", "employee", "CONSOLIDATED_MAIL_TO_EMPLOYEE_FOR_PROJECT_LISTING_IN_MARKETPLACE" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 171L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "RMS Tasks pending for your action", "Dear User,<br/><br/> \r\nFollowing tasks are pending for your action:<br/><br/> \r\n<<allocationtablecontent>><br/><br/> \r\n<<skilltablecontent>><br/><br/> \r\nPlease <a href='<<BaseUrl:config>>myapproval'>click here</a> for to review further details and approve/reject the request.<br/><br/> \r\nRegards,<br/>RMS Team<br/><br/>** This is an autogenerated email. Kindly do not respond to this.**", "employee", "RMS_ALLOCATION_AND_SKILL_TASKS_PENDING_FOR_ACTION_SUMMARY_MAIL" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 172L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "RMS Tasks pending for your action", "Dear User,<br/><br/> \r\nFollowing tasks are pending for your action: \r\n<br/><br/> \r\n<<allocationtablecontent>> \r\n<br/><br/> \r\n<<skilltablecontent>> \r\n<br/><br/> \r\nPlease <a href='<<BaseUrl:config>>myapproval'>click here</a> for to review further details and approve/reject the request. \r\n<br/><br/> \r\nRegards,<br/>RMS Team<br/><br/>** This is an autogenerated email. Kindly do not respond to this.**", "employee", "ADDITION_OF_NEW_PROJECT_NOTIFICATION_TO_RESOURCE_REQUESTOR" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 263L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>> Allocation Update", "Allocation updates for Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>>", "employee_name_release_resource,ResourceRequestor,Reviewer,AdditionalEl,Delegate,AdditionalDelegate", "NOTIFICATION_TO_EMPLOYEE_AS_RELEASED_FROM_PROJECT" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 264L,
                columns: new[] { "cc", "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "ResourceRequestor,Reviewer,AdditionalEl,Delegate,AdditionalDelegate", "Notification", "EMAIL", "Notification", "Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>> Allocation Update", "Dear User,<br/><br/> \r\nYour Allocation on the Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>> has been released.<br/> \r\nPlease reach out to <<modifiedBy>> for any queries.<br/><br/> \r\nRegards,<br/>RMS Team<br/><br/>** This is an autogenerated email. Kindly do not respond to this.**", "employee_name_release_resource", "NOTIFICATION_TO_EMPLOYEE_AS_RELEASED_FROM_PROJECT" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 267L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>> Roll Forward Update.", "Dear User,<br/><br/> \r\nAllocation of Employees has been updated on the  project : <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>> as the Project dates have been rolled forward from <<pipelineoldstartdate>> to <<pipelinestartdate>>.<br/><br/> \r\nPlease <a href='<<BaseUrl:config>>project-details/<<pipelinecode##>>/<<jobcode##>>?tab=3'>click here</a> for the details.<br/><br/> \r\nRegards,<br/>RMS Team<br/><br/>**This is an autogenerated email.Kindly do not respond to this.**", "Reviewer", "NOTIFICATION_TO_REVIEWER_ALLOCATIONS_ARE_SHIFTED_AND_NEW_WORKFLOW_ARE_TRIGGERED" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 268L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>> Roll Forward Update.", "Allocation Update - Project <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>> rolled forward to <<pipelinestartdate>>", "Reviewer", "NOTIFICATION_TO_REVIEWER_ALLOCATIONS_ARE_SHIFTED_AND_NEW_WORKFLOW_ARE_TRIGGERED" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 269L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>> Name Marketplace Summary", "Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>> has received <<employeeshowedinterestcount>> interests. ", "ResourceRequestor,Delegate", "NOTIFICATION_TO_RESOURCE_REQUESTOR_AND_DELEGATE_FOR_REMOVAL_OF_PROJECT_FROM_MP" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 270L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>> Name Marketplace Summary", "Dear User,<br/><br/> \r\nProject: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>> has been removed from Marketplace & has received <<employeeshowedinterestcount>> interests.<br/><br/> \r\nPlease <a href='<<BaseUrl:config>>project-details/<<pipelinecode##>>/<<jobcode##>>?tab=5'>click here</a> for the details.<br/><br/> \r\nRegards,<br/>RMS Team<br/><br/>**This is an autogenerated email.Kindly do not respond to this.**", "ResourceRequestor,Delegate", "NOTIFICATION_TO_RESOURCE_REQUESTOR_AND_DELEGATE_FOR_REMOVAL_OF_PROJECT_FROM_MP" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 271L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> Roll Forward Update - Allocation Release", "Dear User,<br/><br/> \r\nProject <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> dates have been rolled forward from <<pipelineoldstartdate>> to <<pipelinestartdate>>.  Since you are not available on the revised project dates, your allocation on the project has been released.<br/><br/> \r\nPlease speak to <<ProjectResourceRequestor:GetProjectResourceRequestor:pipelineCode|jobCode>> for concerns on the allocations.<br/><br/> \r\nRegards,<br/>RMS Team<br/><br/>** This is an autogenerated email. Kindly do not respond to this.**", "employee_name_roll_over_draft", "ROLL_OVER_ALLOCATION_IN_DRAFT_DUE_TO_UNAVAILABLITY" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 272L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> Roll Forward Update - Allocation Release", "Project <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> has been rolled forward to <<pipelinestartdate>>. Your allocation has been released. ", "employee_name_roll_over_draft", "ROLL_OVER_ALLOCATION_IN_DRAFT_DUE_TO_UNAVAILABLITY" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 273L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> Roll Forward Update - Allocation Release", "Dear User,<br/><br/> \r\nProject <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> dates have been rolled forward from <<pipelineoldstartdate>> to <<pipelinestartdate>>.  Since you are not available on the revised project dates, your allocation on the project has been released.<br/><br/> \r\nPlease speak to <<ProjectResourceRequestor:GetProjectResourceRequestor:pipelineCode|jobCode>> for concerns on the allocations.<br/><br/> \r\nRegards,<br/>RMS Team<br/><br/>** This is an autogenerated email. Kindly do not respond to this.**", "employee_name_roll_over_terminated", "ROLL_OVER_ALLOCATION_TERMINATED_DUE_TO_UNAVAILABLITY" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 274L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> Roll Forward Update - Allocation Release", "Project <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> has been rolled forward to <<pipelinestartdate>>. ", "employee_name_roll_over_terminated", "ROLL_OVER_ALLOCATION_TERMINATED_DUE_TO_UNAVAILABLITY" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 276L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", " Allocation rejection by <<empEmail:ResourceAllocationDetails:item_id>> for Project: <<ProjectName:ResourceAllocationDetails:item_id>> have been approved by reviewer <<updated_by>>. Please do new allocations", "resource_requestor_allocation_wf", "REVIEWER_ACCEPTED_EMPLOYEE_REJECTION_RESPONSE" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 277L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "Dear User,<br/><br/> \r\nThe Allocation request rejected by employee <<empEmail:ResourceAllocationDetails:item_id>> for Project: <<ProjectName:ResourceAllocationDetails:item_id>> has been approved by Reviewer <<updated_by>>. \r\nRequest you to please allocate another resource.<br/><br/> \r\nPlease <a href='<<BaseUrl:config>>project-details/<<pipelinecode##>>/<<jobcode##>>?tab=4'>click here</a> for the details.<br/><br/> \r\nRegards,<br/>RMS Team<br/><br/>**This is an autogenerated email.Kindly do not respond to this.**", "resource_requestor_allocation_wf", "REVIEWER_ACCEPTED_EMPLOYEE_REJECTION_RESPONSE" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 293L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update ", "Dear User,<br/><br/> \r\nYour assignment to the Project: <<ProjectName:ResourceAllocationDetails:item_id>> has been cancelled. \r\nYour request for non acceptance of the allocation  has been approved by Reviewer <<updated_by>>. \r\n<br/><br/> \r\nRegards,<br/>RMS Team<br/><br/>** This is an autogenerated email. Kindly do not respond to this.**", "employee_name_allocation_wf", "NOTIFICATION_TO_EMPLOYEE_REVIEWER_APPROVED_EMPLOYEE_REJECTION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 295L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update ", "Your allocation to the Project: <<ProjectName:ResourceAllocationDetails:item_id>> has been cancelled.Allocation Rejection request has been approved by Reviewer <<updated_by>> .", "employee_name_allocation_wf", "NOTIFICATION_TO_EMPLOYEE_REVIEWER_APPROVED_EMPLOYEE_REJECTION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 296L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Project: <<projectname>> Allocation Update", "Project: <<projectname>> allocations have been moved to new project code", "previousrolesemails", "JOB_CODE_UPDATE_TO_NEW_CODE" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 297L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Project: <<projectname>> Allocation Update", "Dear User,<br/><br/> \r\nAllocations on the Project: <<projectname>> have been moved from Project Code <<oldprojectcode>> to new Project Code <<newprojectcode>> due to an update in WCGT of the Job Code.<br/><br/> \r\nPlease <a href='<<BaseUrl:config>>project-details/<<newpipelinecode##>>/<<newjobcode##>>?tab=0'>click here<a/> for the details.<br/><br/> \r\nRegards,<br/>RMS Team<br/><br/>**This is an autogenerated email.Kindly do not respond to this.**", "previousrolesemails", "JOB_CODE_UPDATE_TO_NEW_CODE" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 298L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Additional Delegate assigned to Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>>", "You have been assigned as Additional Delegate for <<User Requesting>> for Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>>.", "employee_name_additional_delegate", "ASSIGNMENT_OF_ADDITIONAL_DELEGATE_BY_ADDITIONAL_EL" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 299L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Additional Delegate assigned to Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>>", "Dear User,<br/><br/> \r\n<<User requesting>> has assigned you as Additional Delegate to the Additional EL for Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> on date <<userassignmentdate>> <br/><br/> \r\nPlease <a href='<<BaseUrl:config>>project-details/<<pipelinecode##>>/<<jobcode##>>?tab=0'>click here</a> for the details. You may contact <<User Requesting>> to discuss your responsibilities on the project in RMS.<br/><br/> \r\nRegards,<br/>RMS Team<br/><br/>**This is an autogenerated email.Kindly do not respond to this.**", "employee_name_additional_delegate", "ASSIGNMENT_OF_ADDITIONAL_DELEGATE_BY_ADDITIONAL_EL" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 300L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "RMS Admin Role assigned", "You have been assigned the Admin role.Please check your email or visit the User Management Section.", "employee_name_assign_as_admin", "ADMIN_ASSIGNED_BY_SYSTEM_ADMIN" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 301L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "RMS Admin Role assigned", "Dear User,<br/><br/> \r\nYou have been assigned the role of Admin in the RMS application.<br/><br/> \r\nPlease <a href='<<BaseUrl:config>>roles-permission'>click here</a> to view details in the User Management section.<br/><br/> \r\nRegards,<br/>RMS Team<br/><br/>**This is an autogenerated email.Kindly do not respond to this.**", "employee_name_assign_as_admin", "ADMIN_ASSIGNED_BY_SYSTEM_ADMIN" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 302L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Project:<<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> - <<pipelineStatus :GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>>", "Dear User,<br/><br/> \r\nProject: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>>. has been <<pipelineStatus :GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> on <<SuspendedAt:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>>. Your allocation has been auto-released from the project. <br/><br/> \r\nRegards,<br/>RMS Team<br/><br/>** This is an autogenerated email. Kindly do not respond to this.**", "employee_name_allocation", "PROJECT_SUSPENSION_ALLOCATION_RELEASE_NOTIFICATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 303L,
                columns: new[] { "cc", "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Reviewer,AdditionalEl,AdditionalDelegate", "Notification", "EMAIL", "Notification", "Project:<<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> - Suspended", "Dear User, <br/><br/> \r\n<<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>>. has been <<pipelineStatus:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> on <<:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>>. Any allocated resources have been auto-released. <br/><br/> \r\nPlease <a href='<<BaseUrl:config>>project-details/<<pipelinecode##>>/<<jobcode##>>?tab=0'>click here</a> for the details. <br/><br/> \r\nRegards,<br/>RMS Team<br/><br/>**This is an autogenerated email.Kindly do not respond to this.**", "ResourceRequestor", "PROJECT_SUSPENSION_NOTIFICATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 304L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Project:<<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> - Suspended", "Pipeline: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>>. has been <<pipelineStatus:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> on <<:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>>. Any allocated resources have been auto-released.", "ResourceRequestor,Reviewer,AdditionalEl,AdditionalDelegate", "PROJECT_SUSPENSION_NOTIFICATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 305L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Project:<<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> - <<pipelineStatus :GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>>", "Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>>. has been <<pipelineStatus :GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> on <<SuspendedAt:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>>. Your allocation has been auto-released from the project.", "employee_name_allocation", "PROJECT_SUSPENSION_ALLOCATION_RELEASE_NOTIFICATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 306L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Allocation conflicts for <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>>", "Dear User,<br/><br/> \r\nEmployee: <<Employee Name>> is on leave for the below mentioned dates which conflicts with allocations on the Project <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>>.<br/><br/> \r\nLeave Dates : <<LeaveDates:EmployeeLeaves:empEmail>><br/><br/> \r\nPlease <a href='<<BaseUrl:config>>project-details/<<pipelinecode##>>/<<jobcode##>>?tab=3'>click here</a> to review allocation detail and kindly update the allocations.<br/><br/> \r\nRegards,<br/>RMS Team<br/><br/>**This is an autogenerated email.Kindly do not respond to this.**", "Requestor,Reviewer,AdditionalEl,AdditionalDelegate", "EMPLOYEE_LEAVE_NOTIFCATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 307L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> Budget Updates", "Dear User,<br/><br/> \r\nProject: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> has consumed <<Limit:GETPROJECTBUDGET:pipelineCode|jobCode>> % of the budget in allocations.<br/><br/> \r\nPlease <a href='<<BaseUrl:config>>project-details/<<pipelinecode##>>/<<jobcode##>>?tab=2'>click here</a> for the details. Request you to review the future budget requirements and seek additional budget approval in WCGT if required.<br/><br/> \r\nRegards,<br/>RMS Team<br/><br/>**This is an autogenerated email.Kindly do not respond to this.**", "Requestor,Reviewer", "PROJECT_BUDGET_NOTIFICATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 308L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> Budget Updates", "Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> has consumed <<ProjectBudget:GETPROJECTBUDGET:pipelineCode|jobCode>> % of the budget in allocations.", "Requestor,Reviewer", "PROJECT_BUDGET_NOTIFICATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 309L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Allocation conflicts for <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>>", "Employee <<empEmail:ResourceAllocationDetails:item_id>> is not available on the allocated dates for the project <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>>. \r\nKindly update the allocations accordingly.", "Requestor,Reviewer,AdditionalEl,AdditionalDelegate", "EMPLOYEE_LEAVE_NOTIFCATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 310L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Allocation conflicts for <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>>", "Dear User,<br/><br/> \r\nAllocation conflict for Employee: <<empEmail:ResourceAllocationDetails:item_id>> as the employee is not available.<br/><br/> \r\nKindly update the allocations accordingly.<br/><br/> \r\nPlease <a href='<<BaseUrl:config>>project-details/<<pipelinecode##>>/<<jobcode##>>?tab=3'>click here</a> for the details.<br/><br/> \r\nRegards,<br/>RMS Team<br/><br/>**This is an autogenerated email.Kindly do not respond to this.**", "Requestor,Reviewer,AdditionalEl,AdditionalDelegate", "ALLOCATION_CONFLICT_NOTIFICATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 311L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Skill Assessment Review Updates", "Dear User,<br/><br/> \r\nYour skill self-assessment request for the following skill has been approved by your Supercoach \r\n<<updated_by>>. <br/><br/> \r\nSkill Name : <<skillname:UserSkill:item_id>><br/> \r\nSkill Proficiency Level : <<Proficiency:UserSkill:item_id>><br/><br/> \r\nPlease <a href='<<BaseUrl:config>>myskill'>click here</a> for the details.<br/><br/> \r\nRegards,<br/>RMS Team<br/><br/>**This is an autogenerated email.Kindly do not respond to this.**", "userskillemail", "SUPERCOACH_ACCEPTED_SKILL_ASSESSMENT_REQUEST" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 312L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Skill Assessment Review Updates", "Your Skill Review request has been actioned.", "userskillemail", "SUPERCOACH_ACCEPTED_SKILL_ASSESSMENT_REQUEST" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 313L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Allocation conflicts for <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>>", "Dear User,<br/><br/> \r\nAllocation conflicts in the project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> as Employee: <<empEmail:ResourceAllocationDetails:item_id>> is not available.<br/><br/> \r\nPlease <a href='<<BaseUrl:config>>project-details/<<pipelinecode##>>/<<jobcode##>>?tab=3'>click here</a> to review allocation detail and kindly update the allocations.<br/><br/> \r\nRegards,<br/>RMS Team<br/><br/>**This is an autogenerated email.Kindly do not respond to this.**", "Requestor,Reviewer,AdditionalEl,AdditionalDelegate", "EMPLOYEE_ABSCONDING_NOTIFCATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 314L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Allocation conflicts for <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>>", "Employee <<empEmail:ResourceAllocationDetails:item_id>> is not available on the allocated dates for the project <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>>. \r\nKindly update the allocations accordingly.", "Requestor,Reviewer,AdditionalEl,AdditionalDelegate", "EMPLOYEE_ABSCONDING_NOTIFCATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 315L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Allocation conflicts for <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>>", "Employee <<empEmail:ResourceAllocationDetails:item_id>> is not available on the allocated dates for the project <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>>. \r\nKindly update the allocations accordingly.", "Requestor,Reviewer,AdditionalEl,AdditionalDelegate", "ALLOCATION_CONFLICT_NOTIFICATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 316L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Project Updates", "Dear User, \r\n<br/><br/> \r\nThe following activities have been performed on the below mentioned Job / Pipeline (s): \r\n<br/> \r\n<<RequistionSummary:Allocation:PipelineCode|JobCode>> \r\n<br/> \r\n<<RequstionSummary:Requistion:PipelineCode|JobCode>> \r\n<br/> \r\n<<AllocationSummary:AllocationPublished:PipelineCode|JobCode>> \r\n<br/> \r\n<<MarketPlaceSummary:MarketPlace:PipelineCode|JobCode>> \r\n<br/> \r\nPlease <a href='<<BaseUrl:config>>projects'>click here </a> to view the details of project(s). \r\n<br/> \r\n<br/> \r\nRegards,<br/> \r\nRMS Team<br/><br/>** This is an autogenerated email. Kindly do not respond to this.**", "ResourceRequestor,Reviewer,AdditionalEl,AdditionalDelegate", "ALLOCATION_SUMMARY_NOTIFICATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 317L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", " Additional EL assigned to Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>>", "Dear User, <br/><br/> \r\n<<userrequesting>> has assigned you Additional EL for project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>> on date <<userassignmentdate>> \r\n<br/> <br/> Please <a href='<<BaseUrl:config>>project-details/<<pipelinecode##>>/<<jobcode##>>?tab=0'>click here</a> for the details. You may contact <<userrequesting>> to discuss your responsibilities on the project in RMS. <br/><br/> \r\nRegards,<br/>RMS Team<br/><br/>** This is an autogenerated email. Kindly do not respond to this.**", "newadditionalels", "PROJECT_ADDITIONAL_EL_UPDATE_NOTIFICATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 318L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", " Additional EL assigned to Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>>", "You have been assigned as Additional EL for Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>> on date <<userassignmentdate>>", "newadditionalels", "PROJECT_ADDITIONAL_EL_UPDATE_NOTIFICATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 319L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Delegate assignment on Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>> ", "Dear User,<br/><br/> \r\nYou have been assigned as Delegate to <<delegateassignedby>> on Project <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>> on date <<delegateassignmentdate>>. \r\n<br/> <br/> Please <a href='<<BaseUrl:config>>project-details/<<pipelinecode##>>/<<jobcode##>>?tab=0'>click here</a> for the details.You may contact <<delegateassignedby>> to discuss your responsibilities on the project in RMS. <br/><br/> \r\nRegards,<br/>RMS Team<br/><br/>** This is an autogenerated email. Kindly do not respond to this.**", "Delegate", "PROJECT_DELEGATE_UPDATE_NOTIFICATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 320L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Delegate assignment on Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>> ", " \r\nYou have been assigned as Delegate for <<delegateassignedby>> in Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>> on date <<delegateassignmentdate>>.", "Delegate", "PROJECT_DELEGATE_UPDATE_NOTIFICATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 321L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Additional Delegate assigned to Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>> ", "Dear User,<br/><br/> \r\n<<userrequesting>> has assigned you as Additional Delegate to the Additional EL for Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>> on date <<userassignmentdate>> \r\n<br/> <br/> Please <a href='<<BaseUrl:config>>project-details/<<pipelinecode##>>/<<jobcode##>>?tab=0'>click here</a> for the details. You may contact <<userrequesting>> to discuss your responsibilities on the project in RMS. <br/> <br/> \r\nRegards,<br/>RMS Team<br/><br/>** This is an autogenerated email. Kindly do not respond to this.**", "newadditionaldelegates", "PROJECT_ADDITIONAL_DELEGATE_UPDATE_NOTIFICATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 322L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Additional Delegate assigned to Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>>", " \r\nYou have been assigned as Additional Delegate for <<userrequesting>> for Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>>.  ", "newadditionaldelegates", "PROJECT_ADDITIONAL_DELEGATE_UPDATE_NOTIFICATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 323L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Skill Assessment Review Updates", "Your Skill Review request has been actioned.", "userskillemail", "SUPERCOACH_REJECTED_SKILL_ASSESSMENT_REQUEST" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 324L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Skill Assessment Review Updates", "Dear User, <br/><br/> \r\nYour skill assessment approval has been declined by your Supercoach <<updated_by>>. <br/><br/> \r\nSkill Name : <<skillname:UserSkill:item_id>><br/> \r\nSkill Proficiency Level : <<Proficiency:UserSkill:item_id>><br/> \r\nReason : <<rejection_remark>> <br/><br/> \r\nPlease <a href='<<BaseUrl:config>>myskill'>click here</a> for the details and reach out to your Supercoach for any further clarifications.<br/><br/> \r\nRegards,<br/>RMS Team<br/><br/>** This is an autogenerated email. Kindly do not respond to this.**", "userskillemail", "SUPERCOACH_REJECTED_SKILL_ASSESSMENT_REQUEST" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 400L,
                columns: new[] { "cc", "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "", "Notification", "PUSH", "Notification", "Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>> Allocations Update.", "Project <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>> end date has been updated to <<enddate>>. Please take necessary actions.", "ResourceRequestor,Reviewer,AdditionalEl,AdditionalDelegate,Delegate", "PROJECT_END_DATE_UPDATE_NOTIFICATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 401L,
                columns: new[] { "cc", "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "", "Notification", "EMAIL", "Notification", "Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>> end date update.", "Dear User,<br/><br/> \r\nProject <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>> end date has been updated to <<enddate>>.Please review allocations made to the project and take any necessary actions. \r\n<br/><br/> \r\nPlease <a href='<<BaseUrl:config>>project-details/<<pipelinecode##>>/<<jobcode##>>?tab=0'>click here</a> for the details. \r\n<br/><br/> \r\nRegards,<br/>RMS Team<br/><br/>**This is an autogenerated email.Kindly do not respond to this.* *", "ResourceRequestor,Reviewer,AdditionalEl,AdditionalDelegate,Delegate", "PROJECT_END_DATE_UPDATE_NOTIFICATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 402L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Project: <<projectname>> Allocation Update", "Dear User,<br/><br/> \r\nYour Allocation on the Project: <<projectname>> has been moved from Project Code <<oldprojectcode>> to new Project Code <<newprojectcode>>.<br/><br/> \r\nPlease <a href='<<BaseUrl:config>>project-details/<<newpipelinecode##>>/<<newjobcode##>>?tab=0'>click here<a/> for the details.<br/><br/> \r\nRegards,<br/>RMS Team<br/><br/>** This is an autogenerated email. Kindly do not respond to this.**", "employee_jobcode_change", "PROJECT_JOB_CODE_CHANGE_INFORMATION_TO_EMPLOYEE" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 403L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Project: <<projectname>> Allocation Update", "Project: <<projectname>> allocations have been moved to new project code", "employee_jobcode_change", "PROJECT_JOB_CODE_CHANGE_INFORMATION_TO_EMPLOYEE" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 404L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "You have been successfully  allocated  to the project <<ProjectName:ResourceAllocationDetails:item_id>> on <<startdate:ResourceAllocationDetails:item_id>>.", "employee_name_allocation_wf", "NOTIFICATION_TO_EMPLOYEE_AFTER_EMPLOYEE_ACCEPTS" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 405L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "Dear User,<br/><br/> \r\nYou have been allocated to the Project: <<ProjectName:ResourceAllocationDetails:item_id>>  on <<startdate:ResourceAllocationDetails:item_id>>.<br/><br/> \r\nPlease <a href='<<BaseUrl:config>>project-details/<<pipelinecode##>>/<<jobcode##>>?tab=3'>click here</a> for the details.<br/><br/> \r\nRegards,<br/>RMS Team<br/><br/>**This is an autogenerated email.Kindly do not respond to this.**", "employee_name_allocation_wf", "NOTIFICATION_TO_EMPLOYEE_AFTER_EMPLOYEE_ACCEPTS" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 406L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", " \r\nYour allocation to the project <<ProjectName:ResourceAllocationDetails:item_id>> has been cancelled. Allocation Rejection request has been auto approved by the system.", "employee_name_allocation_wf", "NOTIFICATION_TO_EMPLOYEE_REQUESTOR_AUTO_APPROVED_REJECTION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 407L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", " \r\nDear User,<br/><br/> \r\nResource allocation for employee <<empEmail:ResourceAllocationDetails:item_id>> for Project: <<ProjectName:ResourceAllocationDetails:item_id>> has been cancelled, as the resource did not accept the allocation.<br/><br/> \r\nRequest you to allocate another resource for the Requisition.<br/><br/> \r\nRegards,<br/>RMS Team<br/><br/>** This is an autogenerated email. Kindly do not respond to this.**", "employee_name_allocation_wf", "NOTIFICATION_TO_EMPLOYEE_REQUESTOR_AUTO_APPROVED_REJECTION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 408L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Project: <<projectname>> Markeplace Interest Submitted", "You have received an interest for the Project: <<projectname>> in Marketplace. Total <<noofinterested>> likes received.", "ResourceRequestor,Delegate", "NOTIFICATION_FOR_INTEREST_IN_MARKETPLACE_AGAINST_THEIR_PROJECT" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 409L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "PUSH_NOTIFICATION_FOR_ALLOCATION_OF_RESOURCE_TO_PROJECT_REVIEWER_CONFIG_OFF", "Resources have been assigned to the project <<ProjectName:ResourceAllocationDetails:item_id>> on <<created_at>>. ", "resource_requestor_allocation_wf,Reviewer", "PUSH_NOTIFICATION_FOR_ALLOCATION_OF_RESOURCE_TO_PROJECT_REVIEWER_CONFIG_OFF" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 410L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "PUSH_NOTIFICATION_TO_REQUESTOR_AFTER_REVIEWER_ACCEPTS", "Resource Allocation Request for Project: <<ProjectName:ResourceAllocationDetails:item_id>>  have been approved by reviewer <<updated_by>>. ", "resource_requestor_allocation_wf", "PUSH_NOTIFICATION_TO_REQUESTOR_AFTER_REVIEWER_ACCEPTS" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 411L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "PUSH_NOTIFICATION_TO_REQUESTOR_ONCE_APPROVED_BY_EMPLOYEE", "Resource Allocation Request for Project: <<ProjectName:ResourceAllocationDetails:item_id>> have been confirmed by employee <<empEmail:ResourceAllocationDetails:item_id>>. ", "resource_requestor_allocation_wf,Reviewer", "PUSH_NOTIFICATION_TO_REQUESTOR_ONCE_APPROVED_BY_EMPLOYEE" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 412L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "PUSH_NOTIFICATION_TO_REQUESTOR_ONCE_WITHDRAWAL_BY_EMPLOYEE", "Resource Allocation Request for Project: <<ProjectName:ResourceAllocationDetails:item_id>> have been confirmed by employee <<empEmail:ResourceAllocationDetails:item_id>>. ", "resource_requestor_allocation_wf,Reviewer", "PUSH_NOTIFICATION_TO_REQUESTOR_ONCE_WITHDRAWAL_BY_EMPLOYEE" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 413L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "PUSH_NOTIFICATION_TO_REQUESTOR_REVIEWER_ACCEPTS_EMPLOYEE_UPDATE", "Allocation of <<empEmail:ResourceAllocationDetails:item_id>>, <<designation:ResourceAllocationDetails:item_id>> , <<grade:ResourceAllocationDetails:item_id>> on the Project: <<ProjectName:ResourceAllocationDetails:item_id>> has been updated. ", "resource_requestor_allocation_wf", "PUSH_NOTIFICATION_TO_REQUESTOR_REVIEWER_ACCEPTS_EMPLOYEE_UPDATE" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 414L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "PUSH_NOTIFICATION_TO_REQUESTOR_AND_REVIEWER_AFTER_EMPLOYEE_UPDATE", "Allocation of <<empEmail:ResourceAllocationDetails:item_id>>, <<designation:ResourceAllocationDetails:item_id>> , <<grade:ResourceAllocationDetails:item_id>> on the Project: <<ProjectName:ResourceAllocationDetails:item_id>> has been updated. ", "resource_requestor_allocation_wf,Reviewer", "PUSH_NOTIFICATION_TO_REQUESTOR_AND_REVIEWER_AFTER_EMPLOYEE_UPDATE" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Link", "Module", "SubModule" },
                values: new object[] { "/myskill", "Notification", "Skill" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Link", "Module", "SubModule" },
                values: new object[] { "/skill-review", "Notification", "Skill" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "Link", "Module", "SubModule" },
                values: new object[] { "/searchskill", "Notification", "Skill" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "Link", "Module", "SubModule" },
                values: new object[] { "/skillmaster", "Notification", "Skill" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "Link", "Module", "SubModule" },
                values: new object[] { "/projects", "Notification", "Skill" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "Link", "Module", "SubModule" },
                values: new object[] { "/projects", "Notification", "Project" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "Link", "Module", "SubModule" },
                values: new object[] { "/marketplace", "Notification", "Project" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "Link", "Module", "SubModule" },
                values: new object[] { "/my-preference", "Notification", "Manage" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "Link", "Module", "SubModule" },
                values: new object[] { "/my-preference", "Notification", "Manage" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "Link", "Module", "SubModule" },
                values: new object[] { "/my-calender", "Notification", "Manage" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 11L,
                columns: new[] { "Link", "Module", "SubModule" },
                values: new object[] { "/roles-permission", "Notification", "User" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 12L,
                columns: new[] { "Link", "Module", "SubModule" },
                values: new object[] { "/myapproval", "Notification", "User" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 13L,
                columns: new[] { "Link", "Module", "SubModule" },
                values: new object[] { "/project-details/<<pipelinecode##>>/<<jobcode##>>?tab=0", "Notification", "Details" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 14L,
                columns: new[] { "Link", "Module", "SubModule" },
                values: new object[] { "/project-details/<<pipelinecode##>>/<<jobcode##>>?tab=1", "Notification", "Details" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 15L,
                columns: new[] { "Link", "Module", "SubModule" },
                values: new object[] { "/project-details/<<pipelinecode##>>/<<jobcode##>>?tab=2", "Notification", "Details" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 16L,
                columns: new[] { "Link", "Module", "SubModule" },
                values: new object[] { "/project-details/<<pipelinecode##>>/<<jobcode##>>?tab=3", "Notification", "Details" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 17L,
                columns: new[] { "Link", "Module", "SubModule" },
                values: new object[] { "/project-details/<<pipelinecode##>>/<<jobcode##>>?tab=4", "Notification", "Details" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 18L,
                columns: new[] { "Link", "Module", "SubModule" },
                values: new object[] { "/project-details/<<pipelinecode##>>/<<jobcode##>>?tab=5", "Notification", "Details" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 19L,
                columns: new[] { "Link", "Module", "SubModule" },
                values: new object[] { "/project-details/<<newpipelinecode##>>/<<newjobcode##>>?tab=0", "Notification", "Details" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 42L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "You have been assigned to the Project <<ProjectName:ResourceAllocationDetails:item_id>> on <<created_at>>.", "employee_name_allocation_wf", "NOTIFICATION_TO_EMPLOYEE_FOR_HIS_ALLOCATION_TASK_TO_PROJECT" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 43L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "Dear User,<br/><br/> \r\nYou have been assigned to the Project: <<ProjectName:ResourceAllocationDetails:item_id>> on <<created_at>>.<br/> \r\nThe Project Allocation Period is:<br/> \r\nStart Date: <<startdate:ResourceAllocationDetails:item_id>><br/> End Date: <<enddate:ResourceAllocationDetails:item_id>><br/> Hours: <<totaleffort:ResourceAllocationDetails:item_id>>.<br/><br/> \r\nPlease <a href='<<BaseUrl:config>>myapproval'>click here</a> to view further details of the allocation.<br/><br/> \r\nPlease note, if you are unable to review the request for approval, it will be auto - approved by <<due_date>>.<br/><br/> \r\nRegards,<br/>RMS Team<br/><br/>**This is an autogenerated email.Kindly do not respond to this.**", "employee_name_allocation_wf", "NOTIFICATION_TO_EMPLOYEE_FOR_HIS_ALLOCATION_TASK_TO_PROJECT" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 139L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "Dear User,<br/><br/> \r\nYour assignment on the Project: <<ProjectName:ResourceAllocationDetails:item_id>> has been updated on <<modifiedat>>.<br/><br/> \r\nYou revised allocation dates are:<br/> \r\nStart Date: <<startdate:ResourceAllocationDetails:item_id>><br/> End Date: <<enddate:ResourceAllocationDetails:item_id>><br/> Hours: <<totaleffort:ResourceAllocationDetails:item_id>>.<br/><br/> \r\nPlease <a href='<<BaseUrl:config>>project-details/<<pipelinecode##>>/<<jobcode##>>?tab=3'>click here</a> or the details.<br/><br/> \r\nRegards,<br/> \r\nRMS Team<br/><br/>**This is an autogenerated email.Kindly do not respond to this.**", "employee_name_allocation_wf", "NOTIFICATION_FOR_ALLOCATION_UPDATE_TO_EMPLOYEE" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 140L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "Your assignment on the project <<ProjectName:ResourceAllocationDetails:item_id>> has been updated on <<modifiedat>>. ", "employee_name_allocation_wf", "NOTIFICATION_FOR_ALLOCATION_UPDATE_TO_EMPLOYEE" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 141L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "Dear User,<br/><br/> \r\nResource allocation request has been rejected by reviewer: <<updated_by:GetUserInfo:updated_by>> for employee: <<empEmail:ResourceAllocationDetails:item_id>> \r\nin the project: <<ProjectName:ResourceAllocationDetails:item_id>>.<br/> \r\nRejection reason: <<ReasonForRejectionProvidedByReviewer>><br/><br/> \r\nRequest you to please allocate another resource.<br/><br/> \r\nPlease <a href='<<BaseUrl:config>>project-details/<<pipelinecode##>>/<<jobcode##>>?tab=4'>click here</a> for the details.<br/><br/> \r\nRegards,<br/>RMS Team<br/><br/>**This is an autogenerated email.Kindly do not respond to this.**", "resource_requestor_allocation_wf,Reviewer", "EMPLOYEE_ALLOCATION_REJECTED_BY_REVIEWER_NOTIFICATION_TO_RESOURCE_REQUESTOR" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 142L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "Resource allocation requests have been rejected for Project: <<ProjectName:ResourceAllocationDetails:item_id>> by reviewer <<updated_by>>. Please do new allocations", "resource_requestor_allocation_wf,Reviewer", "EMPLOYEE_ALLOCATION_REJECTED_BY_REVIEWER_NOTIFICATION_TO_RESOURCE_REQUESTOR" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 143L,
                columns: new[] { "cc", "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "", "Notification", "EMAIL", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "Dear User,<br/><br/> \r\nResource allocation request has been rejected by Employee: <<empEmail:ResourceAllocationDetails:item_id>> for Project: <<ProjectName:ResourceAllocationDetails:item_id>>.<br/> \r\nYou can terminate the workflow by <<due_date>> to make new allocations provided the allocated employee does not withdraw their rejection.<br/> \r\nRejection reason: <<ReasonForRejectionProvidedByEmployee>><br/><br/> \r\nPlease <a href='<<BaseUrl:config>>project-details/<<pipelinecode##>>/<<jobcode##>>?tab=4'>click here</a> for the details.<br/><br/> \r\nRegards,<br/>RMS Team<br/><br/>**This is an autogenerated email.Kindly do not respond to this.**", "resource_requestor_allocation_wf,Reviewer", "NOTIFICATION_TO_RESOURCE_REQUESTOR_AND_REVIEWER_ON_EMPLOYEE_REJECTION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 144L,
                columns: new[] { "cc", "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "", "Notification", "PUSH", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "Resource allocation requests have been rejected for Project: <<ProjectName:ResourceAllocationDetails:item_id>> by employee <<empEmail:ResourceAllocationDetails:item_id>>. Please make new allocations.", "resource_requestor_allocation_wf,Reviewer", "NOTIFICATION_TO_RESOURCE_REQUESTOR_AND_REVIEWER_ON_EMPLOYEE_REJECTION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 145L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "Dear User,<br/><br/> \r\nResource allocation request has been rejected by employee <<empEmail:ResourceAllocationDetails:item_id>> for Project: <<ProjectName:ResourceAllocationDetails:item_id>><br/> \r\nRejection reason: <<ReasonForRejectionProvidedByEmployee>><br/><br/> \r\nPlease <a href='<<BaseUrl:config>>project-details/<<pipelinecode##>>/<<jobcode##>>?tab=4'>click here</a> to view details and review employee action.<br/><br/> \r\nRegards,<br/>RMS Team<br/><br/>**This is an autogenerated email.Kindly do not respond to this.**", "resource_requestor_allocation_wf,Reviewer", "NOTIFICATION_TO_RESOURCE_REQUESTOR_AND_REVIEWER_ON_EMPLOYEE_REJECTION_REQUESTOR_CONFIG_IS_ON" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 146L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "Resource allocation requests have been rejected for Project: <<ProjectName:ResourceAllocationDetails:item_id>> by employee <<empEmail:ResourceAllocationDetails:item_id>>. Please review employee action", "resource_requestor_allocation_wf,Reviewer", "NOTIFICATION_TO_RESOURCE_REQUESTOR_AND_REVIEWER_ON_EMPLOYEE_REJECTION_REQUESTOR_CONFIG_IS_ON" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 147L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "Dear User,<br/><br/> \r\nYour request for non-acceptance of allocation has been rejected by the Reviewer <<updated_by>>. You have now been allocated to the Project: <<ProjectName:ResourceAllocationDetails:item_id>><br/><br/>The Project Allocation Period is:<br/> \r\nStart Date: <<startdate:ResourceAllocationDetails:item_id>><br/> End Date: <<enddate:ResourceAllocationDetails:item_id>><br/> Hours: <<totaleffort:ResourceAllocationDetails:item_id>><br/><br/> \r\nFor any queries please reach out to the above mentioned Reviewer.<br/><br/> \r\nPlease <a href='<<BaseUrl:config>>project-details/<<pipelinecode##>>/<<jobcode##>>?tab=3'>click here</a> review project and allocation the details..<br/><br/> \r\nRegards,<br/>RMS Team<br/><br/>**This is an autogenerated email.Kindly do not respond to this.**", "employee_name_allocation_wf", "NOTIFICATION_TO_EMPLOYEE_AFTER_REVIEWER_ACCEPTS" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 148L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "You have been allocated to the project <<ProjectName:ResourceAllocationDetails:item_id>> on <<startdate:ResourceAllocationDetails:item_id>> as your rejection has not been accepted.", "employee_name_allocation_wf", "NOTIFICATION_TO_EMPLOYEE_AFTER_REVIEWER_ACCEPTS" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 149L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "Dear User,<br/><br/> \r\nYou have been auto-allocated to the Project: <<ProjectName:ResourceAllocationDetails:item_id>>.<br/> \r\nStart Date: <<startdate:ResourceAllocationDetails:item_id>><br/> End Date: <<enddate:ResourceAllocationDetails:item_id>><br/> Hours: <<totaleffort:ResourceAllocationDetails:item_id>>.<br/><br/> \r\nPlease <a href='<<BaseUrl:config>>project-details/<<pipelinecode##>>/<<jobcode##>>?tab=3'>click here</a> to view allocation details.<br/><br/> \r\nRegards,<br/>RMS Team<br/><br/>**This is an autogenerated email.Kindly do not respond to this.**", "employee_name_allocation_wf", "NOTIFICATION_TO_EMPLOYEE_AUTO_APPROVED_AFTER_EMPLOYEE_DUE_DATE_CROSS" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 150L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "Your allocation has been auto-approved for the Project <<ProjectName:ResourceAllocationDetails:item_id>> on <<startdate:ResourceAllocationDetails:item_id>>", "employee_name_allocation_wf", "NOTIFICATION_TO_EMPLOYEE_AUTO_APPROVED_AFTER_EMPLOYEE_DUE_DATE_CROSS" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 151L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "Dear User,<br/><br/>As you have accepted the allocation, you have now been allocated to the Project: <<ProjectName:ResourceAllocationDetails:item_id>> on <<startdate:ResourceAllocationDetails:item_id>>. \r\n<br/><br/> \r\nPlease <a href='<<BaseUrl:config>>project-details/<<pipelinecode##>>/<<jobcode##>>?tab=3'>click here</a> to view further details.<br/><br/> \r\n<br/><br/> \r\nRegards,<br/>RMS Team<br/><br/>**This is an autogenerated email.Kindly do not respond to this.**", "employee_name_allocation_wf", "NOTIFICATION_TO_EMPLOYEE_WITHDRAWS_REJECTION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 152L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "You have successfully been allocated to the project <<ProjectName:ResourceAllocationDetails:item_id>> on <<startdate:ResourceAllocationDetails:item_id>>", "employee_name_allocation_wf", "NOTIFICATION_TO_EMPLOYEE_WITHDRAWS_REJECTION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 153L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "Dear User,<br/><br/> \r\nYour allocation to the Project: <<ProjectName:ResourceAllocationDetails:item_id>> has been cancelled, as you have not agreed to accept the allocation.<br/><br/> \r\nYour  request for not being allocated was approved by  <<updated_by>>.<br/><br/> \r\nRegards,<br/>RMS Team<br/><br/>** This is an autogenerated email. Kindly do not respond to this.**", "employee_name_allocation_wf", "NOTIFICATION_TO_EMPLOYEE_REQUESTOR_APPROVED_REJECTION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 154L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", " \r\nYour allocation to the project <<ProjectName:ResourceAllocationDetails:item_id>> has been cancelled. Allocation Rejection request has been approved by Resoruce Requestor <<updated_by>>.", "employee_name_allocation_wf", "NOTIFICATION_TO_EMPLOYEE_REQUESTOR_APPROVED_REJECTION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 155L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> Roll Forward Update", "Dear User,<br/><br/> \r\nProject <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> dates have been rolled forward from <<pipelineoldstartdate>> to <<pipelinestartdate>>. The system has taken the following actions based on the new start date which are set out below:<br/><br/> \r\n1. For allocated employees who are available on the revised dates, new allocation workflow and relevant approvals will be triggered.<br/> \r\n2. For allocated employees who are not available on certain dates in the new project period, those allocations will be moved to DRAFT state to review and change as appropriate.<br/> \r\n3. For allocated employees who are not available for the requested period, they will be released from the project.<br/><br/> \r\nPlease <a href='<<BaseUrl:config>>project-details/<<pipelinecode##>>/<<jobcode##>>?tab=3'>click here</a> for the details.<br/><br/> \r\nRegards,<br/>RMS Team<br/><br/>**This is an autogenerated email.Kindly do not respond to this.**", "Requestor,Delegate,Reviewer,AdditionalEl,AdditionalDelegate", "ROLL_OVER_ALLOCATED_EMPLOYEES_AVAILABLE_FOR_REVISED_DATES_ALLOCATION_SHIFTED_NEW_WF_TRIGGERED_NOTIFICATION_TO_REQUESTOR_AND_REVIEWER" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 156L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> Roll Forward Update", "Project <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> has been rolled forward to <<pipelinestartdate>>. Review Project details for change in allocations.", "Requestor,Delegate,Reviewer,AdditionalEl,AdditionalDelegate", "ROLL_OVER_ALLOCATED_EMPLOYEES_AVAILABLE_FOR_REVISED_DATES_ALLOCATION_SHIFTED_NEW_WF_TRIGGERED_NOTIFICATION_TO_REQUESTOR_AND_REVIEWER" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 169L,
                columns: new[] { "cc", "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "", "Notification", "EMAIL", "Notification", "Project Marketplace Updates", "Dear User,<br/><br/> \r\nBelow listed projects have been moved in the Marketplace.<br/><br/> \r\n<projectmovedtomarketplacetable><br/><br/> \r\nRegards,<br/>RMS Team<br/><br/>** This is an autogenerated email. Kindly do not respond to this.**", "Reviewer,ResourceRequestor,Delegate", "PROJECT_MOVED_TO_MARKETPLACE_SUMMARY_EMAIL" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 170L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "New Projects in the Marketplace", "Dear User,<br/><br/> \r\nNew projects have been added to the Marketplace in the RMS. These projects offer exciting opportunities for you to contribute your skills and expertise. A summary of the new projects provided in the table below:<br/><br/> \r\n<<projectlistinginmarketplacenotificationtoemployee>> \r\n<br/><br/> \r\nPlease <a href='<<BaseUrl:config>>marketplace'>click here</a> to view details in the Marketplace and submit your interest.<br/><br/> \r\nRegards,<br/>RMS Team<br/><br/>** This is an autogenerated email. Kindly do not respond to this.**", "employee", "CONSOLIDATED_MAIL_TO_EMPLOYEE_FOR_PROJECT_LISTING_IN_MARKETPLACE" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 171L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "RMS Tasks pending for your action", "Dear User,<br/><br/> \r\nFollowing tasks are pending for your action:<br/><br/> \r\n<<allocationtablecontent>><br/><br/> \r\n<<skilltablecontent>><br/><br/> \r\nPlease <a href='<<BaseUrl:config>>myapproval'>click here</a> for to review further details and approve/reject the request.<br/><br/> \r\nRegards,<br/>RMS Team<br/><br/>** This is an autogenerated email. Kindly do not respond to this.**", "employee", "RMS_ALLOCATION_AND_SKILL_TASKS_PENDING_FOR_ACTION_SUMMARY_MAIL" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 172L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "RMS Tasks pending for your action", "Dear User,<br/><br/> \r\nFollowing tasks are pending for your action: \r\n<br/><br/> \r\n<<allocationtablecontent>> \r\n<br/><br/> \r\n<<skilltablecontent>> \r\n<br/><br/> \r\nPlease <a href='<<BaseUrl:config>>myapproval'>click here</a> for to review further details and approve/reject the request. \r\n<br/><br/> \r\nRegards,<br/>RMS Team<br/><br/>** This is an autogenerated email. Kindly do not respond to this.**", "employee", "ADDITION_OF_NEW_PROJECT_NOTIFICATION_TO_RESOURCE_REQUESTOR" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 263L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>> Allocation Update", "Allocation updates for Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>>", "employee_name_release_resource,ResourceRequestor,Reviewer,AdditionalEl,Delegate,AdditionalDelegate", "NOTIFICATION_TO_EMPLOYEE_AS_RELEASED_FROM_PROJECT" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 264L,
                columns: new[] { "cc", "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "ResourceRequestor,Reviewer,AdditionalEl,Delegate,AdditionalDelegate", "Notification", "EMAIL", "Notification", "Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>> Allocation Update", "Dear User,<br/><br/> \r\nYour Allocation on the Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>> has been released.<br/> \r\nPlease reach out to <<modifiedBy>> for any queries.<br/><br/> \r\nRegards,<br/>RMS Team<br/><br/>** This is an autogenerated email. Kindly do not respond to this.**", "employee_name_release_resource", "NOTIFICATION_TO_EMPLOYEE_AS_RELEASED_FROM_PROJECT" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 267L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>> Roll Forward Update.", "Dear User,<br/><br/> \r\nAllocation of Employees has been updated on the  project : <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>> as the Project dates have been rolled forward from <<pipelineoldstartdate>> to <<pipelinestartdate>>.<br/><br/> \r\nPlease <a href='<<BaseUrl:config>>project-details/<<pipelinecode##>>/<<jobcode##>>?tab=3'>click here</a> for the details.<br/><br/> \r\nRegards,<br/>RMS Team<br/><br/>**This is an autogenerated email.Kindly do not respond to this.**", "Reviewer", "NOTIFICATION_TO_REVIEWER_ALLOCATIONS_ARE_SHIFTED_AND_NEW_WORKFLOW_ARE_TRIGGERED" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 268L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>> Roll Forward Update.", "Allocation Update - Project <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>> rolled forward to <<pipelinestartdate>>", "Reviewer", "NOTIFICATION_TO_REVIEWER_ALLOCATIONS_ARE_SHIFTED_AND_NEW_WORKFLOW_ARE_TRIGGERED" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 269L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>> Name Marketplace Summary", "Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>> has received <<employeeshowedinterestcount>> interests. ", "ResourceRequestor,Delegate", "NOTIFICATION_TO_RESOURCE_REQUESTOR_AND_DELEGATE_FOR_REMOVAL_OF_PROJECT_FROM_MP" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 270L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>> Name Marketplace Summary", "Dear User,<br/><br/> \r\nProject: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>> has been removed from Marketplace & has received <<employeeshowedinterestcount>> interests.<br/><br/> \r\nPlease <a href='<<BaseUrl:config>>project-details/<<pipelinecode##>>/<<jobcode##>>?tab=5'>click here</a> for the details.<br/><br/> \r\nRegards,<br/>RMS Team<br/><br/>**This is an autogenerated email.Kindly do not respond to this.**", "ResourceRequestor,Delegate", "NOTIFICATION_TO_RESOURCE_REQUESTOR_AND_DELEGATE_FOR_REMOVAL_OF_PROJECT_FROM_MP" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 271L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> Roll Forward Update - Allocation Release", "Dear User,<br/><br/> \r\nProject <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> dates have been rolled forward from <<pipelineoldstartdate>> to <<pipelinestartdate>>.  Since you are not available on the revised project dates, your allocation on the project has been released.<br/><br/> \r\nPlease speak to <<ProjectResourceRequestor:GetProjectResourceRequestor:pipelineCode|jobCode>> for concerns on the allocations.<br/><br/> \r\nRegards,<br/>RMS Team<br/><br/>** This is an autogenerated email. Kindly do not respond to this.**", "employee_name_roll_over_draft", "ROLL_OVER_ALLOCATION_IN_DRAFT_DUE_TO_UNAVAILABLITY" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 272L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> Roll Forward Update - Allocation Release", "Project <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> has been rolled forward to <<pipelinestartdate>>. Your allocation has been released. ", "employee_name_roll_over_draft", "ROLL_OVER_ALLOCATION_IN_DRAFT_DUE_TO_UNAVAILABLITY" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 273L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> Roll Forward Update - Allocation Release", "Dear User,<br/><br/> \r\nProject <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> dates have been rolled forward from <<pipelineoldstartdate>> to <<pipelinestartdate>>.  Since you are not available on the revised project dates, your allocation on the project has been released.<br/><br/> \r\nPlease speak to <<ProjectResourceRequestor:GetProjectResourceRequestor:pipelineCode|jobCode>> for concerns on the allocations.<br/><br/> \r\nRegards,<br/>RMS Team<br/><br/>** This is an autogenerated email. Kindly do not respond to this.**", "employee_name_roll_over_terminated", "ROLL_OVER_ALLOCATION_TERMINATED_DUE_TO_UNAVAILABLITY" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 274L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> Roll Forward Update - Allocation Release", "Project <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> has been rolled forward to <<pipelinestartdate>>. ", "employee_name_roll_over_terminated", "ROLL_OVER_ALLOCATION_TERMINATED_DUE_TO_UNAVAILABLITY" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 276L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", " Allocation rejection by <<empEmail:ResourceAllocationDetails:item_id>> for Project: <<ProjectName:ResourceAllocationDetails:item_id>> have been approved by reviewer <<updated_by>>. Please do new allocations", "resource_requestor_allocation_wf", "REVIEWER_ACCEPTED_EMPLOYEE_REJECTION_RESPONSE" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 277L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "Dear User,<br/><br/> \r\nThe Allocation request rejected by employee <<empEmail:ResourceAllocationDetails:item_id>> for Project: <<ProjectName:ResourceAllocationDetails:item_id>> has been approved by Reviewer <<updated_by>>. \r\nRequest you to please allocate another resource.<br/><br/> \r\nPlease <a href='<<BaseUrl:config>>project-details/<<pipelinecode##>>/<<jobcode##>>?tab=4'>click here</a> for the details.<br/><br/> \r\nRegards,<br/>RMS Team<br/><br/>**This is an autogenerated email.Kindly do not respond to this.**", "resource_requestor_allocation_wf", "REVIEWER_ACCEPTED_EMPLOYEE_REJECTION_RESPONSE" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 293L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update ", "Dear User,<br/><br/> \r\nYour assignment to the Project: <<ProjectName:ResourceAllocationDetails:item_id>> has been cancelled. \r\nYour request for non acceptance of the allocation  has been approved by Reviewer <<updated_by>>. \r\n<br/><br/> \r\nRegards,<br/>RMS Team<br/><br/>** This is an autogenerated email. Kindly do not respond to this.**", "employee_name_allocation_wf", "NOTIFICATION_TO_EMPLOYEE_REVIEWER_APPROVED_EMPLOYEE_REJECTION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 295L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update ", "Your allocation to the Project: <<ProjectName:ResourceAllocationDetails:item_id>> has been cancelled.Allocation Rejection request has been approved by Reviewer <<updated_by>> .", "employee_name_allocation_wf", "NOTIFICATION_TO_EMPLOYEE_REVIEWER_APPROVED_EMPLOYEE_REJECTION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 296L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Project: <<projectname>> Allocation Update", "Project: <<projectname>> allocations have been moved to new project code", "previousrolesemails", "JOB_CODE_UPDATE_TO_NEW_CODE" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 297L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Project: <<projectname>> Allocation Update", "Dear User,<br/><br/> \r\nAllocations on the Project: <<projectname>> have been moved from Project Code <<oldprojectcode>> to new Project Code <<newprojectcode>> due to an update in WCGT of the Job Code.<br/><br/> \r\nPlease <a href='<<BaseUrl:config>>project-details/<<newpipelinecode##>>/<<newjobcode##>>?tab=0'>click here<a/> for the details.<br/><br/> \r\nRegards,<br/>RMS Team<br/><br/>**This is an autogenerated email.Kindly do not respond to this.**", "previousrolesemails", "JOB_CODE_UPDATE_TO_NEW_CODE" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 298L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Additional Delegate assigned to Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>>", "You have been assigned as Additional Delegate for <<User Requesting>> for Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>>.", "employee_name_additional_delegate", "ASSIGNMENT_OF_ADDITIONAL_DELEGATE_BY_ADDITIONAL_EL" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 299L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Additional Delegate assigned to Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>>", "Dear User,<br/><br/> \r\n<<User requesting>> has assigned you as Additional Delegate to the Additional EL for Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> on date <<userassignmentdate>> <br/><br/> \r\nPlease <a href='<<BaseUrl:config>>project-details/<<pipelinecode##>>/<<jobcode##>>?tab=0'>click here</a> for the details. You may contact <<User Requesting>> to discuss your responsibilities on the project in RMS.<br/><br/> \r\nRegards,<br/>RMS Team<br/><br/>**This is an autogenerated email.Kindly do not respond to this.**", "employee_name_additional_delegate", "ASSIGNMENT_OF_ADDITIONAL_DELEGATE_BY_ADDITIONAL_EL" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 300L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "RMS Admin Role assigned", "You have been assigned the Admin role.Please check your email or visit the User Management Section.", "employee_name_assign_as_admin", "ADMIN_ASSIGNED_BY_SYSTEM_ADMIN" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 301L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "RMS Admin Role assigned", "Dear User,<br/><br/> \r\nYou have been assigned the role of Admin in the RMS application.<br/><br/> \r\nPlease <a href='<<BaseUrl:config>>roles-permission'>click here</a> to view details in the User Management section.<br/><br/> \r\nRegards,<br/>RMS Team<br/><br/>**This is an autogenerated email.Kindly do not respond to this.**", "employee_name_assign_as_admin", "ADMIN_ASSIGNED_BY_SYSTEM_ADMIN" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 302L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Project:<<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> - <<pipelineStatus :GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>>", "Dear User,<br/><br/> \r\nProject: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>>. has been <<pipelineStatus :GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> on <<SuspendedAt:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>>. Your allocation has been auto-released from the project. <br/><br/> \r\nRegards,<br/>RMS Team<br/><br/>** This is an autogenerated email. Kindly do not respond to this.**", "employee_name_allocation", "PROJECT_SUSPENSION_ALLOCATION_RELEASE_NOTIFICATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 303L,
                columns: new[] { "cc", "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Reviewer,AdditionalEl,AdditionalDelegate", "Notification", "EMAIL", "Notification", "Project:<<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> - Suspended", "Dear User, <br/><br/> \r\n<<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>>. has been <<pipelineStatus:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> on <<:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>>. Any allocated resources have been auto-released. <br/><br/> \r\nPlease <a href='<<BaseUrl:config>>project-details/<<pipelinecode##>>/<<jobcode##>>?tab=0'>click here</a> for the details. <br/><br/> \r\nRegards,<br/>RMS Team<br/><br/>**This is an autogenerated email.Kindly do not respond to this.**", "ResourceRequestor", "PROJECT_SUSPENSION_NOTIFICATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 304L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Project:<<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> - Suspended", "Pipeline: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>>. has been <<pipelineStatus:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> on <<:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>>. Any allocated resources have been auto-released.", "ResourceRequestor,Reviewer,AdditionalEl,AdditionalDelegate", "PROJECT_SUSPENSION_NOTIFICATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 305L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Project:<<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> - <<pipelineStatus :GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>>", "Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>>. has been <<pipelineStatus :GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> on <<SuspendedAt:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>>. Your allocation has been auto-released from the project.", "employee_name_allocation", "PROJECT_SUSPENSION_ALLOCATION_RELEASE_NOTIFICATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 306L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Allocation conflicts for <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>>", "Dear User,<br/><br/> \r\nEmployee: <<Employee Name>> is on leave for the below mentioned dates which conflicts with allocations on the Project <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>>.<br/><br/> \r\nLeave Dates : <<LeaveDates:EmployeeLeaves:empEmail>><br/><br/> \r\nPlease <a href='<<BaseUrl:config>>project-details/<<pipelinecode##>>/<<jobcode##>>?tab=3'>click here</a> to review allocation detail and kindly update the allocations.<br/><br/> \r\nRegards,<br/>RMS Team<br/><br/>**This is an autogenerated email.Kindly do not respond to this.**", "Requestor,Reviewer,AdditionalEl,AdditionalDelegate", "EMPLOYEE_LEAVE_NOTIFCATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 307L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> Budget Updates", "Dear User,<br/><br/> \r\nProject: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> has consumed <<Limit:GETPROJECTBUDGET:pipelineCode|jobCode>> % of the budget in allocations.<br/><br/> \r\nPlease <a href='<<BaseUrl:config>>project-details/<<pipelinecode##>>/<<jobcode##>>?tab=2'>click here</a> for the details. Request you to review the future budget requirements and seek additional budget approval in WCGT if required.<br/><br/> \r\nRegards,<br/>RMS Team<br/><br/>**This is an autogenerated email.Kindly do not respond to this.**", "Requestor,Reviewer", "PROJECT_BUDGET_NOTIFICATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 308L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> Budget Updates", "Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> has consumed <<ProjectBudget:GETPROJECTBUDGET:pipelineCode|jobCode>> % of the budget in allocations.", "Requestor,Reviewer", "PROJECT_BUDGET_NOTIFICATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 309L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Allocation conflicts for <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>>", "Employee <<empEmail:ResourceAllocationDetails:item_id>> is not available on the allocated dates for the project <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>>. \r\nKindly update the allocations accordingly.", "Requestor,Reviewer,AdditionalEl,AdditionalDelegate", "EMPLOYEE_LEAVE_NOTIFCATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 310L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Allocation conflicts for <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>>", "Dear User,<br/><br/> \r\nAllocation conflict for Employee: <<empEmail:ResourceAllocationDetails:item_id>> as the employee is not available.<br/><br/> \r\nKindly update the allocations accordingly.<br/><br/> \r\nPlease <a href='<<BaseUrl:config>>project-details/<<pipelinecode##>>/<<jobcode##>>?tab=3'>click here</a> for the details.<br/><br/> \r\nRegards,<br/>RMS Team<br/><br/>**This is an autogenerated email.Kindly do not respond to this.**", "Requestor,Reviewer,AdditionalEl,AdditionalDelegate", "ALLOCATION_CONFLICT_NOTIFICATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 311L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Skill Assessment Review Updates", "Dear User,<br/><br/> \r\nYour skill self-assessment request for the following skill has been approved by your Supercoach \r\n<<updated_by>>. <br/><br/> \r\nSkill Name : <<skillname:UserSkill:item_id>><br/> \r\nSkill Proficiency Level : <<Proficiency:UserSkill:item_id>><br/><br/> \r\nPlease <a href='<<BaseUrl:config>>myskill'>click here</a> for the details.<br/><br/> \r\nRegards,<br/>RMS Team<br/><br/>**This is an autogenerated email.Kindly do not respond to this.**", "userskillemail", "SUPERCOACH_ACCEPTED_SKILL_ASSESSMENT_REQUEST" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 312L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Skill Assessment Review Updates", "Your Skill Review request has been actioned.", "userskillemail", "SUPERCOACH_ACCEPTED_SKILL_ASSESSMENT_REQUEST" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 313L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Allocation conflicts for <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>>", "Dear User,<br/><br/> \r\nAllocation conflicts in the project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> as Employee: <<empEmail:ResourceAllocationDetails:item_id>> is not available.<br/><br/> \r\nPlease <a href='<<BaseUrl:config>>project-details/<<pipelinecode##>>/<<jobcode##>>?tab=3'>click here</a> to review allocation detail and kindly update the allocations.<br/><br/> \r\nRegards,<br/>RMS Team<br/><br/>**This is an autogenerated email.Kindly do not respond to this.**", "Requestor,Reviewer,AdditionalEl,AdditionalDelegate", "EMPLOYEE_ABSCONDING_NOTIFCATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 314L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Allocation conflicts for <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>>", "Employee <<empEmail:ResourceAllocationDetails:item_id>> is not available on the allocated dates for the project <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>>. \r\nKindly update the allocations accordingly.", "Requestor,Reviewer,AdditionalEl,AdditionalDelegate", "EMPLOYEE_ABSCONDING_NOTIFCATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 315L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Allocation conflicts for <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>>", "Employee <<empEmail:ResourceAllocationDetails:item_id>> is not available on the allocated dates for the project <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>>. \r\nKindly update the allocations accordingly.", "Requestor,Reviewer,AdditionalEl,AdditionalDelegate", "ALLOCATION_CONFLICT_NOTIFICATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 316L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Project Updates", "Dear User, \r\n<br/><br/> \r\nThe following activities have been performed on the below mentioned Job / Pipeline (s): \r\n<br/> \r\n<<RequistionSummary:Allocation:PipelineCode|JobCode>> \r\n<br/> \r\n<<RequstionSummary:Requistion:PipelineCode|JobCode>> \r\n<br/> \r\n<<AllocationSummary:AllocationPublished:PipelineCode|JobCode>> \r\n<br/> \r\n<<MarketPlaceSummary:MarketPlace:PipelineCode|JobCode>> \r\n<br/> \r\nPlease <a href='<<BaseUrl:config>>projects'>click here </a> to view the details of project(s). \r\n<br/> \r\n<br/> \r\nRegards,<br/> \r\nRMS Team<br/><br/>** This is an autogenerated email. Kindly do not respond to this.**", "ResourceRequestor,Reviewer,AdditionalEl,AdditionalDelegate", "ALLOCATION_SUMMARY_NOTIFICATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 317L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", " Additional EL assigned to Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>>", "Dear User, <br/><br/> \r\n<<userrequesting>> has assigned you Additional EL for project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>> on date <<userassignmentdate>> \r\n<br/> <br/> Please <a href='<<BaseUrl:config>>project-details/<<pipelinecode##>>/<<jobcode##>>?tab=0'>click here</a> for the details. You may contact <<userrequesting>> to discuss your responsibilities on the project in RMS. <br/><br/> \r\nRegards,<br/>RMS Team<br/><br/>** This is an autogenerated email. Kindly do not respond to this.**", "newadditionalels", "PROJECT_ADDITIONAL_EL_UPDATE_NOTIFICATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 318L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", " Additional EL assigned to Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>>", "You have been assigned as Additional EL for Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>> on date <<userassignmentdate>>", "newadditionalels", "PROJECT_ADDITIONAL_EL_UPDATE_NOTIFICATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 319L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Delegate assignment on Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>> ", "Dear User,<br/><br/> \r\nYou have been assigned as Delegate to <<delegateassignedby>> on Project <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>> on date <<delegateassignmentdate>>. \r\n<br/> <br/> Please <a href='<<BaseUrl:config>>project-details/<<pipelinecode##>>/<<jobcode##>>?tab=0'>click here</a> for the details.You may contact <<delegateassignedby>> to discuss your responsibilities on the project in RMS. <br/><br/> \r\nRegards,<br/>RMS Team<br/><br/>** This is an autogenerated email. Kindly do not respond to this.**", "Delegate", "PROJECT_DELEGATE_UPDATE_NOTIFICATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 320L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Delegate assignment on Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>> ", " \r\nYou have been assigned as Delegate for <<delegateassignedby>> in Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>> on date <<delegateassignmentdate>>.", "Delegate", "PROJECT_DELEGATE_UPDATE_NOTIFICATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 321L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Additional Delegate assigned to Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>> ", "Dear User,<br/><br/> \r\n<<userrequesting>> has assigned you as Additional Delegate to the Additional EL for Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>> on date <<userassignmentdate>> \r\n<br/> <br/> Please <a href='<<BaseUrl:config>>project-details/<<pipelinecode##>>/<<jobcode##>>?tab=0'>click here</a> for the details. You may contact <<userrequesting>> to discuss your responsibilities on the project in RMS. <br/> <br/> \r\nRegards,<br/>RMS Team<br/><br/>** This is an autogenerated email. Kindly do not respond to this.**", "newadditionaldelegates", "PROJECT_ADDITIONAL_DELEGATE_UPDATE_NOTIFICATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 322L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Additional Delegate assigned to Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>>", " \r\nYou have been assigned as Additional Delegate for <<userrequesting>> for Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>>.  ", "newadditionaldelegates", "PROJECT_ADDITIONAL_DELEGATE_UPDATE_NOTIFICATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 323L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Skill Assessment Review Updates", "Your Skill Review request has been actioned.", "userskillemail", "SUPERCOACH_REJECTED_SKILL_ASSESSMENT_REQUEST" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 324L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Skill Assessment Review Updates", "Dear User, <br/><br/> \r\nYour skill assessment approval has been declined by your Supercoach <<updated_by>>. <br/><br/> \r\nSkill Name : <<skillname:UserSkill:item_id>><br/> \r\nSkill Proficiency Level : <<Proficiency:UserSkill:item_id>><br/> \r\nReason : <<rejection_remark>> <br/><br/> \r\nPlease <a href='<<BaseUrl:config>>myskill'>click here</a> for the details and reach out to your Supercoach for any further clarifications.<br/><br/> \r\nRegards,<br/>RMS Team<br/><br/>** This is an autogenerated email. Kindly do not respond to this.**", "userskillemail", "SUPERCOACH_REJECTED_SKILL_ASSESSMENT_REQUEST" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 400L,
                columns: new[] { "cc", "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "", "Notification", "PUSH", "Notification", "Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>> Allocations Update.", "Project <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>> end date has been updated to <<enddate>>. Please take necessary actions.", "ResourceRequestor,Reviewer,AdditionalEl,AdditionalDelegate,Delegate", "PROJECT_END_DATE_UPDATE_NOTIFICATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 401L,
                columns: new[] { "cc", "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "", "Notification", "EMAIL", "Notification", "Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>> end date update.", "Dear User,<br/><br/> \r\nProject <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>> end date has been updated to <<enddate>>.Please review allocations made to the project and take any necessary actions. \r\n<br/><br/> \r\nPlease <a href='<<BaseUrl:config>>project-details/<<pipelinecode##>>/<<jobcode##>>?tab=0'>click here</a> for the details. \r\n<br/><br/> \r\nRegards,<br/>RMS Team<br/><br/>**This is an autogenerated email.Kindly do not respond to this.* *", "ResourceRequestor,Reviewer,AdditionalEl,AdditionalDelegate,Delegate", "PROJECT_END_DATE_UPDATE_NOTIFICATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 402L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Project: <<projectname>> Allocation Update", "Dear User,<br/><br/> \r\nYour Allocation on the Project: <<projectname>> has been moved from Project Code <<oldprojectcode>> to new Project Code <<newprojectcode>>.<br/><br/> \r\nPlease <a href='<<BaseUrl:config>>project-details/<<newpipelinecode##>>/<<newjobcode##>>?tab=0'>click here<a/> for the details.<br/><br/> \r\nRegards,<br/>RMS Team<br/><br/>** This is an autogenerated email. Kindly do not respond to this.**", "employee_jobcode_change", "PROJECT_JOB_CODE_CHANGE_INFORMATION_TO_EMPLOYEE" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 403L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Project: <<projectname>> Allocation Update", "Project: <<projectname>> allocations have been moved to new project code", "employee_jobcode_change", "PROJECT_JOB_CODE_CHANGE_INFORMATION_TO_EMPLOYEE" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 404L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "You have been successfully  allocated  to the project <<ProjectName:ResourceAllocationDetails:item_id>> on <<startdate:ResourceAllocationDetails:item_id>>.", "employee_name_allocation_wf", "NOTIFICATION_TO_EMPLOYEE_AFTER_EMPLOYEE_ACCEPTS" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 405L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "Dear User,<br/><br/> \r\nYou have been allocated to the Project: <<ProjectName:ResourceAllocationDetails:item_id>>  on <<startdate:ResourceAllocationDetails:item_id>>.<br/><br/> \r\nPlease <a href='<<BaseUrl:config>>project-details/<<pipelinecode##>>/<<jobcode##>>?tab=3'>click here</a> for the details.<br/><br/> \r\nRegards,<br/>RMS Team<br/><br/>**This is an autogenerated email.Kindly do not respond to this.**", "employee_name_allocation_wf", "NOTIFICATION_TO_EMPLOYEE_AFTER_EMPLOYEE_ACCEPTS" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 406L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", " \r\nYour allocation to the project <<ProjectName:ResourceAllocationDetails:item_id>> has been cancelled. Allocation Rejection request has been auto approved by the system.", "employee_name_allocation_wf", "NOTIFICATION_TO_EMPLOYEE_REQUESTOR_AUTO_APPROVED_REJECTION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 407L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", " \r\nDear User,<br/><br/> \r\nResource allocation for employee <<empEmail:ResourceAllocationDetails:item_id>> for Project: <<ProjectName:ResourceAllocationDetails:item_id>> has been cancelled, as the resource did not accept the allocation.<br/><br/> \r\nRequest you to allocate another resource for the Requisition.<br/><br/> \r\nRegards,<br/>RMS Team<br/><br/>** This is an autogenerated email. Kindly do not respond to this.**", "employee_name_allocation_wf", "NOTIFICATION_TO_EMPLOYEE_REQUESTOR_AUTO_APPROVED_REJECTION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 408L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Project: <<projectname>> Markeplace Interest Submitted", "You have received an interest for the Project: <<projectname>> in Marketplace. Total <<noofinterested>> likes received.", "ResourceRequestor,Delegate", "NOTIFICATION_FOR_INTEREST_IN_MARKETPLACE_AGAINST_THEIR_PROJECT" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 409L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "PUSH_NOTIFICATION_FOR_ALLOCATION_OF_RESOURCE_TO_PROJECT_REVIEWER_CONFIG_OFF", "Resources have been assigned to the project <<ProjectName:ResourceAllocationDetails:item_id>> on <<created_at>>. ", "resource_requestor_allocation_wf,Reviewer", "PUSH_NOTIFICATION_FOR_ALLOCATION_OF_RESOURCE_TO_PROJECT_REVIEWER_CONFIG_OFF" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 410L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "PUSH_NOTIFICATION_TO_REQUESTOR_AFTER_REVIEWER_ACCEPTS", "Resource Allocation Request for Project: <<ProjectName:ResourceAllocationDetails:item_id>>  have been approved by reviewer <<updated_by>>. ", "resource_requestor_allocation_wf", "PUSH_NOTIFICATION_TO_REQUESTOR_AFTER_REVIEWER_ACCEPTS" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 411L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "PUSH_NOTIFICATION_TO_REQUESTOR_ONCE_APPROVED_BY_EMPLOYEE", "Resource Allocation Request for Project: <<ProjectName:ResourceAllocationDetails:item_id>> have been confirmed by employee <<empEmail:ResourceAllocationDetails:item_id>>. ", "resource_requestor_allocation_wf,Reviewer", "PUSH_NOTIFICATION_TO_REQUESTOR_ONCE_APPROVED_BY_EMPLOYEE" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 412L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "PUSH_NOTIFICATION_TO_REQUESTOR_ONCE_WITHDRAWAL_BY_EMPLOYEE", "Resource Allocation Request for Project: <<ProjectName:ResourceAllocationDetails:item_id>> have been confirmed by employee <<empEmail:ResourceAllocationDetails:item_id>>. ", "resource_requestor_allocation_wf,Reviewer", "PUSH_NOTIFICATION_TO_REQUESTOR_ONCE_WITHDRAWAL_BY_EMPLOYEE" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 413L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "PUSH_NOTIFICATION_TO_REQUESTOR_REVIEWER_ACCEPTS_EMPLOYEE_UPDATE", "Allocation of <<empEmail:ResourceAllocationDetails:item_id>>, <<designation:ResourceAllocationDetails:item_id>> , <<grade:ResourceAllocationDetails:item_id>> on the Project: <<ProjectName:ResourceAllocationDetails:item_id>> has been updated. ", "resource_requestor_allocation_wf", "PUSH_NOTIFICATION_TO_REQUESTOR_REVIEWER_ACCEPTS_EMPLOYEE_UPDATE" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 414L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "template", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "PUSH_NOTIFICATION_TO_REQUESTOR_AND_REVIEWER_AFTER_EMPLOYEE_UPDATE", "Allocation of <<empEmail:ResourceAllocationDetails:item_id>>, <<designation:ResourceAllocationDetails:item_id>> , <<grade:ResourceAllocationDetails:item_id>> on the Project: <<ProjectName:ResourceAllocationDetails:item_id>> has been updated. ", "resource_requestor_allocation_wf,Reviewer", "PUSH_NOTIFICATION_TO_REQUESTOR_AND_REVIEWER_AFTER_EMPLOYEE_UPDATE" });
        }
    }
}
