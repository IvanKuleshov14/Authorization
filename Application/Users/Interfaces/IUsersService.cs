using Domain;

namespace Application.Users.Interfaces
{
    public interface IUsersService
    {
        Task<User> GetByIdentityOrCreateAsync(string identity, string provider);
        Task<User> GetByIdentityAsync(string identity);
    }
}
