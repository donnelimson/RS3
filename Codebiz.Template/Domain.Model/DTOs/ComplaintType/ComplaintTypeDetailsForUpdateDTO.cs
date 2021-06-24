using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.Common.Model.DTOs.ComplaintType
{
    public class ComplaintTypeDetailsForUpdateDTO
    {
        public int ComplaintTypeId { get; set; }
        public string ComplaintTypeName { get; set; }
        public int? NatureTypeId { get; set; }
        public List<ComplaintSubTypeDTO> ComplaintSubTypes { get; set; }
    }

    public class ComplaintSubTypeDTO
    {
        public int SubTypeId { get; set; }
        public string Complaint { get; set; }
        public bool IsActive { get; set; }
    }
}
