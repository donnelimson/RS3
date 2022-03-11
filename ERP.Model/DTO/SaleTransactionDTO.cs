using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Model.DTO
{
    public class SaleTransactionIndexDTO
    {
        [DisplayName("REFERENCE NO")]
        public string ReferenceNo { get; set; }
        [DisplayName("TOTAL COST")]
        public decimal? TotalCost { get; set; } = 0;
        [DisplayName("CREATED ON")]
        public DateTime CreatedOn { get; set; }
        [DisplayName("CREATED BY")]
        public string CreatedBy { get; set; }
        public int Id { get; set; }

    }
}
