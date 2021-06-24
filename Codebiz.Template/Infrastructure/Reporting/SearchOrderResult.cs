using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Reporting
{
    public class SearchOrderResult
    {
        public string OrderNumber { get; set; }

        public string OrderType { get; set; }

        public DateTime AuthDate { get; set; }

        public DateTime EffectivityDate { get; set; }

        public string TransactionOrderType { get; set; }

        public string PersonnelAccountNumber { get; set; }

        public string PersonnelLastName { get; set; }

        public string PersonnelFirstName { get; set; }

        public string PersonnelMiddleName { get; set; }
    }
}
