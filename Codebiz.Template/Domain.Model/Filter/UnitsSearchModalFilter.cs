using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.Filter
{
    public class UnitsSearchModalFilter:FilterBase
    {
        public int OfficeId { get; set; }
        public string Searcher { get; set; }
        public string MeterReader { get; set; }
        public string Unit { get; set; }
        public int? MeterReadingId { get; set; }

    }
}
