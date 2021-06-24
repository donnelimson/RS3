using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Codebiz.Domain.Common.Model
{
    public class ModelBase
    {
        public bool IsActive { get; set; } = true;

        [ForeignKey("CreatedByAppUser")]
        public int CreatedByAppUserId { get; set; } = 1;
        [ScriptIgnore]
        public virtual AppUser CreatedByAppUser { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        [ForeignKey("ModifiedByAppUser")]
        public int? ModifiedByAppUserId { get; set; }
        [ScriptIgnore]
        public virtual AppUser ModifiedByAppUser { get; set; }

        public DateTime? ModifiedOn { get; set; }
    }
}
