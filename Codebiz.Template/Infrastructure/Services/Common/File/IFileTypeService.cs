using Codebiz.Domain.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Repository;
using Codebiz.Domain.Repository;

namespace Infrastructure.Services
{
   public interface IFileTypeService
   {
       FileType GetByType(string type);
       IEnumerable<FileType> GetAll();
    }

    public class FileTypeService : IFileTypeService
    {
        private readonly IFileTypeRepository _fileTypeRepository;

        public FileTypeService(IFileTypeRepository fileTypeRepository)
        {
            _fileTypeRepository = fileTypeRepository;
        }
        public FileType GetByType(string type)
        {
            return _fileTypeRepository.GetByType(type);
        }

        public IEnumerable<FileType> GetAll()
        {
            return _fileTypeRepository.GetAll.AsEnumerable();
        }
      
    }
}
