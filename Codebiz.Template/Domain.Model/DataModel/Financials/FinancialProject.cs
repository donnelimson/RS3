using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codebiz.Domain.Common.Model.ViewModel.FinancialProjects;

namespace Codebiz.Domain.Common.Model.DataModel.Financials
{
    public class FinancialProject : ModelBase
    {
        [Key]
        public int FinancialProjectsId { get; set; }
        [MaxLength(20)]
        public string Code { get; set; }
        [MaxLength(300)]
        [DisplayName("NAME")]
        public string Name { get; set; }
        public DateTime? ValidFrom { get; set; }
        public DateTime? ValidTo { get; set; }
        public bool IsDeleted { get; set; }

        public void setProjects(FinancialProjectsViewModel model)
        {
            Code = model.Code;
            Name = model.Name;
            IsActive = model.IsActive;
            ValidFrom = model.ValidFrom;
            ValidTo = model.ValidTo;
        }
    }
}
