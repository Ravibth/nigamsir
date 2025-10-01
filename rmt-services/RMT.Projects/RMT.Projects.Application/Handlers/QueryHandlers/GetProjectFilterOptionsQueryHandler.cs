using MediatR;
using RMT.Projects.Application.IHttpServices;
using RMT.Projects.Application.Responses;
using RMT.Projects.Domain;
using RMT.Projects.Domain.DTOs.Request;
using RMT.Projects.Domain.Entities;
using RMT.Projects.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Projects.Application.Handlers.QueryHandlers
{
    public class GetProjectFilterOptionsQuery : IRequest<ProjectFilterOptionsResponse>
    {
        public string? UserEmail { get; set; }
        public UserDecorator userDecorator { get; set; }
    }
    public class GetProjectFilterOptionsQueryHandler : IRequestHandler<GetProjectFilterOptionsQuery, ProjectFilterOptionsResponse>
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IWcgtHttpService _wcgtHttpService;
        public GetProjectFilterOptionsQueryHandler(IProjectRepository projectRepository, IWcgtHttpService wcgtHttpService)
        {
            _projectRepository = projectRepository;
            _wcgtHttpService = wcgtHttpService;
        }

        public async Task<ProjectFilterOptionsResponse> Handle(GetProjectFilterOptionsQuery request, CancellationToken cancellationToken)
        {
            bool isLeader = request.userDecorator != null && request.userDecorator.roles != null &&
                                request.userDecorator.roles.Contains(Domain.Constant.UserRoles.Leaders);
            GetBuExpertiesDTO buExpertiesSmegList = new GetBuExpertiesDTO();
            List<CompetencyMasterDTO> competencyMasters = new List<CompetencyMasterDTO>();
            if (isLeader)
            {
                buExpertiesSmegList = await _wcgtHttpService.GetBUExpertiesByMID(request.userDecorator.employee_id);
                competencyMasters = await _wcgtHttpService.GetCompetencyMasterByMid(request.userDecorator.employee_id);
            }
            List<Project> projectsList = await _projectRepository.GetProjectsByRequestorEmail(request.userDecorator, request.UserEmail, null, null, null, null, null, null, null, null, null, null, null, null, null, null, null, 1, 1000, buExpertiesSmegList, competencyMasters);
            var buList = projectsList.DistinctBy(x => x.bu).Select(t => t.bu).ToList();
            List<Expertises> expList = projectsList.DistinctBy(x => x.Expertise).Select(t => new Expertises { BU = t.bu, Name = t.Expertise }).ToList();//Recheck
            List<Smes> smeList = projectsList.DistinctBy(x => x.Sme).Select(t => new Smes { Expertise = t.Expertise, Name = t.Sme }).ToList();//Recheck
            List<Smegs> smegList = projectsList.DistinctBy(x => x.Smeg).Select(t => new Smegs { Expertise = t.Expertise, Name = t.Smeg }).ToList();//Recheck
            List<Offerings> offeringsList = projectsList.DistinctBy(x => x.Offerings).Select(t => new Offerings { BU = t.bu, Name = t.Offerings }).ToList();
            List<Solutions> solutionsList = projectsList.DistinctBy(x => x.Solutions).Select(t => new Solutions { Offerings = t.Offerings, Name = t.Solutions }).ToList();
            var industryList = projectsList.DistinctBy(x => x.Industry).Select(t => t.Industry).ToList();
            List<SubIndustry> subIndustryList = projectsList.DistinctBy(x => x.Subindustry).Select(t => new SubIndustry { Industry = t.Industry, Name = t.Subindustry }).ToList();
            var pipelineCodeList = projectsList.DistinctBy(x => x.PipelineCode).Select(t => t.PipelineCode).ToList();
            var pipelineCodeAndNameList = projectsList.DistinctBy(x => x.PipelineCode).Select(t => $"{t.PipelineCode} - {t.PipelineName}").ToList();
            var jobCodeList = projectsList.DistinctBy(x => x.JobCode).Select(t => t.JobCode).ToList();
            var jobNameList = projectsList.DistinctBy(x => x.JobName).Select(t => t.JobName).ToList();
            var jobCodeAndNameList = projectsList.DistinctBy(x => x.JobCode).Select(t => $"{t.PipelineCode} - {t.PipelineName}").ToList();
            var clientNameList = projectsList.DistinctBy(x => x.ClientName).Select(t => $"{t.ClientName}").ToList();
            List<RevenueUnit> revenueUnit = projectsList.DistinctBy(x => x.RevenueUnit).Select(t => new RevenueUnit { Expertise = t.Expertise, Name = t.RevenueUnit }).ToList();//Recheck
            List<string> status = new List<string>() { "Approved", "WON", "Suspended" };
            List<string> projectType = new List<string>() { "Chargeable", "Nonchargeable" };
            List<string> marketPlaceType = new List<string>() { "Yes", "No" };
            if (projectsList.Count == 0)
            {
                status = new List<string>();
                projectType = new List<string>();
                marketPlaceType = new List<string>();
            }
            ProjectFilterOptionsResponse response = new ProjectFilterOptionsResponse()
            {
                DistinctBUSet = buList,
                DistinctExpertises = expList,//Recheck
                DistinctOfferings = offeringsList,
                DistinctSolutions = solutionsList,
                DistinctClientNames = clientNameList,
                DistinctJobs = jobCodeList,
                DistinctJobNames = jobNameList,
                DistinctPipelines = pipelineCodeAndNameList,
                DistinctSmes = smeList,//Recheck
                DistinctSmegs = smegList,//Recheck
                Status = status,
                ProjectType = projectType,
                MarketPlaceType = marketPlaceType,
                DistinctIndustry = industryList,
                DistinctSubIndustry = subIndustryList,
                RevenueUnit = revenueUnit //Recheck
            };
            return response;
        }
    }
}
