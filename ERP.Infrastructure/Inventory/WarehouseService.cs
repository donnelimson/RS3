using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Codebiz.Domain.ERP.Model.Data.Inventory;
using Codebiz.Domain.ERP.Repository.Inventory;

namespace Codebiz.Domain.ERP.Infrastructure.Inventory
{
    public class WarehouseService : IDisposable
    {
        private WarehouseRepository _repository;

        private DbContext _ctx;

        public WarehouseService(DbContext ctx)
        {
            _repository = new WarehouseRepository(ctx);
            _ctx = ctx;
        }


        public Warehouse Add(Warehouse p1) => _repository.Add(p1);

        public Warehouse SaveOrUpdate(Warehouse p1) => _repository.SaveOrUpdate(p1);

        public IQueryable<Warehouse> GetList(Expression<Func<Warehouse, bool>> q) => _repository.GetList(q);

        public IQueryable<Warehouse> GetListEx(Expression<Func<Warehouse, bool>> q, params string[] _includes) => _repository.GetListEx(q, _includes);

        public int Commit() => _repository.Commit();

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
