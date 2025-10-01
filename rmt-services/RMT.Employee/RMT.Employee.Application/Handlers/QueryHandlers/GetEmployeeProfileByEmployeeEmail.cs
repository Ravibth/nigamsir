using MediatR;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using RMT.Employee.Application.DTOs;
using RMT.Employee.Application.Mappers;
using RMT.Employee.Application.Response;
using RMT.Employee.Application.Services;
using RMT.Employee.Domain.DTOs;
using RMT.Employee.Domain.Entities;
using RMT.Employee.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Employee.Application.Handlers.QueryHandlers
{
    public class GetEmployeeProfileByEmployeeEmailQuery : IRequest<EmployeeProfileResponseDTO>
    {
        public string employee_email { get; set; }
        public string email { get; set; }
    }
    public class GetEmployeeProfileByEmployeeEmail : IRequestHandler<GetEmployeeProfileByEmployeeEmailQuery, EmployeeProfileResponseDTO>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ISkillService _skillService;
        private readonly IAllocationService _allocationService;
        private readonly IProjectService _projectService;
        public GetEmployeeProfileByEmployeeEmail(IEmployeeRepository employeeRepository,
                ISkillService skillService,
                IAllocationService allocationService,   
                IProjectService projectService)
        {
            _employeeRepository = employeeRepository;
            _skillService = skillService;
            _allocationService = allocationService;
            _projectService = projectService;
        }

        public async Task<EmployeeProfileResponseDTO> Handle(GetEmployeeProfileByEmployeeEmailQuery request, CancellationToken cancellationToken)
        {

            var employeeProfile = await _employeeRepository.GetEmployeeProfileByEmployeeEmail(request.employee_email);
            var employee_preference_list = await _employeeRepository.GetEmployeePreferencesByEmailAsync(request.employee_email);

            if (employeeProfile == null)
            {
                throw new Exception("Not able to find the User");
            }
            EmployeeProfileResponseDTO response = EmployeeMapper.Mapper.Map<EmployeeProfileResponseDTO>(employeeProfile);
            if (employee_preference_list.Any(x => x.Category.ToUpper() == "INDUSTRY_MAPPING"))
            {
                var employee_industry_preference_list = employee_preference_list.Where(x => x.Category.ToUpper() == "INDUSTRY_MAPPING").ToList();
                List<EmployeeIndustryExperience> employee_idustry_expereence = new();
                foreach (var item in employee_industry_preference_list)
                {
                    EmployeeIndustryExperience empIndusExp = new()
                    {
                        industry_id = item.PreferenceDetails.industry.industry_id,
                        industry_name = item.PreferenceDetails.industry.industry_name,
                        sub_industry_id = item.PreferenceDetails.subIndustry == null ? null : item.PreferenceDetails.subIndustry.sub_industry_id,
                        sub_industry_name = item.PreferenceDetails.subIndustry == null ? null : item.PreferenceDetails.subIndustry.sub_industry_name,
                        year_of_experience = Convert.ToString(item.PreferenceDetails.industry.year_of_experience), 
                        description = item.PreferenceDetails.industry.description,
                    };
                    employee_idustry_expereence.Add(empIndusExp);
                }
                response.employee_industry_expereience = employee_idustry_expereence;
            }

            var userSkillInfo = await _skillService.GetUserApprovedSkillByEmail(new List<string>() { request.employee_email });
            if (userSkillInfo != null && userSkillInfo.Count > 0)
            {
                response.skillInformation = userSkillInfo;
            }
            if(!request.email.Equals(request.employee_email , StringComparison.OrdinalIgnoreCase))
            {
                var published_qualifications = response.employee_qualification.Where(x => x.is_published == true).ToList();
                response.employee_qualification = published_qualifications;
            }

            //Project Experience
            List<AllocationByEmailResponse> userProjectExp = await _allocationService.GetAllocationsByEmail(request.employee_email);

            var groupedData = userProjectExp
                .GroupBy(x => (x.PipelineCode, x.JobCode))
                .SelectMany(g => g.Take(1))
                .ToList();
            
            List<EmployeeProjectExperience> employeeProjectExperiences = new List<EmployeeProjectExperience>();
            foreach (var projectExp in groupedData)
            {
                var projectDetails = await _projectService.GetProjectDetailsEmployee(projectExp.PipelineCode, projectExp.JobCode);
                long totalHrs = projectExp.ResourceAllocationDays.Sum(day => day.Efforts);

                EmployeeProjectExperience employeeProjectExperience = new()
                {
                    job_name = projectExp?.JobName,
                    client_name = projectExp.Requisition.ClientName,
                    client_group = projectDetails?.ClientGroup,
                    business_unit = projectExp.Requisition.BusinessUnit,
                    Offering = projectExp.Requisition.Offerings,
                    Solution = projectExp.Requisition.Solutions,
                    job_start_date = projectExp.StartDate.ToString(),
                    job_end_date = projectExp.EndDate.ToString(),
                    task_description = projectExp.Requisition.Description,
                    actual_hours = totalHrs,//projectExp.Requisition.TotalHours,
                    skills_utilized = string.Join(", ", projectExp.PublishedResAllocSkillEntity
                                        .Where(s => !string.IsNullOrEmpty(s.SkillName))
                                        .Select(s => s.SkillName)),
                    industry = projectDetails?.Industry,
                    sub_industry = projectDetails?.Subindustry,
                    project_type = projectDetails?.ProjectType,
                    project_description = projectDetails?.Description,
                    primary_el = projectDetails?.ProjectRoles.Where(x => x.Role == "EngagementLeader").FirstOrDefault().UserName,
                    csp = projectDetails?.ProjectRoles.Where(x => x.Role == "CSP").FirstOrDefault().UserName,
                };
                employeeProjectExperiences.Add(employeeProjectExperience);
            }
            response.employee_project_experience = employeeProjectExperiences;
            return response;
        }
    }
}
