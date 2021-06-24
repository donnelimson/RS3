using System;
using System.ComponentModel;

namespace Codebiz.Domain.Common.Model.DTOs.ExportToExcel
{
    public class PaymentToExcel
    {
        [DisplayName("OR NO.")]
        public string ORNo { get; set; }

        [DisplayName("OR DATE")]
        public DateTime? ORDate { get; set; }

        [DisplayName("CODE")]
        public string CardCode { get; set; }

        [DisplayName("NAME")]
        public string CardName { get; set; }

        [DisplayName("REFERENCE")]
        public string Reference { get; set; }

        [DisplayName("AMOUNT PAID")]
        public decimal? AmountPaid { get; set; }
       
        [DisplayName("CREATED BY")]
        public string CreatedBy { get; set; }
    }
 }


