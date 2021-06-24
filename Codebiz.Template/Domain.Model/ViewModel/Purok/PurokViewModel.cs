namespace Codebiz.Domain.Common.Model.ViewModel.Purok
{
    public class PurokViewModel
    {
        public int PurokId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int? ProvinceId { get; set; }
        public int? CityTownId { get; set; }
        public int? BarangayId { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string SortColumn { get; set; }
        public string SortOrder { get; set; }
        public string Barangay { get; set; }
    }
}
