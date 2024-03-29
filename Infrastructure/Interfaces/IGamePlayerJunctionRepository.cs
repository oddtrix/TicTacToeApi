using Domain.Entities;
using System.Linq.Expressions;

namespace Infrastructure.Interfaces
{
    public interface IGamePlayerJunctionRepository
    {
        void Delete(Guid id);

        GamePlayerJunction GetById(Guid id);

        IEnumerable<GamePlayerJunction> GetAll();

        GamePlayerJunction Create(GamePlayerJunction entity);

        GamePlayerJunction Update(GamePlayerJunction entity);

        GamePlayerJunction GetByIdWithInclude(Guid id, params Expression<Func<GamePlayerJunction, object>>[] includes);

        IEnumerable<GamePlayerJunction> GetAllByIdWithInclude(Guid id, string propertyName, params Expression<Func<GamePlayerJunction, object>>[] includes);
    }
}
