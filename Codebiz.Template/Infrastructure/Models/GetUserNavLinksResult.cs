using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class GetUserNavLinksResult
    {
        public List<NavLinkModel> NavLinks { get; set; }
    }

    public class NavLinkModel
    {
        public int NavLinkId { get; set; }
        public string Name { get; set; }
        public string Controller { get; set; }
        public string Area { get; set; }
        public string Action { get; set; }
        public string IconClass { get; set; }
        public string Parameters { get; set; }
        public bool IsParent { get; set; }
        public string Permission { get; set; }
        public string PendingApprovalCount { get; set; }
        public int Ordinal { get; set; }

        public List<NavLinkModel> ChildNavLinks { get; set; }
    }
}
