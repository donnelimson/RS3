using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model
{
    public class EnumBase<TEnum> where TEnum : struct
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public virtual int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public virtual string Name { get; set; }

        [MaxLength(200)]
        public virtual string Description { get; set; }

        [MaxLength(30)]
        public string Abbr { get; set; }
    }
}
