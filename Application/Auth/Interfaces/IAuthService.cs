namespace Application.Auth.Interfaces
{
    public interface IAuthService
    {
        Task<(bool isSuccess, string Message)> SendCodeAsync(string identity, string provider);

        Task<string> VerifyCodeAsync(string identity, string code);
    }
}
