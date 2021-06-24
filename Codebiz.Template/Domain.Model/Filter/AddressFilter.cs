using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.Filter
{
    public class AddressFilter
    {
        public int? MemberId { get; set; }
        public int? AccountId { get; set; }
        public string BlkAndLotNumber { get; set; }
        public string StreetName { get; set; }
        public int? SitioId { get; set; }
        public int? PurokId { get; set; }
        public int? BarangayId { get; set; }
        public int? CityTownId { get; set; }
        public int? ProvinceId { get; set; }

        public void SetAddressFilter(string blkLotNumber, int? sitioId, int? purokId, int? barangayId, int? cityTownId, int? provinceId, string streetName, int accountId)
        {
            AccountId = accountId;
            BlkAndLotNumber = blkLotNumber;
            StreetName = streetName;
            SitioId = sitioId;
            PurokId = purokId;
            BarangayId = barangayId;
            CityTownId = cityTownId;
            ProvinceId = provinceId;
        }
    }
}
