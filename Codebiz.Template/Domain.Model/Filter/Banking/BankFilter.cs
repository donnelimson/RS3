using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.Filter.Banking
{
    public class BankFilter:FilterBase
    {
        public string CountryCode { get; set; }
        public string BankCode { get; set; }
        public string BankName { get; set; }
        public string BICSwiftCode { get; set; }
        public string AccountNo { get; set; }
        public string Branch { get; set; }
        public string NextCheckNo { get; set; }
    }

}
