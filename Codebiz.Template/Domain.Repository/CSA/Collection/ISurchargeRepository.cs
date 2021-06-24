using Codebiz.Domain.Common.Model.DataModel.Collection;
using Codebiz.Domain.Common.Model.DTOs;
using Codebiz.Domain.Common.Model.DTOs.ExportToExcel;
using Codebiz.Domain.Common.Model.Filter;
using Codebiz.Domain.Repository;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository.Collection
{
    public interface ISurchargeRepository : IRepositoryBase<Surcharge>
    {
        Surcharge GetById(int id);
        IEnumerable<Surcharge> GetAllSurcharge();
        Surcharge GetallById(int? surchargeId);
        IPagedList<SurchargeDTO> SearchSurcharge(SurchargeFilter surchargeFilter);
        bool IsConsumerClassandYearOfDelinquencyExist(int? consumerClass, int id, int yearOfDelinquency);
        IEnumerable<SurchargeToExcel> GetDataForExportingToExcel(SurchargeFilter filter);

    }
}
