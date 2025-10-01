using RMT.Notification.Application.HttpServices.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services.EmailService
{
    public interface IEmailService
    {
        Task SendEmail(List<EmailMetaPayloadDTO> emailNotificationPayloads);
    }
}
