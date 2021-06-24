using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.DTOs
{
   public class SubStationDetailsForUpdateDTO
    {
        public string Description { get; set; }
        public List<FeederDetailsForUpdateDTO> Feeders { get; set; }
    }
    public class FeederDetailsForUpdateDTO
    {
        public int FeederId { get; set; }
        public string Name { get; set; }
    }
}
