using Codebiz.Domain.ERP.Context;
using Codebiz.Domain.ERP.Model.Data;
using Codebiz.Domain.ERP.Model.Data.Financials;
using Codebiz.Domain.ERP.Repository.Financials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.ERP.Infrastructure.Finance
{
    public class TaxGroupService
    {

        private FinanceDataContext _ctx;
        private TaxGroupRepository _repository;

        public TaxGroupService(FinanceDataContext ctx)
        {
            _ctx = ctx;

            _repository = new TaxGroupRepository(_ctx);
        }

        public TaxGroup Add(TaxGroup p1) => _repository.Add(p1);

        public TaxGroup SaveOrUpdate(TaxGroup p1) => _repository.SaveOrUpdate(p1);

        public IQueryable<TaxGroup> GetList(Expression<Func<TaxGroup, bool>> q) => _repository.GetList(q);

        public IQueryable<TaxGroup> GetListEx(Expression<Func<TaxGroup, bool>> q, params string[] _includes) => _repository.GetListEx(q, _includes);

        public IQueryable<TaxGroup> GetListByPage(Paging p)
        {
            var skip = (p.Page - 1) * p.PageSize;
            var res = _repository.GetListByPage(x => x.VatCode, skip, p.PageSize);
            return res;
        }

        public int Commit() => _repository.Commit();
    }
}
