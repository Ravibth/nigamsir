namespace RMT.Configuration.Application.DTOs.ApplicationConfigurationDTOs
{
    //checked
    public class CreateConfigurationGroupDTO
    {
        public string ConfigGroup { get; set; }
        public string ConfigGroupDisplay { get; set; }
        public string ConfigKey { get; set; }
        public string CongigDisplayText { get; set; }
        public string ValueType { get; set; }
        public string ConfigType { get; set; }
        public bool IsAll { get; set; }
        public string AllValue { get; set; }
        public bool IsActive { get; set; }
    }
}
