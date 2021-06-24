

namespace Codebiz.Domain.Common.Model.ViewModel.Banking
{
    public class BankAddOrUpdateModel
    {
        public int BankId { get; set; }
        public int CountryCodeId { get; set; }
        public string BankCode { get; set; }
        public string BankName { get; set; }
        public string BICSwiftCode { get;set;}
        public bool IsPostOffice { get; set; }
        public int? HouseBankAccountId { get; set; }

    }
}
