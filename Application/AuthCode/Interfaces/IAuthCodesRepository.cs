using Domain;

namespace Application.AuthCode.Interfaces
{
    public interface IAuthCodesRepository
    {
        Task AddAsync(Domain.AuthCode authCode);
        Task<Domain.AuthCode?> GetLastCodeByUderIdAsync(Guid userId);
        Task UpdateAsync(Domain.AuthCode authCode);
    }
}
