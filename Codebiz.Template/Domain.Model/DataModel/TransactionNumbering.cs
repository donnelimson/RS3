using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.DataModel
{
    public class TransactionNumbering
    {
        [Key]

        public int Id { get; set; }
        [ForeignKey("TransactionType")]
        public int TransactionTypeId { get; set; }
        public virtual TransactionType TransactionType { get; set; }
        public string DocSeriesName { get; set; }
        public int NextNum { get; set; }
        public string NumberFormat { get; set; }
        public int NoOfDigits { get; set; }
        public string Prefix { get; set; }
        public string Suffix { get; set; }
        public bool IsYearPrefix { get; set; }
    }
}
