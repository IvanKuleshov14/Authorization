using Domain;

namespace Application.Users.Interfaces
{
    public interface IUsersService
    {
        Task<User> GetByIdentityAsync(string identity, string provider);
    }
}
