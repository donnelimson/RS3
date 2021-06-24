using System;
using System.Collections.Generic;
using System.Linq;
using PagedList;
using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Repository;
using Codebiz.Domain.Common.Model.DTOs;
using Codebiz.Domain.Common.Model.Filter;
using System.Web;
using Infrastructure.Utilities;
using Codebiz.Domain.Common.Model.DTOs.ExportToExcel;
using Codebiz.Domain.Common.Model.ViewModel.Region;

namespace Infrastructure.Services
{
    public interface IRegionService
    {
        Region GetById(int id);
        void InsertOrUpdate(Region entity, int appUserId);
        bool CheckNameIfExists(string name, int id);
        bool CheckAbbreviationIfExists(string abbreviation, int id);

        IEnumerable<Region> GetAll();
        IEnumerable<Region> GetAllActive();
        IPagedList<RegionDTO> SearchRegions(RegionFilter regionFilter);
        string ExportDataToExcelFile(RegionFilter filter, HttpServerUtilityBase server, int currentAppUserId, string currentOffice);

        void AddUpdateRegion(RegionViewModel model ,int currentUserId);
        void ToggleRegionActiveStatus(int id , int currentUserId);

        bool CheckRegionIfInUse(int id);
        RegionDTO GetRegionDetails(int id);

    }

    public class RegionService : IRegionService
    {

        private readonly IRegionRepository _regionRepository;

        public RegionService(IRegionRepository regionRepository)
        {
            _regionRepository = regionRepository;
        }

        #region Get All / By Id 

        public IEnumerable<Region> GetAllActive()
        {
            return _regionRepository.GetAll.Where(a => a.IsActive).AsEnumerable();
        }
        public Region GetById(int id)
        {
            return _regionRepository.GetById(id);
        }

        public IEnumerable<Region> GetAll()
        {
            return _regionRepository.GetAll;
        }
        public RegionDTO GetRegionDetails(int id)
        {
            var data = _regionRepository.GetById(id);

            var dataDTO = new RegionDTO
            {
               RegionId = data.RegionId,
                Name = data.Name,
                Abbreviation = data.Abbreviation,
            };

            return dataDTO;
        }

        #endregion

        #region Insert Or Update

        public void InsertOrUpdate(Region entity, int appUserId)
        {
            var now = DateTime.Now;
            if (entity.RegionId.Equals(0))
            {
                entity.IsActive = true;
                entity.CreatedByAppUserId = appUserId;
                entity.CreatedOn = now;
            }
            else
            {
                entity.ModifiedByAppUserId = appUserId;
                entity.ModifiedOn = now;
            }

            _regionRepository.InsertOrUpdate(entity);
        }

        #endregion

        #region Validations

        public bool CheckNameIfExists(string name, int id)
        {
            return _regionRepository.CheckNameIfExists(name, id);
        }

        public bool CheckAbbreviationIfExists(string abbreviation, int id)
        {
            return _regionRepository.CheckAbbreviationIfExists(abbreviation, id);
        }

        public bool CheckRegionIfInUse(int id)
        {
            return _regionRepository.CheckRegionIfInUse(id);
        }

        #endregion

        #region Search Regions

        public IPagedList<RegionDTO> SearchRegions(RegionFilter regionFilter)
        {
            return _regionRepository.SearchRegions(regionFilter);
        }

        #endregion

        #region Export Data To Excel File

        public string ExportDataToExcelFile(RegionFilter filter, HttpServerUtilityBase server, int currentAppUserId, string currentOffice)
        {
            var data = _regionRepository.GetDataForExportingToExcel(filter).ToList();
            var fileName = ExportToExcelFileHelper.GenerateExcelFile(
                ExportToExcelFileHelper.CreateObjectBy(typeof(RegionDTO)),
                data,
                "Region_" + DateTime.Now.ToString("MM-dd-yyyy hh mm ss tt"),
                server,
                "Region List",
                currentAppUserId, currentOffice);

            return fileName;
        }

        #endregion

        #region Add Or Update Region

        public void AddUpdateRegion(RegionViewModel model, int currentUserId)
        {
            var region = _regionRepository.GetById(model.RegionId) ?? new Region();

            region.Name = model.Name;
            region.Abbreviation = model.Abbreviation;

            InsertOrUpdate(region, currentUserId);
        }

        #endregion

        #region Reactivate / Deactivate

        public void ToggleRegionActiveStatus(int id, int currentUserId)
        {
            var region = _regionRepository.GetById(id);

            region.IsActive = !region.IsActive;
            InsertOrUpdate(region, currentUserId);
        }

        #endregion
    }
}
