using ApplicationCore.Interfaces;
using System.Linq.Expressions;
using TicTacToeApi.Models.Repositories;

namespace ApplicationCore.Services
{
    public class EntityService<TEntity> : IEntityService<TEntity> where TEntity : class
    {
        private readonly IEntityRepository<TEntity> repository;

        public EntityService(IEntityRepository<TEntity> repository)
        {
            this.repository = repository;
        }

        public TEntity Create(TEntity entity)
        {
            repository.Create(entity);
            return entity;
        }

        public void Delete(Guid id)
        {
            repository.Delete(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            var entities = repository.GetAll();
            return entities;
        }

        public TEntity GetById(Guid id)
        {
            var entity = repository.GetById(id);
            return entity;
        }

        public TEntity GetByIdWithInclude(Guid id, params Expression<Func<TEntity, object>>[] includes)
        {
            return repository.GetByIdWithInclude(id, includes);
        }

        public TEntity Update(TEntity entity)
        {
            repository.Update(entity);
            return entity;
        }
    }
}
