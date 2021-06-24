using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.Filter
{
    public class PurposeFilter:FilterBase
    {
        public int? TransactionTypeId { get; set; }
        public string Purpose { get; set; }
    }
}
