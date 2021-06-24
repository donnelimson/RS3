using System.ComponentModel;

namespace Codebiz.Domain.Common.Model.DTOs.ExportToExcel
{
    public class ChangeOfNameToExcel
    {
        [DisplayName("CONTROL NO.")]
        public string ControlNo { get; set; }

        [DisplayName("ACCOUNT NO")]
        public string AccountNo { get; set; }

        [DisplayName("CHANGE FROM")]
        public string ChangeFrom { get; set; }

        [DisplayName("CHANGE TO")]
        public string ChangeTo { get; set; }

        [DisplayName("TYPE")]
        public string Type { get; set; }

        [DisplayName("PURPOSE/REASONS")]
        public string PurposeReasons { get; set; }

        [DisplayName("BURIAL ASSISTANCE")]
        public bool BurialAssistance { get; set; }

        [DisplayName("CURRENT PHASE")]
        public string CurrentPhase { get; set; }

        [DisplayName("STATUS")]
        public string Status { get; set; }

        //public string FromConsumerNo { get; set; }

        //public string AccountName { get; set; }

        //public string ToConsumerNo { get; set; }

        //public string ConsumerName { get; set; }
        //public string AccountNo { get; set; }
    }
}
