namespace Application.AuthCode.Interfaces
{
    public interface IAuthCodesService
    {
        Task AddAsync(Guid userId, string code);
        Task<Domain.AuthCode> GetLastCodeByUserIdAsync(Guid userId);
        Task UpdateAsync(Domain.AuthCode code);
    }
}
