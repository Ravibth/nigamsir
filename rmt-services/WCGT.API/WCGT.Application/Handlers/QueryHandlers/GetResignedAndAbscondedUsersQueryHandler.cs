using MediatR;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCGT.Application.Services.IHttpServices;
using WCGT.Domain.DTO;
using WCGT.Domain.Entities;
using WCGT.Domain.IRepositories;
using WCGT.Infrastructure;
using static WCGT.Application.Common;

namespace WCGT.Application.Handlers.QueryHandlers
{
    public class GetResignedAndAbscondedUsersWithLastAvailableDayQuery : IRequest<List<GetResignedAndAbscondedUsersWithLastAvailableDayResponseDto>>
    {
        public List<string> emails { get; set; }
    }
    public class GetResignedAndAbscondedUsersWithLastAvailableDayQueryHandler : IRequestHandler<GetResignedAndAbscondedUsersWithLastAvailableDayQuery, List<GetResignedAndAbscondedUsersWithLastAvailableDayResponseDto>>
    {
        private readonly IWcgtDataRepository _repository;
        private readonly IConfiguration _config;
        private readonly IConfigurationHttpService _configurationHttpService;

        public GetResignedAndAbscondedUsersWithLastAvailableDayQueryHandler(IWcgtDataRepository repository, IConfiguration config, IConfigurationHttpService configurationHttpService)
        {
            _repository = repository;
            _config = config;
            _configurationHttpService = configurationHttpService;
        }

        public async Task<List<GetResignedAndAbscondedUsersWithLastAvailableDayResponseDto>> Handle(GetResignedAndAbscondedUsersWithLastAvailableDayQuery request, CancellationToken cancellationToken)
        {

            Task<Dictionary<string, string>> applicationLevelSetting = _configurationHttpService.GetApplicationLevelSettings(new List<string> { ApplicationLevelSettingsKeys.ResignedUserAvailabilityThreshold });
            Task<List<Employee>> abscondedResignedUsers = _repository.GetResignedAndAbscondedUsers(request.emails);

            await Task.WhenAll(applicationLevelSetting, abscondedResignedUsers);
            string DaysBeforeEmployeeBecomesUnavailableBeforeLastWorkingDay = applicationLevelSetting.Result.Where(m => m.Key.Equals(ApplicationLevelSettingsKeys.ResignedUserAvailabilityThreshold)).FirstOrDefault().Value;

            List<GetResignedAndAbscondedUsersWithLastAvailableDayResponseDto> result = new();
            foreach (var user in abscondedResignedUsers.Result)
            {
                var isAbsconderCase = user.employee_status.ToLower() == Constants.ABSCONDER.ToLower() ? true : false;

                if (isAbsconderCase)
                {
                    result.Add(new()
                    {
                        email_id = user.employee_mid + "__" + user.email_id,
                        uemail_id = user.email_id,
                        employee_mid = user.employee_mid,
                        last_available_day = DateOnly.FromDateTime(DateTime.Now)
                    });
                }
                else
                {
                    if (user.proposed_lwd != null && user.resignation_date != null)
                    {
                        var last_working_day = user.proposed_lwd;

                        int daysDifference = Convert.ToInt16(DaysBeforeEmployeeBecomesUnavailableBeforeLastWorkingDay);

                        daysDifference = user.proposed_lwd != null ? daysDifference * -1 : daysDifference * 1;

                        var last_available_day = !String.IsNullOrEmpty(DaysBeforeEmployeeBecomesUnavailableBeforeLastWorkingDay) ? ((DateOnly)last_working_day).AddDays(daysDifference) : last_working_day;
                        result.Add(new()
                        {
                            email_id = user.employee_mid + "__" + user.email_id,
                            uemail_id = user.email_id,
                            employee_mid = user.employee_mid,
                            last_available_day = (DateOnly)last_available_day
                        });
                    }
                }
            }
            return result;
        }
    }
}
