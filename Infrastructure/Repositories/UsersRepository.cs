using Application.Users.Interfaces;
using Domain;

namespace Infrastructure.Repositories
{
    public class UsersRepository : IUsersRepository
    {
        public async Task AddAsync(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<User?> GetByIdentityAsync(string identity)
        {
            throw new NotImplementedException();
        }
    }
}
