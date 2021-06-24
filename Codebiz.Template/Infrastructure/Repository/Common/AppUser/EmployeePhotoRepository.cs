using Codebiz.Domain.Common.Model;
using Codebiz.Domain.ERP.Context;
using Domain.Repository;
using System.Data.Entity;
using System.Linq;

namespace Infrastructure.Repository
{
    public class EmployeePhotoRepository : RepositoryBase<EmployeePhoto>, IEmployeePhotoRepository
    {
        public EmployeePhotoRepository(DbTrackerContext context) : base(context)
        {
        }
        public override void InsertOrUpdate(EmployeePhoto entity)
        {
            if (entity.EmployeeId.Equals(0))
            {
                this.Context.Entry(entity).State = EntityState.Added;
            }
            else
            {
                this.Context.Entry(entity).State = EntityState.Modified;
            }
        }
        public EmployeePhoto GetPhotoThumbnailUrlByEmployeeId(int id)
        {
            return GetAll.OrderByDescending(a => a.EmployeePhotoId).Where(a => a.EmployeeId == id).FirstOrDefault();
        }
    }
}
