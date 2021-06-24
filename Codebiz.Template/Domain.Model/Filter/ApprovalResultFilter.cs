using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.Filter
{
   public class ApprovalResultFilter: FilterBase
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public int? Stage { get; set; }
        public DateTime? ActionDateTo { get; set; }
        public DateTime? ActionDateFrom { get; set; }
        public int? StatusId { get; set; }
        public string Remarks { get; set; }
    }
}
