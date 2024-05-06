using ApplicationCore.Interfaces;
using Domain.Entities;
using Infrastructure.Interfaces;

namespace ApplicationCore.Services
{
    public class PlayerService : IPlayerService
    {
        private readonly IUnitOfWork unitOfWork;

        public PlayerService(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void Delete(Guid id)
        {
            this.unitOfWork.PlayerRepository.Delete(id);
            this.unitOfWork.Save();
        }

        public IEnumerable<Player> GetAll()
        {
            return this.unitOfWork.PlayerRepository.GetAll();
        }

        public Player GetById(Guid id)
        {
            return this.unitOfWork.PlayerRepository.GetById(id);
        }

        public (IEnumerable<GamePlayerJunction>, int) History(Guid userId, int page, int pageSize)
        {
            var count = this.unitOfWork.GamePlayerRepository.GetAllByIdWithInclude(userId, "PlayerId").Count();  
            var totalPages = (int)Math.Ceiling(count / (double)pageSize);
            var history = this.unitOfWork.GamePlayerRepository.GetAllByIdPaginationWithInclude(userId, "PlayerId", page, pageSize,  g => g.Game.Winner);
            foreach (var game in history)
            {
                game.Game.GamesPlayers = this.unitOfWork.GamePlayerRepository.GetAllByIdWithInclude(game.GameId, "GameId", p => p.Player).ToList();
            }
            
            return (history, totalPages);
        }

        public Player Update(Player player)
        {
            var updatedPlayer = this.unitOfWork.PlayerRepository.Update(player);
            this.unitOfWork.Save();
            return updatedPlayer;
        }
    }
}
