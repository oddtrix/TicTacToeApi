using ApplicationCore.Interfaces;
using Domain.Entities;
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
            this.repository.Create(entity);
            return entity;
        }

        public void Delete(Guid id)
        {
            this.repository.Delete(id);
        }

        public IEnumerable<TEntity> GetAll()
        {
            var entities = this.repository.GetAll();
            return entities;
        }

        public TEntity GetById(Guid id)
        {
            var entity = this.repository.GetById(id);
            return entity;
        }

        public List<GamePlayerJunction> GetGamesByUserId(Guid userId)
        {
            var entities = this.repository.GetGamesByUserId(userId);
            return entities;
        }

        public TEntity GetByIdWithInclude(Guid id, params Expression<Func<TEntity, object>>[] includes)
        {
            return this.repository.GetByIdWithInclude(id, includes);
        }

        public TEntity Update(TEntity entity)
        {
            this.repository.Update(entity);
            return entity;
        }
    }
}
