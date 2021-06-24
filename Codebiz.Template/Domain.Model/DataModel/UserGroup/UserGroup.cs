using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Codebiz.Domain.Common.Model
{
    public class UserGroup : ModelBase
    {
        [Key]
        public int UserGroupId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }
        public bool IsDeleted { get; set; }

        public virtual ICollection<Permission> Permissions { get; set; }
        public virtual ICollection<AppUser> AppUsers { get; set; }
    }
}
