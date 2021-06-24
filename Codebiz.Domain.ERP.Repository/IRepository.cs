using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.ERP.Repository
{
    public interface IRepository<T>
    {
        T Add(T p1);
        T SaveOrUpdate(T p1);
        IQueryable<T> GetList(Expression<Func<T, bool>> q);
        IQueryable<T> GetListEx(Expression<Func<T, bool>> q, params string[] _includes);
        int Commit();
    }
}
