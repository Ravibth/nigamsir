using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using System.Globalization;
using System.Net;
using System.Reflection.Emit;
using WCGT.API.Services;
using WCGT.Application.DTOs;
using WCGT.Application.Handlers.CMDHandler;
using WCGT.Application.Handlers.QueryHandlers;
using WCGT.Application.Mappers;
using WCGT.Application.Responses;
using WCGT.Domain;
using WCGT.Domain.DTO;
using WCGT.Domain.DTOs;
using WCGT.Domain.Entities;
using WCGT.Infrastructure.DTOs;
using WCGT.Infrastructure.DTOs.Base;

namespace WCGT.API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class WcgtDataController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IUserAccessor _userAccessor;

        public WcgtDataController(IMediator mediator, IUserAccessor userAccessor, ILogger<BaseController> logger) : base(logger)
        {
            _mediator = mediator;
            _userAccessor = userAccessor;
        }

        //*****************************************************************************************///

        #region Update endpoints 

        [HttpPost("UpdateClientLegalEntitys")]
        public async Task<List<ClientLegalEntityListResponse>> UpdateClientLegalEntitys([FromBody] GTClientLegalEntityDTO[] clientLegalEntityDTO)
        {
            try
            {
                var request = new ClientLegalEntityQuery()
                {
                    clientLegalEntitys = clientLegalEntityDTO.ToList()
                };

                var response = await _mediator.Send(request);
                return response;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpPost("UpdateClients")]
        public async Task<List<ClientListResponse>> UpdateClients([FromBody] GTClientDTO[] clientDTO)
        {
            try
            {
                var request = new ClientQuery()
                {
                    clients = clientDTO.ToList()
                };

                var response = await _mediator.Send(request);
                return response;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpPost("UpdateLocations")]
        public async Task<List<LocationListResponse>> UpdateLocations([FromBody] GTLocationDTO[] locationDTO)
        {
            try
            {
                var request = new LocationQuery()
                {
                    locations = locationDTO.ToList()
                };

                var response = await _mediator.Send(request);
                return response;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpPost("UpdateDesignations")]
        public async Task<List<DesignationListResponse>> UpdateDesignations([FromBody] GTDesignationDTO[] designationDTO)
        {
            try
            {
                var request = new DesignationQuery()
                {
                    designations = designationDTO.ToList()
                };

                var response = await _mediator.Send(request);
                return response;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        //[HttpPost("UpdateServiceLines")]
        //public async Task<List<ServiceLineListResponse>> UpdateServiceLines([FromBody] GTServiceLineDTO[] serviceLineDTO)
        //{
        //    try
        //    {
        //        var request = new ServiceLineQuery()
        //        {
        //            serviceLines = serviceLineDTO.ToList()
        //        };

        //        var response = await _mediator.Send(request);
        //        return response;
        //    }
        //    catch (Exception ex)
        //    {
        //        this.HandleException(ex); return null;
        //    }
        //}

        [HttpPost("UpdateSectorIndustries")]
        public async Task<List<SectorIndustryListResponse>> UpdateSectorIndustries([FromBody] GTSectorIndustryDTO[] sectorIndustryDTO)
        {
            try
            {
                var request = new SectorIndustryQuery()
                {
                    sectorIndustries = sectorIndustryDTO.ToList()
                };

                var response = await _mediator.Send(request);
                return response;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpPost("UpdateBUTreeMappings")]
        public async Task<List<BUTreeMappingListResponse>> UpdateBUTreeMappings([FromBody] GTBUTreeMappingDTO[] buTreeMappingDTO)
        {
            try
            {
                var request = new BUTreeMappingQuery()
                {
                    buTreeMappings = buTreeMappingDTO.ToList()
                };

                var response = await _mediator.Send(request);
                return response;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpPost("UpdateBUEfficiencyLeader")]
        public async Task<List<GTBUEfficiencyLeaderResponse>> UpdateBUEfficiencyLeader([FromBody] BUEfficiencyLeaderDTO[] buEfficiencyLeaderDTO)
        {
            try
            {
                var request = new BUEfficiencyLeaderCommand()
                {
                    buEfficiencyLeaders = buEfficiencyLeaderDTO.ToList()
                };

                var response = await _mediator.Send(request);
                return response;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpPost("UpdateCompetency")]
        public async Task<List<CompetencyResponse>> UpdateCompetency([FromBody] GTCompetencyDTO[] competencyDTO)
        {
            try
            {
                var request = new CompetencyQuery()
                {
                    competencyDTO = competencyDTO.ToList()
                };

                var response = await _mediator.Send(request);
                return response;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpPost("UpdateHolidays")]
        public async Task<List<HolidayListResponse>> UpdateHolidays([FromBody] GTHolidayDTO[] holidayDTO)
        {
            try
            {
                var request = new HolidayQuery()
                {
                    holidays = holidayDTO.ToList()
                };

                var response = await _mediator.Send(request);
                return response;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpPost("UpdateLeaves")]
        public async Task<List<LeaveListResponse>> UpdateLeaves([FromBody] GTLeaveDTO[] leaveDTO)
        {
            try
            {
                var request = new LeaveQuery()
                {
                    leaves = leaveDTO.ToList()
                };

                var response = await _mediator.Send(request);
                return response;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpPost("UpdateEmployees")]
        public async Task<List<EmployeeListResponse>> UpdateEmployees([FromBody] GTEmployeeDTO[] employeeDTO)
        {
            try
            {
                var request = new EmployeeQuery()
                {
                    employees = employeeDTO.ToList()
                };

                var response = await _mediator.Send(request);
                return response;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpPost("UpdateJobs")]
        public async Task<List<JobListResponse>> UpdateJobs([FromBody] GTJobDTO[] jobDTO)
        {
            try
            {
                var request = new JobQuery()
                {
                    jobs = jobDTO.ToList()
                };

                var response = await _mediator.Send(request);
                return response;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpPost("UpdatePipelines")]
        public async Task<List<PipelineListResponse>> UpdatePipelines([FromBody] GTPipelineDTO[] pipelineDTO)
        {
            try
            {
                var request = new PipelineQuery()
                {
                    pipelines = pipelineDTO.ToList()
                };

                var response = await _mediator.Send(request);
                return response;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpPost("UpdateBudget")]
        public async Task<List<BudgetResponse>> UpdateBudget([FromBody] List<GTBudgetDTO> budgetDto)
        {
            try
            {
                var request = new BudgetCommand()
                {
                    budget = budgetDto
                };

                var response = await _mediator.Send(request);
                return response;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpPost("UpdateRateMaster")]
        public async Task<List<DesignationRateMasterResponse>> UpdateRateMaster([FromBody] List<GTDesignationRateMasterDTO> designationRate)
        {
            try
            {
                var request = new UpdateRateMasterCommand()
                {
                    DesingationRateMaster = designationRate
                };

                var response = await _mediator.Send(request);
                return response;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        #endregion

        //*****************************************************************************************///

        #region Getter endpoints 

        [HttpGet("GetClientLegalEntityList")]
        public async Task<List<GTClientLegalEntityDTO>> GetClientLegalEntityList([FromQuery] string? inputDTO)
        {
            try
            {
                var request = new GetClientLegalEntityListQuery();
                {
                }
                return await _mediator.Send(request);
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpGet("GetClientList")]
        public async Task<List<GTClientDTO>> GetClientList([FromQuery] string? inputDTO)
        {
            try
            {
                var request = new GetClientListQuery();
                {
                }
                return await _mediator.Send(request);
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpPost("GetClientListByJobCodes")]
        public async Task<List<GetJobCodeClientDTO>> GetClientListByJobCodes([FromBody] List<string> jobcodes)
        {
            try
            {
                var request = new GetClientListByJobCodeQuery()
                {
                    jobCodes = jobcodes.ToList()
                };
                return await _mediator.Send(request);

            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpGet("GetLocationList")]
        public async Task<List<GTLocationDTO>> GetLocationList([FromQuery] string? inputDTO)
        {
            try
            {
                var request = new GetLocationListQuery();
                {
                }
                return await _mediator.Send(request);
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpGet("GetDesignationList")]
        public async Task<List<GTDesignationDTO>> GetDesignationList([FromQuery] string? inputDTO)
        {
            try
            {
                var request = new GetDesignationListQuery();
                {
                }
                return await _mediator.Send(request);
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        //[HttpGet("GetServiceLineList")]
        //public async Task<List<GTServiceLineDTO>> GetServiceLineList([FromQuery] string? inputDTO)
        //{
        //    try
        //    {
        //        var request = new GetServiceLineListQuery();
        //        {
        //        }
        //        return await _mediator.Send(request);
        //    }
        //    catch (Exception ex)
        //    {
        //        this.HandleException(ex); return null;
        //    }
        //}

        [HttpGet("GetSectorIndustryList")]
        public async Task<List<GTSectorIndustryDTO>> GetSectorIndustryList([FromQuery] string? inputDTO)
        {
            try
            {
                var request = new GetSectorIndustryListQuery();
                {
                }
                return await _mediator.Send(request);
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpGet("GetBUTreeMappingList")]
        public async Task<List<GTBUTreeMappingDTO>> GetBUTreeMappingList([FromQuery] string? inputDTO)
        {
            try
            {
                var request = new GetBUTreeMappingListQuery();
                {
                }
                return await _mediator.Send(request);
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpGet("GetBUTreeMappingListByMID")]
        public async Task<GTBUExpertiesGroupDTO> GetBUTreeMappingListByMID([FromQuery] string mid)
        {
            try
            {
                var request = new GetBUTreeMappingByMIDQuery()
                {
                    mid = mid
                };
                var result = await _mediator.Send(request);
                return result;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpGet("GetHolidayList")]
        public async Task<List<GTHolidayDTO>> GetHolidayList([FromQuery] string? inputDTO)
        {
            try
            {
                var request = new GetHolidayListQuery();
                return await _mediator.Send(request);
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpPost("GetHolidaysListByParams")]
        public async Task<List<GTHolidayDTO>> GetHolidayByParamList([FromBody] GetHolidaysByParamListQuery holidayParams)
        {
            try
            {
                var response = await _mediator.Send(holidayParams);
                return response;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpGet("GetLeaveList")]
        public async Task<List<GTLeaveDTO>> GetLeaveList([FromQuery] string? inputDTO)
        {
            try
            {
                var request = new GetLeaveListQuery();
                return await _mediator.Send(request);
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpPost("GetEmployeeLeaves")]
        public async Task<List<GTLeaveDTO>> GetLeavesByParamList([FromBody] LeaveParamsDTO leaveParamsDTO)
        {
            try
            {
                var request = new GetLeavesByParamListQuery()
                {
                    emp_emailid = leaveParamsDTO.emp_emailid,
                    created_at = leaveParamsDTO.created_at,
                };
                return await _mediator.Send(request);
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }
        [HttpPost("GetLeavesInfo")]
        public async Task<List<LeaveReport>> GetLeavesInfo([FromBody] LeaveParamsDTO leaveParamsDTO)
        {
            try
            {
                var request = new GetLeavesInfoQuery()
                {
                    emp_mid = leaveParamsDTO.emp_mid,
                    start_date = (DateTime)leaveParamsDTO.start_date,
                    end_date = (DateTime)leaveParamsDTO.end_date
                };
                return await _mediator.Send(request);
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpGet("GetLeavesDetailsByCreateDate")]
        public async Task<List<GTLeaveDTO>> GetLeavesDetailsByCreateDate([FromQuery] DateTime createDate)
        {
            try
            {
                var request = new GetLeavesByCreateDateQuery
                {
                    created_at = createDate
                };
                return await _mediator.Send(request);
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpGet("GetEmployeeList")]
        public async Task<List<GTEmployeeDTO>> GetEmployeeList([FromQuery] string? inputDTO)
        {
            try
            {
                var request = new GetEmployeeListQuery();
                {
                }
                return await _mediator.Send(request);
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpGet("GetCompetencyList")]
        public async Task<List<GTCompetencyDTO>> GetCompetencyList([FromQuery] string? competency_leader_mid)
        {
            try
            {
                var request = new GetCompetencyListQuery()
                {
                    CompetencyLeaderMID = competency_leader_mid
                };
                return await _mediator.Send(request);
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpGet("GetEmployeeByParam")]
        public async Task<GTEmployeeDTO> GetEmployeeByParam([FromQuery] string? emp_mid, string? emp_emailid)
        {
            try
            {
                var request = new GetEmployeeByParamListQuery()
                {
                    emp_mid = emp_mid,
                    emp_emailid = emp_emailid
                };

                return await _mediator.Send(request);
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpGet("GetPipelineList")]
        public async Task<List<GTPipelineDTO>> GetPipelineList([FromQuery] string? inputDTO)
        {
            try
            {
                var request = new GetPipelineListQuery();
                {
                }
                return await _mediator.Send(request);
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpGet("GetJobList")]
        public async Task<List<GTJobDTO>> GetJobList([FromQuery] string? inputDTO)
        {
            try
            {
                var request = new GetJobListQuery();
                {
                }
                return await _mediator.Send(request);
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpGet("GetJobByJobCode")]
        public async Task<GTJobDTO> GetJobByJobCode([FromQuery] string? pipelineCode, string jobCode)
        {
            try
            {
                var request = new GetJobByJobCodeQuery()
                {
                    PipelineCode = pipelineCode,
                    JobCode = jobCode,
                };
                return await _mediator.Send(request);

            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }

        }

        [HttpGet("GetTimesheet")]
        public async Task<List<TimesheetDesignationResponse>> GetTimesheet([FromQuery] TimesheetRequestDTO timesheetRequest)
        {
            try
            {
                var request = new GetTimesheetQuery()
                {
                    //StartDate = timesheetRequest.StartDate,
                    //EndDate = timesheetRequest.EndDate,
                    JobCode = timesheetRequest.JobCode,
                    employeecode = timesheetRequest.employeecode
                };
                return await _mediator.Send(request);
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        //Not in use anymore
        [HttpGet("GetTimesheetGroup")]
        public async Task<List<WcgtTimesheetGroup>> GetTimesheetGroup([FromQuery] TimesheetGroupRequestDTO timesheetRequest)
        {
            try
            {
                var request = new GetTimesheetGroupQuery()
                {
                    JobCode = timesheetRequest.JobCode,
                    TimeOption = timesheetRequest.TimeOption,
                    StartDate = timesheetRequest.StartDate,
                    EndDate = timesheetRequest.EndDate
                };
                return await _mediator.Send(request);
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        //Not in use anymore
        [HttpGet("GetResourceTimesheetGroup")]
        public async Task<List<WcgtResoureTimesheetGroup>> GetResourceTimesheetGroup([FromQuery] TimesheetGroupResourceDTO timesheetRequest)
        {
            try
            {
                var request = new GetResourceTimesheetGroupQuery()
                {
                    JobCode = timesheetRequest.JobCode,
                    StartDate = timesheetRequest.StartDate,
                    EndDate = timesheetRequest.EndDate,
                };
                return await _mediator.Send(request);
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpPost("GetRateByDesignation")]
        public async Task<List<RateDesignationDTO>> GetRateByDesignation([FromBody] List<GetRateDesignationRequestDTO> req)
        {
            try
            {
                var request = new GetRateDesignationQuery()
                {
                    Designation = req
                };
                return await _mediator.Send(request);
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }
        #endregion

        [HttpPost("GetResignedAndAbscondedUsersWithLastAvailableDay")]
        public async Task<List<GetResignedAndAbscondedUsersWithLastAvailableDayResponseDto>> GetResignedAndAbscondedUsersWithLastAvailableDay([FromBody] GetResignedAndAbscondedUsersWithLastAvailableDayQuery requestQuery)
        {
            var request = new GetResignedAndAbscondedUsersWithLastAvailableDayQuery()
            {
                emails = requestQuery.emails
            };
            return await _mediator.Send(request);
        }

        [HttpPost("GetUserLeaveHolidayResponseForSystemSuggestion")]
        public async Task<List<GetUserLeaveHolidayResponseWithUserMaster>> GetUserLeaveHolidayResponseQuery([FromBody] GetUserLeaveHolidayResponseQuery getUserLeaveHolidayResponseQuery)
        {
            return await _mediator.Send(getUserLeaveHolidayResponseQuery);
        }


        [HttpGet("GetProjectBudgetByModifiedDateRange")]
        public async Task<List<string>> GetProjectBudgetByModifiedDateRange(DateTime startDate, DateTime endDate)
        {
            var request = new GetProjectBudgetByModifiedDateRangeQuery()
            {
                startDate = startDate,
                endDate = endDate,
            };

            return await _mediator.Send(request);
        }


        [HttpGet("GetEmployeeBySuperCoachOrCSC")]
        public async Task<List<GTEmployeeDTO>> GetEmployeeBySuperCoachOrCSC([FromQuery] string emp_mid)
        {
            try
            {
                var request = new GetEmployeesBySuperCoachOrCSCQuery()
                {
                    emp_mid = emp_mid,
                };

                return await _mediator.Send(request);
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpGet("GetAllSuperCoach")]
        public async Task<List<SuperCoach>> GetAllSuperCoach()
        {
            try
            {
                var request = new GetSuperCoachCQuery();
                return await _mediator.Send(request);
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpPost("GetEmployeesForPortfolio")]
        public async Task<List<EmployeeLeavesHolidayAndAvailabity>> GetEmployeesForPortfolio([FromBody] GetEmployeesForPortfolioQuery req )
        {
            try
            {
                UserDecorator user = _userAccessor.GetUser();

                var request = req;               
                request.emp_mid = user.employee_id;
                request.userDecorator = user;

                var resp = await _mediator.Send(request);
                return resp;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpPost("GetPortfolioFiltersOptions")]
        public async Task<IActionResult> GetPortfolioFiltersOptions([FromBody] GetEmployeesForPortfolioQuery req)
        {
            try
            {
                var user = _userAccessor.GetUser();
 
                var requestEmployeePortfolio = req;
                requestEmployeePortfolio.emp_mid = user.employee_id;
                requestEmployeePortfolio.userDecorator = user;
                requestEmployeePortfolio.PageNumber = 0;
                requestEmployeePortfolio.PageSize = 0;
                var respEmployeePortFolio = await _mediator.Send(requestEmployeePortfolio);
                var request = new GetPortfolioFiltersOptionsQuery()
                {
                    UserEmail = user != null ? user.email : null,
                    userDecorator = user,
                    employeesAvailabity = respEmployeePortFolio,
                    startDate = req.StartDate,
                    endDate = req.EndDate,
                    
                    
                };

                var resp = await _mediator.Send(request);
                return Ok(resp);
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpPost("GetEmployeesForPortfolioReport")]
        public async Task<List<EmployeesForPortfolioReportDTO>> GetEmployeesForPortfolioReport([FromBody] GetEmployeesForPortfolioReportQuery req)
        {
            try
            {
                UserDecorator user = _userAccessor.GetUser();
                var request = req;
                request.emp_mid = user.employee_id;
                request.userDecorator = user;
                request.periodType = req.periodType;

                var resp = await _mediator.Send(request);
                return resp;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        /// <summary>
        /// HandleException
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        /// <exception cref="BadHttpRequestException"></exception>
        private object HandleException(Exception ex)
        {
            Guid guid = Guid.NewGuid();
            this.LogException(ex, guid);
            throw new BadHttpRequestException($"{ex.Message}-errorid:{guid}", StatusCodes.Status400BadRequest);//, ex);
        }

    }
}
