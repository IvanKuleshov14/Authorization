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

        [HttpPost]
        public async Task<IActionResult> VerifyCode([FromBody] VerifyCodeDTO request)
        {
            if(request == null || string.IsNullOrEmpty(request.Code))
            {
                return BadRequest("Данныез запроса не могут быть пустыми");
            }

            try
            {
                var token = await _authService.VerifyCodeAsync(request.Identity, request.Code);
                return Ok(token);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
