using Codebiz.Domain.ERP.Model.Data.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.ERP.Infrastructure.BP.DTO
{
    public class BusinessPartnerDTO : BusinessPartnerBase
    {
        public string CardCode { get; set; }
        public ICollection<BusinessPartnerAddressDTO> BPAddresses { get; set; }
        public ICollection<BusinessPartnerContactPersonDTO> BPContactPersons { get; set; }
    }

    public class BusinessPartnerAddressDTO : BusinessPartnerAddressBase
    {
        public string AddressCode { get; set; }
    }

    public class BusinessPartnerContactPersonDTO : BusinessPartnerContactPersonBase
    {
        public int BusinessPartnerContactPersonID { get; set; }
    }
}
