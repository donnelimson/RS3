using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Codebiz.Domain.Common.Model.ViewModel.CityTown
{
    public class CityTownViewModel
    {
        [Key]
        public int CityTownId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Abbreviation { get; set; }

        [Required]
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }

        [Required]
        [Display(Name = "Region")]
        public int RegionId { get; set; }

        [Required]
        [Display(Name = "Province")]
        public int ProvinceId { get; set; }

        public List<SelectListItem> RegionLookup { get; set; }
        public List<SelectListItem> ProvinceLookup { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
    public class CityTownAddUpdateViewModel
    {
        public int CityTownId { get; set; }
        public string Name { get; set; }
        public int? ProvinceId { get; set; }
        public string ZipCode { get; set; }
        public double Latitude { get; set; }
        public string abbv { get; set; }
        public double Longitude { get; set; }
        public string CityTownCode { get; set; }
    }
}
