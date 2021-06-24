namespace Codebiz.Domain.Common.Model.ViewModel.Sitio
{
    public class SitioViewModel
    {
        public int SitioId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int? ProvinceId { get; set; }
        public int? CityTownId { get; set; }
        public int? BarangayId { get; set; }
        public int? PurokId { get; set; }
    }
}
