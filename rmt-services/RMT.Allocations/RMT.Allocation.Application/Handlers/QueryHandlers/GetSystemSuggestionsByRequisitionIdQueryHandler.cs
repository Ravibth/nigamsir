using MediatR;
using RMT.Allocation.Domain.DTO;
using RMT.Allocation.Domain.Entities;
using RMT.Allocation.Domain.Repositories;
using RMT.Allocation.Application.HttpServices;
using RMT.Allocation.Application.HttpServices.DTOs;
using RMT.Allocation.Application.IHttpServices;
using RMT.Allocation.Application.Utils;
using RMT.Scheduler.service;
using RMT.Allocation.Infrastructure;
using Constants = RMT.Allocation.Infrastructure.Constants;
using RMT.Allocation.Application.DTOs;
using RMT.Allocation.Domain.DTO.Response;
using System.Dynamic;
using RMT.Allocation.Domain.DTO.Request;
using Microsoft.Extensions.Configuration;

namespace RMT.Allocation.Application.Handlers.QueryHandlers
{

    public class GetSystemSuggestionsByRequisitionIdQuery : IRequest<List<SystemSuggestionResponseDTO>>
    {
        public Guid requisitionId { get; set; }
        public int limit { get; set; }
        public int pagination { get; set; }
        public string[]? parameter_value_pairs { get; set; }
    }

    public class GetSystemSuggestionsByRequisitionIdQueryHandler : IRequestHandler<GetSystemSuggestionsByRequisitionIdQuery, List<SystemSuggestionResponseDTO>>
    {
        private readonly IRequisitionRepository _requisitionRepository;
        private readonly IMediator _mediator;
        private readonly IEmployeeMasterHttpApi _employeeMasterHttpApi;
        private readonly IEmployeePreferencesHttpApi _employeePreferencesHttpApi;
        private readonly IGetEmployeeLeavesHttpApi _getEmployeeLeavesHttpApi;
        private readonly IMarketPlaceHttpApi _marketPlaceHttpApi;
        private readonly IDatesUtils _datesUtils;
        private readonly ITokenService _tokenService;
        private readonly IConfigurationHttpService _configurationHttpService;
        private readonly ISkillHttpServiceApi _skillHttpServiceApi;
        private readonly IWCGTMasterHttpApi _wCGTMasterHttpApi;
        private readonly IConfiguration _config;

        public GetSystemSuggestionsByRequisitionIdQueryHandler(
            IRequisitionRepository requisitionRepository
            , IEmployeeMasterHttpApi employeeMasterHttpApi
            , IEmployeePreferencesHttpApi employeePreferencesHttpApi
            , IMarketPlaceHttpApi marketPlaceHttpApi
            , IGetEmployeeLeavesHttpApi getEmployeeLeavesHttpApi
            , IDatesUtils datesUtils
            , ITokenService tokenService
            , IConfigurationHttpService configurationHttpService
            , ISkillHttpServiceApi skillHttpServiceApi
            , IWCGTMasterHttpApi wCGTMasterHttpApi
            , IMediator mediator
            , IConfiguration config
        )
        {
            _requisitionRepository = requisitionRepository;
            _employeeMasterHttpApi = employeeMasterHttpApi;
            _employeePreferencesHttpApi = employeePreferencesHttpApi;
            _marketPlaceHttpApi = marketPlaceHttpApi;
            _getEmployeeLeavesHttpApi = getEmployeeLeavesHttpApi;
            _datesUtils = datesUtils;
            _tokenService = tokenService;
            _configurationHttpService = configurationHttpService;
            _skillHttpServiceApi = skillHttpServiceApi;
            _wCGTMasterHttpApi = wCGTMasterHttpApi;
            _mediator = mediator;
            _config = config;
        }

