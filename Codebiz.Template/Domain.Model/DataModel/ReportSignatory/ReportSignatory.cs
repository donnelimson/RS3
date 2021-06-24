using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Codebiz.Domain.Common.Model.DataModel
{
    public class ReportSignatory : ModelBase
    {
        public ReportSignatory()
        {
            Signatories = new HashSet<ReportSignatoryDetail>();
        }

        [Key]
        public int ReportSignatoryId { get; set; }

        public bool IsDeleted { get; set; }

        [ForeignKey("ReportCategory")]
        public int ReportCategoryId { get; set; }
        public virtual ReportCategory ReportCategory { get; set; }

        [ForeignKey("Report")]
        public int ReportId { get; set; }
        public virtual Report Report { get; set; }

        public virtual ICollection<ReportSignatoryDetail> Signatories { get; set; }
    }
}
