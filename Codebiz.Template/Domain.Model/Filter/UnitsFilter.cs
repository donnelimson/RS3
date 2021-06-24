using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.Filter
{
    public class UnitsFilter : FilterBase
    {
        public string Units { get; set; }
        public string MeterReader { get; set; }
        public string Office { get; set; }
        public string AppUser { get; set; }
    }
}
