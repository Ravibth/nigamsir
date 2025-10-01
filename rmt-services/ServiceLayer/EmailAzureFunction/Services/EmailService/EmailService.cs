using Azure;
using Azure.Communication.Email;
using Azure.Core;
using Azure.Core.Pipeline;
using Azure.Identity;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ServiceLayer.Constants;
using ServiceLayer.DTOs;
using ServiceLayer.Services.ConfigurationService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Threading.Tasks;

namespace ServiceLayer.Services.EmailService
{
    public class EmailService : IEmailService
    {
        private readonly ILogger<NotificationSubscription> _logger;

        public EmailService(ILogger<NotificationSubscription> log)
        {
            this._logger = log;
        }

        public async Task SendEmail(List<EmailMetaPayloadDTO> emailNotificationPayloads)
        {
            //var emailClient = new EmailClient(Environment.GetEnvironmentVariable("EmailServiceConnectionString"));
            var emailClient = GetEmailClient();

            string fromEmailAddress = Environment.GetEnvironmentVariable("MailFromAddress");

            foreach (var emailNotificationItem in emailNotificationPayloads)
            {
                List<EmailAddress> emailAddresses = new List<EmailAddress>();
                foreach (var item in emailNotificationItem.to)
                {
                    if (!string.IsNullOrEmpty(item))
                        emailAddresses.Add(new EmailAddress(item));
                }

                string mailAdditionalAddress = Environment.GetEnvironmentVariable("MailAdditionalAddress");

                _logger.LogInformation($"SendEmail-{mailAdditionalAddress}");

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
                emailContent.Html = emailNotificationItem.body;

                if (emailAddresses.Count > 0)
                {
                    EmailRecipients recipients = new EmailRecipients(emailAddresses);
                    EmailMessage emailMessage = new EmailMessage(fromEmailAddress, recipients, emailContent);

                    _logger.LogInformation($"SendEmail--SendAsync-Start");

                    await emailClient.SendAsync(Azure.WaitUntil.Completed, emailMessage);

                    _logger.LogInformation($"SendEmail-SendAsync-End");
                }
                else
                {
                    _logger.LogInformation($"SendEmail-To EmailAddress field is empty or invalid values");
                }
            }
        }

        public EmailClient GetEmailClient()
        {
            EmailClient emailClient;

            string commServiceConnectionMethod = Environment.GetEnvironmentVariable("CommServiceConnectionMethod");
            Console.WriteLine(commServiceConnectionMethod);

            _logger.LogInformation($"GetEmailClient-Start-{commServiceConnectionMethod}");

            if (commServiceConnectionMethod == "AD")
            {
                //ServiceBusConnection using AD authentication
                var clientId = Environment.GetEnvironmentVariable("SBClientId");
                var clientSecret = Environment.GetEnvironmentVariable("SBClientSecret");
                var tenatId = Environment.GetEnvironmentVariable("SBTenantId");
                // Create a TokenCredential using client credentials
                var credential = new ClientSecretCredential(tenatId, clientId, clientSecret);

                ChainedTokenCredential credential1 = new(credential);

                //TokenCredential tokenCredential = new DefaultAzureCredential();
                //tokenCredential = new DefaultAzureCredential();

                emailClient = new EmailClient(new Uri(Environment.GetEnvironmentVariable("EmailServiceURL")), credential1, null);

            }
            else
            {
                emailClient = new EmailClient(Environment.GetEnvironmentVariable("EmailServiceConnectionString"));
            }

            _logger.LogInformation($"GetEmailClient-End");

            return emailClient;
        }

    }
}
