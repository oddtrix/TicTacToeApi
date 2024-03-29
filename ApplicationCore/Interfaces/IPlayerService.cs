using Domain.Entities;

namespace ApplicationCore.Interfaces
{
    public interface IPlayerService
    {
        void Delete(Guid id);

        Player GetById(Guid id);

        Player Update(Player player);

        IEnumerable<Player> GetAll();

        IEnumerable<GamePlayerJunction> History(Guid userId);
    }
}
