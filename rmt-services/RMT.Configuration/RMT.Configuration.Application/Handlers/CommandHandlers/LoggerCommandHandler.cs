using MediatR;
using Microsoft.Extensions.Logging;
using RMT.Configuration.Application.Mappers;
using RMT.Configuration.Domain.DTO;
using RMT.Configuration.Domain.Entities;
using RMT.Configuration.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RMT.Configuration.Application.Handlers.CommandHandlers
{
    public class LoggerCommand : IRequest<bool>
    {
        public LogLevel LogLevel { get; set; }
        public string? Category { get; set; }
        public string? Function { get; set; }
        public string? Message { get; set; }
        public string? StackTrace { get; set; }
        public object[]? LogObjects { get; set; }
    }

    public class LoggerCommandHandler : IRequestHandler<LoggerCommand, bool>
    {
        private readonly ILoggerRepository _loggerRepository;
        public LoggerCommandHandler(ILoggerRepository loggerRepository)
        {
            _loggerRepository = loggerRepository;
        }
        public async Task<bool> Handle(LoggerCommand request, CancellationToken cancellationToken)
        {
            LoggerDTO logObj = ConfigurationMapper.Mapper.Map<LoggerDTO>(request);
            var result = await _loggerRepository.LogObject(logObj);
            return result;
        }
    }
}
