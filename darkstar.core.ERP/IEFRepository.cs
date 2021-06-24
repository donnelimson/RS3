using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Codebiz.ERP.Core.Domain
{
    public interface IEFRepository<TEntity, TId> where TEntity : class
    {
        TEntity Add(TEntity p1);

        TEntity SaveOrUpdate(TEntity p1);

        void Delete(TId Id);

        void Delete(TEntity p1);

        IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> q);

        TEntity GetByKey(Expression<Func<TEntity, bool>> q);
        
    }
}