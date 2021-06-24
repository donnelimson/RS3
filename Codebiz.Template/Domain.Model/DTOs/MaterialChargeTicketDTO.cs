using Codebiz.Domain.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.DTOs
{
    public class MTCViewModel
    {
        public string SerialNumber { get; set; }

        public string StockDescription { get; set; }
        public string MTCNumber { get; set; }
        public int ConnectionOrderId{ get; set; }
        public int MaterialChargeTicketId{ get; set; }
        public int Quantity { get; set; }

        public bool IsActive { get; set; }
        public int ModifiedByAppUserId { get; set; }
        public DateTime ModifiedOn { get; set; }


    }
}
