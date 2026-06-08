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
                DateTime.UtcNow.AddMinutes(3),
                false
                );

            await _authCodesRepository.AddAsync(authCode);
        }

        public async Task<Domain.AuthCode> GetLastCodeByUserIdAsync(Guid userId)
        {
            var lastCode = await _authCodesRepository.GetLastCodeByUderIdAsync(userId);
            return lastCode!;
        }

        public async Task UpdateAsync(Domain.AuthCode code)
        {
            await _authCodesRepository.UpdateAsync(code);
        }
    }
}
