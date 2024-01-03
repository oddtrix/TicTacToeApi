﻿using Domain.Entities;
using System.Linq.Expressions;

namespace ApplicationCore.Interfaces
{
    public interface IEntityService<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();

        TEntity GetById(Guid id);

        TEntity GetByIdWithInclude(Guid id, params Expression<Func<TEntity, object>>[] includes);

        List<GamePlayerJunction> GetGamesByUserId(Guid userId);

        TEntity Create(TEntity entity);

        TEntity Update(TEntity entity);

        void Delete(Guid id);
    }
}
