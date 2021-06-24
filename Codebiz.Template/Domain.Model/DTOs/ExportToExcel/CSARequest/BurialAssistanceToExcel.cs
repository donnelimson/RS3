using System;
using System.ComponentModel;

namespace Codebiz.Domain.Common.Model.DTOs.ExportToExcel.CSARequest
{
    public class BurialAssistanceToExcel
    {
        [DisplayName("CONTROL NO")]
        public string ControlNo { get; set; }

        [DisplayName("ACCOUNT NO")]
        public string AccountNo { get; set; }

        [DisplayName("MEMBER NO")]
        public string MemberNo { get; set; }

        [DisplayName("DECEASED NAME")]
        public string DeceasedName { get; set; }

        [DisplayName("DATE OF DEATH ")]
        public string DateOfDeath { get; set; }

        [DisplayName("CLAIMANT NAME")]
        public string ClaimantName { get; set; }

        [DisplayName("CURRENT PHASE")]
        public string CurrentPHase { get; set; }

        [DisplayName("STATUS")]
        public string Status { get; set; }
    }
}
