using Domain.Entities;
using System.Linq.Expressions;

namespace Infrastructure.Interfaces
{
    public interface IRepository<TEntity> where TEntity : BaseEntity
    {
        void Delete(Guid id);

        TEntity GetById(Guid id);

        IEnumerable<TEntity> GetAll();

        TEntity Create(TEntity entity);

        TEntity Update(TEntity entity);

        TEntity GetByIdWithInclude(Guid id, params Expression<Func<TEntity, object>>[] includes);

        IEnumerable<TEntity> GetAllByIdWithInclude(Guid id, string propertyName, params Expression<Func<TEntity, object>>[] includes);
    }
}
