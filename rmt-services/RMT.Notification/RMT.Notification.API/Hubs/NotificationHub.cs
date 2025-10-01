using MediatR;
using Microsoft.AspNetCore.SignalR;
using RMT.Notification.API.Services;
using RMT.Notification.Application.Handlers.CommandHandlers;
using RMT.Notification.Application.Handlers.QueryHandlers;
using RMT.Notification.Application.RequestDTO;
using RMT.Notification.Application.Responses;
using RMT.Notification.Domain.Entities;

namespace RMT.Notification.API.Hubs
{
    public class NotificationHub : Hub
    {
        private readonly IMediator _mediator;

        public NotificationHub(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Get User Connected Result
        /// </summary>
        /// <returns></returns>
        public override async Task<Task> OnConnectedAsync()
        {
            Console.WriteLine("Connected");
            return base.OnConnectedAsync();
        }

        /// <summary>
        /// Get User Disconnected Result
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public override async Task<Task> OnDisconnectedAsync(Exception exception)
        {
            Console.WriteLine("Disconnected");
            return base.OnDisconnectedAsync(exception);
        }

        /// <summary>
        /// Join Master Group for loggedinuser email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<bool> JoinMasterGroup(string email)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, email);
            return true;
        }

        /// <summary>
        /// Leave Master Group for loggedinuser
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<bool> LeaveMasterGroup(string email)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, email);
            return true;
        }

        public async Task GetLoggedInUserAllNotificationsCount(string email)
        {
            var request = new GetLoggedInUserAllNotificationsCountQuery()
            {
                email = email
            };
            var response = await _mediator.Send(request);
            Clients.Caller.SendAsync(Constants.NotificationCount, response);
        }

        /// <summary>
        /// Get all the unread notifications for a user
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task GetLoggedInUserNotifications(GetNotificationsRequestDTO requestDTO)
        {
            var request = new GetLoggedInUserNotificationsQuery()
            {
                email = requestDTO.email,
                limit = requestDTO.limit,
                pagination = requestDTO.pagination
            };
            var response = await _mediator.Send(request);
            
            Clients.Caller.SendAsync(Constants.GetLoggedInUserAllNotifications, response);
        }

        /// <summary>
        /// Send Newly Added Notifications to only specific users
        /// </summary>
        /// <param name="emails"></param>
        /// <returns></returns>
        public async Task SendNewlyPostedNotificationsFoundToUsersOnline(List<UserNotificationsResponse> emails)
        {
            foreach (var item in emails)
            {
                Clients.Groups(item.email).SendAsync(Constants.FoundNewNotification, item);
            }
        }

        /// <summary>
        /// Mark Notification as read when a user reads them
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task MarkNotificationsAsRead(Guid[] id)
        {
            var request = new MarkNotificationsAsReadCommand() { id = id };
            await _mediator.Send(request);
        }
    }
}