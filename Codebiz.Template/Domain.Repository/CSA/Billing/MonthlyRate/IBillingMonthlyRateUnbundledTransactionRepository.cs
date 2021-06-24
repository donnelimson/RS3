using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Repository;
using System.Collections.Generic;

namespace Domain.Repository.CSA.Billing
{
    public interface IBillingMonthlyRateUnbundledTransactionRepository : IRepositoryBase<BillingMonthlyRateUnbundledTransaction>   
    {
        BillingMonthlyRateUnbundledTransaction GetById(int id);
        IEnumerable<BillingMonthlyRateUnbundledTransaction> GetUnbundledTransactionByMonthlyRateId(int monthlyRateId);
        bool CheckIfUnbundledTrasactionIdExist(int? id);
    }
}
