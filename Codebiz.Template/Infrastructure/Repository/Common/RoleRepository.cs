using Codebiz.Domain.Common.Model.DataModel;
using Codebiz.Domain.Common.Model.DTOs;
using Codebiz.Domain.Repository;
using Domain.Context;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Infrastructure.Repository.Common
{
    public interface IRoleRepository : IRepositoryBase<Role>
    {
        List<Select2LookUpDTO> GetRolesForSelect2LookUp();
    }
    public class RoleRepository : RepositoryBase<Role>, IRoleRepository
    {
        public RoleRepository(AppCommonContext context) : base(context)
        {
        }
        public override void InsertOrUpdate(Role entity)
        {
            if (entity.Id.Equals(0))
            {
                this.Context.Entry(entity).State = EntityState.Added;
            }
            else
            {
                this.Context.Entry(entity).State = EntityState.Modified;
            }
        }
        public List<Select2LookUpDTO> GetRolesForSelect2LookUp()
        {
            return GetAll.Select(a => new Select2LookUpDTO {Id = a.Id, Description = a.Description }).ToList();
        }
    }
}
