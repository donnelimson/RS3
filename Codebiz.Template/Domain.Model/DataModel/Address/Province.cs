using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Codebiz.Domain.Common.Model
{
    public class Province : ModelBase
    {
        [Key]
        public int ProvinceId { get; set; }      

        [Required]
        [MaxLength(250)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Abbreviation { get; set; }

        [ForeignKey("Region")]
        public int? RegionId { get; set; }
        public virtual Region Region { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
