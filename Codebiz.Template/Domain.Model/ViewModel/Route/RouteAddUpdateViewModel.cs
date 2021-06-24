using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.ViewModel.Route
{
    public class RouteAddUpdateViewModel
    {
        public int RouteId { get; set; }
        public string Description { get; set; }
        public string RouteCode { get; set; }
        public string BookNo { get; set; }
        public int ProvinceId { get; set; }
        public int CityTownId { get; set; }
        public int BarangayId { get; set; }
    }
}
