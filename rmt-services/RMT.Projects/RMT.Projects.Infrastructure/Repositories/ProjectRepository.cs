using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using Microsoft.EntityFrameworkCore.Update.Internal;
using Newtonsoft.Json;
using RMT.Projects.Domain;
using RMT.Projects.Domain.DTOs.Request;
using RMT.Projects.Domain.DTOs.Response;
using RMT.Projects.Domain.Entities;
using RMT.Projects.Domain.Repositories;
using RMT.Projects.Infrastructure.Data;
using RMT.Projects.Infrastructure.Helpers;
using RMT.Projects.Infrastructure.Util;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data;
using System.Linq;
using System.Net.Http.Json;
using System.Reflection.Emit;
using System.Text.Json.Serialization;
using static RMT.Projects.Domain.Constant;
using static RMT.Projects.Infrastructure.Constants;

namespace RMT.Projects.Infrastructure.Repositories
{
    public class ProjectRepository : IProjectRepository
    {
        protected readonly ProjectDbContext _projectDbContext;

        public ProjectRepository(ProjectDbContext projectDbContext)
        {
            _projectDbContext = projectDbContext;
        }

        public async Task<List<Project>> GetAllAsync()
        {
            return await _projectDbContext.Projects
                    .Where(d => d.IsActive == true)
                    .Include(d => d.ProjectRolesView)
                    .ToListAsync();
        }
        public async Task<List<Project>> GetAllProjectBudgetList()
        {


            var projectList = await _projectDbContext.Projects
                                .Include(e => e.ProjectBudget.Where(e => e.IsActive == true))
                                .Include(e => e.ProjectRoles.Where(e => e.IsActive == true))
                                .Where(e => e.ProjectBudget != null && e.ProjectBudget.Count > 0 && e.IsActive == true).ToListAsync();
            return projectList;
        }

        public async Task<List<Project>> GetAllProjectForBudget()
        {
            return await _projectDbContext.Projects
                    .Where(d => d.IsActive == true)
                    .Include(e => e.ProjectBudget)
                    .Select(a => new Project
                    {
                        Id = a.Id,
                        PipelineName = a.PipelineName,
                        PipelineCode = a.PipelineCode,
                        JobName = a.JobName,
                        JobCode = a.JobCode,
                        ClientName = a.ClientName,
                        Expertise = a.Expertise,//Recheck
                        Sme = a.Sme,//Recheck
                        Smeg = a.Smeg,//Recheck
                        bu = a.bu,
                        BUId = a.BUId,
                        Offerings = a.Offerings,
                        OfferingsId = a.OfferingsId,
                        Solutions = a.Solutions,
                        SolutionsId = a.SolutionsId,
                        StartDate = a.StartDate,
                        EndDate = a.EndDate,
                        Location = a.Location,
                        ProjectType = a.ProjectType,
                        ChargableType = a.ChargableType,
                        RevenueUnit = a.RevenueUnit,//Recheck
                        BudgetStatus = a.BudgetStatus,
                        IsActive = a.IsActive,
                        JobId = a.JobId
                    }
                    )
                    .ToListAsync();
        }

        public async Task<List<Project>> GetAllProjectForBudgetByJobCodes(List<string> JobCodes)
        {
            JobCodes = JobCodes.Where(a => !string.IsNullOrEmpty(a)).ToList();

            return await _projectDbContext.Projects
                    .Where(d => d.IsActive == true && !string.IsNullOrEmpty(d.JobCode) && JobCodes.Any(a => a == d.JobCode))
                    .Include(e => e.ProjectBudget)
                    .Select(a => new Project
                    {
                        Id = a.Id,
                        PipelineName = a.PipelineName,
                        PipelineCode = a.PipelineCode,
                        JobName = a.JobName,
                        JobCode = a.JobCode,
                        ClientName = a.ClientName,
                        bu = a.bu,
                        BUId = a.BUId,
                        Offerings = a.Offerings,
                        OfferingsId = a.OfferingsId,
                        Solutions = a.Solutions,
                        SolutionsId = a.SolutionsId,
                        StartDate = a.StartDate,
                        EndDate = a.EndDate,
                        Location = a.Location,
                        ProjectType = a.ProjectType,
                        ChargableType = a.ChargableType,
                        BudgetStatus = a.BudgetStatus,
                        IsActive = a.IsActive,
                        JobId = a.JobId
                    }
                    )
                    .ToListAsync();
        }


        public async Task<List<Project>> GetAllProjectByBUandExpertiseAsync(string BU, string Offerings, DateTime EndDate)
        {
            return await _projectDbContext.Projects
                    .Where(d => d.IsActive == true
                    && !string.IsNullOrEmpty(d.bu) && !string.IsNullOrEmpty(BU) && d.bu.Trim().ToLower() == BU.Trim().ToLower()
                    && !string.IsNullOrEmpty(d.Offerings) && !string.IsNullOrEmpty(Offerings) && d.Offerings.Trim().ToLower() == Offerings.Trim().ToLower()
                    && d.EndDate != null && DateOnly.FromDateTime((DateTime)d.EndDate) == DateOnly.FromDateTime(EndDate))
                    .Include(d => d.ProjectRolesView)
                    .ToListAsync();
        }

