using Codebiz.Domain.ERP.Context;
using Codebiz.Domain.ERP.Model.Data.Financials;
using Codebiz.Domain.ERP.Repository.Financials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.ERP.Infrastructure.Finance
{
    public class FinancialPeriodService
    {

        private FinancialPeriodRepository _repository;
        private FinanceDataContext _ctx;

        public FinancialPeriodService(FinanceDataContext ctx)
        {
            _ctx = ctx;
            _repository = new FinancialPeriodRepository(ctx);

        }

        public FinancialPeriodRepository FinancialPeriodRepository
        {
            get
            {
                return _repository;
            }
        }


        public int GenerateFinancePeriod(
            string periodCode, string periodName, int noPeriod,
            SubPeriodType subPeriodType, string periodIndicator,
            DateTime? postingDateFrom, DateTime? postingDateTo,
            DateTime? dueDateFrom, DateTime? dueDateTo,
            DateTime? documentDateFrom, DateTime? documentDateTo,
            DateTime? financialYearStart, int financialYear
            )
        {
            try
            {
                var fp = new FinancialPeriod()
                {

                    PeriodCategoryCode = periodCode,
                    PeriodCategoryName = periodName,
                    FinancialYear = financialYear,
                    FinancialYearStartDate = financialYearStart,
                    FromRefDate = postingDateFrom,
                    ToRefDate = postingDateTo,
                    FromDueDate = dueDateFrom,
                    ToDueDate = dueDateTo,
                    FromTaxDate = documentDateFrom,
                    ToTaxDate = documentDateTo,
                };

                var fp_details = new List<FinancialPeriodDetail>();

                switch (subPeriodType)
                {
                    case SubPeriodType.Year:
                        fp.SubType = "Y";
                        fp.PeriodNum = 1;
                        break;
                    case SubPeriodType.Quarter:
                        fp.SubType = "Q";
                        fp.PeriodNum = 4;
                        break;
                    case SubPeriodType.Month:
                        fp.SubType = "M";
                        fp.PeriodNum = 12;
                        break;
                    case SubPeriodType.Day:
                        fp.SubType = "D";
                        fp.PeriodNum = noPeriod;
                        break;
                    default:
                        break;
                }


                for (var x = 1; x <= fp.PeriodNum; x++)
                {
                    DateTime? tPostingDateFrom;
                    DateTime? tPostingDateTo;
                    DateTime? tDueDateFrom;
                    DateTime? tDueDateTo;
                    DateTime? tDocumentDateFrom;
                    DateTime? tDocumentDateTo;

                    var fp_detail = new FinancialPeriodDetail()
                    {
                        PeriodIndicator = periodIndicator,
                        PeriodStatus = "O",
                        SubNum = x
                    };

                    if (subPeriodType == SubPeriodType.Year)
                    {
                        fp_detail.PeriodCode = $"{periodCode}";
                        fp_detail.PeriodName = $"{periodName}";

                        fp_detail.FromRefDate = postingDateFrom;
                        fp_detail.ToRefDate = postingDateTo;
                        fp_detail.FromDueDate = dueDateFrom;
                        fp_detail.ToDueDate = dueDateTo;
                        fp_detail.FromTaxDate = documentDateFrom;
                        fp_detail.ToTaxDate = documentDateTo;
                    }
                    else if (subPeriodType == SubPeriodType.Quarter)
                    {
                        fp_detail.PeriodCode = $"{periodCode}-Q{x:00}";
                        fp_detail.PeriodName = $"{periodName}-Q{x:00}";

                        tPostingDateFrom = postingDateFrom.Value.AddMonths((x-1) * 3);
                        tPostingDateTo = postingDateFrom.Value.AddMonths(x * 3).AddDays(-1);
                        tDueDateFrom = dueDateFrom.Value.AddMonths((x - 1) * 3);
                        tDueDateTo = dueDateFrom.Value.AddMonths(x * 3).AddDays(-1);
                        tDocumentDateFrom = documentDateFrom.Value.AddMonths((x - 1) * 3);
                        tDocumentDateTo = documentDateFrom.Value.AddMonths(x * 3).AddDays(-1);
 
                        fp_detail.FromRefDate = tPostingDateFrom;
                        fp_detail.ToRefDate = tPostingDateTo;
                        fp_detail.FromDueDate = tDueDateFrom;
                        fp_detail.ToDueDate = tDueDateTo;
                        fp_detail.FromTaxDate = tDocumentDateFrom;
                        fp_detail.ToTaxDate = tDocumentDateTo;
                          
                    }
                    else if (subPeriodType == SubPeriodType.Month)
                    {
                        fp_detail.PeriodCode = $"{periodCode}-{x:00}";
                        fp_detail.PeriodName = $"{periodName}-{x:00}";


                        tPostingDateFrom = postingDateFrom.Value.AddMonths(x-1);
                        tPostingDateTo = postingDateFrom.Value.AddMonths(x).AddDays(-1);
                        tDueDateFrom = dueDateFrom.Value.AddMonths(x - 1);
                        tDueDateTo = dueDateFrom.Value.AddMonths(x).AddDays(-1);
                        tDocumentDateFrom = documentDateFrom.Value.AddMonths(x - 1);
                        tDocumentDateTo = documentDateFrom.Value.AddMonths(x).AddDays(-1);

                        fp_detail.FromRefDate = tPostingDateFrom;
                        fp_detail.ToRefDate = tPostingDateTo;
                        fp_detail.FromDueDate = tDueDateFrom;
                        fp_detail.ToDueDate = tDueDateTo;
                        fp_detail.FromTaxDate = tDocumentDateFrom;
                        fp_detail.ToTaxDate = tDocumentDateTo;
                    }
                    else if (subPeriodType == SubPeriodType.Day)
                    {
                        fp_detail.PeriodCode = $"{periodCode}-{x:00}";
                        fp_detail.PeriodName = $"{periodName}-{x:00}";
                    }

                    fp.PeriodDetails.Add(fp_detail);
                }

                //TODO
                // Add Validations

                _repository.Add(fp);
                _repository.Commit();
                return 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }





        
    }
}
