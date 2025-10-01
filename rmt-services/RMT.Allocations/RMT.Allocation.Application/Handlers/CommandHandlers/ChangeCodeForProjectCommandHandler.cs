using MediatR;
using RMT.Allocation.Application.DTOs;
using RMT.Allocation.Application.HttpServices;
using RMT.Allocation.Application.HttpServices.DTOs;
using RMT.Allocation.Application.IHttpServices;
using RMT.Allocation.Application.Responses;
using RMT.Allocation.Domain;
using RMT.Allocation.Domain.DTO;
using RMT.Allocation.Domain.Entities;
using RMT.Allocation.Domain.Repositories;
using RMT.Allocation.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.Handlers.CommandHandlers
{
    public class ChangeCodeForProjectCommand : IRequest<ChangeCodeDTO>
    {
        public ChangeCodeDTO changeProjectCodeDTO { get; set; }
        public UserDecorator user { get; set; }

    }
    public class ChangeCodeForProjectCommandHandler : IRequestHandler<ChangeCodeForProjectCommand, ChangeCodeDTO>
    {
        private readonly IProjectServiceHttpApi _projectServiceHttpApi;
        private readonly IResourceAllocationRepository _allocationRepository;
        private readonly IRequisitionRepository _requisitionRepository;
        public ChangeCodeForProjectCommandHandler(IProjectServiceHttpApi projectServiceHttpApi, IResourceAllocationRepository allocationRepository, IRequisitionRepository requisitionRepository)
        {
            _projectServiceHttpApi = projectServiceHttpApi;
            _allocationRepository = allocationRepository;
            _requisitionRepository = requisitionRepository;
        }

        public async Task<ChangeCodeDTO> Handle(ChangeCodeForProjectCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var reqData = request.changeProjectCodeDTO;
                Boolean result = await  _allocationRepository.UpdateAllocationWithNewJobCode(reqData.pipelineCode, reqData.jobCode, reqData.newPipelineCode,
                    reqData.newJobCode, reqData.newJobName, reqData.modifiedBy);
                return request.changeProjectCodeDTO;

            }
            catch (Exception )
            {
                throw;
            }
        }
    }


}
