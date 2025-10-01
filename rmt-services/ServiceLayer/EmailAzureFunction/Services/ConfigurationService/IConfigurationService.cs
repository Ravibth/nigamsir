using Microsoft.Extensions.Logging;
using ServiceLayer.DTOs;
using ServiceLayer.DTOs.Budget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.ConfigurationService
{
    public interface IConfigurationService
    {
        Task<List<NotificationTemplateDTO>> GetNotificationTemplate(string[] type, string token);

        string TransformMessageTemplateAccordingToPayloads(string template, List<NotificationPlaceHolderDTO> payload, Dictionary<string, string> data);

        Task<Boolean> SendNotification(List<SendNotificationRequest> request, string token);

        Task<List<ProjectConfiguration>> GetConfigurationByConfigGroupType(string expertiesName, string configurationGroup, string token, ILogger _logger);
    }
}
