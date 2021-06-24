using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Repository;
using Domain.Context;
using System.Data.Entity;

namespace Infrastructure.Repository
{
    class LoginHistoryRepository : RepositoryBase<LoginHistory>,ILoginHistoryRepository
    {
        public LoginHistoryRepository(AppCommonContext context) : base(context)
        {

        }

        public override void InsertOrUpdate(LoginHistory entity)
        {
            if (entity.LoginHistoryId.Equals(0))
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
