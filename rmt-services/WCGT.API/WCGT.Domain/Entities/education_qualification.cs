using System.Text.Json.Serialization;

namespace WCGT.Domain.Entities;

public class education_qualification
{
    [JsonIgnore]
    public Guid Id { get; set; }
    public string qualification { get; set; } = string.Empty;
    public string institution_name_location { get; set; } = string.Empty;
    public string month_year_of_passing { get; set; } = string.Empty;
    public string area_of_specialisation { get; set; } = string.Empty;
}

