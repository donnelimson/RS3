using Codebiz.Domain.ERP.Context;
using System;

namespace Codebiz.Domain.ERP.Infrastructure.Finance
{
    public class FinanceDataService : IDisposable, IFinanceDataService
    {

        private Lazy<FinancialPeriodService> _lazy_FinancialPeriodService;

        private Lazy<CostCenterService> _lazy_CostCenterService;

        private Lazy<DepartmentService> _lazy_DepartmentService;

        private Lazy<ProjectService> _lazy_ProjectService;

        private Lazy<TaxGroupService> _lazy_TaxGroupService;

        private Lazy<GLAccountService> _lazy_GLAccountService;

        private Lazy<JournalEntryService> _lazy_JournalEntryService;

        private Lazy<JournalVoucherService> _lazy_JournalVoucherEntryService;

        private FinanceDataContext _ctx;

        public FinanceDataService()
        {
            _ctx = new FinanceDataContext();

            _lazy_FinancialPeriodService = new Lazy<FinancialPeriodService>(() => new FinancialPeriodService(_ctx));

            _lazy_ProjectService = new Lazy<ProjectService>(() => new ProjectService(_ctx));

            _lazy_TaxGroupService = new Lazy<TaxGroupService>(() => new TaxGroupService(_ctx));

            _lazy_CostCenterService = new Lazy<CostCenterService>(() => new CostCenterService(_ctx));

            _lazy_DepartmentService = new Lazy<DepartmentService>(() => new DepartmentService(_ctx));

            _lazy_GLAccountService = new Lazy<GLAccountService>(() => new GLAccountService(_ctx));

            _lazy_JournalEntryService = new Lazy<JournalEntryService>(() => new JournalEntryService(_ctx));

            _lazy_JournalVoucherEntryService = new Lazy<JournalVoucherService>(() => new JournalVoucherService(_ctx));

        }

        public FinancialPeriodService FinancialPeriodService { get { return _lazy_FinancialPeriodService.Value;}}

        public CostCenterService CostCenterService { get { return _lazy_CostCenterService.Value; }}

        public DepartmentService DepartmentService { get { return _lazy_DepartmentService.Value; }}

        public ProjectService ProjectService { get { return _lazy_ProjectService.Value; }}

        public TaxGroupService TaxGroupService{ get { return _lazy_TaxGroupService.Value; }}

        public GLAccountService GLAccountService{ get { return _lazy_GLAccountService.Value; }}

        public JournalEntryService JournalEntryService { get { return _lazy_JournalEntryService.Value; } }

        public JournalVoucherService JournalVoucherEntryService { get { return _lazy_JournalVoucherEntryService.Value; } }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _ctx.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
