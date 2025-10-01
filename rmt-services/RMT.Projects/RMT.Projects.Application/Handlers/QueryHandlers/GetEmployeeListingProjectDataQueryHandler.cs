using MediatR;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using RMT.Projects.Application.DTOs;
using RMT.Projects.Domain.Entities;
using RMT.Projects.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Projects.Application.Handlers.QueryHandlers
{
    public class GetEmployeeListingProjectDataQuery : IRequest<List<EmployeeListingProjectData>>
    {
        public IEnumerable<KeyValuePair<string, string>> codes;
    }

    public class GetEmployeeListingProjectDataQueryHandler : IRequestHandler<GetEmployeeListingProjectDataQuery, List<EmployeeListingProjectData>>
    {
        private readonly IProjectRepository _ProjectRepo;
        public GetEmployeeListingProjectDataQueryHandler(IProjectRepository projectRepo)
        {
            _ProjectRepo = projectRepo;
        }

        public async Task<List<EmployeeListingProjectData>> Handle(GetEmployeeListingProjectDataQuery request, CancellationToken cancellationToken)
        {
            //IEnumerable<Project> projectData = await _ProjectRepo.GetEmployeeListingProjectData(request.codes.ToList());

            //List<EmployeeListingProjectData> result = new List<EmployeeListingProjectData>();

            //foreach (var code in request.codes)
            //{
            //    result.Add(new EmployeeListingProjectData()
            //    {
            //        Name = code,
            //        Value = projectData.FirstOrDefault(a => Convert.ToString(a.PipelineCode).ToLower().Trim() == Convert.ToString(code).ToLower().Trim())
            //    });
            //}

            //return result;
            return null;

        }
    }

}
