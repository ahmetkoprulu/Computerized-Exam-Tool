using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cet.Core.DataAccess;
using Cet.Core.Entities;

namespace Cet.Core.BusinessLogic
{
    public class GenericService<TEntity, TRepository> : IService<TEntity>
        where TEntity : class, IEntity, new()
        where TRepository : IRepository<TEntity>
    {
        protected readonly TRepository Repository;

        public GenericService(TRepository repository)
        {
            Repository = repository;
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter = null)
        {
            return Repository.Get(filter);
        }

        public List<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            return Repository.GetList(filter);
        }

        public List<TEntity> GetIncludedList(Expression<Func<TEntity, bool>> filter = null, params Expression<Func<TEntity, object>>[] properties)
        {
            return Repository.GetIncludedList(filter, properties);
        }

        public TEntity GetIncludedSingle(Expression<Func<TEntity, bool>> filter, params Expression<Func<TEntity, object>>[] properties)
        {
            return Repository.GetIncludedSingle(filter, properties);
        }

        public void Add(TEntity entity)
        {
            Repository.Add(entity);
        }

        public void Update(TEntity entity)
        {
            Repository.Update(entity);
        }

        public void Delete(TEntity entity)
        {
            Repository.Delete(entity);
        }
    }
}