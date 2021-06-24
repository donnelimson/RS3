using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.DTOs.ExportToExcel
{
    public class DepartmentsToExcel
    {
        [DisplayName("CODE")]
        public string Code { get; set; }

        [DisplayName("NAME")]
        public string Name { get; set; }

        [DisplayName("ACTIVE")]
        public bool IsActive { get; set; }

        [DisplayName("CREATED BY")]
        public string CreatedBy { get; set; }

        [DisplayName("CREATED DATE")]
        public DateTime? CreatedDate { get; set; }
    }
}
