using Domain.Entities;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TicTacToeApi.Contexts;

namespace Infrastructure.Repositories
{
    public class GamePlayerJunctionRepository : IGamePlayerJunctionRepository
    {
        private readonly AppDomainContext context;

        public GamePlayerJunctionRepository(AppDomainContext context)
        {
            this.context = context;
        }

        public GamePlayerJunction Create(GamePlayerJunction entity)
        {
            this.context.Add(entity);
            return entity;
        }

        public GamePlayerJunction Update(GamePlayerJunction entity)
        {
            this.context.Update(entity);

            return entity;
        }

        public void Delete(Guid id)
        {
            var entityToDelete = this.context.GamePlayers.Find(id);

            if (entityToDelete != null)
            {
                this.context.Remove(entityToDelete);
            }
        }

        public IQueryable<GamePlayerJunction> GetAll()
        {
            return this.context.GamePlayers;
        }

        public GamePlayerJunction GetById(Guid id)
        {
            return this.context.GamePlayers.Find(id);
        }

        public IEnumerable<GamePlayerJunction> GetAllByIdWithInclude(Guid id, string propertyName, params Expression<Func<GamePlayerJunction, object>>[] includes)
        {
            var query = this.context.GamePlayers.AsQueryable();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query.Where(e => EF.Property<Guid>(e, propertyName) == id);
        }

        public GamePlayerJunction GetByIdWithInclude(Guid id, params Expression<Func<GamePlayerJunction, object>>[] includes)
        {
            var query = this.context.GamePlayers.AsQueryable();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query.FirstOrDefault(e => EF.Property<Guid>(e, "Id") == id);
        }

        public IEnumerable<GamePlayerJunction> GetAllByIdPaginationWithInclude(Guid id, string propertyName, int page, int pageSize, params Expression<Func<GamePlayerJunction, object>>[] includes)
        {
            var query = this.context.GamePlayers.AsQueryable().Skip((page - 1) * pageSize).Take(pageSize);

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query.Where(e => EF.Property<Guid>(e, propertyName) == id);
        }

        IEnumerable<GamePlayerJunction> IRepository<GamePlayerJunction>.GetAll()
        {
            return this.context.GamePlayers.ToList();
        }
    }
}
