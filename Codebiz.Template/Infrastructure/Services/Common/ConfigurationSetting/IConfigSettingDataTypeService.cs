using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
   public interface IConfigSettingDataTypeService
    {
        ConfigSettingDataType GetById(int id);
        IEnumerable<ConfigSettingDataType> GetAll();
    }

    public class ConfigSettingDataTypeService : IConfigSettingDataTypeService
    {
        private readonly IConfigSettingDataTypeRepository _configSettingDataTypeRepository;

        public ConfigSettingDataTypeService(IConfigSettingDataTypeRepository configSettingDataTypeRepository)
        {
            _configSettingDataTypeRepository = configSettingDataTypeRepository;
        }
        public ConfigSettingDataType GetById(int id)
        {
            return _configSettingDataTypeRepository.GetAll.FirstOrDefault(a => a.ConfigSettingDataTypeId == id);
        }
        public IEnumerable<ConfigSettingDataType> GetAll()
        {
            return _configSettingDataTypeRepository.GetAll.AsEnumerable();
        }


    }
}
