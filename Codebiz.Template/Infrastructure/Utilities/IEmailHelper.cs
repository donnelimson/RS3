using Logging;
using Codebiz.Domain.Common.Model.Enums;
using Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Utilities
{
    public interface IEmailHelper
    {
        bool SendEmail(string message, string subject, List<MailAddress> sendToEmailAddresses, List<Attachment> attachments = null);
        bool SendEmail(string message, string subject, List<string> sendToEmailAddresses, List<string> attachments = null);
    }

    public class EmailHelper : IEmailHelper
    {
        private readonly IConfigSettingService _configSettingService;

        private  string _displayName;
        private  string _emailClientUsername;
        private  string _emailClientPassword;
        private  string _smtpHost;
        private  int _smtpPort;

        //public EmailHelper(string displayName, string emailClientUsername, string emailClientPassword, string smtpHost, int smtpPort)
        public EmailHelper(IConfigSettingService configSettingService)
        {
            _configSettingService = configSettingService;

         
        }

        public string SmtpHost
        {
            get
            {
                return _smtpHost;
            }
        }

        public int SmtpPort
        {
            get
            {
                return _smtpPort;
            }
        }

        public string Username
        {
            get
            {
                return _emailClientUsername;
            }
        }

        public string Password
        {
            get
            {
                return _emailClientPassword;
            }
        }

        public bool SendEmail(string message, string subject, List<MailAddress> sendToEmailAddresses, List<Attachment> attachments = null)
        {
            _displayName = _configSettingService.GetStringValueById((int)ConfigurationSettings.SmtpDisplayName);
            _emailClientUsername = _configSettingService.GetStringValueById((int)ConfigurationSettings.SmtpUsername);
            _emailClientPassword = _configSettingService.GetStringValueById((int)ConfigurationSettings.SmtpPassword);
            _smtpHost = _configSettingService.GetStringValueById((int)ConfigurationSettings.SmtpHost);
            _smtpPort = _configSettingService.GetInt32ValueById((int)ConfigurationSettings.SmtpPort);
            try
            {
                Logger.Info(new { Subject = subject, SendToEmailAddresses = sendToEmailAddresses, Attachments = attachments, Message = message });

                SmtpClient smtpClient = new SmtpClient(_smtpHost, _smtpPort)
                {
                    UseDefaultCredentials = false,
                    Credentials = new System.Net.NetworkCredential(_emailClientUsername, _emailClientPassword),
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    EnableSsl = true
                };


                var mail = new MailMessage
                {
                    From = new MailAddress(_emailClientUsername, _displayName),
                    Subject = subject,
                    SubjectEncoding = Encoding.UTF8,
                    Body = message,
                    BodyEncoding = Encoding.UTF8,
                    IsBodyHtml = true,
                    Priority = MailPriority.High
                };

                if (sendToEmailAddresses != null)
                {
                    foreach (var emailAddress in sendToEmailAddresses)
                    {
                        mail.To.Add(emailAddress);
                    }
                }

                if (attachments != null)
                {
                    foreach (var attachment in attachments)
                    {
                        mail.Attachments.Add(attachment);
                    }
                }

                smtpClient.Send(mail);

                Logger.Info("Email sent");

                return true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                return false;
            }
        }

        public bool SendEmail(string message, string subject, List<string> sendToEmailAddresses, List<string> attachments = null)
        {
            var _sentEmailAddesses = new List<MailAddress>();
            var _attachments = new List<Attachment>();

            if (sendToEmailAddresses != null)
            {
                _sentEmailAddesses.AddRange(sendToEmailAddresses.Select(a=> new MailAddress(a)));
            }

            if (attachments != null)
            {
                _attachments.AddRange(attachments.Select(a => new Attachment(a)));
            }

            return SendEmail(message, subject, _sentEmailAddesses, _attachments);
        }
    }
}
