using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.DTOs.AccountTransaction
{
    public class AccountTransactionDetailsDTO
    {
        public int MemberId { get; set; }   
        public string ConsumerName { get; set; }
        public string ConsumerNo { get; set; }
        public string AccountNo { get; set; }
        public string MeterNo { get; set; }
        public string ConsumerType { get; set; }
        public string Address { get; set; }
        public string Near { get; set; }
        public decimal? CurrentReading { get; set; }
        public string TransactionName { get; set; }
        public int? AccountTransactionId { get; set; }
    }
}
