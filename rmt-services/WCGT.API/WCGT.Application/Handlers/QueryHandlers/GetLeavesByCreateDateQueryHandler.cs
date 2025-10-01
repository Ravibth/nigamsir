using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCGT.Application.Mappers;
using WCGT.Domain.IRepositories;
using WCGT.Infrastructure.DTOs;

namespace WCGT.Application.Handlers.QueryHandlers
{
    public class GetLeavesByCreateDateQuery:IRequest<List<GTLeaveDTO>>
    {
        public DateTime created_at { get; set; }
    }
    public class GetLeavesByCreateDateQueryHandler : IRequestHandler<GetLeavesByCreateDateQuery, List<GTLeaveDTO>>
    {
        private readonly IWcgtDataRepository _wcgtRepository;
        public GetLeavesByCreateDateQueryHandler(IWcgtDataRepository wcgtDataRepository)
        {
            _wcgtRepository = wcgtDataRepository;
        }

        public async Task<List<GTLeaveDTO>> Handle(GetLeavesByCreateDateQuery request, CancellationToken cancellationToken)
        {
            var result = await _wcgtRepository.GetAllLeavesByCreateDate(request.created_at);
            List<GTLeaveDTO> response = WcgtMapper.Mapper.Map<List<GTLeaveDTO>>(result);
            return response;
        }
    }
}
