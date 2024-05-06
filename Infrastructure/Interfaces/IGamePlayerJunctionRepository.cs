using Domain.Entities;
using System.Linq.Expressions;

namespace Infrastructure.Interfaces
{
    public interface IGamePlayerJunctionRepository : IRepository<GamePlayerJunction>
    {
        IQueryable<GamePlayerJunction> GetAllAsQueryable();

        IEnumerable<GamePlayerJunction> GetAllByIdPaginationWithInclude(Guid id, string propertyName, int page, int pageSize, params Expression<Func<GamePlayerJunction, object>>[] includes);
    }
}
