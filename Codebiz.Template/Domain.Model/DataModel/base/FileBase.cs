using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Codebiz.Domain.Common.Model
{
    public class FileBase
    {
        [MaxLength(500)]
        public string Name { get; set; }

        [ForeignKey("ContentFile")]
        public int? ContentFileId { get; set; }
        public virtual ContentFile ContentFile { get; set; }
        public bool IsActive { get; set; }
    }
}
