using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Web.Models.ViewModels.Province
{
    public class ProvinceViewModel
    {
        [Key]
        public int ProvinceId { get; set; }

        [Required]
        [MaxLength(250)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Abbreviation { get; set; }

        [Display(Name = "Region")]
        public int RegionId { get; set; }

        public List<SelectListItem> RegionLookUp { get; set; }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}