using Application.Auth;
using Application.Auth.Interfaces;
using Application.AuthCode;
using Application.AuthCode.Interfaces;
using Application.Email;
using Application.Email.Interfaces;
using Application.Telegram;
using Application.Telegram.Interfaces;
using Application.Users;
using Application.Users.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DI
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, string telegramToken)
        {
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAuthCodesService, AuthCodesService>();
            services.AddScoped<IEmailService, EmailService>();

            services.AddScoped<ITelegramService>(sp => new TelegramService(telegramToken));
            return services;
        }
    }
}
