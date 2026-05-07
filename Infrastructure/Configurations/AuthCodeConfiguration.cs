using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public class AuthCodeConfiguration : IEntityTypeConfiguration<AuthCode>
{
    public void Configure(EntityTypeBuilder<AuthCode> builder)
    {
        builder.
            HasKey(c => c.Id);

        builder.
            Property(c => c.Code).
            IsRequired().
            HasMaxLength(10);

        builder.
            Property(c => c.ExpiryTime).
            IsRequired();

        builder.
            Property(c => c.IsUsed).
            HasDefaultValue(false);
    }
}
