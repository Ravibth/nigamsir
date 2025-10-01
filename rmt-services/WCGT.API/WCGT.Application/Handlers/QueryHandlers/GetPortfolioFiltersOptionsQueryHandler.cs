using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCGT.Application.Mappers;
using WCGT.Application.Services.IHttpServices;
using WCGT.Domain;
using WCGT.Domain.DTO;
using WCGT.Domain.Entities;
using WCGT.Domain.IRepositories;
using WCGT.Infrastructure.DTOs;
using static WCGT.Infrastructure.Constants;

namespace WCGT.Application.Handlers.QueryHandlers
{
    public class GetPortfolioFiltersOptionsQuery : IRequest<PortfolioFiltersOptions>
    {
        public string? UserEmail { get; set; }
        public List<EmployeeLeavesHolidayAndAvailabity> employeesAvailabity { get; set; }

        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public UserDecorator userDecorator { get; set; }
    }

    public class GetPortfolioFiltersOptionsQueryHandler : IRequestHandler<GetPortfolioFiltersOptionsQuery, PortfolioFiltersOptions>
    {
        private readonly IWcgtDataRepository _repository;
        private readonly IAllocationHttpService _allocationHttpService;
        public GetPortfolioFiltersOptionsQueryHandler(IWcgtDataRepository repository, IAllocationHttpService allocationHttpService)
        {
            _repository = repository;
            _allocationHttpService = allocationHttpService;
        }


        public async Task<PortfolioFiltersOptions> Handle(GetPortfolioFiltersOptionsQuery request, CancellationToken cancellationToken)
        {

            List<SuperCoach>? supercoaches = await _repository.GetAllSuperCoach();
            List<SuperCoach>? cosupercoaches = await _repository.GetAllCoSuperCoach();
            List<Employee>? employees = await _repository.GetAllEmployees();
            List<Designation>? degsinations = await _repository.GetAllDesignations();
            List<Job>? jobs = await _repository.GetJobs();
            List<Client>? client = await _repository.GetAllClients();

            var matchedEmployees = employees
             .Where(ea => request.employeesAvailabity.Any(emp => emp.employee_mid == ea.employee_mid))
             .ToList();

            var matchedSupercoaches = supercoaches
            .Where(s => matchedEmployees.Any(emp => emp.supercoach_mid == s.employee_mid))
            .ToList();

            var matchedCosupercoaches = cosupercoaches
           .Where(s => matchedEmployees.Any(emp => emp.reporting_partner_mid == s.employee_mid))
           .ToList();

            var matchedDegsinations = degsinations
           .Where(s => matchedEmployees.Any(emp => emp.designation_id == s.designation_id))
           .ToList();

            List<string> uniquelocationIds = request.employeesAvailabity.Select(a=>a.location_id).DistinctBy(uid => uid).ToList();
            List<Location> locations = (await _repository.GetAllLocations())
                .Where(a => uniquelocationIds.Any(loc=> loc == a.location_id)).ToList();


            var uniqueUserIds = request.employeesAvailabity.Select(e => e.email_id_uid).Distinct().OrderBy(uid => uid).ToList();
            var publishedAllocationDays = await _allocationHttpService.PublishedResourceAllocationDays(uniqueUserIds, request.startDate, request.endDate);
            List<SuperCoach> employeeResponse = WcgtMapper.Mapper.Map<List<SuperCoach>>(matchedEmployees);

            var clientList = (from job in jobs
                              join published in publishedAllocationDays
                              on job.job_code equals published.jobCode
                              where !string.IsNullOrEmpty(job.job_client)
                                    && !string.IsNullOrEmpty(job.job_name)
                              select new
                              {
                                  ClientId = job.job_client,
                                  ClientName = job.job_name
                              })
                      .Distinct()
                      .ToList();
            var filteredClients = client
            .Where(c => clientList.Any(cl => cl.ClientId == c.client_id))
            .ToList();

            var response = new PortfolioFiltersOptions();
            response.employees = employeeResponse;
            response.designations = matchedDegsinations;
            response.clients = filteredClients;
            response.supercoaches = matchedSupercoaches;
            response.cosupercoaches = matchedCosupercoaches;
            response.locations = locations;
            return response;

        }
    }
}
