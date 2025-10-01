using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RMT.Employee.Application.DTOs;
using RMT.Employee.Domain.DTOs;
using RMT.Employee.Domain.Entities;
using RMT.Employee.Domain.Repositories;
using RMT.Employee.Infrastructure.Data;

namespace RMT.Employee.Infrastructure.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        protected readonly EmployeeDbContext _employeeDbContext;
        public EmployeeRepository(EmployeeDbContext employeeDbContext)
        {
            _employeeDbContext = employeeDbContext;
        }
        public async Task<PreferenceMaster> AddPreferenceMasterAsync(PreferenceMaster preferenceMaster)
        {
            var response = (await _employeeDbContext.Set<PreferenceMaster>().AddAsync(preferenceMaster)).Entity;
            await _employeeDbContext.SaveChangesAsync();
            return response;
        }
        public async Task<PreferenceMaster> UpdatePreferenceMaster(PreferenceMaster preferenceMaster)
        {
            var masterPreferenceData = await _employeeDbContext.PreferenceMasters.Where(a => a.Name.ToUpper().Trim() == preferenceMaster.Name.ToUpper().Trim() && a.Category.ToUpper().Trim() == preferenceMaster.Category.Trim().ToUpper()).FirstOrDefaultAsync();
            var response = new PreferenceMaster();
            if (masterPreferenceData != null)
            {
                masterPreferenceData.Name = preferenceMaster.Name;
                masterPreferenceData.Category = preferenceMaster.Category;
                masterPreferenceData.SortOrder = preferenceMaster.SortOrder;
                masterPreferenceData.Description = preferenceMaster.Description;
                masterPreferenceData.CreatedBy = preferenceMaster.CreatedBy;
                masterPreferenceData.CreatedAt = preferenceMaster.CreatedAt;
                masterPreferenceData.ModifiedBy = preferenceMaster.ModifiedBy;
                masterPreferenceData.ModifiedAt = preferenceMaster.ModifiedAt;
                masterPreferenceData.IsActive = preferenceMaster.IsActive;
                var result = _employeeDbContext.Update(masterPreferenceData);
                response = result.Entity;
            }
            else
            {
                var masterPreferenceEntity = new PreferenceMaster()
                {
                    Name = preferenceMaster.Name,
                    Category = preferenceMaster.Category,
                    SortOrder = preferenceMaster.SortOrder,
                    Description = preferenceMaster.Description,
                    CreatedBy = preferenceMaster.CreatedBy,
                    CreatedAt = preferenceMaster.CreatedAt,
                    ModifiedBy = preferenceMaster.ModifiedBy,
                    ModifiedAt = preferenceMaster.ModifiedAt,
                    IsActive = preferenceMaster.IsActive,
                };
                var result = await _employeeDbContext.PreferenceMasters.AddAsync(masterPreferenceEntity);
                response = result.Entity;
            }
            _employeeDbContext.SaveChangesAsync();
            return response;
        }
        public async Task<EmployeePreference> AddEmployeePreferenceAsync(EmployeePreference employeePreference)
        {
            var response = (await _employeeDbContext.Set<EmployeePreference>().AddAsync(employeePreference)).Entity;
            await _employeeDbContext.SaveChangesAsync();
            return response;
        }
        public async Task<List<EmployeePreference>> GetEmployeePreferencesByEmailAsync(string employeeEmail)
        {
            return await _employeeDbContext.EmployeePreferences
            //    .Select(a => new EmployeePreference
            //{
            //    Category = a.Category,
            //    EmployeeEmail = a.EmployeeEmail,
            //    IsActive = a.IsActive,
            //    Id = a.Id,
            //    PreferenceName = a.PreferenceName,
            //    PreferenceOrder = a.PreferenceOrder,
            //    PreferenceId = a.PreferenceId,
            //    CreatedAt = a.CreatedAt,
            //    ModifiedAt = a.ModifiedAt,
            //    CreatedBy = a.CreatedBy,
            //    ModifiedBy = a.ModifiedBy
            //})
                .Where(m =>
              m.IsActive == true &&
             Convert.ToString(m.EmployeeEmail).Trim().ToLower() == Convert.ToString(employeeEmail).Trim().ToLower())
                .OrderBy(e => e.PreferenceOrder)
                .ToListAsync<EmployeePreference>();
        }
        public async Task<List<PreferenceMaster>> GetPreferenceMastersAsync()
        {
            return await _employeeDbContext.PreferenceMasters.Where(a => a.IsActive).ToListAsync<PreferenceMaster>();
        }
        public async Task<List<EmployeePreference>> UpdateAsync(List<EmployeePreference> employeePreferences, string userEmail)
        {
            var employeeEmail = userEmail;
            if (employeePreferences != null && employeePreferences.Count > 0)
            {
                employeeEmail = employeePreferences[0].EmployeeEmail;
                var currentActivePrefetence = await _employeeDbContext
                                .EmployeePreferences.Where(a => a.EmployeeEmail.ToLower().Trim() == employeeEmail.ToLower().Trim() && a.IsActive == true)
                                .ToListAsync();
                var currentPreferenceToDeactivate = currentActivePrefetence.Where(e => !employeePreferences.Any(x => x.Id == e.Id && x.IsActive == true));
                foreach (var item in currentPreferenceToDeactivate)
                {
                    item.IsActive = false;
                    item.ModifiedBy = employeeEmail;
                    _employeeDbContext.EmployeePreferences.Update(item);
                }
                foreach (var preference in employeePreferences)
                {
                    employeeEmail = preference.EmployeeEmail;
                    if (preference.Id == null || preference.Id == -1)
                    {
                        var preferenceEntity = new EmployeePreference()
                        {
                            IsActive = preference.IsActive,
                            Category = preference.Category,
                            PreferenceOrder = preference.PreferenceOrder,
                            EmployeeEmail = preference.EmployeeEmail,
                            CreatedAt = preference.CreatedAt,
                            CreatedBy = preference.CreatedBy,
                            ModifiedAt = preference.ModifiedAt,
                            ModifiedBy = preference.ModifiedBy,
                            PreferenceDetails = preference.PreferenceDetails
                        };
                        var response = await _employeeDbContext.EmployeePreferences.AddAsync(preferenceEntity);
                    }
                    else
                    {
                        var currentPref = currentActivePrefetence.Where(e => e.Id == preference.Id).FirstOrDefault();

                        currentPref.IsActive = preference.IsActive;
                        currentPref.Category = preference.Category;
                        currentPref.PreferenceOrder = preference.PreferenceOrder;
                        currentPref.EmployeeEmail = employeeEmail;
                        currentPref.ModifiedAt = preference.ModifiedAt;
                        currentPref.ModifiedBy = preference.ModifiedBy;
                        currentPref.PreferenceDetails = preference.PreferenceDetails;
                        var response = _employeeDbContext.EmployeePreferences.Update(currentPref);
                    }
                }
            }
            else if (employeePreferences != null && employeePreferences.Count == 0)
            {
                var currentActivePrefetence = await _employeeDbContext
                                .EmployeePreferences.Where(a => a.EmployeeEmail.ToLower().Trim() == employeeEmail.ToLower().Trim() && a.IsActive == true)
                                .ToListAsync();
                if (currentActivePrefetence != null && currentActivePrefetence.Count > 0)
                {
                    foreach (var activeEmployeePref in currentActivePrefetence)
                    {
                        activeEmployeePref.IsActive = false;
                        activeEmployeePref.ModifiedBy = userEmail;
                        activeEmployeePref.ModifiedAt = DateTime.UtcNow;
                        _employeeDbContext.EmployeePreferences.Update(activeEmployeePref);
                    }
                }
                //await _employeeDbContext.SaveChangesAsync();
            }
            await _employeeDbContext.SaveChangesAsync();
            return await GetEmployeePreferencesByEmailAsync(employeeEmail);
        }
        public async Task<List<EmployeePreference>> GetEmployeePreferencesByEmails(List<string> emails)
        {
            try
            {

                // Retrieve data from the database
                var employeePreferences = await _employeeDbContext.EmployeePreferences.ToListAsync();

                // Filter the data in memory
                var result = employeePreferences
                    .Where(m => emails.Any(s => s.Equals(Convert.ToString(m.EmployeeEmail), StringComparison.OrdinalIgnoreCase)))
                    .ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<List<EmployeeProjectMapping>> UpdateEmployeeProjectMapping(List<EmployeeProjectMapping> employeeMapping, string userEmail)
        {
            var employeeEmail = userEmail;
            if (employeeMapping != null && employeeMapping.Count > 0)
            {
                foreach (var item in employeeMapping)
                {
                    var current = await _employeeDbContext.EmployeeProjectMapping
                                    .Where(a => a.EmpMID.ToLower().Trim() == item.EmpMID.ToLower().Trim()
                                    && a.Offering.ToLower().Trim() == item.Offering.ToLower().Trim()
                                    && a.Solution.ToLower().Trim() == item.Solution.ToLower().Trim()
                                    )
                                    .ToListAsync();
                    if (current != null)
                    {
                        var emtityObj = new EmployeeProjectMapping()
                        {
                            IsActive = true,
                            EmpMID = item.EmpMID.Trim(),
                            Offering = item.Offering.Trim(),
                            OfferingId = item.OfferingId?.Trim(),
                            Solution = item.Solution.Trim(),
                            SolutionId = item.SolutionId?.Trim(),
                            CreatedAt = DateTime.UtcNow,
                            CreatedBy = userEmail?.Trim(),
                            ModifiedAt = DateTime.UtcNow,
                            ModifiedBy = userEmail?.Trim(),
                        };
                        var response = await _employeeDbContext.EmployeeProjectMapping.AddAsync(emtityObj);
                    }
                }
            }

            await _employeeDbContext.SaveChangesAsync();

            return employeeMapping;
        }

        public async Task<EmployeeProfile> GetEmployeeProfileByEmployeeEmail(string employee_email)
        {
            var result = await _employeeDbContext.EmployeeProfile
                            .Where(x => x.employee_email.ToLower().Trim() == employee_email.ToLower().Trim())
                            .Include(x => x.employee_language)
                            .Include(x => x.employee_qualification)
                            .Include(x => x.employee_work_experience)
                            .Include(x => x.experience_outside_gt)
                            .FirstOrDefaultAsync();
            return result;
        }
        public async Task<EmployeeProfile> UpdateEmployeeProfile(UpdateEmployeeProfileRequest req)
        {
            try
            {
                var employeeProfile = await GetEmployeeProfileByEmployeeEmail(req.employee_email);
                if (req.year_of_experience != null)
                {
                    employeeProfile.year_of_experience = req.year_of_experience;
                }
                if (req.about_me != null)
                {
                    employeeProfile.about_me = req.about_me;
                }
                if (req.linkedin_url != null)
                {
                    employeeProfile.linkedin_url = req.linkedin_url;
                }
                if (req.present_work_location != null)
                {
                    employeeProfile.present_work_location = req.present_work_location;
                }
                if (req.experience_outside_gt != null)
                {
                    // Separate new and existing experiences
                    var newExperiences = req.experience_outside_gt.Where(x => x.id == 0).ToList();
                    var existingExperiences = req.experience_outside_gt.Where(x => x.id > 0).ToList();

                    // Add new experiences
                    foreach (var newExp in newExperiences)
                    {
                        newExp.employee_profile_id = employeeProfile.id;
                        newExp.created_by = req.employee_email;
                        _employeeDbContext.EmployeeExperienceOutsideGT.Add(newExp);
                    }

                    // Update existing experiences
                    foreach (var exp in existingExperiences)
                    {
                        var existingExp = employeeProfile.experience_outside_gt
                            .FirstOrDefault(x => x.id == exp.id);
                        if (existingExp != null)
                        {
                            // Update only the properties you want to allow updating
                            _employeeDbContext.Entry(existingExp).CurrentValues.SetValues(exp);
                        }
                        else
                        {
                            // This shouldn't normally happen, but handle gracefully
                            exp.employee_profile_id = employeeProfile.id;
                            _employeeDbContext.EmployeeExperienceOutsideGT.Add(exp);
                        }
                    }

                    // Optional: Handle deletions if needed
                    //var existingIds = req.experience_outside_gt.Select(x => x.id).Where(id => id > 0).ToHashSet();
                    //var toDelete = employeeProfile.experience_outside_gt.Where(x => !existingIds.Contains(x.id));
                    //_employeeDbContext.ExperienceOutsideGT.RemoveRange(toDelete);
                }
                _employeeDbContext.EmployeeProfile.Update(employeeProfile);
                if (req.qualification_update != null && req.qualification_update.Count > 0)
                {
                    foreach (var item in req.qualification_update)
                    {
                        var emp_qualification_info = await _employeeDbContext.EmployeeQualification.Where(x => x.id == item.id).FirstOrDefaultAsync();
                        if (emp_qualification_info != null)
                        {
                            emp_qualification_info.is_published = item.is_published;
                            _employeeDbContext.EmployeeQualification.Update(emp_qualification_info);
                        }
                    }
                }
                await _employeeDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw;
            }
            return await GetEmployeeProfileByEmployeeEmail(req.employee_email);

        }
        public async Task<List<EmployeeProjectMapping>> GetEmpByProjectMapping(EmpByProjectMappingRequestDto request)
        {
            var emp = await _employeeDbContext.EmployeeProjectMapping
                .Where(a => a.IsActive == true)
                .ToListAsync();

            var result = emp
                .Where(a => a.IsActive == true
                    && (request.Offerings == null || (request.Offerings != null && request.Offerings.Any(x => x.ToLower().Trim() == a.Offering.ToLower().Trim())))
                    && (request.Solutions == null || (request.Solutions != null && request.Solutions.Any(x => x.ToLower().Trim() == a.Solution.ToLower().Trim())))
                )
                .ToList();

            return result;
        }

    }
}
