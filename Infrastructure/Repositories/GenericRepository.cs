using Domain.Entities;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TicTacToeApi.Contexts;

namespace Infrastructure.Repositories
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly AppDomainContext context;

        private readonly AppIdentityContext identityContext;

        public GenericRepository(AppDomainContext context)
        {
            this.context = context;
        }

        public GenericRepository(AppDomainContext context, AppIdentityContext identityContext)
        {
            this.context = context;
            this.identityContext = identityContext;
        }

        public TEntity Create(TEntity entity)
        {
            this.context.Add(entity);
            return entity;
        }

        public void Delete(Guid id)
        {
            var entityToDelete = this.GetById(id);

            if (entityToDelete != null && entityToDelete.GetType() == typeof(Player))
            {
                var identityEntityToDelete = this.identityContext.Users.Find(id);
                this.identityContext.Users.Remove(identityEntityToDelete);
                this.identityContext.SaveChanges();

                this.context.Remove(entityToDelete);
            }
            else
            {
                this.context.Remove(entityToDelete);
            }
        }

        public IEnumerable<TEntity> GetAll()
        {
            return this.context.Set<TEntity>().ToList();
        }

        public IEnumerable<TEntity> GetAllByIdWithInclude(Guid id, string propertyName, params Expression<Func<TEntity, object>>[] includes)
        {
            var query = this.context.Set<TEntity>().AsQueryable();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query.Where(e => EF.Property<Guid>(e, propertyName) == id);
        }

        public TEntity GetById(Guid id)
        {
            return this.context.Set<TEntity>().Find(id);
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

        public TEntity Update(TEntity entity)
        {
            this.context.Update(entity);
            return entity;
        }
    }
}
