using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace TicTacToeApi.Contexts
{
    public class AppDomainContext : DbContext
    {
        public AppDomainContext(DbContextOptions<AppDomainContext> options) : base(options) { }

        public DbSet<Player> Players { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<Chat> Chats { get; set; }
        
        public DbSet<Message> Messages { get; set; }

        public DbSet<Field> Fields { get; set; }

        public  DbSet<FieldMoves> FieldMoves { get; set; }

        public  DbSet<Cell> Cells { get; set; }

        public DbSet<GamePlayerJunction> GamePlayers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Chat).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Cell).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Field).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FieldMoves).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Game).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Message).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Player).Assembly);

            modelBuilder.Entity<GamePlayerJunction>().HasKey(gp => new { gp.PlayerId, gp.GameId }); // ??
        }
    }
}
