using MediatR;
using RMT.Allocation.Application.DTOs;
using RMT.Allocation.Application.Responses;
using RMT.Allocation.Domain.DTO;
using RMT.Allocation.Domain.Entities;
using RMT.Allocation.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.Handlers.CommandHandlers
{
    public class UpdateDesignationCostCommand : IRequest<List<UpdateDesignationCost>>
    {
        public List<UpdateDesignationCostDTO> UpdateDesignationCostDTO { get; set; }
    }
    public class UpdateDesignationCostCommandHandler : IRequestHandler<UpdateDesignationCostCommand, List<UpdateDesignationCost>>
    {
        private readonly IResourceAllocationRepository _resourceAllocationRepository;
        public UpdateDesignationCostCommandHandler(IResourceAllocationRepository resourceAllocationRepository)
        {
            _resourceAllocationRepository = resourceAllocationRepository;
        }

        public async Task<List<UpdateDesignationCost>> Handle(UpdateDesignationCostCommand request, CancellationToken cancellationToken)
        {

            List<UpdateDesignationCost> result = await _resourceAllocationRepository.UpdateDesignationCost(request.UpdateDesignationCostDTO);
            return result;
              
        }
    }
}
