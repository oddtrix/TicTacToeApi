using Domain.Entities;
using System.Linq.Expressions;

namespace TicTacToeApi.Models.Repositories
{
    public interface IEntityRepository<TEntity>
    {
        IEnumerable<TEntity> GetAll();

        TEntity GetById(Guid id);

        TEntity GetByIdWithInclude(Guid id, params Expression<Func<TEntity, object>>[] includes);

        List<GamePlayerJunction> GetGamesByUserId(Guid userId);

        TEntity Create(TEntity entity);

        TEntity Update(TEntity entity);

        void Delete(Guid id);
    }
}
