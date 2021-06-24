
namespace Codebiz.Domain.Common.Model.ViewModel.Barangay
{
    public class BarangayAddOrUpdateViewModel
    {
        public int BarangayId { get; set; }
        public string Name { get; set; }
        public int CityTownId { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
        public string BarangayCode { get; set; }

    }

}
