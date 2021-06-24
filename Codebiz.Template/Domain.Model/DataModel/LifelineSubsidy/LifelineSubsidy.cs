using System.ComponentModel.DataAnnotations;

namespace Codebiz.Domain.Common.Model
{
    public class LifelineSubsidy : ModelBase
    {
        [Key]
        public int LifelineSubsidyId { get; set; }
        public string Code { get; set; }
        public int Minimum { get; set; }
        public int Maximum { get; set; }

        public float Discount { get; set; }
        public bool IsDeleted { get; set; }
    }
}
