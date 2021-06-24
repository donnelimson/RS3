using Codebiz.Domain.Common.Model.DTOs.Division;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.ViewModel.Department
{
    public class DepartmentViewModel
    {
        public List<PositionsDataViewModel> Details { get; set; }
        public int DepartmentId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
    }

    public class PositionsDataViewModel
    {
        public int PositionId { get; set; }
        public string PositionName { get; set; }
        public int? DivisionId { get; set; }
        public string DivisionName { get; set; }
        public int? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<DivisionCategoryLookUpDTO> Categories { get; set; }
    }
}
