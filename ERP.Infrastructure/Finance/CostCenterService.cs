using Codebiz.Domain.ERP.Context;
using Codebiz.Domain.ERP.Repository.Financials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.ERP.Infrastructure.Finance
{
    public class CostCenterService
    {
        private FinanceDataContext _ctx;

        private CostCenterRepository _repository;

        public CostCenterService(FinanceDataContext ctx)
        {
            _ctx = ctx;
            _repository = new CostCenterRepository(ctx);
        }
    }
}
