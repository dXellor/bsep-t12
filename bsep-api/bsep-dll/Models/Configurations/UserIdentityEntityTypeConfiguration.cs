using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace bsep_dll.Models.Configurations;

public class UserIdentityEntityTypeConfiguration: IEntityTypeConfiguration<UserIdentity>
{
    public void Configure(EntityTypeBuilder<UserIdentity> builder)
    {
        builder.ToTable("user_identities");
        builder.HasKey(ui => ui.Email);

        builder.Property(ui => ui.Email).HasColumnName("email").IsRequired();
        builder.Property(ui => ui.Password).HasColumnName("password").IsRequired();
        builder.Property(ui => ui.Salt).HasColumnName("salt").IsRequired();
        builder.Property(ui => ui.Iterations).HasColumnName("iterations").IsRequired();
        builder.Property(ui => ui.OutputLength).HasColumnName("output_length").IsRequired();
        builder.Property(ui => ui.RefreshToken).HasColumnName("refresh_token");
        builder.Property(ui => ui.RefreshTokenExpirationDateTime).HasColumnName("refresh_token_expires");
        builder.Property(ui => ui.TwoFaEnabled).HasColumnName("two_fa_enabled").IsRequired();
        builder.Property(ui => ui.IsAwaitingTotp).HasColumnName("awaiting_totp").IsRequired();
        builder.Property(ui => ui.TotpSecret).HasColumnName("totp_secret");

        builder.HasOne(u => u.User)
            .WithOne(ui => ui.UserIdentity)
            .HasPrincipalKey<User>(u => u.Email);
    }
}