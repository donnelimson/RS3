using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.Filter
{
    public class AuditLogsFilter : FilterBase
    {
        public int? LogId { get; set; }
        public string ActivityId { get; set; }
        public string User { get; set; }
        public List<String> Level { get; set; }
        public string Thread { get; set; }
        public string Logger { get; set; }
        public int? LogEventTitleId { get; set; }
        public string Exception { get; set; }
        public string Message { get; set; }
    }
}
