using Codebiz.Domain.Common.Model.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Model.Filter
{
    public class BrandFilter:FilterBase
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
