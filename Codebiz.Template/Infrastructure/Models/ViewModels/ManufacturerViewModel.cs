using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models.ViewModels
{
    public class ManufacturerViewModel
    {
        public int ManufacturerId { get; set; }
        public List<ManufacturerNameViewModel> Manufacturers { get; set; }
    }
    public class ManufacturerNameViewModel
    {
        public int ManufacturerId { get; set; }
        public bool IsDeleted { get; set; }
        public string Name { get; set; }
    }
}
