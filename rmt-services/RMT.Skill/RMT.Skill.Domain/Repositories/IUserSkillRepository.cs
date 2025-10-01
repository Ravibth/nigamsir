using RMT.Skill.Domain.DTOs;
using RMT.Skill.Domain.DTOs.Responses;
using RMT.Skill.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Skill.Domain.Repositories
{
    public interface IUserSkillRepository
    {
        Task<List<AddUpdateNewUserSkill>> AddUserSkills(List<UserSkills> newUserSkills);
        Task<List<GetUserSkillsByEmailResponse>> GetUserSkillsByEmail(string email);
        Task<List<UserSkillsResponseWithSkillDTO>> GetUserApprovedSkillsByEmail(List<string> emails);
        Task<List<UserSkills>> UpdateUserSkillStatusAfterWorkflow(List<UpdateUserSkillStatusAfterWorkflowRequestDTO> requests);
        Task<List<UserSkills>> DeactiveUserSkill(string skillCode,Boolean status);
        Task<List<GetUserSkillsByEmailResponse>> GetUserSkillsById(string id);
    }
}
