using Codebiz.Domain.Common.Model.EnumBaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Codebiz.Domain.Common.Model
{
    public class ContentFile : ModelBase
    {
        [Key]
        public int ContentFileId { get; set; }

        public string FileName { get; set; }

        public long? Size { get; set; }

        [ForeignKey("ContentFileType")]
        public int? ContentFileTypeId { get; set; }
        public virtual ContentFileType ContentFileType { get; set; }

        [ForeignKey("FileType")]
        public int? FileTypeId { get; set; }
        public virtual FileType FileType { get; set; }

        public string OrigFileName { get; set; }
    }
}
