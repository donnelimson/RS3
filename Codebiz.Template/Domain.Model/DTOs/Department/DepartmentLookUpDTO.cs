using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.DTOs
{
    public class DepartmentLookUpDTO
    {
        public int DepartmentId { get; set; }
        public int? OfficeId { get; set; }
        public string DepartmentName { get; set; }
    }
}
