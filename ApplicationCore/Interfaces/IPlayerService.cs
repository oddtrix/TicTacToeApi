using Domain.Entities;

namespace ApplicationCore.Interfaces
{
    public interface IPlayerService
    {
        IEnumerable<GamePlayerJunction> History(Guid userId);
    }
}
