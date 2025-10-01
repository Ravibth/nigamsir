namespace RMT.Configuration.Application.DTOs.ApplicationConfigurationDTOs
{
    //checked
    public class ConfigrationGroupDto
    {
        public Int64 Id { get; set; }
        public string ConfigGroup { get; set; }
        public string ConfigGroupDisplay { get; set; }
        public string ConfigKey { get; set; }
        public string CongigDisplayText { get; set; }
        public string ValueType { get; set; } // stri , bool ,
        public string ConfigType { get; set; }
        public bool IsAll { get; set; }
        public string AllValue { get; set; }
        public List<ProjectConfigurationDto> ProjectConfigurations { get; set; }
    }
    public class ProjectConfigurationDto
    {
        public Int64 Id { get; set; }
        public Int64 ConfigId { get; set; }
        public string AttributeName { get; set; }
        public string AttributeValue { get; set; }
    }
    public class UpdateConfigurationDTO
    {
        public List<ConfigrationGroupDto> ConfigrationGroupDtos { get; set; }
        public string ConfigurationType { get; set; }
    }
}
