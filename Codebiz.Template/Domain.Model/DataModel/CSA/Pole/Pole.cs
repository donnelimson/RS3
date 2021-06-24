using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codebiz.Domain.Common.Model.ViewModel.Pole;

namespace Codebiz.Domain.Common.Model
{
   public class Pole: ModelBase
    {
        [Key]
        public int PoleId { get; set; }
        public string PoleNo { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }

        [ForeignKey("Province")]
        public int ProvinceId { get; set; }
        public virtual Province Province { get; set; }

        [ForeignKey("CityTown")]
        public int CityTownId { get; set; }
        public virtual CityTown CityTown { get; set; }

        [ForeignKey("Barangay")]
        public int? BarangayId { get; set; }
        public virtual Barangay Barangay { get; set; }

        public string ItemCode { get; set; }
        public string ItemName { get; set; }

        public void setPole(PoleViewModel model)
        {
            PoleId = model.PoleId;
            Code = model.Code;
            Name = model.Name;
            CityTownId = model.CityTownId;
            ProvinceId = model.ProvinceId;
            BarangayId = model.BarangayId;
            Longitude = model.Longitude;
            Latitude = model.Latitude;
            IsDeleted = false;
            ItemCode = model.ItemCode;
            ItemName = model.ItemName;
        }
    }
}
