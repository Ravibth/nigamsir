using MediatR;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RMT.Projects.Application.HttpServices;
using RMT.Projects.Application.HttpServices.DTOs;
using RMT.Projects.Application.IHttpServices;
using RMT.Projects.Application.Mappers;
using RMT.Projects.Application.Responses;
using RMT.Projects.Domain;
using RMT.Projects.Domain.DTOs.Request;
using RMT.Projects.Domain.Entities;
using RMT.Projects.Domain.Repositories;
using RMT.Projects.Infrastructure.Util;
using System.Collections.Generic;

namespace RMT.Projects.Application.Handlers.QueryHandlers
{
    public class GetProjectListDataByUserQuery : IRequest<List<ProjectFullDetailsResponse>>
    {
        public int Limit { get; set; }
        public int Pagination { get; set; }
        public string? OrderBy { get; set; }
        public string? SearchQuery { get; set; }
        public string? RequestorEmail { get; set; }
        public string? ProjectChargeType { get; set; }
        public List<string>? Bu { get; set; }
        public List<string>? Roles { get; set; }
        public List<string>? Expertises { get; set; }//Recheck
        public List<string>? Smes { get; set; }//Recheck

        public List<string>? Offerings { get; set; }
        public List<string>? Solutions { get; set; }

        public List<string>? Industry { get; set; }
        public List<string>? SubIndustry { get; set; }
        public List<string>? Smegs { get; set; }//Recheck
        public List<string>? ClientNames { get; set; }
        public List<string>? PipelineCodes { get; set; }
        public List<string>? JobCodes { get; set; }
        public List<string>? JobNames { get; set; }
        public List<string>? ProjectStatus { get; set; }
        public List<string>? RevenueUnit { get; set; }//Recheck
        public string? ProjectType { get; set; }
        public bool? MarketPlace { get; set; }
        public bool IsAllocatedHoursRequired { get; set; }
        public UserDecorator userDecorator { get; set; }

    }
    public class GetProjectListDataByUserQueryHandler : IRequestHandler<GetProjectListDataByUserQuery, List<ProjectFullDetailsResponse>>
    {
        private readonly IProjectRepository _projectRepository;

        private readonly IResourceAllocationHttpApi _resourceAllocationHttpApi;
        private readonly IWcgtHttpService _wcgtHttpService;
        private readonly IConfiguration _config;

        public GetProjectListDataByUserQueryHandler(IProjectRepository projectRepository, IResourceAllocationHttpApi resourceAllocationHttpApi, IWcgtHttpService wcgtHttpService
            , IConfiguration config
            )
        {
            _projectRepository = projectRepository;
            _resourceAllocationHttpApi = resourceAllocationHttpApi;
            _wcgtHttpService = wcgtHttpService;
            _config = config;
        }

        public async Task<List<ProjectFullDetailsResponse>> Handle(GetProjectListDataByUserQuery request, CancellationToken cancellationToken)
        {
            Int32 limitConfig = Convert.ToInt32(_config.GetSection("MicroserviceApiSettings").GetSection("PaginationLimit").Value);
            if (request.Limit > limitConfig)
            {
                throw new Exception($"Invalid limit, Request limit is max allowed value is {limitConfig}");
            }
            bool isLeader = request.userDecorator != null && request.userDecorator.roles != null &&
                                request.userDecorator.roles.Contains(Domain.Constant.UserRoles.Leaders);
            GetBuExpertiesDTO buExpertiesSmegList = new();
            List<CompetencyMasterDTO> competencyMasters = new List<CompetencyMasterDTO>();
            if (isLeader)
            {
                buExpertiesSmegList = await _wcgtHttpService.GetBUExpertiesByMID(request.userDecorator.employee_id);
                competencyMasters = await _wcgtHttpService.GetCompetencyMasterByMid(request.userDecorator.employee_id);
            }

            List<Project> result = await _projectRepository.GetProjectsByRequestorEmail(request.userDecorator, request.RequestorEmail, request.Bu, request.Offerings, request.Solutions, request.Roles,
                  request.ProjectChargeType, request.Industry, request.SubIndustry, request.ClientNames,
                  request.PipelineCodes, request.JobCodes, request.JobNames, request.ProjectStatus, request.ProjectType,
                 request.MarketPlace, request.SearchQuery, request.Pagination, request.Limit, buExpertiesSmegList, competencyMasters);

            List<ProjectAllocatedHoursRatioDto> ratioColl = new List<ProjectAllocatedHoursRatioDto>();
            List<ProjectFullDetailsResponse> response = new List<ProjectFullDetailsResponse>();

            if (result != null && result.Count() > 0)
            {
                try
                {
                    response = ProjectMapper.Mapper.Map<List<ProjectFullDetailsResponse>>(result);
                }
                catch (Exception ex)
                {
                    //throw;
                }
            }

            //// code for pagination 
            //int len = (response != null && response.Count > 0) ? response.Count() : 0;
            //int startIndex, finalCountToReturn;
            //startIndex = (request.Pagination - 1) * request.Limit;
            //finalCountToReturn = Math.Min(len - startIndex, request.Limit);
            //if (finalCountToReturn <= 0)
            //{
            //    return new List<ProjectFullDetailsResponse> { };
            //}
            //List<ProjectFullDetailsResponse> pagedResponse = response.GetRange(startIndex, finalCountToReturn);

            List<ProjectFullDetailsResponse> pagedResponse = response;
            if (request.IsAllocatedHoursRequired == true)
            {
                //GetAllocated Hours ratio for the paged projects
                //List<KeyValuePair<string, string>> pipelineCodes = pagedResponse.Select(a => new KeyValuePair<string, string>(a.PipelineCode, a.JobCode)).Where(x => !string.IsNullOrEmpty(x.Value)).ToList();
                List<KeyValuePair<string, string>> pipelineCodes = pagedResponse.Select(a => new KeyValuePair<string, string>(a.PipelineCode, a.JobCode)).ToList();
                ratioColl = await _resourceAllocationHttpApi.GetAllocatedHoursRatioByPipelineCode(pipelineCodes);

                foreach (var _item in pagedResponse)
                {
                    _item.ProjectAllocatedHoursRatio = ratioColl
                        .Where(a =>
                            a.pipelineCode.TrimLower() == _item.PipelineCode.TrimLower()
                            && ((a.jobCode == null && _item.JobCode == null) || a.jobCode.TrimLower() == _item.JobCode.TrimLower())
                        )
                        .FirstOrDefault();
                }
            }
            return await Task.FromResult(pagedResponse.ToList());

            //return response;
        }

    }
}
