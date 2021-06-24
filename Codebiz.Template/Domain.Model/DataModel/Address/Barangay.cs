using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model
{
    public class Barangay : ModelBase
    {
        [Key]
        public int BarangayId { get; set; }


        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [ForeignKey("CityTown")]
        public int CityTownId { get; set; }
        public virtual CityTown CityTown { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
        public string BarangayCode { get; set; }

    }
}
