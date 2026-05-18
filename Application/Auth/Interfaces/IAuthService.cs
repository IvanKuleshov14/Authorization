namespace Application.Auth.Interfaces
{
    public interface IAuthService
    {
        Task<bool> SendCodeAsync(string identity, string provider);

        Task<string> VerifyCodeAsync(string identity, string code);
    }
}
