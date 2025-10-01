using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static RMT.Projects.Infrastructure.Constants;

namespace RMT.Projects.Infrastructure.Helpers
{
    public static class Helper
    {
        static string projectStatusSeperator = " - ";

        public static string GetProjectStateByActivationAndClosureState(bool? projectClosureState, bool? projectActivationState)
        {
            if (projectClosureState == null && projectActivationState == null)
            {
                return null;
            }
            else if (projectClosureState == null && projectActivationState != null)
            {
                return (bool)projectActivationState ? ProjectActivationStatus.ACTIVE : ProjectActivationStatus.IN_ACTIVE;
            }
            else if (projectClosureState != null && projectActivationState == null)
            {
                return !(bool)projectClosureState ? ProjectClosureStatus.OPEN : ProjectClosureStatus.CLOSED;
            }
            else
            {
                return (!(bool)projectClosureState ? ProjectClosureStatus.OPEN : ProjectClosureStatus.CLOSED) + projectStatusSeperator + ((bool)projectActivationState ? ProjectActivationStatus.ACTIVE : ProjectActivationStatus.IN_ACTIVE);
            }
        }
    }
}
