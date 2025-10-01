using MediatR;
using RMT.Allocation.Application.HttpServices.DTOs;
using RMT.Allocation.Application.IHttpServices;
using RMT.Allocation.Application.Mappers;
using RMT.Allocation.Application.Responses;
using RMT.Allocation.Application.Utils;
using RMT.Allocation.Domain.DTO.Response;
using RMT.Allocation.Domain.DTO;
using RMT.Allocation.Domain.Entities;
using RMT.Allocation.Domain.Repositories;
using RMT.Scheduler.service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RMT.Allocation.Application.HttpServices;
using RMT.Allocation.Application.HttpServices.DTOs;
using RMT.Allocation.Application.IHttpServices;
using RMT.Allocation.Application.Mappers;
using RMT.Allocation.Application.Responses;
using RMT.Allocation.Application.Utils;
using RMT.Allocation.Domain.DTO;
using RMT.Allocation.Domain.DTO.Response;
using RMT.Allocation.Domain.Entities;
using RMT.Allocation.Domain.Repositories;
using RMT.Scheduler.service;
using System.Collections.Generic;
using static RMT.Allocation.Domain.ConstantsDomain;
using static System.Formats.Asn1.AsnWriter;
using RMT.Allocation.Domain;

namespace RMT.Allocation.Application.Handlers.QueryHandlers
{
    public class GetAllRequisitionByProjectCodeForProjectDetailsQuery : IRequest<List<GetAllRequisitionByProjectCodeResponse>>
    {
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

    public class GetAllRequisitionByProjectCodeForProjectDetailsQueryHandler : IRequestHandler<GetAllRequisitionByProjectCodeForProjectDetailsQuery, List<GetAllRequisitionByProjectCodeResponse>>
    {
        private readonly IRequisitionRepository _requisitionRepository;

        private readonly IEmployeeMasterHttpApi _employeeMasterHttpApi;
        private readonly IEmployeePreferencesHttpApi _employeePreferencesHttpApi;
        private readonly IGetEmployeeLeavesHttpApi _getEmployeeLeavesHttpApi;
        private readonly IDatesUtils _datesUtils;
        private readonly ITokenService _tokenService;
        private readonly IProjectServiceHttpApi _projectServiceHttpApi;
        private readonly IConfigurationHttpService _configurationHttpService;
        private readonly IWCGTMasterHttpApi _wCGTMasterHttpApi;
        private readonly IMediator _mediator;

        public GetAllRequisitionByProjectCodeForProjectDetailsQueryHandler(
            IRequisitionRepository requisitionRepository
            , IEmployeeMasterHttpApi employeeMasterHttpApi
            , IEmployeePreferencesHttpApi employeePreferencesHttpApi
            , IGetEmployeeLeavesHttpApi getEmployeeLeavesHttpApi
            , IDatesUtils datesUtils
            , ITokenService tokenService
            , IProjectServiceHttpApi projectServiceHttpApi
            , IConfigurationHttpService configurationHttpService
            , IWCGTMasterHttpApi wCGTMasterHttpApi
            , IMediator mediator

            )
        {
            _requisitionRepository = requisitionRepository;
            _employeeMasterHttpApi = employeeMasterHttpApi;
            _employeePreferencesHttpApi = employeePreferencesHttpApi;
            _getEmployeeLeavesHttpApi = getEmployeeLeavesHttpApi;
            _datesUtils = datesUtils;
            _tokenService = tokenService;
            _projectServiceHttpApi = projectServiceHttpApi;
            _configurationHttpService = configurationHttpService;
            _wCGTMasterHttpApi = wCGTMasterHttpApi;
            _mediator = mediator;

        }

