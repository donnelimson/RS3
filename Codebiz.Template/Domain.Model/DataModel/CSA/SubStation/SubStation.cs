using System.Collections.Generic;

namespace Codebiz.Domain.Common.Model
{
    public class SubStation : ModelBase
    {
        public SubStation()
        {
            Feeders = new HashSet<Feeder>();
        }
        public int SubStationId { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<Feeder> Feeders { get; set; }

    }
}
