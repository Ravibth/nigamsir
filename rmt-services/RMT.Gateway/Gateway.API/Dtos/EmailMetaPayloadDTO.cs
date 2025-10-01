using System.Text.Json.Nodes;

namespace Gateway.API.Dtos
{
    public class EmailMetaPayloadDTO
    {
        public string[] to { get; set; }
        public string[] cc { get; set; }
        public string meta {  get; set; }
    }
}
