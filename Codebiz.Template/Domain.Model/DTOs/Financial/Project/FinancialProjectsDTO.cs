using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.DTOs.Financial.Project
{
    public class FinancialProjectsDTO
    {
        public int FinancialProjectsId { get; set; }

        [DisplayName("PROJECT CODE")]
        public string Code { get; set; }

        [DisplayName("PROJECT NAME")]
        public string Name { get; set; }

        [DisplayName("VALID FROM")]
        [DisplayFormat(DataFormatString = "{MM/dd/yyyy}")]
        public DateTime? ValidFrom { get; set; }

        [DisplayName("VALID TO")]
        public DateTime? ValidTo { get; set; }

        [DisplayName("IS ACTIVE")]
        public bool IsActive { get; set; }

        [DisplayName("ACTION BY")]
        public string CreatedBy { get; set; }

        [DisplayName("ACTION ON")]
        public DateTime CreatedDate { get; set; }

    }
}
