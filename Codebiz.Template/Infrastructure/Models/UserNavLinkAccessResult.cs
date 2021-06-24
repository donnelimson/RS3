using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class UserNavLinkAccessResult
    {
        public List<int> ParentNavLinkIds { get; set; }
        public List<int> NavLinkIds { get; set; }
    }
}
