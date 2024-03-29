using Domain.Entities;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TicTacToeApi.Contexts;

namespace Infrastructure.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly AppDomainContext context;

        public MessageRepository(AppDomainContext context)
        {
            this.context = context;
        }

        public Message Create(Message entity)
        {
            this.context.Add(entity);
            return entity;
        }

        public Message Update(Message entity)
        {
            this.context.Update(entity);

            return entity;
        }

        public void Delete(Guid id)
        {
            var entityToDelete = this.context.Messages.Find(id);

            if (entityToDelete != null)
            {
                this.context.Remove(entityToDelete);
            }
        }

        public IEnumerable<Message> GetAll()
        {
            return this.context.Messages;
        }

        public Message GetById(Guid id)
        {
            return this.context.Messages.Find(id);
        }

        public IEnumerable<Message> GetAllByIdWithInclude(Guid id, string propertyName, params Expression<Func<Message, object>>[] includes)
        {
            var query = this.context.Messages.AsQueryable();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query.Where(e => EF.Property<Guid>(e, propertyName) == id);
        }

        public Message GetByIdWithInclude(Guid id, params Expression<Func<Message, object>>[] includes)
        {
            var query = this.context.Messages.AsQueryable();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query.FirstOrDefault(e => EF.Property<Guid>(e, "Id") == id);
        }
    }
}
