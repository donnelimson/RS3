using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.Filter.Transformer
{
    public class TransformerTestingFilter : FilterBase
    {
        public string TrackingNo { get; set; }
        public string AccountNo { get; set; }
        public int? Brand { get; set; }
        public string SerialNo { get; set; }
        public int? Status { get; set; }
        public string ItemNo { get; set; }
        public int? TransactionType { get; set; }
        public bool IsForTesting { get; set; }
        public bool IsForOutgoing { get; set; }
        public bool IsForInventory { get; set; }
        public int CurrentDivisionId { get; set; }
        public int? CurrentOfficeId { get; set; }
        public int? CurrentDepartmentId { get; set; }
        public int CurrentUserId { get; set; }
        public int? CurrentDivisionCategoryId { get; set; }
    }
}
