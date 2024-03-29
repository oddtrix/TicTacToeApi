using Domain.Entities;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TicTacToeApi.Contexts;

namespace Infrastructure.Repositories
{
    public class FieldRepository : IFieldRepository
    {
        private readonly AppDomainContext context;

        public FieldRepository(AppDomainContext context)
        {
            this.context = context;
        }

        public Field Create(Field entity)
        {
            this.context.Add(entity);
            return entity;
        }

        public Field Update(Field entity)
        {
            this.context.Update(entity);

            return entity;
        }

        public void Delete(Guid id)
        {
            var entityToDelete = this.context.Fields.Find(id);

            if (entityToDelete != null)
            {
                this.context.Remove(entityToDelete);
            }
        }

        public IEnumerable<Field> GetAll()
        {
            return this.context.Fields;
        }

        public Field GetById(Guid id)
        {
            return this.context.Fields.Find(id);
        }

        public IEnumerable<Field> GetAllByIdWithInclude(Guid id, string propertyName, params Expression<Func<Field, object>>[] includes)
        {
            var query = this.context.Fields.AsQueryable();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query.Where(e => EF.Property<Guid>(e, propertyName) == id);
        }

        public Field GetByIdWithInclude(Guid id, params Expression<Func<Field, object>>[] includes)
        {
            var query = this.context.Fields.AsQueryable();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query.FirstOrDefault(e => EF.Property<Guid>(e, "Id") == id);
        }
    }
}
