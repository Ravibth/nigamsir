using MediatR;
using RMT.Allocation.Application.DTOs;
using RMT.Allocation.Application.DTOs.RequisitionDTOs;
using RMT.Allocation.Application.HttpServices;
using RMT.Allocation.Application.HttpServices.DTOs;
using RMT.Allocation.Application.IHttpServices;
using RMT.Allocation.Application.Mappers;
using RMT.Allocation.Application.Responses;
using RMT.Allocation.Domain.DTO;
using RMT.Allocation.Domain.DTO.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.Handlers.CommandHandlers
{
    public class UploadWcgtValidationCommand : IRequest<List<BulkUploadValidationResponse>>

    {
        public List<BulkCreateRequisitionDTO> bulkRequisitionsValidation { get; set; }
    }

    public class UploadWcgtValidationCommandHandler : IRequestHandler<UploadWcgtValidationCommand, List<BulkUploadValidationResponse>>
    {
        private readonly IWCGTMasterHttpApi _WCGTMasterHttpApi;
        private readonly IIdentityUserDetailsHttpApi _IdentityUserDetailsHttpApi;
        private readonly ISkillHttpServiceApi _ISkillHttpServiceApi;
        private readonly IProjectServiceHttpApi _ProjectServiceHttpApi;

        public UploadWcgtValidationCommandHandler(IWCGTMasterHttpApi wCGTMasterHttpApi, IIdentityUserDetailsHttpApi identityUserDetailsHttpApi, ISkillHttpServiceApi skillHttpServiceApi, IProjectServiceHttpApi projectServiceHttpApi)
        {
            _WCGTMasterHttpApi = wCGTMasterHttpApi;
            _IdentityUserDetailsHttpApi = identityUserDetailsHttpApi;
            _ISkillHttpServiceApi = skillHttpServiceApi;
            _ProjectServiceHttpApi = projectServiceHttpApi;
        }

        public async Task<List<BulkUploadValidationResponse>> Handle(UploadWcgtValidationCommand request, CancellationToken cancellationToken)
        {
            try
            {   //get all  skill code and name
                Task<List<SkillCodeNameDTO>> skillCodes = _ISkillHttpServiceApi.GetSkillCodeName();

                //get all users
                Task<List<UserDTO>> emailResult = _IdentityUserDetailsHttpApi.GetEmailDataHttpApiQuery();

                //get all desigantion
                Task<List<WCGTDesigantionDTO>> desigantionResult = _WCGTMasterHttpApi.GetDesignationWCGTMAsterHttpApiQuery();

                //Task<List<SkillCodeNameDTO>> smegResult = _WCGTMasterHttpApi.GetWCGTMAsterDataHttpApiQuery();
                Task<List<WCGTLocationDTO>> locationResult = _WCGTMasterHttpApi.GetLocationWCGTMAsterHttpApiQuery();
                Task<List<WCGTIndustryDTO>> subIndustryResult = _WCGTMasterHttpApi.GetIndustryWCGTMAsterHttpApiQuery();

                //Get competency master
                Task<List<WcgtCompetencyMasterDTO>> competencyMasterResult = _WCGTMasterHttpApi.GetCompetencyMaster();

                await Task.WhenAll(
                    skillCodes
                    , emailResult
                    , desigantionResult
                    , locationResult
                    , subIndustryResult
                    , competencyMasterResult
                );

                List<BulkCreateRequisitionDTO> result = new();
                var duplicateEmpItems = new List<string>();

                foreach (var req in request.bulkRequisitionsValidation)
                {
                    bool stopFurtherChecks = false;

                    if (req.comments.Count > 0)
                    {
                        result.Add(req);
                        continue;
                    }
                    var comments = new List<string>();

                    var EmailId = req.EmailId;
                    var EmpCode = req.EmpCode;
                    if (EmailId != null && EmpCode != null)
                    {
                        if (!String.IsNullOrEmpty(EmailId) && !IsValidEmail(EmailId))
                        {
                            comments.Add("Email Id must be a valid email address");
                            stopFurtherChecks = true;
                        }
                        else if (!emailResult.Result.Any(e => e.email_id.ToLower().Trim() == EmailId.ToLower().Trim()))
                        {
                            comments.Add("Email Id not found");
                            stopFurtherChecks = true;
                        }
                        else if (!emailResult.Result.Any(e => e.email_id.ToLower().Trim() == EmailId.ToLower().Trim() && e.employee_id.ToLower().Trim() == EmpCode.ToLower().Trim()))
                        {
                            comments.Add("Email Id found but Employee Code does not match");
                            stopFurtherChecks = true;
                        }
                    }

                    if (!String.IsNullOrEmpty(req.EmpCode) && !stopFurtherChecks)
                    {
                        if (duplicateEmpItems.Any(dup => dup.ToLower() == req.EmpCode.ToLower()))
                        {
                            comments.Add("Multiple records not allowed for a user");
                        }
                        else
                        {
                            duplicateEmpItems.Add(req.EmpCode);
                        }
                    }

                    if (req.StartDate != null)
                    {
                        req.StartDate = (DateOnly.FromDateTime((DateTime)req.StartDate)).ToDateTime(TimeOnly.MinValue);
                    }
                    if (req.EndDate != null)
                    {
                        req.EndDate = (DateOnly.FromDateTime((DateTime)req.EndDate)).ToDateTime(TimeOnly.MaxValue);
                    }

                    if (req.DesignationId != null)
                    {
                        var designationMatched = desigantionResult.Result
                            .Where(d => d.designation_id.ToLower().Trim() == req.DesignationId.ToLower().Trim())
                            .FirstOrDefault();

                        if (!(req.DesignationId.Length > 0))
                        {
                            comments.Add("Designation Id can not be Empty");
                        }
                        else if (req.DesignationId.GetType() != typeof(string))
                        {
                            comments.Add("Designation Id must be string type");
                        }
                        else if (designationMatched == null)
                        {
                            comments.Add("Designation Id not found");
                        }
                        else if (designationMatched != null)
                        {
                            req.Designation = designationMatched.designation_name;
                            req.Grade = designationMatched.grade;
                        }
                    }

                    if (req.selectedOption == "requisition")
                    {

                        if (req.NumberOfResources < 1)
                        {
                            comments.Add("Number Of Resources can not be less than 1");
                        }

                        if (String.IsNullOrEmpty(req.CompetencyId))
                        {
                            comments.Add("Competency Id can not be Empty");
                        }
                        else
                        {
                            var competencyMatched = competencyMasterResult.Result
                                .Where(s => s.CompetencyId.ToLower().Trim() == req.CompetencyId.ToLower().Trim())
                                .FirstOrDefault();
                            if (req.CompetencyId.GetType() != typeof(string))
                            {
                                comments.Add("Competency Id must be string type");
                            }

                            else if (competencyMatched == null || competencyMatched?.CompetencyId.ToLower().Trim() != req.CompetencyId.ToLower().Trim())
                            {
                                comments.Add("Competency Id not found");
                            }
                            else
                            {
                                req.Competency = competencyMatched.CompetencyName;
                            }
                        }
                    }

                    List<SkillCodeNameDTO> mandatorySkillCodes = new();
                    List<UserSkillDto> approvedSkill = new();

                    //TODO check aayush 2808 --> expertse commented
                    if (
                        req.selectedOption == "requisition"
                        && !String.IsNullOrEmpty(req.Designation)
                        && !String.IsNullOrEmpty(req.CompetencyId)
                    )
                    {
                        mandatorySkillCodes = await _ISkillHttpServiceApi.GetMandatorySkill(
                            null
                            , req.CompetencyId
                            , req.Designation);
                    }
                    else if (req.selectedOption == "allocation")
                    {

                        approvedSkill = await _ISkillHttpServiceApi.GetApprovedSkill(new List<string> { req.EmailId });
                    }

                    if (req.skills != null)
                    {
                        List<string> skillsEntered = req.skills.Split(",").ToList();
                        List<SkillCodeNameDTO> skillList = new();

                        if (!(req.skills.Length > 0))
                        {
                            comments.Add("Skill Code can not be Empty");
                        }
                        foreach (var skillItem in skillsEntered)
                        {

                            if (skillItem.GetType() != typeof(string))
                            {
                                comments.Add($"Skill Code Id {skillItem} must be string type");
                            }
                            else if (!String.IsNullOrEmpty(req.CompetencyId) && !IsSkillCodePresent(skillItem, skillCodes.Result))
                            {
                                comments.Add($"Skill Code {skillItem} not found");
                            }
                            else if (req.selectedOption == "requisition"
                                && !String.IsNullOrEmpty(req.CompetencyId)
                                && competencyMasterResult.Result.Any(s => s.CompetencyId.ToLower().Trim() == req.CompetencyId.ToLower().Trim())
                                && !IsSkillCodePresent(skillItem, mandatorySkillCodes))
                            {
                                comments.Add($"Mandatory skill code {skillItem} not mapped in competency");
                            }
                            else if (req.selectedOption == "requisition"
                                && !String.IsNullOrEmpty(req.CompetencyId)
                                && competencyMasterResult.Result.Any(s => s.CompetencyId.ToLower().Trim() == req.CompetencyId.ToLower().Trim())
                                && !IsSkillMappedInCompetency(skillItem, mandatorySkillCodes, req.CompetencyId))
                            {
                                comments.Add($"Mandatory skill code {skillItem} not mapped in competency");
                            }
                            else if (req.selectedOption == "allocation" && !IsApprovedSkillPresent(approvedSkill, skillItem) && !stopFurtherChecks)
                            {
                                comments.Add($"Skill {skillItem} not approved");
                            }
                            else
                            {
                                skillList.AddRange(GetSkillNameAndCode(skillItem, skillCodes.Result));
                            }
                        }
                        req.SkillList = skillList;

                    }

                    if (req.locationCode != null)
                    {

                        if (!(req.locationCode.Length > 0))
                        {
                            comments.Add("Location code can not be Empty");
                        }
                        if (req.locationCode.GetType() != typeof(string))
                        {
                            comments.Add("Location code must be string type");
                        }
                        else if (!locationResult.Result.Any(l => l.location_id.ToLower().Trim() == req.locationCode.ToLower().Trim()))
                        {
                            comments.Add("Location code not found");
                        }
                        else if (locationResult.Result.Any(l => l.location_id.ToLower().Trim() == req.locationCode.ToLower().Trim()))
                        {
                            req.locations = locationResult.Result.Find(l => l.location_id.ToLower().Trim() == req.locationCode.ToLower().Trim()).location_name;
                        }
                    }

                    if (req.industryID != null)
                    {
                        if (!(req.industryID.Length > 0))
                        {
                            comments.Add("Industry Id can not be Empty");
                        }
                        else if (req.industryID.GetType() != typeof(string))
                        {
                            comments.Add("Industry Id must be string type");
                        }
                        else if (!subIndustryResult.Result.Any(l => l.industry_id.ToLower().Trim() == req.industryID.ToLower().Trim()))
                        {
                            comments.Add("Industry Id not found");
                        }
                        else if (subIndustryResult.Result.Any(l => l.industry_id.ToLower().Trim() == req.industryID.ToLower().Trim()))
                        {
                            var industryData = subIndustryResult.Result.Find(l => l.industry_id.ToLower().Trim() == req.industryID.ToLower().Trim());
                            req.Industry = industryData.industry_name;
                        }
                    }

                    if (req.subIndustryID != null)
                    {
                        var matchedSubIndustryResult = subIndustryResult.Result
                            .Where(l => l.sub_industry_id.ToLower().Trim() == req.subIndustryID.ToLower().Trim())
                            .FirstOrDefault();

                        if (!(req.subIndustryID.Length > 0))
                        {
                            comments.Add("Sub Industry Id can not be Empty");
                        }
                        else if (req.subIndustryID.GetType() != typeof(string))
                        {
                            comments.Add("Sub Industry Id must be string type");
                        }
                        else if (matchedSubIndustryResult == null)
                        {
                            comments.Add("Sub Industry Id not found");
                        }
                        else if (matchedSubIndustryResult != null && req.industryID != null && matchedSubIndustryResult.industry_id != null && matchedSubIndustryResult.industry_id != req.industryID)
                        {
                            comments.Add("Sub Industry does not exist in given Industry");
                        }
                        else if (matchedSubIndustryResult != null)
                        {
                            var subIndustryData = subIndustryResult.Result.Find(l => l.sub_industry_id.ToLower().Trim() == req.subIndustryID.ToLower().Trim());
                            req.subIndustry = subIndustryData.sub_industry_name;
                            req.Industry = subIndustryData.industry_name;
                        }
                    }



                    var StartDate = req.StartDate;
                    var EndDate = req.EndDate;
                    var ProjectStartDate = req.projectStartDate;
                    var ProjectEndDate = req.projectEndDate;
                    if (StartDate != null && StartDate.HasValue && DateTime.TryParse(Convert.ToString(StartDate), out DateTime startDate))
                    {
                        if (startDate < DateTime.Now.Date)
                        {
                            comments.Add("Start date cannot be less than the current date");
                        }
                        else if (startDate < ProjectStartDate)
                        {
                            comments.Add("Start date cannot be less than project start date");
                        }
                        else if (startDate >= ProjectEndDate)
                        {
                            comments.Add("Start date cannot be greater than or equal to project end date");
                        }
                        else if (startDate != ProjectStartDate && startDate < ProjectStartDate)
                        {
                            comments.Add("Start date must be equal to or greater than project start date");
                        }
                    }
                    if (EndDate != null && DateTime.TryParse(Convert.ToString(EndDate), out DateTime endDate) && DateTime.TryParse(Convert.ToString(StartDate), out DateTime startdate))
                    {
                        if (endDate < startdate)
                        {
                            comments.Add("End date cannot be less than the start date");
                        }
                        else if (endDate < DateTime.Now.Date)
                        {
                            comments.Add("End date cannot be less than the current date");
                        }
                        else if (endDate > ProjectEndDate)
                        {
                            comments.Add("End date cannot be greater than project end date");
                        }

                    }



                    if (req.StartDate != null)
                    {
                        req.StartDate = new DateTime(req.StartDate.Value.Year, req.StartDate.Value.Month, req.StartDate.Value.Day, 12, 0, 0);
                    }

                    if (req.EndDate != null)
                    {
                        req.EndDate = new DateTime(req.EndDate.Value.Year, req.EndDate.Value.Month, req.EndDate.Value.Day, 12, 0, 0);
                    }
                    var resp = req;
                    resp.comments = comments;
                    if (comments.Count == 0)
                    {
                        resp.status = true;
                    }
                    else
                    {
                        resp.status = false;
                    }

                    if (EmailId != null && EmpCode != null)
                    {

                        if (emailResult.Result.Any(e => e.email_id.ToLower().Trim() == EmailId.ToLower().Trim()))
                        {
                            req.EmailId = emailResult.Result.Find(e => e.email_id.ToLower().Trim() == EmailId.ToLower().Trim()).uemail_id;
                        }
                        else if (emailResult.Result.Any(e => e.email_id.ToLower().Trim() == EmailId.ToLower().Trim() && e.employee_id.ToLower().Trim() == EmpCode.ToLower().Trim()))
                        {
                            req.EmpCode = emailResult.Result.Find(e => e.email_id.ToLower().Trim() == EmailId.ToLower().Trim() && e.employee_id.ToLower().Trim() == EmpCode.ToLower().Trim()).employee_id;
                        }
                        else
                        {
                            var inputEmail = req.EmailId.Split("__").LastOrDefault();
                            if (!String.IsNullOrEmpty(inputEmail))
                            {
                                req.EmailId = inputEmail;
                            }
                        }
                    }

                    result.Add(resp);

                }
                var response = AllocationMapper.Mapper.Map<List<BulkUploadValidationResponse>>(result);
                return response;
            }
            catch (Exception ex)
            {
                throw;
            }



        }
        private List<SkillCodeNameDTO> GetSkillNameAndCode(string skillCodes, List<SkillCodeNameDTO> skillNameCode)
        {
            string[] codes = skillCodes.Split(',');
            List<SkillCodeNameDTO> skillList = new List<SkillCodeNameDTO>();

            foreach (string code in codes)
            {
                var skill = skillNameCode.FirstOrDefault(skill => skill.SkillCode.Trim().ToLower() == code.Trim().ToLower());
                if (skill != null)
                {
                    skillList.Add(new SkillCodeNameDTO
                    {
                        Competency = skill.Competency,
                        CompetencyId = skill.CompetencyId,
                        SkillCode = skill.SkillCode,
                        SkillName = skill.SkillName
                    });
                }
            }

            return skillList;
        }
        private bool IsValidEmail(string email)
        {
            var emailRegex = new System.Text.RegularExpressions.Regex(@"^[^\s@]+@[^\s@]+\.[^\s@]+$");
            return emailRegex.IsMatch(email);
        }
        private bool IsApprovedSkillPresent(List<UserSkillDto> approvedSkill, string inputSkillCode)
        {
            Boolean isPresent = false;
            if (approvedSkill == null || approvedSkill.Count == 0)
            {
                return isPresent;
            }
            string[] codes = inputSkillCode.Split(',');
            List<string> approvedSkillCode = approvedSkill.Select(code => code.skillCode).ToList();
            foreach (string code in codes)
            {
                if (approvedSkillCode.Contains(code))
                {
                    isPresent = true;
                }
                else
                {
                    isPresent = false;
                    break;
                }
            }
            return isPresent;
        }

        private bool IsSkillMappedInCompetency(string skill, List<SkillCodeNameDTO> skillCodes, string competencyId)
        {
            string[] skillCode = skill.Split(',');
            Boolean IsValid = true;
            foreach (string code in skillCode)
            {
                var result = skillCodes
                    .Where(a =>
                        a.SkillCode == code
                        && a.CompetencyId != null
                        && a.CompetencyId.Count > 0
                        && a.CompetencyId
                            .Select(m => m.ToLower().Trim())
                            .ToList()
                            .Contains(competencyId.ToLower().Trim())
                     )
                    .ToList();

                if (result.Count == 0)
                {
                    IsValid = false;
                    break;
                }
            }
            return IsValid;
        }

        private bool IsSkillCodePresent(string skill, List<SkillCodeNameDTO> skillCodes)
        {
            string[] skillCode = skill.Split(',');
            Boolean IsValid = true;
            foreach (string code in skillCode)
            {
                var result = skillCodes
                    .Where(a =>
                        a.SkillCode.ToLower().Trim() == code.ToLower().Trim()
                     )
                    .ToList();

                if (result.Count == 0)
                {
                    IsValid = false;
                    break;
                }
            }
            return IsValid;
        }

        private string GetSkillNameFromCode(string skillCodes, List<SkillCodeNameDTO> skillNameCode)
        {
            string[] codes = skillCodes.Split(',');
            string skillName = "";
            int i = 0;
            foreach (string code in codes)
            {
                if (i > 0)
                {
                    skillName += ",";
                }
                i++;
                skillName += skillNameCode.Where(skill => skill.SkillCode.Trim().ToLower() == code.Trim().ToLower()).FirstOrDefault().SkillName;
            }
            return skillName;
        }
    }
}
