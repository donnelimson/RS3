using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.ERP.Core.Domain
{
    public class EFRepository<TEntity, TId> : IEFRepository<TEntity, TId> where TEntity : class
    {
        DbContext _ctx;
        DbSet<TEntity> _dbSet;

        public EFRepository(DbContext ctx)
        {
            _ctx = ctx;
            _dbSet = _ctx.Set<TEntity>();
        }
        public virtual TEntity Add(TEntity p1)
        {
            try
            {
                _dbSet.Add(p1);
                //_ctx.SaveChanges();
                return p1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public virtual TEntity SaveOrUpdate(TEntity p1)
        {
            try
            {
                _dbSet.Attach(p1);
                _ctx.Entry(p1).State = EntityState.Modified;
                //_ctx.SaveChanges();
                return p1;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual void Delete(TId Id)
        {
            try
            {
                var entity = _dbSet.Find(Id);
                Delete(entity);
                //_ctx.SaveChanges();
            }
            catch (Exception ex) 
            {
                throw ex;
            }
        }

        public virtual void Delete(TEntity p1)
        {
            try
            {
                if (_ctx.Entry(p1).State == EntityState.Detached) _dbSet.Attach(p1);
                _dbSet.Remove(p1);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
 
        public virtual IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> q)
        {
            try
            {
                var res = _dbSet.Where(q);
                if (res.Count() > 0) return res;
                return default(List<TEntity>);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual TEntity GetByKey(Expression<Func<TEntity, bool>> q)
        {
            try
            {
                var res = _dbSet.Where(q);
                if (res.Count() > 0) return res.FirstOrDefault();
                return default(TEntity);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
