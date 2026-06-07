using Application.Email.Interfaces;
using System.Net.Mail;
using System.Threading.Tasks;

namespace Application.Email
{
    public class EmailService : IEmailService
    {
        public async Task SendCodeAsync(string identity, string code)
        {
            using var client = new SmtpClient("mailserver", 1025);

            var mailMessage = new MailMessage
            {
                From = new MailAddress("noreply@authservice.local", "Auth Service"),
                Subject = "Код подтверждения",
                Body = $"Код подтверждения: {code}"
            };

            mailMessage.To.Add(identity);

            await client.SendMailAsync(mailMessage);
        }
    }
}
