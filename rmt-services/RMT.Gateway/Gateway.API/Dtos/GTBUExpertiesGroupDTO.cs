using System.Collections.Generic;

namespace Gateway.API.Dtos
{
    public class GTBUExpertiesGroupDTO
    {

        public Dictionary<string, string> BU { get; set; }
        //public Dictionary<string, string> Experties { get; set; }//Recheck
        //public Dictionary<string, string> SMEG { get; set; }//Recheck
        public Dictionary<string, string> Offerings { get; set; }
        public Dictionary<string, string> Solutions { get; set; }



    }
}
