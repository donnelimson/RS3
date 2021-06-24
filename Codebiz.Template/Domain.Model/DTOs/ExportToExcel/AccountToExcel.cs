using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.DTOs.ExportToExcel
{
    public class AccountToExcel
    {
        [DisplayName("ACCOUNT NO.")]
        public string AccountNo { get; set; }
        [DisplayName("ACCOUNT TYPE")]
        public string AccountType { get; set; }

        [DisplayName("CONSUMER NO.")]
        public string ConsumerNo { get; set; }

        [DisplayName("CONSUMER TYPE")]
        public string ConsumerType { get; set; }

        [DisplayName("NAME")]
        public string Name { get; set; }

        [DisplayName("ADDRESS")]
        public string Address { get; set; }

        [DisplayName("COMPLETE REQUIREMENTS")]
        public bool IsCompleteRequirements { get; set; }

        [DisplayName("STATUS")]
        public string Status { get; set; }

        [DisplayName("CURRENT PHASE")]
        public string CurrentPhase { get; set; }

        [DisplayName("CREATED BY")]
        public string CreatedBy { get; set; }  
    }
}
