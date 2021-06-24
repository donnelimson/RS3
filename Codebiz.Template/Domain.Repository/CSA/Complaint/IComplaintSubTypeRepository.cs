using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public interface IComplaintSubTypeRepository : IRepositoryBase<ComplaintSubType>
    {
        ComplaintSubType GetById(int id);
        ComplaintSubType GetAllById(int? compliantSubTypeId);
        bool CheckIfNameIsExist(string name, int id=0);
        bool CheckIfNameIsExist(List<ComplaintSubType> modelSub);
    }
}
