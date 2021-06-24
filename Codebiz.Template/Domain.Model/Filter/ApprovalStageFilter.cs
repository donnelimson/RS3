using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.Filter
{
    public class ApprovalStageFilter : FilterBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
