using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RMT.Allocation.Domain.ConstantsDomain;

namespace RMT.Allocation.Domain.Helpers
{
    public static class Helper
    {
        public static bool IsResourceRequestor(List<string> userRoles)
        {
            List<string> ResourceRequestor = new List<string>()
            {
                UserRolesConstant.ResourceRequestor,
                UserRolesConstant.JobManager,
                UserRolesConstant.EO,
                UserRolesConstant.EngagementLeader,
                UserRolesConstant.ProposedEL
            };
            bool isRR = ResourceRequestor.Where((ur) => userRoles.Any(r => r.Equals(ur, StringComparison.OrdinalIgnoreCase))).Any();
            return isRR;
        }
    }
}
