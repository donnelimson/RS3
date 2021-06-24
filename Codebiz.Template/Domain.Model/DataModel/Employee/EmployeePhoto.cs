using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Codebiz.Domain.Common.Model
{
    public class EmployeePhoto : ImageBase
    {
        [Key]
        public int EmployeePhotoId { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
    }
    public class EmployeeSignature : ImageBase
    {
        [Key]
        public int EmployeeSignatureId { get; set; }

        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
