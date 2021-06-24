using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Common.Model.DTOs;
using Codebiz.Domain.Common.Model.DTOs.ExportToExcel;
using Codebiz.Domain.Common.Model.Filter;
using System.Collections.Generic;
using System.Linq;

namespace Codebiz.Domain.Repository
{
    public interface IContentFileRepository : IRepositoryBase<ContentFile>
    {
        ContentFile GetById(int Id);
        ContentFile GetByFileName(string fileName);

        ContentFileDTO GetCurrentCroppedEmployeePhotoById(int id);
        ContentFileDTO GetCurrentCroppedEmployeeSignatureById(int id);
        ContentFileDTO GetImageVideoToDisplay(int id);
    }

    public interface IConfigSettingRepository : IRepositoryBase<ConfigSetting>
    {
        ConfigSetting GetById(int id);
        IEnumerable<ConfigSettingsToExcelCopy> GetDataForExportingToExcel(ConfigSettingsFilter filter);
    }
}
