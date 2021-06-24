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
    public class PaymentTermService
    {
        private readonly AccountDataContext _ctx;

        private readonly PaymentTermRepository _paymentTermRepository;

        public PaymentTermService(AccountDataContext ctx)
        {
            _ctx = ctx;
            _paymentTermRepository = new PaymentTermRepository(_ctx);
        }

        public PaymentTerm Add(PaymentTerm p1) => _paymentTermRepository.Add(p1);

        public PaymentTerm SaveOrUpdate(PaymentTerm p1) => _paymentTermRepository.SaveOrUpdate(p1);

        public IQueryable<PaymentTerm> GetList(Expression<Func<PaymentTerm, bool>> q) => _paymentTermRepository.GetList(q);

        public IQueryable<PaymentTerm> GetListEx(Expression<Func<PaymentTerm, bool>> q, params string[] _includes) => _paymentTermRepository.GetListEx(q, _includes);

        public int Commit() => _paymentTermRepository.Commit();

    }
}
