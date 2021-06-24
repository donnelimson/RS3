using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model
{
    public class ConfigSetting : ModelBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ConfigSettingId { get; set; }

        [Required]
        [MaxLength(60)]
        [Index("NameIndex", IsUnique = true)]
        public string Name { get; set; }

        [Required]
        [MaxLength(300)]
        public string Description { get; set; }

        [Required]
        [MaxLength(500)]
        public string Value { get; set; }

        [ForeignKey("ConfigSettingDataType")]
        public int? ConfigSettingDataTypeId { get; set; }
        public virtual ConfigSettingDataType ConfigSettingDataType { get; set; }

        [ForeignKey("ConfigSettingGroup")]
        public int? ConfigSettingGroupId { get; set; }
        public virtual ConfigSettingGroup ConfigSettingGroup { get; set; }
    }
}
