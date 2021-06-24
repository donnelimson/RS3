using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model
{
    public class ItemSales
    {
        [Key]
        public int ItemSalesId { get; set; }
        public int InvoiceNo { get; set; }
        public int MasterItemId { get; set; }
        public string LongDescription { get; set; }
        public string ShortDescription { get; set; }
        public string Department { get; set; }
        public double Cost { get; set; }
        public string Mode { get; set; }
        public DateTime Date { get; set; }
    }
}
