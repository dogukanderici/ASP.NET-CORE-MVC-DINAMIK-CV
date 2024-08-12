using Core.DataAccess.Abstract;
using Core.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Core.DataAccess.Concrete.EntityFramework
{
    public class EfRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, new()
        where TContext : DbContext, new()
    {

        public List<TEntity> GetDataList(Expression<Func<TEntity, bool>> filter = null, Expression<Func<TEntity, decimal>> orderBy = null)
        {
            using (TContext context = new TContext())
            {
                IQueryable<TEntity> query = context.Set<TEntity>();

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                if (orderBy != null)
                {
                    query = query.OrderByDescending(orderBy).Take(5);
                }

                return query.ToList();
            }
        }

        public TEntity GetData(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext context = new TContext())
            {
                IQueryable<TEntity> query = context.Set<TEntity>();

                if (filter != null)
                {
                    query = query.Where(filter);
                }

                return query.SingleOrDefault();
            }
        }

        public void AddData(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void DeleteData(int id)
        {
            using (TContext context = new TContext())
            {
                TEntity result = context.Set<TEntity>().Find(id);


                if (result != null)
                {
                    var addedEntity = context.Entry(result);
                    addedEntity.State = EntityState.Deleted;
                    context.SaveChanges();
                }
            }
        }

        public void UpdateData(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }
        }
    }
}
