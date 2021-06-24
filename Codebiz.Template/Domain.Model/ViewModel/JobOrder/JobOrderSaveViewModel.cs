using System.Collections.Generic;

namespace Codebiz.Domain.Common.Model.ViewModel
{
    public class JobOrderSaveViewModel
    {
        public List<JobOrderDataToSave> DataResult { get; set; }
        public string JobOrderType { get; set; }
    }

    public class JobOrderDataToSave
    {
        public int JobOrderDataId { get; set; }
        public string JobOrderData { get; set; }
        public bool ForJORequest { get; set; }
        public bool ForJOComplaint { get; set; }
        public List<int> NatureTypeIds { get; set; }
        public bool IsActive { get; set; }
    }

    public class NatureTypeLookUpDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public bool ForJORequest { get; set; }
        public bool ForJOComplaint { get; set; }
    }
}
