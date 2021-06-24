using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Common.Model.Filter.MeterReadingRemarks;
using Codebiz.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository.Billing.MeterReadingRemarks
{
    public interface IMeterReadingRemarksRepository : IRepositoryBase<MeterReadingRemark>
    {
        MeterReadingRemark GetById(int id);
        IEnumerable<MeterReadingRemark> GetAllMeterReadingRemarks();
        MeterReadingRemark GetallById(int? meterReadingRemarkId);
        T SearchMeterReadingRemarksOrExportToExcel<T>(MeterReadingRemarksFilter filter);
        bool IsCodeExist(string code, int meterReadingRemarkId);
        bool IsNameExist(string name, int meterReadingRemarkId);
    }
}
