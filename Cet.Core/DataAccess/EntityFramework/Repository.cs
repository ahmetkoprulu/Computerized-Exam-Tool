using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cet.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cet.Core.DataAccess.EntityFramework
{
    public class Repository<TEntity, TContext> : IRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
        public void Add(TEntity entity)
        {
            using (var context = new TContext())
            {
                context.Add(entity);
                context.SaveChanges();
            }
        }

        public void Delete(TEntity entity)
        {
            using (var context = new TContext())
            {
                var deletedEntityEntry = context.Entry(entity);
                deletedEntityEntry.State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            using (var context = new TContext())
            {
                return context.Set<TEntity>().SingleOrDefault(filter);
            }
        }

        public TEntity GetIncludedSingle(Expression<Func<TEntity, bool>> filter,
            params Expression<Func<TEntity, object>>[] properties)
        {
            using (var context = new TContext())
            {
                IQueryable<TEntity> dbQuery = context.Set<TEntity>();

                foreach (var navigationProperty in properties)
                    dbQuery = dbQuery.Include<TEntity, object>(navigationProperty);

                return dbQuery.AsNoTracking().SingleOrDefault(filter);
            }
        }

        public List<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            using (var context = new TContext())
            {
                return filter == null
                    ? context.Set<TEntity>().ToList()
                    : context.Set<TEntity>().Where(filter).ToList();
            }
        }

        public List<TEntity> GetIncludedList(Expression<Func<TEntity, bool>> filter = null,
            params Expression<Func<TEntity, object>>[] properties)
        {
            using (var context = new TContext())
            {
                IQueryable<TEntity> dbQuery = context.Set<TEntity>();

                foreach (var navigationProperty in properties)
                    dbQuery = dbQuery.Include<TEntity, object>(navigationProperty);

                return filter == null
                    ? dbQuery.AsNoTracking().ToList()
                    : dbQuery.Where(filter).AsNoTracking().ToList();
            }
        }

        public void Update(TEntity entity)
        {
            using (var context = new TContext())
            {
                var updatedEntry = context.Entry(entity);
                updatedEntry.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}