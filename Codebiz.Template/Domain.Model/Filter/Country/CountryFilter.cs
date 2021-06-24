using Codebiz.Domain.Common.Model.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model
{
    public class CountryFilter : FilterBase
    {
        public string Code { get; set; }

        public string Name { get; set; }

    }
}
