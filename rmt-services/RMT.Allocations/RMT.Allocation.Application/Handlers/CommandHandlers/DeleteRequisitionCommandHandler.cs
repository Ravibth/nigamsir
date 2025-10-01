using MediatR;
using RMT.Allocation.Application.IHttpServices;
using RMT.Allocation.Application.Responses;
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
    public class DeleteRequisitionCommand : IRequest<DeleteRequisitionResponse>
    {
        public Guid Id { get; set; }
    }
    public class DeleteRequisitionCommandHandler : IRequestHandler<DeleteRequisitionCommand, DeleteRequisitionResponse>
    {
        private readonly IRequisitionRepository _requisitionRepository;
        private readonly IResourceAllocationRepository _resourceAllocationRepository;
        private readonly IProjectServiceHttpApi _projectServiceHttpApi;

        public DeleteRequisitionCommandHandler(IRequisitionRepository requisitionRepository, IResourceAllocationRepository resourceAllocationRepository, IProjectServiceHttpApi projectServiceHttpApi)
        {
            _requisitionRepository = requisitionRepository;
            _resourceAllocationRepository = resourceAllocationRepository;
            _projectServiceHttpApi = projectServiceHttpApi;
        }
        public async Task<DeleteRequisitionResponse> Handle(DeleteRequisitionCommand request, CancellationToken cancellationToken)
        {
            var result = await _requisitionRepository.DeleteRequisitionById(request.Id);
            var deleteResponse = new DeleteRequisitionResponse();
            if (result.IsActive == true)
            {
                deleteResponse.is_deleted = false;
            }
            else
            {
                deleteResponse.is_deleted = true;
            }
            deleteResponse.pipelineCode = result.PipelineCode;
            deleteResponse.jobCode = result.JobCode;
            deleteResponse.Type = result?.RequisitionType?.Type;
            
            var requisitionData = await _requisitionRepository.GetRequisitionDetailsByRequisitionId(request.Id);
            if (requisitionData != null)
            {
                int allocationcount = await _resourceAllocationRepository.GetAllocationCount(requisitionData.PipelineCode, requisitionData.JobCode);
                int requicount = await _resourceAllocationRepository.GetRequisitionCount(requisitionData.PipelineCode, requisitionData.JobCode);
                await _projectServiceHttpApi.AddUpdateProjectRequisitionAllocation(requisitionData.PipelineCode, requisitionData.JobCode, requicount, allocationcount);
            }
            return deleteResponse;
        }
    }
}
