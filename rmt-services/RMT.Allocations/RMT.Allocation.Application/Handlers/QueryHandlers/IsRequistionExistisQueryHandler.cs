using MediatR;
using RMT.Allocation.Domain.DTO;
using RMT.Allocation.Domain.Entities;
using RMT.Allocation.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace RMT.Allocation.Application.Handlers.QueryHandlers
{
    public class IsRequistionExistsQuery : IRequest<Boolean>
    {
        public string pipelineCode { get; set; }
        public string? jobcode { get; set; }
    }
    public class IsRequistionExistsQueryHandler : IRequestHandler<IsRequistionExistsQuery, Boolean>
    {
        private readonly IRequisitionRepository _requisitionRepository;
        public IsRequistionExistsQueryHandler(IRequisitionRepository requisitionRepository)
        {
            _requisitionRepository = requisitionRepository;
        }
        public async Task<Boolean> Handle(IsRequistionExistsQuery request, CancellationToken cancellationToken)
        {
            Boolean isExists = await _requisitionRepository.IsRequistionExistsInProject(request.pipelineCode, request.jobcode);
            return isExists;
        }
    }
}
