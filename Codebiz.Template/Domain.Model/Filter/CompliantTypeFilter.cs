using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.Filter
{
    public class CompliantTypeFilter : FilterBase
    {
        public int CompliantTypeId { get; set; }

        public string Code { get; set; }
        public bool IsDelete { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ComplaintSubType> CompliantSubTypes { get; set; }
    }
}
