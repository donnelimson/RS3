using System;
using System.Collections.Generic;
using System.Text;

namespace Codebiz.Domain.Common.Model.DTOs
{
    public class AjaxResult
    {
        public string Message { get; set; } = "";
        public bool Success { get; set; } = true;

        public LogEventTitles LogEventTitle { get; set; }
        public static StringBuilder BaseModelLogs { get; set; } = null;
        public string Ref1 { get; set; }
        public DateTime? DateRef1 { get; set; }
        public int IntRef1 { get; set; }
        public string Action { get; set; }
        public string Module { get; set; }
        #region Attachment
        public static List<FIleNameAndUrl> Attachments { get; set; } = new List<FIleNameAndUrl>();
        #endregion
        

        //    public static List<string> FileUploadAttachments { get; set; } = new List<string>();
    }
    public class FIleNameAndUrl
    {
        public string FileName { get; set; }
        public string FileUploadUrl { get; set; }
    }
}