        public async Task<List<GetAllRequisitionByProjectCodeResponse>> Handle(GetAllRequisitionByProjectCodeForProjectDetailsQuery request, CancellationToken cancellationToken)
        {
            List<Requisition> result = await _requisitionRepository.GetAllRequisitionByProjectCodeForProjectDetails(request.PipelineCode, request.JobCode);

            List<GetAllRequisitionByProjectCodeResponse> AllRequisitionResponse = AllocationMapper.Mapper.Map<List<GetAllRequisitionByProjectCodeResponse>>(result);

            //Check for nullability where it is used TODO 11-09

            //if ((bool)request.ScoreCalculationForRequisitionIdsAllowed)
            //{
            //    var employeeFinalObj = await GetEmployeeFinalDto(request.currentEmp, DateTime.Now.AddDays(-60), DateTime.Now.AddDays(60));
            //    for (int i = 0; i < AllRequisitionResponse.Count; i++)
            //    {
            //        var val = await GetMatchingScoreForRequisitionId(request.limit, request.pagination, AllRequisitionResponse[i].Id, employeeFinalObj);
            //        if (val.Count != 0)
            //        {
            //            AllRequisitionResponse[i].Score = val[0].score;
            //        }
            //        else
            //        {
            //            AllRequisitionResponse[i].Score = 0.ToString();

            //        }
            //    }
            //}
            if (!string.IsNullOrEmpty(request.UserEmail) && request.IsRequsitionFilterByProjectRoles != null && (bool)request.IsRequsitionFilterByProjectRoles)
            {
                return await Helper.GetRequsitionListByCurrentUserRole(AllRequisitionResponse, request.PipelineCode, request.JobCode, request.UserEmail, _projectServiceHttpApi, _configurationHttpService, request.UserInfo);
            }

            return AllRequisitionResponse;
        }

        //public async Task<List<SystemSuggestionResponseDTO>> GetMatchingScoreForRequisitionId(int limit, int pagination, Guid requisitionId, List<EmployeeDetailsDTO> employeesFinal)
        //{
        //    try
        //    {
        //        //Fetch Requisition Details
        //        Requisition requisitionDetails = await _requisitionRepository.GetRequisitionDetailsByRequisitionId(requisitionId);
        //        if (requisitionDetails == null)
        //        {
        //            throw new InvalidOperationException("Invalid Requisition");
        //        }

        //        //Will be fetched from config microservice later
        //        var pref_weightage_constraint = 0.8;
        //        var suggestions = await _requisitionRepository.GetSystemSuggestions(limit, pagination, pref_weightage_constraint, employeesFinal, requisitionDetails, requisitionId, 0, Array.Empty<object>(), Array.Empty<string>(), "DESC");
        //        return suggestions;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw;
        //    }
        //}

        //private async Task<List<EmployeeDetailsDTO>> GetEmployeeFinalDto(string currentEmp, DateTime startDate, DateTime endDate)
        //{
        //    var getEmployeeMasterDetailsInfo = new GetEmployeeMasterDetailsDTO()
        //    {
        //        Emails = new List<string> { currentEmp },
        //    };
        //    var identityToken = await _tokenService.GetToken();
        //    List<EmployeeMasterDTO> employeesMaster = await _employeeMasterHttpApi.GetEmployeeMasterDataHttpApiQuery(getEmployeeMasterDetailsInfo, identityToken);

        //    //TODO: need to pass as parameter 104 -
        //    var filteredEmployeesMaster = employeesMaster.Where(m => Convert.ToString(m.email).Trim().ToLower() == Convert.ToString(currentEmp).Trim().ToLower()).ToList<EmployeeMasterDTO>();
        //    var emails = filteredEmployeesMaster.Select(emp => emp.email).ToArray();

        //    var getEmployeePreferenceDetailsInfo = new GetEmployeePreferenceDetailsDTO()
        //    {
        //        email = emails
        //    };

        //    Task<List<GetResignedAndAbscondedUsersWithLastAvailableDayResponseDto>> abscondedResignedUsers = _wCGTMasterHttpApi.GetResignedAndAbscondedUsersByEmails(emails.ToList());
        //    GetEmployeeLeaves getEmployeeLeaves = new() { emails = emails.ToList(), start_date = startDate, end_date = endDate };
        //    Task<List<EmployeeLeavesDTO>> employeeLeaves = _getEmployeeLeavesHttpApi.GetEmployeeLeavesByEmails(getEmployeeLeaves);
        //    Task<int> weekends = _datesUtils.GetNumberOfWeekends(startDate, endDate);

        //    //Fetch the preferred details of all the employees
        //    Task<List<EmployeePreferencesByEmailDTO>> pref_data = _employeePreferencesHttpApi.GetEmployeePreferenceDetailsByEmails(getEmployeePreferenceDetailsInfo);

