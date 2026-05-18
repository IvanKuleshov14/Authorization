namespace Application.AuthCode.Interfaces
{
    public interface IAuthCodesService
    {
        Task AddAsync(Guid userId, string code);
    }
}
