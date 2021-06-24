using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.Filter
{
    public class ForDisconnectionFilter : FilterBase
    {
        public string AccountNo { get; set; }
        public string Name { get; set; }
        public int? ConsumerTypeId { get; set; }
        public string DisconnectedBy { get; set; }
        public string  Status { get; set; }
        public DateTime? DisconnectedOn { get; set; }
    }
}
