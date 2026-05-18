using Application.Email.Interfaces;

namespace Application.Email
{
    public class EmailService : IEmailService
    {
        public Task SendCodeAsync(string identity, string code)
        {
            throw new NotImplementedException();
        }
    }
}
