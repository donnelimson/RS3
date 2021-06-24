using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.ViewModel
{
    public class PurposeAddOrUpdateViewModel
    {
        public int Id { get; set; }
        public int TransactionTypeId { get; set; }
        public List<PurposeDetailViewModel> Purposes { get; set; }
    }
    public class PurposeDetailViewModel
    {
        public int Id { get; set; }
        public string Purpose { get; set; }
        public bool CanDelete { get; set; }
        public bool IsActive { get; set; }
    }
}
