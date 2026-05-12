namespace Application.Telegram.Interfaces
{
    public interface ITelegramService
    {
        Task SendCodeAsync(long identity, string code);
    }
}
