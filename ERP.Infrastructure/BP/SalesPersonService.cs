using Codebiz.Domain.ERP.Context;
using Codebiz.Domain.ERP.Model.Data.Accounts;
using Codebiz.Domain.ERP.Repository.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.ERP.Infrastructure.BP
{
    public class SalesPersonService
    {
        private readonly AccountDataContext _ctx;
        private readonly SalesPersonRepository _repository;
        public SalesPersonService(AccountDataContext ctx)
        {
            _ctx = ctx;
            _repository = new SalesPersonRepository(_ctx);
        }

        public SalesPerson Add(SalesPerson p1) => _repository.Add(p1);

        public SalesPerson SaveOrUpdate(SalesPerson p1) => _repository.SaveOrUpdate(p1);

        public IQueryable<SalesPerson> GetList(Expression<Func<SalesPerson, bool>> q) => _repository.GetList(q);

        public IQueryable<SalesPerson> GetListEx(Expression<Func<SalesPerson, bool>> q, params string[] _includes) => _repository.GetListEx(q, _includes);

        public int Commit() => _repository.Commit();

    }
}
