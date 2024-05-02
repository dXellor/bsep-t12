using bsep_dll.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace bsep_dll.Models.Configurations;

public class UserEntityTypeConfiguration: IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");
        builder.HasKey(u => u.Id);
        
        builder.Property(x => x.Id).HasColumnName("user_id").ValueGeneratedOnAdd();
        builder.Property(x => x.FirstName).HasColumnName("first_name").HasMaxLength(25).IsRequired();
        builder.Property(x => x.LastName).HasColumnName("last_name").HasMaxLength(25).IsRequired();
        builder.Property(x => x.Email).HasColumnName("email").HasMaxLength(255).IsRequired();
        builder.Property(x => x.Role).HasColumnName("role").HasConversion(new EnumToStringConverter<UserRoleEnum>()).IsRequired();
    }
}