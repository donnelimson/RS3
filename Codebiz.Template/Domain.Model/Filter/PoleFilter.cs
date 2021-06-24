using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.Filter
{
   public class PoleFilter: FilterBase
    {
        public string PoleNo { get; set; }
        public string Code { get; set; }
        public string Location { get; set; }
        public string Item { get; set; }
    }
}
