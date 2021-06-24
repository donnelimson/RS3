using Codebiz.Domain.Common.Model.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model
{
    public class ReportSignatoryFilter : FilterBase
    {
        public string Category { get; set; }
        public string Report { get; set; }
        public string Signatory { get; set; }
    }
}
