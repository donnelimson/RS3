using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.Filter.Request
{
    public class ReconnectionFilter: FilterBase
    {
        public string ControlNo { get; set; }
        public string AccountNo { get; set; }
        public string Circumstances { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int? CurrentPhaseId { get; set; }
        public int? StatusId { get; set; }
    }
}
