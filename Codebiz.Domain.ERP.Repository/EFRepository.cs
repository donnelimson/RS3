using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.ERP.Repository
{
    public class EFRepository<TEntity, TID> : IDisposable
        where TEntity : class
    {

        DbContext _ctx;

        DbSet<TEntity> _dbset;

        public virtual DbContext DataContext { get { return _ctx; } }

        public virtual DbSet<TEntity> DBSet { get { return _dbset; } }

        public virtual Task<int> CommitAsync()
        {

            try
            {
                return _ctx.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual int Commit()
        {
            try
            {
                return _ctx.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public EFRepository(DbContext ctx)
        {
            _ctx = ctx;
            _dbset = _ctx.Set<TEntity>();
        }

        public virtual TEntity Add(TEntity p1)
        {
            try
            {
                var res = _dbset.Add(p1);
                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual IEnumerable<TEntity> AddRange(IEnumerable<TEntity> p1)
        {
            try
            {
                var res = _dbset.AddRange(p1.ToList());
                return res;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual TEntity SaveOrUpdate(TEntity p1)
        {
            try
            {
                //if (_ctx.Entry(p1).State == EntityState.Detached)
                //{
                var res = _dbset.Attach(p1);
                _ctx.Entry(p1).State = EntityState.Modified;
                return res;
                //}
                // return default(TEntity);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Delete(TID Id)
        {
            var entity = _dbset.Find(Id);
            Delete(entity);
            _ctx.SaveChanges();
        }

        public void Delete(TEntity p1)
        {
            if (_ctx.Entry(p1).State == EntityState.Detached) _dbset.Attach(p1);
            _dbset.Remove(p1);
        }

        public virtual IQueryable<TEntity> GetList(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                return _dbset.Where(predicate);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual IQueryable<TEntity> GetListByPage(Expression<Func<TEntity, TID>> order, int recordOffset, int recordCount, params string[] _includes)
        {
            try
            {
                IQueryable<TEntity> q = _dbset;
                if (_includes != null)
                {
                    if (_includes.Count() > 0)
                        _includes.ToList().ForEach(i => q = q.Include(i));
                }
                return q.OrderBy(order).Skip(recordOffset).Take(recordCount).AsNoTracking<TEntity>();
            }
            catch (Exception)
            {
                throw;
            }

        }

        public virtual IQueryable<TEntity> GetListEx(Expression<Func<TEntity, bool>> predicate, params string[] _includes)
        {
            try
            {
                IQueryable<TEntity> q = _dbset;
                if (_includes != null)
                {
                    if (_includes.Count() > 0)
                        _includes.ToList().ForEach(i => q = q.Include(i));
                }

                return q.Where(predicate);
            }
            catch (Exception)
            {
                throw;
            }

        }

        #region IDisposable Support

        private bool disposedValue = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _dbset = null;
                    _ctx.Dispose();
                    _ctx = null;
                }
                disposedValue = true;
            }
        }

        public virtual void Dispose() => Dispose(true);

        #endregion
    }
}
