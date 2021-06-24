using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Codebiz.Domain.Common.Model.DataModel.CSA
{
    public class DivisionPosition
    {
        [Key]
        public int DivisionPositionId { get; set; }

        [ForeignKey("Division")]
        public int DivisionId { get; set; }
        public virtual Division Division { get; set; }

        [ForeignKey("Position")]
        public int PositionId { get; set; }
        public virtual Position Position { get; set; }
    }
}
