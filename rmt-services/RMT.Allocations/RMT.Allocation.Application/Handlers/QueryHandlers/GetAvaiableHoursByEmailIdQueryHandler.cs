using MediatR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RMT.Allocation.Application.DTOs;
using RMT.Allocation.Application.HttpServices.DTOs;
using RMT.Allocation.Application.HttpServices.HolidayService;
using RMT.Allocation.Application.IHttpServices;
using RMT.Allocation.Application.Utils;
using RMT.Allocation.Domain.DTO;
using RMT.Allocation.Domain.DTO.Response;
using RMT.Allocation.Domain.Entities;
using RMT.Allocation.Domain.Repositories;
using RMT.Allocation.Infrastructure;
using RMT.Allocation.Infrastructure.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Runtime.Serialization.Formatters;
using System.Text;
using System.Threading.Tasks;
using Constants = RMT.Allocation.Infrastructure.Constants;

namespace RMT.Allocation.Application.Handlers.QueryHandlers
{
    public class GetAvaiableHoursByEmailIdQery : IRequest<List<ResourceAvailable>>
    {
        public Guid? RequisitionId { get; set; }
        public string[] EmailId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Int64 TotalAvaibleHours { get; set; }
        public Int64 RequireWorkingHours { get; set; }
        public Boolean isPerDayHourAllocation { get; set; }
        public string? PipelineCode { get; set; }
        public string? JobCode { get; set; }
    }

    public class GetAvaiableHoursByEmailIdQueryHandler : IRequestHandler<GetAvaiableHoursByEmailIdQery, List<ResourceAvailable>>
    {
        private readonly IResourceAllocationRepository _resourceAllocationRepository;
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;
        private readonly IWCGTMasterHttpApi _wCGTMasterHttpApi;
        private readonly IHolidayHttpService _holidayHttpService;
        public GetAvaiableHoursByEmailIdQueryHandler(IResourceAllocationRepository resourceAllocationRepository, IConfiguration configuration, HttpClient httpClient,
            IWCGTMasterHttpApi wCGTMasterHttpApi, IHolidayHttpService holidayHttpService)
        {
            _resourceAllocationRepository = resourceAllocationRepository;
            _configuration = configuration;
            _httpClient = httpClient;
            _wCGTMasterHttpApi = wCGTMasterHttpApi;
            _holidayHttpService = holidayHttpService;
        }

        public async Task<List<ResourceAvailable>> Handle(GetAvaiableHoursByEmailIdQery request, CancellationToken cancellationToken)
        {
            Dictionary<string, string> EmployeeEmailLocation = new();
            List<GTHolidayDTO> holidayList = new();
            Task<HolidayResponseDTO> holidayResponse = _holidayHttpService.GetLocationSpecificHolidays(request.EmailId.ToList(), null, null);

            Task<GetHolidayLeaveResignedAbsconded> userLeavesResponse = Helper.GetHolidayLeaveResignedAbscondedByEmailIds(_configuration, request.EmailId.ToList(), _httpClient, _wCGTMasterHttpApi , request.StartDate , request.EndDate);
            await Task.WhenAll(holidayResponse, userLeavesResponse);
            if (holidayResponse != null)
            {
                if (holidayResponse.Result.EmailLocationCollection != null)
                {
                    EmployeeEmailLocation = holidayResponse.Result.EmailLocationCollection;

                }
                if (holidayResponse.Result.HolidayList != null)
                {
                    holidayList = holidayResponse.Result.HolidayList;
                }

            }
            List<GTLeaveBaseDTO> leaveList = userLeavesResponse.Result.LeavesResponseTask;
            List<GetResignedAndAbscondedUsersWithLastAvailableDayResponseDto> resignedAbscondedResponse = userLeavesResponse.Result.ResignedAbscondedResponse;
            var response = new List<ResourceAvailable>();
            List<Task<ResourceAvailable>> tasksList = new();
            foreach (var item in request.EmailId)
            {
                var lastAvailableDay = resignedAbscondedResponse.Where(m => m.email_id.ToLower() == item.ToLower()).FirstOrDefault();
                ResourceAvailable resourceAvailable = new()
                {
                    EmailId = item.ToLower(),
                    StartDate = request.StartDate,
                    EndDate = request.EndDate,
                    RequireWorkingHours = request.RequireWorkingHours,
                    isPerDayHourAllocation = request.isPerDayHourAllocation,
                    //RequisitionId = (Guid)request.RequisitionId,
                    LastAvailableDay = lastAvailableDay != null ? lastAvailableDay.last_available_day : null,
                };

                if (request.RequisitionId != null)
                {
                    resourceAvailable.RequisitionId = (Guid)request.RequisitionId;
                }

                //If per day allocation then check here if day exceeds last available day directly
                if (lastAvailableDay != null && lastAvailableDay.last_available_day != null)
                {
                    if (request.isPerDayHourAllocation && (resourceAvailable.LastAvailableDay.Value <= DateOnly.FromDateTime(request.StartDate) || DateOnly.FromDateTime(request.EndDate) >= resourceAvailable.LastAvailableDay.Value)
                        || resourceAvailable.LastAvailableDay.Value <= DateOnly.FromDateTime(request.StartDate)
                        )
                    {
                        resourceAvailable.ErrorMsg = String.Format(Constants.error_IsAvailableHour_Crossed_Last_Available_Day, lastAvailableDay.last_available_day.ToShortDateString());
                        resourceAvailable.IsHoursAvialable = false;
                        response.Add(resourceAvailable);
                        continue;
                    }
                }
                var end_date_temp = lastAvailableDay != null && lastAvailableDay.last_available_day != null && DateOnly.FromDateTime(request.EndDate) >= lastAvailableDay.last_available_day ? lastAvailableDay.last_available_day.ToDateTime(TimeOnly.MinValue) : request.EndDate;
                resourceAvailable.EndDate = end_date_temp;
                string? resourceLocation = string.Empty;
                if (EmployeeEmailLocation != null)
                {
                    EmployeeEmailLocation.TryGetValue(item, out resourceLocation);

                }
                List<GTHolidayDTO> resourceHolidayList = new List<GTHolidayDTO>();
                if (request.isPerDayHourAllocation) {
                    leaveList = new List<GTLeaveBaseDTO>();
                }
                if (!request.isPerDayHourAllocation && resourceLocation != null && holidayList.Count > 0)
                {
                    resourceHolidayList = holidayList.Where(holiday => holiday.location_name.Trim().ToLower() == resourceLocation.Trim().ToLower()).ToList();
                }
                ResourceAvailable a = await _resourceAllocationRepository.GetResourceAvailableHours(resourceAvailable, resourceHolidayList, leaveList, request.PipelineCode, request.JobCode);
                response.Add(a);
            }
            return response;
        }
    }
}
