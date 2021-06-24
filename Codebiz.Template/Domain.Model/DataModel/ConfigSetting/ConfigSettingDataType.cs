using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Codebiz.Domain.Common.Model
{
    public class ConfigSettingDataType
    {
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ConfigSettingDataTypeId { get; set; }

        [Required]
        [MaxLength(50)]
        [Index("CodeNameIndex", IsUnique = true)]
        public string CodeName { get; set; }      
    }
}
