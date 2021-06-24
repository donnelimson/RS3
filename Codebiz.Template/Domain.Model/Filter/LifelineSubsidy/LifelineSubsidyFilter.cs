using Codebiz.Domain.Common.Model.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model
{
    public class LifelineSubsidyFilter : FilterBase
    {
        public string Code { get; set; }
        public int Minimum { get; set; }
        public int Maximum { get; set; }
        public float Discount { get; set; }
    }
}
