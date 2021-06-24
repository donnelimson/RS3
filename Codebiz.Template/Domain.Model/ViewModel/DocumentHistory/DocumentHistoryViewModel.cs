using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.ViewModel.DocumentHistory
{
    public class DocumentHistoryViewModel
    {
        public string ReferenceNo { get; set; }
        public int? TransactionTypeId { get; set; }
        public int Id { get; set; }
        public string Remarks { get; set; }
        public string Details { get; set; }
        public int AppuserId { get; set; }
        public string GroupReferenceNo { get; set; }
        public int GroupTransactionTypeId { get; set; }
    }
    public class DocumentHistoryTableViewModel
    {
        public string DocumentDescription { get; set; }
        public string ReferenceNo { get; set; }
        public string Details { get; set; }
        public string Remarks { get; set; }
        public int? TransactionTypeId { get; set; }
        public string Status { get; set; }
        public int? Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool CanView { get; set; }
    }
}
