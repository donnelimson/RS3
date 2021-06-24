using Codebiz.Domain.Common.Model.DataModel.Financials;
using Codebiz.Domain.Common.Model.Filter.Financials;
using Codebiz.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository.Financials
{
    public interface IFinancialProjectsRepository : IRepositoryBase<FinancialProject>
    {
        FinancialProject GetById(int id);
        IEnumerable<FinancialProject> GetAllFinancialProjects();
        FinancialProject GetallById(int? financialProjectsId);
        T SearchFinancialProjectsOrExportToExcel<T>(FinancialProjectsFilter filter);
        bool IsCodeExist(string code, int financialProjectsId);
        bool IsNameExist(string name, int financialProjectsId);
    }
}
