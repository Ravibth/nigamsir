using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.ProjectService.DTOs
{
    public class SuspendedProjectRequest
    {
        public List<KeyValuePair<string, string>> projectCodes;
        public Boolean isSuspended;
    }
}
