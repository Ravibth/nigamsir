using MediatR;
using RMT.Projects.Application.DTOs;
using RMT.Projects.Application.IHttpServices;
using RMT.Projects.Application.Responses;
using RMT.Projects.Domain.Entities;
using RMT.Projects.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RMT.Projects.Domain.Constant;

namespace RMT.Projects.Application.Handlers.QueryHandlers
{
    public class ProjectActualBudgetOverShootQuery : IRequest<List<ProjectActualBudgetResponse>>
    {

    }
    public class ProjectActualBudgetOverShootQueryHandler : IRequestHandler<ProjectActualBudgetOverShootQuery, List<ProjectActualBudgetResponse>>
    {
        private readonly IProjectRepository _repository;
        private readonly IResourceAllocationHttpApi _resourceAllocationHttpApi;
        private readonly IConfigurationHttpApi _configurationHttpApi;
        public ProjectActualBudgetOverShootQueryHandler(IProjectRepository projectRepository,
            IResourceAllocationHttpApi resourceAllocationHttpApi,
            IConfigurationHttpApi configurationHttpApi)
        {
            _repository = projectRepository;
            _resourceAllocationHttpApi = resourceAllocationHttpApi;
            _configurationHttpApi = configurationHttpApi;
        }
        public async Task<List<ProjectActualBudgetResponse>> Handle(ProjectActualBudgetOverShootQuery request, CancellationToken cancellationToken)
        {
            var projectBudgetList = await _repository.GetAllProjectBudgetList();


            List<ProjectActualBudgetResponse> projectBudgetsResult = new List<ProjectActualBudgetResponse>();
            var groupedPublishedResponse = await _resourceAllocationHttpApi.PublishResouceAllocationDayGroupedHttpApiQuery();
            var configurationGroup = await _configurationHttpApi.GetProjectConfigurationByConfigGroupAndConfigType("Alert_condition_for_Timesheet_Hours", "OFFERINGS");
            foreach (var project in projectBudgetList)
            {
                var totalBudgetedHour = project.ProjectBudget.Sum(x => x.BudgetedHour);
                var bu = project.bu;
                var offering = project.Offerings;
                var projectRequestorRoles = project?.ProjectRoles?.Where(e => e?.ApplicationRole?.ToLower()?.Trim() == UserRoles.ResourceRequestor.ToLower().Trim() && e.IsActive == true);
                var reviewerRoles = project?.ProjectRoles?.Where(e => e?.ApplicationRole?.ToLower()?.Trim() == UserRoles.Reviewer.ToLower().Trim() && e.IsActive == true);
                if (string.IsNullOrEmpty(offering))
                {
                    continue;
                }
                if (string.IsNullOrEmpty(bu))
                {
                    continue;
                }
                if (totalBudgetedHour == 0)
                {
                    continue;
                }
                var selectorKey = $"{bu}|{offering}";
                var configurationInfo = configurationGroup.FirstOrDefault()
                                            ?.ProjectConfigurations?.Where(e => e.AttributeName.ToLower().Trim() == selectorKey.ToLower().Trim())
                                            ?.FirstOrDefault();
                if (configurationInfo == null)
                {
                    continue;
                }

                var groupPublished = groupedPublishedResponse.Where(e => e.JobCode != null && project?.JobCode != null && e.JobCode.ToLower().Trim() == project.JobCode.ToLower().Trim()
                                                    && e.PipelineCode != null && project.PipelineCode != null && e.PipelineCode.ToLower().Trim() == project.PipelineCode.ToLower().Trim()).FirstOrDefault();
                
                string requestors = string.Join(",", projectRequestorRoles.Select(e => e.User));
                string revierers = string.Join(",", reviewerRoles.Select(e => e.User));
                string sender_notifications = string.Empty;
                if (string.IsNullOrEmpty(requestors))
                {
                    sender_notifications = revierers;
                }
                else if (string.IsNullOrEmpty(revierers))
                {
                    sender_notifications = requestors;
                }
                else
                {
                    sender_notifications = string.Join(",", requestors, revierers);
                }
                if (groupPublished != null)
                {
                    var timesheetConfigPcnt = Int32.Parse(configurationInfo.AttributeValue) / 100.00;
                    var ratio = groupPublished.TotalActualEfforts / totalBudgetedHour;
                    if (ratio > timesheetConfigPcnt)
                    {
                        projectBudgetsResult.Add(new()
                        {
                            Action = "NOTIFICATION_FOR_TIMESHEET_CROSSING_BUDGETED_THRESHOLD",
                            PipelineCode = string.IsNullOrEmpty(project.PipelineCode) == true ? string.Empty : project.PipelineCode,
                            JobCode = string.IsNullOrEmpty(project.JobCode) == true ? string.Empty : project.JobCode,
                            PipelineName = string.IsNullOrEmpty(project.PipelineName) == true ? string.Empty : project.PipelineName,
                            JobName = string.IsNullOrEmpty(project.JobName) == true ? string.Empty : project.JobName,
                            ProjectName = string.IsNullOrEmpty(project.JobCode) ? project.PipelineName : project.JobName,
                            ConsumedTimesheetBudgetPct = Convert.ToString((int)(ratio * 100)),
                            sender_for_notification = sender_notifications
                        });

                    }
                }
            }
            return projectBudgetsResult;
        }
    }
}
