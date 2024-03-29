using Domain.Entities;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TicTacToeApi.Contexts;

namespace Infrastructure.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly AppDomainContext context;

        public GameRepository(AppDomainContext context)
        {
            this.context = context;
        }

        public Game Create(Game entity)
        {
            this.context.Add(entity);
            return entity;
        }

        public Game Update(Game entity)
        {
            this.context.Update(entity);

            return entity;
        }

        public void Delete(Guid id)
        {
            var entityToDelete = this.context.Games.Find(id);

            if (entityToDelete != null)
            {
                this.context.Remove(entityToDelete);
            }
        }

        public IEnumerable<Game> GetAll()
        {
            return this.context.Games;
        }

        public Game GetById(Guid id)
        {
            return this.context.Games.Find(id);
        }

        public IEnumerable<Game> GetAllByIdWithInclude(Guid id, string propertyName, params Expression<Func<Game, object>>[] includes)
        {
            var query = this.context.Games.AsQueryable();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query.Where(e => EF.Property<Guid>(e, propertyName) == id);
        }

        public Game GetByIdWithInclude(Guid id, params Expression<Func<Game, object>>[] includes)
        {
            var query = this.context.Games.AsQueryable();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query.FirstOrDefault(e => EF.Property<Guid>(e, "Id") == id);
        }
    }
}
