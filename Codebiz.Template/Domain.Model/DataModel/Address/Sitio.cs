using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model
{
    public class Sitio : ModelBase
    {
        [Key]
        public int SitioId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Code { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        [ForeignKey("Province")]
        public int? ProvinceId { get; set; }
        public virtual Province Province { get; set; }

        [ForeignKey("CityTown")]
        public int? CityTownId { get; set; }
        public virtual CityTown CityTown { get; set; }

        [ForeignKey("Barangay")]
        public int? BarangayId { get; set; }
        public virtual Barangay Barangay { get; set; }

        [ForeignKey("Purok")]
        public int? PurokId { get; set; }
        public virtual Purok Purok { get; set; }
    }
}
