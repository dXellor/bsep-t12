using bsep_dll.Models;
using bsep_dll.Models.Configurations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace bsep_dll.Data;

public class DataContext: DbContext
{
    private IConfiguration _configuration;
    
    public DbSet<User> Users { get; init; }
    public DbSet<UserIdentity> Identities { get; init; }
    
    public DbSet<Advertisement> Advertisements { get; init; }

    public DataContext(DbContextOptions options, IConfiguration configuration) : base(options)
    {
        _configuration = configuration;
    }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_configuration["DefaultConnection:ConnectionString"]);
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new UserIdentityEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new AdvertisementConfiguration());
    }
}