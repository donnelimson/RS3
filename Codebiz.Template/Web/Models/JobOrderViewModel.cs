using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class JobOrderViewModel
    {
        public string JobOrderType { get; set; }
        public string UserCanViewJO { get; set; }
        public string UserCanExportJO { get; set; }
        public string UserCanAddJO { get; set; }
        public string UserCanEditJO { get; set; }
        public string UserCanDeleteJO { get; set; }
        public string UserCanReactivateOrDeactivateJO { get; set; }
    }
}
