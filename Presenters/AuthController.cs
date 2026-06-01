using Application.Auth.Interfaces;
using Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Presenters
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> SendCode([FromBody] SendCodeDTO request)
        {
            var result = await _authService.SendCodeAsync(request.Identity, request.Provider);
            if (result)
            {
                return Ok("Код отправлен");
            }
            else
            {
                return BadRequest("Не удалось отправить код");
            }
        }
    }
}
