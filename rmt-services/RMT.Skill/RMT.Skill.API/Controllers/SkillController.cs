using MediatR;
using Microsoft.AspNetCore.Mvc;
using RMT.Skill.API.Services;
using RMT.Skill.Application.DTOs;
using RMT.Skill.Application.Handlers.CommandHandler;
using RMT.Skill.Application.Handlers.QueryHandler;
using RMT.Skill.Application.Mappers;
using RMT.Skill.Domain.DTOs;
using RMT.Skill.Domain.DTOs.Request;
using RMT.Skill.Domain.Entities;

namespace RMT.Skill.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SkillController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IUserAccessor _userAccessor;

        public SkillController(IMediator mediator, IUserAccessor userAccessor, ILogger<BaseController> logger) : base(logger)
        {
            _mediator = mediator;
            _userAccessor = userAccessor;
        }

        [HttpPost("SearchSkillsByNameOrCode")]
        public async Task<List<UserSkills>> SearchSkillsByNameOrCode([FromBody] List<string> skillNames)
        {
            try
            {
                var response = await _mediator.Send(new SkillSearchCommand()
                {
                    SkillName = skillNames
                });
                return response;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        //front end checked
        [HttpPost("SubmitSKill")]
        public async Task<IActionResult> AddSkills([FromBody] SkillSubmitDTO skillSubmit)
        {
            try
            {
                SkillSubmitCommand request = SkillMapper.Mapper.Map<SkillSubmitCommand>(skillSubmit);
                Boolean response = await _mediator.Send(request);
                if (response == true)
                {
                    return Ok(StatusCodes.Status201Created);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }

        }

        //front end checked
        [HttpPut("UpdateSkill")]
        public async Task<IActionResult> UpdateSkill([FromBody] SkillSubmitDTO skillSubmit)
        {
            try
            {
                SkillUpdateCommand request = SkillMapper.Mapper.Map<SkillUpdateCommand>(skillSubmit);
                var response = await _mediator.Send(request);
                if (response != null)
                {
                    return Ok(StatusCodes.Status200OK);
                }
                else
                {
                    return BadRequest();
                }

            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }

        }

        //front end checked
        [HttpPut("UpdateSkillStatus")]
        public async Task<IActionResult> UpdateSkillStatus([FromBody] UpdateSkillStatusDTO skillSubmit)
        {
            try
            {
                SkillStatusUpdateCommand request = SkillMapper.Mapper.Map<SkillStatusUpdateCommand>(skillSubmit);
                var response = await _mediator.Send(request);
                if (response != null)
                {
                    return Ok(StatusCodes.Status200OK);
                }
                else
                {
                    return BadRequest();
                }

            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }

        }

        [HttpGet("GetAllSkill")]
        public async Task<List<Skills>> GetAllSkill()
        {
            try
            {
                var request = new GetAllSkillsQuery();
                List<Skills> response = await _mediator.Send(request);
                return response;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }

        }
        [HttpPost("GetAllSkillBySkillCodeOrName")]
        public async Task<List<Skills>> GetAllSkillBySkillCode([FromBody] List<string> skillCodes)
        {
            try
            {
                var request = new GetAllSkillsBySkillCodeQuery()
                {
                    SkillCodes = skillCodes
                };
                List<Skills> response = await _mediator.Send(request);
                return response;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }

        }

        [HttpGet("GetSkillByName")]
        public async Task<Skills> GetSkillByName([FromQuery] string skillName)
        {
            try
            {
                var request = new GetSkillByNameQuery { SkillName = skillName };
                Skills response = await _mediator.Send(request);
                return response;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }

        }
        [HttpGet("GetAllSkillNameCode")]
        public async Task<List<SkillCodeNameDTO>> GetAllSkillCodeName()
        {
            try
            {
                var query = new GetAllSkillCodeNameQuery();
                List<SkillCodeNameDTO> response = await _mediator.Send(query);
                return response;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }

        }
        //TODO:COMP_CHANGE
        [HttpGet("GetMandatorySkill")]
        public async Task<List<SkillCodeNameDTO>> GetMandetorySkill([FromQuery] MandetorySkillRequestDTO request)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Competency) && string.IsNullOrEmpty(request.CompetencyId))
                {
                    throw new Exception("Both Competency Id And competency Name are null or empty");
                }
                var query = SkillMapper.Mapper.Map<GetMenatorySkillQuery>(request);
                List<SkillCodeNameDTO> response = await _mediator.Send(query);
                return response;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpGet("GetSkiilCategoryMaster")]
        public async Task<List<SkillCategoryMaster>> GetSkiilCategoryMaster()
        {
            try
            {
                var request = new GetSkillCategoryMasterQuery();
                List<SkillCategoryMaster> response = await _mediator.Send(request);
                return response;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }

        }


        [HttpPost("GetUserApprovedSkillByEmail")]
        public async Task<List<UserSkillsResponseWithSkillDTO>> GetUserApprovedSkillByEmail([FromBody] List<string> EmpEmails)
        {
            try
            {
                var request = new GetUserApprovedSkillByEmailQuery { emails = EmpEmails };
                List<UserSkillsResponseWithSkillDTO> response = await _mediator.Send(request);
                return response;
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
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
