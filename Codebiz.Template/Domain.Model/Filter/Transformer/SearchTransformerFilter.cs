using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.Filter.Transformer
{
    public class SearchTransformerFilter : FilterBase
    {
        public string SerialNo { get; set; }
        public string Supplier { get; set; }
        public string BrandName { get; set; }

        public int? RequiredFilter { get; set; }
    }
}
