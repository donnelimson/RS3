using Codebiz.Domain.Common.Model.ViewModel.MeterReadingRemarks;
using System.ComponentModel.DataAnnotations;

namespace Codebiz.Domain.Common.Model
{
    public class MeterReadingRemark : ModelBase
    {
        [Key]
        public int MeterReadingRemarkId { get; set; }

        [Required]
        [MaxLength(20)]
        public string Code { get; set; }

        [Required]
        [MaxLength(300)]
        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public void setMeterReadingRemarks(MeterReadingRemarksViewModel model)
        {
            Code = model.Code;
            Name = model.Name;
        }
    }
}
