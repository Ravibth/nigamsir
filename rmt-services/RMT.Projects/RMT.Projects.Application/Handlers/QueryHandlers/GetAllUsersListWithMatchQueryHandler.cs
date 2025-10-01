using MediatR;
using RMT.Projects.Application.HttpServices.DTOs;
using RMT.Projects.Application.IHttpServices;
using RMT.Projects.Domain.Entities;
using RMT.Projects.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RMT.Projects.Domain.Constant;
using static RMT.Projects.Infrastructure.Constants;

namespace RMT.Projects.Application.Handlers.QueryHandlers
{
    public class CurrentUserRoles
    {
        public int? ProjectRoleId { get; set; }
        public string? AdditionalElName { get; set; }
        public string? AdditionalDelegateName { get; set; }
        public string? AdditionalElEmail { get; set; }
        public string? AdditionalDelegateEmail { get; set; }
    }
    public class GetAllUsersListWithMatchQuery : IRequest<List<EmployeeResponseDTO>>
    {
        public string InputEmail { get; set; }
        public string PipelineCode { get; set; }
        public string? JobCode { get; set; }
        public List<CurrentUserRoles>? CurrentUserRoles { get; set; }
        public List<string>? usersNotToInclude { get; set; }
        public string EmailId { get; set; }
    }

    public class GetAllUsersListWithMatchQueryHandler : IRequestHandler<GetAllUsersListWithMatchQuery, List<EmployeeResponseDTO>>
    {
        private readonly IIdentityHttpService _identityHttpService;
        private readonly IProjectRepository _projectRepository;
        public GetAllUsersListWithMatchQueryHandler(IIdentityHttpService identityHttpService, IProjectRepository projectRepository)
        {
            _identityHttpService = identityHttpService;
            _projectRepository = projectRepository;
        }

        public async Task<List<EmployeeResponseDTO>> Handle(GetAllUsersListWithMatchQuery request, CancellationToken cancellationToken)
        {

            List<KeyValuePair<string, string>> keyValuePairs = new List<KeyValuePair<string, string>>();
            keyValuePairs.Add(new KeyValuePair<string, string>(request.PipelineCode, request.JobCode));
            //1. IDENTITY
            List<EmployeeResponseDTO> identityEmployees = await _identityHttpService.GetEmployeesListByInputEmail(request.InputEmail);
            //2. GET PROJECT ROLE
            //Project project = await _projectRepository.GetProjectByPipelineCode(request.PipelineCode, request.JobCode);
            //List<ProjectRoles> userRoles = project.ProjectRoles
            //                                .Where(r => (r.User.Equals(request.EmailId , StringComparison.OrdinalIgnoreCase)) &&
            //                                            r.IsActive == true).ToList();
            //List<ProjectRoles> additionalDelegateRole = project.ProjectRoles
            //                                .Where(r => (r.DelegateEmail.Equals(request.EmailId, StringComparison.OrdinalIgnoreCase)) &&
            //                                            r.IsActive == true).ToList();
            //bool isAdditionalDelegate = additionalDelegateRole.Any();

            //bool isRR = userRoles.Where(ur => Constants.Constants.ResourceRequestors.Any(ur.Role))
            Dictionary<string, List<ProjectRolesView>> roleResponse = await _projectRepository.GetProjectRolesByPipelineCodeAndRoles(keyValuePairs, null);
            //List<ProjectRoles> projectUserRoles = new List<ProjectRoles>();
            List<EmployeeResponseDTO> response = new List<EmployeeResponseDTO>();
            List<string> emailsToRemove = new List<string>();
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

            var onlyEmployees = await _projectRepository.GetOnlyEmployeesOfProject(request.PipelineCode, request.JobCode);
            foreach (var emp in onlyEmployees)
            {
                if (!string.IsNullOrEmpty(emp.User))
                {
                    emailsToRemove.Remove(emp.User);
                }
            }
            //foreach (var currentUser in request.CurrentUserRoles)
            //{
            //    if (!string.IsNullOrEmpty(currentUser.AdditionalDelegateEmail))
            //    {
            //        emailsToRemove.Add(currentUser.AdditionalDelegateEmail);
            //    }
            //    if (!string.IsNullOrEmpty(currentUser.AdditionalElEmail))
            //    {
            //        emailsToRemove.Add(currentUser.AdditionalElEmail);
            //    }
            //}
            foreach (var users in request.usersNotToInclude)
            {
                if (!string.IsNullOrEmpty(users))
                {
                    emailsToRemove.Add(users);
                }
                //if (!string.IsNullOrEmpty(users))
                //{
                //    emailsToRemove.Add(users);
                //}
            }
            //foreach (var identityEmployee in identityEmployees)
            //{

            //    var projectUserRole = projectUserRoles.Find(d => d.User.ToLower().Trim() == identityEmployee.emailId.ToLower().Trim() && d.Role != UserRoles.AdditionalEl);
            //    if (projectUserRole == null)
            //    {
            //        response.Add(identityEmployee);
            //    }
            //}
            string emailIdNotToRemove = emailsToRemove.Where(er => er.Equals(request.InputEmail, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            if (!string.IsNullOrEmpty(emailIdNotToRemove))
            {
                emailsToRemove.Remove(emailIdNotToRemove);
            }
            foreach (var identityEmployee in identityEmployees)
            {
                var employeeInfo = emailsToRemove
                                    .Where((cr) => identityEmployee.emailId.Equals(cr, StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
                                                   //|| identityEmployee.emailId.Equals(cr, StringComparison.OrdinalIgnoreCase);
                if (employeeInfo == null)
                {
                    response.Add(identityEmployee);
                }
            }

            return response;
        }
    }
}
