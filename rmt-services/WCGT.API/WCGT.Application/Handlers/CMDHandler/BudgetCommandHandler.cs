using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCGT.Application.Mappers;
using WCGT.Application.Responses;
using WCGT.Domain.Entities;
using WCGT.Domain.IRepositories;
using WCGT.Infrastructure.DTOs;

namespace WCGT.Application.Handlers.CMDHandler
{
    public class BudgetCommand : IRequest<List<BudgetResponse>>
    {
        public List<GTBudgetDTO> budget { get; set; }
    }
    public class BudgetCommandHandler : IRequestHandler<BudgetCommand, List<BudgetResponse>>
    {
        private readonly IWcgtDataRepository _repository;

        public BudgetCommandHandler(IWcgtDataRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<BudgetResponse>> Handle(BudgetCommand request, CancellationToken cancellationToken)
        {
            List<BudgetResponse> response = new();

            BudgetResponse _response = null;
            foreach (var current_item in request.budget)
            {
                _response = WcgtMapper.Mapper.Map<BudgetResponse>(current_item);
                try
                {
                    Budget budget = WcgtMapper.Mapper.Map<Budget>(current_item);
                    Budget response1 = await _repository.UpdateBudget(budget);
                }
                catch (Exception ex)
                {
                    _response.isfailed = true;
                    _response.failed_message = ex.Message;
                    var dataLog = Common.CreateWCGTDataLogObject(_response, current_item.GetType(), ex);
                    await _repository.AddWCGTDataLogEntry(dataLog);
                }
                response.Add(_response);
            }

            return response;
        }
    }
}
