using Application.Users.Interfaces;
using Domain;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace Infrastructure.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        private readonly AuthorizationDbContext _dbContext;
        public UsersRepository(AuthorizationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(User user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<User?> GetByIdentityAsync(string identity)
        {
            long.TryParse(identity, out long tgId);

            return await _dbContext.Users.FirstOrDefaultAsync(
                u => (tgId != 0 && u.TelegramId == tgId) || u.Email == identity
                );
        }
    }
}
