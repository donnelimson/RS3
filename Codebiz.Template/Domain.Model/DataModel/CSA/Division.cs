using Codebiz.Domain.Common.Model.Enums;
using Codebiz.Domain.Common.Model.ViewModel.Division;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Codebiz.Domain.Common.Model.DataModel.CSA
{
    public class Division : ModelBase
    {
        public Division()
        {
            Categories = new HashSet<DivisionCategory>();
        }

        [Key]
        public int DivisionId { get; set; }

        [MaxLength(20)]
        public string Code { get; set; }

        [Required]
        [MaxLength(300)]
        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public virtual ICollection<DivisionCategory> Categories { get; set; }

        public virtual ICollection<DepartmentDetail> Positions { get; set; }

        public void SetNewDivision(DivisionViewModel model)
        {
            Name = model.DivisionName;
            Code = model.DivisionCode;
        }
    }

    public class DivisionType : EnumBase<DivisionTypeEnums>
    {

    }
}