        public async Task<Project> AddAsync(Project entity)
        {
            await _projectDbContext.Set<Project>().AddAsync(entity);

            await _projectDbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<List<Project>> GetAllProjectsByCreationDate(DateOnly creationDate)
        {
            var result = await _projectDbContext.Projects.Where(e => e.IsActive == true
                                                                && e.CreatedAt != null && creationDate == DateOnly.FromDateTime(e.CreatedAt.Value.Date))
                                                         .Include(e => e.ProjectRolesView)
                                                         .ToListAsync();
            return result;
        }

        public async Task<GetProjectDetailsByUserRole> GetProjectDetailsByPipelineCodeAndUserRole(string pipelineCode, string? jobCode, string? userEmailId, List<string> applicationRoles)
        {
            applicationRoles = applicationRoles.Where((d) => !(string.IsNullOrEmpty(d) || d.Equals(Constants.NULL_STR, StringComparison.OrdinalIgnoreCase))).ToList();
            List<string> EmployeeView = new () { UserRoles.Employee };
            List<string> EmployeeChargableRoles = new List<string>() { UserRoles.EngagementLeader, UserRoles.CSP, UserRoles.CSL, UserRoles.LeadGenerator };
            List<string> EmployeeNonChargableRoles = new List<string>() { UserRoles.JobManager, UserRoles.SMEGLeader, UserRoles.AssignmentIncharge, UserRoles.LeadGenerator };
            List<string> EmployeePipelineRoles = new List<string>() { UserRoles.EO, UserRoles.ProposedCSP, UserRoles.ProposedEL, UserRoles.FindingPartner, UserRoles.LeadGenerator, UserRoles.FindingPartner1, UserRoles.FindingPartner2 };

            List<string> DelegateView = new List<string>()
            {
                UserRoles.Delegate,
                UserRoles.AdditionalEl,
                UserRoles.SystemAdmin,
                UserRoles.Admin,
                UserRoles.CEOCOO,
                UserRoles.Leaders,
                UserRoles.AdditionalDelegate,
            };

            List<string> DelegateChargableRoles = new List<string>() { UserRoles.EngagementLeader, UserRoles.CSP, UserRoles.CSL, UserRoles.LeadGenerator };//CSP REMAINING
            List<string> DelegateNonChargableRoles = new List<string>() { UserRoles.JobManager, UserRoles.SMEGLeader, UserRoles.AssignmentIncharge, UserRoles.LeadGenerator }; // DONE
            List<string> DelegatePipelinesRoles = new List<string>() { UserRoles.EO, UserRoles.ProposedCSP, UserRoles.ProposedEL, UserRoles.FindingPartner, UserRoles.LeadGenerator, UserRoles.FindingPartner1, UserRoles.FindingPartner2 };//DONE

            List<string> RequestorView = new List<string>()
            {
                UserRoles.ResourceRequestor,
                UserRoles.Reviewer,
                UserRoles.ProposedCSP,
                UserRoles.CSP,
                UserRoles.CSL,
                UserRoles.SMEGLeader,
                UserRoles.EngagementLeader,
                UserRoles.JobManager,
                UserRoles.EO,
                UserRoles.ProposedEL,
            };

            List<string> RequestorChargableRoles = new List<string>() { UserRoles.EngagementLeader, UserRoles.CSL, UserRoles.LeadGenerator, UserRoles.CSP };//CSP REMAINING
            List<string> RequestorNonChargableRoles = new List<string>() { UserRoles.JobManager, UserRoles.SMEGLeader, UserRoles.AssignmentIncharge, UserRoles.LeadGenerator };//DONE
            List<string> RequestorPipelineRoles = new List<string>() { UserRoles.EO, UserRoles.ProposedCSP, UserRoles.ProposedEL, UserRoles.FindingPartner, UserRoles.LeadGenerator, UserRoles.FindingPartner1, UserRoles.FindingPartner2 };//DONE

            var currentView = "";
            GetProjectDetailsByUserRole projectResponse = null;
            var project = await _projectDbContext.Projects.Where(d =>
                        !string.IsNullOrEmpty(d.PipelineCode) && d.PipelineCode.Trim().ToLower() == pipelineCode.Trim().ToLower()
                        && ((string.IsNullOrEmpty(d.JobCode) && (string.IsNullOrEmpty(jobCode) || jobCode == "undefined" || jobCode == "null")) ||
                        (string.IsNullOrEmpty(jobCode) == false && (!string.IsNullOrEmpty(d.JobCode) && d.JobCode.Trim().ToLower() == jobCode.Trim().ToLower())))
                        )
                        .Include(d => d.ProjectRoles.Where(d => d.IsActive == true))
                        .Include(d => d.ProjectRolesView.Where(d => d.IsActive == true))
                        .Include(d => d.ProjectRequisitionAllocations)
                        .FirstOrDefaultAsync();
            if (project != null)
            {
                bool IsJobType = false;
                if (!string.IsNullOrEmpty(project.JobCode))
                {
                    IsJobType = true;
                }
                if (String.IsNullOrEmpty(userEmailId))
                {
                    return new GetProjectDetailsByUserRole();
                }
                var userRole = project.ProjectRolesView
                                .Where(d => d.User.Trim().ToLower().Equals(userEmailId.Trim().ToLower()) && d.IsActive == true);
                if (userRole == null)
                {
                    return null;
                }
                var empRole = EmployeeView.Where(d => userRole.Any(t => t.Role.Trim().ToLower().Equals(d.Trim().ToLower())));
                if (empRole.Any())
                {
                    currentView = ViewTypes.EMPLOYEE_VIEW;
                }
                var delegateRole = DelegateView.Where(d => userRole.Any(t => t.Role.Trim().ToLower().Equals(d.Trim().ToLower())));
                var adminRole = applicationRoles.Where(t => t.Equals(UserRoles.Admin, StringComparison.OrdinalIgnoreCase) || t.Equals(UserRoles.CEOCOO, StringComparison.OrdinalIgnoreCase) || t.Equals(UserRoles.SystemAdmin, StringComparison.OrdinalIgnoreCase));
                var leadersRole = applicationRoles.Where(t => t.Equals(UserRoles.Leaders, StringComparison.OrdinalIgnoreCase));

                if (delegateRole.Any() || adminRole.Any() || leadersRole.Any())
                {
                    currentView = ViewTypes.DELEGATE_VIEW;
                }
                var reqRole = RequestorView.Where(d => userRole.Any(t => t.Role.Trim().ToLower().Equals(d.Trim().ToLower())));
                var systemAdminRole = applicationRoles.Any(m => m.Trim().ToLower() == UserRoles.SystemAdmin.Trim().ToLower());
                if (reqRole.Any() || systemAdminRole)
                {
                    currentView = ViewTypes.REQUESTOR_VIEW;
                }
                var projectType = !string.IsNullOrEmpty(project.ChargableType) ? project.ChargableType : string.Empty;
                projectResponse = new GetProjectDetailsByUserRole()
                {
                    StartDate = project.StartDate,//
                    EndDate = project.EndDate,//
                    ProjectType = project.ProjectType,//
                    ChargableType = project.ChargableType,//
                    ClientGroup = project.ClientGroup,//
                    ClientName = project.ClientName,//
                    bu = project.bu,
                    BUId = project.BUId,
                    Expertise = project.Expertise,//Recheck
                    Sme = project.Sme,//Recheck
                    Smeg = project.Smeg,//Recheck
                    Offerings = project.Offerings,
                    OfferingsId = project.OfferingsId,
                    Solutions = project.Solutions,
                    SolutionsId = project.SolutionsId,
                    RevenueUnit = project.RevenueUnit,//Recheck
                    PipelineCode = project.PipelineCode,
                    JobCode = project.JobCode,//
                    GtRefferenceCountry = project.GtRefferenceCountry,//
                    LegalEntity = project.LegalEntity,//
                    PipelineStatus = project.PipelineStatus,
                    ProjectRequisitionAllocations = project.ProjectRequisitionAllocations,
                    Industry = project.Industry,
                    JobId = project.JobId,
                    ProjectActivationAndClosureState = Helper.GetProjectStateByActivationAndClosureState(project.ProjectClosureState, project.ProjectActivationStatus),
                    IsConfidential = project.IsConfidential
                };

                if (IsJobType == true && projectType.Trim().ToLower() == ProjectChargeType.CHARGEABLE.Trim().ToLower() && currentView.Trim().ToLower().Equals(ViewTypes.EMPLOYEE_VIEW.Trim().ToLower()))
                {
                    projectResponse.Industry = project.Industry;
                    projectResponse.Subindustry = project.Subindustry;
                    projectResponse.DeliveryLocation = project.DeliveryLocation;
                    projectResponse.JobLocation = project.JobLocation;
                    projectResponse.ViewType = ViewTypes.EMPLOYEE_VIEW;
                    projectResponse.IsBudgetRequired = false;
                    projectResponse.PipelineStatus = Helper.GetProjectStateByActivationAndClosureState(project.ProjectClosureState, project.ProjectActivationStatus);
                    projectResponse.ResourceRequestorNames = project.ProjectRolesView.Where(r => EmployeeChargableRR.Any(d => d.Trim().ToLower().Equals(r.Role.Trim().ToLower()))).Select(a => a.UserName).ToList();
                    projectResponse.ProjectRoles = project.ProjectRoles.Where(r => EmployeeChargableRoles.Any(d => d.Trim().ToLower().Equals(r.Role.Trim().ToLower()))).ToList();
                    projectResponse.ProjectRolesView = project.ProjectRolesView.Where(r => EmployeeChargableRoles.Any(d => d.Trim().ToLower().Equals(r.Role.Trim().ToLower()))).ToList();
                }
                else if (IsJobType == true && projectType.Trim().ToLower() == ProjectChargeType.NON_CHARGABLE.Trim().ToLower() && currentView.Trim().ToLower().Equals(ViewTypes.EMPLOYEE_VIEW.Trim().ToLower()))
                {
                    projectResponse.Industry = project.Industry;//
                    projectResponse.Subindustry = project.Subindustry;//
                    projectResponse.JobLocation = project.JobLocation;//
                    projectResponse.DeliveryLocation = project.DeliveryLocation;
                    projectResponse.ViewType = ViewTypes.EMPLOYEE_VIEW;
                    projectResponse.IsBudgetRequired = false;
                    projectResponse.PipelineStatus = Helper.GetProjectStateByActivationAndClosureState(project.ProjectClosureState, project.ProjectActivationStatus);
                    projectResponse.ResourceRequestorNames = project.ProjectRolesView.Where(r => EmployeeNonChargableRR.Any(d => d.Trim().ToLower().Equals(r.Role.Trim().ToLower()))).Select(a => a.UserName).ToList();
                    projectResponse.ProjectRoles = project.ProjectRoles.Where(r => EmployeeNonChargableRoles.Any(d => d.Trim().ToLower().Equals(r.Role.Trim().ToLower()))).ToList();
                    projectResponse.ProjectRolesView = project.ProjectRolesView.Where(r => EmployeeNonChargableRoles.Any(d => d.Trim().ToLower().Equals(r.Role.Trim().ToLower()))).ToList();
                }
                else if (IsJobType == true && projectType.Trim().ToLower() == ProjectChargeType.CHARGEABLE.Trim().ToLower() && currentView.Trim().ToLower().Equals(ViewTypes.DELEGATE_VIEW.Trim().ToLower()))
                {
                    projectResponse.IsBudgetRequired = false;
                    projectResponse.ViewType = ViewTypes.DELEGATE_VIEW;
                    projectResponse.Industry = project.Industry;
                    projectResponse.Subindustry = project.Subindustry;
                    projectResponse.JobLocation = project.JobLocation;
                    projectResponse.DeliveryLocation = project.DeliveryLocation;
                    projectResponse.PipelineStatus = Helper.GetProjectStateByActivationAndClosureState(project.ProjectClosureState, project.ProjectActivationStatus);
                    projectResponse.ResourceRequestorNames = project.ProjectRolesView.Where(r => EmployeeChargableRR.Any(d => d.Trim().ToLower().Equals(r.Role.Trim().ToLower()))).Select(a => a.UserName).ToList();
                    projectResponse.ProjectRequisitionAllocations = project.ProjectRequisitionAllocations;
                    projectResponse.ProjectRoles = project.ProjectRoles.Where(r => DelegateChargableRoles.Any(d => d.Trim().ToLower().Equals(r.Role.Trim().ToLower()))).ToList();
                    projectResponse.ProjectRolesView = project.ProjectRolesView.Where(r => DelegateChargableRoles.Any(d => d.Trim().ToLower().Equals(r.Role.Trim().ToLower()))).ToList();
                }
                else if (IsJobType == true && projectType.Trim().ToLower() == ProjectChargeType.CHARGEABLE.Trim().ToLower() && currentView.Trim().ToLower().Equals(ViewTypes.REQUESTOR_VIEW.Trim().ToLower()))
                {
                    projectResponse.IsBudgetRequired = true;
                    projectResponse.ViewType = ViewTypes.REQUESTOR_VIEW;
                    projectResponse.ProjectRequisitionAllocations = project.ProjectRequisitionAllocations;
                    projectResponse.Industry = project.Industry;
                    projectResponse.Subindustry = project.Subindustry;
                    projectResponse.JobLocation = project.JobLocation;
                    projectResponse.PipelineStatus = Helper.GetProjectStateByActivationAndClosureState(project.ProjectClosureState, project.ProjectActivationStatus);
                    projectResponse.ResourceRequestorNames = project.ProjectRolesView.Where(r => EmployeeChargableRR.Any(d => d.Trim().ToLower().Equals(r.Role.Trim().ToLower()))).Select(a => a.UserName).ToList();
                    projectResponse.BudgetStatus = project.BudgetStatus;
                    projectResponse.DeliveryLocation = project.DeliveryLocation;
                    projectResponse.ProjectRoles = project.ProjectRoles.Where(r => RequestorChargableRoles.Any(d => d.Trim().ToLower().Equals(r.Role.Trim().ToLower()))).ToList();
                    projectResponse.ProjectRolesView = project.ProjectRolesView.Where(r => RequestorChargableRoles.Any(d => d.Trim().ToLower().Equals(r.Role.Trim().ToLower()))).ToList();
                }
                else if (IsJobType == true && projectType.Trim().ToLower() == ProjectChargeType.NON_CHARGABLE.Trim().ToLower() && currentView.Trim().ToLower().Equals(ViewTypes.DELEGATE_VIEW.Trim().ToLower()))
                {
                    projectResponse.Industry = project.Industry;
                    projectResponse.ProjectRequisitionAllocations = project.ProjectRequisitionAllocations;
                    projectResponse.Subindustry = project.Subindustry;
                    projectResponse.JobLocation = project.JobLocation;
                    projectResponse.IsBudgetRequired = false;
                    projectResponse.DeliveryLocation = project.DeliveryLocation;
                    projectResponse.ViewType = ViewTypes.DELEGATE_VIEW;
                    projectResponse.PipelineStatus = Helper.GetProjectStateByActivationAndClosureState(project.ProjectClosureState, project.ProjectActivationStatus);
                    projectResponse.ResourceRequestorNames = project.ProjectRolesView.Where(r => EmployeeNonChargableRR.Any(d => d.Trim().ToLower().Equals(r.Role.Trim().ToLower()))).Select(a => a.UserName).ToList();
                    projectResponse.ProjectRoles = project.ProjectRoles.Where(r => DelegateNonChargableRoles.Any(d => d.Trim().ToLower().Equals(r.Role.Trim().ToLower()))).ToList();
                    projectResponse.ProjectRolesView = project.ProjectRolesView.Where(r => DelegateNonChargableRoles.Any(d => d.Trim().ToLower().Equals(r.Role.Trim().ToLower()))).ToList();
                }
                else if (IsJobType == true && projectType.Trim().ToLower() == ProjectChargeType.NON_CHARGABLE.Trim().ToLower() && currentView.Trim().ToLower().Equals(ViewTypes.REQUESTOR_VIEW.Trim().ToLower()))
                {
                    projectResponse.Industry = project.Industry;
                    projectResponse.Subindustry = project.Subindustry;
                    projectResponse.JobLocation = project.JobLocation;
                    projectResponse.DeliveryLocation = project.DeliveryLocation;
                    projectResponse.ProjectRequisitionAllocations = project.ProjectRequisitionAllocations;
                    projectResponse.BudgetStatus = project.BudgetStatus;
                    projectResponse.IsBudgetRequired = true;
                    projectResponse.PipelineStatus = Helper.GetProjectStateByActivationAndClosureState(project.ProjectClosureState, project.ProjectActivationStatus);
                    projectResponse.ViewType = ViewTypes.REQUESTOR_VIEW;
                    projectResponse.ResourceRequestorNames = project.ProjectRolesView.Where(r => EmployeeNonChargableRR.Any(d => d.Trim().ToLower().Equals(r.Role.Trim().ToLower()))).Select(a => a.UserName).ToList();
                    projectResponse.ProjectRoles = project.ProjectRoles.Where(r => RequestorNonChargableRoles.Any(d => d.Trim().ToLower().Equals(r.Role.Trim().ToLower()))).ToList();
                    projectResponse.ProjectRolesView = project.ProjectRolesView.Where(r => RequestorNonChargableRoles.Any(d => d.Trim().ToLower().Equals(r.Role.Trim().ToLower()))).ToList();
                }
                else if (IsJobType == false && currentView.Trim().ToLower().Equals(ViewTypes.REQUESTOR_VIEW.Trim().ToLower()))
                {
                    projectResponse.Industry = project.Industry;
                    projectResponse.IsBudgetRequired = true;
                    projectResponse.ViewType = ViewTypes.REQUESTOR_VIEW;
                    projectResponse.BudgetStatus = project.BudgetStatus;
                    projectResponse.ProjectRequisitionAllocations = project.ProjectRequisitionAllocations;
                    projectResponse.DeliveryLocation = project.DeliveryLocation;
                    projectResponse.ResourceRequestorNames = project.ProjectRolesView.Where(r => EmployeePipelineRR.Any(d => d.Trim().ToLower().Equals(r.Role.Trim().ToLower()))).Select(a => a.UserName).ToList();
                    projectResponse.ProjectRoles = project.ProjectRoles.Where(r => RequestorPipelineRoles.Any(d => d.Trim().ToLower().Equals(r.Role.Trim().ToLower()))).ToList();
                    projectResponse.ProjectRolesView = project.ProjectRolesView.Where(r => RequestorPipelineRoles.Any(d => d.Trim().ToLower().Equals(r.Role.Trim().ToLower()))).ToList();
                }
                else if (IsJobType == false && currentView.Trim().ToLower().Equals(ViewTypes.DELEGATE_VIEW.Trim().ToLower()))
                {
                    projectResponse.Industry = project.Industry;
                    projectResponse.IsBudgetRequired = false;
                    projectResponse.ProjectRequisitionAllocations = project.ProjectRequisitionAllocations;
                    projectResponse.ViewType = ViewTypes.DELEGATE_VIEW;
                    projectResponse.ResourceRequestorNames = project.ProjectRolesView.Where(r => EmployeePipelineRR.Any(d => d.Trim().ToLower().Equals(r.Role.Trim().ToLower()))).Select(a => a.UserName).ToList();
                    projectResponse.ProjectRoles = project.ProjectRoles.Where(r => DelegatePipelinesRoles.Any(d => d.Trim().ToLower().Equals(r.Role.Trim().ToLower()))).ToList();
                    projectResponse.ProjectRolesView = project.ProjectRolesView.Where(r => DelegatePipelinesRoles.Any(d => d.Trim().ToLower().Equals(r.Role.Trim().ToLower()))).ToList();
                }
                else if (IsJobType == false && currentView.Trim().ToLower().Equals(ViewTypes.EMPLOYEE_VIEW.Trim().ToLower()))
                {
                    projectResponse.Industry = project.Industry;
                    projectResponse.IsBudgetRequired = false;
                    projectResponse.ViewType = ViewTypes.EMPLOYEE_VIEW;
                    projectResponse.ResourceRequestorNames = project.ProjectRolesView.Where(r => EmployeePipelineRR.Any(d => d.Trim().ToLower().Equals(r.Role.Trim().ToLower()))).Select(a => a.UserName).ToList();
                    projectResponse.ProjectRoles = project.ProjectRoles.Where(r => EmployeePipelineRoles.Any(d => d.Trim().ToLower().Equals(r.Role.Trim().ToLower()))).ToList();
                    projectResponse.ProjectRolesView = project.ProjectRolesView.Where(r => EmployeePipelineRoles.Any(d => d.Trim().ToLower().Equals(r.Role.Trim().ToLower()))).ToList();

                }
                if (IsJobType == true && leadersRole.Any())
                {
                    projectResponse.BudgetStatus = project.BudgetStatus;
                }

            }

            return projectResponse;
        }

        public async Task<List<ProjectRolesView>> GetOnlyEmployeesOfProject(string pipelineCode, string? jobCode)
        {
            List<ProjectRolesView> result = new List<ProjectRolesView>();
            var projects = await _projectDbContext.Projects
                                                    .Include(d => d.ProjectRolesView.Where(e => e.IsActive == true))
                                                    .Where(d => !string.IsNullOrEmpty(d.PipelineCode) && d.PipelineCode.Trim().ToLower() == pipelineCode.Trim().ToLower()
                                                    && ((string.IsNullOrEmpty(d.JobCode) && (string.IsNullOrEmpty(jobCode) || jobCode == "undefined" || jobCode == "null")) ||
                                                    (string.IsNullOrEmpty(jobCode) == false && (!string.IsNullOrEmpty(d.JobCode) && d.JobCode.Trim().ToLower() == jobCode.Trim().ToLower()))))
                                                    .FirstOrDefaultAsync();
            var emailGrp = projects.ProjectRolesView.GroupBy(e => e.User).ToDictionary(d => d.Key, d => d.ToList());
            foreach (var kv in emailGrp.AsEnumerable())
            {
                if (kv.Value.Count == 1 && kv.Value[0].Role.Equals(UserRoles.Employee, StringComparison.OrdinalIgnoreCase))
                {
                    result.Add(kv.Value[0]);
                }
            }
            return result;
        }

        public async Task<Dictionary<string, List<ProjectRolesView>>> GetProjectRolesByPipelineCodeAndRoles(List<KeyValuePair<string, string?>> pipelineCodes, List<string>? roles)
        {
            Dictionary<string, List<ProjectRolesView>> finalResult = null;
            if (pipelineCodes != null)
            {
                pipelineCodes = pipelineCodes.Where(p => !string.IsNullOrEmpty(p.Key)).ToList();
                {
                    var projectResults = await GetMultipleProjectByCodes(pipelineCodes);
                    if (projectResults != null && projectResults.Count > 0)
                    {
                        var project = projectResults[0];
                        var projectRoles = await _projectDbContext.ProjectRolesView
                            .Where(z => z.ProjectId == project.Id && z.IsActive == true)
                            .ToListAsync();
                        var result = projectRoles
                            .GroupBy(x => x.Role)
                            .ToDictionary(d => string.IsNullOrEmpty(d.Key) ? string.Empty : d.Key, d => d.ToList());
                        finalResult = result;
                        if (roles != null && roles.Count > 0)
                        {
                            var finalRoles = roles.Select(d => d.Trim().ToLower()).ToList();
                            roles = roles.Select(role => role.Trim().ToLower()).ToList();
                            finalResult = result.Where(x => roles.Contains(x.Key.Trim().ToLower())).ToDictionary(i => i.Key, i => i.Value); ;
                        }
                        return finalResult;
                    }
                    else
                    {
                        finalResult = new Dictionary<string, List<ProjectRolesView>>();
                        return finalResult;
                    }
                }
            }
            else
            {
                throw new Exception("Pipeline code Not Provided");
            }
            return finalResult;
        }
        public async Task<List<ProjectRoles>> UpdateProjectRolesForSupercoachDelegate(string supercoach_email, string? prev_supercoach_delegate_email, string? new_supercoach_delegate_email, string? new_supercoach_delegate_name)
        {
            var projectRoles = await _projectDbContext.ProjectRoles.Where(x => x.User.ToLower().Trim() == supercoach_email.ToLower().Trim()
                                                        && x.ApplicationRole.ToLower().Trim() == UserRoles.SuperCoach.ToLower().Trim()
                                                       && x.IsActive == true)
                                                      .ToListAsync();
            if (projectRoles != null && projectRoles.Count > 0)
            {
                foreach (var item in projectRoles)
                {
                    item.DelegateEmail = new_supercoach_delegate_email;
                    item.DelegateUserName = new_supercoach_delegate_name;
                    _projectDbContext.ProjectRoles.Update(item);
                }
                await _projectDbContext.SaveChangesAsync();
            }
            return projectRoles;
        }
        public async Task<Project> AddSuperCoachRole(AddSuperCoachProjectRoleRequestDTO req)
        {
            List<KeyValuePair<string, string?>> projectCodes = new();
            projectCodes.Add(new KeyValuePair<string, string?>(req.PipelineCode, String.IsNullOrEmpty(req.JobCode) ? null : req.JobCode));
            var projectResults = await GetMultipleProjectByCodes(projectCodes);
            var projectRoles = projectResults.FirstOrDefault().ProjectRoles;
            foreach (var item in req.SuperCoachInformation)
            {
                var doesSCExist = projectRoles.FirstOrDefault(e =>
                e.User.Equals(item.SuperCoachEmailId, StringComparison.OrdinalIgnoreCase)
                && e.Role.Equals(UserRoles.SuperCoach, StringComparison.OrdinalIgnoreCase)
                );
                if (doesSCExist == null)
                {
                    ProjectRoles newProjectRole = new()
                    {
                        ProjectId = (long)projectResults.FirstOrDefault().Id,
                        UserName = item.SuperCoachName,
                        User = item.SuperCoachEmailId,
                        Role = UserRoles.SuperCoach,
                        ApplicationRole = UserRoles.SuperCoach,
                        DelegateEmail = item.NewAllocationSuperCoachDelegateEmail,
                        DelegateUserName = item.NewAllocationSuperCoachDelegateName,
                        IsActive = true
                    };
                    await _projectDbContext.ProjectRoles.AddAsync(newProjectRole);
                }
                else
                {
                    doesSCExist.DelegateEmail = item.NewAllocationSuperCoachDelegateEmail;
                    doesSCExist.DelegateUserName = item.NewAllocationSuperCoachDelegateName;
                    _projectDbContext.ProjectRoles.Update(doesSCExist);
                }
            }
            _projectDbContext.SaveChanges();
            return projectResults.FirstOrDefault();
        }

        public async Task<Dictionary<string, List<ProjectRolesView>>> GetProjectRolesByPipelineCodeAndAppRoles(List<KeyValuePair<string, string?>> pipelineCodes, List<string>? roles)
        {
            Dictionary<string, List<ProjectRolesView>> finalResult = null;
            if (pipelineCodes != null)
            {
                pipelineCodes = pipelineCodes.Where(p => !string.IsNullOrEmpty(p.Key)).ToList();
                {
                    var projectResults = await GetMultipleProjectByCodes(pipelineCodes);
                    if (projectResults != null && projectResults.Count > 0)
                    {
                        var project = projectResults[0];
                        var projectRoles = await _projectDbContext.ProjectRolesView
                            .Where(z => z.ProjectId == project.Id && z.IsActive == true)
                            .ToListAsync();
                        var result = projectRoles
                            .GroupBy(x => string.IsNullOrEmpty(x.ApplicationRole) ? string.Empty : x.ApplicationRole)
                            .ToDictionary(d => string.IsNullOrEmpty(d.Key) ? string.Empty : d.Key, d => d.ToList());

                        finalResult = result;
                        if (roles != null && roles.Count > 0)
                        {
                            var finalRoles = roles.Select(d => d.Trim().ToLower()).ToList();
                            roles = roles.Select(role => role.Trim().ToLower()).ToList();
                            finalResult = result.Where(x => roles.Contains(x.Key.Trim().ToLower())).ToDictionary(i => i.Key, i => i.Value); ;
                        }
                        return finalResult;
                    }
                    else
                    {
                        finalResult = new Dictionary<string, List<ProjectRolesView>>();
                        return finalResult;
                    }
                }
            }
            else
            {
                throw new Exception("Pipeline code Not Provided");
            }
            return finalResult;
        }

        public async Task<List<GetProjectRolesByEmailsResponse>> GetProjectRolesByEmails(List<string> emails)
        {
            List<GetProjectRolesByEmailsResponse> resp = new();
            if (emails != null && emails.Count > 0)
            {
                resp = await _projectDbContext.ProjectRolesView
                    .Where(p => emails.Contains(p.User) && p.IsActive == true)
                    .GroupBy(p => p.User)
                    .Select(m => new GetProjectRolesByEmailsResponse()
                    {
                        Role = m.Select(p => p.Role).Distinct().ToList(),
                        User = m.Key
                    })
                    .ToListAsync();
            }
            return resp;

        }

        public async Task<List<GetMembersOfAllProjectsOfUserResponse>> GetMembersOfAllProjectsOfUser(List<string> userEmail, List<string>? rolesProvided)
        {
            var users = userEmail.Select(a => a.Trim().ToLower()).ToList();
            if (users != null && users.Count > 0)
            {
                List<ProjectRolesView> projectRoles = await _projectDbContext.ProjectRolesView
                    .Where(d => users.Contains(d.User.Trim().ToLower()) && d.IsActive == true)
                    .Include(x => x.Project)
                    .ToListAsync();
                var response = new List<GetMembersOfAllProjectsOfUserResponse>();
                foreach (var user in users)
                {
                    var projectWithMembers = new List<AllProjectsOfUserWithMembers>();
                    var userProjects = await _projectDbContext.Projects
                                    .Where(d => d.ProjectRolesView.Any(x => x.User.Trim().ToLower().Equals(user) && d.IsActive == true))
                                    .ToListAsync();

                    foreach (var proj in userProjects)
                    {
                        Dictionary<string, List<string>> res = _projectDbContext.ProjectRolesView.Where(d => d.ProjectId == proj.Id && d.IsActive == true).GroupBy(x => x.Role).ToDictionary(x => x.Key, x => x.Select(e => e.User).ToList());
                        AllProjectsOfUserWithMembers projectWithMember = new AllProjectsOfUserWithMembers()
                        {
                            PipelineName = proj.PipelineName,
                            JobName = proj.JobName,
                            RoleEmails = res
                        };
                        projectWithMembers.Add(projectWithMember);
                    }
                    var resp = new GetMembersOfAllProjectsOfUserResponse()
                    {
                        userEmail = user,
                        ProjectMembers = projectWithMembers
                    };
                    response.Add(resp);
                }
                return response;
            }
            else
            {
                return new List<GetMembersOfAllProjectsOfUserResponse>();
            }
        }

        public async Task<Project> UpdateAsync(Project entity)
        {
            Project? projectEntity = _projectDbContext.Projects
                .Include(a => a.ProjectRoles)
                .Include(a => a.ProjectDemands).FirstOrDefault(a => a.Id == entity.Id);
            if (projectEntity != null)
            {
                var projectRoles = projectEntity.ProjectRoles.ToList();
                var projectDemand = projectEntity.ProjectDemands.ToList();

                foreach (var role in entity.ProjectRoles)
                {
                    if (role.Id <= 0)
                    {
                        role.ApplicationRole = MappingRoleApplication[role.Role];
                        await _projectDbContext.Set<ProjectRoles>().AddAsync(role);
                    }
                    else
                    {
                        var obj = _projectDbContext.ProjectRoles.Where(a => a.Id == role.Id).FirstOrDefault();
                        if (obj != null)
                        {
                            obj.IsActive = role.IsActive;
                            obj.User = role.User;
                            obj.Role = role.Role;
                            obj.ApplicationRole = MappingRoleApplication[role.Role];
                            obj.DelegateEmail = role.DelegateEmail;
                            obj.DelegateUserName = role.DelegateUserName;
                            obj.Description = role.Description;
                            obj.ModifiedAt = entity.ModifiedAt;
                            obj.ModifiedBy = entity.ModifiedBy;
                            _projectDbContext.ProjectRoles.Update(obj);
                        }
                        else
                        {
                            //record not found
                        }
                    }
                }

                foreach (var demand in entity.ProjectDemands)
                {
                    if (demand.Id <= 0)
                    {
                        var addedDemand = await _projectDbContext.Set<ProjectDemand>().AddAsync(demand); //Todo: update to modifiedBy
                        await _projectDbContext.Set<ProjectDemandSkills>().AddRangeAsync(demand.ProjectDemandSkills); //Todo: update to modifiedBy

                    }
                    else
                    {
                        var obj = _projectDbContext.ProjectDemands.Where(a => a.Id == demand.Id)
                            .Include(a => a.ProjectDemandSkills).FirstOrDefault();
                        if (obj != null)
                        {
                            obj.IsActive = demand.IsActive;
                            obj.Designation = demand.Designation;
                            obj.NoOfResources = demand.NoOfResources;
                            obj.ModifiedAt = entity.ModifiedAt;
                            obj.ModifiedBy = entity.ModifiedBy;

                            foreach (ProjectDemandSkills demandSkill in demand.ProjectDemandSkills)
                            {
                                if (demandSkill.Id <= 0)
                                {
                                    await _projectDbContext.Set<ProjectDemandSkills>().AddAsync(demandSkill); //Todo: update to modifiedBy
                                }
                                else
                                {
                                    ProjectDemandSkills? objSkill = _projectDbContext.ProjectDemandSkills.Where(a => a.Id == demandSkill.Id)
                                        .FirstOrDefault();
                                    if (objSkill != null)
                                    {
                                        objSkill.IsActive = demandSkill.IsActive;
                                        objSkill.SkillName = demandSkill.SkillName;
                                        objSkill.ModifiedBy = entity.ModifiedBy;
                                    }
                                    else
                                    {
                                        //record not found
                                    }
                                }
                            }

                        }
                        else
                        {
                            //record not found
                        }
                    }
                }

                projectEntity.Description = entity.Description;

                if (entity.EndDate.HasValue)
                    projectEntity.EndDate = entity.EndDate.Value.Date;
                if (entity.IsConfidential.HasValue)
                   projectEntity.IsConfidential = entity.IsConfidential;

                await _projectDbContext.SaveChangesAsync();
                entity = await _projectDbContext.Projects
                    .Where(e => e.Id == entity.Id)
                    .Include(t => t.ProjectRolesView)
                    .Include(x => x.ProjectRoles).FirstOrDefaultAsync();

            }
            else
            {
                throw new Exception("Invalid Record!");
            }


            return entity;// projectEntity;
        }

        /// <summary>
        /// Update the project rolled over flag of the project by pipeline code
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<Project> UpdateProjectRolledOver(Project entity)
        {
            Project? projectEntity;
            if (!string.IsNullOrWhiteSpace(entity.PipelineCode))
            {

                projectEntity = _projectDbContext.Projects.FirstOrDefault(
                                        d => !string.IsNullOrEmpty(d.PipelineCode) && d.PipelineCode.Trim().ToLower() == entity.PipelineCode.Trim().ToLower()
                                        && ((string.IsNullOrEmpty(d.JobCode) && (string.IsNullOrEmpty(entity.JobCode) || entity.JobCode == "undefined" || entity.JobCode == "null")) ||
                                        (string.IsNullOrEmpty(entity.JobCode) == false && (!string.IsNullOrEmpty(d.JobCode) && d.JobCode.Trim().ToLower() == entity.JobCode.Trim().ToLower()))));

                if (projectEntity != null)
                {
                    projectEntity.EndDate = projectEntity.EndDate.HasValue ? projectEntity.EndDate.Value.AddDays(entity.RolloverDays) : null;
                    projectEntity.IsRollover = entity.IsRollover;
                    projectEntity.ModifiedBy = entity.ModifiedBy;
                    _projectDbContext.Projects.Update(projectEntity);
                    await _projectDbContext.SaveChangesAsync();
                }
                else
                {
                    return null;
                }
            }
            else
            {
                throw new Exception("Invalid Pipeline code");
            }
            return projectEntity;
        }

        public async Task<Project> UpdateProjectSuspensionStatus(List<Project> entities)
        {
            try
            {
                Project? projectEntity = new Project();
                foreach (var entity in entities)
                {
                    try
                    {
                        if (!string.IsNullOrWhiteSpace(entity.PipelineCode))
                        {
                            projectEntity = _projectDbContext.Projects.FirstOrDefault(a => !string.IsNullOrEmpty(a.PipelineCode) && a.PipelineCode.Trim().ToLower() == entity.PipelineCode.Trim().ToLower()
                            && ((string.IsNullOrEmpty(a.JobCode) && (string.IsNullOrEmpty(entity.JobCode) || entity.JobCode == "undefined" || entity.JobCode == "null")) ||
                            (string.IsNullOrEmpty(entity.JobCode) == false && (!string.IsNullOrEmpty(a.JobCode) && a.JobCode.Trim().ToLower() == entity.JobCode.Trim().ToLower()))));

                            if (projectEntity != null)
                            {
                                projectEntity.IsSuspended = entity.IsSuspended;
                                projectEntity.SuspendedModifyAt = DateTime.UtcNow;
                                _projectDbContext.Projects.Update(projectEntity);
                            }
                        }
                        else
                        {
                            throw new Exception("Invalid Pipeline code");
                        }
                    }
                    catch (Exception ex1)
                    {
                        Console.WriteLine(ex1.Message + "--" + ex1.StackTrace);
                    }
                }
                await _projectDbContext.SaveChangesAsync();
                return projectEntity;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<Project> UpdateProjectBudgetStatus(Project entity, string budgetStatus)
        {
            try
            {
                Project? projectEntity = new Project();
                if (!string.IsNullOrWhiteSpace(entity.PipelineCode))
                {
                    projectEntity = _projectDbContext.Projects.FirstOrDefault(a => !string.IsNullOrEmpty(a.PipelineCode) && a.PipelineCode.Trim().ToLower() == entity.PipelineCode.Trim().ToLower()
                    && ((string.IsNullOrEmpty(a.JobCode) && (string.IsNullOrEmpty(entity.JobCode) || entity.JobCode == "undefined" || entity.JobCode == "null")) ||
                    (string.IsNullOrEmpty(entity.JobCode) == false && (!string.IsNullOrEmpty(a.JobCode) && a.JobCode.Trim().ToLower() == entity.JobCode.Trim().ToLower()))));
                    if (projectEntity != null)
                    {
                        projectEntity.BudgetStatus = budgetStatus;
                        projectEntity.ModifiedAt = DateTime.UtcNow;
                        _projectDbContext.Projects.Update(projectEntity);
                    }
                    else
                    {
                        throw new Exception("Invalid PipelineCode");
                    }
                    await _projectDbContext.SaveChangesAsync();
                    return projectEntity;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return null;
        }

        public async Task<Project> AddJusticiationText(Project entity, string justificationText)
        {

            Project? projectEntity = new Project();
            if (!string.IsNullOrWhiteSpace(entity.PipelineCode))
            {
                projectEntity = _projectDbContext.Projects.FirstOrDefault(a => !string.IsNullOrEmpty(a.PipelineCode) && a.PipelineCode.Trim().ToLower() == entity.PipelineCode.Trim().ToLower()
                && ((string.IsNullOrEmpty(a.JobCode) && (string.IsNullOrEmpty(entity.JobCode) || entity.JobCode == "undefined" || entity.JobCode == "null")) ||
                (string.IsNullOrEmpty(entity.JobCode) == false && (!string.IsNullOrEmpty(a.JobCode) && a.JobCode.Trim().ToLower() == entity.JobCode.Trim().ToLower()))));

                if (projectEntity != null)
                {
                    projectEntity.JustificationToAllocate = justificationText;
                    projectEntity.ModifiedAt = DateTime.UtcNow;
                    _projectDbContext.Projects.Update(projectEntity);
                }
                else
                {
                    throw new Exception("Invalid PipelineCode");
                }
            }
            else
            {
                throw new Exception("Invalid Project code");
            }
            await _projectDbContext.SaveChangesAsync();
            return projectEntity;

        }

        public async Task<List<Project>> GetProjectsByUniqueCodes(List<KeyValuePair<string, string?>> projectUniqueCode)
        {
            var projectColl = await _projectDbContext.Projects
                .Include(i => i.ProjectRoles.Where(w => w.IsActive == true))
                .Include(i => i.ProjectRolesView.Where(w => w.IsActive == true))
                .Include(i => i.ProjectDemands.Where(w => w.IsActive == true))
                .Include(i => i.ProjectRolesView.Where(w => w.IsActive == true && !string.IsNullOrEmpty(w.User)))
                .Where(p => p.IsActive == true
                && projectUniqueCode.AsEnumerable().Any(u =>
                string.Compare(u.Key, p.PipelineCode, true) == 0 && string.Compare(u.Value, p.JobCode, true) == 0)
                ).ToListAsync();

            return await Task.FromResult(projectColl);

        }


        public async Task<Project> GetProjectByCode(string pipelineCode, string? jobCode)
        {
            var response = await _projectDbContext.Projects
                .Include(m => m.ProjectRoles.Where(s => s.IsActive == true))
                .Include(m => m.ProjectRolesView.Where(s => s.IsActive == true))
                .Include(m => m.ProjectDemands.Where(s => s.IsActive == true))
                .Where(m =>
                    m.IsActive == true
                    && ((m.JobCode == null && (jobCode == null || jobCode == "undefined" || jobCode == "null")) || (jobCode != null && m.JobCode != null && m.JobCode.Trim().ToLower() == jobCode.Trim().ToLower()))
                    && !string.IsNullOrEmpty(m.PipelineCode) && m.PipelineCode.Trim().ToLower() == pipelineCode.Trim().ToLower()
                )
                .FirstOrDefaultAsync();
            return response;
        }

        public async Task<Project> GetProjectByPipelineCode(string pipelineCode, string? jobCode)
        {
            var project = await _projectDbContext.Projects
               .Include(co => co.ProjectRoles)
               .Include(co => co.ProjectRolesView)
               .Include(co => co.ProjectCompetencies)
               .Where(p => p.IsActive == true
               && (!string.IsNullOrEmpty(p.PipelineCode)
               && p.PipelineCode.Trim().ToLower() == pipelineCode.Trim().ToLower())
               && ((string.IsNullOrEmpty(p.JobCode) && (string.IsNullOrEmpty(jobCode) || jobCode == "undefined" || jobCode == "null")) ||
               (string.IsNullOrEmpty(jobCode) == false && (!string.IsNullOrEmpty(p.JobCode) && p.JobCode.Trim().ToLower() == jobCode.Trim().ToLower()))))
               .FirstOrDefaultAsync<Project>();

            return project;
        }

        public async Task<List<Project>> GetProjectsDetailsByEmail(string emailId, int pagination, int limit, string role)
        {

            var response = await _projectDbContext.Projects
         .Include(m => m.ProjectRoles.Where(s => s.IsActive == true))
         .Include(m => m.ProjectRolesView.Where(s => s.IsActive == true))
         .Include(m => m.ProjectDemands.Where(s => s.IsActive == true))
         .Where(m =>
             m.IsActive == true &&
             m.ProjectRoles.Any(s =>
                 !string.IsNullOrEmpty(s.User) &&
                 s.User.ToLower() == emailId.Trim().ToLower() &&
                 s.ApplicationRole == role &&
                 s.IsActive == true
             )
         )
         .Skip((pagination - 1) * limit)
         .Take(limit)
         .ToListAsync();



            return response;
        }

        //existing method
        public async Task<List<Project>> GetProjectsByRequestorEmail(UserDecorator userDecorator, string requestorEmail, List<string>? bu, List<string>? offerings, List<string>? solutions,
            List<string> roles,
            string projectChargeType, List<string>? industry, List<string>? subIndustry, List<string>? clientNames, List<string>? pipelineCodes, List<string>? jobCodes, List<string>? jobNames,
            List<string>? projectStatus, string? projectType, bool? marketPlace, string? searchQuery, int pagination, int limit, GetBuExpertiesDTO? buMappingList, List<CompetencyMasterDTO>? competencyMasters)
        {
            var compareDate = DateTime.UtcNow;

            requestorEmail = !string.IsNullOrEmpty(requestorEmail) ? requestorEmail.Trim().ToLower() : string.Empty;
            searchQuery = !string.IsNullOrEmpty(searchQuery) ? searchQuery.Trim().ToLower() : string.Empty;

            bool isCurrentUserAdmin = userDecorator != null && userDecorator.roles != null &&
                                userDecorator.roles.ToList().Intersect(Domain.Constant.UserAdminRolesArray).ToList().Count > 0;
            isCurrentUserAdmin = roles != null && roles.Count > 0
            && (roles.Contains(Domain.Constant.UserRoles.Admin) || roles.Contains(Domain.Constant.UserRoles.CEOCOO) || roles.Contains(Domain.Constant.UserRoles.SystemAdmin))
            ? true
            : roles != null && roles.Count > 0 ? false : isCurrentUserAdmin;

            bool isLeader = userDecorator != null && userDecorator.roles != null &&
                                userDecorator.roles.Contains(Domain.Constant.UserRoles.Leaders);
            var buList = new List<string>();
            var offeringsList = new List<string>();
            var solutionsList = new List<string>();
            var competencyList = new List<string>();
            if (isLeader && ((roles == null) || (roles != null && roles.Contains(Domain.Constant.UserRoles.Leaders))))
            {
                if (buMappingList.BU != null && buMappingList.BU.Count > 0)
                    buList = buMappingList.BU.Where(x => !string.IsNullOrEmpty(x.Value)).Select(b => b.Value.Trim().ToLower()).Distinct().ToList();
                if (buMappingList.Offerings != null && buMappingList.Offerings.Count > 0)
                    offeringsList = buMappingList.Offerings.Where(x => !string.IsNullOrEmpty(x.Value)).Select(b => b.Value.Trim().ToLower()).Distinct().ToList();
                if (buMappingList.Solutions != null && buMappingList.Solutions.Count > 0)
                    solutionsList = buMappingList.Solutions.Where(x => !string.IsNullOrEmpty(x.Value)).Select(b => b.Value.Trim().ToLower()).Distinct().ToList();
                if (competencyMasters != null && competencyMasters.Count > 0)
                    competencyList = competencyMasters.Select(e => e.CompetencyId).Distinct().ToList();
            }

            //Get valid values with lowercase value
            if (projectStatus != null)
            {
                projectStatus = projectStatus.Where(x => !string.IsNullOrEmpty(x)).Select(x => x.Trim().ToLower()).ToList();
            }

            if (jobCodes != null)
            {
                jobCodes = jobCodes.Where(x => !string.IsNullOrEmpty(x)).Select(x => x.Trim().ToLower()).ToList();
            }
            if (jobNames != null)
            {
                jobNames = jobNames.Where(x => !string.IsNullOrEmpty(x)).Select(x => x.Trim().ToLower()).ToList();
            }

            System.Linq.Expressions.Expression<Func<Project, bool>> prjWhereClause =
                (x =>
                    (
                        x.IsActive == true
                        &&
                        (
                            isCurrentUserAdmin == true
                            || x.ProjectRolesView.Any(a => a.IsActive == true
                                && (
                                    (roles == null)
                                    || (
                                        roles != null && roles.Contains(a.ApplicationRole)
                                        && !string.IsNullOrEmpty(a.User) && a.User.Trim().ToLower() == requestorEmail.Trim().ToLower()
                                        )
                                    )
                                )
                            || !string.IsNullOrEmpty(x.bu) && buList.Contains(x.bu.Trim().ToLower())
                            || !string.IsNullOrEmpty(x.Offerings) && offeringsList.Contains(x.Offerings.Trim().ToLower())
                            || !string.IsNullOrEmpty(x.Solutions) && solutionsList.Contains(x.Solutions.Trim().ToLower())
                            || x.ProjectCompetencies.Any(c => c.IsActive == true && competencyList.Contains(c.CompetencyId))
                       )
                        &&
                        (
                            (
                                bu == null && offerings == null && clientNames == null &&//Recheck                                
                                industry == null && subIndustry == null && jobCodes == null && jobNames == null && projectStatus == null
                                && projectType == null && pipelineCodes == null
                            )
                            ||
                            (
                                (
                                    ((bu != null && (offerings == null || offerings.Count < 1) && (solutions == null || solutions.Count < 1) && !string.IsNullOrEmpty(x.bu) && bu.Contains(x.bu))
                                    || (bu != null && offerings != null && (solutions == null || solutions.Count < 1) && !string.IsNullOrEmpty(x.bu) && !string.IsNullOrEmpty(x.Offerings) && offerings.Contains(x.Offerings) && bu.Contains(x.bu)))

                                    || (bu != null && offerings != null && solutions != null && !string.IsNullOrEmpty(x.bu) && !string.IsNullOrEmpty(x.Offerings) && !string.IsNullOrEmpty(x.Solutions)
                                        && bu.Contains(x.bu) && offerings.Contains(x.Offerings) && solutions.Contains(x.Solutions))
                                    || (clientNames != null && !string.IsNullOrEmpty(x.ClientName) && clientNames.Contains(x.ClientName))

                                    || (industry != null && subIndustry == null && !string.IsNullOrEmpty(x.Industry) && industry.Contains(x.Industry))
                                    || (industry != null && subIndustry != null && !string.IsNullOrEmpty(x.Industry) && !string.IsNullOrEmpty(x.Subindustry) && industry.Contains(x.Industry) && subIndustry.Contains(x.Subindustry))
                                    || (pipelineCodes != null && !string.IsNullOrEmpty(x.PipelineCode) && pipelineCodes.Contains(x.PipelineCode.Trim().ToLower()))
                                    || (jobCodes != null && !string.IsNullOrEmpty(x.JobCode) && jobCodes.Contains(x.JobCode.ToLower().Trim()))
                                    || (jobNames != null && !string.IsNullOrEmpty(x.JobName) && jobNames.Contains(x.JobName.ToLower().Trim()))
                                    || (projectStatus != null && x.PipelineStatus != null &&
                                        projectStatus.Contains(x.PipelineStatus.Trim().ToLower()))
                                    || (projectType != null && projectType.Trim().ToLower() == ProjectClosureStatus.OPEN.Trim().ToLower() && x.ProjectClosureState != true)
                                    || (projectType != null && projectType.Trim().ToLower() == ProjectClosureStatus.CLOSED.Trim().ToLower() && (x.ProjectClosureState == true))
                                    || (roles != null && x.ProjectRolesView.Any(r => (roles.Contains(r.ApplicationRole + "")) && r.User == requestorEmail && r.IsActive == true))
                                )
                            )
                        )
                        && (marketPlace == null || (x.IsPublishedToMarketPlace == true ? true : false) == marketPlace)
                        //charge type will work with and condition with all other conditions
                        && (projectChargeType == null || (projectChargeType != null && !string.IsNullOrEmpty(x.ChargableType) && projectChargeType.Trim().ToLower() == x.ChargableType.Trim().ToLower()))
                    ) &&
                    (
                        string.IsNullOrEmpty(searchQuery)
                        ||
                        (
                            !string.IsNullOrEmpty(searchQuery) &&
                            (
                                (!string.IsNullOrEmpty(x.PipelineCode) && EF.Functions.Like(x.PipelineCode.Trim().ToLower(), string.Format("%{0}%", searchQuery))) ||
                                (!string.IsNullOrEmpty(x.JobCode) && EF.Functions.Like(x.JobCode.Trim().ToLower(), string.Format("%{0}%", searchQuery))) ||
                                (!string.IsNullOrEmpty(x.PipelineName) && EF.Functions.Like(x.PipelineName.Trim().ToLower(), string.Format("%{0}%", searchQuery))) ||
                                (!string.IsNullOrEmpty(x.JobName) && EF.Functions.Like(x.JobName.Trim().ToLower(), string.Format("%{0}%", searchQuery)))
                            )
                        )
                    )
                );

            var list = await _projectDbContext.Projects
               .Select
               (a => new Project
               {
                   JobName = a.JobName,
                   JobCode = a.JobCode,
                   PipelineCode = a.PipelineCode,
                   ClientName = a.ClientName,
                   BudgetStatus = a.BudgetStatus,
                   PipelineName = a.PipelineName,
                   bu = a.bu,
                   BUId = a.BUId,
                   Sme = a.Sme,//Recheck
                   Smeg = a.Smeg,//Recheck
                   Offerings = a.Offerings,
                   OfferingsId = a.OfferingsId,
                   Solutions = a.Solutions,
                   SolutionsId = a.SolutionsId,
                   IsActive = a.IsActive,
                   Id = a.Id,
                   JobId = a.JobId,
                   StartDate = a.StartDate.Value.Date,
                   EndDate = a.EndDate.Value.Date,
                   CreatedAt = a.CreatedAt,
                   ModifiedAt = a.ModifiedAt,
                   ProjectAllocationStatus = a.ProjectAllocationStatus,
                   Location = a.Location,
                   ChargableType = a.ChargableType,
                   CreatedBy = a.CreatedBy,
                   Description = a.Description,
                   Expertise = a.Expertise,//Recheck
                   Industry = a.Industry,
                   IsPublishedToMarketPlace = a.IsPublishedToMarketPlace,
                   IsRequisitionCreationallowed = a.IsRequisitionCreationallowed,
                   ModifiedBy = a.ModifiedBy,
                   PipelineStage = a.PipelineStage,
                   ProjectFulFilledDemands = a.ProjectFulFilledDemands,
                   ProjectActivationStatus = a.ProjectActivationStatus,
                   ProjectClosureState = a.ProjectClosureState,
                   ProjectType = a.ProjectType,
                   RevenueUnit = a.RevenueUnit,//Recheck
                   Subindustry = a.Subindustry,
                   PipelineStatus = a.PipelineStatus,
                   IsRollover = a.IsRollover,
                   RolloverDays = a.RolloverDays,
                   ProjectRoles = a.ProjectRoles.Where(x => x.IsActive == true
                   && (!string.IsNullOrEmpty(x.User) && x.User.Trim().ToLower() == requestorEmail.Trim().ToLower())
                   ).ToList(),
                   ProjectRolesView = a.ProjectRolesView.Where(x => x.IsActive == true
                   && (!string.IsNullOrEmpty(x.User) && x.User.Trim().ToLower() == requestorEmail.Trim().ToLower())
                   ).ToList(),
                   ProjectRequisitionAllocations = a.ProjectRequisitionAllocations,
                   ProjectCompetencies = a.ProjectCompetencies.Where(x => x.IsActive == true).ToList(),
                   MarketPlaceExpirationDate= a.MarketPlaceExpirationDate,
                   IsConfidential = a.IsConfidential
               }
               )
               .Where
                (prjWhereClause)
               .OrderBy(p =>
               //first show current running projects> future dated project then past dates projest 
               (p.StartDate.HasValue && p.EndDate.HasValue && p.StartDate <= DateTime.UtcNow && p.EndDate >= DateTime.UtcNow) ? 0 :
                   (
                       (p.StartDate.HasValue && p.StartDate > DateTime.UtcNow) ? 1 : 2
                   )
               )
               .ThenBy(p => p.StartDate)
               .Skip((pagination - 1) * limit).Take(limit)
               .ToListAsync();

            //if (list != null && list.Count > 0)
            //{
            //    var listTotalCount = _projectDbContext.Projects
            //      .Where(prjWhereClause)
            //      .Count();
            //    list.First().Total = listTotalCount;

            //}

            return list;
        }



        public async Task RemoveProjectCompetency(List<RefreshProjectCompetencyRequestDTO> request)
        {
            var distinctPipelineCodeJobCode = request.GroupBy(r => new { r.PipelineCode, r.JobCode }).Select(g => new RefreshProjectCompetencyRequestDTO
            {
                PipelineCode = g.Key.PipelineCode,
                JobCode = g.Key.JobCode
            })
            .ToList();
            foreach (var req in request)
            {
                var projCompetency = await _projectDbContext.ProjectCompetency
                    .Include(p => p.Project)
                    .Where(pc => (string.IsNullOrEmpty(req.PipelineCode) && string.IsNullOrEmpty(pc.Project.PipelineCode) ||
                                  !string.IsNullOrEmpty(req.PipelineCode) && pc.Project.PipelineCode.ToLower().Trim() == req.PipelineCode.ToLower().Trim()) &&
                                 (string.IsNullOrEmpty(req.JobCode) && string.IsNullOrEmpty(pc.Project.JobCode) ||
                                  !string.IsNullOrEmpty(req.JobCode) && pc.Project.JobCode.ToLower().Trim() == req.JobCode.ToLower().Trim()) &&
                                  pc.IsActive == true)
                    .ToListAsync();
                _projectDbContext.ProjectCompetency.RemoveRange(projCompetency);
            }
            await _projectDbContext.SaveChangesAsync();
        }
        public async Task<List<ProjectCompetency>> AddProjectCompetencies(List<AddCompetencyRequestDTO> request)
        {
            List<ProjectCompetency> result = new();
            foreach (var item in request)
            {
                var project = await GetProjectByCode(item.PipelineCode, item.JobCode);
                ProjectCompetency projectCompetency = new()
                {
                    ProjectId = (long)project.Id,
                    Competency = item.Competency,
                    CompetencyId = item.CompetencyId,
                    IsActive = true,
                    ModifiedAt = DateTime.UtcNow,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "abc@gmail.com",
                    ModifiedBy = "bcd@gmail.com",
                };
                var resp = _projectDbContext.ProjectCompetency.Add(projectCompetency);
                result.Add(resp.Entity);
            }
            await _projectDbContext.SaveChangesAsync();
            return result;
        }
        public static ProjectCompetency AddProjectCompetency()
        {
            return null;
        }
        public async Task<Project> GetProjectById(long id)
        {
            return await _projectDbContext.Projects.Where(m => m.IsActive == true && m.Id == id).FirstAsync<Project>();
        }

        public async Task<Project> GetProjectDetailsForRequestor(string pipelineCode, string? jobCode)
        {
            var res = await _projectDbContext.Projects
            .Include(pd => pd.ProjectDemands).ThenInclude(ds => ds.ProjectDemandSkills)
            .Where(m => (!string.IsNullOrEmpty(m.PipelineCode) && m.PipelineCode.Trim().ToLower() == pipelineCode.Trim().ToLower())
            && ((string.IsNullOrEmpty(m.JobCode) && (string.IsNullOrEmpty(jobCode) || jobCode == "undefined" || jobCode == "null")) ||
            (string.IsNullOrEmpty(jobCode) == false && (!string.IsNullOrEmpty(m.JobCode) && m.JobCode.Trim().ToLower() == jobCode.Trim().ToLower())))
            && m.IsActive == true)
            .Select(a => new Project
            {
                Id = a.Id,
                ProjectDemands = a.ProjectDemands.Where(x => x.IsActive == true).ToList(),
                ProjectRoles = a.ProjectRoles.Where(x => x.IsActive == true).ToList(),
                ProjectRolesView = a.ProjectRolesView.Where(x => x.IsActive == true).ToList(),
                ProjectCompetencies = a.ProjectCompetencies.Where(x => x.IsActive == true).ToList(),
                JobCode = a.JobCode,
                JobName = a.JobName,
                JobLocation = a.JobLocation,
                PipelineCode = a.PipelineCode,
                PipelineName = a.PipelineName,
                ClientName = a.ClientName,
                ClientGroup = a.ClientGroup,
                bu = a.bu,
                BUId = a.BUId,
                Expertise = a.Expertise,//Recheck
                Sme = a.Sme,//Recheck
                Smeg = a.Smeg,//Recheck
                Offerings = a.Offerings,
                OfferingsId = a.OfferingsId,
                Solutions = a.Solutions,
                SolutionsId = a.SolutionsId,
                StartDate = a.StartDate,
                EndDate = a.EndDate,
                Description = a.Description,
                JobId = a.JobId,
                ProjectAllocationStatus = a.ProjectAllocationStatus,
                Location = a.Location,
                PipelineStage = a.PipelineStage,
                ProjectType = a.ProjectType,
                ChargableType = a.ChargableType,
                RevenueUnit = a.RevenueUnit,//Recheck
                Industry = a.Industry,
                BudgetStatus = a.BudgetStatus,
                ProjectFulFilledDemands = a.ProjectFulFilledDemands,
                IsPublishedToMarketPlace = a.IsPublishedToMarketPlace,
                IsRequisitionCreationallowed = a.IsRequisitionCreationallowed,
                IsActive = a.IsActive,
                CreatedBy = a.CreatedBy,
                ModifiedBy = a.ModifiedBy,
                ProjectActivationStatus = a.ProjectActivationStatus,
                ProjectClosureState = a.ProjectClosureState,
                CreatedAt = a.CreatedAt,
                ModifiedAt = a.ModifiedAt,
                CreatedDate = a.CreatedDate,
                Subindustry = a.Subindustry,
                PipelineStatus = a.PipelineStatus,
                IsRollover = a.IsRollover,
                RolloverDays = a.RolloverDays,
                JustificationToAllocate = a.JustificationToAllocate,
                IsConfidential = a.IsConfidential
            }).FirstOrDefaultAsync<Project>();

            return res;
        }

        public async Task<Project> GetProjectDetailsByCode(string pipelineCode, string? jobCode)
        {
            return await _projectDbContext.Projects.Where(m => m.IsActive == true
            && !string.IsNullOrEmpty(m.PipelineCode) && m.PipelineCode.Trim().ToLower() == pipelineCode.Trim().ToLower()
             && ((string.IsNullOrEmpty(m.JobCode) && (string.IsNullOrEmpty(jobCode) || jobCode == "undefined" || jobCode == "null")) ||
            (string.IsNullOrEmpty(jobCode) == false && (!string.IsNullOrEmpty(m.JobCode) && m.JobCode.Trim().ToLower() == jobCode.Trim().ToLower())))
            )
            .Include(e => e.ProjectRolesView).FirstOrDefaultAsync<Project>();
        }

        public async Task<Project> GetProjectDetailsForEmployee(string pipelineCode, string? jobCode)
        {
            return await _projectDbContext.Projects.Select(a => new Project
            {
                Id = a.Id,
                ProjectDemands = a.ProjectDemands,
                ProjectRoles = a.ProjectRoles,
                ProjectRolesView = a.ProjectRolesView,
                JobCode = a.JobCode,
                PipelineCode = a.PipelineCode,
                PipelineName = a.PipelineName,
                JobName = a.JobName,
                ClientName = a.ClientName,
                ClientGroup = a.ClientGroup,
                Expertise = a.Expertise,//Recheck
                Sme = a.Sme,//Recheck
                Smeg = a.Smeg,//Recheck
                bu = a.bu,
                BUId = a.BUId,
                Offerings = a.Offerings,
                OfferingsId = a.OfferingsId,
                Solutions = a.Solutions,
                SolutionsId = a.SolutionsId,
                StartDate = a.StartDate,
                EndDate = a.EndDate,
                Description = a.Description,
                ProjectAllocationStatus = a.ProjectAllocationStatus,
                Location = a.Location,
                PipelineStage = a.PipelineStage,
                ProjectType = a.ProjectType,
                JobId = a.JobId,
                ChargableType = a.ChargableType,
                RevenueUnit = a.RevenueUnit,//Recheck
                Industry = a.Industry,
                BudgetStatus = a.BudgetStatus,
                ProjectFulFilledDemands = a.ProjectFulFilledDemands,
                IsPublishedToMarketPlace = a.IsPublishedToMarketPlace,
                IsRequisitionCreationallowed = a.IsRequisitionCreationallowed,
                IsActive = a.IsActive,
                CreatedBy = a.CreatedBy,
                ModifiedBy = a.ModifiedBy,
                CreatedAt = a.CreatedAt,
                Subindustry = a.Subindustry,
                PipelineStatus = a.PipelineStatus,
                ModifiedAt = a.ModifiedAt,
                IsConfidential = a.IsConfidential

            }).Where(m => m.IsActive == true
            && !string.IsNullOrEmpty(m.PipelineCode) && m.PipelineCode.Trim().ToLower() == pipelineCode.Trim().ToLower()
             && ((string.IsNullOrEmpty(m.JobCode) && (string.IsNullOrEmpty(jobCode) || jobCode == "undefined" || jobCode == "null")) ||
            (string.IsNullOrEmpty(jobCode) == false && (!string.IsNullOrEmpty(m.JobCode) && m.JobCode.Trim().ToLower() == jobCode.Trim().ToLower())))
            ).FirstOrDefaultAsync<Project>();

        }

        public async Task<ProjectRoles[]> AddProjectUserWithRole(ProjectRoles[] entities)
        {
            foreach (var entity in entities)
            {
                var lst = await _projectDbContext.ProjectRoles.Where(a => a.User == entity.User && a.ProjectId == entity.ProjectId && a.Role == entity.Role && a.IsActive == true).ToListAsync();
                if (lst.Count == 0)
                {
                    entity.ApplicationRole = MappingRoleApplication[entity.Role];

                    await _projectDbContext.Set<ProjectRoles>().AddRangeAsync(entity);
                }
            }
            await _projectDbContext.SaveChangesAsync();
            return entities;
        }

        public async Task<ProjectRoles[]> RemoveProjectUserWithRole(ProjectRoles[] entities)
        {
            foreach (var entity in entities)
            {
                var lstProjectRoles = await _projectDbContext.ProjectRoles.Where(a => a.User == entity.User && a.ProjectId == entity.ProjectId
                && a.Role == entity.Role && a.IsActive == true).ToListAsync();
                if (lstProjectRoles.Count > 0)
                {
                    foreach (var item in lstProjectRoles)
                    {
                        item.IsActive = false;
                    }
                    _projectDbContext.ProjectRoles.UpdateRange(lstProjectRoles.ToArray());
                }
            }
            await _projectDbContext.SaveChangesAsync();
            return entities;
        }

        /// <summary>
        /// Get Project data for employee listing view
        /// </summary>
        /// <param name="codes"></param>
        /// <returns></returns>
        public async Task<List<Project>> GetEmployeeListingProjectData(List<KeyValuePair<string, string>> codes)
        {
            var result = (from project in _projectDbContext.Projects.AsEnumerable()
                          join code in codes.AsEnumerable()
                          on Convert.ToString(project.PipelineCode + "-" + project.JobCode).Trim().ToLower() equals Convert.ToString(code.Value + "-" + code.Key).Trim().ToLower()
                          where project.IsActive == true
                          select project).ToList<Project>();

            return await Task.FromResult(result);
        }
        public async Task<List<Project>> GetMultipleProjectByCodes(List<KeyValuePair<string, string>> projectCodes)
        {
            var result = _projectDbContext.Projects
                  .Include(a => a.ProjectDemands.Where(p => p.IsActive == true))
                  .Include(a => a.ProjectRoles.Where(p => p.IsActive == true))
                  .Include(a => a.ProjectRolesView.Where(p => p.IsActive == true))
              .AsEnumerable()
                  .Where(m => projectCodes.AsEnumerable().Any(p => string.Compare(p.Key, m.PipelineCode, true) == 0
                  && ((string.IsNullOrEmpty(m.JobCode) && string.IsNullOrEmpty(p.Value)) ||
                  (string.IsNullOrEmpty(p.Value) == false && string.Compare(m.JobCode, p.Value, true) == 0))))
                  .ToList<Project>();
            return await Task.FromResult(result);
        }
        /// <summary>
        /// Get the Reviewer Emails By PipelineCode
        /// </summary>
        /// <param name="pipelineCode"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<List<ProjectRolesView>> GetReviewerEmailsByPipelineCode(string pipelineCode, string? jobCode)
        {
            var project = await _projectDbContext.Projects
                                                 .Where(d => !string.IsNullOrEmpty(d.PipelineCode) && d.PipelineCode.Trim().ToLower() == pipelineCode.Trim().ToLower()
                                                 && ((string.IsNullOrEmpty(d.JobCode) && (string.IsNullOrEmpty(jobCode) || jobCode == "undefined" || jobCode == "null")) ||
                                                 (string.IsNullOrEmpty(jobCode) == false && (!string.IsNullOrEmpty(d.JobCode) && d.JobCode.Trim().ToLower() == jobCode.Trim().ToLower())))
                                                 && d.IsActive == true)
                                                 .Include(d => d.ProjectRoles)
                                                 .Include(d => d.ProjectRolesView.Where(x => x.IsActive == true))
                                                 .FirstOrDefaultAsync();
            if (project is null)
            {
                return null;
            }
            var projectRoles = project.ProjectRolesView;
            var IsJobType = !string.IsNullOrEmpty(project.JobCode);

            List<string> ChargableReviewers = new List<string>() { UserRoles.CSP };
            List<string> NonChargableReviewers = new List<string>() { UserRoles.SMEGLeader };
            List<string> PipelineReviewers = new List<string>() { UserRoles.ProposedCSP };

            if (IsJobType && project.ChargableType.Trim().ToLower() == ProjectChargeType.CHARGEABLE.Trim().ToLower())
            {
                var userRoles = projectRoles.Where(d => ChargableReviewers.Any(t => t.Trim().ToLower() == d.Role.Trim().ToLower()) == true).ToList();
                return userRoles;
            }
            else if (IsJobType && project.ChargableType.Trim().ToLower() == ProjectChargeType.NON_CHARGABLE.Trim().ToLower())
            {
                var userRoles = projectRoles.Where(d => NonChargableReviewers.Any(t => t.Trim().ToLower() == d.Role.Trim().ToLower()) == true).ToList();
                return userRoles;
            }
            else if (!IsJobType)
            {
                var userRoles = projectRoles.Where(d => PipelineReviewers.Any(t => t.Trim().ToLower() == d.Role.Trim().ToLower()) == true).ToList();
                return userRoles;
            }
            return null;
        }

        /// <summary>
        /// GetAllProjectRolesByCodes
        /// </summary>
        /// <param name="pipelineCodes"></param>
        /// <returns></returns>
        public async Task<List<ProjectRolesView>> GetAllProjectRolesByCodes(KeyValuePair<string, string?> pipelineCode)
        {
            var project = await _projectDbContext.Projects
                                                .Where(d => !string.IsNullOrEmpty(d.PipelineCode) && d.PipelineCode.Trim().ToLower() == pipelineCode.Key.Trim().ToLower()
                                                  && ((string.IsNullOrEmpty(d.JobCode) && string.IsNullOrEmpty(pipelineCode.Value)) ||
                                                 (string.IsNullOrEmpty(pipelineCode.Value) == false && (!string.IsNullOrEmpty(d.JobCode) && d.JobCode.Trim().ToLower() == pipelineCode.Value.Trim().ToLower())))
                                                 && d.IsActive == true)
                                                 .Include(d => d.ProjectRolesView.Where(x => x.IsActive == true))
                                                 .FirstOrDefaultAsync();

            List<ProjectRolesView> projectRoles = null;
            if (project != null && project.ProjectRolesView != null)
            {
                projectRoles = project.ProjectRolesView.ToList();
            }
            else
            {
                projectRoles = new List<ProjectRolesView>();
            }
            return projectRoles;
        }

        /// <summary>
        /// Get the Requestors Email By Pipeline Code
        /// </summary>
        /// <param name="pipelineCode"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<List<ProjectRolesView>> GetRequestorEmailsByPipelineCode(string pipelineCode, string? jobCode)
        {
            var project = await _projectDbContext.Projects
                                                 .Where(d => !string.IsNullOrEmpty(d.PipelineCode) && d.PipelineCode.Trim().ToLower() == pipelineCode.Trim().ToLower()
                                                  && ((string.IsNullOrEmpty(d.JobCode) && (string.IsNullOrEmpty(jobCode) || jobCode == "undefined" || jobCode == "null")) ||
                                                 (string.IsNullOrEmpty(jobCode) == false && (!string.IsNullOrEmpty(d.JobCode) && d.JobCode.Trim().ToLower() == jobCode.Trim().ToLower())))
                                                 && d.IsActive == true)
                                                 .Include(d => d.ProjectRoles.Where(x => x.IsActive == true))
                                                 .Include(d => d.ProjectRolesView.Where(x => x.IsActive == true))
                                                 .FirstOrDefaultAsync();
            if (project is null)
            {
                return null;
            }
            var projectRoles = project.ProjectRolesView;
            var IsJobType = !string.IsNullOrEmpty(project.JobCode);
            List<string> ChargableRequestor = new List<string>() { UserRoles.EO, UserRoles.EngagementLeader };
            List<string> NonChargableRequestor = new List<string>() { UserRoles.JobManager };
            List<string> PipelineRequestor = new List<string>() { UserRoles.EO, UserRoles.ProposedEL };//todo check with ankita

            if (IsJobType && project.ChargableType.Trim().ToLower() == ProjectChargeType.CHARGEABLE.Trim().ToLower())
            {
                var userRoles = projectRoles.Where(d => ChargableRequestor.Any(t => t.Trim().ToLower() == d.Role.Trim().ToLower()) == true).ToList();
                return userRoles;
            }
            else if (IsJobType && project.ChargableType.Trim().ToLower() == ProjectChargeType.NON_CHARGABLE.Trim().ToLower())
            {
                var userRoles = projectRoles.Where(d => NonChargableRequestor.Any(t => t.Trim().ToLower() == d.Role.Trim().ToLower()) == true).ToList();
                return userRoles;
            }
            else if (!IsJobType)
            {
                var userRoles = projectRoles.Where(d => PipelineRequestor.Any(t => t.Trim().ToLower() == d.Role.Trim().ToLower()) == true).ToList();
                return userRoles;
            }
            return null;
        }
        public async Task<List<ProjectRoles>> GetRequestorEmailForAllocationWorkflow(string pipelineCode, string? jobCode, string workflowStartedBy)
        {
            var project = await _projectDbContext.Projects
                                                .Where(d => !string.IsNullOrEmpty(d.PipelineCode) && d.PipelineCode.Trim().ToLower() == pipelineCode.Trim().ToLower()
                                                 && ((string.IsNullOrEmpty(d.JobCode) && (string.IsNullOrEmpty(jobCode) || jobCode == "undefined" || jobCode == "null")) ||
                                                (string.IsNullOrEmpty(jobCode) == false && (!string.IsNullOrEmpty(d.JobCode) && d.JobCode.Trim().ToLower() == jobCode.Trim().ToLower())))
                                                && d.IsActive == true)
                                                .Include(d => d.ProjectRoles.Where(x => x.IsActive == true))
                                                .Include(d => d.ProjectRolesView.Where(x => x.IsActive == true))
                                                .FirstOrDefaultAsync();
            if (project == null)
            {
                return null;
            }
            //do not change projectrole to projectrolesview--deepesh
            var projectRoles = project.ProjectRoles.Where(d => d.IsActive == true);
            var workflowStartedByRole = projectRoles.Where((d) =>
                                                                   !string.IsNullOrEmpty(d.User) && d.User.Equals(workflowStartedBy, StringComparison.OrdinalIgnoreCase) ||
                                                                   !string.IsNullOrEmpty(d.DelegateEmail) && d.DelegateEmail.Equals(workflowStartedBy, StringComparison.OrdinalIgnoreCase));
            List<ProjectRoles> projectRoleResponse = new List<ProjectRoles>();
            if (workflowStartedByRole == null)
            {
                return null;
            }
            bool isAdditionalElOrAdditionalDelegate = workflowStartedByRole.Any(d => d.Role.Equals(UserRoles.AdditionalEl));
            if (isAdditionalElOrAdditionalDelegate)
            {
                projectRoleResponse.Add(workflowStartedByRole.Where(e => e.Role.Equals(UserRoles.AdditionalEl)).FirstOrDefault());
                return projectRoleResponse;
            }
            else
            {
                List<string> ChargableRequestor = new List<string>() { UserRoles.EngagementLeader, UserRoles.Delegate };
                List<string> NonChargableRequestor = new List<string>() { UserRoles.JobManager, UserRoles.Delegate };
                List<string> PipelineRequestor = new List<string>() { UserRoles.EO, UserRoles.ProposedEL, UserRoles.Delegate };//todo check with ankita
                if (!string.IsNullOrEmpty(project.JobCode) && !string.IsNullOrEmpty(project.ChargableType) && project.ChargableType.Equals(ProjectChargeType.CHARGEABLE, StringComparison.OrdinalIgnoreCase))
                {
                    var chargableReq = projectRoles.Where((pr) => ChargableRequestor.Any((cr) => cr.Equals(pr.Role, StringComparison.OrdinalIgnoreCase)));
                    return chargableReq.ToList();
                }
                else if (!string.IsNullOrEmpty(project.JobCode) && !string.IsNullOrEmpty(project.ChargableType) && project.ChargableType.Equals(ProjectChargeType.NON_CHARGABLE, StringComparison.OrdinalIgnoreCase))
                {
                    var nonChargableReq = projectRoles.Where((pr) => NonChargableRequestor.Any((ncr) => ncr.Equals(pr.Role, StringComparison.OrdinalIgnoreCase)));
                    return nonChargableReq.ToList();
                }
                else if (string.IsNullOrEmpty(project.JobCode))
                {
                    var pipelineReq = projectRoles.Where((pr) => PipelineRequestor.Any((piper) => piper.Equals(pr.Role, StringComparison.OrdinalIgnoreCase)));
                    return pipelineReq.ToList();
                }
            }
            return null;
        }
        public async Task<List<Project>> GetRequestorEmailsListByPipelineCode(List<KeyValuePair<string, string?>> pipelineCodes)
        {
            var LowerCasePipelineCode = new List<KeyValuePair<string, string?>>();
            List<string> resourceRequestors = new List<string>();
            foreach (var pipelineCode in pipelineCodes)
            {
                var pc = pipelineCode.Key.Trim().ToLower();
                var jc = string.IsNullOrEmpty(pipelineCode.Value) ? null : pipelineCode.Value.Trim().ToLower();
                LowerCasePipelineCode.Add(new KeyValuePair<string, string?>(pc, jc));
            }
            var RequestorRoles = new List<string>() { UserRoles.EngagementLeader.Trim().ToLower() };

            var projectList = _projectDbContext.Projects
            .Include(e => e.ProjectRolesView.Where(d => RequestorRoles.Contains(d.Role.Trim().ToLower()) && d.IsActive == true))
            .AsEnumerable()
            .Where(d => LowerCasePipelineCode.AsEnumerable().Any(m => (!string.IsNullOrEmpty(d.PipelineCode) && m.Key.Trim().ToLower() == d.PipelineCode.Trim().ToLower())
            && ((string.IsNullOrEmpty(d.JobCode) && string.IsNullOrEmpty(m.Value)) ||
            (!string.IsNullOrEmpty(m.Value) && !string.IsNullOrEmpty(d.JobCode) && d.JobCode.Trim().ToLower() == m.Value.Trim().ToLower())))
            && d.IsActive == true)
            .ToList();
            return await Task.FromResult(projectList);
        }

        public async Task<List<Project>> GetAllProjectDetailsForMarketPlace()
        {
            try
            {
                return await _projectDbContext.Projects
                .Include(pd => pd.ProjectRoles)
                .Include(pd => pd.ProjectRolesView)
                .Include(pd => pd.ProjectDemands).ThenInclude(pd => pd.ProjectDemandSkills)
                .Where(m => m.IsPublishedToMarketPlace == false && m.IsActive == true).ToListAsync();
            }
            catch (Exception ex)
            {
                throw;
            }


            // todo send only active record for project role and demand 
        }
        public async Task<Project> SetIsRequisitionCreationAllowed(bool IsRequisitionCreationallowed, string pipelineCode, string? jobCode)
        {
            try
            {
                var _project = await _projectDbContext.Projects.Where(m => !string.IsNullOrEmpty(m.PipelineCode) && m.PipelineCode.Trim().ToLower() == pipelineCode.Trim().ToLower()
                && ((string.IsNullOrEmpty(m.JobCode) && string.IsNullOrEmpty(jobCode)) ||
                (string.IsNullOrEmpty(jobCode) == false && (!string.IsNullOrEmpty(m.JobCode) && m.JobCode.Trim().ToLower() == jobCode.Trim().ToLower()))))
                .FirstOrDefaultAsync<Project>();
                if (_project != null && IsRequisitionCreationallowed != null)
                {
                    _project.IsRequisitionCreationallowed = IsRequisitionCreationallowed;
                }
                _projectDbContext.Projects.Update(_project);
                await _projectDbContext.SaveChangesAsync();
                return _project;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<Project>> UpdatePublishedToMarketPlace(List<UpdatePublishedToMarketPlaceDTO> updatePublishedToMarketPlaceDTO)
        {
            try
            {
                List<Project> project = new List<Project>();

                foreach (var item in updatePublishedToMarketPlaceDTO)
                {
                    var _project = await _projectDbContext.Projects.Where(m => m.IsActive == true &&
                                           !string.IsNullOrEmpty(m.PipelineCode) && m.PipelineCode.Trim().ToLower() == item.PipelineCode.Trim().ToLower()
                                            && ((string.IsNullOrEmpty(m.JobCode) && string.IsNullOrEmpty(item.JobCode)) ||
                                            (string.IsNullOrEmpty(item.JobCode) == false && (!string.IsNullOrEmpty(m.JobCode) && m.JobCode.Trim().ToLower() == item.JobCode.Trim().ToLower()))))
                                            .FirstOrDefaultAsync<Project>();

                    if (_project != null && item.IsPublishedToMarketPlace != null)
                    {
                        _project.IsPublishedToMarketPlace = item.IsPublishedToMarketPlace;
                        _project.PublishedToMarketPlaceDate = DateTime.UtcNow.ToLocalTime().Date;
                        _project.MarketPlaceExpirationDate = item.MarketPlaceExpirationDate;

                        _projectDbContext.Projects.Update(_project);
                        await _projectDbContext.SaveChangesAsync();

                        project.Add(_project);
                    }
                    else
                    {
                        Console.Write("project does not exist.");
                    }

                }

                return project;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<List<FieldForMarketPlace>> GetFieldForMarketPlace()
        {
            try
            {
                return await _projectDbContext.FieldForMarketPlaces.Where(m => m.IsActive == true).ToListAsync();

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<FieldForMarketPlace> CreateOrUpdateActiveFieldForMarketPlace(string InternalName, string DisplayName, bool IsActive)
        {
            var entity = new FieldForMarketPlace()
            {
                InternalName = InternalName,
                DisplayName = DisplayName,
                IsActive = IsActive,
            };

            var _fieldForMarketPlaces = await _projectDbContext.FieldForMarketPlaces
                .Where(m => m.InternalName == InternalName && m.DisplayName == DisplayName).FirstOrDefaultAsync<FieldForMarketPlace>();
            if (_fieldForMarketPlaces != null)
            {
                _fieldForMarketPlaces.InternalName = InternalName;
                _fieldForMarketPlaces.DisplayName = DisplayName;
                _fieldForMarketPlaces.IsActive = IsActive;
                await _projectDbContext.SaveChangesAsync();
            }
            else
            {

                await _projectDbContext.Set<FieldForMarketPlace>().AddAsync(entity);
                await _projectDbContext.SaveChangesAsync();
            }
            return entity;
        }

        public async Task<List<PublishedFieldForMarketPlace>> GetPublishedFieldForMarketPlace(string PipelineCode, string? JobCode)
        {
            try
            {
                return await _projectDbContext.PublishedFieldForMarketPlaces.
                    Where(m => !string.IsNullOrEmpty(m.PipelineCode) && m.PipelineCode.Trim().ToLower() == PipelineCode.Trim().ToLower()
                    && ((string.IsNullOrEmpty(m.JobCode) && (string.IsNullOrEmpty(JobCode))) ||
                    (string.IsNullOrEmpty(JobCode) == false && !string.IsNullOrEmpty(m.JobCode) && m.JobCode.Trim().ToLower() == JobCode.Trim().ToLower()))
                    && m.IsActive == true
                    ).ToListAsync();

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<PublishedFieldForMarketPlace> CreateOrUpdatePublishedFieldForMarketPlace(string PipelineCode, string? JobCode, string FieldName, bool IsActive)
        {
            var entity = new PublishedFieldForMarketPlace()
            {
                PipelineCode = PipelineCode,
                JobCode = JobCode,
                FieldName = FieldName,
                IsActive = IsActive
            };

            var _PublishedfieldForMarketPlaces = await _projectDbContext.PublishedFieldForMarketPlaces
                .Where(m =>
                !string.IsNullOrEmpty(m.PipelineCode) && m.PipelineCode.Trim().ToLower() == PipelineCode.Trim().ToLower()
                && ((string.IsNullOrEmpty(m.JobCode) && (string.IsNullOrEmpty(JobCode) || JobCode == "undefined" || JobCode == "null")) ||
                (string.IsNullOrEmpty(JobCode) == false && (!string.IsNullOrEmpty(m.JobCode) && m.JobCode.Trim().ToLower() == JobCode.Trim().ToLower())))
                && m.FieldName == FieldName).FirstOrDefaultAsync<PublishedFieldForMarketPlace>();
            if (_PublishedfieldForMarketPlaces != null)
            {
                _PublishedfieldForMarketPlaces.PipelineCode = PipelineCode;
                _PublishedfieldForMarketPlaces.JobCode = JobCode;
                _PublishedfieldForMarketPlaces.FieldName = FieldName;
                _PublishedfieldForMarketPlaces.IsActive = IsActive;
                await _projectDbContext.SaveChangesAsync();
            }
            else
            {

                await _projectDbContext.Set<PublishedFieldForMarketPlace>().AddAsync(entity);
                await _projectDbContext.SaveChangesAsync();

            }

            return entity;

        }
        public async Task<List<ProjectBudget>> GetProjectBudget(string pipelineCode, string? jobCode)
        {
            return await _projectDbContext.ProjectBudget
               .Where(p =>
                        p.IsActive == true
                        && ((string.IsNullOrEmpty(p.JobCode) && (string.IsNullOrEmpty(jobCode) || jobCode == "undefined" || jobCode == "null")) ||
                            (string.IsNullOrEmpty(jobCode) == false && (!string.IsNullOrEmpty(p.JobCode) && p.JobCode.Trim().ToLower() == jobCode.Trim().ToLower())))
                        && !string.IsNullOrEmpty(p.PipelineCode) && pipelineCode.Trim().ToLower() == p.PipelineCode.Trim().ToLower()
               )
               .ToListAsync();
        }

        public async Task<Boolean> AddUpdateProjectRequisitionAllocation(List<ProjectRequisitionAllocationRequestDTO> projectRequisitionAllocationRequestDTOs, string updatedBy)
        {
            foreach (var item in projectRequisitionAllocationRequestDTOs)
            {
                try
                {
                    var existingProjectRequisitionAllocationDetails = _projectDbContext.ProjectRequisitionAllocation
                        .Where(m =>
                        !string.IsNullOrEmpty(m.PipelineCode) && m.PipelineCode.Trim().ToLower().Equals(item.pipelineCode.Trim().ToLower())
                        && ((string.IsNullOrEmpty(m.JobCode) && (string.IsNullOrEmpty(item.jobCode) || item.jobCode == "undefined" || item.jobCode == "null")) ||
                        (string.IsNullOrEmpty(item.jobCode) == false && (!string.IsNullOrEmpty(m.JobCode) && m.JobCode.Trim().ToLower() == item.jobCode.Trim().ToLower())))
                        ).FirstOrDefault();

                    if (existingProjectRequisitionAllocationDetails != null)
                    {
                        existingProjectRequisitionAllocationDetails.RequisitionCount = Convert.ToInt16(item.requisitionCountAdded);
                        existingProjectRequisitionAllocationDetails.AllocationCount = Convert.ToInt16(item.allocationCountAdded);
                        existingProjectRequisitionAllocationDetails.ModifiedAt = DateTime.UtcNow;
                        existingProjectRequisitionAllocationDetails.ModifiedBy = updatedBy;
                        existingProjectRequisitionAllocationDetails.Status = GetProjectRequisitionAllocationStatus(existingProjectRequisitionAllocationDetails.RequisitionCount, existingProjectRequisitionAllocationDetails.AllocationCount);
                        _projectDbContext.ProjectRequisitionAllocation.Update(existingProjectRequisitionAllocationDetails);
                    }
                    else
                    {
                        var projectInfo = _projectDbContext.Projects.Where(m =>
                        !string.IsNullOrEmpty(m.PipelineCode) && m.PipelineCode.Trim().ToLower().Equals(item.pipelineCode.Trim().ToLower())
                         && ((string.IsNullOrEmpty(m.JobCode) && (string.IsNullOrEmpty(item.jobCode) || item.jobCode == "undefined" || item.jobCode == "null")) ||
                        (string.IsNullOrEmpty(item.jobCode) == false && (!string.IsNullOrEmpty(m.JobCode) && m.JobCode.Trim().ToLower() == item.jobCode.Trim().ToLower())))
                        ).FirstOrDefault();
                        if (projectInfo != null)
                        {
                            _projectDbContext.ProjectRequisitionAllocation.Add(new ProjectRequisitionAllocation
                            {
                                ProjectId = (long)projectInfo.Id,
                                PipelineCode = item.pipelineCode,
                                JobCode = item.jobCode,
                                RequisitionCount = Convert.ToInt16(item.requisitionCountAdded),
                                AllocationCount = Convert.ToInt16(item.allocationCountAdded),
                                CreatedAt = DateTime.UtcNow,
                                ModifiedAt = DateTime.UtcNow,
                                CreatedBy = updatedBy,
                                ModifiedBy = updatedBy,
                                Status = GetProjectRequisitionAllocationStatus(Convert.ToInt16(item.requisitionCountAdded), Convert.ToInt16(item.allocationCountAdded))
                            });
                        }
                        else
                        {
                            throw new Exception("Project Not found");
                        }
                    }
                }
                catch (Exception ex1)
                {
                    Console.WriteLine(ex1 + "--" + ex1.StackTrace);
                }
                await _projectDbContext.SaveChangesAsync();
            }
            return true;
        }

        public static string GetProjectRequisitionAllocationStatus(int requisitionCount, int allocationCount)
        {
            if (requisitionCount > 0 && requisitionCount == allocationCount)
            {
                return ProjectRequisitionAllocationStatus.Completed;
            }
            else if (requisitionCount > 0 && requisitionCount > allocationCount)
            {
                return ProjectRequisitionAllocationStatus.PENDING;
            }
            else
            {
                return ProjectRequisitionAllocationStatus.ToBeStarted;
            }
        }

        public async Task<List<string>> GetAllJobCodesForPipelineCode(string pipelineCode, string jobCode, bool? SameTeamAllocation)
        {
            if (SameTeamAllocation == true)
            {
                var currentProject = await _projectDbContext.Projects
                                        .Where(m => !string.IsNullOrEmpty(m.PipelineCode) && m.PipelineCode.Trim().ToLower().Equals(pipelineCode.Trim().ToLower())
                                        && (jobCode == null || (jobCode != null && m.JobCode != null && m.JobCode.Trim().ToLower().Equals(jobCode.Trim().ToLower())))
                                        && m.IsActive == true)
                                        .FirstOrDefaultAsync();

                if (currentProject != null && currentProject.ProjectType != null && currentProject.ProjectType.Trim().ToLower().Equals(Domain.Constant.ProjectType.Recurring.Trim().ToLower()))
                {
                    return await _projectDbContext.Projects
                        .Where(m => !string.IsNullOrEmpty(m.PipelineCode) && m.PipelineCode.Trim().ToLower().Equals(pipelineCode.Trim().ToLower()) && m.JobCode != currentProject.JobCode)
                        .Select(m => m.JobCode)
                        .ToListAsync();
                }
                else
                {
                    return new List<string>();
                }
            }

            else
            {

                return await _projectDbContext.Projects
                    .Where(m => !string.IsNullOrEmpty(m.PipelineCode) && m.PipelineCode.Trim().ToLower().Equals(pipelineCode.Trim().ToLower()))
                    .Select(m => m.JobCode)
                    .ToListAsync();
            }
        }

        public async Task<Boolean> UpdateProjectJobCode(string pipelineCode, string jobCode, string new_jobCode, string new_jobName, string UpdatedBy)
        {
            var obj = _projectDbContext.Projects.Where(a => a.PipelineCode == pipelineCode && a.JobCode == jobCode && a.IsActive == true).FirstOrDefault();
            if (obj != null)
            {
                obj.JobCode = new_jobCode;
                obj.JobName = new_jobName;
                obj.ModifiedAt = new DateTime();
                obj.ModifiedBy = UpdatedBy;

                _projectDbContext.Projects.Update(obj);
            }
            //**************** project Budget*************************
            var obj_project_budget = _projectDbContext.ProjectBudget.Where(a => a.PipelineCode == pipelineCode && a.JobCode == jobCode && a.IsActive == true).FirstOrDefault();
            if (obj_project_budget != null)
            {
                obj_project_budget.JobCode = new_jobCode;
                obj_project_budget.ModifiedAt = new DateTime();
                obj_project_budget.ModifiedBy = UpdatedBy;
                _projectDbContext.ProjectBudget.Update(obj_project_budget);
            }
            //******************* ProjectRequisitionAllocation ******************************/
            var obj_project_req_allocation = _projectDbContext.ProjectRequisitionAllocation.Where(a => a.PipelineCode == pipelineCode && a.JobCode == jobCode).FirstOrDefault();
            if (obj_project_req_allocation != null)
            {
                obj_project_req_allocation.JobCode = new_jobCode;
                obj_project_req_allocation.ModifiedAt = new DateTime();
                obj_project_req_allocation.ModifiedBy = UpdatedBy;
                _projectDbContext.ProjectRequisitionAllocation.Update(obj_project_req_allocation);
            }
            var obj_project_published_fieldfor_marketplaces = _projectDbContext.PublishedFieldForMarketPlaces.Where(a => a.PipelineCode == pipelineCode && a.JobCode == jobCode && a.IsActive == true).FirstOrDefault();
            if (obj_project_published_fieldfor_marketplaces != null)
            {
                obj_project_published_fieldfor_marketplaces.JobCode = new_jobCode;
                obj_project_published_fieldfor_marketplaces.ModifiedAt = new DateTime();
                obj_project_published_fieldfor_marketplaces.ModifiedBy = UpdatedBy;
                _projectDbContext.PublishedFieldForMarketPlaces.Update(obj_project_published_fieldfor_marketplaces);
            }
            await _projectDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<List<GetOfferingSolutionsByJobCodeResponseDTO>> GetOfferingSolutionsByJobCode(List<string> jobCodes)
        {
            List<string> jobCodesLowerCased = jobCodes
                .ConvertAll(m => m.ToLower().Trim())
                .ToList();

            return await _projectDbContext.Projects
                .Select(m => new GetOfferingSolutionsByJobCodeResponseDTO()
                {
                    JobCode = String.IsNullOrEmpty(m.JobCode) ? String.Empty : m.JobCode,
                    Solution = String.IsNullOrEmpty(m.Solutions) ? String.Empty : m.Solutions,
                    SolutionId = String.IsNullOrEmpty(m.SolutionsId) ? String.Empty : m.SolutionsId,
                    Offering = String.IsNullOrEmpty(m.Offerings) ? String.Empty : m.Offerings,
                    OfferingId = String.IsNullOrEmpty(m.OfferingsId) ? String.Empty : m.OfferingsId
                })
                .Where(m => !String.IsNullOrEmpty(m.JobCode) && jobCodesLowerCased.Contains(m.JobCode.ToLower().Trim()))
                .ToListAsync();
        }
    }
}