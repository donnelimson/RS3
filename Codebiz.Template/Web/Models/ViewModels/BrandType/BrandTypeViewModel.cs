using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models.ViewModels.BrandType
{
    public class BrandTypeViewModel
    {
        public int BrandTypeId { get; set; }
        public string CodeName { get; set; }
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }

    }
}