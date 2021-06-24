using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models.ViewModels.CompliantType
{
    public class CompliantTypeViewModel
    {
        public int CompliantTypeId { get; set; }
        public string Code { get; set; }
        public bool IsDelete { get; set; }
        public string Name { get; set; }
        public int? CompliantSubType { get; set; }
    }
}