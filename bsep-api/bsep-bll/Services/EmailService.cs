using bsep_bll.Contracts;
using Microsoft.Extensions.Configuration;
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
    public class EmailService: IEmailService
    {
        private readonly SmtpClient _smtpClient;
        private readonly IConfiguration _configuration;

        private string ACTIVATION_MESSAGE = $@"
            <h2>Activate your account</h2>
            <p>Hi, you've created a new account. All you have to do is activate it.</p>
            <a style='font-size: 16px; text-decoration: none; display: block; color: #000;padding: 20px 25px;' href='https://localhost:8080/account/activate?token={1}'>Activate your account</a>
            ";

        private string OTP_MESSAGE = $@"
            <h2>Log in to your account</h2>
            <p>Hi, here is your one time password. It expires in 10mins</p>
            <p>otp:{0}</p>
            ";

        private string BLOCK_MESSAGE = $@"
            <h2>Your account registration has been denied</h2>
            <p>You will be unable to register for the next 3 months.</p>
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

        public void SendTest()
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress("jvujovic30@gmail.com"),
                Subject = "Oh, hi",
                Body = "<h1>Hello fren</h1>",
                IsBodyHtml = true,
            };
            mailMessage.To.Add("jelenaena.3a@gmail.com");

            _smtpClient.Send(mailMessage);
        }

        public void SendActivationMessage(string recipient, string token)
        {
            //String.Format(ACTIVATION_MESSAGE, token);

            var mailMessage = new MailMessage
            {
                From = new MailAddress(_configuration["Email:SmtpClient:Email"]),
                Subject = "Account activation",
                Body = $@"
                        <h2>Activate your account</h2>
                        <p>Hi, you've created a new account. All you have to do is activate it.</p>
                        <p>token:{token}</p>
                        <a style='font-size: 16px; text-decoration: none; display: block; color: #000;padding: 20px 25px;' href='https://localhost:8080/account/activate?token={token}'>Activate your account</a>
                        ",
                IsBodyHtml = true
            };
            mailMessage.To.Add(recipient);

            _smtpClient.Send(mailMessage);
        }        
        
        public void SendOTPMessage(string recipient, string otp)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_configuration["Email:SmtpClient:Email"]),
                Subject = "Your one time login password",
                Body = $@"
                        <h2>Log in to your account</h2>
                        <p>Hi, here is your one time password. It expires in 10mins</p>
                        <p>{otp}</p>
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
