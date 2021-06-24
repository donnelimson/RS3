using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.Filter
{
    public class OfficeAddressFilter
    {
        public int? OfficeId { get; set; }
        public string BlkAndLotNumber { get; set; }
        public string StreetName { get; set; }
        public int? SitioId { get; set; }
        public int? PurokId { get; set; }
        public int? BarangayId { get; set; }
        public int? CityTownId { get; set; }
        public int? ProvinceId { get; set; }
    }
}
