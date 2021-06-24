using System;
using System.ComponentModel;

namespace Codebiz.Domain.Common.Model.DTOs.Purok
{
    public class PurokDTO
    {
        public int PurokId { get; set; }
        public int? ProvinceId { get; set; }
        public int? CityTownId { get; set; }
        public int? BarangayId { get; set; }

        [DisplayName("CODE")]
        public string Code { get; set; }

        [DisplayName("NAME")]
        public string Name { get; set; }

        [DisplayName("BARANGAY")]
        public string Barangay { get; set; }

        [DisplayName("ACTIVE")]
        public bool IsActive { get; set; }

        [DisplayName("CREATED BY")]
        public string CreatedBy { get; set; }

        [DisplayName("CREATED DATE")]
        public DateTime? CreatedDate { get; set; }
    }
}
