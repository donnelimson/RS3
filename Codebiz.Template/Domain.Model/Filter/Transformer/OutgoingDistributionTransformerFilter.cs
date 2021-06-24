using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.Filter.Transformer
{
    public class OutgoingDistributionTransformerFilter : FilterBase
    {
        public string TrackingNo { get; set; }
        public string AccountNo { get; set; }
        public int? BrandId { get; set; }
        public string SerialNo { get; set; }
        public int? Status { get; set; }
        public double? KVARating { get; set; }
        public int? TestedBy { get; set; }
        public DateTime? TestedDate { get; set; }
    }
}
