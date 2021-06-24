using Codebiz.Domain.Common.Model.DataModel.SalesEmployee;
using Codebiz.Domain.Common.Model.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model
{
    public class SalesEmployee : ModelBase
    {
        [Key]
        public int SalesEmployeeId { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }

        public double Commission { get; set; }

        [ForeignKey("CommissionGroup")]
        public int? CommissionGroupId { get; set; }
        public virtual CommisionGroups CommissionGroup { get; set; }

        public string Remarks { get; set; }

        public bool IsDeleted { get; set; }

        public void SetData(SalesEmployeeViewModel model)
        {
            EmployeeId = model.EmployeeId;
            Commission = model.Commission;
            CommissionGroupId = model.CommissionGroupId;
            Remarks = model.Remarks;
            IsActive = model.IsActive;
        }
    }
}
