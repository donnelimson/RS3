using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.DTOs.ExportToExcel
{
    public class NavigationLinksToExcel
    {
        [DisplayName("NAME")]
        public string Name { get; set; }

        [DisplayName("NAVIGATION GROUP")]
        public string NavigationGroup { get; set; }

        [DisplayName("CONTROLLER")]
        public string Controller { get; set; }

        [DisplayName("ACTION")]
        public string Action { get; set; }

        [DisplayName("ICON CLASS")]
        public string IconClass { get; set; }

        [DisplayName("PARAMETERS")]
        public string Parameters { get; set; }

        [DisplayName("IS PARENT")]
        public bool IsParent { get; set; }

        [DisplayName("IS ACTIVE")]
        public bool IsActive { get; set; }

        [DisplayName("CREATED BY")]
        public string CreatedBy { get; set; }

        [DisplayName("DATE CREATED")]
        public DateTime? CreatedDate { get; set; }
    }
}
