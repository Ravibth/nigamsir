using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WCGT.Domain.Entities;

public class Qualifications
{
    [JsonIgnore]
    public Guid id { get; set; }
    public string qualification_type { get; set; } = string.Empty; // e.g., "Education" or "Professional"    
    public string qualification { get; set; } = string.Empty;
    public string institution_name_location { get; set; } = string.Empty;
    public string month_year_of_passing { get; set; } = string.Empty;
    public string area_of_specialisation { get; set; } = string.Empty;  // Optional
    [JsonIgnore]
    public string employee_mid { get; set; } = string.Empty;
    [JsonIgnore]
    public Employee? Employee { get; set; }
}