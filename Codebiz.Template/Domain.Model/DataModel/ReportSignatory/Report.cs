using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Codebiz.Domain.Common.Model.DataModel
{
    public class Report
    {
        [Key]
        public int ReportId { get; set; }

        public string Description { get; set; }

        [ForeignKey("ReportCategory")]
        public int ReportCategoryId { get; set; }
        public virtual ReportCategory ReportCategory { get; set; }

        public bool HasSignatory { get; set; }

        public bool IsActive { get; set; }
    }
}
