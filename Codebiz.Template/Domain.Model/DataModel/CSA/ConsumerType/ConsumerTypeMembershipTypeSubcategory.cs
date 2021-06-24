using Codebiz.Domain.Common.Model.EnumBaseModels;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Codebiz.Domain.Common.Model
{
    public class ConsumerTypeMembershipTypeSubcategory
    {
        [Key]
        public int ConsumerTypeMembershipTypeSubcategoryId { get; set; }

        [ForeignKey("ConsumerTypeMembershipType")]
        public int ConsumerTypeMembershipTypeId { get; set; }
        public virtual ConsumerTypeMembershipType ConsumerTypeMembershipType { get; set; }

        [ForeignKey("MembershipTypeSubcategory")]
        public int MembershipTypeSubcategoryId { get; set; }
        public virtual MembershipTypeSubcategory MembershipTypeSubcategory { get; set; }
    }
}
