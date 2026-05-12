namespace Application.Email.Interfaces
{
    public interface IEmailService
    {
        Task SendCodeAsync(string identity, string code);
    }
}
