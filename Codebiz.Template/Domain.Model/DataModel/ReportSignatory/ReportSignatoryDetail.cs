using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Codebiz.Domain.Common.Model.DataModel
{
    public class ReportSignatoryDetail
    {
        [Key]
        public int ReportSignatoryDetailId { get; set; }


        [ForeignKey("ReportSignatory")]
        public int ReportSignatoryId { get; set; }
        public virtual ReportSignatory ReportSignatory { get; set; }

        [ForeignKey("Label")]
        public int LabelId { get; set; }
        public virtual ApproverLabel Label { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
