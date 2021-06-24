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
    public class BusinessPartnerService
    {

        private readonly AccountDataContext _ctx;
        private readonly BusinessPartnerRepository _bpRepository;

        public BusinessPartnerService(AccountDataContext ctx)
        {
            _ctx = ctx;
            _bpRepository = new BusinessPartnerRepository(_ctx);
        }

        public BusinessPartner Add(BusinessPartner p1) => _bpRepository.Add(p1);

        public BusinessPartner SaveOrUpdate(BusinessPartner p1) => _bpRepository.SaveOrUpdate(p1);

        public IQueryable<BusinessPartner> GetList(Expression<Func<BusinessPartner, bool>> q) => _bpRepository.GetList(q);

        public IQueryable<BusinessPartner> GetListEx(Expression<Func<BusinessPartner, bool>> q, params string[] _includes) => _bpRepository.GetListEx(q, _includes);

        public BusinessPartner GetBusinessPartnerByCode(string cardCode) => _bpRepository.GetBusinessPartnerByCode(cardCode);

        public int Commit() => _bpRepository.Commit();
    }
}
