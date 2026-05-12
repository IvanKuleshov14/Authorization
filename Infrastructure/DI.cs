using Application.Auth.Interfaces;
using Application.Users.Interfaces;
using Application.AuthCode.Interfaces;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class DI
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IAuthCodesRepository, AuthCodesRepository>();
            return services;
        }
    }
}
