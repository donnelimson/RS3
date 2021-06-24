using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Common.Model.DTOs.BillingCategory;
using Codebiz.Domain.Common.Model.Filter;
using Codebiz.Domain.Repository;
using PagedList;
using System.Collections.Generic;

namespace Domain.Repository
{
    public interface IBillingUnbundledTransactionCategoryRepository : IRepositoryBase<BillingUnbundledTransactionCategory>
    {
        BillingUnbundledTransactionCategory GetById(int id);
        BillingUnbundledTransactionCategory GetByCode(string code);
        List<BillingUnbundledTransactionCategory> GetAllNotSelected(List<int> ids);
        IPagedList<CategoryTableDTO> SearchCategories(CategoryFilter categoryFilter);
        IEnumerable<BillingUnbundledTransactionCategory> GetAllCategories();
        IEnumerable<CategoryTableDTO> GetAllCategoriesForAddOrUpdate(CategoryFilter categoryFilter);

        IEnumerable<CategoryTableDTO> GetDataForExportingToExcel(CategoryFilter categoryFilter);

        bool CheckIfIsInUse(int id);
    }
}
