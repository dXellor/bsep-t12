using bsep_dll.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace bsep_dll.Models.Configurations;

public class AdvertisementConfiguration : IEntityTypeConfiguration<Advertisement>
{
    public void Configure(EntityTypeBuilder<Advertisement> builder)
    {
        builder.ToTable("advertisements");
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Id).HasColumnName("advertisement_id").ValueGeneratedOnAdd();
        builder.Property(a => a.Slogan).HasColumnName("slogan").HasMaxLength(255);
        builder.Property(a => a.Start).HasColumnName("start_date");
        builder.Property(a => a.End).HasColumnName("end_date");
        builder.Property(a => a.Description).HasColumnName("description").HasMaxLength(1000);
        
    }
}