using Application.AuthCode.Interfaces;
using Domain;

namespace Infrastructure.Repositories
{
    public class AuthCodesRepository : IAuthCodesRepository
    {
        public async Task AddAsync(AuthCode authCode)
        {
            throw new NotImplementedException();
        }
    }
}
