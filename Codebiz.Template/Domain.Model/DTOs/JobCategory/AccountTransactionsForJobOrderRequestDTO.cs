using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.DTOs.JobCategory
{
    public class AccountTransactionsForJobOrderRequestDTO
    {
        public int AccountTransactionId { get; set; }
        public string ControlNo { get; set; }

        public int AccountId { get; set; }

        public string AccountNo { get; set; }
    }
}
