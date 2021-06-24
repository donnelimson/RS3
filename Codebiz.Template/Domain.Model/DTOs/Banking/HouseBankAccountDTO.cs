using Codebiz.Domain.Common.Model.ViewModel.Banking;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.DTOs.Banking
{
    public class HouseBankAccountIndexDTO
    {
        public int HouseBankAccountId { get; set; }
        [DisplayName("BANK CODE")]
        public string BankCode { get; set; }
        [DisplayName("COUNTRY")]
        public string Country { get; set; }
        [DisplayName("BRANCH")]
        public string Branch { get; set; }
        [DisplayName("ACCOUNT NO.")]
        public string AccountNo { get; set; }
        [DisplayName("BANK ACCOUNT NAME")]
        public string BankAccountName { get; set; }
        [DisplayName("ACTION BY")]
        public string ActionBy { get; set; }
        [DisplayName("ACTION ON")]
        public DateTime? ActionOn { get; set; }

    }
    public class HouseBankAccountDetailsDTO: HouseBankAccountAddOrUpdateViewModel
    {
        public string BICSwiftCode { get; set; }
        public string GLAccountNo { get; set; }
        public string GLAccountInterimNo { get; set; }
        public string BankCode { get; set; }
        public string Country { get; set; }
    }
    public class HouseBankAccountLookupDTO
    {
        public int HouseBankAccountId { get; set; }
        public string BankCode { get; set; }
        public string Country { get; set; }
        public string Branch { get; set; }
        public string HouseBankAccountNo { get; set; }
        public string NextCheckNo { get; set; }
    }
}
