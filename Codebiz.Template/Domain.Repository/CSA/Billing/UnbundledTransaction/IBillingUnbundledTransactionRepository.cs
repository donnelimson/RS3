using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Common.Model.DataModel.CSA.Billing.UnbundledTransaction;
using Codebiz.Domain.Common.Model.DTOs;
using Codebiz.Domain.Common.Model.DTOs.ExportToExcel;
using Codebiz.Domain.Common.Model.Filter;
using PagedList;
using System.Collections.Generic;

namespace Codebiz.Domain.Repository
{
    public interface IBillingUnbundledTransactionRepository : IRepositoryBase<BillingUnbundledTransaction>
    {
        BillingUnbundledTransaction GetById(int billingUnbundledTransactionId);
        bool IsBillingUnbundledTransactionCodeExist(string code, int id, int categoryId);
        bool IsBillingUnbundledTransactionNameExist(string name, int id, int categoryId);

        IPagedList<BillingUnbundledTransactionDTO> SearchBillingUnbundledTransaction(BillingUnbundledTransactionFilter billingUnbundledTransactionFilter);
        IPagedList<BillingUnbundledTransactionDTO> SearchBillingUnbundledTransactionModal(BillingUnbundledTransactionFilter billingUnbundledTransactionFilter, int category);
        IEnumerable<BillingUnbundledTransactionToExcel> GetDataForExportingToExcel(BillingUnbundledTransactionFilter billingUnbundledTransactionFilter);
        IPagedList<BillingUnbundledTransactionLookUpDTO> GetUnbundledTransactionLookUp(LookUpFilter filter);
        BillingUnbundledTransactionLookUpDTO GetUnbundledTransactionByCode(string code,bool? forVat, int id);
        List<BillingUnbundledTransactionIdAndNameDTO> GetUnbundledTransactionForVAT(bool forVat);
        BillingUnbundledTransactionIdAndDisplayedNameDTO GetUnbundledTransactionByIdForLookUp(int id);
        List<BillingUnbundledTransactionDetailsIdAndDisplayedNameDTO> GetUnbundledTransactionForVatDetailsById(int id);
        List<BillingUnbundledTransactionDetailsIdAndDisplayedNameDTO> GetUnbundledTransactionForDiscountDetailsById(int id);
        void DeleteUnbundledTransactionForVatDetail(BillingUnbundledTransactionForVatDetails entity);
        void DeleteUnbundledTransactionForDiscountDetail(BillingUnbundledTransactionForDiscountDetails entity);
        BillingUnbundledTransaction GetByCategoryAndDisplayedName(string category, string displayedName);

        IPagedList<BillingUnbundledTransactionForBPDTO> GetAllBillingUnbundledTransactionForBP(LookUpFilter filter);
        List<BillingUnbundledTransactionForBPDTO> GetAllBillingUnbundledTransactionForBPOnLoad();
        List<LookUpDTO> GetAllBillingUnbundledTransactionLookup();
        void InsertVatDetails(BillingUnbundledTransactionForVatDetails vatEntity);
        void InsertDiscountetails(BillingUnbundledTransactionForDiscountDetails discountEntity);
        bool CheckIfHasDetails(int id);
    }
}
