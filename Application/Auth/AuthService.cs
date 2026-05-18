using Domain;
using Application.Auth.Interfaces;
using Application.AuthCode.Interfaces;
using Application.Users.Interfaces;
using System.Security.Cryptography;
using Application.Email.Interfaces;
using Application.Telegram.Interfaces;

namespace Application.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IUsersService _usersService;
        private readonly IAuthCodesService _authCodesService;
        private readonly IEmailService _emailService;
        private readonly ITelegramService _telegramService;
        public AuthService(
            IUsersService usersService,
            IAuthCodesService authCodesService,
            IEmailService emailService,
            ITelegramService telegramService
            )
        {
            _usersService = usersService;
            _authCodesService = authCodesService;
            _emailService = emailService;
            _telegramService = telegramService;
        }

        public async Task<bool> SendCodeAsync(string identity, string provider)
        {
            if(provider.Equals("Telegram", StringComparison.OrdinalIgnoreCase))
            {
                if (!long.TryParse(identity, out _))
                {
                    return false;
                }
            }

            var user = await _usersService.GetByIdentityAsync(identity, provider);
            if(user == null)
            {
                return false;
            }

            string code = RandomNumberGenerator.GetInt32(100000, 1000000).ToString();

            await _authCodesService.AddAsync(user.Id, code);

            if (provider.Equals("Email", StringComparison.OrdinalIgnoreCase))
            {
                await _emailService.SendCodeAsync(identity, code);
                return true;
            }
            else if (provider.Equals("Telegram", StringComparison.OrdinalIgnoreCase))
            {
                long tgId = long.Parse(identity);
                await _telegramService.SendCodeAsync(tgId, code);
                return true;
            }

            return false;
        }

        public Task<string> VerifyCodeAsync(string identity, string code)
        {
            throw new NotImplementedException();
        }
    }
}
