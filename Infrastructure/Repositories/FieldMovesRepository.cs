using Domain.Entities;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TicTacToeApi.Contexts;

namespace Infrastructure.Repositories
{
    public class FieldMovesRepository : IFieldMovesRepository
    {
        private readonly AppDomainContext context;

        public FieldMovesRepository(AppDomainContext context)
        {
            this.context = context;
        }

        public FieldMoves Create(FieldMoves entity)
        {
            this.context.Add(entity);
            return entity;
        }

        public FieldMoves Update(FieldMoves entity)
        {
            this.context.Update(entity);

            return entity;
        }

        public void Delete(Guid id)
        {
            var entityToDelete = this.context.FieldMoves.Find(id);

            if (entityToDelete != null)
            {
                this.context.Remove(entityToDelete);
            }
        }

        public IEnumerable<FieldMoves> GetAll()
        {
            return this.context.FieldMoves;
        }

        public FieldMoves GetById(Guid id)
        {
            return this.context.FieldMoves.Find(id);
        }

        public IEnumerable<FieldMoves> GetAllByIdWithInclude(Guid id, string propertyName, params Expression<Func<FieldMoves, object>>[] includes)
        {
            var query = this.context.FieldMoves.AsQueryable();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query.Where(e => EF.Property<Guid>(e, propertyName) == id);
        }

        public FieldMoves GetByIdWithInclude(Guid id, params Expression<Func<FieldMoves, object>>[] includes)
        {
            var query = this.context.FieldMoves.AsQueryable();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query.FirstOrDefault(e => EF.Property<Guid>(e, "Id") == id);
        }
    }
}
