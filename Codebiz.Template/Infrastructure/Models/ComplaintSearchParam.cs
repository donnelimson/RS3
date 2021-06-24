using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models
{
    public class ComplaintSearchParam : NgTablePagingBaseParam
    {
        public string ComplaintNumber { get; set; }
        public int CaseCategoryId { get; set; }
        public int CaseSubCategoryId { get; set; }
        public DateTime? DateAndTime { get; set; }
    }
}
