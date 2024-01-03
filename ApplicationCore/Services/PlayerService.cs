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

        public List<GamePlayerJunction> History(Guid userId)
        {
            var gamePlayer = this.gamePlayerRepository.GetGamesByUserId(userId); 
            return gamePlayer;
        }
    }
}
