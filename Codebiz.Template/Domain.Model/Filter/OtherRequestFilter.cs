using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.Filter
{
    public class OtherRequestFilter : FilterBase
    {
        public string Name { get; set; }
        public string AccountNo { get; set; }
        public int? TransactionTypeId { get; set; }
        public int? StatusId { get; set; }
        public int? CurrentPhaseId { get; set; }
    }
}
