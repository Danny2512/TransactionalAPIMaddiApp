using MailKit.Net.Smtp;
using MimeKit;

namespace TransactionalAPIMaddiApp.Helpers.Mail
{
    public class MailHelper : IMailHelper
    {
        private readonly IConfiguration _configuration;

        public MailHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<object> SendMail(string[] toEmails, string[] ccEmails, string subject, string body)
        {
            try
            {
                string from = _configuration["Mail:From"];
                string fromName = _configuration["Mail:FromName"];
                string smtp = _configuration["Mail:Smtp"];
                string port = _configuration["Mail:Port"];
                string password = _configuration["Mail:Password"];

                MimeMessage message = new MimeMessage();
                message.From.Add(new MailboxAddress(fromName, from));

                foreach (var toEmail in toEmails)
                {
                    message.To.Add(MailboxAddress.Parse(toEmail));
                }

                if (ccEmails != null && ccEmails.Length != 0)
                {
                    foreach (var ccEmail in ccEmails)
                    {
                        message.Cc.Add(MailboxAddress.Parse(ccEmail));
                    }
                }

                message.Subject = subject;

                BodyBuilder bodyBuilder = new BodyBuilder
                {
                    HtmlBody = body
                };
                message.Body = bodyBuilder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync(smtp, int.Parse(port), false);
                    await client.AuthenticateAsync(from, password);
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }

                return new { Rpta = "Email enviado", Cod = "0" };
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("5.1.3"))
                {
                    return new { Rpta = "Email inválido", Cod = "-1" };
                }
                else
                {
                    return new { Rpta = ex.Message.ToString(), Cod = "-1" };
                }
            }
        }
    }
}