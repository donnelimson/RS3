using Codebiz.Domain.ERP.Context;
using ERP.Model.DataModel;
using ERP.Model.DTO;
using ERP.Model.Filter;
using Infrastructure.Utilities.QueryHelper;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repository.Sales
{
    public interface ISaleTransactionRepository : IERepositoryBase<SaleTransaction, int>
    {
        IPagedList<SaleTransactionIndexDTO> Search(SaleTransactionFilter filter);
    }
    public class SaleTransactionRepository : ERepositoryBase<SaleTransaction,int>,IDisposable, ISaleTransactionRepository
    {
        private readonly ERPDataContext _ctx;
        public SaleTransactionRepository(ERPDataContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }
        public IPagedList<SaleTransactionIndexDTO> Search(SaleTransactionFilter filter)
        {
            var data = GetData(filter);
            filter.FilteredRecordCount = data.Count();
            var dataDTO = data.Select(a => new SaleTransactionIndexDTO
            {
                Id = a.Id,
                CreatedBy = a.CreatedByAppUser.LastName+", "+a.CreatedByAppUser.FirstName,
                CreatedOn = a.CreatedOn,
                ReferenceNo = a.ReferenceNo,
                TotalCost =a.TotalCost
            });
            dataDTO = QueryHelper.Ordering(dataDTO, filter.SortColumn, filter.SortDirection != "asc", false);
            return dataDTO.ToPagedList(filter.Page, filter.PageSize);
        }
        private IQueryable<SaleTransaction> GetData(SaleTransactionFilter filter)
        {
            var data = GetAll;
            if (!string.IsNullOrEmpty(filter.ReferenceNo))
                data = data.Where(x => x.ReferenceNo.Contains(filter.ReferenceNo));
            if (!string.IsNullOrWhiteSpace(filter.CreatedBy))
            {
                filter.CreatedBy = filter.CreatedBy.Replace(",", "").Trim();
                data = data.Where(a => (a.CreatedByAppUser.LastName + " "
                + a.CreatedByAppUser.FirstName + " "
                + (a.CreatedByAppUser.MiddleName == null ? "" : a.CreatedByAppUser.MiddleName + " "))
                .Trim().Contains(filter.CreatedBy));
            }
            if (filter.CreatedOnFrom != null)
                data = data.Where(x => DbFunctions.TruncateTime(x.CreatedOn) >= filter.CreatedOnFrom && DbFunctions.TruncateTime(x.CreatedOn) <= filter.CreatedOnTo);
            return data;
        }
    }
}
