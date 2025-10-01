using MediatR;
using RMT.Skill.API.Constant;
using RMT.Skill.Application.DTOs;
using RMT.Skill.Application.HttpServices;
using RMT.Skill.Application.IHttpServices;
using RMT.Skill.Domain.DTOs.Responses;
using RMT.Skill.Domain.Entities;
using RMT.Skill.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Skill.Application.Handlers.QueryHandler
{
    public class GetUserSkillsByEmailQuery : IRequest<List<GetUserSkillsByEmailResponse>>
    {
        public string email { get; set; }
        public bool approvals { get; set; }
    }
    public class GetUserSkillsByEmailQueryHandler : IRequestHandler<GetUserSkillsByEmailQuery, List<GetUserSkillsByEmailResponse>>
    {
        private readonly IUserSkillRepository _userSkillRepository;
        private readonly IWorkflowHttpService _workflowHttpService;
        private readonly IIdentityUserDetailsHttpApi _identityServiceHttpApi;
        public GetUserSkillsByEmailQueryHandler(IUserSkillRepository userSkillRepository, IWorkflowHttpService workflowHttpService, IIdentityUserDetailsHttpApi identityServiceHttpApi)
        {
            _workflowHttpService = workflowHttpService;
            _userSkillRepository = userSkillRepository;
            _identityServiceHttpApi = identityServiceHttpApi;
        }
        public async Task<List<GetUserSkillsByEmailResponse>> Handle(GetUserSkillsByEmailQuery query, CancellationToken cancellationToken)
        {
            var userSkills = await _userSkillRepository.GetUserSkillsByEmail(query.email);
            if (query.approvals)
            {
                List<Guid> item_ids = userSkills.Select(x => x.Id).ToList();
                if (item_ids.Count > 0)
                {
                    List<WorkflowDTO> workflows = await _workflowHttpService.GetWorkflowDetailsByItemId(item_ids);

                    foreach (var item in userSkills)
                    {
                        WorkflowDTO workflowItem = workflows.Where(m => m.item_id.ToLower() == item.Id.ToString().ToLower()).FirstOrDefault();
                        if (workflowItem != null)
                        {
                            var workflowTaskItem = workflowItem?.task_list?.FirstOrDefault();
                            if (workflowTaskItem != null)
                            {
                                List<string> emails = workflowTaskItem.assigned_to.Split(",").ToList();
                                var identityUserResponse = await _identityServiceHttpApi.GetEmployeesDataHttpApiQuery(emails);
                                // item.Approver = workflowTaskItem.assigned_to;
                                var Approver = identityUserResponse;
                                if (Approver != null)
                                {
                                    item.Approver = string.Join(",", Approver.Select(e => e.name) );
                                }
                                if (workflowTaskItem?.status.ToLower() == Constants.WorkflowTaskStatus.APPROVED.ToLower() || workflowTaskItem.status.ToLower() == Constants.WorkflowTaskStatus.REJECTED.ToLower())
                                {
                                    item.ApprovedOn = workflowTaskItem.updated_at;
                                }
                            }
                        }
                    }
                }
            }
            return userSkills;
        }
    }
}
