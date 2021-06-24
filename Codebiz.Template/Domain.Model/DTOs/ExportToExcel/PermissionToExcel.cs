using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.DTOs.ExportToExcel
{
    public class PermissionToExcel
    {
        [DisplayName("DESCRIPTION")]
        public string Description { get; set; }

        [DisplayName("PERMISSION GROUP")]
        public string PermissionGroup { get; set; }

        [DisplayName("NAVIGATION LINKS")]
        public string NavigationLink { get; set; }

        [DisplayName("ACTIVE")]
        public bool IsActive { get; set; }

        [DisplayName("CREATED BY")]
        public string CreatedBy { get; set; }

        [DisplayName("CREATED DATE")]
        public DateTime? CreatedDate { get; set; }
    }
}
