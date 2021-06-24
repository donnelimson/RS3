using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.DTOs.BillingCategory
{
    public class CategoryTableDTO
    {
        public int CategoryId { get; set; }

        [DisplayName("CODE")]
        public string Code { get; set; }

        [DisplayName("NAME")]
        public string Name { get; set; }

        [DisplayName("DISPLAYED NAME")]
        public string DisplayedName { get; set; }

        [DisplayName("BILL")]
        public bool IsBill { get; set; }

        [DisplayName("ACTIVE")]
        public bool IsActive { get; set; }

        public bool IsEditing { get; set; }
        public int? ModifiedIndex { get; set; }
        public bool IsInUse { get; set; }
    }
}
