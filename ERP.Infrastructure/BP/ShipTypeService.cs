using Codebiz.Domain.ERP.Context;
using Codebiz.Domain.ERP.Model.Data.Accounts;
using Codebiz.Domain.ERP.Repository.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.ERP.Infrastructure.BP
{
    public class ShipTypeService
    {
        private readonly AccountDataContext _ctx;
        private readonly ShipTypeRepository _repository;
        public ShipTypeService(AccountDataContext ctx)
        {
            _ctx = ctx;
            _repository = new ShipTypeRepository(_ctx);
        }

        public ShipType Add(ShipType p1) => _repository.Add(p1);

        public ShipType SaveOrUpdate(ShipType p1) => _repository.SaveOrUpdate(p1);

        public IQueryable<ShipType> GetList(Expression<Func<ShipType, bool>> q) => _repository.GetList(q);

        public IQueryable<ShipType> GetListEx(Expression<Func<ShipType, bool>> q, params string[] _includes) => _repository.GetListEx(q, _includes);

        public int Commit() => _repository.Commit();

    }
}
