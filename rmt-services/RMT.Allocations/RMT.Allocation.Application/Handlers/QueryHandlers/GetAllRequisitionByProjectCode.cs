using MediatR;
using Newtonsoft.Json;
using RMT.Allocation.Application.HttpServices;
using RMT.Allocation.Application.HttpServices.DTOs;
using RMT.Allocation.Application.IHttpServices;
using RMT.Allocation.Application.Mappers;
using RMT.Allocation.Application.Responses;
using RMT.Allocation.Application.Utils;
using RMT.Allocation.Domain;
using RMT.Allocation.Domain.DTO;
using RMT.Allocation.Domain.DTO.Request;
using RMT.Allocation.Domain.DTO.Response;
using RMT.Allocation.Domain.Entities;
using RMT.Allocation.Domain.Repositories;
using RMT.Scheduler.service;
using System.Collections.Generic;
using static RMT.Allocation.Domain.ConstantsDomain;
using static RMT.Allocation.Infrastructure.Constants;
using static System.Formats.Asn1.AsnWriter;

namespace RMT.Allocation.Application.Handlers.QueryHandlers
{
    public class GetAllRequisitionByProjectCodeQuery : IRequest<List<GetAllRequisitionByProjectCodeResponse>>
    {
        //TODO : new to remove limit and PAgination 
        public int limit { get; set; }
        public int pagination { get; set; }
        public string PipelineCode { get; set; }
        public string? JobCode { get; set; }
        public string? currentEmp { get; set; }
        public string? UserEmail { get; set; }
        public bool? ScoreCalculationForRequisitionIdsAllowed { get; set; } = false;
        public bool? IsRequsitionFilterByProjectRoles { get; set; } = false;
        public UserDecorator? UserInfo { get; set; }
    }

    public class GetAllRequisitionByProjectCodeQueryHandler : IRequestHandler<GetAllRequisitionByProjectCodeQuery, List<GetAllRequisitionByProjectCodeResponse>>
    {
        private readonly IRequisitionRepository _requisitionRepository;
        private readonly IEmployeeMasterHttpApi _employeeMasterHttpApi;
        private readonly IEmployeePreferencesHttpApi _employeePreferencesHttpApi;
        private readonly IGetEmployeeLeavesHttpApi _getEmployeeLeavesHttpApi;
        //private readonly IMarketPlaceHttpApi _marketPlaceHttpApi;
        private readonly IDatesUtils _datesUtils;
        private readonly ITokenService _tokenService;
        private readonly IProjectServiceHttpApi _projectServiceHttpApi;
        private readonly IConfigurationHttpService _configurationHttpService;
        private readonly IWCGTMasterHttpApi _wCGTMasterHttpApi;
        private readonly IMediator _mediator;
        private readonly ISkillHttpServiceApi _skillHttpServiceApi;

        public GetAllRequisitionByProjectCodeQueryHandler(
            IRequisitionRepository requisitionRepository
            , IEmployeeMasterHttpApi employeeMasterHttpApi
            , IEmployeePreferencesHttpApi employeePreferencesHttpApi
            //, IMarketPlaceHttpApi marketPlaceHttpApi
            , IGetEmployeeLeavesHttpApi getEmployeeLeavesHttpApi
            , IDatesUtils datesUtils
            , ITokenService tokenService
            , IProjectServiceHttpApi projectServiceHttpApi
            , IConfigurationHttpService configurationHttpService
            , IWCGTMasterHttpApi wCGTMasterHttpApi
            , IMediator mediator
            , ISkillHttpServiceApi skillHttpServiceApi

            )
        {
            _requisitionRepository = requisitionRepository;
            _employeeMasterHttpApi = employeeMasterHttpApi;
            _employeePreferencesHttpApi = employeePreferencesHttpApi;
            //_marketPlaceHttpApi = marketPlaceHttpApi;
            _getEmployeeLeavesHttpApi = getEmployeeLeavesHttpApi;
            _datesUtils = datesUtils;
            _tokenService = tokenService;
            _projectServiceHttpApi = projectServiceHttpApi;
            _configurationHttpService = configurationHttpService;
            _wCGTMasterHttpApi = wCGTMasterHttpApi;
            _mediator = mediator;
            _skillHttpServiceApi = skillHttpServiceApi;
        }

