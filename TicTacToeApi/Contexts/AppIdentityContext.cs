using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TicTacToeApi.Models.Identity;

namespace TicTacToeApi.Contexts
{
    public class AppIdentityContext : IdentityDbContext<AppIdentityUser, IdentityRole<Guid>, Guid>
    {
        public AppIdentityContext(DbContextOptions<AppIdentityContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            SeedRoles(modelBuilder);
        }

        private static void SeedRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityRole<Guid>>().HasData(
                new IdentityRole<Guid>() { Id = Guid.NewGuid(), Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" },
                new IdentityRole<Guid>() { Id = Guid.NewGuid(), Name = "User", ConcurrencyStamp = "2", NormalizedName = "User" }
                );
        }
    }
}
