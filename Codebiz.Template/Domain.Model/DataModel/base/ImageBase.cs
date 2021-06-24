using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model
{
    public class ImageBase : FileBase
    {
        [MaxLength(500)]
        public string CropImageName { get; set; }

        [ForeignKey("CropImageContentFile")]
        public int? CropImageContentFileId { get; set; }
        public virtual ContentFile CropImageContentFile { get; set; }
    }
}
