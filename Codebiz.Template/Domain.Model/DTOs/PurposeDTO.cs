using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.DTOs
{
    public class PurposeIndexDTO
    {
        public int Id { get; set; }
        [DisplayName("TRANSACTION TYPE")]
        public string TransactionType { get; set; }
        [DisplayName("PURPOSE/S")]
        public string Purpose { get; set; }
        [DisplayName("ACTION ON")]
        public DateTime ActionOn { get; set; }
        public List<string> Purposes { get; set; }
    }
    public class PurposeDetailsDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }
}
