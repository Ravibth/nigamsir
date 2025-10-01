using MediatR;
using RMT.Projects.Application.HttpServices.DTOs;
using RMT.Projects.Application.IHttpServices;
using RMT.Projects.Domain.Entities;
using RMT.Projects.Domain.Repositories;
using RMT.Projects.Infrastructure;
using RMT.Projects.Infrastructure.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RMT.Projects.Domain.Constant;
using static RMT.Projects.Infrastructure.Constants;

namespace RMT.Projects.Application.Handlers.QueryHandlers
{
    public class GetEmployeesListWithMatchQuery : IRequest<List<EmployeeResponseDTO>>
    {
        public string InputEmail { get; set; }
        public string PipelineCode { get; set; }
        public string? JobCode { get; set; }
        public List<string>? UsersNotToInclude { get; set; }
    }
    public class GetEmployeesListWithMatchQueryHandler : IRequestHandler<GetEmployeesListWithMatchQuery, List<EmployeeResponseDTO>>
    {
        private readonly IIdentityHttpService _identityHttpService;
        private readonly IProjectRepository _projectRepository;
        public GetEmployeesListWithMatchQueryHandler(IIdentityHttpService identityHttpService, IProjectRepository projectRepository)
        {
            _identityHttpService = identityHttpService;
            _projectRepository = projectRepository;
        }
        public async Task<List<EmployeeResponseDTO>> Handle(GetEmployeesListWithMatchQuery request, CancellationToken cancellationToken)
        {

            List<EmployeeResponseDTO> identityEmployees = await _identityHttpService.GetEmployeesListByInputEmail(request.InputEmail);
            Dictionary<string, List<ProjectRolesView>> roleResponse = await _projectRepository.GetProjectRolesByPipelineCodeAndRoles(
                new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>(request.PipelineCode, request.JobCode) }, null);
            //List<ProjectRoles> projectUserRoles = new List<ProjectRoles>();
            List<string> emailsToRemove = new List<string>();
            List<EmployeeResponseDTO> response = new List<EmployeeResponseDTO>();

            foreach (var kv in roleResponse.AsEnumerable())
            {
                if (kv.Key == UserRoles.AdditionalEl)
                {
                    continue;
                }
                else if (kv.Key == UserRoles.Delegate)
                {
                    continue;
                }
                else if (kv.Key == UserRoles.AdditionalDelegate)
                {
                    continue;
                }
                emailsToRemove = emailsToRemove.Concat(kv.Value.Select(d => d.User)).ToList();
            }
            //Remove Employees From "emailsToRemove"
            var onlyEmployees = await _projectRepository.GetOnlyEmployeesOfProject(request.PipelineCode , request.JobCode);
            foreach (var emp in onlyEmployees)
            {
                if (!string.IsNullOrEmpty(emp.User))
                {
                    emailsToRemove.Remove(emp.User);
                }
            }
            foreach (var user in request.UsersNotToInclude)
            {
                if (!string.IsNullOrEmpty(user))
                {
                    emailsToRemove.Add(user);
                }
            }
            string emailIdNotToRemove = emailsToRemove.Where(er => er.Equals(request.InputEmail, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if (!string.IsNullOrEmpty(emailIdNotToRemove))
            {
                emailsToRemove.Remove(emailIdNotToRemove);
            }

            //var roleResponse = roleResponse.Values;
            //foreach (var employeeRoles in roleResponse.Values)
            //{
            //    foreach (var employee in employeeRoles)
            //    {
            //        projectUserRoles.Add(employee);
            //    }
            //}
            //foreach (var identityEmployee in identityEmployees)
            //{

            //    var projectUserRole = projectUserRoles.Find(d => d.User.ToLower().Trim() == identityEmployee.emailId.ToLower().Trim() && d.Role != UserRoles.Delegate);
            //    if (projectUserRole == null)
            //    {
            //        response.Add(identityEmployee);
            //    }
            //}
            foreach (var identityEmployee in identityEmployees)
            {
                var employeeInfo = emailsToRemove
                                        .Where((cr) => identityEmployee.emailId.Equals(cr, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                if (employeeInfo == null)
                {
                    response.Add(identityEmployee);
                }
            }

            // response.Concat(roleResponse["Delegate"]);
            return response;
        }
    }
}
