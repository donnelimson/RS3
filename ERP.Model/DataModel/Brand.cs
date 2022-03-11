using Codebiz.Domain.Common.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Model.DataModel
{
    public class Brand:ModelBase
    {
        public Brand()
        {
            this.BrandItemMasters = new HashSet<BrandItemMaster>();
        }
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public virtual ICollection<BrandItemMaster> BrandItemMasters { get; set; }
    }
}