        public async Task<List<SystemSuggestionResponseDTO>> Handle(GetSystemSuggestionsByRequisitionIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                Int32 limitConfig = Convert.ToInt32(_config.GetSection("MicroserviceApiSettings").GetSection("PaginationLimit").Value);
                if (request.limit > limitConfig)
                {
                    throw new Exception($"Invalid limit, Request limit is max allowed value is {limitConfig}");
                }
                //Fetch Requisition Details
                Requisition requisitionDetails = await _requisitionRepository.GetRequisitionDetailsByRequisitionId(request.requisitionId, true);
                if (requisitionDetails == null)
                {
                    throw new InvalidOperationException("Invalid Requisition");
                }
                else
                {
                    //Get user master data with leaves and holidays
                    GetUserLeaveHolidayWithUserMasterRequestDTOForSystemSuggestion getUserLeaveHolidaysWithUserMasterRequest = new()
                    {
                        designation = requisitionDetails.Designation,
                        start_date = requisitionDetails.StartDate.ToDateTime(TimeOnly.MinValue),
                        end_date = requisitionDetails.EndDate.ToDateTime(TimeOnly.MaxValue)
                    };
                    Task<List<GetUserLeaveHolidayWithUserMasterResponseForSystemSuggestion>> getUserLeaveHolidaysWithUserMasterResponse = _wCGTMasterHttpApi.GetUserLeaveHolidayResponseForSystemSuggestion(getUserLeaveHolidaysWithUserMasterRequest);

                    //Fetch users who showed interest in marketplace
                    Task<string[]> usersInterested = _marketPlaceHttpApi.GetListOfUsersInterestedByPipelineCode(requisitionDetails.PipelineCode, requisitionDetails.JobCode);

                    //TODO check aayush 2808 --> expertse changed to BusinessUnit
                    Task<int> configurationDetails = GetMinimumPercentageForSystemSuggestions(requisitionDetails.BusinessUnit, requisitionDetails.Offerings);

                    await Task.WhenAll(getUserLeaveHolidaysWithUserMasterResponse, usersInterested, configurationDetails);

                    var initialEmails = getUserLeaveHolidaysWithUserMasterResponse.Result.Select(emp => emp.email_id).ToArray();

                    List<UserSkillDto> allUserSkills = await _skillHttpServiceApi.GetApprovedSkill(initialEmails.ToList());
                    var requisitionMandatorySkills = requisitionDetails.RequisitionSkill.Where(m => m?.Type != null && m?.Type.ToLower() == Constants.RequisitionSkillType.MANDATORY_SKILL.ToLower()).ToList();

                    var skillsByUser = allUserSkills
                        .GroupBy(m => m.email)
                        .ToList();

                    var usersFulfillingMandatorySkills = skillsByUser
                        .Where(userSkills =>
                            requisitionMandatorySkills
                                .All(reqSkill =>
                                    userSkills
                                        .Any(skill =>
                                                skill.skillCode == reqSkill.SkillCode
                                                && skill.skillName == reqSkill.SkillName
                                            )
                                        )
                                ).ToList();
                    var finalEmployeesMasterToUse = getUserLeaveHolidaysWithUserMasterResponse.Result
                        .Where(m => usersFulfillingMandatorySkills
                            .Any(user =>
                                    user.Key.ToLower() == m.email_id.ToLower()
                                )
                            )
                        .ToList();

                    var getEmployeePreferenceDetailsInfo = new GetEmployeePreferenceDetailsDTO()
                    {
                        email = finalEmployeesMasterToUse.Select(emp => emp.email_id).ToArray()
                    };

                    requisitionDetails.StartDate = requisitionDetails.StartDate >= DateOnly.FromDateTime(DateTime.Now)
                                        ? requisitionDetails.StartDate
                                        : DateOnly.FromDateTime(DateTime.Now);

                    Task<List<GetUserAvailabilitiesForSystemSuggestionResponse>> finalUsersAndAvailabilities = _mediator.Send(new GetUserAvailabilitiesForSystemSuggestionQuery()
                    {
                        pipeline_code = requisitionDetails.PipelineCode,
                        job_code = requisitionDetails.JobCode,
                        start_date = requisitionDetails.StartDate.ToDateTime(TimeOnly.MinValue),
                        end_date = requisitionDetails.EndDate.ToDateTime(TimeOnly.MaxValue),
                        required_hours = requisitionDetails.TotalHours,
                        users = finalEmployeesMasterToUse
                    });

                    //Task<int> weekends = _datesUtils.GetNumberOfWeekends(requisitionDetails.StartDate.ToDateTime(TimeOnly.MinValue), requisitionDetails.EndDate.ToDateTime(TimeOnly.MaxValue));
                    //Task<List<GetResignedAndAbscondedUsersWithLastAvailableDayResponseDto>> abscondedResignedUsers = _wCGTMasterHttpApi.GetResignedAndAbscondedUsersByEmails(emails.ToList());

                    //Fetch the preferred details of all the employees
                    Task<List<EmployeePreferencesByEmailDTO>> pref_data = _employeePreferencesHttpApi.GetEmployeePreferenceDetailsByEmails(getEmployeePreferenceDetailsInfo);
                    Task<List<EmployeeOfferingSolutionResp>> employeeOfferingSolutionResp = _employeeMasterHttpApi.GetEmpByProjectMapping(null, null);

                    await Task.WhenAll(
                        pref_data
                        , finalUsersAndAvailabilities
                        , employeeOfferingSolutionResp
                    );

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

                    foreach (var emp in finalUsersAndAvailabilities.Result)
                    {
                        var matchingUserData = employeePrefDataFetched.FirstOrDefault(m => m.email.ToLower().Trim() == emp.email_id.ToLower().Trim());
                        var userSkills = allUserSkills.Where(m => m.email.ToLower() == emp.email_id.ToLower()).ToList();
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
                        var matchedUserSkillsWithMandatoryReqSkills = requisitionMandatorySkills
                            .All(m => userSkills.Any(p =>
                                                p.skillName.ToLower().Trim() == m.SkillName.ToLower().Trim()
                                                && p.skillCode.ToLower().Trim() == m.SkillCode.ToLower().Trim()
                                ));

                        var employeeMid = emp.email_id.Split("__").FirstOrDefault();

                        var employeesOfferSolution = employeeOfferingSolutionResp.Result
                            .FirstOrDefault(m => employeeMid != null && m.emp_mid.ToLower().Trim() == employeeMid.ToLower().Trim());


                        //Remove user who do not have mandatory skills
                        if (matchedUserSkillsWithMandatoryReqSkills)
                        {
                            var employ = new EmployeeDetailsDTO
                            {
                                empName = emp.emp_name,
                                email = emp.email_id,
                                designation = emp.designation,
                                grade = emp.grade,
                                location = emp.location,
                                business_unit = emp.business_unit,
                                supercoach_name = emp.supercoach_name,
                                co_supercoach_name = emp.co_supercoach_name,
                                supercoach_mid = emp.supercoach_mid,
                                co_supercoach_mid = emp.co_supercoach_mid,
                                employee_id = emp.employee_id,
                                uemail_id = emp.uemail_id,
                                competency = emp.competency,
                                competencyId = emp.competency_id,
                                supercoach = emp.supercoach,
                                revenue_unit = "",//Recheck
                                sub_industry = emp.sub_industry,
                                industry = emp.industry,
                                offerings = employeesOfferSolution != null ? employeesOfferSolution.offerings.ToArray() : new List<string> { }.ToArray(),
                                solutions = employeesOfferSolution != null ? employeesOfferSolution.solutions.ToArray() : new List<string> { }.ToArray(),
                                skill = userSkillsAsSkillEntities != null && userSkillsAsSkillEntities.Count > 0 ? userSkillsAsSkillEntities.ToArray() : Array.Empty<SkillsEntities>(),
                                interested = usersInterested.Result.Where(m => m.ToLower() == emp.email_id.ToLower()).FirstOrDefault() != null ? true : false,
                                pref_location = matchingUserData != null && matchingUserData.category.ContainsKey("location") ? matchingUserData.category["location"].ToArray() : Array.Empty<string>(),
                                pref_skill = matchingUserData != null && matchingUserData.category.ContainsKey("skill") ? matchingUserData.category["skill"].ToArray() : Array.Empty<string>(),
                                pref_industry = matchingUserData != null && matchingUserData.category.ContainsKey("industry") ? matchingUserData.category["industry"].ToArray() : Array.Empty<string>(),
                                pref_sub_industry = matchingUserData != null && matchingUserData.category.ContainsKey("sub_industry") ? matchingUserData.category["sub_industry"].ToArray() : Array.Empty<string>(),
                                pref_business_unit = matchingUserData != null && matchingUserData.category.ContainsKey("bussiness_unit") ? matchingUserData.category["bussiness_unit"].ToArray() : Array.Empty<string>(),
                                pref_revenue_unit = matchingUserData != null && matchingUserData.category.ContainsKey("revenue_unit") ? matchingUserData.category["revenue_unit"].ToArray() : Array.Empty<string>(),
                                pref_offerings = matchingUserData != null && matchingUserData.category.ContainsKey("offering") ? matchingUserData.category["offering"].ToArray() : Array.Empty<string>(),
                                pref_solutions = matchingUserData != null && matchingUserData.category.ContainsKey("solution") ? matchingUserData.category["solution"].ToArray() : Array.Empty<string>(),
                                leaves = new(),
                                last_available_day = null,
                                available = emp.available
                            };
                            employeesFinal.Add(employ);
                        }
                    }

                    //Will be fetched from config microservice later
                    var pref_weightage_constraint = 0.8;

                    dynamic filters = new List<dynamic> { };
                    var orderScoreBy = "DESC";

                    var params_value_items = new List<string> { };
                    if (request.parameter_value_pairs != null)
                    {

                        foreach (var item in request.parameter_value_pairs)
                        {

                            if (item.StartsWith(ESystemSuggestionParameters.availability))
                            {
                                var filterItem = item.Split(",");
                                if (filterItem.Count() > 1)
                                {
                                    dynamic parameterItem = new ExpandoObject();
                                    parameterItem.available = Convert.ToString(filterItem[1]).ToLower().Trim() == "yes" ? true : false;
                                    filters.Add(parameterItem);
                                }
                            }
                            else if (item.StartsWith(ESystemSuggestionParameters.marketplace))
                            {
                                var filterItem = item.Split(",");
                                if (filterItem.Count() > 1)
                                {
                                    dynamic parameterItem = new ExpandoObject();
                                    parameterItem.interested = Convert.ToString(filterItem[1]).ToLower().Trim() == "yes" ? true : false;
                                    filters.Add(parameterItem);
                                }
                            }
                            else if (item.StartsWith(ESystemSuggestionParameters.sorting))
                            {
                                var filterItem = item.Split(",");
                                if (filterItem.Count() > 1)
                                {
                                    orderScoreBy = Convert.ToString(filterItem[1]);
                                }
                            }
                            else
                            {
                                params_value_items.Add(item);
                            }

                        }
                    }

                    var suggestions = await _requisitionRepository.GetSystemSuggestions(request.limit, request.pagination, pref_weightage_constraint, employeesFinal, requisitionDetails, request.requisitionId, configurationDetails.Result, filters.ToArray(), params_value_items.ToArray(), orderScoreBy);
                    return suggestions;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<int> GetMinimumPercentageForSystemSuggestions(string businessUnit, string offering)
        {
            var keySelector = businessUnit + "|" + offering;

            List<ConfigInfoDTO> configurationDetails = await _configurationHttpService.GetConfigurationByExpertiesNameAndGroupName(keySelector, Constants.ConfigurationTypes.SYSTEM_SUGGESTION_FOR_REQUISITION_PERCENTAGE_MATCH_DB_GROUP);//TODO:- TO BE CHANGED
            var configItem = configurationDetails.Where(m => m.AttributeName.ToLower().Trim().Equals(keySelector.ToLower().Trim())).FirstOrDefault();
            if (configItem != null)
            {
                return int.Parse(configItem.AttributeValue);
            }
            else
            {
                return 0;
            }
        }
    }
}