        public async Task<List<GetAllRequisitionByProjectCodeResponse>> Handle(GetAllRequisitionByProjectCodeQuery request, CancellationToken cancellationToken)
        {
            List<Requisition> result = await _requisitionRepository.GetAllRequisitionByProjectCode(request.PipelineCode, request.JobCode);

            List<GetAllRequisitionByProjectCodeResponse> AllRequisitionResponse = AllocationMapper.Mapper.Map<List<GetAllRequisitionByProjectCodeResponse>>(result);
            if (request.ScoreCalculationForRequisitionIdsAllowed == true)
            {
                if (AllRequisitionResponse != null && AllRequisitionResponse.Count > 0 && !string.IsNullOrEmpty(request.currentEmp))
                {
                    DateTime reqMinDate = AllRequisitionResponse.Select(x => x.StartDate).Min().ToDateTime(TimeOnly.MinValue);
                    DateTime reqMaxDate = AllRequisitionResponse.Select(x => x.EndDate).Max().ToDateTime(TimeOnly.MaxValue);

                    var employeeFinalObj = await GetEmployeeFinalDto(request.currentEmp, reqMinDate, reqMaxDate);

                    if (employeeFinalObj != null && employeeFinalObj.Count > 0 && AllRequisitionResponse != null && AllRequisitionResponse.Count > 0)
                    {
                        for (int i = 0; i < AllRequisitionResponse.Count; i++)
                        {
                            bool designationMatch = !string.IsNullOrEmpty(AllRequisitionResponse[i].Designation) && !string.IsNullOrEmpty(employeeFinalObj[0].designation)
                                && AllRequisitionResponse[i].Designation.ToLower().Trim() == employeeFinalObj[0].designation.ToLower().Trim();

                            var requisitionMandatorySkills = AllRequisitionResponse[i].RequisitionSkill
                                .Where(m =>
                                    m?.Type != null
                                    && m?.Type.ToLower() == RequisitionSkillType.MANDATORY_SKILL.ToLower())
                                .ToList();

                            var matchedUserSkillsWithMandatoryReqSkills = requisitionMandatorySkills
                                        .All(m =>
                                            employeeFinalObj[0].skill
                                            .Any(p =>
                                                        p.SkillName.ToLower().Trim() == m.SkillName.ToLower().Trim()
                                                        && p.SkillCode.ToLower().Trim() == m.SkillCode.ToLower().Trim()
                                            ));

                            if (!matchedUserSkillsWithMandatoryReqSkills || !designationMatch)
                            {
                                AllRequisitionResponse[i].Score = 0.ToString();
                                AllRequisitionResponse[i].Suggestion = null;
                            }
                            else
                            {
                                var val = await GetMatchingScoreForRequisitionId(request.limit, request.pagination, AllRequisitionResponse[i].Id, employeeFinalObj);
                                if (val.Count != 0)
                                {
                                    AllRequisitionResponse[i].Score = val[0].score;
                                    AllRequisitionResponse[i].Suggestion = JsonConvert.SerializeObject(val[0]);
                                }
                                else
                                {
                                    AllRequisitionResponse[i].Score = 0.ToString();
                                    AllRequisitionResponse[i].Suggestion = null;
                                }
                            }
                        }
                    }
                }
            }
            if (!string.IsNullOrEmpty(request.UserEmail) && request.IsRequsitionFilterByProjectRoles != null && (bool)request.IsRequsitionFilterByProjectRoles)
            {
                //get current user role 
                //1. ResourceRequestor --> all requesions
                //2. AdditionalDelegate OR AdditionalEl --> ConfigCheck --> Result
                //3. If Config True -> show All 
                //4. If Config False -> Show self


                return await Helper.GetRequsitionListByCurrentUserRole(AllRequisitionResponse, request.PipelineCode, request.JobCode, request.UserEmail, _projectServiceHttpApi, _configurationHttpService, request.UserInfo);

            }

            return AllRequisitionResponse;
        }

