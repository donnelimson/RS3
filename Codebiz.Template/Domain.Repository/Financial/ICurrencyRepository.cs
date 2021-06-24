using Codebiz.Domain.Common.Model.DataModel.Financial.Currency;
using Codebiz.Domain.Common.Model.Filter.Currency;
using Codebiz.Domain.Common.Model.Financial;
using Codebiz.Domain.Repository;
using System.Collections.Generic;
using System.Linq;

namespace Domain.Repository.Financial
{
    public interface ICurrencyRepository : IRepositoryBase<Currency>
    {
        Currency GetById(int Id);

        bool IsCodeExists(string code, int id);
        IQueryable<Currency> GetData(CurrencyFilter filter);

        #region Currency Enums

        IEnumerable<Decimals> DecimalEnums();
        IEnumerable<ISOCurrencyCode> ISOCurrencyCodeEnums();
        IEnumerable<Rounding> RoundingEnums();

        #endregion
    }
}
