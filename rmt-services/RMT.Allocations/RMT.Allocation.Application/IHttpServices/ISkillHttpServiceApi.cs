using RMT.Allocation.Application.HttpServices.DTOs;
using RMT.Allocation.Domain.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.IHttpServices
{
    public interface ISkillHttpServiceApi
    {
        Task<List<SkillCodeNameDTO>> GetSkillCodeName();
        Task<List<SkillCodeNameDTO>> GetMandatorySkill(string? competency, string? competencyId, string designation);
        Task<List<UserSkillDto>> GetApprovedSkill(List<string> email);
    }
}
