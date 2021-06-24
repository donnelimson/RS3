using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.Filter
{
    public class DiscountApplicationFilter : FilterBase
    {
        public string ControlNo { get; set; }
        public string AccountNo { get; set; }
        public string Name { get; set; }
        public int? ConsumerType { get; set; }
        public int? DiscountType { get; set; }
        public int? Status { get; set; }
        public DateTime? RenewedOnFrom { get; set; }
        public DateTime? RenewedOnTo { get; set; }
    }
}
