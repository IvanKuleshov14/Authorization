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

        public async Task<Domain.AuthCode> GetLastCodeByUserIdAsync(Guid userId)
        {
            var lastCode = await _authCodesRepository.GetLastCodeByUderIdAsync(userId);
            if(lastCode == null)
            {
                throw new Exception("Код не найден");
            }
            return lastCode;
        }

        public async Task UpdateAsync(Domain.AuthCode code)
        {
            await _authCodesRepository.UpdateAsync(code);
        }
    }
}
