using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RMT.Skill.Domain.DTOs;
using RMT.Skill.Domain.DTOs.Responses;
using RMT.Skill.Domain.Entities;
using RMT.Skill.Domain.Repositories;
using RMT.Skill.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RMT.Skill.Infrastructure.InfrastructureConstants;

namespace RMT.Skill.Infrastructure.Repositories
{
    public class UserSkillRepository : IUserSkillRepository
    {
        private readonly SkillDbContext _dbContext;
        public UserSkillRepository(SkillDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<AddUpdateNewUserSkill>> AddUserSkills(List<UserSkills> newUserSkills)
        {
            try
            {
                List<AddUpdateNewUserSkill> response = new();
                if (newUserSkills.Count > 0)
                {

                    await _dbContext.UserSkills.AddRangeAsync(newUserSkills);
                    await _dbContext.SaveChangesAsync();
                    var userDetails = newUserSkills.FirstOrDefault();
                    if (userDetails != null)
                    {
                        var userSkillsAdded = await _dbContext.UserSkills
                            .Where(m => m.Email.Equals(userDetails.Email) && m.ModifiedAt == userDetails.ModifiedAt)
                            .ToListAsync();
                        foreach (var skillAdded in userSkillsAdded)
                        {
                            var previousSkillLevel = _dbContext.UserSkills
                                .Where(m => m.SkillName.Equals(skillAdded.SkillName)
                                        && m.Email.Equals(skillAdded.Email)
                                        && m.Status.Equals(UserSkillsStatus.APPROVED)
                                    )
                                .OrderByDescending(m => m.ModifiedAt)
                                .FirstOrDefault();
                            response.Add(new AddUpdateNewUserSkill
                            {
                                SkillName = skillAdded.SkillName.Trim(),
                                CreatedAt = userDetails.CreatedAt,
                                CreatedBy = userDetails.CreatedBy,
                                Email = userDetails.Email,
                                SkillCode = skillAdded.SkillCode,
                                Status = userDetails.Status,
                                EmpId = userDetails.EmpId,
                                Id = userDetails.Id,
                                IsActive = userDetails.IsActive,
                                ModifiedAt = userDetails.ModifiedAt,
                                ModifiedBy = userDetails.ModifiedBy,
                                Name = skillAdded.Name,
                                Proficiency = skillAdded.Proficiency,
                                previousUpdatedLevel = previousSkillLevel != null ? previousSkillLevel.Proficiency : ""
                            });
                        }
                    }
                }
                return response;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<GetUserSkillsByEmailResponse>> GetUserSkillsByEmail(string email)
        {
            var response = new List<GetUserSkillsByEmailResponse>();
            var userSkillsFound = _dbContext.UserSkills
                            .Where(m => m.IsActive && m.Email.Equals(email))
                            .GroupBy(m => m.SkillName)
                            .ToList() // Fetch all items from the database
                            .SelectMany(group => group.Where(item => item.Status == InfrastructureConstants.UserSkillsStatus.REJECTED) //Get all the rejected entries
                                .Concat(group.Where(item => item.Status != InfrastructureConstants.UserSkillsStatus.REJECTED))
                            .GroupBy(m => m.SkillName) // Regroup in memory
                            .SelectMany(group => group.Where(item => item.Status == InfrastructureConstants.UserSkillsStatus.REJECTED)
                                .Concat(group.Where(item => item.Status != InfrastructureConstants.UserSkillsStatus.REJECTED)
                                .OrderByDescending(item => item.ModifiedAt).Take(1)))) //Get the last updated entry of a skill which can be in any state other than rejected
                            .ToList();
            foreach (var userSkill in userSkillsFound)
            {
                var responseItem = new GetUserSkillsByEmailResponse()
                {
                    SkillCode = userSkill.SkillCode,
                    SkillName = userSkill.SkillName,
                    Status = userSkill.Status,
                    CreatedAt = userSkill.CreatedAt,
                    CreatedBy = userSkill.CreatedBy,
                    Email = userSkill.Email,
                    EmpId = userSkill.EmpId,
                    Id = userSkill.Id,
                    IsActive = userSkill.IsActive,
                    ModifiedAt = userSkill.ModifiedAt,
                    ModifiedBy = userSkill.ModifiedBy,
                    Name = userSkill.Name,
                    Proficiency = userSkill.Proficiency,
                };

                var skillMasterItem = _dbContext.Skills
                    .Where(m => m.SkillCode.ToLower().Trim().Equals(userSkill.SkillCode.ToLower().Trim())
                                && m.IsActive == true
                          )
                    .FirstOrDefault();
                if (skillMasterItem != null)
                {
                    responseItem.IsEnabled = skillMasterItem.IsActive == true && skillMasterItem.IsEnable == true ? true : false;
                    responseItem.Description = skillMasterItem.Description;
                    responseItem.Basic = skillMasterItem.Basic;
                    responseItem.Intermediate = skillMasterItem.Intermediate;
                    responseItem.Advanced = skillMasterItem.Advanced;
                    responseItem.Expert = skillMasterItem.Expert;

                }
                else
                {
                    responseItem.IsEnabled = false;
                    responseItem.Description = "";
                    responseItem.Basic = "";
                    responseItem.Intermediate = "";
                    responseItem.Advanced = "";
                    responseItem.Expert = "";
                }
                response.Add(responseItem);
            }
            response = response.OrderByDescending(i => i.CreatedAt).ToList();
            return response;
        }

        public async Task<List<GetUserSkillsByEmailResponse>> GetUserSkillsById(string id)
        {
            var response = new List<GetUserSkillsByEmailResponse>();
            var userSkillsFound = _dbContext.UserSkills
                            .Where(m => m.IsActive && m.Id.ToString().Trim().Equals(id.ToString().Trim()))
                            .GroupBy(m => m.SkillName)
                            .ToList() // Fetch all items from the database
                            .SelectMany(group => group.Where(item => item.Status == InfrastructureConstants.UserSkillsStatus.REJECTED) //Get all the rejected entries
                                .Concat(group.Where(item => item.Status != InfrastructureConstants.UserSkillsStatus.REJECTED))
                            .GroupBy(m => m.SkillName) // Regroup in memory
                            .SelectMany(group => group.Where(item => item.Status == InfrastructureConstants.UserSkillsStatus.REJECTED)
                                .Concat(group.Where(item => item.Status != InfrastructureConstants.UserSkillsStatus.REJECTED)
                                .OrderByDescending(item => item.ModifiedAt).Take(1)))) //Get the last updated entry of a skill which can be in any state other than rejected
                            .ToList();
            foreach (var userSkill in userSkillsFound)
            {
                var responseItem = new GetUserSkillsByEmailResponse()
                {
                    SkillCode = userSkill.SkillCode,
                    SkillName = userSkill.SkillName,
                    Status = userSkill.Status,
                    CreatedAt = userSkill.CreatedAt,
                    CreatedBy = userSkill.CreatedBy,
                    Email = userSkill.Email,
                    EmpId = userSkill.EmpId,
                    Id = userSkill.Id,
                    IsActive = userSkill.IsActive,
                    ModifiedAt = userSkill.ModifiedAt,
                    ModifiedBy = userSkill.ModifiedBy,
                    Name = userSkill.Name,
                    Proficiency = userSkill.Proficiency,
                };

                var skillMasterItem = _dbContext.Skills
                    .Where(m => m.SkillCode.ToLower().Trim().Equals(userSkill.SkillCode.ToLower().Trim())
                                && m.IsActive == true
                          )
                    .FirstOrDefault();
                if (skillMasterItem != null)
                {
                    responseItem.IsEnabled = skillMasterItem.IsActive == true && skillMasterItem.IsEnable == true ? true : false;
                    responseItem.Description = skillMasterItem.Description;
                    responseItem.Basic = skillMasterItem.Basic;
                    responseItem.Intermediate = skillMasterItem.Intermediate;
                    responseItem.Advanced = skillMasterItem.Advanced;
                    responseItem.Expert = skillMasterItem.Expert;

                }
                else
                {
                    responseItem.IsEnabled = false;
                    responseItem.Description = "";
                    responseItem.Basic = "";
                    responseItem.Intermediate = "";
                    responseItem.Advanced = "";
                    responseItem.Expert = "";
                }
                response.Add(responseItem);
            }
            response = response.OrderByDescending(i => i.CreatedAt).ToList();
            return response;
        }

        public async Task<List<UserSkillsResponseWithSkillDTO>> GetUserApprovedSkillsByEmail(List<string> emails)
        {
            List<string> emailIds = emails.Select(e => e.ToLower().Trim()).ToList();
            var result = await _dbContext.UserSkills
                .Where(userSkill => emailIds.Contains(userSkill.Email.Trim().ToLower()) && userSkill.IsActive == true && userSkill.Status == InfrastructureConstants.UserSkillsStatus.APPROVED)
                .GroupBy(s => new { s.SkillName, s.Email })
                .Select(m => m.OrderByDescending(s => s.ModifiedAt).First())
                .ToListAsync();

            var response = new List<UserSkillsResponseWithSkillDTO>();
            foreach (var item in result)
            {
                var skillItem = _dbContext.Skills
                    .Include(m => m.Skill_Mapping)
                    .Where(m =>
                        m.SkillName.ToLower().Trim() == item.SkillName.ToLower().Trim()
                        && m.SkillCode.ToLower().Trim() == item.SkillCode.ToLower().Trim()
                    )
                    .FirstOrDefault();
                response.Add(new UserSkillsResponseWithSkillDTO
                {
                    skill = skillItem,
                    CreatedAt = item.CreatedAt,
                    SkillCode = item.SkillCode,
                    SkillName = item.SkillName,
                    ModifiedAt = item.ModifiedAt,
                    Status = item.Status,
                    CreatedBy = item.CreatedBy,
                    Email = item.Email,
                    EmpId = item.EmpId,
                    Id = item.Id,
                    IsActive = item.IsActive,
                    ModifiedBy = item.ModifiedBy,
                    Name = item.Name,
                    Proficiency = item.Proficiency,
                });
            }

            return response;
        }
        public async Task<List<UserSkills>> DeactiveUserSkill(string skillCode, Boolean status)
        {
            var allSkill = await _dbContext.UserSkills.Where(user => skillCode.Trim().ToLower() == user.SkillCode.Trim().ToLower() && user.Status == "Approved").ToListAsync();
            foreach (var item in allSkill)
            {
                item.IsActive = status;
            }
            _dbContext.UserSkills.UpdateRange(allSkill);
            await _dbContext.SaveChangesAsync();
            return allSkill;
        }
        public async Task<List<UserSkills>> UpdateUserSkillStatusAfterWorkflow(List<UpdateUserSkillStatusAfterWorkflowRequestDTO> requests)
        {
            List<UserSkills> skillsUpdatedResponse = new List<UserSkills>();
            foreach (var item in requests)
            {
                var skillToUpdate = _dbContext.UserSkills.Where(m => m.Id.Equals(item.Id)).FirstOrDefault();
                skillToUpdate.Status = item.ActionPerformed.ToLower().Trim() == UpdateUserSkillWorkflowActions.APPROVED.ToLower().Trim()
                    ? UserSkillsStatus.APPROVED
                    : UserSkillsStatus.REJECTED;
                skillToUpdate.ModifiedBy = item.ModifiedBy;
                skillToUpdate.ModifiedAt = item.ModifiedAt;
                _dbContext.UserSkills.Update(skillToUpdate);
                skillsUpdatedResponse.Add(skillToUpdate);
            }
            await _dbContext.SaveChangesAsync();
            return skillsUpdatedResponse;
        }
    }
}
