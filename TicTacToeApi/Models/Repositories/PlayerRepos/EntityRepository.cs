using TicTacToeApi.Contexts;
using TicTacToeApi.Models.Domain;
using TicTacToeApi.Models.Identity;

namespace TicTacToeApi.Models.Repositories.PlayerRepos
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

        public void Delete(Guid id)
        {
            TEntity entityToDelete = this.context.Set<TEntity>().Find(id);

            if (entityToDelete != null)
            {
                if (typeof(TEntity) == typeof(Player))
                {
                    var enityToDeleteInIdentity = this.identityContext.Users
                        .FirstOrDefault(user => user.Email == (this.context.Set<Player>()
                        .Find(id)).Email);
                    this.identityContext.Users.Remove((AppIdentityUser)enityToDeleteInIdentity);
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

        public TEntity Update(TEntity entity)
        {
            this.context.Update<TEntity>(entity);
            this.context.SaveChanges();

            return entity;
        }
    }
}
