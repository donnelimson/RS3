using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.ERP.Infrastructure.Finance.DTO
{
    public class GLAccountSearchDTO
    {
        public string AcctCode { get; set; }

        public string AcctName { get; set; }

        public string FatherCode { get; set; }

        public string Postable { get; set; }
    }
}
