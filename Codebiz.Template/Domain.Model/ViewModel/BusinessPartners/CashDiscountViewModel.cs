using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.ViewModel.BusinessPartners
{
    public class CashDiscountViewModel
    {
        public int CashDiscountId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int? CashDiscountBasedOnId { get; set; }
        public bool ByDate { get; set; }
        public bool IsFreight { get; set; }
        public bool IsSalesTax { get; set; }
        public List<CashDiscountDaysViewModel> CashDiscountDays { get; set; }
        public List<CashDiscountByDateViewModel> CashDiscountByDate { get; set; }
    }

    public class CashDiscountDaysViewModel
    {
        public int CashDiscountDayId { get; set; }
        public int CashDiscountDays { get; set; }
        public string Discount { get; set; }
    }

    public class CashDiscountByDateViewModel
    {
        public int CashDiscountByDateId { get; set; }
        public int Day { get; set; }
        public int Month { get; set; }
        public string Discount { get; set; }
    }

}
