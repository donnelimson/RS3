using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model
{
    public class ReportSignatoryViewModel
    {

        public ReportSignatoryViewModel()
        {
            Signatories = new List<Signatories>();
        }

        public int ReportSignatoryId { get; set; }
        public int CategoryId { get; set; }
        public int ReportId { get; set; }

        public List<Signatories> Signatories { get; set; }

    }

    public class Signatories
    {
        public int ReportSignatoryId { get; set; }
        public int LabelId { get; set; }
        public int EmployeeId { get; set; }
        public string Employee { get; set; }
        public string Position { get; set; }
    }
}
