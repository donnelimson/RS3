using Codebiz.Domain.Common.Model.ViewModel.Barangay;
using System;
namespace Codebiz.Domain.Common.Model.DTOs.Barangay
{
    public class BarangayIndexDTO
    {
        public int BarangayId { get; set; }
        public string BarangayName { get; set; }
        public string CityTown { get; set; }
        public string CreatedBy { get; set; }
        public string Province { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool Status { get; set; }
        public string BarangayCode { get; set; }
    }
    public class GetDetailsBarangayDTO : BarangayAddOrUpdateViewModel
    {
        public int? ProvinceId { get; set; }
    }
}
