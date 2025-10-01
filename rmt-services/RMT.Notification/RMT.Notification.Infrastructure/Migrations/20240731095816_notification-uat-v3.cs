using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RMT.Notification.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class notificationuatv3 : Migration
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
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "employee_name_allocation_wf", "NOTIFICATION_TO_EMPLOYEE_FOR_HIS_ALLOCATION_TASK_TO_PROJECT" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 43L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "employee_name_allocation_wf", "NOTIFICATION_TO_EMPLOYEE_FOR_HIS_ALLOCATION_TASK_TO_PROJECT" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 139L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "employee_name_allocation_wf", "NOTIFICATION_FOR_ALLOCATION_UPDATE_TO_EMPLOYEE" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 140L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "employee_name_allocation_wf", "NOTIFICATION_FOR_ALLOCATION_UPDATE_TO_EMPLOYEE" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 141L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "resource_requestor_allocation_wf,Reviewer", "EMPLOYEE_ALLOCATION_REJECTED_BY_REVIEWER_NOTIFICATION_TO_RESOURCE_REQUESTOR" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 142L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "resource_requestor_allocation_wf,Reviewer", "EMPLOYEE_ALLOCATION_REJECTED_BY_REVIEWER_NOTIFICATION_TO_RESOURCE_REQUESTOR" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 143L,
                columns: new[] { "cc", "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "", "Notification", "EMAIL", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "resource_requestor_allocation_wf,Reviewer", "NOTIFICATION_TO_RESOURCE_REQUESTOR_AND_REVIEWER_ON_EMPLOYEE_REJECTION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 144L,
                columns: new[] { "cc", "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "", "Notification", "PUSH", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "resource_requestor_allocation_wf,Reviewer", "NOTIFICATION_TO_RESOURCE_REQUESTOR_AND_REVIEWER_ON_EMPLOYEE_REJECTION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 145L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "resource_requestor_allocation_wf,Reviewer", "NOTIFICATION_TO_RESOURCE_REQUESTOR_AND_REVIEWER_ON_EMPLOYEE_REJECTION_REQUESTOR_CONFIG_IS_ON" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 146L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "resource_requestor_allocation_wf,Reviewer", "NOTIFICATION_TO_RESOURCE_REQUESTOR_AND_REVIEWER_ON_EMPLOYEE_REJECTION_REQUESTOR_CONFIG_IS_ON" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 147L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "employee_name_allocation_wf", "NOTIFICATION_TO_EMPLOYEE_AFTER_REVIEWER_ACCEPTS" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 148L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "employee_name_allocation_wf", "NOTIFICATION_TO_EMPLOYEE_AFTER_REVIEWER_ACCEPTS" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 149L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "employee_name_allocation_wf", "NOTIFICATION_TO_EMPLOYEE_AUTO_APPROVED_AFTER_EMPLOYEE_DUE_DATE_CROSS" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 150L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "employee_name_allocation_wf", "NOTIFICATION_TO_EMPLOYEE_AUTO_APPROVED_AFTER_EMPLOYEE_DUE_DATE_CROSS" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 151L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "employee_name_allocation_wf", "NOTIFICATION_TO_EMPLOYEE_WITHDRAWS_REJECTION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 152L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "employee_name_allocation_wf", "NOTIFICATION_TO_EMPLOYEE_WITHDRAWS_REJECTION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 153L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "employee_name_allocation_wf", "NOTIFICATION_TO_EMPLOYEE_REQUESTOR_APPROVED_REJECTION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 154L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "employee_name_allocation_wf", "NOTIFICATION_TO_EMPLOYEE_REQUESTOR_APPROVED_REJECTION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 155L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> Roll Forward Update", "Requestor,Delegate,Reviewer,AdditionalEl,AdditionalDelegate", "ROLL_OVER_ALLOCATED_EMPLOYEES_AVAILABLE_FOR_REVISED_DATES_ALLOCATION_SHIFTED_NEW_WF_TRIGGERED_NOTIFICATION_TO_REQUESTOR_AND_REVIEWER" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 156L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> Roll Forward Update", "Requestor,Delegate,Reviewer,AdditionalEl,AdditionalDelegate", "ROLL_OVER_ALLOCATED_EMPLOYEES_AVAILABLE_FOR_REVISED_DATES_ALLOCATION_SHIFTED_NEW_WF_TRIGGERED_NOTIFICATION_TO_REQUESTOR_AND_REVIEWER" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 169L,
                columns: new[] { "cc", "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "", "Notification", "EMAIL", "Notification", "Project Marketplace Updates", "Reviewer,ResourceRequestor,Delegate", "PROJECT_MOVED_TO_MARKETPLACE_SUMMARY_EMAIL" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 170L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "New Projects in the Marketplace", "employee", "CONSOLIDATED_MAIL_TO_EMPLOYEE_FOR_PROJECT_LISTING_IN_MARKETPLACE" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 171L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "RMS Tasks pending for your action", "employee", "RMS_ALLOCATION_AND_SKILL_TASKS_PENDING_FOR_ACTION_SUMMARY_MAIL" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 172L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "RMS Tasks pending for your action", "employee", "ADDITION_OF_NEW_PROJECT_NOTIFICATION_TO_RESOURCE_REQUESTOR" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 263L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>> Allocation Update", "employee_name_release_resource,ResourceRequestor,Reviewer,AdditionalEl,Delegate,AdditionalDelegate", "NOTIFICATION_TO_EMPLOYEE_AS_RELEASED_FROM_PROJECT" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 264L,
                columns: new[] { "cc", "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "ResourceRequestor,Reviewer,AdditionalEl,Delegate,AdditionalDelegate", "Notification", "EMAIL", "Notification", "Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>> Allocation Update", "employee_name_release_resource", "NOTIFICATION_TO_EMPLOYEE_AS_RELEASED_FROM_PROJECT" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 267L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>> Roll Forward Update.", "Reviewer", "NOTIFICATION_TO_REVIEWER_ALLOCATIONS_ARE_SHIFTED_AND_NEW_WORKFLOW_ARE_TRIGGERED" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 268L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>> Roll Forward Update.", "Reviewer", "NOTIFICATION_TO_REVIEWER_ALLOCATIONS_ARE_SHIFTED_AND_NEW_WORKFLOW_ARE_TRIGGERED" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 269L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>> Name Marketplace Summary", "ResourceRequestor,Delegate", "NOTIFICATION_TO_RESOURCE_REQUESTOR_AND_DELEGATE_FOR_REMOVAL_OF_PROJECT_FROM_MP" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 270L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>> Name Marketplace Summary", "ResourceRequestor,Delegate", "NOTIFICATION_TO_RESOURCE_REQUESTOR_AND_DELEGATE_FOR_REMOVAL_OF_PROJECT_FROM_MP" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 271L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> Roll Forward Update - Allocation Release", "employee_name_roll_over_draft", "ROLL_OVER_ALLOCATION_IN_DRAFT_DUE_TO_UNAVAILABLITY" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 272L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> Roll Forward Update - Allocation Release", "employee_name_roll_over_draft", "ROLL_OVER_ALLOCATION_IN_DRAFT_DUE_TO_UNAVAILABLITY" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 273L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> Roll Forward Update - Allocation Release", "employee_name_roll_over_terminated", "ROLL_OVER_ALLOCATION_TERMINATED_DUE_TO_UNAVAILABLITY" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 274L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> Roll Forward Update - Allocation Release", "employee_name_roll_over_terminated", "ROLL_OVER_ALLOCATION_TERMINATED_DUE_TO_UNAVAILABLITY" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 276L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "resource_requestor_allocation_wf", "REVIEWER_ACCEPTED_EMPLOYEE_REJECTION_RESPONSE" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 277L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "resource_requestor_allocation_wf", "REVIEWER_ACCEPTED_EMPLOYEE_REJECTION_RESPONSE" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 293L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update ", "employee_name_allocation_wf", "NOTIFICATION_TO_EMPLOYEE_REVIEWER_APPROVED_EMPLOYEE_REJECTION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 295L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update ", "employee_name_allocation_wf", "NOTIFICATION_TO_EMPLOYEE_REVIEWER_APPROVED_EMPLOYEE_REJECTION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 296L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Project: <<projectname>> Allocation Update", "previousrolesemails", "JOB_CODE_UPDATE_TO_NEW_CODE" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 297L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Project: <<projectname>> Allocation Update", "previousrolesemails", "JOB_CODE_UPDATE_TO_NEW_CODE" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 298L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Additional Delegate assigned to Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>>", "employee_name_additional_delegate", "ASSIGNMENT_OF_ADDITIONAL_DELEGATE_BY_ADDITIONAL_EL" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 299L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Additional Delegate assigned to Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>>", "employee_name_additional_delegate", "ASSIGNMENT_OF_ADDITIONAL_DELEGATE_BY_ADDITIONAL_EL" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 300L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "RMS Admin Role assigned", "employee_name_assign_as_admin", "ADMIN_ASSIGNED_BY_SYSTEM_ADMIN" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 301L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "RMS Admin Role assigned", "employee_name_assign_as_admin", "ADMIN_ASSIGNED_BY_SYSTEM_ADMIN" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 302L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Project:<<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> - <<pipelineStatus :GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>>", "employee_name_allocation", "PROJECT_SUSPENSION_ALLOCATION_RELEASE_NOTIFICATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 303L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Project:<<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> - Suspended", "Requestor,Reviewer,AdditionalEl,AdditionalDelegate", "PROJECT_SUSPENSION_NOTIFICATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 304L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Project:<<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> - Suspended", "Requestor,Reviewer,AdditionalEl,AdditionalDelegate", "PROJECT_SUSPENSION_NOTIFICATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 305L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Project:<<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> - <<pipelineStatus :GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>>", "employee_name_allocation", "PROJECT_SUSPENSION_ALLOCATION_RELEASE_NOTIFICATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 306L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Allocation conflicts for <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>>", "Requestor,Reviewer,AdditionalEl,AdditionalDelegate", "EMPLOYEE_LEAVE_NOTIFCATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 307L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> Budget Updates", "Requestor,Reviewer", "PROJECT_BUDGET_NOTIFICATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 308L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> Budget Updates", "Requestor,Reviewer", "PROJECT_BUDGET_NOTIFICATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 309L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Allocation conflicts for <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>>", "Requestor,Reviewer,AdditionalEl,AdditionalDelegate", "EMPLOYEE_LEAVE_NOTIFCATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 310L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Allocation conflicts for <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>>", "Requestor,Reviewer,AdditionalEl,AdditionalDelegate", "ALLOCATION_CONFLICT_NOTIFICATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 311L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Skill Assessment Review Updates", "userskillemail", "SUPERCOACH_ACCEPTED_SKILL_ASSESSMENT_REQUEST" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 312L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Skill Assessment Review Updates", "userskillemail", "SUPERCOACH_ACCEPTED_SKILL_ASSESSMENT_REQUEST" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 313L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Allocation conflicts for <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>>", "Requestor,Reviewer,AdditionalEl,AdditionalDelegate", "EMPLOYEE_ABSCONDING_NOTIFCATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 314L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Allocation conflicts for <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>>", "Requestor,Reviewer,AdditionalEl,AdditionalDelegate", "EMPLOYEE_ABSCONDING_NOTIFCATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 315L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Allocation conflicts for <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>>", "Requestor,Reviewer,AdditionalEl,AdditionalDelegate", "ALLOCATION_CONFLICT_NOTIFICATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 316L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Project Updates", "ResourceRequestor,Reviewer,AdditionalEl,AdditionalDelegate", "ALLOCATION_SUMMARY_NOTIFICATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 317L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", " Additional EL assigned to Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>>", "newadditionalels", "PROJECT_ADDITIONAL_EL_UPDATE_NOTIFICATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 318L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", " Additional EL assigned to Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>>", "newadditionalels", "PROJECT_ADDITIONAL_EL_UPDATE_NOTIFICATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 319L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Delegate assignment on Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>> ", "Delegate", "PROJECT_DELEGATE_UPDATE_NOTIFICATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 320L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Delegate assignment on Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>> ", "Delegate", "PROJECT_DELEGATE_UPDATE_NOTIFICATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 321L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Additional Delegate assigned to Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>> ", "newadditionaldelegates", "PROJECT_ADDITIONAL_DELEGATE_UPDATE_NOTIFICATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 322L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Additional Delegate assigned to Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>>", "newadditionaldelegates", "PROJECT_ADDITIONAL_DELEGATE_UPDATE_NOTIFICATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 323L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Skill Assessment Review Updates", "userskillemail", "SUPERCOACH_REJECTED_SKILL_ASSESSMENT_REQUEST" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 324L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Skill Assessment Review Updates", "userskillemail", "SUPERCOACH_REJECTED_SKILL_ASSESSMENT_REQUEST" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 400L,
                columns: new[] { "cc", "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "", "Notification", "PUSH", "Notification", "Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>> Allocations Update.", "ResourceRequestor,Reviewer,AdditionalEl,AdditionalDelegate,Delegate", "PROJECT_END_DATE_UPDATE_NOTIFICATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 401L,
                columns: new[] { "cc", "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "", "Notification", "EMAIL", "Notification", "Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>> end date update.", "ResourceRequestor,Reviewer,AdditionalEl,AdditionalDelegate,Delegate", "PROJECT_END_DATE_UPDATE_NOTIFICATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 402L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Project: <<projectname>> Allocation Update", "employee_jobcode_change", "PROJECT_JOB_CODE_CHANGE_INFORMATION_TO_EMPLOYEE" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 403L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Project: <<projectname>> Allocation Update", "employee_jobcode_change", "PROJECT_JOB_CODE_CHANGE_INFORMATION_TO_EMPLOYEE" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 404L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "employee_name_allocation_wf", "NOTIFICATION_TO_EMPLOYEE_AFTER_EMPLOYEE_ACCEPTS" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 405L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "employee_name_allocation_wf", "NOTIFICATION_TO_EMPLOYEE_AFTER_EMPLOYEE_ACCEPTS" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 406L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "employee_name_allocation_wf", "NOTIFICATION_TO_EMPLOYEE_REQUESTOR_AUTO_APPROVED_REJECTION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 407L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "Notification", "EMAIL", "Notification", "Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "employee_name_allocation_wf", "NOTIFICATION_TO_EMPLOYEE_REQUESTOR_AUTO_APPROVED_REJECTION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 408L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "Project: <<projectname>> Markeplace Interest Submitted", "ResourceRequestor,Delegate", "NOTIFICATION_FOR_INTEREST_IN_MARKETPLACE_AGAINST_THEIR_PROJECT" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 409L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "PUSH_NOTIFICATION_FOR_ALLOCATION_OF_RESOURCE_TO_PROJECT_REVIEWER_CONFIG_OFF", "resource_requestor_allocation_wf,Reviewer", "PUSH_NOTIFICATION_FOR_ALLOCATION_OF_RESOURCE_TO_PROJECT_REVIEWER_CONFIG_OFF" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 410L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "PUSH_NOTIFICATION_TO_REQUESTOR_AFTER_REVIEWER_ACCEPTS", "resource_requestor_allocation_wf", "PUSH_NOTIFICATION_TO_REQUESTOR_AFTER_REVIEWER_ACCEPTS" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 411L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "PUSH_NOTIFICATION_TO_REQUESTOR_ONCE_APPROVED_BY_EMPLOYEE", "resource_requestor_allocation_wf,Reviewer", "PUSH_NOTIFICATION_TO_REQUESTOR_ONCE_APPROVED_BY_EMPLOYEE" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 412L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "PUSH_NOTIFICATION_TO_REQUESTOR_ONCE_WITHDRAWAL_BY_EMPLOYEE", "resource_requestor_allocation_wf,Reviewer", "PUSH_NOTIFICATION_TO_REQUESTOR_ONCE_WITHDRAWAL_BY_EMPLOYEE" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 413L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "PUSH_NOTIFICATION_TO_REQUESTOR_REVIEWER_ACCEPTS_EMPLOYEE_UPDATE", "resource_requestor_allocation_wf", "PUSH_NOTIFICATION_TO_REQUESTOR_REVIEWER_ACCEPTS_EMPLOYEE_UPDATE" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 414L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "Notification", "PUSH", "Notification", "PUSH_NOTIFICATION_TO_REQUESTOR_AND_REVIEWER_AFTER_EMPLOYEE_UPDATE", "resource_requestor_allocation_wf,Reviewer", "PUSH_NOTIFICATION_TO_REQUESTOR_AND_REVIEWER_AFTER_EMPLOYEE_UPDATE" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 1L,
                columns: new[] { "Link", "Module", "SubModule" },
                values: new object[] { "$/myskill", "$Notification", "$Skill" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 2L,
                columns: new[] { "Link", "Module", "SubModule" },
                values: new object[] { "$/skill-review", "$Notification", "$Skill" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 3L,
                columns: new[] { "Link", "Module", "SubModule" },
                values: new object[] { "$/searchskill", "$Notification", "$Skill" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 4L,
                columns: new[] { "Link", "Module", "SubModule" },
                values: new object[] { "$/skillmaster", "$Notification", "$Skill" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 5L,
                columns: new[] { "Link", "Module", "SubModule" },
                values: new object[] { "$/projects", "$Notification", "$Skill" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 6L,
                columns: new[] { "Link", "Module", "SubModule" },
                values: new object[] { "$/projects", "$Notification", "$Project" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 7L,
                columns: new[] { "Link", "Module", "SubModule" },
                values: new object[] { "$/marketplace", "$Notification", "$Project" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 8L,
                columns: new[] { "Link", "Module", "SubModule" },
                values: new object[] { "$/my-preference", "$Notification", "$Manage" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 9L,
                columns: new[] { "Link", "Module", "SubModule" },
                values: new object[] { "$/my-preference", "$Notification", "$Manage" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 10L,
                columns: new[] { "Link", "Module", "SubModule" },
                values: new object[] { "$/my-calender", "$Notification", "$Manage" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 11L,
                columns: new[] { "Link", "Module", "SubModule" },
                values: new object[] { "$/roles-permission", "$Notification", "$User" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 12L,
                columns: new[] { "Link", "Module", "SubModule" },
                values: new object[] { "$/myapproval", "$Notification", "$User" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 13L,
                columns: new[] { "Link", "Module", "SubModule" },
                values: new object[] { "$/project-details/<<pipelinecode##>>/<<jobcode##>>?tab=0", "$Notification", "$Details" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 14L,
                columns: new[] { "Link", "Module", "SubModule" },
                values: new object[] { "$/project-details/<<pipelinecode##>>/<<jobcode##>>?tab=1", "$Notification", "$Details" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 15L,
                columns: new[] { "Link", "Module", "SubModule" },
                values: new object[] { "$/project-details/<<pipelinecode##>>/<<jobcode##>>?tab=2", "$Notification", "$Details" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 16L,
                columns: new[] { "Link", "Module", "SubModule" },
                values: new object[] { "$/project-details/<<pipelinecode##>>/<<jobcode##>>?tab=3", "$Notification", "$Details" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 17L,
                columns: new[] { "Link", "Module", "SubModule" },
                values: new object[] { "$/project-details/<<pipelinecode##>>/<<jobcode##>>?tab=4", "$Notification", "$Details" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 18L,
                columns: new[] { "Link", "Module", "SubModule" },
                values: new object[] { "$/project-details/<<pipelinecode##>>/<<jobcode##>>?tab=5", "$Notification", "$Details" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplateLinks",
                keyColumn: "Id",
                keyValue: 19L,
                columns: new[] { "Link", "Module", "SubModule" },
                values: new object[] { "$/project-details/<<newpipelinecode##>>/<<newjobcode##>>?tab=0", "$Notification", "$Details" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 42L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$Notification", "$PUSH", "$Notification", "$Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "$employee_name_allocation_wf", "$NOTIFICATION_TO_EMPLOYEE_FOR_HIS_ALLOCATION_TASK_TO_PROJECT" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 43L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$Notification", "$EMAIL", "$Notification", "$Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "$employee_name_allocation_wf", "$NOTIFICATION_TO_EMPLOYEE_FOR_HIS_ALLOCATION_TASK_TO_PROJECT" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 139L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$Notification", "$EMAIL", "$Notification", "$Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "$employee_name_allocation_wf", "$NOTIFICATION_FOR_ALLOCATION_UPDATE_TO_EMPLOYEE" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 140L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$Notification", "$PUSH", "$Notification", "$Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "$employee_name_allocation_wf", "$NOTIFICATION_FOR_ALLOCATION_UPDATE_TO_EMPLOYEE" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 141L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$Notification", "$EMAIL", "$Notification", "$Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "$resource_requestor_allocation_wf,Reviewer", "$EMPLOYEE_ALLOCATION_REJECTED_BY_REVIEWER_NOTIFICATION_TO_RESOURCE_REQUESTOR" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 142L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$Notification", "$PUSH", "$Notification", "$Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "$resource_requestor_allocation_wf,Reviewer", "$EMPLOYEE_ALLOCATION_REJECTED_BY_REVIEWER_NOTIFICATION_TO_RESOURCE_REQUESTOR" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 143L,
                columns: new[] { "cc", "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$", "$Notification", "$EMAIL", "$Notification", "$Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "$resource_requestor_allocation_wf,Reviewer", "$NOTIFICATION_TO_RESOURCE_REQUESTOR_AND_REVIEWER_ON_EMPLOYEE_REJECTION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 144L,
                columns: new[] { "cc", "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$", "$Notification", "$PUSH", "$Notification", "$Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "$resource_requestor_allocation_wf,Reviewer", "$NOTIFICATION_TO_RESOURCE_REQUESTOR_AND_REVIEWER_ON_EMPLOYEE_REJECTION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 145L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$Notification", "$EMAIL", "$Notification", "$Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "$resource_requestor_allocation_wf,Reviewer", "$NOTIFICATION_TO_RESOURCE_REQUESTOR_AND_REVIEWER_ON_EMPLOYEE_REJECTION_REQUESTOR_CONFIG_IS_ON" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 146L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$Notification", "$PUSH", "$Notification", "$Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "$resource_requestor_allocation_wf,Reviewer", "$NOTIFICATION_TO_RESOURCE_REQUESTOR_AND_REVIEWER_ON_EMPLOYEE_REJECTION_REQUESTOR_CONFIG_IS_ON" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 147L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$Notification", "$EMAIL", "$Notification", "$Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "$employee_name_allocation_wf", "$NOTIFICATION_TO_EMPLOYEE_AFTER_REVIEWER_ACCEPTS" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 148L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$Notification", "$PUSH", "$Notification", "$Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "$employee_name_allocation_wf", "$NOTIFICATION_TO_EMPLOYEE_AFTER_REVIEWER_ACCEPTS" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 149L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$Notification", "$EMAIL", "$Notification", "$Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "$employee_name_allocation_wf", "$NOTIFICATION_TO_EMPLOYEE_AUTO_APPROVED_AFTER_EMPLOYEE_DUE_DATE_CROSS" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 150L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$Notification", "$PUSH", "$Notification", "$Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "$employee_name_allocation_wf", "$NOTIFICATION_TO_EMPLOYEE_AUTO_APPROVED_AFTER_EMPLOYEE_DUE_DATE_CROSS" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 151L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$Notification", "$EMAIL", "$Notification", "$Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "$employee_name_allocation_wf", "$NOTIFICATION_TO_EMPLOYEE_WITHDRAWS_REJECTION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 152L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$Notification", "$PUSH", "$Notification", "$Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "$employee_name_allocation_wf", "$NOTIFICATION_TO_EMPLOYEE_WITHDRAWS_REJECTION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 153L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$Notification", "$EMAIL", "$Notification", "$Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "$employee_name_allocation_wf", "$NOTIFICATION_TO_EMPLOYEE_REQUESTOR_APPROVED_REJECTION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 154L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$Notification", "$PUSH", "$Notification", "$Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "$employee_name_allocation_wf", "$NOTIFICATION_TO_EMPLOYEE_REQUESTOR_APPROVED_REJECTION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 155L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$Notification", "$EMAIL", "$Notification", "$Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> Roll Forward Update", "$Requestor,Delegate,Reviewer,AdditionalEl,AdditionalDelegate", "$ROLL_OVER_ALLOCATED_EMPLOYEES_AVAILABLE_FOR_REVISED_DATES_ALLOCATION_SHIFTED_NEW_WF_TRIGGERED_NOTIFICATION_TO_REQUESTOR_AND_REVIEWER" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 156L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$Notification", "$PUSH", "$Notification", "$Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> Roll Forward Update", "$Requestor,Delegate,Reviewer,AdditionalEl,AdditionalDelegate", "$ROLL_OVER_ALLOCATED_EMPLOYEES_AVAILABLE_FOR_REVISED_DATES_ALLOCATION_SHIFTED_NEW_WF_TRIGGERED_NOTIFICATION_TO_REQUESTOR_AND_REVIEWER" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 169L,
                columns: new[] { "cc", "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$", "$Notification", "$EMAIL", "$Notification", "$Project Marketplace Updates", "$Reviewer,ResourceRequestor,Delegate", "$PROJECT_MOVED_TO_MARKETPLACE_SUMMARY_EMAIL" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 170L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$Notification", "$EMAIL", "$Notification", "$New Projects in the Marketplace", "$employee", "$CONSOLIDATED_MAIL_TO_EMPLOYEE_FOR_PROJECT_LISTING_IN_MARKETPLACE" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 171L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$Notification", "$EMAIL", "$Notification", "$RMS Tasks pending for your action", "$employee", "$RMS_ALLOCATION_AND_SKILL_TASKS_PENDING_FOR_ACTION_SUMMARY_MAIL" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 172L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$Notification", "$EMAIL", "$Notification", "$RMS Tasks pending for your action", "$employee", "$ADDITION_OF_NEW_PROJECT_NOTIFICATION_TO_RESOURCE_REQUESTOR" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 263L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$Notification", "$PUSH", "$Notification", "$Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>> Allocation Update", "$employee_name_release_resource,ResourceRequestor,Reviewer,AdditionalEl,Delegate,AdditionalDelegate", "$NOTIFICATION_TO_EMPLOYEE_AS_RELEASED_FROM_PROJECT" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 264L,
                columns: new[] { "cc", "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$ResourceRequestor,Reviewer,AdditionalEl,Delegate,AdditionalDelegate", "$Notification", "$EMAIL", "$Notification", "$Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>> Allocation Update", "$employee_name_release_resource", "$NOTIFICATION_TO_EMPLOYEE_AS_RELEASED_FROM_PROJECT" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 267L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$Notification", "$EMAIL", "$Notification", "$Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>> Roll Forward Update.", "$Reviewer", "$NOTIFICATION_TO_REVIEWER_ALLOCATIONS_ARE_SHIFTED_AND_NEW_WORKFLOW_ARE_TRIGGERED" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 268L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$Notification", "$PUSH", "$Notification", "$Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>> Roll Forward Update.", "$Reviewer", "$NOTIFICATION_TO_REVIEWER_ALLOCATIONS_ARE_SHIFTED_AND_NEW_WORKFLOW_ARE_TRIGGERED" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 269L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$Notification", "$PUSH", "$Notification", "$Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>> Name Marketplace Summary", "$ResourceRequestor,Delegate", "$NOTIFICATION_TO_RESOURCE_REQUESTOR_AND_DELEGATE_FOR_REMOVAL_OF_PROJECT_FROM_MP" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 270L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$Notification", "$EMAIL", "$Notification", "$Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>> Name Marketplace Summary", "$ResourceRequestor,Delegate", "$NOTIFICATION_TO_RESOURCE_REQUESTOR_AND_DELEGATE_FOR_REMOVAL_OF_PROJECT_FROM_MP" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 271L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$Notification", "$EMAIL", "$Notification", "$Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> Roll Forward Update - Allocation Release", "$employee_name_roll_over_draft", "$ROLL_OVER_ALLOCATION_IN_DRAFT_DUE_TO_UNAVAILABLITY" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 272L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$Notification", "$PUSH", "$Notification", "$Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> Roll Forward Update - Allocation Release", "$employee_name_roll_over_draft", "$ROLL_OVER_ALLOCATION_IN_DRAFT_DUE_TO_UNAVAILABLITY" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 273L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$Notification", "$EMAIL", "$Notification", "$Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> Roll Forward Update - Allocation Release", "$employee_name_roll_over_terminated", "$ROLL_OVER_ALLOCATION_TERMINATED_DUE_TO_UNAVAILABLITY" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 274L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$Notification", "$PUSH", "$Notification", "$Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> Roll Forward Update - Allocation Release", "$employee_name_roll_over_terminated", "$ROLL_OVER_ALLOCATION_TERMINATED_DUE_TO_UNAVAILABLITY" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 276L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$Notification", "$PUSH", "$Notification", "$Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "$resource_requestor_allocation_wf", "$REVIEWER_ACCEPTED_EMPLOYEE_REJECTION_RESPONSE" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 277L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$Notification", "$EMAIL", "$Notification", "$Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "$resource_requestor_allocation_wf", "$REVIEWER_ACCEPTED_EMPLOYEE_REJECTION_RESPONSE" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 293L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$Notification", "$EMAIL", "$Notification", "$Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update ", "$employee_name_allocation_wf", "$NOTIFICATION_TO_EMPLOYEE_REVIEWER_APPROVED_EMPLOYEE_REJECTION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 295L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$Notification", "$PUSH", "$Notification", "$Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update ", "$employee_name_allocation_wf", "$NOTIFICATION_TO_EMPLOYEE_REVIEWER_APPROVED_EMPLOYEE_REJECTION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 296L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$Notification", "$PUSH", "$Notification", "$Project: <<projectname>> Allocation Update", "$previousrolesemails", "$JOB_CODE_UPDATE_TO_NEW_CODE" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 297L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$Notification", "$EMAIL", "$Notification", "$Project: <<projectname>> Allocation Update", "$previousrolesemails", "$JOB_CODE_UPDATE_TO_NEW_CODE" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 298L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$Notification", "$PUSH", "$Notification", "$Additional Delegate assigned to Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>>", "$employee_name_additional_delegate", "$ASSIGNMENT_OF_ADDITIONAL_DELEGATE_BY_ADDITIONAL_EL" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 299L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$Notification", "$EMAIL", "$Notification", "$Additional Delegate assigned to Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>>", "$employee_name_additional_delegate", "$ASSIGNMENT_OF_ADDITIONAL_DELEGATE_BY_ADDITIONAL_EL" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 300L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$Notification", "$PUSH", "$Notification", "$RMS Admin Role assigned", "$employee_name_assign_as_admin", "$ADMIN_ASSIGNED_BY_SYSTEM_ADMIN" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 301L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$Notification", "$EMAIL", "$Notification", "$RMS Admin Role assigned", "$employee_name_assign_as_admin", "$ADMIN_ASSIGNED_BY_SYSTEM_ADMIN" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 302L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$Notification", "$EMAIL", "$Notification", "$Project:<<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> - <<pipelineStatus :GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>>", "$employee_name_allocation", "$PROJECT_SUSPENSION_ALLOCATION_RELEASE_NOTIFICATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 303L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$Notification", "$EMAIL", "$Notification", "$Project:<<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> - Suspended", "$Requestor,Reviewer,AdditionalEl,AdditionalDelegate", "$PROJECT_SUSPENSION_NOTIFICATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 304L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$Notification", "$PUSH", "$Notification", "$Project:<<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> - Suspended", "$Requestor,Reviewer,AdditionalEl,AdditionalDelegate", "$PROJECT_SUSPENSION_NOTIFICATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 305L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$Notification", "$PUSH", "$Notification", "$Project:<<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> - <<pipelineStatus :GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>>", "$employee_name_allocation", "$PROJECT_SUSPENSION_ALLOCATION_RELEASE_NOTIFICATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 306L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$Notification", "$EMAIL", "$Notification", "$Allocation conflicts for <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>>", "$Requestor,Reviewer,AdditionalEl,AdditionalDelegate", "$EMPLOYEE_LEAVE_NOTIFCATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 307L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$Notification", "$EMAIL", "$Notification", "$Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> Budget Updates", "$Requestor,Reviewer", "$PROJECT_BUDGET_NOTIFICATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 308L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$Notification", "$PUSH", "$Notification", "$Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>> Budget Updates", "$Requestor,Reviewer", "$PROJECT_BUDGET_NOTIFICATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 309L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$Notification", "$PUSH", "$Notification", "$Allocation conflicts for <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>>", "$Requestor,Reviewer,AdditionalEl,AdditionalDelegate", "$EMPLOYEE_LEAVE_NOTIFCATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 310L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$Notification", "$EMAIL", "$Notification", "$Allocation conflicts for <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>>", "$Requestor,Reviewer,AdditionalEl,AdditionalDelegate", "$ALLOCATION_CONFLICT_NOTIFICATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 311L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$Notification", "$EMAIL", "$Notification", "$Skill Assessment Review Updates", "$userskillemail", "$SUPERCOACH_ACCEPTED_SKILL_ASSESSMENT_REQUEST" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 312L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$Notification", "$PUSH", "$Notification", "$Skill Assessment Review Updates", "$userskillemail", "$SUPERCOACH_ACCEPTED_SKILL_ASSESSMENT_REQUEST" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 313L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$Notification", "$EMAIL", "$Notification", "$Allocation conflicts for <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>>", "$Requestor,Reviewer,AdditionalEl,AdditionalDelegate", "$EMPLOYEE_ABSCONDING_NOTIFCATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 314L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$Notification", "$PUSH", "$Notification", "$Allocation conflicts for <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>>", "$Requestor,Reviewer,AdditionalEl,AdditionalDelegate", "$EMPLOYEE_ABSCONDING_NOTIFCATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 315L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$Notification", "$PUSH", "$Notification", "$Allocation conflicts for <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelineCode|jobCode>>", "$Requestor,Reviewer,AdditionalEl,AdditionalDelegate", "$ALLOCATION_CONFLICT_NOTIFICATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 316L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$Notification", "$EMAIL", "$Notification", "$Project Updates", "$ResourceRequestor,Reviewer,AdditionalEl,AdditionalDelegate", "$ALLOCATION_SUMMARY_NOTIFICATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 317L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$Notification", "$EMAIL", "$Notification", "$ Additional EL assigned to Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>>", "$newadditionalels", "$PROJECT_ADDITIONAL_EL_UPDATE_NOTIFICATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 318L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$Notification", "$PUSH", "$Notification", "$ Additional EL assigned to Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>>", "$newadditionalels", "$PROJECT_ADDITIONAL_EL_UPDATE_NOTIFICATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 319L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$Notification", "$EMAIL", "$Notification", "$Delegate assignment on Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>> ", "$Delegate", "$PROJECT_DELEGATE_UPDATE_NOTIFICATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 320L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$Notification", "$PUSH", "$Notification", "$Delegate assignment on Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>> ", "$Delegate", "$PROJECT_DELEGATE_UPDATE_NOTIFICATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 321L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$Notification", "$EMAIL", "$Notification", "$Additional Delegate assigned to Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>> ", "$newadditionaldelegates", "$PROJECT_ADDITIONAL_DELEGATE_UPDATE_NOTIFICATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 322L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$Notification", "$PUSH", "$Notification", "$Additional Delegate assigned to Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>>", "$newadditionaldelegates", "$PROJECT_ADDITIONAL_DELEGATE_UPDATE_NOTIFICATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 323L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$Notification", "$PUSH", "$Notification", "$Skill Assessment Review Updates", "$userskillemail", "$SUPERCOACH_REJECTED_SKILL_ASSESSMENT_REQUEST" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 324L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$Notification", "$EMAIL", "$Notification", "$Skill Assessment Review Updates", "$userskillemail", "$SUPERCOACH_REJECTED_SKILL_ASSESSMENT_REQUEST" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 400L,
                columns: new[] { "cc", "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$", "$Notification", "$PUSH", "$Notification", "$Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>> Allocations Update.", "$ResourceRequestor,Reviewer,AdditionalEl,AdditionalDelegate,Delegate", "$PROJECT_END_DATE_UPDATE_NOTIFICATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 401L,
                columns: new[] { "cc", "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$", "$Notification", "$EMAIL", "$Notification", "$Project: <<ProjectName:GetProjectDetailsByPipelineCodeAndJobCode:pipelinecode|jobcode>> end date update.", "$ResourceRequestor,Reviewer,AdditionalEl,AdditionalDelegate,Delegate", "$PROJECT_END_DATE_UPDATE_NOTIFICATION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 402L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$Notification", "$EMAIL", "$Notification", "$Project: <<projectname>> Allocation Update", "$employee_jobcode_change", "$PROJECT_JOB_CODE_CHANGE_INFORMATION_TO_EMPLOYEE" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 403L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$Notification", "$PUSH", "$Notification", "$Project: <<projectname>> Allocation Update", "$employee_jobcode_change", "$PROJECT_JOB_CODE_CHANGE_INFORMATION_TO_EMPLOYEE" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 404L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$Notification", "$PUSH", "$Notification", "$Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "$employee_name_allocation_wf", "$NOTIFICATION_TO_EMPLOYEE_AFTER_EMPLOYEE_ACCEPTS" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 405L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$Notification", "$EMAIL", "$Notification", "$Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "$employee_name_allocation_wf", "$NOTIFICATION_TO_EMPLOYEE_AFTER_EMPLOYEE_ACCEPTS" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 406L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$Notification", "$PUSH", "$Notification", "$Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "$employee_name_allocation_wf", "$NOTIFICATION_TO_EMPLOYEE_REQUESTOR_AUTO_APPROVED_REJECTION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 407L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$Notification", "$EMAIL", "$Notification", "$Project: <<ProjectName:ResourceAllocationDetails:item_id>> Allocations Update", "$employee_name_allocation_wf", "$NOTIFICATION_TO_EMPLOYEE_REQUESTOR_AUTO_APPROVED_REJECTION" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 408L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$Notification", "$PUSH", "$Notification", "$Project: <<projectname>> Markeplace Interest Submitted", "$ResourceRequestor,Delegate", "$NOTIFICATION_FOR_INTEREST_IN_MARKETPLACE_AGAINST_THEIR_PROJECT" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 409L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$Notification", "$PUSH", "$Notification", "$PUSH_NOTIFICATION_FOR_ALLOCATION_OF_RESOURCE_TO_PROJECT_REVIEWER_CONFIG_OFF", "$resource_requestor_allocation_wf,Reviewer", "$PUSH_NOTIFICATION_FOR_ALLOCATION_OF_RESOURCE_TO_PROJECT_REVIEWER_CONFIG_OFF" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 410L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$Notification", "$PUSH", "$Notification", "$PUSH_NOTIFICATION_TO_REQUESTOR_AFTER_REVIEWER_ACCEPTS", "$resource_requestor_allocation_wf", "$PUSH_NOTIFICATION_TO_REQUESTOR_AFTER_REVIEWER_ACCEPTS" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 411L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$Notification", "$PUSH", "$Notification", "$PUSH_NOTIFICATION_TO_REQUESTOR_ONCE_APPROVED_BY_EMPLOYEE", "$resource_requestor_allocation_wf,Reviewer", "$PUSH_NOTIFICATION_TO_REQUESTOR_ONCE_APPROVED_BY_EMPLOYEE" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 412L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$Notification", "$PUSH", "$Notification", "$PUSH_NOTIFICATION_TO_REQUESTOR_ONCE_WITHDRAWAL_BY_EMPLOYEE", "$resource_requestor_allocation_wf,Reviewer", "$PUSH_NOTIFICATION_TO_REQUESTOR_ONCE_WITHDRAWAL_BY_EMPLOYEE" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 413L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$Notification", "$PUSH", "$Notification", "$PUSH_NOTIFICATION_TO_REQUESTOR_REVIEWER_ACCEPTS_EMPLOYEE_UPDATE", "$resource_requestor_allocation_wf", "$PUSH_NOTIFICATION_TO_REQUESTOR_REVIEWER_ACCEPTS_EMPLOYEE_UPDATE" });

            migrationBuilder.UpdateData(
                table: "NotificationTemplates",
                keyColumn: "Id",
                keyValue: 414L,
                columns: new[] { "module", "notification_type", "sub_module", "subject", "to", "type" },
                values: new object[] { "$Notification", "$PUSH", "$Notification", "$PUSH_NOTIFICATION_TO_REQUESTOR_AND_REVIEWER_AFTER_EMPLOYEE_UPDATE", "$resource_requestor_allocation_wf,Reviewer", "$PUSH_NOTIFICATION_TO_REQUESTOR_AND_REVIEWER_AFTER_EMPLOYEE_UPDATE" });
        }
    }
}
