using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model
{
    public class BrandType : ModelBase
    {
        public int BrandTypeId { get; set; }
        public string CodeName { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
    }
}
