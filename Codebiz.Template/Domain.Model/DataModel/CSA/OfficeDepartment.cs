using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.DataModel.CSA
{
    public class OfficeDepartment
    {
        public int OfficeDepartmentId { get; set; }

        [ForeignKey("Office")]
        public int OfficeId { get; set; }
        public virtual Office Office { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
    }
}
