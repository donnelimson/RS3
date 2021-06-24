using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Codebiz.Domain.ERP.Model.Data.Inventory;
using Codebiz.Domain.ERP.Repository.Inventory;

namespace Codebiz.Domain.ERP.Infrastructure.Inventory
{
    public class PriceListService : IDisposable
    {
        private PriceListRepository _repository;

        private DbContext _ctx;

        public PriceListService(DbContext ctx)
        {
            _repository = new PriceListRepository(ctx);
            _ctx = ctx;
        }


        public PriceList Add(PriceList p1) => _repository.Add(p1);

        public PriceList SaveOrUpdate(PriceList p1) => _repository.SaveOrUpdate(p1);

        public IQueryable<PriceList> GetList(Expression<Func<PriceList, bool>> q) => _repository.GetList(q);

        public IQueryable<PriceList> GetListEx(Expression<Func<PriceList, bool>> q, params string[] _includes) => _repository.GetListEx(q, _includes);

        public int Commit() => _repository.Commit();

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
