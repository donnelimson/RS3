using Codebiz.Domain.ERP.Model.Data.Inventory;
using Codebiz.Domain.ERP.Repository.Inventory;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Codebiz.Domain.ERP.Infrastructure.Inventory
{
    public class ItemMasterService : IDisposable
    {


        private ItemMasterRepository _repository;

        private DbContext _ctx;

        public ItemMasterService(DbContext ctx)
        {
            _repository = new ItemMasterRepository(ctx);
            _ctx = ctx;
        }


        public ItemMaster Add(ItemMaster p1) => _repository.Add(p1);

        public ItemMaster SaveOrUpdate(ItemMaster p1) => _repository.SaveOrUpdate(p1);

        public IQueryable<ItemMaster> GetList(Expression<Func<ItemMaster, bool>> q) => _repository.GetList(q);

        public IQueryable<ItemMaster> GetListEx(Expression<Func<ItemMaster, bool>> q, params string[] _includes) => _repository.GetListEx(q, _includes);

        public int Commit() => _repository.Commit();

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
