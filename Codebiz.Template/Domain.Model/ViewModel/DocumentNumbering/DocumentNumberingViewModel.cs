using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.ViewModel.DocumentNumbering
{
    public class DocumentNumberingAddOrUpdateViewModel
    {
        public int DocumentNumberingId { get; set; }
        public int TransactionTypeId { get; set; }
        public List<DocumentNumberingTransactionTypeViewModel> DocumentNumberingTransactions { get; set; }
    }
    public class DocumentNumberingTransactionTypeViewModel
    {
        public int DocumentNumberingTransactionTypeId { get; set; }
        public string Name { get; set; }
        public int? FirstNo { get; set; }
        public int? NextNo { get; set; }
        public int? LastNo { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public int? NoOfDigits { get; set; }
        public bool IsLocked { get; set; }
        public bool IsDefault { get; set; }
        public bool IsDeleted { get; set; }
        
    }
    public class DocNumberingViewModel
    {
        public int DocumentNumberingId { get; set; }
        public string ObjectCode { get; set; }
        public string ObjectName { get; set; }
        public int DocAutoNumber { get; set; }
        public List<DocNumberingDetailsViewModel> DocumentNumberingTransactions { get; set; }
    }
    public class DocNumberingDetailsViewModel
    {
        public int DocNumberingDetailId { get; set; }
        public int DocNumberingId { get; set; }
        public string Name { get; set; }
        public string ObjectCode { get; set; }
        public int FirstNo { get; set; }
        public int NextNo { get; set; }
        public int? LastNo { get; set; }
        public int? NoOfDigits { get; set; }
        public string NumberFormat { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public bool IsLocked { get; set; }
        public bool IsDefault { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsYearPerfix { get; set; }
    }
}
