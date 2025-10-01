using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCGT.Application.Mappers;
using WCGT.Application.Responses;
using WCGT.Domain.DTO;
using WCGT.Domain.Entities;
using WCGT.Domain.IRepositories;
using WCGT.Infrastructure.DTOs;

namespace WCGT.Application.Handlers.QueryHandlers
{

    public class GetClientListByJobCodeQuery : IRequest<List<GetJobCodeClientDTO>>
    {
        public List<string> jobCodes { get; set; }
    }
    public class GetClientListByJobCodeQueryHandler : IRequestHandler<GetClientListByJobCodeQuery, List<GetJobCodeClientDTO>>
    {
        private readonly IWcgtDataRepository _repository;
        public GetClientListByJobCodeQueryHandler(IWcgtDataRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GetJobCodeClientDTO>> Handle(GetClientListByJobCodeQuery request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAllClientsByJobCode(request.jobCodes);
            List<GetJobCodeClientDTO> response = WcgtMapper.Mapper.Map<List<GetJobCodeClientDTO>>(result);
            return response;
        }
    }
}
