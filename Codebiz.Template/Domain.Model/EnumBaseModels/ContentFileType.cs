using Codebiz.Domain.Common.Model.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.EnumBaseModels
{
    public class ContentFileType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ContentFileTypeId { get; set; }

        [MaxLength(100)]
        public string Name { get; set; }
        public string Description { get; set; }

        [ForeignKey("ConfigSettingFolder")]
        public int? ConfigSettingFolderId { get; set; }
        public virtual ConfigSetting ConfigSettingFolder { get; set; }
    }
}
