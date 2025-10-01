using MediatR;
using RMT.Projects.Application.DTOs;
using RMT.Projects.Application.HttpServices.DTOs;
using RMT.Projects.Application.IHttpServices;
using RMT.Projects.Application.Responses;
using RMT.Projects.Domain.Repositories;
using RMT.Projects.Infrastructure.Repositories;
using RMT.Projects.Infrastructure.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RMT.Projects.Application.Handlers.CommandHandlers
{
    public class ChangeCodeForProjectCommand : IRequest<ChangeCodeResponseDTO>
    {
        public ChangeCodeDTO changeProjectCodeDTO { get; set; }
        public string UserEamil { get; set; }

    }
    public class ChangeCodeForProjectCommandHandler : IRequestHandler<ChangeCodeForProjectCommand, ChangeCodeResponseDTO>
    {
     
        private readonly IResourceAllocationHttpApi _resourceAllocationHttp;
       
        private readonly IProjectRepository _ProjectRepo;



        public ChangeCodeForProjectCommandHandler (IProjectRepository ProjectRepository, IResourceAllocationHttpApi resourceAllocationHttpApi)
        {
            _ProjectRepo = ProjectRepository;
            _resourceAllocationHttp = resourceAllocationHttpApi;
           

        }

        public async Task<ChangeCodeResponseDTO> Handle(ChangeCodeForProjectCommand request, CancellationToken cancellationToken)
        {
            try
            {
                ChangeCodeResponseDTO changeCodeResponse = new();

                var reqData = request.changeProjectCodeDTO;
                var newProjectDetail = await _ProjectRepo.GetProjectDetailsByCode(reqData.newPipelineCode ,reqData.newJobCode);
                var currentProjectDetail = await _ProjectRepo.GetProjectDetailsByCode(reqData.pipelineCode, reqData.jobCode);
                if (newProjectDetail != null && currentProjectDetail != null)
                {
                    int result = DateTime.Compare(((DateTime)newProjectDetail.EndDate).Date, ((DateTime)(currentProjectDetail.EndDate)).Date);

                    if (newProjectDetail.bu == currentProjectDetail.bu &&
                        newProjectDetail.Offerings == currentProjectDetail.Offerings && //Recheck
                       result == 0)
                    {
                        /********************************* Update Project's jobcode ****************************/
                        bool resource_allocation_response = await _resourceAllocationHttp.UpdateAllocationbyPiplelineCodeHttpApiQuery(reqData.pipelineCode,
                            reqData.jobCode, reqData.newPipelineCode, reqData.newJobCode, newProjectDetail.JobName, request.UserEamil);

                    if (resource_allocation_response)
                    {
                        return new ChangeCodeResponseDTO
                        {
                            pipelineCode = reqData.pipelineCode,
                            jobCode = reqData.jobCode,
                            newPipelineCode = reqData.newPipelineCode,
                            newJobCode = reqData.newJobCode,
                            newJobName = newProjectDetail.JobName,
                            modifiedBy = request.UserEamil,
                            
                        };
                    }
                    }


                    
                    List<string> actions = new List<string>();
                    changeCodeResponse.NotificationActions = actions;
                    var userAllocationMoved = reqData.NewMovedAlloactions;
                    if(userAllocationMoved != null && userAllocationMoved.Count> 0) { 
                    changeCodeResponse.AllocationMoved = userAllocationMoved.Select(e=>e.UserEmail).ToList();
                        actions.Add(Constant.NotificationActions.JOB_CODE_UPDATE_TO_NEW_CODE);
                    }

                }

                return changeCodeResponse;
            }
            catch (Exception)
            {
                throw;
            }
            throw new NotImplementedException();
        }
    }
}
