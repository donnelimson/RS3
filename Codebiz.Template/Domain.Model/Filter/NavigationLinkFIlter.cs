using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.Filter
{
    public class NavigationLinkFIlter
    {
        public string SearchTerm { get; set; }
        public IPagedList<NavLink> ConfigSettings { get; set; }
    }
}
