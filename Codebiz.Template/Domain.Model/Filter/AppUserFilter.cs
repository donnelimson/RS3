using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.Filter
{
    public class AppUserFilter : FilterBase
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public string Office { get; set; }
        public string Department { get; set; }
        public int? AppUserStatus { get; set; }
    }
}
