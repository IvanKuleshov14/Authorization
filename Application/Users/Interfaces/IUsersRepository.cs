using Domain;

namespace Application.Users.Interfaces
{
    public interface IUsersRepository
    {
        Task<User?> GetByIdentityAsync(string identity);
        Task AddAsync(User user);
    }
}
