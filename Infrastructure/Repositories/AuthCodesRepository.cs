using Application.AuthCode.Interfaces;
using Domain;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace Infrastructure.Repositories
{
    public class AuthCodesRepository : IAuthCodesRepository
    {
        private readonly AuthorizationDbContext _dbContext;
        public AuthCodesRepository(AuthorizationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(AuthCode authCode)
        {
            await _dbContext.AuthCodes.AddAsync(authCode);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<AuthCode?> GetLastCodeByUderIdAsync(Guid userId)
        {
            return await _dbContext.AuthCodes.Where(c => c.UserId == userId && c.IsUsed == false)
                .OrderByDescending(c => c.ExpiryTime)
                .FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(AuthCode authCode)
        {
            _dbContext.AuthCodes.Update(authCode);
            await _dbContext.SaveChangesAsync();
        }
    }
}
