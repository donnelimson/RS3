using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.DTOs.ExportToExcel
{
    public class ConfigSettingsToExcelCopy
    {
        [DisplayName("NAME")]
        public string Name { get; set; }
        [DisplayName("DESCRIPTION")]
        public string Description { get; set; }
        [DisplayName("VALUE")]
        public string Value { get; set; }
        [DisplayName("DATA TYPE")]
        public string ConfigSettingDataType { get; set; }
        [DisplayName("DATA GROUP")]
        public string ConfigSettingGroup { get; set; }
        [DisplayName("CREATED BY")]
        public string CreatedBy { get; set; }
        [DisplayName("DATE CREATED")]
        public DateTime? CreatedDate { get; set; }
    }
}
