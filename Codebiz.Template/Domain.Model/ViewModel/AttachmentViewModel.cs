using Codebiz.Domain.Common.Model.DTOs;
using System.Collections.Generic;

namespace Codebiz.Domain.Common.Model.ViewModel
{
    public class AttachmentViewModel
    {
        public int documentTypeId { get; set; }
        public string documentTypeName { get; set; }
        public string tempName { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public string type { get; set; }
        public long? size { get; set; }
        public string thumbnailUrl { get; set; }
        public string deleteUrl { get; set; }
        public string downloadUrl { get; set; }
        public string deleteType { get; set; }
    }
}
