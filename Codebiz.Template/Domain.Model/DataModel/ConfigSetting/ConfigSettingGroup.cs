using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model
{
   public class ConfigSettingGroup
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ConfigSettingGroupId { get; set; }

        [Required]
        [MaxLength(50)]
        [Index("CodeNameIndex", IsUnique = true)]
        public string CodeName { get; set; }
    }
}
