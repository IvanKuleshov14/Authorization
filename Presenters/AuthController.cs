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

        /// <summary>
        /// Запрос кода
        /// </summary>
        /// <param name="request">
        /// <b> Данные запроса </b>
        /// <br/> <b>identity:</b> <br/>
        /// Адрес электронной почты или TelegramId пользователя<br/>
        /// <br/> <b>provider:</b> <br/>
        /// Email - для отправки кода на почту <br/>
        /// Telegram - для отправки кода в телеграм <br/>
        /// *регистр не учитывается
        /// </param>
        /// <returns></returns>
        [HttpPost("send-code")]
        public async Task<IActionResult> SendCode([FromBody] SendCodeDTO request)
        {
            var result = await _authService.SendCodeAsync(request.Identity, request.Provider);
            if (result.isSuccess)
            {
                return Ok($"{result.Message}");
            }
            else
            {
                return BadRequest($"{result.Message}");
            }
        }

        /// <summary>
        /// Проверка кода
        /// </summary>
        /// <param name="request">
        /// <b> Данные для запроса </b>
        /// <br/> <b> identity: </b> <br/>
        /// Адрес электронной почты или TelegramId пользователя, указанный при запросе кода <br/>
        /// <br/> <b> code: </b> <br/>
        /// Шестизначный код, присланный на электронную почту или в телеграм <br/>
        /// </param>
        /// <returns></returns>
        [HttpPost("verify-code")]
        public async Task<IActionResult> VerifyCode([FromBody] VerifyCodeDTO request)
        {
            if(request == null || string.IsNullOrEmpty(request.Code))
            {
                return BadRequest("Данные запроса не могут быть пустыми");
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
