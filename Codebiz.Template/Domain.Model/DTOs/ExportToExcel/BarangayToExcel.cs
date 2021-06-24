using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.DTOs.ExportToExcel
{
   public class BarangayToExcel
    {
        [DisplayName("BRGY CODE")]
        public string BarangayCode { get; set; }
        [DisplayName("BARANGAY NAME")]
        public string BarangayName { get; set; }
        [DisplayName("CITY TOWN")]
        public string CityTown { get; set; }
        [DisplayName("PROVINCE")]
        public string Province { get; set; }

        [DisplayName("CREATED BY")]
        public string CreatedBy { get; set; }
        [DisplayName("CREATED DATE")]
        public DateTime CreatedOn { get; set; }
        [DisplayName("STATUS")]
        public bool Status { get; set; }
    }
}
