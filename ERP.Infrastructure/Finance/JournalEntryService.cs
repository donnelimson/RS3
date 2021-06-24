using Codebiz.Domain.ERP.Context;
using Codebiz.Domain.ERP.Model.Data;
using Codebiz.Domain.ERP.Model.Data.Financials;
using Codebiz.Domain.ERP.Repository.Financials;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Codebiz.Domain.ERP.Infrastructure.Finance
{
    public class JournalEntryService
    {
        private FinanceDataContext _ctx;
        private JournalEntryRepository _repository;

        public JournalEntryService(FinanceDataContext ctx)
        {
            _ctx = ctx;

            _repository = new JournalEntryRepository(_ctx);
        }

        public JournalEntry Add(JournalEntry p1) => _repository.Add(p1);

        public JournalEntry SaveOrUpdate(JournalEntry p1) => _repository.SaveOrUpdate(p1);

        public IQueryable<JournalEntry> GetList(Expression<Func<JournalEntry, bool>> q) => _repository.GetList(q);

        public IQueryable<JournalEntry> GetListEx(Expression<Func<JournalEntry, bool>> q, params string[] _includes) => _repository.GetListEx(q, _includes);

        public IQueryable<JournalEntry> GetListByPage(Paging p)
        {
            var skip = (p.Page - 1) * p.PageSize;
            var res = _repository.GetListByPage(x => x.JournalEntryID, skip, p.PageSize);
            return res;
        }

          
        public int Commit() => _repository.Commit();

    }
}
