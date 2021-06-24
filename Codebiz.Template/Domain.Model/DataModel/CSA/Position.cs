using Codebiz.Domain.Common.Model.DataModel.CSA;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Codebiz.Domain.Common.Model
{
    public class Position : ModelBase
    {
        [Key]
        public int PositionId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Code { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsManager { get; set; }
        public bool IsHeadOfficer { get; set; }

        public virtual ICollection<DepartmentDetail> Divisions { get; set; }
    }
}
