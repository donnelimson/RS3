using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.Filter
{
    public class NetMeteringFilter : FilterBase
    {
        public string RequestNo { get; set; }
        public string AccountNo { get; set; }
        public string Name { get; set; }
        public int? Status { get; set; }

        public DateTime? ProcessOnFrom { get; set; }
        public DateTime? ProcessOnTo { get; set; }
        
    }
}
