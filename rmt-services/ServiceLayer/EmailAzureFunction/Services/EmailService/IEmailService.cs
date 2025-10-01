using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ServiceLayer.DTOs;

namespace ServiceLayer.Services.EmailService
{
    public interface IEmailService
    {
        Task SendEmail(List<EmailMetaPayloadDTO> emailNotificationPayloads);
    }
}
