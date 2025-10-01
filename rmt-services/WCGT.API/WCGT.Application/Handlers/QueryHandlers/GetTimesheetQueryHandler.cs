using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCGT.Application.DTOs;
using WCGT.Application.Mappers;
using WCGT.Domain.DTOs;
using WCGT.Domain.Entities;
using WCGT.Domain.IRepositories;
using WCGT.Infrastructure.DTOs;

namespace WCGT.Application.Handlers.QueryHandlers
{
    public class GetTimesheetQuery : IRequest<List<TimesheetDesignationResponse>>
    {
        // public DateTime StartDate { get; set; }
        // public DateTime EndDate { get; set; }
        public string? JobCode { get; set; }
        public string? employeecode { get; set; }
    }
    public class GetTimesheetQueryHandler : IRequestHandler<GetTimesheetQuery, List<TimesheetDesignationResponse>>
    {
        private readonly IWcgtDataRepository _repository;
        public GetTimesheetQueryHandler(IWcgtDataRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<TimesheetDesignationResponse>> Handle(GetTimesheetQuery request, CancellationToken cancellationToken)
        {
            try
            {
                TimesheetRequestDTO timesheetReq = new TimesheetRequestDTO()
                {
                    //   StartDate = request.StartDate,
                    //  EndDate = request.EndDate,
                    JobCode = request.JobCode,
                    employeecode = request.employeecode
                };
                var result = await _repository.GetProjectDesignationTimesheet(timesheetReq.JobCode);
                return result;

            }
            catch (Exception ex)
            {
                throw;
            }

            //    List<GTTimesheetDTO> mappedResponse =  WcgtMapper.Mapper.Map<List<GTTimesheetDTO>>(result);
            //   return mappedResponse;
        }
    }
}
