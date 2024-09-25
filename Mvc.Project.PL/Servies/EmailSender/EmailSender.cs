using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Mvc.Project.PL.Servies.EmailSender
{
    public class EmailSender : IEmailSender
    {
        private readonly IConfiguration _configuration;

        public EmailSender(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendAsync(string from, string recipients, string subject, string body)
        {
            //var SenderEmail = _configuration["EmailSetting:SenderEmail"];
            //var SenderPassword = ;

            var SmtpClient = new SmtpClient(_configuration["EmailSetting:SmtpClientServer"], int.Parse(_configuration["EmailSetting:SmtpClientPort"]))
            {
                Credentials = new NetworkCredential(_configuration["EmailSetting:SenderEmail"], _configuration["EmailSetting:SenderPassword"]),
                EnableSsl = true
            };

            await SmtpClient.SendMailAsync(from, recipients, subject, body);
        }
    }
}
