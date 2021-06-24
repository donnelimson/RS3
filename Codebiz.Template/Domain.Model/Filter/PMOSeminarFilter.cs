using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.Filter
{
    public class PMOSeminarFilter : FilterBase
    {
        public string ConsumerNo { get; set; }
        public string Name { get; set; }
        public string Facilitator { get; set; }
        public string AreaOffice { get; set; }
        public int? TransactionTypeId { get; set; }
        public string Status { get; set; }
    }
}
