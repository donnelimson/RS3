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
   public class LogRepository : RepositoryBase<Log> , ILogRepository
    {
        public LogRepository(AppCommonContext context) : base(context)
        {

        }

        public override void InsertOrUpdate(Log entity)
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
        public Log GetByLogEventTitleId(int logEventTitleId)
        {
            var wasap = GetAll.OrderByDescending(x=>x.Id).Where(x => x.LogEventTitleId == logEventTitleId);
            var data = GetAll.OrderByDescending(x => x.Id).FirstOrDefault(x=>x.LogEventTitleId == logEventTitleId);
            return data;
        }
    }
}
