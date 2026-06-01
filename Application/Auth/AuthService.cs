using Domain;
using Application.Auth.Interfaces;
using Application.AuthCode.Interfaces;
using Application.Users.Interfaces;
using System.Security.Cryptography;
using Application.Email.Interfaces;
using Application.Telegram.Interfaces;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

            var user = await _usersService.GetByIdentityOrCreateAsync(identity, provider);
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

        public async Task<string> VerifyCodeAsync(string identity, string code)
        {
            var user = await _usersService.GetByIdentityAsync(identity);
            Domain.AuthCode lastCode = await _authCodesService.GetLastCodeByUserIdAsync(user.Id);

            if (DateTime.UtcNow > lastCode.ExpiryTime)
            {
                throw new Exception("Срок действия кода истек");
            }
            if(lastCode.IsUsed == true)
            {
                throw new Exception("Код уже использован");
            }
            if(lastCode.Code != code)
            {
                throw new Exception("Неверный код");
            }

            lastCode.IsUsed = true;
            await _authCodesService.UpdateAsync(lastCode);

            var token = GenerateJwtToken(user);

            return token;
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            if (!string.IsNullOrEmpty(user.Email))
            {
                claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));
            }
            if (user.TelegramId.HasValue)
            {
                claims.Add(new Claim("TgId", user.TelegramId.Value.ToString()));
            }

            var secretKey = "nvhQkkvm6llmQrdT2Vhdbfn7Qt1RvzptYq";
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: "authorization",
                audience: "client",
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
    }
}
