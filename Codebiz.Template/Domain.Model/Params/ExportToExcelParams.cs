using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.Params
{
    public class ExportToExcelParams
    {
        public int CurrentAppUserId { get; set; }
        public string CurrentOffice { get; set; }
        public string ViewListName { get; set; }
    }
}
