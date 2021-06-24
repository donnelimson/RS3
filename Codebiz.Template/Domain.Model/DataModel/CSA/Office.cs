using Codebiz.Domain.Common.Model.DataModel.CSA;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Codebiz.Domain.Common.Model
{
    public class Office : ModelBase
    {
        public Office()
        {
            OfficeDepartments = new HashSet<OfficeDepartment>();
        }

        [Key]
        public int OfficeId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Code { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string BlkLotNo { get; set; }

        public bool IsDeleted { get; set; }

        [MaxLength(50)]
        public string Street { get; set; }

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

        [ForeignKey("Sitio")]
        public int? SitioId { get; set; }
        public virtual Sitio Sitio { get; set; }

        public bool IsMainOffice { get; set; }

        public virtual ICollection<OfficeDepartment> OfficeDepartments { get; set; }

        [NotMapped]
        public string OfficeInfo
        {
            get
            {
                return OfficeId.Equals(null) ? "" : string.Format("{0}, {1}, {2}",
                    this.Barangay.Name.ToUpper().Substring(0, 1) + this.Barangay.Name.ToLower().Substring(1, this.Barangay.Name.Length - 1),
                    this.CityTown.Name.ToUpper().Substring(0, 1) + this.CityTown.Name.ToLower().Substring(1, this.CityTown.Name.Length - 1),
                    this.Province.Name.ToUpper().Substring(0, 1) + this.Province.Name.ToLower().Substring(1, this.Province.Name.Length - 1)).ToString();
            }
        }

    }
}
