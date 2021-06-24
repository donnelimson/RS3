using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.ViewModel
{
    public class CustomerGroupViewModel
    {
        public int CustomerGroupId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public List<CustomerGroupDetailsViewModel> CGDetails{ get;set; }
        public bool WithElectricalBill { get; set; }
    }
    public class CustomerGroupDetailsViewModel
    {
        public int Id { get; set; }
        public string CreditCode { get; set; }
        public string CreditName { get; set; }
        public string DebitCode { get; set; }
        public string DebitName { get; set; }
        public string Name { get; set; }
        public int? BillingUnbundledTransactionId { get; set; }
        public bool IsActive { get; set; }
    }
}
