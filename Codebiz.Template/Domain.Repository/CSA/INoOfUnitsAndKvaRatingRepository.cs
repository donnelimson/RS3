using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Common.Model.DTOs.ExportToExcel;
using Codebiz.Domain.Common.Model.DTOs.NoOfUnitsAndKvaRating;
using Codebiz.Domain.Common.Model.Filter;
using Codebiz.Domain.Repository;
using PagedList;
using System.Collections.Generic;

namespace Domain.Repository
{
    public interface INoOfUnitsAndKvaRatingRepository : IRepositoryBase<NoOfUnitsAndKvaRating>
    {
        NoOfUnitsAndKvaRating GetById(int id);
        IEnumerable<NoOfUnitsAndKvaRating> GetAllNoOfUnitsAndKvaRating();
        NoOfUnitsAndKvaRating GetallById(int? noOfUnitsKvaId);
        bool IsNoOfUnitsExist(int noOfUnits, int id);
        bool IsNoOfUnitsAndKvaExist(int noOfUnits, int id, decimal kvaRating);
        IPagedList<NoOfUnitsAndKvaRatingDTO> SearchNoOfUnitsAndKvaRating(NoOfUnitsAndKvaRatingFilter noOfUnitsAndKvaRatingFilter);
        bool IsKvaRatingExist(decimal kvaRating, int id);
        decimal GetKVARating(int? id);
        IEnumerable<NoOfUnitsAndKvaRatingToExcel> GetDataForExportingToExcel(NoOfUnitsAndKvaRatingFilter filter);
        List<decimal> GetKVARatingsByNoOfUnit(int noOfUnits);
        NoOfUnitsAndKvaRating GetByUnitAndRating(int unit, decimal rating);
    }
}
