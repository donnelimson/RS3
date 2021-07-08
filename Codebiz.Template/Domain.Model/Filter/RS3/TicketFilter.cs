using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.Filter.RS3
{
    public class TicketFilter:FilterBase
    {
        public string TicketNo { get; set; }
        public string Technician { get; set; }
        public string ConcernWhom { get; set; }
        public string Status { get; set; }
        public int? Priority { get; set; }
        public bool MyTicketsOnly { get; set; }
        public int? TechnicianId { get; set; }
    }
}
