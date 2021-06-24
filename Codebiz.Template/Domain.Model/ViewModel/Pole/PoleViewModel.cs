using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.ViewModel.Pole
{
    public class PoleViewModel
    {
        public int PoleId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int ProvinceId { get; set; }
        public int CityTownId { get; set; }
        public int BarangayId { get; set; }
        public string PoleNo { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string ItemCode { get; set; }
        public string ItemName { get; set; }
    }
}
