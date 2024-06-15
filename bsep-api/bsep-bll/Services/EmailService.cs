using bsep_bll.Contracts;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace bsep_bll.Services
{
    public class EmailService : IEmailService
    {
        private readonly SmtpClient _smtpClient;
        private readonly IConfiguration _configuration;

        private string PASSWORD_RESET = $@"
            <h2>Reset your password</h2>
            <p>Hi, visit the link below to reset your password</p>
            <a style='font-size: 16px; text-decoration: none; display: block; color: #000;padding: 20px 25px;' href='http://localhost:4200/signup/?action=rp&user={0}&token={1}'>Activate your account</a>
            ";

        private string BLOCK_MESSAGE = $@"
            <h2>Your account has been blocked</h2>
            ";

        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
            _smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = int.Parse(_configuration["Email:SmtpClient:Port"]),
                Credentials = new NetworkCredential(_configuration["Email:SmtpClient:Email"], _configuration["Email:SmtpClient:Password"]),
                EnableSsl = bool.Parse(_configuration["Email:SmtpClient:EnableSsl"]),
            };
        }

        public void SendPasswordResetMessage(string recipient, string token)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_configuration["Email:SmtpClient:Email"]),
                Subject = "Reset your password",
                Body = $@"
                        <h2>Reset your password</h2>
                        <p>Hi, visit the link below to reset your password</p>
                        <a style='font-size: 16px; text-decoration: none; display: block; color: #000;padding: 20px 25px;' href='http://localhost:4200/signup/?action=rp&user={recipient}&token={token}'>Activate your account</a>
                        ",
                IsBodyHtml = true
            };
            mailMessage.To.Add(recipient);

            _smtpClient.Send(mailMessage);
        }

        public void SendBlockMessage(string recipient)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_configuration["Email:SmtpClient:Email"]),
                Subject = "Account registration denied",
                Body = BLOCK_MESSAGE,
                IsBodyHtml = true
            };
            mailMessage.To.Add(recipient);

            _smtpClient.Send(mailMessage);
        }

        public void SendMessage(string recipient, string subject, string body, bool isBodyHtml = true)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_configuration["Email:SmtpClient:Email"]),
                Subject = subject,
                Body = body,
                IsBodyHtml = isBodyHtml
            };
            mailMessage.To.Add(recipient);

            _smtpClient.Send(mailMessage);
        }
    }
}