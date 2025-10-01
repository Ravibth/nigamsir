using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using RMT.Employee.API.Attributes;
using RMT.Employee.API.Services;
using RMT.Employee.Application.DTOs;
using RMT.Employee.Application.DTOs.EmployeePreferenceDTOs;
using RMT.Employee.Application.Handlers.CommandHandlers;
using RMT.Employee.Application.Handlers.QueryHandlers;
using RMT.Employee.Application.Response;
using RMT.Employee.Domain.DTOs;
using RMT.Employee.Domain.Entities;
using System.Reflection;
using System.Text.Encodings.Web;

namespace RMT.Employee.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IUserAccessor _userAccessor;

        public EmployeeController(IMediator mediator, IUserAccessor userAccessor, ILogger<BaseController> logger) : base(logger)
        {
            _mediator = mediator;
            _userAccessor = userAccessor;

        }

        [HttpPost("CreatePreferenceMaster")]
        public async Task<ActionResult<PreferenceMasterResponse>> CreatePreferenceMaster([FromBody] PreferenceMasterDTO preferenceMasterDTO)
        {
            try
            {
                var result = await _mediator.Send(new PreferenceMasterCommand()
                {
                    Name = preferenceMasterDTO.Name,
                    Category = preferenceMasterDTO.Category,
                    SortOrder = preferenceMasterDTO.SortOrder,
                    Description = preferenceMasterDTO.Description,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "System",
                    ModifiedAt = DateTime.UtcNow,
                    ModifiedBy = "System",
                    IsActive = true,
                });
                return result;
            }
            catch (Exception ex)
            {

                //this.LogException(ex);
                this.HandleException(ex); return null;
            }
        }

        [HttpPost("UpdatePreferenceMaster")]
        public async Task<ActionResult<PreferenceMasterResponse>> UpdatePreferenceMaster([FromBody] UpdatePreferenceMasterDTO preferenceMasterDTO)
        {
            try
            {
                var result = await _mediator.Send(new UpdatePreferenceMasterCommand()
                {
                    Id = preferenceMasterDTO.PreferenceMasterId,
                    Name = preferenceMasterDTO.Name,
                    Category = preferenceMasterDTO.Category,
                    SortOrder = preferenceMasterDTO.SortOrder,
                    Description = preferenceMasterDTO.Description,
                    CreatedAt = preferenceMasterDTO.CreatedAt,
                    CreatedBy = preferenceMasterDTO.CreatedBy,
                    ModifiedAt = DateTime.UtcNow,
                    ModifiedBy = "System",
                    IsActive = (bool)preferenceMasterDTO.IsActive
                });
                return result;
            }
            catch (Exception ex)
            {

                //this.LogException(ex);
                this.HandleException(ex); return null;
            }
        }

        [HttpPost("CreateEmployeePreference")]
        public async Task<ActionResult<EmployeePreferenceResponse>> CreateEmployeePreference([FromBody] EmployeePreferenceDTO employeePreferenceDTO)
        {
            try
            {
                var result = await _mediator.Send(new EmployeePreferenceCommand()
                {
                    PreferedValue = "Described Preference",
                    EmployeeEmail = employeePreferenceDTO.EmployeeEmail,
                    Category = employeePreferenceDTO.Category,
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "",
                    ModifiedAt = DateTime.UtcNow,
                    ModifiedBy = "",
                    IsActive = true,
                });
                return result;
            }
            catch (Exception ex)
            {
                //this.LogException(ex);
                this.HandleException(ex); return null;
            }
        }

        [HttpGet("GetEmployeePreferenceByEmail")]
        [SanitizeInput]
        public async Task<List<EmployeePreference>> GetEmployeePreferenceByEmail(string EmployeeEmail)
        {
            try
            {
                var request = new GetEmployeePreferenceByEmailQuery()
                {
                    EmployeeEmail = EmployeeEmail
                };
                var result = await _mediator.Send(request);
                return result;
            }
            catch (Exception ex)
            {
                //this.LogException(ex);
                this.HandleException(ex); return null;
            }
        }

        [HttpPost("GetEmployeePreferenceByEmails")]
        [SanitizeInput]
        public async Task<List<EmployeePreferencesByEmailDTO>> GetEmployeePreferencesByEmails(List<string> emails)
        {
            try
            {
                var request = new GetEmployeePrferencesByEmailsQuery()
                {
                    emails = emails
                };
                List<EmployeePreferencesByEmailDTO> preferences = await _mediator.Send(request);
                return preferences;
            }
            catch (Exception ex)
            {
                //this.LogException(ex);
                this.HandleException(ex); return null;
            }
        }

        [HttpGet("GetAllPreferenceMasteer")]
        [SanitizeInput]
        public async Task<List<PreferenceMaster>> GetAllPreferenceMaster()
        {
            try
            {
                var request = new GetAllPreferenceMasterQuery() { };
                var result = await _mediator.Send(request);
                return result;
            }
            catch (Exception ex)
            {
                //this.LogException(ex);
                this.HandleException(ex); return null;
            }
        }

        [HttpPost("UpdateEmployeePreference")]
        //[ValidateAntiForgeryToken]
        [SanitizeInput]
        public async Task<ActionResult<List<EmployeePreference>>> UpdateEmployeePreference([FromBody] List<UpdateEmployeePreferenceDTO> command)
        {
            try
            {
                var user = _userAccessor.GetUser();
                var result = await _mediator.Send(new UpdateEmployeePreferenceCommand()
                {
                    EmployeePreferences = command,
                    UserEmail = string.IsNullOrEmpty(user.email) ? string.Empty : user.email

                });
                return result;

            }
            catch (Exception ex)
            {
                //this.LogException(ex);
                this.HandleException(ex); return null;
            }
        }

        [HttpPost("AddEmployeeProjectMapping")]
        [SanitizeInput]
        public async Task<ActionResult<List<EmpProjectMappingResponse>>> AddEmployeeProjectMapping([FromBody] List<EmployeeProjectMappingDTO> command)
        {
            try
            {
                var user = _userAccessor.GetUser();
                var result = await _mediator.Send(new EmpProjectMappingCommand()
                {
                    EmployeeProjectMappings = command,
                    UserEmail = string.IsNullOrEmpty(user?.email) ? string.Empty : user.email

                });
                return result;

            }
            catch (Exception ex)
            {
                //this.LogException(ex);
                this.HandleException(ex); return null;
            }
        }

        [HttpPost("GetEmpByProjectMapping")]
        [SanitizeInput]
        public async Task<ActionResult<List<EmpProjectMappingResponse>>> GetEmpByProjectMapping([FromBody] EmpByProjectMappingRequestDto command)
        {
            try
            {
                var user = _userAccessor.GetUser();
                var result = await _mediator.Send(new GetEmpByProjectMappingQuery()
                {
                    request = command,
                    UserEmail = string.IsNullOrEmpty(user?.email) ? string.Empty : user.email

                });
                return result;

            }
            catch (Exception ex)
            {
                //this.LogException(ex);
                this.HandleException(ex); return null;
            }
        }
        [HttpGet("get-employee-profile/{emailId}")]
        public async Task<EmployeeProfileResponseDTO> GetEmployeeProfile([FromRoute] string emailId)
        {
            try
            {
                var user_info = _userAccessor.GetUser();
                var result = await _mediator.Send(new GetEmployeeProfileByEmployeeEmailQuery()
                {
                    employee_email = emailId,
                    email = user_info.email
                });
                return result;
            }
            catch (Exception ex)
            {
                this.HandleException(ex);
                return null;
                //throw;
            }
        }
        [HttpPost("update-employee-profile")]
        public async Task<EmployeeProfile> UpdateEmployeeProfile([FromBody] UpdateEmployeeProfileRequest req)
        {
            try
            {
                var result = await _mediator.Send(new UpdateEmployeeProfileCommand()
                {
                    param = req
                });
                return result;
            }
            catch (Exception ex)
            {
                this.HandleException(ex);
                return null;
                //throw;
            }
        }


        /// <summary>
        /// SanitizeInputData
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string SanitizeInputData(string? str)
        {
            if (!string.IsNullOrEmpty(str))
            {
                string strEncode = HtmlEncoder.Default.Encode(str);
                return strEncode;
            }
            else
            {
                return str;
            }
        }

        /// <summary>
        /// HandleException
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        /// <exception cref="BadHttpRequestException"></exception>
        private object HandleException(Exception ex)
        {
            Guid guid = Guid.NewGuid();
            this.LogException(ex, guid);
            throw new BadHttpRequestException($"{ex.Message}-errorid:{guid}", StatusCodes.Status400BadRequest);//, ex);
        }

    }
}

