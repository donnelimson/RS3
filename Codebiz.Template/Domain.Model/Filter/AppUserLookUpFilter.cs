using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.Filter
{
    public class AppUserLookUpFilter : FilterBase
    {
        public string Name { get; set; }
        public int? PositionId { get; set; }
        public int? AreaId { get; set; }
    }
}
