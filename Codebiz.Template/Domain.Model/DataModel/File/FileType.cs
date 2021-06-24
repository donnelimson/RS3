using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model
{
    public class FileType
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FileTypeId { get; set; }
        [Required]
        [MaxLength(25)]
        public string CodeName { get; set; }
        [Required]
        [MaxLength(200)]
        public string MimeType { get; set; }
        [Required]
        [MaxLength(100)]
        public string FileExtension { get; set; }


    }
}
