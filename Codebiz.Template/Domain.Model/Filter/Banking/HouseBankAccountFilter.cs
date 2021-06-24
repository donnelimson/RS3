using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.Filter.Banking
{
    public class HouseBankAccountFilter:FilterBase
    {
        public string BankCode { get; set; }
        public string Country { get; set; }
        public string Branch { get; set; }
        public string AccountNo { get; set; }
        public string BankAccountName { get; set; }

    }
}
