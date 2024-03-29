using Domain.Entities;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TicTacToeApi.Contexts;

namespace Infrastructure.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly AppDomainContext context;

        private readonly AppIdentityContext identityContext;

        public PlayerRepository(AppDomainContext context, AppIdentityContext identityContext)
        {
            this.context = context;
            this.identityContext = identityContext;
        }

        public Player Create(Player entity)
        {
            this.context.Add(entity);
            return entity;
        }

        public Player Update(Player entity)
        {
            this.context.Update(entity);

            return entity;
        }

        public void Delete(Guid id)
        {
            var entityToDelete = this.context.Players.Find(id);

            if (entityToDelete != null)
            {
                var identityEntityToDelete = this.identityContext.Users.Find(id);
                this.identityContext.Users.Remove(identityEntityToDelete);
                this.identityContext.SaveChanges();

                this.context.Remove(entityToDelete);
            }
        }

        public IEnumerable<Player> GetAll()
        {
            return this.context.Players;
        }

        public Player GetById(Guid id)
        {
            return this.context.Players.Find(id);
        }

        public IEnumerable<Player> GetAllByIdWithInclude(Guid id, string propertyName, params Expression<Func<Player, object>>[] includes)
        {
            var query = this.context.Players.AsQueryable();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query.Where(e => EF.Property<Guid>(e, propertyName) == id);
        }

        public Player GetByIdWithInclude(Guid id, params Expression<Func<Player, object>>[] includes)
        {
            var query = this.context.Players.AsQueryable();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query.FirstOrDefault(e => EF.Property<Guid>(e, "Id") == id);
        }
    }
}
