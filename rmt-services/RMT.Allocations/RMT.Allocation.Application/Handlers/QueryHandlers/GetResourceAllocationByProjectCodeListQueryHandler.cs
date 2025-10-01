using MediatR;
using RMT.Allocation.Application.DTOs;
using RMT.Allocation.Application.HttpServices;
using RMT.Allocation.Application.HttpServices.DTOs;
using RMT.Allocation.Application.IHttpServices;
using RMT.Allocation.Domain;
using RMT.Allocation.Domain.Repositories;
using RMT.Allocation.Infrastructure.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.Handlers.QueryHandlers
{
    public class GetResourceAllocationByProjectCodeListQuery : IRequest<List<ResourceAllocationResponse>>
    {
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public UserDecorator User { get; set; }
    }

    public class GetResourceAllocationByProjectCodeListHandler : IRequestHandler<GetResourceAllocationByProjectCodeListQuery, List<ResourceAllocationResponse>>
    {

        private readonly IResourceAllocationRepository _resourceAllocationRepository;
        private readonly IProjectServiceHttpApi _projectServiceHttpApi;

        public GetResourceAllocationByProjectCodeListHandler(IResourceAllocationRepository resourceAllocationRepository, IProjectServiceHttpApi projectServiceHttpApi)
        {
            _resourceAllocationRepository = resourceAllocationRepository;
            _projectServiceHttpApi = projectServiceHttpApi;
        }


        public async Task<List<ResourceAllocationResponse>> Handle(GetResourceAllocationByProjectCodeListQuery request, CancellationToken cancellationToken)
        {
            var result = new List<ResourceAllocationResponse>();
            
            var dto = new ProjectListRequestDTO
            {
                userEmail = request.User.email
            };

            List<ProjectFullDetailsResponse> projectList = await _projectServiceHttpApi.GetProjectListDataByUser(dto); //get project list
            
            foreach (var item in projectList) //Now get allocation realted with each project.
            {
                List<ResourceAllocationResponse> data = await _resourceAllocationRepository.GetProjectsByEmployeeEmailAndPipelineCode(request.User.email,item.PipelineCode,item.JobCode, null);
                if (data != null)
                {
                    result.AddRange(data);
                }
            }

            return result;
        }
    }

}
