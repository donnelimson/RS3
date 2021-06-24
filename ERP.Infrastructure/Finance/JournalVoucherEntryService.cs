using Codebiz.Domain.ERP.Context;
using Codebiz.Domain.ERP.Model.Data;
using Codebiz.Domain.ERP.Model.Data.Financials;
using Codebiz.Domain.ERP.Repository.Financials;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Codebiz.Domain.ERP.Infrastructure.Finance
{
    public class JournalVoucherService
    {
        private FinanceDataContext _ctx;

        private JournalVoucherRepository _repository;

        public JournalVoucherService(FinanceDataContext ctx)
        {
            _ctx = ctx;

            _repository = new JournalVoucherRepository(_ctx);
        }

        public JournalVoucher Add(JournalVoucher p1) => _repository.Add(p1);

        public JournalVoucher SaveOrUpdate(JournalVoucher p1) => _repository.SaveOrUpdate(p1);

        public IQueryable<JournalVoucher> GetList(Expression<Func<JournalVoucher, bool>> q) => _repository.GetList(q);

        public IQueryable<JournalVoucher> GetListEx(Expression<Func<JournalVoucher, bool>> q, params string[] _includes) => _repository.GetListEx(q, _includes);

        public IQueryable<JournalVoucher> GetListByPage(Paging p)
        {
            var skip = (p.Page - 1) * p.PageSize;
            var res = _repository.GetListByPage(x => x.JournalVoucherID, skip, p.PageSize);
            return res;
        }
        
        public int Commit() => _repository.Commit();

    }
}
