using MediatR;
using Microsoft.Extensions.Configuration;
using RMT.Allocation.Application.Mappers;
using RMT.Reports.Application.DTO.Request;
using RMT.Reports.Application.DTO.Response;
using RMT.Reports.Application.IHttpServices;
using RMT.Reports.Domain;
using RMT.Reports.Infrastructure.Helpers;
using RMT.Reports.Infrastructure.Infra.Request;
using RMT.Reports.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Reports.Application.Handlers.QueryHandlers
{
    public class GetCapacityUtilizationOverviewQuery : IRequest<List<CapacityUtilizationOverviewResponseDto>>
    {
        public CapacityUtiliationOverviewRequestDto args { get; set; }
        public UserDecorator? userDecorator { get; set; }
    }
    public class GetCapacityUtilizationOverviewQueryHandler : IRequestHandler<GetCapacityUtilizationOverviewQuery, List<CapacityUtilizationOverviewResponseDto>>
    {
        private readonly IReportRepository _reportRepository;
        private readonly IWcgtHttpService _wcgtHttpService;
        private readonly IEmployeeHttpService _employeeHttpService;
        private readonly IConfiguration _config;

        public GetCapacityUtilizationOverviewQueryHandler(IReportRepository reportRepository, IWcgtHttpService wcgtHttpService, IEmployeeHttpService employeeHttpService, IConfiguration config)
        {
            _reportRepository = reportRepository;
            _wcgtHttpService = wcgtHttpService;
            _employeeHttpService = employeeHttpService;
            _config = config;
        }

        public async Task<List<CapacityUtilizationOverviewResponseDto>> Handle(GetCapacityUtilizationOverviewQuery request, CancellationToken cancellationToken)
        {
            bool isLeader = request.userDecorator != null && request.userDecorator.roles != null &&
                request.userDecorator.roles.Contains(ConfigConstants.UserRoles.Leaders);

            bool isCeoCoo = request.userDecorator != null && request.userDecorator.roles != null &&
                request.userDecorator.roles.Contains(ConfigConstants.UserRoles.CeoCoo);

            bool isSystemAdmin = request.userDecorator != null && request.userDecorator.roles != null &&
               request.userDecorator.roles.Contains(ConfigConstants.UserRoles.SystemAdmin);

            GetBuExpertiesDTO buTreeGroupByMid = new GetBuExpertiesDTO();
            List<GTBUTreeMappingDTO> AllBUTreeMappingDTO = new List<GTBUTreeMappingDTO>();
            List<GTCompetencyDTO> buCompetencyList = new List<GTCompetencyDTO>();
            string empId = (request != null && request.userDecorator != null && !string.IsNullOrEmpty(request.userDecorator.employee_id))
                ? request.userDecorator.employee_id : string.Empty;

            ////temp hardcoded
            //empId = "E124";
            //isLeader = true;

            if (isLeader || isCeoCoo || isSystemAdmin)
            {
                AllBUTreeMappingDTO = await _wcgtHttpService.GetBUTreeMappingList();
                if (isCeoCoo || isSystemAdmin)
                {
                    buCompetencyList = await _wcgtHttpService.GetBUCompetencyListByMID(null);
                }
                else
                {
                    buCompetencyList = await _wcgtHttpService.GetBUCompetencyListByMID(empId);
                }
                buTreeGroupByMid = await _wcgtHttpService.GetBUTreeMappingListByMID(empId);
            }

            if (request == null || request.args == null)
            {
                request = new()
                {
                    args = new()
                    {
                        BusinessUnit = new(),
                        Competency = new(),
                        //Offering = new(),
                        //Solution = new(),
                    }
                };
            }
            else
            {
                if (request.args.BusinessUnit == null)
                {
                    request.args.BusinessUnit = new();
                }

                if (request.args.Competency == null)
                {
                    request.args.Competency = new();
                }

                //if (request.args.Offering == null)
                //{
                //    request.args.Offering = new();
                //}

                //if (request.args.Solution == null)
                //{
                //    request.args.Solution = new();
                //}
            }

            if ((buTreeGroupByMid != null
                && ((buTreeGroupByMid.BU != null && buTreeGroupByMid.BU.Any())
                   //|| (buTreeGroupByMid.Offerings != null && buTreeGroupByMid.Offerings.Any())
                   //|| (buTreeGroupByMid.Solutions != null && buTreeGroupByMid.Solutions.Any())
                   || (buCompetencyList != null && buCompetencyList.Any())
                   )
                ) || isCeoCoo || isSystemAdmin)
            {

                buTreeGroupByMid = this.GetMatchingParams(buTreeGroupByMid, AllBUTreeMappingDTO, request.args, (isCeoCoo || isSystemAdmin));

                List<string> listBu = new();
                List<string> listCompetency = new();
                //List<string> listOffering = new();
                //List<string> listSolution = new();

                //Leader view

                //filter has value
                if (request.args?.BusinessUnit != null && request.args?.BusinessUnit.Count > 0)
                {
                    var validBUs = buTreeGroupByMid.BU.Values?.Distinct().Intersect(request.args.BusinessUnit).ToList();
                    listBu = validBUs != null ? validBUs : new();
                }
                else
                {
                    listBu = buTreeGroupByMid.BU != null ? buTreeGroupByMid.BU.Values.ToList() : new();
                }

                //filter has value
                if (request.args?.Competency != null && request.args?.Competency?.Count > 0)
                {
                    listCompetency.Clear();
                    var validComp = buCompetencyList?.Select(c => c.CompetencyName).Distinct().Intersect(request.args.Competency).ToList();
                    listCompetency = validComp != null ? validComp : new();
                }
                else
                {
                    List<string> buIdsByName = AllBUTreeMappingDTO.Where(x => listBu.Contains(x.bu + "")).Select(a => a.bu_id + "").Distinct().ToList();
                    List<string> childComp1 = buCompetencyList.Where(c => buIdsByName.Contains(c.BuId)).Select(a => a.CompetencyName).Distinct().ToList();
                    var validComp = childComp1.Union(buCompetencyList?.Select(c => c.CompetencyName).Distinct());
                    listCompetency.AddRange(validComp);
                }

                ////filter has value
                //if (request.args?.Offering != null && request.args?.Offering?.Count > 0)
                //{
                //    listOffering.Clear();
                //    var validOfferings = buTreeGroupByMid?.Offerings?.Values?.Distinct().Intersect(request.args.Offering).ToList();
                //    listOffering = validOfferings != null ? validOfferings : new();

                //    listSolution.Clear();
                //    var validSln = buTreeGroupByMid?.Solutions?.Values?.Distinct().Intersect(request.args.Solution).ToList();
                //    listSolution = validSln != null ? validSln : new();
                //}
                //else
                //{
                //    listOffering.Clear();
                //    var validOfferings = buTreeGroupByMid.Offerings != null ? buTreeGroupByMid.Offerings.Values.ToList() : new();
                //    listOffering = validOfferings;

                //    listSolution.Clear();
                //    var validSln = buTreeGroupByMid.Solutions != null ? buTreeGroupByMid.Solutions.Values.ToList() : new();
                //    listSolution = validSln;
                //}

                //if (request.args?.Solution != null && request.args?.Solution.Count > 0)
                //{
                //    listSolution.Clear();
                //    var validSln = buTreeGroupByMid?.Solutions?.Values?.Distinct().Intersect(request.args.Solution).ToList();
                //    listSolution = validSln != null ? validSln : new();
                //}
                //else
                //{
                //    listSolution.Clear();
                //    var validSln = buTreeGroupByMid?.Solutions != null ? buTreeGroupByMid.Solutions.Values.ToList() : new();
                //    listSolution = validSln;
                //}

                request.args.BusinessUnit = listBu?.Distinct().ToList();
                request.args.Competency = listCompetency?.Distinct().ToList();
                //request.args.Offering = listOffering?.Distinct().ToList();
                //request.args.Solution = listSolution?.Distinct().ToList();

                CapacityUtiliationOverviewRequestInfra req = ReportMapper.Mapper.Map<CapacityUtiliationOverviewRequestInfra>(request.args);

                //req.CheckEmpMids = Convert.ToBoolean(_config.GetSection("MicroserviceApiSettings").GetSection("CheckEmpByProjectMapping").Value);
                //if (req.CheckEmpMids == true)
                //{
                //    var empResponse = await _employeeHttpService.GetEmpByProjectMapping(null, request.args.Solution);
                //    List<string> allowedEmpMids = empResponse != null ? empResponse.Select(a => a.EmpMID).Distinct().ToList() : new();
                //    req.EmpMids = allowedEmpMids;
                //}

                var res = await _reportRepository.GetCapacityUtilizationOverview(req);
                var result = ReportMapper.Mapper.Map<List<CapacityUtilizationOverviewResponseDto>>(res);

                if (result != null)
                {
                    result = result.Where(x => !string.IsNullOrEmpty(x.business_unit)).ToList();
                    result = result.Where(x => !string.IsNullOrEmpty(x.competency)).ToList();
                }
                else
                {
                    result = new List<CapacityUtilizationOverviewResponseDto>();
                }

                return result;
            }
            else
            {
                Console.WriteLine(@$"Current User is not Leader for any entity,{empId}");
                return new List<CapacityUtilizationOverviewResponseDto>();
            }
        }

        private GetBuExpertiesDTO GetMatchingParams(GetBuExpertiesDTO buTreeGroupByMid, List<GTBUTreeMappingDTO> allBUTreeMappingDTO, CapacityUtiliationOverviewRequestDto args, bool getDataForAllBus = false)
        {
            List<GTBUTreeMappingDTO> allMatchedRecords = new List<GTBUTreeMappingDTO>();
            if (getDataForAllBus)
            {
                allMatchedRecords = allBUTreeMappingDTO.Distinct().ToList();
            }
            else
            {
                allMatchedRecords = allBUTreeMappingDTO.Where(bt =>
                                    buTreeGroupByMid.BU.Values.Contains(bt.bu)
                                    //|| buTreeGroupByMid.Offerings.Values.Contains(bt.offering)
                                    //|| buTreeGroupByMid.Solutions.Values.Contains(bt.solution)
                                    ).Distinct().ToList();
            }

            List<GTBUTreeMappingDTO> result1 = allMatchedRecords;

            var distinctBU = result1.Where(w => !string.IsNullOrEmpty(w.bu_id)).GroupBy(g => g.bu_id).Select(s => new KeyValuePair<string, string>(s.Key, s.First().bu)).Distinct().ToList();
            //var distinctOffering = result1.Where(w => !string.IsNullOrEmpty(w.offering_id)).GroupBy(g => g.offering_id).Select(s => new KeyValuePair<string, string>(s.Key, s.First().offering)).Distinct().ToList();
            //var distinctSln = result1.Where(w => !string.IsNullOrEmpty(w.solution_id)).GroupBy(g => g.solution_id).Select(s => new KeyValuePair<string, string>(s.Key, s.First().solution)).Distinct().ToList();


            if (args.BusinessUnit != null && args.BusinessUnit.Count > 0)
            {
                result1 = result1.Where(a => args.BusinessUnit.Contains(a.bu)).ToList();
            }

            //if (args.Offering != null && args.Offering.Count > 0)
            //{
            //    result1 = result1.Where(a => args.Offering.Contains(a.offering)).ToList();
            //}

            //if (args.Solution != null && args.Solution.Count > 0)
            //{
            //    result1 = result1.Where(a => args.Solution.Contains(a.solution)).ToList();
            //}

            GetBuExpertiesDTO output = new GetBuExpertiesDTO();

            output.BU = new Dictionary<string, string>(distinctBU);
            //output.Offerings = new Dictionary<string, string>(result1.Where(w => !string.IsNullOrEmpty(w.offering_id))
            //    .Select(a => new KeyValuePair<string, string>(a.offering_id, a.offering)).Distinct());
            //output.Solutions = new Dictionary<string, string>(result1.Where(w => !string.IsNullOrEmpty(w.solution_id))
            //    .Select(a => new KeyValuePair<string, string>(a.solution_id, a.solution)).Distinct());

            return output;
        }

    }
}