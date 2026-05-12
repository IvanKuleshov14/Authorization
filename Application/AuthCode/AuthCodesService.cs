using Application.AuthCode.Interfaces;

namespace Application.AuthCode
{
    public class AuthCodesService : IAuthCodesService
    {
        private readonly IAuthCodesRepository _authCodesRepository;
        public AuthCodesService(IAuthCodesRepository authCodesRepository)
        {
            _authCodesRepository = authCodesRepository;
        }
        public async Task AddAsync(Guid userId, string code)
        {
            var id = Guid.NewGuid();
            var authCode = new Domain.AuthCode(
                id,
                userId,
                code,
                DateTime.UtcNow.AddMinutes(5),
                false
                );

            await _authCodesRepository.AddAsync(authCode);
        }
    }
}
