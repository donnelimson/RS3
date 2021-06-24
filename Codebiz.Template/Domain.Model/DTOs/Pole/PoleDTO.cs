using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.DTOs.Pole
{
    public class PoleDTO
    {
        public int PoleId { get; set; }
        public string PoleNo { get; set; }
        public string Code { get; set; }
        public string Item { get; set; }
        public string Location { get; set; }
        public string ItemName { get; set; }
        public string ItemCode { get; set; }
        public double? Longhitude { get; set; }
        public double? Latitude { get; set; }
        public string CreatedBy { get; set; }
        public bool IsActive { get; set; }
        public DateTime DateCreated { get; set; }
    }
    public class PoleLatitudeLongtitudeDTO
    {
        public double? Latitude { get; set; }
        public double? Longtitude { get; set; }
    }

    public class PoleLookUpDTO
    {
        public int Id { get; set; }
        public string PoleNo { get; set; }
        public double? Longitude { get; set; }
        public double? Latitude { get; set; }
        public string Address { get; set; }
        public bool IsValid { get; set; }
    }
}
