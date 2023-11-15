using Microsoft.EntityFrameworkCore;
using TicTacToeApi.Models.Domain;
using TicTacToeApi.Models.EntityConfiguration;
using TicTacToeApi.Models.Identity;

namespace TicTacToeApi.Contexts
{
    public class AppDomainContext : DbContext
    {
        public AppDomainContext(DbContextOptions<AppDomainContext> options) : base(options) { }

        DbSet<Player> Players { get; set; }

        DbSet<Game> Games { get; set; }

        DbSet<Chat> Chats { get; set; }
        
        DbSet<Message> Messages { get; set; }

        DbSet<Field> Fields { get; set; }

        DbSet<FieldMoves> FieldMoves { get; set; }

        DbSet<Cell> Cells { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Chat).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Field).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FieldMoves).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Game).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(GamePlayerJunction).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Message).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(Player).Assembly);
        }
    }
}
