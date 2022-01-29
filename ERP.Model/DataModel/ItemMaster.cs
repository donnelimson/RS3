using Codebiz.Domain.Common.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.ERP.Model
{
    public class ItemMaster:ModelBase
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(13)]
        public string ItemCode { get; set; }
        [MaxLength(100)]
        public string LongDescription { get; set; }
        [MaxLength(50)]
        public string ShortDescription { get; set; }
        public bool IsDeleted { get; set; }
        public decimal? BasePrice { get; set; } = 0;
    }
}
