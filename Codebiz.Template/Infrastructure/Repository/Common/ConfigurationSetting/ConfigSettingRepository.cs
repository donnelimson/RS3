using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Common.Model.DTOs.ExportToExcel;
using Codebiz.Domain.Common.Model.Filter;
using Codebiz.Domain.Repository;
using Domain.Context;
using Infrastructure.Utilities.QueryHelper;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Infrastructure.Repository
{
    public class ConfigSettingRepository : RepositoryBase<ConfigSetting>, IConfigSettingRepository
    {
        public ConfigSettingRepository(AppCommonContext context) : base(context)
        { 
        }

        public ConfigSetting GetById(int id)
        {
            return this.GetAll.FirstOrDefault(a => a.ConfigSettingId == id && a.IsActive);
        }

        public IEnumerable<ConfigSettingsToExcelCopy> GetDataForExportingToExcel(ConfigSettingsFilter filter)
        {
            var data = GetData(filter);

            var dataDTO = data.ToList().Select(a => new ConfigSettingsToExcelCopy
            {
                Name = a.Name,
                Description = a.Description,
                Value = a.Value,
                ConfigSettingDataType = a.ConfigSettingDataType.CodeName,
                ConfigSettingGroup = a.ConfigSettingGroup.CodeName,
                CreatedBy = a.CreatedByAppUser.Employee.FirstName + " " + a.CreatedByAppUser.Employee.LastName,
                CreatedDate = a.CreatedOn
            });

            dataDTO = QueryHelper.Ordering(dataDTO.AsQueryable(), filter.SortColumn, filter.SortDirection != "asc", false);

            return dataDTO.AsEnumerable();
        }

        private IQueryable<ConfigSetting> GetData(ConfigSettingsFilter filter)
        {
            var data = GetAll.Where(a => a.IsActive);

            if (!string.IsNullOrWhiteSpace(filter.SearchTerm))
            {
                data = data.Where(a => a.Name.Contains(filter.SearchTerm.Trim()));
            }

            if (!string.IsNullOrWhiteSpace(filter.SearchTerm))
            {
                data = data.Where(a => a.Description.Contains(filter.SearchTerm.Trim()));
            }
            if (!string.IsNullOrWhiteSpace(filter.SearchTerm))
            {
                data = data.Where(a => a.Value.Contains(filter.SearchTerm.Trim()));
            }
            if (!string.IsNullOrWhiteSpace(filter.SearchTerm))
            {
                data = data.Where(a => a.ConfigSettingDataType.CodeName.Contains(filter.SearchTerm.Trim()));
            }
            if (!string.IsNullOrWhiteSpace(filter.SearchTerm))
            {
                data = data.Where(a => a.ConfigSettingGroup.CodeName.Contains(filter.SearchTerm.Trim()));
            }

            if (!string.IsNullOrWhiteSpace(filter.CreatedBy))
            {
                data = data.Where(a => (a.CreatedByAppUser.Employee.FirstName + " " + a.CreatedByAppUser.Employee.LastName)
                    .Trim().Contains(filter.CreatedBy.Trim()));
            }

            if (filter.CreatedOnFrom != null && filter.CreatedOnTo != null)
            {
                data = data.Where(a => DbFunctions.TruncateTime(a.CreatedOn) >= filter.CreatedOnFrom && DbFunctions.TruncateTime(a.CreatedOn) <= filter.CreatedOnTo);
            }

            return data;
        }

        public override void InsertOrUpdate(ConfigSetting entity)
        {
            if (entity.ConfigSettingId.Equals(0))
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
