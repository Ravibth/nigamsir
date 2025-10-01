using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Newtonsoft.Json;

namespace RMT.Configuration.Domain.Entities
{
    public class ConfigurationMasterSchema
    {
        public string Key { get; set; }
        public string KeyDisplay { get; set; }
        public string Description { get; set; } = String.Empty;
        public string ControlType { get; set; }
        public string? ValidationRegEx { get; set; }
    }

    public class ConfigurationMaster
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string ConfigGroup { get; set; }
        public string ConfigGroupDisplay { get; set; }
        public string Description { get; set; } = String.Empty;
        public string ConfigNote { get; set; } = String.Empty;
        public bool GlobalDefaultDisplay { get; set; }
        public bool SelectorWiseDisplay { get; set; }
        public string SelectorConfigType { get; set; }
        public JsonDocument? schema { get; set; }
        [NotMapped]
        public List<ConfigurationMasterSchema> schemaValues
        //{ get; set; }
        {
            get => string.IsNullOrEmpty(schema.RootElement.GetRawText()) ? new List<ConfigurationMasterSchema>() : JsonConvert.DeserializeObject<List<ConfigurationMasterSchema>>(schema.RootElement.GetRawText());
            set => schema = schema;
        }

        public virtual List<ConfigurationMainBreakup> ConfigurationMainBreakups { get; set; }
    }
}
