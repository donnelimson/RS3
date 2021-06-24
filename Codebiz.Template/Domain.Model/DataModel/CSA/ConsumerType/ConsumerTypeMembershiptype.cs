using Codebiz.Domain.Common.Model.EnumBaseModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Codebiz.Domain.Common.Model
{
    public class ConsumerTypeMembershipType
    {
        public ConsumerTypeMembershipType()
        {
            this.ConsumerTypeMembershipTypeSubcategories = new HashSet<ConsumerTypeMembershipTypeSubcategory>();
        }

        [Key]
        public int ConsumerTypeMembershiptypeId { get; set; }

        [ForeignKey("ConsumerType")]
        public int ConsumerTypeId { get; set; }
        public virtual ConsumerType ConsumerType { get; set; }

        [ForeignKey("MembershipType")]
        public int MembershipTypeId { get; set; }
        public virtual MembershipType MembershipType { get; set; }

        public virtual ICollection<ConsumerTypeMembershipTypeSubcategory> ConsumerTypeMembershipTypeSubcategories { get; set; }

    }
}
