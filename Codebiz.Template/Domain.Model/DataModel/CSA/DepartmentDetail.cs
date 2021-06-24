using System.ComponentModel.DataAnnotations.Schema;

namespace Codebiz.Domain.Common.Model.DataModel.CSA
{
    public class DepartmentDetail
    {
        public int DepartmentDetailId { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        [ForeignKey("Position")]
        public int PositionId { get; set; }
        public virtual Position Position { get; set; }

        [ForeignKey("Division")]
        public int? DivisionId { get; set; }
        public virtual Division Division { get; set; }


        [ForeignKey("DivisionCategory")]
        public int? DivisionCategoryId { get; set; }
        public virtual DivisionType DivisionCategory { get; set; }

    }
}
