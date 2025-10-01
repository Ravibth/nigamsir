using MediatR;
using Microsoft.AspNetCore.Mvc;
using RMT.Skill.API.Attributes;
using RMT.Skill.API.Services;
using RMT.Skill.Application.Handlers.CommandHandler;
using RMT.Skill.Application.Handlers.QueryHandler;
using RMT.Skill.Application.Requests;
using RMT.Skill.Domain;
using RMT.Skill.Domain.DTOs;
using RMT.Skill.Domain.DTOs.Responses;
using RMT.Skill.Domain.Entities;

namespace RMT.Skill.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserSkillsController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IUserAccessor _userAccessor;

        public UserSkillsController(IMediator mediator, IUserAccessor userAccessor, ILogger<UserSkillsController> logger) : base(logger)
        {
            _mediator = mediator;
            _userAccessor = userAccessor;
        }

        [HttpPost("AddUpdateUserSkill")]
        public async Task<List<AddUpdateNewUserSkill>> AddUpdateUserSkill([FromBody] List<AddUpdateUserSkillsRequestDTO> skillsToAdd)
        {
            try
            {
                UserDecorator userInfo = _userAccessor.GetUser();
                var request = new AddUpdateUserSkillsCommand()
                {
                    skills = skillsToAdd,
                    user = userInfo
                };
                return await _mediator.Send(request);
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpGet("GetUserSkillsByEmail")]
        public async Task<List<GetUserSkillsByEmailResponse>> GetUserSkillsByEmail([FromQuery] bool approvals = false)
        {
            try
            {
                UserDecorator userInfo = _userAccessor.GetUser();
                var request = new GetUserSkillsByEmailQuery()
                {
                    email = userInfo.email,
                    approvals = approvals
                };
                return await _mediator.Send(request);
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpPut("UpdateUserSkillStatusAfterWorkflow")]
        public async Task<List<UserSkills>> UpdateUserSkillStatusAfterWorkflow([FromBody] List<UpdateUserSkillStatusAfterWorkflowRequestDTO> request)
        {
            try
            {
                return await _mediator.Send(new UpdateUserSkillStatusAfterWorkflowCommand
                {
                    updatedActions = request
                });
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpGet("GetUserSkillsByID")]
        public async Task<List<GetUserSkillsByEmailResponse>> GetUserSkillsByID([FromQuery] string id, bool approvals = false)
        {
            try
            {
                var request = new GetUserSkillByIdQuery()
                {
                    Id = id,
                    approvals = approvals
                };
                return await _mediator.Send(request);
            }
            catch (Exception ex)
            {
                this.HandleException(ex); return null;
            }
        }

        [HttpGet("GetUserSkillsWithProficiency")]
        public async Task<List<UserSkills>> GetUserSkillsWithProficiency([FromQuery] string email)
        {
            try
            {
                var request = new GetUserSkillsWithProficiencyQuery()
                {
                    email = email
                };
                return await _mediator.Send(request);
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
