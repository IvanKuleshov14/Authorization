using Application.Users.Interfaces;
using Domain;

namespace Application.Users
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        public UsersService(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }
        public async Task<User> GetByIdentityAsync(string identity, string provider)
        {
            var user = await _usersRepository.GetByIdentityAsync(identity);
            if (user == null)
            {
                var userId = Guid.NewGuid();
                var userName = "Nameless User";

                user = new User(userId, userName);

                if (provider.Equals("Email", StringComparison.OrdinalIgnoreCase))
                {
                    user.Email = identity;
                }
                else if (provider.Equals("Telegram", StringComparison.OrdinalIgnoreCase))
                {
                    long tgId = long.Parse(identity);
                    user.TelegramId = tgId;
                }

                await _usersRepository.AddAsync(user);
            }

            return user;
        }
    }
}
