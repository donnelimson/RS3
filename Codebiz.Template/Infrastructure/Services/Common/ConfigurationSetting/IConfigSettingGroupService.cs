using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface IConfigSettingGroupService
    {
        ConfigSettingGroup GetById(int id);
        IEnumerable<ConfigSettingGroup> GetAll();
    }

    public class ConfigSettingGroupService : IConfigSettingGroupService
    {

        private readonly IConfigSettingGroupRepository _configSettingGroupRepository;

        public ConfigSettingGroupService(IConfigSettingGroupRepository configSettingGroupRepository)
        {
            _configSettingGroupRepository = configSettingGroupRepository;
        }

        public ConfigSettingGroup GetById(int id)
        {
            return _configSettingGroupRepository.GetAll.FirstOrDefault(a => a.ConfigSettingGroupId == id);
        }

        public IEnumerable<ConfigSettingGroup> GetAll()
        {
            return _configSettingGroupRepository.GetAll.AsEnumerable();
        }
    }

}
