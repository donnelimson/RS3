using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model
{
    public class NavLink : ModelBase
    {
        [Key]
        public int NavLinkId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Area { get; set; }

        [MaxLength(100)]
        public string Controller { get; set; }

        [MaxLength(100)]
        public string Action { get; set; }

        [MaxLength(100)]
        public string IconClass { get; set; }

        [MaxLength(2000)]
        public string Parameters { get; set; }
        public bool IsParent { get; set; }

        public int Ordinal { get; set; }

        [ForeignKey("ParentNavLink")]
        public int? ParentNavLinkId { get; set; }
        public virtual NavLink ParentNavLink { get; set; }

        public virtual ICollection<Permission> Permissions { get; set; }

        public virtual ICollection<NavLink> ChildNavLinks { get; set; }
    }
}
