using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models.ViewModels
{
   public class SubStationViewModel
    {
        public int SubStationId { get; set; }
        public string Description { get; set; }
        public List<FeederViewModal> Feeders { get; set; }
    }
    public class FeederViewModal
    {
        public int FeederId { get; set; }
        public string Name { get; set; }
    }
}
