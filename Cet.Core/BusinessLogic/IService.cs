using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Cet.Core.Entities;

namespace Cet.Core.BusinessLogic
{
    public interface IService<TEntity> where TEntity : class, IEntity, new() 
    {
        TEntity Get(Expression<Func<TEntity, bool>> filter = null);
        List<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null);

        List<TEntity> GetIncludedList(Expression<Func<TEntity, bool>> filter = null,
            params Expression<Func<TEntity, object>>[] properties);
        TEntity GetIncludedSingle(Expression<Func<TEntity, bool>> filter,
            params Expression<Func<TEntity, object>>[] properties);

        void Add(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
