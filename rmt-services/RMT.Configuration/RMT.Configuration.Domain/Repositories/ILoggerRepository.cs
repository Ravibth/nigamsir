using RMT.Configuration.Domain.DTO;
using RMT.Configuration.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Configuration.Domain.Repositories
{
    public interface ILoggerRepository
    {
        Task<bool> LogObject(LoggerDTO logObj);

    }
}
