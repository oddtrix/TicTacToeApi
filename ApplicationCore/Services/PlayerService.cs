using ApplicationCore.Interfaces;
using Domain.Entities;

namespace ApplicationCore.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IEntityService<GamePlayerJunction> gamePlayerRepository;

        public PlayerService(IEntityService<GamePlayerJunction> gamePlayerRepository)
        {
            this.gamePlayerRepository = gamePlayerRepository;
        }

        public IEnumerable<GamePlayerJunction> History(Guid userId)
        {
            var history = this.gamePlayerRepository.GetAllByIdWithInclude(userId, "PlayerId", g => g.Game.Winner);
            foreach (var game in history)
            {
                game.Game.GamesPlayers = this.gamePlayerRepository.GetAllByIdWithInclude(game.GameId, "GameId", p => p.Player).ToList();
            }
            
            return history;
        }
    }
}
