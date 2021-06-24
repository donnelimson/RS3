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
    public class BusinessPartnerGroupService
    {
        private readonly AccountDataContext _ctx;
        private readonly BusinessPartnerGroupRepository _bpGroupRepository;

        public BusinessPartnerGroupService(AccountDataContext ctx)
        {
            _ctx = ctx;
            _bpGroupRepository = new BusinessPartnerGroupRepository(_ctx);
        }

        public BusinessPartnerGroup Add(BusinessPartnerGroup p1) => _bpGroupRepository.Add(p1);

        public BusinessPartnerGroup SaveOrUpdate(BusinessPartnerGroup p1) => _bpGroupRepository.SaveOrUpdate(p1);

        public IQueryable<BusinessPartnerGroup> GetList(Expression<Func<BusinessPartnerGroup, bool>> q) => _bpGroupRepository.GetList(q);

        public IQueryable<BusinessPartnerGroup> GetListEx(Expression<Func<BusinessPartnerGroup, bool>> q, params string[] _includes) => _bpGroupRepository.GetListEx(q, _includes);

        public int Commit() => _bpGroupRepository.Commit();
    }
}
