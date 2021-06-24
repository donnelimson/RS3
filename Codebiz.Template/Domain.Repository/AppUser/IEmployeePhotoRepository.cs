using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public interface IEmployeePhotoRepository : IRepositoryBase<EmployeePhoto>
    {
        EmployeePhoto GetPhotoThumbnailUrlByEmployeeId(int id);
    }
}