        //    var availabilities = _mediator.Send(new GetAvaiableHoursByEmailIdQery()
        //    {
        //        EmailId = emails,
        //        StartDate = startDate,
        //        EndDate = endDate,
        //        RequireWorkingHours = 0,
        //        isPerDayHourAllocation = false
        //    });

        //    await Task.WhenAll(employeeLeaves, pref_data, weekends, availabilities);

        //    var employeePrefDataFetched = pref_data.Result
        //    .GroupBy(emp => emp.Email)
        //    .Select(usergroup => new
        //    {
        //        email = usergroup.Key,
        //        category = usergroup
        //        .SelectMany(emp => emp.EmployeePreference)
        //        .GroupBy(emp => emp.Category)
        //        .ToDictionary(
        //            categoryGroup => categoryGroup.Key.ToLower(),
        //            categoryGroup => categoryGroup.Select(prefData => prefData.Name).Distinct().ToList())
        //    });
        //    List<EmployeeDetailsDTO> employeesFinal = new() { };
        //    foreach (var emp in filteredEmployeesMaster)
        //    {
        //        var matchingUserData = employeePrefDataFetched.FirstOrDefault(m => m.email == emp.email);
        //        EmployeeLeavesDTO userLeavesFetched = employeeLeaves.Result.FirstOrDefault(m => m.email.ToLower() == emp.email.ToLower());
        //        var isUserAbscondedOrResigned = abscondedResignedUsers.Result.Where(m => m.email_id.ToLower().Trim() == emp.email.ToLower().Trim()).FirstOrDefault();
        //        var leaves = await _getEmployeeLeavesHttpApi.CalculateTotalUserLeavesInHours(userLeavesFetched, weekends.Result, 8);
        //        var isUserAvailable = availabilities.Result.Where(m => m.EmailId.ToLower() == emp.email.ToLower()).FirstOrDefault();

        //        var employ = new EmployeeDetailsDTO
        //        {
        //            empName = emp.empName,
        //            email = emp.email,
        //            designation = emp.designation,
        //            location = emp.location,
        //            business_unit = emp.business_unit,
        //            supercoach_name = emp.supercoach_name,
        //            co_supercoach_name = emp.co_supercoach_name,
        //            supercoach_mid = emp.supercoach_mid,
        //            co_supercoach_mid = emp.co_supercoach_mid,
        //            employee_id = emp.employee_id,
        //            uemail_id = emp.uemail_id,
        //            competency = emp.competency,
        //            competencyId = emp.competencyId,
        //            grade = emp.grade,
        //            supercoach = emp.supercoach,
        //            revenue_unit = emp.revenue_unit,//Recheck
        //            sub_industry = emp.sub_industry,
        //            skill = emp.skill.ToArray(),
        //            interested = false,
        //            pref_location = matchingUserData != null && matchingUserData.category.ContainsKey("location") ? matchingUserData.category["location"].ToArray() : Array.Empty<string>(),
        //            pref_skill = matchingUserData != null && matchingUserData.category.ContainsKey("skill") ? matchingUserData.category["skill"].ToArray() : Array.Empty<string>(),
        //            pref_industry = matchingUserData != null && matchingUserData.category.ContainsKey("industry") ? matchingUserData.category["industry"].ToArray() : Array.Empty<string>(),
        //            pref_sub_industry = matchingUserData != null && matchingUserData.category.ContainsKey("sub_industry") ? matchingUserData.category["sub_industry"].ToArray() : Array.Empty<string>(),
        //            pref_business_unit = matchingUserData != null && matchingUserData.category.ContainsKey("buisness_unit") ? matchingUserData.category["buisness_unit"].ToArray() : Array.Empty<string>(),
        //            pref_revenue_unit = matchingUserData != null && matchingUserData.category.ContainsKey("revenue_unit") ? matchingUserData.category["revenue_unit"].ToArray() : Array.Empty<string>(),
        //            leaves = leaves,
        //            last_available_day = isUserAbscondedOrResigned != null ? isUserAbscondedOrResigned.last_available_day : null,
        //            available = isUserAvailable != null ? isUserAvailable.IsHoursAvialable : false,

        //        };
        //        employeesFinal.Add(employ);
        //    }
        //    return employeesFinal;
        //}
    }
}
