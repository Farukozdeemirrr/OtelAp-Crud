using DataAccess.Abstract;
using Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        public IQueryable<TEntity> GetAll(OtelDbContext context)
        {
            return context.Set<TEntity>();
        }

        public TEntity Add(OtelDbContext context, TEntity entity)
        {
            context.Set<TEntity>().Add(entity);
            return entity;
        }

        public TEntity Update(OtelDbContext context, TEntity entity)
        {
            context.Set<TEntity>().Update(entity);
            return entity;
        }

        public void Delete(OtelDbContext context, long id)
        {
            var deletedEntity = GetById(context, id);
            context.Set<TEntity>().Remove(deletedEntity);
        }

        public TEntity GetById(OtelDbContext context, long id)
        {

            return context.Set<TEntity>().Find(id);

        }
    }
}
