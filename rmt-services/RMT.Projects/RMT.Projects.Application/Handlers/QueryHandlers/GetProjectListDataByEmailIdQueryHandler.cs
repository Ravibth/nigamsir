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
    public class GetProjectListDataByEmailIdQuery : IRequest<List<ProjectFullDetailsResponse>>
    {
        public string UserEmail { get; set; }
        public int Limit { get; set; }
        public int Pagination { get; set; }
        public string? Role { get; set; }

    }
    public class GetProjectListDataByEmailIdQueryHandler : IRequestHandler<GetProjectListDataByEmailIdQuery, List<ProjectFullDetailsResponse>>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IResourceAllocationHttpApi _resourceAllocationHttpApi;
        private readonly IWcgtHttpService _wcgtHttpService;
        private readonly IConfiguration _config;

        public GetProjectListDataByEmailIdQueryHandler(IProjectRepository projectRepository, IResourceAllocationHttpApi resourceAllocationHttpApi, IWcgtHttpService wcgtHttpService
            , IConfiguration config
            )
        {
            _projectRepository = projectRepository;
            _resourceAllocationHttpApi = resourceAllocationHttpApi;
            _wcgtHttpService = wcgtHttpService;
            _config = config;
        }

        public async Task<List<ProjectFullDetailsResponse>> Handle(GetProjectListDataByEmailIdQuery request, CancellationToken cancellationToken)
        {      
            List<Project> result = await _projectRepository.GetProjectsDetailsByEmail(request.UserEmail, request.Pagination, request.Limit, request.Role);
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
            List<ProjectFullDetailsResponse> pagedResponse = response;          
            return await Task.FromResult(pagedResponse.ToList());

            //return response;
        }

    }
}
