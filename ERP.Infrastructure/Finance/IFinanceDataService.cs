using Codebiz.Domain.ERP.Infrastructure.Finance;

namespace Codebiz.Domain.ERP.Infrastructure.Finance
{
    public interface IFinanceDataService
    {
        FinancialPeriodService FinancialPeriodService { get; }
        CostCenterService CostCenterService { get; }
        DepartmentService DepartmentService { get; }
        ProjectService ProjectService { get; }
        TaxGroupService TaxGroupService { get; }
        GLAccountService GLAccountService { get; }
        JournalEntryService JournalEntryService { get; }
        JournalVoucherService JournalVoucherEntryService { get; }

        void Dispose();
    }
}