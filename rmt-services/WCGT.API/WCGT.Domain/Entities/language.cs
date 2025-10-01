using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WCGT.Domain.Entities;

public class language
{
    [JsonIgnore]
    public Guid id { get; set; }
    [JsonIgnore]
    public string employee_mid { get; set; } = string.Empty;
    public string language_name { get; set; } = string.Empty;
    public string read { get; set; } = string.Empty;
    public string write { get; set; } = string.Empty;
    public string speak { get; set; } = string.Empty;
    [JsonIgnore]
    public Employee? Employee { get; set; }
}
