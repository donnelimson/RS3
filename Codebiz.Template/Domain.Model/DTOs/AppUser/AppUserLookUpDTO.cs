using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.DTOs.AppUser
{
    public class AppUserLookUpDTO
    {
        public int AppUserId { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Area { get; set; }
    }

    public class AccessRightDTO
    {
        public string Name { get; set; }
        public List<string> Permissions { get; set; }
    }
}
