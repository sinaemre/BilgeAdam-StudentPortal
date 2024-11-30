using Microsoft.AspNetCore.Identity.UI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Business.Manager.Concrete
{
    public class EmailSender : IEmailSender
    {
        private readonly SmtpClient _smtpClient;
        public EmailSender()
        {
            _smtpClient = new SmtpClient("smtp-relay.brevo.com")
            {
                Port = 443,
                Credentials = new NetworkCredential("80fe8b004@smtp-brevo.com", "OHrSMpz5hEPQbNAv"),
                EnableSsl = true,
                Timeout = 20000
            };
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress("80fe8b004@smtp-brevo.com"),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true
            };

            mailMessage.To.Add(email);

            try
            {
                await _smtpClient.SendMailAsync(mailMessage);
            }
            catch (Exception ex)
            {

                throw new InvalidOperationException("Mail gönderilemedi! => " + ex.Message);
            }
        }
    }
}
