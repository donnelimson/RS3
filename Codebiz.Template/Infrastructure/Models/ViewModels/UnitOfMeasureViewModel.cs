using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models.ViewModels
{
    public class UnitOfMeasureViewModel
    {
        public int UnitOfMeasureId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Length { get; set; }
        public int? LengthUoMId { get; set; }
        public decimal Width { get; set; }
        public int? WidthUoMId { get; set; }
        public decimal Height { get; set; }
        public int? HeightUoMId { get; set; }
        public decimal Volume { get; set; }
        public int? VolumeUoMId { get; set; }
        public decimal Weight { get; set; }
        public int? WeightUoMId { get; set; }


        //public List<UnitOfMeasureCodeNameViewModel> UnitOfMeasures { get; set; }
    }
    public class UnitOfMeasureCodeNameViewModel
    {
        public int UnitOfMeasureId { get; set; }
        public bool IsDeleted { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class UnitOfMeasureComputationViewModel
    {
        public decimal Length { get; set; }
        public int LengthUoMId { get; set; }
        public decimal Width { get; set; }
        public int WidthUoMId { get; set; }
        public decimal Height { get; set; }
        public int HeightUoMId { get; set; }
        public int VolumeUoMId { get; set; }
    }
}
