using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models.ViewModels.CompliantType
{
    public class CompliantSubTypeViewModel
    {
        public int CompliantSubTypeId { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public int? CompliantTypeId { get; set; }
    }
}