using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.DTOs
{
    public class PermissionDTO
    {
        public int PermisssionId { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string PermissionGroup { get; set; }
        public string NavLink { get; set; }
        public string UserGroups { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}
