using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TicTacToeApi.Contexts;

namespace TicTacToeApi.Models.Repositories
{
    public class EntityRepository<TEntity> : IEntityRepository<TEntity> where TEntity : class
    {
        private AppDomainContext context;

        private AppIdentityContext identityContext;

        public EntityRepository(AppDomainContext context, AppIdentityContext identityContext)
        {
            this.context = context;
            this.identityContext = identityContext;
        }

        public TEntity Create(TEntity entity)
        {
            this.context.Add<TEntity>(entity);
            this.context.SaveChanges();
            return entity;
        }

        public TEntity Update(TEntity entity)
        {
            this.context.Update(entity);
            this.context.SaveChanges();

            return entity;
        }

        public void Delete(Guid id)
        {
            TEntity entityToDelete = this.context.Set<TEntity>().Find(id);

            if (entityToDelete != null)
            {
                if (typeof(TEntity) == typeof(Player))
                {
                    var identityEntityToDelete = this.identityContext.Users.Find(id);
                    this.identityContext.Users.Remove(identityEntityToDelete);
                    this.identityContext.SaveChanges();
                }

                this.context.Remove(entityToDelete);
                this.context.SaveChanges();
            }
        }

        public IEnumerable<TEntity> GetAll()
        {
            return this.context.Set<TEntity>();
        }

        public TEntity GetById(Guid id)
        {
            return this.context.Find<TEntity>(id);
        }

        public TEntity GetByIdWithInclude(Guid id, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = this.context.Set<TEntity>().AsQueryable();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query.FirstOrDefault(e => EF.Property<Guid>(e, "Id") == id);
        }
    }
}
