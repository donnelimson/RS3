using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.DTOs.Banking
{
    public class BankIndexDTO
    {
        public int BankId { get; set; }
        [DisplayName("COUNTRY CODE")]
        public string CountryCode { get; set; }
        [DisplayName("BANK CODE")]
        public string BankCode { get; set; }
        [DisplayName("BANK NAME")]
        public string BankName { get; set; }
        [DisplayName("BIC/SWIFT CODE")]
        public string BICSwiftCode { get; set; }
        [DisplayName("POST OFFICE")]
        public bool IsPostOffice { get; set; }
        [DisplayName("ACCOUNT NO.")]
        public string AccountNo { get; set; }
        [DisplayName("BRANCH")]
        public string Branch { get; set; }
        [DisplayName("NEXT CHECK NO.")]
        public string NextCheckNo { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
    public class BankDetailsDTO
    {
        public int BankId { get; set; }
        public int CountryCodeId { get; set; }
        public string BankCode { get; set; }
        public string BankName { get; set; }
        public string BICSwiftCode { get; set; }
        public bool IsPostOffice { get; set; }
        public int? HouseBankAccountId { get; set; }
        public string AccountNo { get; set; }
        public string Branch { get; set; }
        public string NextCheckNo { get; set; }
    }
    public class BankLookUpDTO
    {
        public int? BankId { get; set; }
        public string BankCountry { get; set; }
        public string BankCode { get; set; }
        public string BankName { get; set; }
        public string BankAccount { get; set; }
        public string BankAccountName { get; set; }
        public string CtrlIntId { get; set; }
        public string IBAN { get; set; }
        public string BICSwiftCode { get; set; }
        public string Branch { get; set; }
    }
    public class BankForChequeDTO
    {
        public int BankId { get; set; }
        public string BankName { get; set; }
        public string Branch { get; set; }
    }
}
