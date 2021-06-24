using System;
using System.Collections.Generic;
using System.ComponentModel;


namespace Codebiz.Domain.Common.Model.DTOs.DocumentNumbering
{
    public class DocumentNumberingDTO
    {
        public int DocumentNumberingId { get; set; }
        [DisplayName("TRANSACTION TYPE")]

        public string TransactionType { get; set; }
        [DisplayName("DEFAULT SERIES")]

        public string DefaultSeries { get; set; }
        [DisplayName("FIRST NO.")]

        public int? FirstNo { get; set; }
        [DisplayName("NEXT NO.")]

        public int? NextNo { get; set; }
        [DisplayName("LAST NO.")]

        public int? LastNo { get; set; }
        [DisplayName("LOCKED")]

        public bool IsLocked { get; set; }
        [DisplayName("ACTION BY")]

        public string ActionBy { get; set; }
        [DisplayName("ACTION DATE")]

        public DateTime? ActionDate { get; set; }
    }
    public class DocumentNumberingForUpdateDTO
    {
        public int DocumentNumberingId { get; set; }
        public int TransactionTypeId { get; set; }
        public List<DocumentNumberingTransactionTypeForUpdateDTO> DocumentNumberingTransactionTypes { get; set; }
    }
    public class DocumentNumberingTransactionTypeForUpdateDTO
    {
        public int DocNumberingDetailId { get; set; }
        public int DocNumberingId { get; set; }
        public string Name { get; set; }
        public long? FirstNo { get; set; }
        public long? NextNo { get; set; }
        public long? LastNo { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public int? NoOfDigits { get; set; }
        public bool IsLocked { get; set; }
        public bool IsDefault { get; set; }
        public bool FirstNoEditable { get; set; }
        public bool IsYearPrefix { get; set; }
    }
    public class DocumentNumberingDetailsDTO : DocumentNumberingTransactionTypeForUpdateDTO
    {
        public int TransactionTypeId { get; set; }
    }
    public class DocumentNumberingInUseDTO
    {
        //  public int DocumentNumberingTransactionTypeId { get; set; }
        public string Name { get; set; }
        public int? FirstNo { get; set; }
        public int? NextNo { get; set; }
        public int? LastNo { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public int? NoOfDigits { get; set; }
        public int? CurrentValue { get; set; }
        public bool IsLocked { get; set; }
    }
    public class DocumentNumberingTransactionTypeDTO
    {
        public int DocumentNumberingTransactionTypeId { get; set; }
        public string Name { get; set; }
        public bool IsLocked { get; set; }
        public bool? IsDefault { get; set; }
    }
    public class DocNumberingDetailDTO
    {
        public int DocNumberingDetailId { get; set; }
        public string Name { get; set; }
        public long? FirstNo { get; set; }
        public long? NextNo { get; set; }
        public long? LastNo { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public int? NoOfDigits { get; set; }
        public bool IsLocked { get; set; }
        public bool IsDefault { get; set; }
    }

}
