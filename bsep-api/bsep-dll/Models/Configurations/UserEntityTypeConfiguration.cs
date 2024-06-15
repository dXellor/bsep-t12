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
        builder.HasAlternateKey(u => u.Email);

        builder.Property(x => x.Id).HasColumnName("user_id").ValueGeneratedOnAdd();
        builder.Property(x => x.Email).HasColumnName("email").HasMaxLength(255).IsRequired();

        builder.Property(x => x.FirstName).HasColumnName("first_name");
        builder.Property(x => x.LastName).HasColumnName("last_name");
        builder.Property(x => x.CompanyName).HasColumnName("company_name").HasMaxLength(50);
        builder.Property(x => x.CompanyPib).HasColumnName("company_pib").IsFixedLength().HasMaxLength(9);

        builder.Property(x => x.Address).HasColumnName("address").IsRequired();
        builder.Property(x => x.City).HasColumnName("city").IsRequired();
        builder.Property(x => x.Country).HasColumnName("country").IsRequired();
        builder.Property(x => x.Phone).HasColumnName("phone").IsRequired();

        builder.Property(x => x.Type).HasColumnName("type").HasConversion(new EnumToStringConverter<UserTypeEnum>()).IsRequired();
        builder.Property(x => x.Role).HasColumnName("role").HasConversion(new EnumToStringConverter<UserRoleEnum>()).IsRequired();
        builder.Property(x => x.Package).HasColumnName("package").HasConversion(new EnumToStringConverter<PackageTypeEnum>()).IsRequired();

        builder.HasIndex(u => u.Email).IsUnique();
    }
}