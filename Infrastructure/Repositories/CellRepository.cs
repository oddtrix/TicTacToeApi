using Domain.Entities;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TicTacToeApi.Contexts;

namespace Infrastructure.Repositories
{
    public class CellRepository : ICellRepository
    {
        private readonly AppDomainContext context;

        public CellRepository(AppDomainContext context)
        {
            this.context = context;
        }

        public Cell Create(Cell entity)
        {
            this.context.Add(entity);
            return entity;
        }

        public Cell Update(Cell entity)
        {
            this.context.Update(entity);

            return entity;
        }

        public void Delete(Guid id)
        {
            var entityToDelete = this.context.Cells.Find(id);

            if (entityToDelete != null)
            {
                this.context.Remove(entityToDelete);
            }
        }

        public IEnumerable<Cell> GetAll()
        {
            return this.context.Cells;
        }

        public Cell GetById(Guid id)
        {
            return this.context.Cells.Find(id);
        }

        public IEnumerable<Cell> GetAllByIdWithInclude(Guid id, string propertyName, params Expression<Func<Cell, object>>[] includes)
        {
            var query = this.context.Cells.AsQueryable();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query.Where(e => EF.Property<Guid>(e, propertyName) == id);
        }

        public Cell GetByIdWithInclude(Guid id, params Expression<Func<Cell, object>>[] includes)
        {
            var query = this.context.Cells.AsQueryable();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query.FirstOrDefault(e => EF.Property<Guid>(e, "Id") == id);
        }
    }
}
