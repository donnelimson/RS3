using ERP.Model.DTO;
using ERP.Model.Filter;
using Infrastructure.Repository.Sales;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services.Sale
{
    public interface ISaleTransactionService
    {
        IPagedList<SaleTransactionIndexDTO> Search(SaleTransactionFilter filter);
    }
    public class SaleTransactionService : ISaleTransactionService
    {
        private readonly ISaleTransactionRepository _saleTransactionRepository;
        public SaleTransactionService(ISaleTransactionRepository saleTransactionRepository)
        {
            _saleTransactionRepository = saleTransactionRepository;
        }
        public IPagedList<SaleTransactionIndexDTO> Search(SaleTransactionFilter filter)
        {
            return _saleTransactionRepository.Search(filter);
        }
    }
}
