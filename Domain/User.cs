namespace Domain
{
    public class User
    {
        public Guid Id { get; set; }

        public string? Email { get; set; }

        public long? TelegramId { get; set; }

        public string Name { get; set; } = string.Empty;


        public List<AuthCode> Codes { get; set; } = new();
    }
}
