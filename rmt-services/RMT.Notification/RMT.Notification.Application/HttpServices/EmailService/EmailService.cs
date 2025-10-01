using Azure.Communication.Email;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using RMT.Notification.Application.Handlers.CommandHandlers;
using RMT.Notification.Application.HttpServices.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;
using static RMT.Notification.Application.Constants.Constants;

namespace ServiceLayer.Services.EmailService
{
    public class EmailService : IEmailService
    {
        public readonly IConfiguration _configuration;
        private readonly ILogger<EmailService> _logger;
        public EmailService(IConfiguration configuration , ILogger<EmailService> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task SendEmail(List<EmailMetaPayloadDTO> emailNotificationPayloads)
        {
            _logger.LogInformation("--NOTIFICATION_SERVICE--SendEmail--{0}", JsonConvert.SerializeObject(emailNotificationPayloads));
            //var emailClient = new EmailClient(Environment.GetEnvironmentVariable("EmailServiceConnectionString"));
            string emailClientString = Convert.ToString(_configuration.GetSection("EmailServiceConnectionString").Value);
            var emailClient = new EmailClient(emailClientString);
            string fromEmailAddress = Convert.ToString(_configuration.GetSection("MailFromAddress").Value);

            string notificationSystemAdminEmailId = _configuration.GetSection(EnvVariblesAppsetting.MicroserviceApiSettings).GetSection(EnvVariblesAppsetting.SystemAdminEmailId).Value;
            string notificationSystemAdminDisplayName = _configuration.GetSection(EnvVariblesAppsetting.MicroserviceApiSettings).GetSection(EnvVariblesAppsetting.SystemAdminDisplayName).Value;

            foreach (var emailNotificationItem in emailNotificationPayloads)
            {
                List<EmailAddress> emailAddresses = new List<EmailAddress>();
                List<EmailAddress> emailAddressesCC = new List<EmailAddress>();
                try
                {
                    foreach (var item in emailNotificationItem.to)
                    {
                        if (!string.IsNullOrEmpty(item))
                            emailAddresses.Add(new EmailAddress(item));
                    }
                    foreach (var item in emailNotificationItem.cc)
                    {
                        emailAddressesCC.Add(new EmailAddress(item));
                    }

                    string mailAdditionalAddress = Convert.ToString(_configuration.GetSection("MicroserviceApiSettings").GetSection("MailAdditionalAddress").Value);

                    if (!string.IsNullOrWhiteSpace(mailAdditionalAddress))
                    {
                        var additionalAddress = mailAdditionalAddress.Split(";");
                        foreach (var email in additionalAddress)
                        {
                            if (!string.IsNullOrEmpty(email))
                                emailAddresses.Add(new EmailAddress(email));
                        }
                    }

                    //TODO remove later**************************
                    // emailAddresses.Add(new EmailAddress(""));
                    // emailAddresses.Add(new EmailAddress(""));
                    //*******************************************
                    emailAddresses = emailAddresses.DistinctBy(m => m.Address).ToList();
                    EmailContent emailContent = new EmailContent(String.Join(" ", emailNotificationItem.subject.Split("_")));

                    if (!string.IsNullOrEmpty(notificationSystemAdminEmailId)
                        && emailNotificationItem.body.ToLower().Contains(notificationSystemAdminEmailId.ToLower()))
                    {
                        emailNotificationItem.body = emailNotificationItem.body.Replace(notificationSystemAdminEmailId, notificationSystemAdminDisplayName);
                    }

                    emailContent.Html = emailNotificationItem.body;
                    if (emailAddresses.Count > 0)
                    {
                        EmailRecipients recipients = new EmailRecipients(emailAddresses, emailAddressesCC);
                        EmailMessage emailMessage = new EmailMessage(fromEmailAddress, recipients, emailContent);

                        await emailClient.SendAsync(Azure.WaitUntil.Completed, emailMessage);
                    }
                    else
                    {
                        Console.Write("email address empty");
                    }
                }
                catch (Exception ex)
                {
                    Console.Write("Exception occur in SendEmail {0} ", ex);
                    _logger.LogInformation("--NOTIFICATION_SERVICE--SendEmail--EX--{0}", JsonConvert.SerializeObject(ex));

                    throw new Exception($"Exception occur in SendEmail to{String.Join(",", emailAddresses.Select(m => m.Address).ToList())}:- {ex}");
                }
            }
        }
    }
}
