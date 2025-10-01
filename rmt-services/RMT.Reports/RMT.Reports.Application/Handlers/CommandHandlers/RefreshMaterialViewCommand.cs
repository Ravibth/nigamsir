using MediatR;
using Microsoft.VisualBasic;
using RMT.Reports.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RMT.Reports.Infrastructure.Helpers.ConfigConstants;

namespace RMT.Reports.Application.Handlers.CommandHandlers
{

    public class RefreshMaterialViewCommand : IRequest<Dictionary<string, bool>>
    {
        public Dictionary<string, bool> materializedViewNames { get; set; }
    }
    public class RefreshMaterialViewCommandHandlers : IRequestHandler<RefreshMaterialViewCommand, Dictionary<string, bool>>
    {
        private readonly IReportRepository _repository;
        public RefreshMaterialViewCommandHandlers(IReportRepository repository)
        {
            _repository = repository;
        }

        public async Task<Dictionary<string, bool>> Handle(RefreshMaterialViewCommand request, CancellationToken cancellationToken)
        {

            var viewsToRefresh = new List<string>();
            if (request.materializedViewNames.ContainsKey(MaterilizedViews.employee_working_days))
            {
                viewsToRefresh.Add(MaterilizedViews.employee_working_days);
            }
            
            if (request.materializedViewNames.ContainsKey(MaterilizedViews.employee_allocation_timesheet))
            {
                viewsToRefresh.Add(MaterilizedViews.employee_allocation_timesheet);
            }

            if (request.materializedViewNames.ContainsKey(MaterilizedViews.project_budget))
            {
                viewsToRefresh.Add(MaterilizedViews.project_budget);
            }

            if (request.materializedViewNames.ContainsKey(MaterilizedViews.employee_view))
            {
                viewsToRefresh.Add(MaterilizedViews.employee_view);
            }

            if (request.materializedViewNames.ContainsKey(MaterilizedViews.employee_skill))
            {
                viewsToRefresh.Add(MaterilizedViews.employee_skill);
            }

            await _repository.RefreshMaterializedViews(viewsToRefresh);

            //if (request.materializedViewNames.ContainsKey(MaterilizedViews.employee_allocation_timesheet))
            //{
            //    var result = await _repository.RefreshEmployeeAllocationView();
            //    request.materializedViewNames[MaterilizedViews.employee_allocation_timesheet] = result;
            //}

            //if (request.materializedViewNames.ContainsKey(MaterilizedViews.employee_working_days))
            //{
            //    var result = await _repository.RefreshEmployeeWorkingDaysView();
            //    request.materializedViewNames[MaterilizedViews.employee_working_days] = result;
            //}

            //if (request.materializedViewNames.ContainsKey(MaterilizedViews.project_budget))
            //{
            //    var result = await _repository.RefreshProjectBudgetView();
            //    request.materializedViewNames[MaterilizedViews.project_budget] = result;
            //}
            return request.materializedViewNames;
        }
    }
}
