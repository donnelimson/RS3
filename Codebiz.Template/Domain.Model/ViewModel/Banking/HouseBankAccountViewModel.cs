using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.ViewModel.Banking
{
    public class HouseBankAccountAddOrUpdateViewModel
    {
        public int HouseBankAccountId { get; set; }
        public int BankId { get; set; }
        public string Branch { get; set; }
        public string AccountNo { get; set; }
        public string BankAccountName { get; set; }
        public string NextCheckNo { get; set; }
        public int? GLAccountId { get; set; }
        public int? GLAccountInterimId { get; set; }
        public string Block { get; set; }
        public string StreetNo { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string IBAN { get; set; }
        public string County { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        public string ControlKey { get; set; }
        public int? ABARoutingNumber { get; set; }
        public int? FractionalNumber { get; set; }
        public string UserNo3 { get; set; }
        public string UserNo4 { get; set; }
        public int? PaperTypeId { get; set; }
        public string MaximumLines { get; set; }
        public int? TemplateNameId { get; set; }
        public bool IsLockChecksPrinting { get; set; }


    }
}
