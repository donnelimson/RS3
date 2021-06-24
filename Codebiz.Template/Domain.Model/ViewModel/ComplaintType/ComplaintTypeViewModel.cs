using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.ViewModel.ComplaintType
{
    public class ComplaintTypeViewModel
    {
        public int ComplaintTypeId { get; set; }
        public string ComplaintTypeName { get; set; }
        public int NatureTypeId { get; set; }
        public List<ComplaintSubTypeViewModel> ComplaintSubTypes { get; set; }
    }

    public class ComplaintSubTypeViewModel
    {
        public int SubTypeId { get; set; }
        public string Complaint { get; set; }
        public bool IsActive { get; set; }
    }
}
