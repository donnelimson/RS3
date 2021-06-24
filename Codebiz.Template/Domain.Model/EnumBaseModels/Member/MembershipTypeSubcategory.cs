using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Codebiz.Domain.Common.Model.EnumBaseModels
{
    public class MembershipTypeSubcategory
    {
        [Key]
        public int MembershipTypeSubcategoryId { get; set; }
        [ForeignKey("MembershipType")]
        public int MembershipTypeId { get; set; }
        public virtual MembershipType MembershipType { get;set;}
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
