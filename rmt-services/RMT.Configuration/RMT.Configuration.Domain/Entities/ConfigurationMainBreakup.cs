using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace RMT.Configuration.Domain.Entities
{
    public class ConfigurationMainBreakupMetaValue
    {
        public string Key { get; set; }
        public string DisplayKey { get; set; }
        public string Value { get; set; }
    }

    public class ConfigurationMainBreakup
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public Guid ConfigurationMasterId { get; set; }//
        public string KeySelector { get; set; } = ConfigMasterKeyDisplayLabel.Default_Key_Selector;//  BU1|OFFER1
        public JsonDocument MetaValue { get; set; }
        public string CreatedBy { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        [NotMapped]
        public List<ConfigurationMainBreakupMetaValue> ConfigurationMainBreakupMetaValues
        //{ get; set; }
        {
            get => MetaValue == null ? new List<ConfigurationMainBreakupMetaValue>() : JsonConvert.DeserializeObject<List<ConfigurationMainBreakupMetaValue>>(MetaValue.RootElement.GetRawText());
            set => MetaValue = MetaValue;
        }
        [ForeignKey("ConfigurationMasterId")]
        public virtual ConfigurationMaster ConfigurationMaster { get; set; }
    }
}
