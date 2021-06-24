using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models.ViewModels
{
    public class PackagingTypeViewModel
    {
        public int PackagingTypeId { get; set; }
        public List<CodeNameViewModel> PackagingTypes { get; set; }
    }
    public class CodeNameViewModel
    {
        public int PackagingTypeId { get; set; }
        public bool IsDeleted { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }
}
