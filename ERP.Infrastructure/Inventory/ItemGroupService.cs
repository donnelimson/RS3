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
    public class ItemGroupService : IDisposable
    {
        private ItemGroupRepository _repository;

        private DbContext _ctx;

        public ItemGroupService(DbContext ctx)
        {
            _repository = new ItemGroupRepository(ctx);
            _ctx = ctx;
        }


        public ItemGroup Add(ItemGroup p1) => _repository.Add(p1);

        public ItemGroup SaveOrUpdate(ItemGroup p1) => _repository.SaveOrUpdate(p1);

        public IQueryable<ItemGroup> GetList(Expression<Func<ItemGroup, bool>> q) => _repository.GetList(q);

        public IQueryable<ItemGroup> GetListEx(Expression<Func<ItemGroup, bool>> q, params string[] _includes) => _repository.GetListEx(q, _includes);

        public int Commit() => _repository.Commit();

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
