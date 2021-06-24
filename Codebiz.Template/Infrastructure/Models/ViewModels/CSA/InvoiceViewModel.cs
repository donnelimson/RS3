using Codebiz.Domain.ERP.Model.Data.ERP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models.ViewModels.CSA
{
    public class InvoiceViewModel
    {
        public int InvoiceId { get; set; }
        public int AccountId { get; set; }
        public int? PayerId { get; set; }
        public decimal VatAmount { get; set; }
        public string Transaction { get; set; }
        public decimal AmountDue { get; set; }
        
    
    }
}
