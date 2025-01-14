using System.Net.Mail;
using System.Net;

namespace Sistema_Web_Gestao.Helper
{

    // Serviço de Email ultizando Mailtrap
    public class SendEmail
    {
        private readonly string _smtpServer;
        private readonly int _port;
        private readonly string _username;
        private readonly string _password;
        private readonly bool _enableSsl;

        public SendEmail(IConfiguration configuration)
        {
            _smtpServer = configuration["AppMailSmtp"];
            _port = 2525; 
            _username = "c85bc4ba0269bd"; 
            _password = "57eecc4cb9e881"; 
            _enableSsl = true;
        }

        public void Send(string fromEmail, string fromName, string toEmail, string subject, string body)
        {
            var smtpClient = new SmtpClient(_smtpServer)
            {
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Port = _port,
                EnableSsl = _enableSsl,
                Credentials = new NetworkCredential(_username, _password)
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(fromEmail, fromName),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            mailMessage.To.Add(toEmail);

            smtpClient.Send(mailMessage);
        }
    }
}
