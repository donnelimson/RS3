namespace Codebiz.Domain.Common.Model.ViewModel
{
    public class FileUploadViewModel
    {
        public int? id { get; set; }
        public int attachmentId { get; set; }
        public int documentTypeId { get; set; }
        public int contentFileId { get; set; }
        public string tempName { get; set; }
        public string name { get; set; }
        public string url { get; set; }
        public string type { get; set; }
        public long? size { get; set; }
        public string thumbnailUrl { get; set; }
        public string deleteUrl { get; set; }
        public string deleteType { get; set; }
        public string documentType { get; set; }
        public int documentId { get; set; }
    }
}
