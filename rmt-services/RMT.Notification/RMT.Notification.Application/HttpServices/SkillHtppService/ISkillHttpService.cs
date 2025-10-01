using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Notification.Application.HttpServices.SkillHtppService
{
    public interface ISkillHttpService
    {
        Task<string> GetUserSkillById(string guid);
    }
}
