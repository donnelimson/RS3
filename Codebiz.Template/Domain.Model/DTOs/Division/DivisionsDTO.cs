using System;
using System.ComponentModel;
using Codebiz.Domain.Common.Model.DataModel;

namespace Codebiz.Domain.Common.Model.DTOs.Division
{
    public class DivisionsDTO
    {
        public int DivisionId { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }
        
        public string Office { get; set; }

        public string Department { get; set; }

        [ParameterOrderAttribute(0)]
        [DisplayName("DIVISION")]
        public string Division { get; set; }
        [ParameterOrderAttribute(1)]
        [DisplayName("CATEGORIES")]
        public string Categories { get; set; }

        [ParameterOrderAttribute(2)]
        [DisplayName("POSITIONS")]
        public string Position { get; set; }

        [ParameterOrderAttribute(3)]
        [DisplayName("ACTIVE")]
        public bool IsActive { get; set; }

        [ParameterOrderAttribute(4)]
        [DisplayName("CREATED BY")]
        public string CreatedBy { get; set; }

        [ParameterOrderAttribute(5)]
        [DisplayName("CREATED DATE")]
        public string Date { get; set; }

        public DateTime CreatedDate { get; set; }
    }
   public class DivisionCategoryLookUpDTO
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }

    public class DivisionLookUpDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
