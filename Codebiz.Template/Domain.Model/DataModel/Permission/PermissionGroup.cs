using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model
{
    public class PermissionGroup : ModelBase
    {
        [Key]
        public int PermissionGroupId { get; set; }

        [Required]
        [MaxLength(50)]
        [Index("PermissionGroupNameIndex", IsUnique = true)]
        public string Name { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        public virtual ICollection<Permission> Permissions { get; set; }
    }
}
