using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.ViewModel.Finance
{
    public class JournalEntryViewModel
    {
        public int? FinancePeriodId { get; set; }
        public int CreatedByAppuserId { get; set; }
        public int AccountId { get; set; }
        public float TotalAmount { get; set; } = 0;
        public List<int> UnbundledTransactionIds { get; set; }
        public List<float> TotalAmounts { get; set; }
        public List<string> UnbundledTransactions { get; set; }
    }
   
}
