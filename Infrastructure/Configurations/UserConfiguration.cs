using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.
                HasKey(u => u.Id);

            builder.
                HasIndex(u => u.Email).
                IsUnique();

            builder.
                HasIndex(u => u.TelegramId).
                IsUnique();

            builder.
                Property(u => u.Name).
                IsRequired().
                HasMaxLength(100);

            builder.
                HasMany(u => u.Codes).
                WithOne(c => c.User).
                HasForeignKey(c => c.UserId).
                OnDelete(DeleteBehavior.Cascade);
        }
    }
}
