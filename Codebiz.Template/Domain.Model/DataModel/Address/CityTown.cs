using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Codebiz.Domain.Common.Model
{
    public class CityTown : ModelBase
    {
        [Key]
        public int CityTownId { get; set; }      

        [Required]
        [MaxLength(250)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Abbreviation { get; set; }


        
        [MaxLength(6)]
        public string ZipCode { get; set; }

        [ForeignKey("Province")]
        public int? ProvinceId { get; set; }
        public virtual Province Province { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
        public ICollection<Office> Offices { get; set; }
        public string CityTownCode { get; set; }
    }
}
