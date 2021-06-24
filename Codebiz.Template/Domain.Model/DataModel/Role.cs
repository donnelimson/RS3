using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.DataModel
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(4)]
        public string Code { get; set; }
        [MaxLength(50)]
        public string Description { get; set; }
    }
}
