using Application.Users;
using Application.Users.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DI
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IUsersService, UsersService>();
            return services;
        }
    }
}
