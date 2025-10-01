using MediatR;
using RMT.Allocation.Application.HttpServices.DTOs;
using RMT.Allocation.Application.IHttpServices;
using RMT.Allocation.Application.Utils;
using RMT.Allocation.Domain.DTO;
using RMT.Allocation.Domain.DTO.Response;
using RMT.Allocation.Domain.Repositories;
using Constants = RMT.Allocation.Infrastructure.Constants;

namespace RMT.Allocation.Application.Handlers.QueryHandlers
{
    public class IsUserAvailableQuery : IRequest<List<UsersAvailability>>
    {
        public string[] emails { get; set; }
        public DateTime start_date { get; set; }
        public DateTime end_date { get; set; }
        public int total_required_hours { get; set; }
    }

    public class IsUserAvailableQueryHandler : IRequestHandler<IsUserAvailableQuery, List<UsersAvailability>>
    {
        private readonly IDatesUtils _datesUtils;
        private readonly IResourceAllocationRepository _resourceAllocationRepository;
        private readonly IGetEmployeeLeavesHttpApi _getEmployeeLeavesHttpApi;
        public IsUserAvailableQueryHandler(
             IResourceAllocationRepository resourceAllocationRepository
             , IDatesUtils datesUtils
             , IGetEmployeeLeavesHttpApi getEmployeeLeavesHttpApi
            )
        {
            _resourceAllocationRepository = resourceAllocationRepository;
            _datesUtils = datesUtils;
            _getEmployeeLeavesHttpApi = getEmployeeLeavesHttpApi;
        }

        public async Task<List<UsersAvailability>> Handle(IsUserAvailableQuery request, CancellationToken cancellationToken)
        {
            GetEmployeeLeaves getEmployeeLeaves = new GetEmployeeLeaves { emails = request.emails.ToList(), start_date = request.start_date, end_date = request.end_date };
            Task<List<EmployeeLeavesDTO>> employeeLeaves = _getEmployeeLeavesHttpApi.GetEmployeeLeavesByEmails(getEmployeeLeaves);
            Task<int> weekdends = _datesUtils.GetNumberOfWeekends(request.start_date, request.end_date);
            await Task.WhenAll(employeeLeaves, weekdends);

            List<Leaves> leaves = new List<Leaves> { };
            foreach (var user in request.emails)
            {
                EmployeeLeavesDTO userLeavesFetched = employeeLeaves.Result.FirstOrDefault(m => m.email.ToLower() == user.ToLower());
                if (userLeavesFetched != null)
                {


                    var totalLeaveHours = await _getEmployeeLeavesHttpApi.CalculateTotalUserLeavesInHours(userLeavesFetched, weekdends.Result, Constants.WorkingHourPerDay);
                    Leaves userleaves = new Leaves
                    {
                        email = user,
                        leavesAllocationDTOs = userLeavesFetched.leaves,
                        totalhours = (int)totalLeaveHours
                    };
                    leaves.Add(userleaves);
                }
                else
                {
                    Leaves userleaves = new Leaves
                    {
                        email = user,
                        leavesAllocationDTOs = new List<LeavesDTO> { },
                        totalhours = 0
                    };
                }
            }

            UsersAvailabilityCheckDTO usersAvailabilityCheckDTO = new UsersAvailabilityCheckDTO()
            {
                emails = request.emails,
                end_date = request.end_date,
                start_date = request.start_date,
                leaves = leaves,
                total_required_hours = request.total_required_hours,
                perday_max_effort = (int)Constants.WorkingHourPerDay
            };
            //aayush_cross_check
            List<UsersAvailability> response = await _resourceAllocationRepository.isUserAvailable(usersAvailabilityCheckDTO);
            return response;
        }
    }
}
