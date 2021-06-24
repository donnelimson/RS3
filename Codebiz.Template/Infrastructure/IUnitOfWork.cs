using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class ApplicationContext
    {
        public IEnumerable<DbContext> Contexts { get; set; }
    }
    public interface IUnitOfWork
    {
        void SaveChanges();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _applicationContext;

        public UnitOfWork(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public void SaveChanges()
        {
            foreach (var dbContext in _applicationContext.Contexts)
            {
                dbContext.SaveChanges();
            }
        }
    }
}
