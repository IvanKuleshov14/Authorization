using Application.Telegram.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Application.Telegram
{
    public class TelegramService : ITelegramService
    {
        private readonly TelegramBotClient _botClient;
        public TelegramService(string botToken)
        {
            _botClient = new TelegramBotClient(botToken);
        }

        public async Task SendCodeAsync(long identity, string code)
        {
            await _botClient.SendMessage(
                chatId: identity,
                text: $"Код подтверждения: {code}"
                );
        }
    }
}
