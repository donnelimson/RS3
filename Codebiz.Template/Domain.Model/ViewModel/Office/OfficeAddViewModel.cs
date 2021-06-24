using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.ViewModel.Office
{
    public class OfficeAddViewModel
    {
        public List<DepartmentsViewModel> DepartmentData { get; set; }
        public int OfficeId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string BlkLotNo { get; set; }
        public string Street { get; set; }
        public int? ProvinceId { get; set; }
        public int? CityTownId { get; set; }
        public int? BarangayId { get; set; }
        public int? PurokId { get; set; }
        public int? SitioId { get; set; }
        public bool IsMainOffice { get; set; }
    }

    public class DepartmentsViewModel
    {
        public bool IsEditing { get; set; }

        public int DepartmentId { get; set; }
        public string Name { get; set; }
        public DepartmentsDTO Department { get; set; }
        public List<DepartmentDTOs> departments { get; set; }
    }

    public class DepartmentDTOs
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; }

    }

    public class DepartmentsDTO
    {
        public int DepartmentId { get; set; }
        public string Name { get; set; }

    }
}
