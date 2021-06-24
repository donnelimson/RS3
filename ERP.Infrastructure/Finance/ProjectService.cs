using Codebiz.Domain.ERP.Context;
using Codebiz.Domain.ERP.Model.Data;
using Codebiz.Domain.ERP.Model.Data.Financials;
using Codebiz.Domain.ERP.Repository.Financials;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.ERP.Infrastructure.Finance
{
    public class ProjectService
    {
        private FinanceDataContext _ctx;
        private ProjectRepository _repository;
        public ProjectService(FinanceDataContext ctx)
        {
            _ctx = ctx;
            _repository = new ProjectRepository(_ctx);
        }


        public Project Add(Project p1) => _repository.Add(p1);

        public Project SaveOrUpdate(Project p1) => _repository.SaveOrUpdate(p1);

        public IQueryable<Project> GetList(Expression<Func<Project, bool>> q) => _repository.GetList(q);

        public IQueryable<Project> GetListEx(Expression<Func<Project, bool>> q, params string[] _includes) => _repository.GetListEx(q, _includes);

        public IQueryable<Project> GetListByPage(Paging p)
        {
            var skip = (p.Page - 1) * p.PageSize;
            var res = _repository.GetListByPage(x => x.ProjectCode , skip, p.PageSize);
            return res;
        }

        public int Commit() => _repository.Commit();
    }
}
