using MediatR;
using RMT.Allocation.Application.DTOs;
using RMT.Allocation.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Allocation.Application.Handlers.QueryHandlers
{
    public class GetEmployeeTimelineForEmployeePortfolioReportQuery : IRequest<List<EmployeePerDayTimelineForEmployeePortfolioDto>>
    {
        public GetEmployeeTimelineForEmployeePortfolioReportRequestDto reqInput { get; set; }
    }
    public class GetEmployeeTimelineForEmployeePortfolioReportQueryHandler : IRequestHandler<GetEmployeeTimelineForEmployeePortfolioReportQuery, List<EmployeePerDayTimelineForEmployeePortfolioDto>>
    {
        //1. Get all published recource allocation day info
        //2. Perday loop from startDate to endDate
        //3. get distinct employees
        //4. employees leaves and holidays combined
        //5. filter things by each employee or group by employee or dictionary by employee
        //6. based on timeline type create the timeline
        private readonly IResourceAllocationRepository _resourceAllocationRepository;
        public GetEmployeeTimelineForEmployeePortfolioReportQueryHandler(IResourceAllocationRepository resourceAllocationRepository)
        {
            _resourceAllocationRepository = resourceAllocationRepository;
        }

        public async Task<List<EmployeePerDayTimelineForEmployeePortfolioDto>> Handle(GetEmployeeTimelineForEmployeePortfolioReportQuery request, CancellationToken cancellationToken)
        {
            var publishedAllocationDays = await _resourceAllocationRepository.GetPublishedResourceAllocationDays(request.reqInput.StartDate, request.reqInput.EndDate);
            var publishedEmployeeAllocationDayGrp = publishedAllocationDays
                                                            .GroupBy(x => new { x.EmailId, x.AllocationDate })
                                                            .Select(g => new
                                                            {
                                                                EmailId = g.Key.EmailId,
                                                                AllocationDate = g.Key.AllocationDate,
                                                                TotalEffort = g.Sum(x => x.Efforts)
                                                            });
            var publishedEmployeeAllocationDayDict = publishedEmployeeAllocationDayGrp.ToDictionary(g => g.EmailId, g => new
            {
                AllocationDate = g.AllocationDate,
                TotalEffort = g.TotalEffort
            });
            var publishedEmployeeAllocationDayUniqDict = publishedEmployeeAllocationDayGrp.ToDictionary(g => string.Concat(g.EmailId, g.AllocationDate.ToString()), g => new
            {
                EmployeeEmail = g.EmailId,
                AllocationDate = g.AllocationDate,
                TotalEffort = g.TotalEffort
            });
            List<EmployeePerDayTimelineForEmployeePortfolioDto> employeePerdayTimelineForEmployeePortfolio = new();
            foreach (var kvp in publishedEmployeeAllocationDayDict)
            {

                var currentDate = request.reqInput.StartDate;
                while (currentDate <= request.reqInput.EndDate)
                {
                    if (currentDate.DayOfWeek == DayOfWeek.Saturday || currentDate.DayOfWeek == DayOfWeek.Sunday)
                    {
                        currentDate.AddDays(1);
                        continue;
                    }
                    employeePerdayTimelineForEmployeePortfolio.Add(new EmployeePerDayTimelineForEmployeePortfolioDto
                    {
                        AvailableHours = 8,
                        LeaveHours = 0,
                        AllocationHours = 0,
                        Date = currentDate,
                        EmployeeEmail = kvp.Key
                    });
                    currentDate.AddDays(1);
                }
            }
            foreach (var item in employeePerdayTimelineForEmployeePortfolio)
            {
                string dictKey = string.Concat(item.EmployeeEmail, item.Date);
                if (publishedEmployeeAllocationDayUniqDict.ContainsKey(dictKey))
                {
                    item.AllocationHours = (int)publishedEmployeeAllocationDayDict[dictKey].TotalEffort;
                }
            }
            return employeePerdayTimelineForEmployeePortfolio;
        }
    }
}
