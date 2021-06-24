using Codebiz.Domain.Common.Model.DataModel.CSA;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Codebiz.Domain.Common.Model
{
    public class Department : ModelBase
    {
        public Department()
        {
            Details = new HashSet<DepartmentDetail>();
        }

        [Key]
        public int DepartmentId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Code { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<DepartmentDetail> Details { get; set; }
        public virtual ICollection<AppUser> AppUsers { get; set; }
        public virtual ICollection<OfficeDepartment> Offices { get; set; }
    }
}
