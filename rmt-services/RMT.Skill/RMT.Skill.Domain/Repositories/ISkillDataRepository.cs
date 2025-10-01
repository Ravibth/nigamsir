using RMT.Skill.Domain.DTOs;
using RMT.Skill.Domain.DTOs.Request;
using RMT.Skill.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Skill.Domain.Repositories
{
    public interface ISkillDataRepository
    {
        Task<Boolean> AddOrUpdateSkill(Skills input);

        //Task<Skills> GetSkillAsync(string SkillName);

        Task<Skills> GetSkillByCode(string SkillName);

        Task<Skills> GetSkillByName(string SkillCode);

        Task<List<SkillMapping>> GetSkillMappingBySkillName(string SkillName);

        Task<SkillMapping> GetSpecificMappingById(Int64 mappingId);

        Task<List<SkillCategoryMaster>> GetAllSkillsCategory();
        Task<List<Skills>> GetAllSkillsByNameOrCode(List<string> skillNameOrCode);

        Task<List<Skills>> GetAllSkillAsync();

        Task<Skills> UpdateSkill(Skills input);

        Task<List<UserSkills>> SearchSkillByNameOrCode(List<string> skillsNameOrCode);

        Task<List<SkillMapping>> GetManatorySkillAsync(string? competency, string? competencyId, string designation);
    }
}
