using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.Filter
{
    public class SupportingDocumentFilter : FilterBase
    {
        public int? TransactionTypeId { get; set; }
    }
}
