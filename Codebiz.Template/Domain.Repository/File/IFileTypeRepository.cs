using Codebiz.Domain.Common.Model;

namespace Codebiz.Domain.Repository
{
    public interface IFileTypeRepository : IRepositoryBase<FileType>
    {
        FileType GetByType(string type);
    }
}
