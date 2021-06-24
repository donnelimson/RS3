using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.Filter
{
    public class DocumentNumberingFilter:FilterBase
    {
        public int? TransactionTypeId { get; set; }
        public string ActionBy { get; set; }

    }
    public class DocumentNumberingDetailFilter : FilterBase
    {
       public int DocumentNumberingId { get; set; }

    }
}
