using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models.ViewModels.ConsumerType
{
    public class ConsumerTypeViewModel
    {
        public int ConsumerTypeId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int? ConsumerTypeVoltageId { get; set; }
        public bool? CanBeTagAsSoleUse { get; set; }
        public List<int> MembershipTypes { get; set; }
        public List<int> MembershipTypeSubcategories { get; set; }
    }
}