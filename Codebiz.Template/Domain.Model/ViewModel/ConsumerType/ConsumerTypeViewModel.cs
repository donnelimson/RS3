using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.ViewModel.ConsumerType
{
    public class ConsumerTypeViewModel
    {
        public int ConsumerTypeId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int? ConsumerTypeVoltageId { get; set; }
        public bool CanBeTagAsSoleUse { get; set; }
        public bool IsBapa { get; set; }
        public bool IsMapa { get; set; }
        public List<int> MembershipTypes { get; set; }
        public List<int> MembershipTypeSubcategories { get; set; }
    }
}
