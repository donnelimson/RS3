using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.DTOs.ExportToExcel
{
   public class CityTownExportToExcel
    {
        [DisplayName("CODE")]
        public string Code { get; set; }
        [DisplayName("CITYTOWN")]
        public string Citytown { get; set; }
        [DisplayName("PROVINCE")]
        public string Province { get; set; }
        [DisplayName("CREATED DATE")]
        public DateTime CreatedOn { get; set; }
        [DisplayName("CREATED BY")]
        public string CreatedBy { get; set; }
        [DisplayName("STATUS")]
        public bool Status { get; set; }

    }
}
