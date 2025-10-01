using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WCGT.Application.Mappers;
using WCGT.Application.Responses;
using WCGT.Domain.Entities;
using WCGT.Domain.Entities.Enums;
using WCGT.Domain.IRepositories;
using WCGT.Infrastructure.DTOs;

namespace WCGT.Application.Handlers.CMDHandler
{
    public class EmployeeQuery : IRequest<List<EmployeeListResponse>>
    {
        public List<GTEmployeeDTO> employees { get; set; }
    }

    public class EmployeeCommandHandler : IRequestHandler<EmployeeQuery, List<EmployeeListResponse>>
    {
        private readonly IWcgtDataRepository _repository;
        public EmployeeCommandHandler(IWcgtDataRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<EmployeeListResponse>> Handle(EmployeeQuery request, CancellationToken cancellationToken)
        {
            List<EmployeeListResponse> response = new();

            EmployeeListResponse _response = null;
            foreach (var current_item in request.employees)
            {
                _response = WcgtMapper.Mapper.Map<EmployeeListResponse>(current_item);
                try
                {
                    Employee employee = WcgtMapper.Mapper.Map<Employee>(current_item);

                    // Add Qualifications
                    employee.Qualifications = current_item.education_qualification != null
                            ? current_item.education_qualification.Select(q => new Qualifications
                            {
                                qualification = q.qualification,
                                institution_name_location = q.institution_name_location,
                                month_year_of_passing = q.month_year_of_passing,
                                area_of_specialisation = q.area_of_specialisation,
                                qualification_type = QualificationType.Education.ToString(),
                                employee_mid = current_item.employee_mid
                            }).ToList()
                            : new List<Qualifications>();

                    employee.Qualifications.AddRange(current_item.professional_qualification.Select(q => new Qualifications
                    {
                        qualification = q.qualification,
                        institution_name_location = q.institution_name_location,
                        month_year_of_passing = q.month_year_of_passing,
                        area_of_specialisation = q.area_of_specialisation,
                        qualification_type = QualificationType.Professional.ToString(),
                        employee_mid = current_item.employee_mid
                    }));

                    employee.PastEmploymentDetails = current_item.past_employment_details;
                    employee.Language = current_item.language;

                    Employee response1 = await _repository.UpdateEmployee(employee);
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
