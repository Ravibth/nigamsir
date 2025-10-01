using MediatR;
using RMT.Employee.Application.DTOs.EmployeePreferenceDTOs;
using RMT.Employee.Domain.Repositories;

namespace RMT.Employee.Application.Handlers.QueryHandlers
{
    public class GetEmployeePrferencesByEmailsQuery : IRequest<List<EmployeePreferencesByEmailDTO>>
    {
        public IEnumerable<string> emails;
    }
    public class GetEmployeePrferencesByEmailsQueryHandler : IRequestHandler<GetEmployeePrferencesByEmailsQuery, List<EmployeePreferencesByEmailDTO>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        public GetEmployeePrferencesByEmailsQueryHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public async Task<List<EmployeePreferencesByEmailDTO>> Handle(GetEmployeePrferencesByEmailsQuery request, CancellationToken cancellationToken)
        {
            var employeePreference = await _employeeRepository.GetEmployeePreferencesByEmails(request.emails.ToList());
            List<EmployeePreferencesByEmailDTO> result = new List<EmployeePreferencesByEmailDTO>();
            foreach (var email in request.emails)
            {
                var empPref = employeePreference.Where(d => d.EmployeeEmail.Trim().ToLower() == email.Trim().ToLower());
                var finalPrefList = new List<EmployeePrefDTO>();
                foreach (var pref in empPref)
                {
                    if (pref.Category == "LOCATION")
                    {
                        if (pref.PreferenceDetails.location != null)
                        {
                            var prefItem = new EmployeePrefDTO
                            {
                                Category = "LOCATION",
                                Name = pref.PreferenceDetails.location.location_name,
                                PreferenceId = pref.PreferenceDetails.location.location_id,
                            };
                            finalPrefList.Add(prefItem);
                        }
                    }
                    else if (pref.Category == "INDUSTRY_MAPPING")
                    {
                        if (pref.PreferenceDetails.industry != null)
                        {

                            var prefIndItem = new EmployeePrefDTO
                            {
                                Category = "INDUSTRY",
                                Name = pref.PreferenceDetails.industry.industry_name,
                                PreferenceId = pref.PreferenceDetails.industry.industry_id,
                            };
                            finalPrefList.Add(prefIndItem);
                        }
                    }
                    if (pref.PreferenceDetails.subIndustry != null)
                    {
                        var prefItem = new EmployeePrefDTO
                        {
                            Category = "SUB_INDUSTRY",
                            Name = pref.PreferenceDetails.subIndustry.sub_industry_name,
                            PreferenceId = pref.PreferenceDetails.subIndustry.sub_industry_id,
                        };
                        finalPrefList.Add(prefItem);
                    }
                    else if (pref.Category == "BU_TREE_MAPPING")
                    {
                        if (pref.PreferenceDetails.businessUnit != null)
                        {
                            var prefBuItem = new EmployeePrefDTO
                            {
                                Category = "BUSSINESS_UNIT",
                                Name = pref.PreferenceDetails.businessUnit.BuName,
                                PreferenceId = pref.PreferenceDetails.businessUnit.BuId,
                            };
                            finalPrefList.Add(prefBuItem);
                        }

                        if (pref.PreferenceDetails.solution != null)
                        {
                            var prefItem = new EmployeePrefDTO
                            {
                                Category = "SOLUTION",
                                Name = pref.PreferenceDetails.solution.SolutionName,
                                PreferenceId = pref.PreferenceDetails.solution.SolutionId,
                            };
                            finalPrefList.Add(prefItem);
                        }

                        if (pref.PreferenceDetails.offering != null)
                        {
                            var prefItem = new EmployeePrefDTO
                            {
                                Category = "OFFERING",
                                Name = pref.PreferenceDetails.offering.OfferingName,
                                PreferenceId = pref.PreferenceDetails.offering.OfferingId,
                            };
                            finalPrefList.Add(prefItem);
                        }
                    }
                }
                result.Add(new EmployeePreferencesByEmailDTO()
                {
                    Email = email,
                    EmployeePreference = finalPrefList
                });
            }
            return result;
        }
    }
}
