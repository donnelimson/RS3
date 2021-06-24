using System;
using System.ComponentModel;

namespace Codebiz.Domain.Common.Model.DTOs
{
    public class ComplaintDto
    {
        public int ComplaintTypeId { get; set; }
        public int NatureTypeId { get; set; }

        public string Description { get; set; }

    }
    public class ComplaintSubDto
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }
}
