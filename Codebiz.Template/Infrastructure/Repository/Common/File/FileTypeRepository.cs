using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Repository;
using Domain.Context;
using System.Data.Entity;
using System.Linq;

namespace Infrastructure.Repository
{
    public class FileTypeRepository : RepositoryBase<FileType>, IFileTypeRepository
    {
        public FileTypeRepository(AppCommonContext context) : base(context)
        { 
        }

        public FileType GetByType(string type)
        {
            return this.GetAll.FirstOrDefault(a => a.MimeType == type);
        }

        public override void InsertOrUpdate(FileType entity)
        {
            if (entity.FileTypeId.Equals(0))
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
