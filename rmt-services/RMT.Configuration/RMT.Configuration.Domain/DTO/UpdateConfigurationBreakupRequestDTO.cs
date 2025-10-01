using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Configuration.Domain.DTO
{
    public class ConfigurationMetaValue
    {
        public string key { get; set; }
        public string value { get; set; }
    }
    public class UpdateConfigurationBreakupRequestDTO
    {
        public string ConfigurationMasterId { get; set; }//nn
        public List<ConfigurationMetaValue> configurationMetaValues { get; set; }
        public string KeySelector { get; set; }//BU1|OFFERING1
        public bool IsActive { get; set; }
    }
}