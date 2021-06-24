using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models.DTO
{
    public class SearchAppUserDTO
    {
        public int Id { get; set; }
        public int AppUserId { get; set; }
        public string Name { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        public string Area { get; set; }
        public string Office { get; set; }
        public bool IsSelected { get; set; }
        public int LabelId { get; set; }
        public int ApprovalStageApproverId { get; set; }
    }
}