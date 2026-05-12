using Application.Telegram.Interfaces;

namespace Application.Telegram
{
    public class TelegramService : ITelegramService
    {
        public Task SendCodeAsync(long identity, string code)
        {
            throw new NotImplementedException();
        }
    }
}
