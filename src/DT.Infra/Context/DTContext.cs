using DT.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace DT.Infra.Context
{
    public class DTContext : IdentityDbContext
    {
        private readonly string _connString = "Server=localhost;Port=5432;Username=postgres;Password=changeme;Database=DeliverIt";
        private static readonly ILoggerFactory _logger = LoggerFactory.Create(x => x.AddConsole());

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityUser>(b =>
            {
                b.ToTable("User");
            });

            modelBuilder.Entity<IdentityUserClaim<string>>(b =>
            {
                b.ToTable("UserClaim");
            });

            modelBuilder.Entity<IdentityUserLogin<string>>(b =>
            {
                b.ToTable("UserLogin");
            });

            modelBuilder.Entity<IdentityUserToken<string>>(b =>
            {
                b.ToTable("UserToken");
            });

            modelBuilder.Entity<IdentityRole>(b =>
            {
                b.ToTable("Role");
            });

            modelBuilder.Entity<IdentityRoleClaim<string>>(b =>
            {
                b.ToTable("RoleClaim");
            });

            modelBuilder.Entity<IdentityUserRole<string>>(b =>
            {
                b.ToTable("UserRole");
            });

            modelBuilder.Entity<Bill>().HasQueryFilter(p => !p.Removed);
            modelBuilder.Entity<Interest>().HasQueryFilter(p => !p.Removed);

            modelBuilder.Seed();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
               .UseNpgsql(_connString,
               sqlOptions => sqlOptions.EnableRetryOnFailure(3));

            optionsBuilder.UseLoggerFactory(_logger)
                .EnableSensitiveDataLogging();

            base.OnConfiguring(optionsBuilder);
        }
    }
}
