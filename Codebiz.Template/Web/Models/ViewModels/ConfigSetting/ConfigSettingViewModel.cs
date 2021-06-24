using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Models.ViewModels.ConfigSetting
{
    public class ConfigSettingViewModel
    {
        [Key]
        public int ConfigSettingId { get; set; }

        [Required]
        [MaxLength(50)]
        [Index("NameIndex", IsUnique = true)]
        public string Name { get; set; }

        [Required]
        [MaxLength(300)]
        public string Description { get; set; }

        [Required]
        [MaxLength(500)]
        public string Value { get; set; }

        [Display(Name = "ConfigSettingDataType")]
        public int ConfigSettingDataTypeId { get; set; }
        public List<SelectListItem> ConfigSettingDataTypeLookUp { get; set; }

        [Display(Name = "ConfigSettingGroup")]
        public int ConfigSettingGroupId { get; set; }
        public List<SelectListItem> ConfigSettingGroupLookUp { get; set; }
    }
}