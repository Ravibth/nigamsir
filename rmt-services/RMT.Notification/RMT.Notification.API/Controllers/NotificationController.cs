using MediatR;
using Microsoft.AspNetCore.Mvc;
using RMT.Notification.Application.Handlers.CommandHandlers;
using RMT.Notification.Application.Handlers.QueryHandlers;
using RMT.Notification.Application.Responses;
using RMT.Notification.Domain.Entities;
using RMT.Notification.API;
using RMT.Notification.Application.RequestDTO;
using RMT.Notification.API.Services;
using Microsoft.AspNetCore.SignalR;
using RMT.Notification.API.Hubs;
using RMT.Notification.Domain.DTO;
using RMT.Notification.API.Attributes;
using System.Collections.Generic;

namespace RMT.Notification.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : BaseController
    {
        private readonly IMediator _mediator;
        private readonly IUserAccessor _userAccessor;
        private readonly IHubContext<NotificationHub> _hubContext;

        public NotificationController(IMediator mediator, IUserAccessor userAccessor, IHubContext<NotificationHub> hubContext, ILogger<BaseController> logger) : base(logger)
        {
            _mediator = mediator;
            _userAccessor = userAccessor;
            _hubContext = hubContext;
        }

        [HttpGet("GetLoggedInUserNotifications")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<List<UserNotificationsResponse>> GetLoggedInUserNotifications([FromQuery] Int64 limit, [FromQuery] Int64 pagination)
        {
            try
            {
                var userInfo = _userAccessor.GetUser();

                var request = new GetLoggedInUserNotificationsQuery()
                {
                    email = userInfo.email,
                    limit = limit,
                    pagination = pagination
                };
                return await _mediator.Send(request);
            }
            catch (Exception ex)
            {
                //this.LogException(ex);
                this.HandleException(ex); return null;
            }
        }

        [HttpGet("GetLoggedInUserAllNotificationsCount")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<int> GetLoggedInUserAllNotificationsCount()
        {
            try
            {
                var userInfo = _userAccessor.GetUser();
                if (!String.IsNullOrEmpty(userInfo.email))
                {
                    var request = new GetLoggedInUserAllNotificationsCountQuery()
                    {
                        email = userInfo.email
                    };
                    return await _mediator.Send(request);
                }
                else
                {
                    throw new Exception("Email not found");
                }

            }
            catch (Exception ex)
            {
                //this.LogException(ex);
                this.HandleException(ex); return 0;
            }
        }

        [HttpGet("MarkNotificationsAsRead")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<bool> MarkNotificationsAsRead([FromQuery] Guid[] id)
        {
            var request = new MarkNotificationsAsReadCommand() { id = id };
            await _mediator.Send(request);
            return true;
        }

        [HttpGet("MarkAllNotificationsAsRead")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<bool> MarkAllNotificationsAsRead()
        {
            try
            {
                var userInfo = _userAccessor.GetUser();
                if (!String.IsNullOrEmpty(userInfo.email))
                {
                    var request = new MarkAllNotificationsAsReadCommand()
                    {
                        email = userInfo.email,
                    };
                    await _mediator.Send(request);
                    return true;
                }
                else
                {
                    throw new Exception("Email not found");
                }
            }
            catch (Exception ex)
            {
                //this.LogException(ex);
                this.HandleException(ex); return false;
            }
        }


        [HttpPost("PostNewNotification")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<bool> PostNewNotification([FromBody] Application.RequestDTO.PostNewNotificationRequestDTO requestDTO)
        {
            try
            {
                var userInfo = _userAccessor.GetUser();
                var request = new PostNewNotificationCommand()
                {
                    users = requestDTO.users,
                    data = new Domain.DTO.PostNewNotificationRequestDTO()
                    {
                        createdBy = userInfo.email,
                        createdDate = DateTime.UtcNow,
                        type = requestDTO.type,
                        message = requestDTO.message,
                        meta = requestDTO.meta,
                        notification_template_id = requestDTO.notification_template_id,
                        link = requestDTO.link
                    },
                };
                var result = await _mediator.Send(request);
                foreach (var item in result)
                {
                    _hubContext.Clients.Groups(item.email).SendAsync(Constants.FoundNewNotification, item);
                }
                return true;
            }
            catch (Exception ex)
            {
                //this.LogException(ex);
                this.HandleException(ex); return false;
            }
        }

        [HttpGet("GetNotificationTemplate")]
        public async Task<List<GetNotificationTemplateResponse>> GetNotificationTemplate([FromQuery] string[] type)
        {
            try
            {
                var request = new GetNotificationTemplateQuery()
                {
                    type = type,
                };
                return await _mediator.Send(request);
            }
            catch (Exception ex)
            {
                //this.LogException(ex);
                this.HandleException(ex); return null;
            }
        }

        [HttpPost("SubscribeToNotification")]
        public async Task<NotificationSubscriptionDTO> SubscribeToNotification([FromBody] NotificationSubscriptionDTO requestDTO)
        {
            try
            {
                //var userInfo = _userAccessor.GetUser();
                var request = new NotificationSubscriptionCommand()
                {
                    module = requestDTO.module,
                    subscription_role = requestDTO.subscription_role,
                    user_emailid = requestDTO.user_emailid,
                    user_name = requestDTO.user_name,
                    is_active = requestDTO.is_active,
                    createdDate = DateTime.UtcNow,
                    modifiedDate = DateTime.UtcNow,
                    createdBy = requestDTO.createdBy,
                    modifiedBy = requestDTO.modifiedBy
                };

                return await _mediator.Send(request);
            }
            catch (Exception ex)
            {
                //this.LogException(ex);
                this.HandleException(ex); return null;
            }
        }

        [HttpGet("GetNotificationSubscription")]
        //[ValidateAntiForgeryToken]
        [SanitizeInput]
        public async Task<List<NotificationSubscriptionDTO>> GetNotificationSubscription([FromQuery] GetNotificationSubscriptionQuery requestDTO)
        {
            try
            {
                //var userInfo = _userAccessor.GetUser();
                var request = new GetNotificationSubscriptionQuery()
                {
                    module = requestDTO.module,
                    subscription_role = requestDTO.subscription_role,
                    user_emailid = requestDTO.user_emailid,
                };

                return await _mediator.Send(request);
            }
            catch (Exception ex)
            {
                //this.LogException(ex);
                this.HandleException(ex); return null;
            }
        }


        [HttpPost("SendNotification")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<Boolean> SendNotification([FromBody] List<SendNotificationRequest> requestDTO)
        {
            try
            {
                foreach (var request in requestDTO)
                {
                    try
                    {
                        var templateRequest = new GetNotificationTemplateQuery()
                        {
                            type = new String[] { request.NotificationKey },
                        };
                        var template = await _mediator.Send(templateRequest);
                        foreach (var item in template)
                        {
                            try
                            {
                                if (template != null)
                                {
                                    var command = new SendNotificationCommand
                                    {
                                        meta = request.meta,
                                        NotificationKey = request.NotificationKey,
                                        NotificationType = item.notification_type,
                                        NotificationTemplate = item
                                    };
                                    var result = await _mediator.Send(command);
                                    // return true;
                                }
                            }
                            catch (Exception ex1)
                            {
                                this.LogException(ex1, "SendNotification--templateRequest-exception");
                            }
                        }
                    }
                    catch (Exception ex2)
                    {
                        this.LogException(ex2, "SendNotification--requestDTO-exception");
                    }
                }


                return true;
            }
            catch (Exception ex)
            {
                //this.LogException(ex);
                this.HandleException(ex); return false;
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
