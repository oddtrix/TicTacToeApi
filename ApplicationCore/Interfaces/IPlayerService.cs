using Domain.Entities;

namespace ApplicationCore.Interfaces
{
    public interface IPlayerService
    {
        List<GamePlayerJunction> History(Guid userId);
    }
}
