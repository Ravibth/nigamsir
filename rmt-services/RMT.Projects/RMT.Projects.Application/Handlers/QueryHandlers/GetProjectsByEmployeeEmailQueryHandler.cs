//Not in use
//using MediatR;
//using Newtonsoft.Json;
//using RMT.Projects.Application.HttpServices;
//using RMT.Projects.Application.HttpServices.DTOs;
//using RMT.Projects.Application.IHttpServices;
//using RMT.Projects.Application.Mappers;
//using RMT.Projects.Application.Responses;
//using RMT.Projects.Domain;
//using RMT.Projects.Domain.DTOs.Request;
//using RMT.Projects.Domain.Entities;
//using RMT.Projects.Domain.Repositories;
//using System.Collections.Generic;

//namespace RMT.Projects.Application.Handlers.QueryHandlers
//{
//    public class GetProjectsByEmployeeEmailQuery : IRequest<List<ProjectFullDetailsResponse>>
//    {
//        public string RequestorEmail { get; set; }
//        public string? ProjectChargeType { get; set; }
//        public List<string>? Bu { get; set; }
//        public List<string>? Expertises { get; set; }//Recheck
//        public List<string>? Smes { get; set; }//Recheck

//        public List<string>? Offerings { get; set; }
//        public List<string>? Solutions { get; set; }
//        public List<string>? Roles { get; set; }

//        public List<string>? Industry { get; set; }
//        public List<string>? SubIndustry { get; set; }
//        public List<string>? RevenueUnit { get; set; }//Recheck
//        public List<string>? Smegs { get; set; }//Recheck
//        public List<string>? ClientNames { get; set; }
//        public List<string>? PipelineCodes { get; set; }
//        public List<string>? JobCodes { get; set; }
//        public List<string>? ProjectStatus { get; set; }
//        public string? ProjectType { get; set; }
//        public bool? MarketPlace { get; set; }
//        public string? SearchQuery { get; set; }
//        public int pagination { get; set; } = 1;
//        public int limit { get; set; } = 10000;

//        public bool IsAllocatedHoursRequired { get; set; }
//        public UserDecorator userDecorator { get; set; }

//    }
//    public class GetProjectsByEmployeeEmailQueryHandler : IRequestHandler<GetProjectsByEmployeeEmailQuery, List<ProjectFullDetailsResponse>>
//    {
//        private readonly IProjectRepository _projectRepository;

//        private readonly IResourceAllocationHttpApi _resourceAllocationHttpApi;
//        private readonly IWcgtHttpService _wcgtHttpService;


//        public GetProjectsByEmployeeEmailQueryHandler(IProjectRepository projectRepository, IResourceAllocationHttpApi resourceAllocationHttpApi, IWcgtHttpService wcgtHttpService)
//        {
//            _projectRepository = projectRepository;
//            _resourceAllocationHttpApi = resourceAllocationHttpApi;
//            _wcgtHttpService = wcgtHttpService;

//        }

//        public async Task<List<ProjectFullDetailsResponse>> Handle(GetProjectsByEmployeeEmailQuery request, CancellationToken cancellationToken)
//        {
//            bool isLeader = request.userDecorator != null && request.userDecorator.roles != null &&
//                          request.userDecorator.roles.Contains(Domain.Constant.UserRoles.Leaders);
//            GetBuExpertiesDTO buMappingList = new GetBuExpertiesDTO();
//            List<CompetencyMasterDTO> competencyMasters = new();
//            if (isLeader)
//            {
//                buMappingList = await _wcgtHttpService.GetBUExpertiesByMID(request.userDecorator.employee_id);
//                competencyMasters = await _wcgtHttpService.GetCompetencyMasterByMid(request.userDecorator.employee_id);
//            }

//            var result = await _projectRepository.GetProjectsByRequestorEmail(request.userDecorator, request.RequestorEmail, request.Bu, request.Offerings, request.Solutions,request.Roles,
//                  request.ProjectChargeType, request.Industry, request.SubIndustry, request.ClientNames,
//                  request.PipelineCodes, request.JobCodes, request.ProjectStatus, request.ProjectType,
//                 request.MarketPlace, request.SearchQuery, request.pagination, request.limit, buMappingList , competencyMasters);
//            List<ProjectAllocatedHoursRatioDto> ratioColl = new List<ProjectAllocatedHoursRatioDto>();

//            if (result != null && result.Count() > 0)
//            {
//                try
//                {
//                    List<KeyValuePair<string, string>> pipelineCodes = result.Select(a => new KeyValuePair<string, string>(a.PipelineCode, a.JobCode)).ToList();
//                    ratioColl = await _resourceAllocationHttpApi.GetAllocatedHoursRatioByPipelineCode(pipelineCodes);
//                }
//                catch (Exception ex)
//                {
//                    //throw;
//                }
//            }

//            var response = ProjectMapper.Mapper.Map<List<ProjectFullDetailsResponse>>(result);

//            foreach (var responseItem in response)
//            {
//                responseItem.ProjectAllocatedHoursRatio = ratioColl.Where(a => a.pipelineCode.ToLower() == responseItem.PipelineCode.ToLower()
//                && a.jobCode.ToLower() == responseItem.JobCode.ToLower()).FirstOrDefault();
//            }

//            return response;
//            //return await _projectRepository.GetProjectsByEmail(request.EmployeeEmail, request.ProjectChargeType, request.Expertises, request.Smes, request.ClientNames, request.PipelineCodes, request.PipelineNames);
//            //return await _projectRepository.GetProjectsByRequestorEmail(request.RequestorEmail, request.ProjectChargeType, request.Expertises, request.Smes, request.ClientNames, request.PipelineCodes, request.PipelineNames);
//        }

//    }
//}
