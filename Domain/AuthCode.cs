namespace Domain
{
    public class AuthCode
    {
        public Guid Id { get; set; }

        public string Code { get; set; } = string.Empty;

        public DateTime ExpiryTime { get; set; }

        public bool IsUsed { get; set; }


        public Guid UserId { get; set; }

        public User? User { get; set; }
    }
}
