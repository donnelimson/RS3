using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.Filter
{
    public class ChangeOfNameFilter : FilterBase
    {
        
        public string ControlNo { get; set; }
        public string AccountNo { get; set; }
        public string OldConsumerNo { get; set; }
        public string AccountName { get; set; }
        public string ConsumerNo { get; set; }
        public string ConsumerName { get; set; }
        public string PurposeReason { get; set; }
        public int? Status { get; set; }
        public int? RelationShip { get; set; }
        public string OtherRelationship { get; set; }
        public string CurrentPhase { get; set; }

    }
}
