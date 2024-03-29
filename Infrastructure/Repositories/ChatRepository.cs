using Domain.Entities;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using TicTacToeApi.Contexts;

namespace Infrastructure.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly AppDomainContext context;

        public ChatRepository(AppDomainContext context)
        {
            this.context = context;
        }

        public Chat Create(Chat entity)
        {
            this.context.Add(entity);
            return entity;
        }

        public Chat Update(Chat entity)
        {
            this.context.Update(entity);

            return entity;
        }

        public void Delete(Guid id)
        {
            var entityToDelete = this.context.Chats.Find(id);

            if (entityToDelete != null)
            {
                this.context.Remove(entityToDelete);
            }
        }

        public IEnumerable<Chat> GetAll()
        {
            return this.context.Chats;
        }

        public Chat GetById(Guid id)
        {
            return this.context.Chats.Find(id);
        }

        public IEnumerable<Chat> GetAllByIdWithInclude(Guid id, string propertyName, params Expression<Func<Chat, object>>[] includes)
        {
            var query = this.context.Chats.AsQueryable();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query.Where(e => EF.Property<Guid>(e, propertyName) == id);
        }

        public Chat GetByIdWithInclude(Guid id, params Expression<Func<Chat, object>>[] includes)
        {
            var query = this.context.Chats.AsQueryable();

            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            return query.FirstOrDefault(e => EF.Property<Guid>(e, "Id") == id);
        }
    }
}
