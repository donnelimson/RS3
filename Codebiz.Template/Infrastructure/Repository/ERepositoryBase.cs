using Codebiz.Domain.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Infrastructure.Repository
{
    public interface IERepositoryBase<TEntity, TID> : IRepositoryBase<TEntity>
      where TEntity : class
    {
        void AddOrUpdate(Expression<Func<TEntity, object>> p1, params TEntity[] entities);
        IQueryable<TEntity> GetList(Expression<Func<TEntity, bool>> q);
        IQueryable<TEntity> GetListEx(Expression<Func<TEntity, bool>> q, params string[] _includes);
        IQueryable<TEntity> GetEntityList(List<Expression<Func<TEntity, bool>>> predicates, params string[] _includes);
        int UpdateBaseOpenQty(long docID, string itemCode, string objType);
        void Commit();
    }

    public abstract class ERepositoryBase<TEntity, TID> : RepositoryBase<TEntity>, IERepositoryBase<TEntity, TID>, IDisposable
         where TEntity : class
    {
        private readonly DbContext _ctx;

        private readonly DbSet<TEntity> _entitySet;

        public ERepositoryBase(DbContext ctx) : base(ctx)
        {
            _ctx = ctx;
            _entitySet = ctx.Set<TEntity>();
        }

        public override void InsertOrUpdate(TEntity entity)
        {
            PropertyInfo[] p = entity.GetType().GetProperties();

            for (var x = 0; x < p.Count(); x++)
            {
                var a = Attribute.GetCustomAttribute(p[x], typeof(KeyAttribute)) as KeyAttribute;

                if (a != null)
                {
                    TID val = (TID)p[x].GetValue(entity);

                    if (val.Equals(0))
                        this.Context.Entry(entity).State = EntityState.Added;
                    else
                        this.Context.Entry(entity).State = EntityState.Modified;

                    break;
                }
            }
        }

        public virtual void AddOrUpdate(Expression<Func<TEntity, object>> p1, params TEntity[] entities)
        {
            try
            {
                _entitySet.AddOrUpdate(p1, entities);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public virtual IQueryable<TEntity> GetList(Expression<Func<TEntity, bool>> predicate)
        {
            try
            {
                return _entitySet.Where(predicate);
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
                IQueryable<TEntity> q = _entitySet;
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

        public virtual IQueryable<TEntity> GetEntityList(List<Expression<Func<TEntity, bool>>> predicates, params string[] _includes)
        {
            try
            {
                IQueryable<TEntity> q = _entitySet;
                if (_includes != null)
                {
                    if (_includes.Count() > 0)
                        _includes.ToList().ForEach(i => q = q.Include(i));
                }
                if (predicates.Count > 0)
                    predicates.ForEach(x => q = q.Where(x));
                return q;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public virtual int UpdateBaseOpenQty(long docID, string itemCode, string objType)
        {
            try
            {
                var res = _ctx.Database.ExecuteSqlCommand(
                    "exec SP_UpdateBaseOpenQty @docID  = {0}, @itemCode = {1} ,@objType = {2}", docID, itemCode, objType);
                return res;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public virtual void Commit() => _ctx.SaveChanges();
    }
}
