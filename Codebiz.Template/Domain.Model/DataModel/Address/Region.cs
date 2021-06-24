using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Codebiz.Domain.Common.Model
{
    public class Region : ModelBase
    {
        [Key]
        public int RegionId { get; set; }

        [Required]
        [MaxLength(250)]
        [Index("NameIndex", IsUnique = true)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Abbreviation { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }
}
