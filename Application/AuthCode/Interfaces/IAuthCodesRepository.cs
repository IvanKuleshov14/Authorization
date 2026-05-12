using Domain;

namespace Application.AuthCode.Interfaces
{
    public interface IAuthCodesRepository
    {
        Task AddAsync(Domain.AuthCode authCode);
    }
}
