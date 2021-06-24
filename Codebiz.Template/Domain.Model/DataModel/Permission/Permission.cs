using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Codebiz.Domain.Common.Model
{
    public class Permission : ModelBase
    {
        [Key]
        public int PermisssionId { get; set; }

        [Required]
        [MaxLength(50)]
        [Index("PermissionCodeIndex", IsUnique = true)]
        public string Code { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [ForeignKey("PermissionGroup")]
        public int PermissionGroupId { get; set; }
        public virtual PermissionGroup PermissionGroup { get; set; }

        [ForeignKey("NavLink")]
        public int? NavLinkId { get; set; }
        public virtual NavLink NavLink { get; set; }

        public virtual ICollection<UserGroup> UserGroups { get; set; }
    }
}
