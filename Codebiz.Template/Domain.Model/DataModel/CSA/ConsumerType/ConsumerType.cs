using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Codebiz.Domain.Common.Model
{
    public class ConsumerType: ModelBase
    {
        public ConsumerType()
        {
            this.ConsumerTypeMembershipTypes = new HashSet<ConsumerTypeMembershipType>();
        }

        [Key]
        public int CosumerTypeId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public bool CanBeTagAsSoleUse { get; set; }
        public bool IsBapa { get; set; }
        public bool IsMapa { get; set; }
        public virtual ICollection<ConsumerTypeMembershipType> ConsumerTypeMembershipTypes { get; set; }
    }
}
