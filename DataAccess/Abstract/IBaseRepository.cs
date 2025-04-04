using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
       IQueryable<TEntity> GetAll(OtelDbContext context);

       TEntity Add(OtelDbContext context, TEntity entity);

       TEntity Update(OtelDbContext context, TEntity entity);

       void Delete(OtelDbContext context, long id);

       TEntity GetById(OtelDbContext context, long id);
    }
}
