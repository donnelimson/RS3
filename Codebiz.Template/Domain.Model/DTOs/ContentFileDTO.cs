using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.DTOs
{
    public class ContentFileDTO
    {
        public int ContentFileId { get; set; }
        public string FileName { get; set; }
        public string OrigFileName { get; set; }
        public string Folder { get; set; }
        public string FileMimeType { get; set; }
        public string FileType { get; set; }
        public string FileExtension { get; set; }
        public long? FileSize { get; set; }
    }
}