        public async Task<List<SystemSuggestionResponseDTO>> GetMatchingScoreForRequisitionId(int limit, int pagination, Guid requisitionId, List<EmployeeDetailsDTO> employeesFinal)
        {
            try
            {
                //Fetch Requisition Details
                Requisition requisitionDetails = await _requisitionRepository.GetRequisitionDetailsByRequisitionId(requisitionId, true);
                if (requisitionDetails == null)
                {
                    throw new InvalidOperationException("Invalid Requisition");
                }

                //Will be fetched from config microservice later
                var pref_weightage_constraint = 0.8;
                var suggestions = await _requisitionRepository.GetSystemSuggestions(limit, pagination, pref_weightage_constraint, employeesFinal, requisitionDetails, requisitionId, 0, Array.Empty<object>(), Array.Empty<string>(), "DESC");
                return suggestions;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private async Task<List<EmployeeDetailsDTO>> GetEmployeeFinalDto(string currentEmp, DateTime startDate, DateTime endDate)
        {
            var emails = new List<string> { currentEmp };

            var getEmployeeMasterDetailsInfo = new GetEmployeeMasterDetailsDTO()
            {
                Emails = emails,
            };

            var getEmployeePreferenceDetailsInfo = new GetEmployeePreferenceDetailsDTO()
            {
                email = emails.ToArray(),
            };

            var identityToken = await _tokenService.GetToken();
            Task<List<UserSkillDto>> allUserSkills = _skillHttpServiceApi.GetApprovedSkill(new List<string> { currentEmp });
            Task<List<EmployeeMasterDTO>> employeesMaster = _employeeMasterHttpApi.GetEmployeeMasterDataHttpApiQuery(getEmployeeMasterDetailsInfo, identityToken);
            Task<List<GetResignedAndAbscondedUsersWithLastAvailableDayResponseDto>> abscondedResignedUsers = _wCGTMasterHttpApi.GetResignedAndAbscondedUsersByEmails(emails.ToList());
            GetEmployeeLeaves getEmployeeLeaves = new()
            {
                emails = emails.ToList(),
                start_date = startDate,
                end_date = endDate
            };
            Task<List<EmployeeLeavesDTO>> employeeLeaves = _getEmployeeLeavesHttpApi.GetEmployeeLeavesByEmails(getEmployeeLeaves);
            Task<int> weekends = _datesUtils.GetNumberOfWeekends(startDate, endDate);

            //Fetch the preferred details of all the employees
            Task<List<EmployeePreferencesByEmailDTO>> pref_data = _employeePreferencesHttpApi.GetEmployeePreferenceDetailsByEmails(getEmployeePreferenceDetailsInfo);
            var availabilities = _mediator.Send(new GetAvaiableHoursByEmailIdQery()
            {
                EmailId = emails.ToArray(),
                StartDate = startDate,
                EndDate = endDate,
                RequireWorkingHours = 0,
                isPerDayHourAllocation = false
            });

            await Task.WhenAll(allUserSkills, employeesMaster, employeeLeaves, pref_data, weekends, availabilities);

            var employeePrefDataFetched = pref_data.Result
            .GroupBy(emp => emp.Email)
            .Select(usergroup => new
            {
                email = usergroup.Key,
                category = usergroup
                .SelectMany(emp => emp.EmployeePreference)
                .GroupBy(emp => emp.Category)
                .ToDictionary(
                    categoryGroup => categoryGroup.Key.ToLower(),
                    categoryGroup => categoryGroup.Select(prefData => prefData.Name).Distinct().ToList())
            });
            List<EmployeeDetailsDTO> employeesFinal = new() { };
            foreach (var emp in employeesMaster.Result)
            {
                var userSkills = allUserSkills.Result
                    .Where(m => m.email.ToLower() == emp.email.ToLower())
                    .ToList();

                var userSkillsAsSkillEntities = new List<SkillsEntities>();
                if (userSkills != null && userSkills.Count > 0)
                {
                    foreach (var skillItem in userSkills)
                    {
                        userSkillsAsSkillEntities.Add(new()
                        {
                            SkillCode = skillItem.skillCode.Trim(),
                            SkillName = skillItem.skillName.Trim()
                        });
                    }
                }

                var matchingUserData = employeePrefDataFetched.FirstOrDefault(m => m.email == emp.email);
                EmployeeLeavesDTO userLeavesFetched = employeeLeaves.Result.FirstOrDefault(m => m.email.ToLower() == emp.email.ToLower());
                var isUserAbscondedOrResigned = abscondedResignedUsers.Result.Where(m => m.email_id.ToLower().Trim() == emp.email.ToLower().Trim()).FirstOrDefault();
                var leaves = await _getEmployeeLeavesHttpApi.CalculateTotalUserLeavesInHours(userLeavesFetched, weekends.Result, 8);
                var isUserAvailable = availabilities.Result.Where(m => m.EmailId.ToLower() == emp.email.ToLower()).FirstOrDefault();

                var employ = new EmployeeDetailsDTO
                {
                    empName = emp.empName,
                    email = emp.email,
                    designation = emp.designation,
                    location = emp.location,
                    business_unit = emp.business_unit,
                    supercoach_name = emp.supercoach_name,
                    co_supercoach_name = emp.co_supercoach_name,
                    supercoach_mid = emp.supercoach_mid,
                    co_supercoach_mid = emp.co_supercoach_mid,
                    employee_id = emp.employee_id,
                    uemail_id = emp.uemail_id,
                    competency = emp.competency,
                    competencyId = emp.competencyId,
                    grade = emp.grade,
                    supercoach = emp.supercoach,
                    revenue_unit = emp.revenue_unit,//Recheck
                    sub_industry = emp.sub_industry,
                    industry = emp.industry,
                    skill = userSkillsAsSkillEntities != null && userSkillsAsSkillEntities.Count > 0 ? userSkillsAsSkillEntities.ToArray() : Array.Empty<SkillsEntities>(),
                    interested = false,
                    offerings = new List<string> { }.ToArray(),
                    solutions = new List<string> { }.ToArray(),
                    pref_location = matchingUserData != null && matchingUserData.category.ContainsKey("location") ? matchingUserData.category["location"].ToArray() : Array.Empty<string>(),
                    pref_skill = matchingUserData != null && matchingUserData.category.ContainsKey("skill") ? matchingUserData.category["skill"].ToArray() : Array.Empty<string>(),
                    pref_industry = matchingUserData != null && matchingUserData.category.ContainsKey("industry") ? matchingUserData.category["industry"].ToArray() : Array.Empty<string>(),
                    pref_sub_industry = matchingUserData != null && matchingUserData.category.ContainsKey("sub_industry") ? matchingUserData.category["sub_industry"].ToArray() : Array.Empty<string>(),
                    pref_business_unit = matchingUserData != null && matchingUserData.category.ContainsKey("bussiness_unit") ? matchingUserData.category["bussiness_unit"].ToArray() : Array.Empty<string>(),
                    pref_revenue_unit = matchingUserData != null && matchingUserData.category.ContainsKey("revenue_unit") ? matchingUserData.category["revenue_unit"].ToArray() : Array.Empty<string>(),
                    pref_offerings = matchingUserData != null && matchingUserData.category.ContainsKey("offering") ? matchingUserData.category["offering"].ToArray() : Array.Empty<string>(),
                    pref_solutions = matchingUserData != null && matchingUserData.category.ContainsKey("solution") ? matchingUserData.category["solution"].ToArray() : Array.Empty<string>(),
                    leaves = leaves,
                    last_available_day = isUserAbscondedOrResigned != null ? isUserAbscondedOrResigned.last_available_day : null,
                    available = isUserAvailable != null ? isUserAvailable.IsHoursAvialable : false,
                };
                employeesFinal.Add(employ);
            }
            return employeesFinal;
        }
    }
}
