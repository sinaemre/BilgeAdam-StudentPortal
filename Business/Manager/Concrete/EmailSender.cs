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
            _smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("snabkr7010@gmail.com", "geil qtea jghw ccqn"),
                EnableSsl = true,
                Timeout = 10000
            };
        }
        public async Task SendEmailAsync(string email, string subject, string htmlMessage)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress("snabkr7010@gmail.com"),
                Subject = subject,
                Body = htmlMessage,
                IsBodyHtml = true
            };

            mailMessage.To.Add("arslanfatih3606@gmail.com");

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
