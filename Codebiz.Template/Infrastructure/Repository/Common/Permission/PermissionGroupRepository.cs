using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Context;

namespace Infrastructure.Repository
{
    public class PermissionGroupRepository : RepositoryBase<PermissionGroup>, IPermissionGroupRepository
    {
        public PermissionGroupRepository(AppCommonContext context) : base(context)
        {
        }

        public override void InsertOrUpdate(PermissionGroup entity)
        {
            if (entity.PermissionGroupId.Equals(0))
            {
                this.Context.Entry(entity).State = EntityState.Added;
            }
            else
            {
                this.Context.Entry(entity).State = EntityState.Modified;
            }
        }
    }
}
