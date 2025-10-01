using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WCGT.Domain.Entities;

public class PastEmploymentDetails
{
    [JsonIgnore]
    public Guid id { get; set; }
    [JsonIgnore]
    public string employee_mid { get; set; } = string.Empty;
    public string name_of_employer { get; set; } = string.Empty;
    public string from { get; set; } = string.Empty;
    public string to { get; set; } = string.Empty;
    public string last_designation_held { get; set; } = string.Empty;

    [JsonIgnore]
    public Employee? Employee { get; set; }
}
