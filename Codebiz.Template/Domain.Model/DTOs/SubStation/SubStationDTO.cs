using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.DTOs.SubStation
{
   public class SubStationDTO
    {
        public int SubStationId { get; set; }
        public string SubStation { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Feeders { get; set; }
    }
    public class FeederDTO
    {
        public int FeederId { get; set; }
        public string Name { get; set; }
    }
}
