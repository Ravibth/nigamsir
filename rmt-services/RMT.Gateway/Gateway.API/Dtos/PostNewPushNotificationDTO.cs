using System.Text.Json;

namespace Gateway.API.Dtos
{
    public class PostNewPushNotificationDTO
    {
        public string[] users { get; set; }
        public string? type { get; set; }
        public string? message { get; set; }
        public JsonDocument? meta { get; set; }
    }
}
