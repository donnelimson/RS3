using Codebiz.Domain.Common.Model.ViewModel.Route;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Codebiz.Domain.Common.Model
{
    public class Route : ModelBase
    {

        [Key]
        public int RouteId { get; set; }

        [MaxLength(300)]
        public string Description { get; set; }

        [Required]
        [MaxLength(50)]
        public string Code { get; set; }

        public string BookNo { get; set; }

        public bool IsDeleted { get; set; }

        [ForeignKey("Province")]
        public int ProvinceId { get; set; }
        public virtual Province Province { get; set; }

        [ForeignKey("CityTown")]
        public int CityTownId { get; set; }
        public virtual CityTown CityTown { get; set; }

        [ForeignKey("Barangay")]
        public int BarangayId { get; set; }
        public virtual Barangay Barangay { get; set; }
        public void setRoute(RouteAddUpdateViewModel model)
        {
            Description = model.Description;
            Code = model.RouteCode.Length <=3 ? model.RouteCode.PadLeft(4,'0') : model.RouteCode;
            BookNo = model.BookNo;
            ProvinceId = model.ProvinceId;
            CityTownId = model.CityTownId;
            BarangayId = model.BarangayId;
        }
    }

}
