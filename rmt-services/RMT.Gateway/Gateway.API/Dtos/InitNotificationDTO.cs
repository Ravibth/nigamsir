namespace Gateway.API.Dtos
{
    public class InitNotificationDTO
    {
        public string path { get; set; }
        public string response_payload { get; set; }
        public string? token { get; set; }
        public string? request_payload { get; set; }
        public string? userinfo { get; set; }


    }

    public class NotificationPayload
    {
        public string? token { get; set; }
        public string action { get; set; }
        public string payload { get; set; }
    }
}
