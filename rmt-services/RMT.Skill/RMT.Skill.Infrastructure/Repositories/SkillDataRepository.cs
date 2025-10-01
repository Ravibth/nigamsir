using Azure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RMT.Skill.API.Constant;
using RMT.Skill.Domain.DTOs;
using RMT.Skill.Domain.DTOs.Request;
using RMT.Skill.Domain.Entities;
using RMT.Skill.Domain.Repositories;
using RMT.Skill.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static RMT.Skill.Infrastructure.InfrastructureConstants;

namespace RMT.Skill.Infrastructure.Repositories
{
    public class SkillDataRepository : ISkillDataRepository
    {
        private readonly SkillDbContext _dbContext;
        private readonly IConfiguration _configuration;

        public SkillDataRepository(SkillDbContext dbContext, IConfiguration configuration)
        {
            _dbContext = dbContext;
            _configuration = configuration;
        }

        public async Task<List<UserSkills>> SearchSkillByNameOrCode(List<string> skillNameOrCode)
        {
            if (skillNameOrCode.Count > 0)
            {
                var _skillResult = await _dbContext.Skills.Include(x => x.Skill_Mapping)
                    .Where(x => (skillNameOrCode.Contains(x.SkillName) || skillNameOrCode.Contains(x.SkillCode))).ToListAsync();
                //.Include(d => d.Skill_Mapping).ToListAsync();
                List<UserSkills> userSkills = await _dbContext.UserSkills
                    .Where(x => x.IsActive == true && x.Status == UserSkillsStatus.APPROVED
                    && (skillNameOrCode.Contains(x.SkillName) || skillNameOrCode.Contains(x.SkillCode)))
                    .GroupBy(s => new { s.SkillName, s.Email })
                    .Select(m => m.OrderByDescending(s => s.ModifiedAt).First())
                   .ToListAsync();
                return userSkills;
            }
            return null;
        }
        public async Task<List<Skills>> GetAllSkillsByNameOrCode(List<string> skillNameOrCode)
        {
            if (skillNameOrCode.Count > 0)
            {
                var _skillResult = await _dbContext.Skills.Include(x => x.Skill_Mapping)
                    .Where(x => (skillNameOrCode.Contains(x.SkillName) || skillNameOrCode.Contains(x.SkillCode))).ToListAsync();
                return _skillResult;
            }
            return new List<Skills>();
        }
        public async Task<Boolean> AddOrUpdateSkill(Skills input)
        {
            var skill = await _dbContext.Skills.ToListAsync();
            var skillId = skill.Max(a => a.Skill_Id);
            string skillCode = string.Concat("SK", String.Format("{0:000000}", skillId.HasValue ? skillId : 0 + 1));
            input.SkillCode = skillCode;
            await _dbContext.Set<Skills>().AddAsync(input);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0 ? true : false;
        }

        public async Task<Skills> UpdateSkill(Skills input)
        {
            _dbContext.Skills.Update(input);
            var result = await _dbContext.SaveChangesAsync();
            Skills response = new Skills();
            if (result > 0)
            {
                response = await GetSkillByCode(input.SkillCode);
            }
            return response;
        }

        public async Task<List<Skills>> GetAllSkillAsync()
        {
            return await _dbContext.Skills
              .Include(x => x.Skill_Mapping)
              .OrderByDescending(x => x.ModifieDate == null ? x.CreateDate : x.ModifieDate)
              .ToListAsync();
        }

        public async Task<Skills> GetSkillByName(string SkillName)
        {
            return await _dbContext.Skills.Where(d => !string.IsNullOrEmpty(d.SkillName) && !string.IsNullOrEmpty(SkillName) &&
                        d.SkillName.ToLower().Trim() == SkillName.ToLower().Trim()
                        ).Include(d => d.Skill_Mapping)
                        .FirstOrDefaultAsync();
        }

        public async Task<Skills> GetSkillByCode(string SkillCode)
        {
            return await _dbContext.Skills.Where(d => !string.IsNullOrEmpty(d.SkillCode) && !string.IsNullOrEmpty(SkillCode) &&
                        d.SkillCode.ToLower().Trim() == SkillCode.ToLower().Trim()
                        ).Include(d => d.Skill_Mapping)
                        .FirstOrDefaultAsync();
        }

        //TODO:COMP_CHANGE
        public async Task<List<SkillMapping>> GetManatorySkillAsync(string? competency, string? competencyId, string designation)
        {
            return await _dbContext.SkillMapping.Include(x => x.Skill)
                .Where(map =>
                    (
                        (!string.IsNullOrEmpty(competencyId) && map.CompetencyId.Trim().ToLower() == competencyId.Trim().ToLower())
                        || (!string.IsNullOrEmpty(competency) && map.Competency.Trim().ToLower() == competency.Trim().ToLower())
                    )
                    && map.Designation.Contains(designation))
                .ToListAsync();
            // return await _dbContext.Skills.Where(skill => skill.SkillName.ToLower() == SkillName.ToLower()).Include(skill => skill.Skill_Mapping.Where(id => id.Skill_Id == skill.Skill_Id)).FirstOrDefaultAsync();

        }
        public async Task<List<SkillMapping>> GetSkillMappingBySkillName(string SkillName)
        {
            var skill = await _dbContext.Skills.Where(skill => skill.SkillName.ToLower() == SkillName.ToLower()).FirstOrDefaultAsync();
            List<SkillMapping> selectedSkillMapping = new List<SkillMapping>();
            if (skill != null)
            {
                selectedSkillMapping = await _dbContext.SkillMapping.Where(map => map.Skill_Id == skill.Skill_Id).ToListAsync();
            }
            return selectedSkillMapping;
        }

        public async Task<SkillMapping> GetSpecificMappingById(Int64 mappingId)
        {
            var mapping = await _dbContext.SkillMapping.Where(map => map.id == mappingId).FirstOrDefaultAsync();
            return mapping;
        }

        public async Task<List<SkillCategoryMaster>> GetAllSkillsCategory()
        {
            return await _dbContext.SkillCategoryMaster.ToListAsync();
        }


    }
}
