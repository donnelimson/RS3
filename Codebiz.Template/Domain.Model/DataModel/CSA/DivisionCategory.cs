using System.ComponentModel.DataAnnotations.Schema;

namespace Codebiz.Domain.Common.Model.DataModel.CSA
{
    public class DivisionCategory
    {
        public int DivisionCategoryId { get; set; }

        [ForeignKey("Division")]
        public int DivisionId { get; set; }
        public virtual Division Division { get; set; }

        [ForeignKey("DivisionType")]
        public int DivisionTypeId { get; set; }
        public virtual DivisionType DivisionType { get; set; }
    }
}
