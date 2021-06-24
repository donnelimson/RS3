using Codebiz.Domain.ERP.Context;
using Codebiz.Domain.ERP.Repository.Financials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.ERP.Infrastructure.Finance
{
    public class DepartmentService
    {
        private FinanceDataContext _ctx;
        private DepartmentRepository _repository;
        public DepartmentService(FinanceDataContext ctx)
        {
            _ctx = ctx;
            _repository = new DepartmentRepository(_ctx);
        }
    }
}
