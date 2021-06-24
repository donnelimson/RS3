using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Common.Model.DTOs;
using Codebiz.Domain.Common.Model.DTOs.ExportToExcel;
using Codebiz.Domain.Common.Model.Filter;
using Codebiz.Domain.Repository;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Repository
{
    public interface IBillingMonthlyRateRepository : IRepositoryBase<BillingMonthlyRate>
    {
        List<BillingMonthlyRate> GetByBillingPeriod(string billingPeriod);
        IPagedList<MonthlyRateDTO> SearchMonthlyRate(MonthlyRateFilter filter);
        bool CheckIfConsumerClassExist(int consumerClassId);
        bool CheckIfConsumerClassAndCreatedOnExist(int? consumerClassId, int id, string billingPeriod);
        BillingMonthlyRate GetById(int id);
        IQueryable<BillingMonthlyRate> GetByConsumerClassId(int id);
        MonthlyRateDetailDTO GetDetailsById(int id);
        GetUnbundledNameByCodeDTO GetUnbundledNameByCode(string code, int? categoryId);
        IEnumerable<BillingMonthlyRateToExcel> GetDataForExportingToExcel(MonthlyRateFilter filter);
        void DeleteBillingMonthlyRateUnbundledTransaction(BillingMonthlyRateUnbundledTransaction entity);
        IQueryable<BillingMonthlyRate> GetByConsumerClassIdAndBillingPeriod(int id, string billingPeriod);
    }
}
