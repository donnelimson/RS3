using PagedList;
using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codebiz.Domain.Common.Model.Filter;
using Infrastructure.Utilities.QueryHelper;
using Codebiz.Domain.Common.Model.DTOs;
using System.Web;
using Infrastructure.Utilities;
using Codebiz.Domain.Common.Model.DTOs.ExportToExcel;

namespace Infrastructure.Services
{
    public interface IConfigSettingService
    {
        ConfigSetting GetById(int id);
        string GetValueById(int id);
        String GetStringValueById(int id);
        Int32 GetInt32ValueById(int id);
        Boolean GetBooleanValueById(int id);
        ConfigSetting GetByName(string name);
        void Update(ConfigSetting entity, int appUserId);
        bool CheckNameIfExists(string name, int ConfigSettingDataTypeId, int ConfigSettingGroupId, int id = 0);
        IEnumerable<ConfigSetting> GetAll();
        IPagedList<ConfigSettingsDTO> Search(ConfigSettingsFilter configSettingsFilter);
     
    }

    public class ConfigSettingService : IConfigSettingService
    {
        private readonly IConfigSettingRepository _configSettingRepository;

        public ConfigSettingService(IConfigSettingRepository configSettingRepository)
        {
            _configSettingRepository = configSettingRepository;
        }

        public ConfigSetting GetById(int id)
        {
            return _configSettingRepository.GetAll.FirstOrDefault(a => a.ConfigSettingId == id && a.IsActive);
        }

        public ConfigSetting GetByName(string name)
        {
            return _configSettingRepository.GetAll.FirstOrDefault(a => a.Name == name && a.IsActive);
        }

        public void Update(ConfigSetting entity, int appUserId)
        {
            entity.ModifiedByAppUserId = appUserId;
            entity.ModifiedOn = DateTime.Now;

            _configSettingRepository.InsertOrUpdate(entity);
        }

        public bool CheckNameIfExists(string name, int configSettingDataTypeId, int configSettingGroupId, int id = 0)
        {
            return _configSettingRepository.GetAll.Where(o => o.ConfigSettingId != id)
                                        .Where(o => (o.ConfigSettingDataTypeId == configSettingDataTypeId
                                        || o.ConfigSettingGroupId == configSettingGroupId) && o.IsActive)
                                        .Any(o => o.Name == name);
        }

        public IEnumerable<ConfigSetting> GetAll()
        {
            return _configSettingRepository.GetAll.Where(a => a.IsActive).AsEnumerable();
        }

        public IPagedList<ConfigSettingsDTO> Search(ConfigSettingsFilter configSettingsFilter)
        {
            var data = _configSettingRepository.GetAll.Where(a => a.IsActive == true);

            if (!string.IsNullOrEmpty(configSettingsFilter.SearchTerm))
            {
                data = data.Where(a => a.Name.Contains(configSettingsFilter.SearchTerm)
                                    || a.Description.Contains(configSettingsFilter.SearchTerm)
                                    || a.Value.Contains(configSettingsFilter.SearchTerm)
                                    || a.ConfigSettingDataType.CodeName.Contains(configSettingsFilter.SearchTerm)
                                    || a.ConfigSettingGroup.CodeName.Contains(configSettingsFilter.SearchTerm));
            }

            configSettingsFilter.FilteredRecordCount = data.Count();
            var dataDTO = data.Select(a => new ConfigSettingsDTO
            {
                ConfigSettingId = a.ConfigSettingId,
                Name = a.Name,
                Description = a.Description,
                Value = a.Value,
                ConfigSettingDataType = a.ConfigSettingDataType.CodeName,
                ConfigSettingGroup = a.ConfigSettingGroup.CodeName,
                IsActive = a.IsActive,
                CreatedBy = a.CreatedByAppUser.Employee.FirstName + " " + a.CreatedByAppUser.Employee.LastName,
                CreatedDate = a.CreatedOn
            });
            dataDTO = QueryHelper.Ordering(dataDTO, configSettingsFilter.SortColumn, configSettingsFilter.SortDirection != "asc", false);

            return dataDTO.ToPagedList(configSettingsFilter.Page, configSettingsFilter.PageSize);
        }

        public string GetValueById(int id)
        {
            return _configSettingRepository.GetAll.Where(a => a.ConfigSettingId == id && a.IsActive).Select(a=>a.Value)
                .FirstOrDefault();
        }

        public Int32 GetInt32ValueById(int id)
        {
            var value = _configSettingRepository.GetAll.Where(a => a.ConfigSettingId == id && a.IsActive).Select(a => a.Value).FirstOrDefault();

            if (value == null)
            {
                return default(Int32);
            }

            return Convert.ToInt32(value);
        }

        public string GetStringValueById(int id)
        {
            var value = _configSettingRepository.GetAll.Where(a => a.ConfigSettingId == id && a.IsActive).Select(a => a.Value).FirstOrDefault();

            if (value == null)
            {
                return default(String);
            }

            return Convert.ToString(value);
        }

        public bool GetBooleanValueById(int id)
        {
            var value = _configSettingRepository.GetAll.Where(a => a.ConfigSettingId == id && a.IsActive).Select(a => a.Value).FirstOrDefault();

            if (value == null)
            {
                return default(Boolean);
            }

            return Convert.ToBoolean(value);
        }

    }
}