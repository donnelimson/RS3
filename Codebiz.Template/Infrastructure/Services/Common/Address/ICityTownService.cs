using System;
using System.Collections.Generic;
using System.Linq;
using PagedList;
using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Repository;
using Codebiz.Domain.Common.Model.Filter;
using System.Web;
using Infrastructure.Utilities;
using Codebiz.Domain.Common.Model.DTOs.CityTown;
using Codebiz.Domain.Common.Model.ViewModel.CityTown;

namespace Infrastructure.Services
{
    public interface ICityTownService
    {
        CityTown GetById(int id);
        void Insert(CityTown entity, int appUserId);
        void Update(CityTown entity, int appUserId);
      //  void Delete(int id);
        bool CheckNameIfExists(string name, int provinceId, int id=0);
        IEnumerable<CityTown> GetAll();
        IEnumerable<CityTown> GetByProvinceId(int provinceId);
        void ReactivateDeactivate(int cityTownId, int appUserId);
        bool CheckIfCityTownCodeExist(string cityTownCode, int cityTownId);
        void InsertOrUpdate(CityTown entity, int appUserId);
        GetDetatilsCityTownDTO GetDetails(int cityTownId);
        IPagedList<CityTownIndexDTO> SearchCityTown(CityTownFilter filter);
        string ExportDataToExcelFile(CityTownFilter filter, HttpServerUtilityBase server, int currentAppUserId, string currentOffice);
        void AddOrUpdate(CityTownAddUpdateViewModel model, int appuserId);
    }

    public class CityTownService : ICityTownService
    {

        private readonly ICityTownRepository _cityTownRepository;
        private readonly IAppUserRepository _appUserRepository;
        public GetDetatilsCityTownDTO GetDetails(int cityTownId)
        {
            return _cityTownRepository.GetDetails(cityTownId);
        }
        public CityTownService(ICityTownRepository cityTownRepository, IAppUserRepository appUserRepository)
        {
            _cityTownRepository = cityTownRepository;
            _appUserRepository = appUserRepository;
        }
        public bool CheckIfCityTownCodeExist(string cityTownCode, int cityTownId)
        {
            return _cityTownRepository.CheckIfCityTownCodeExist(cityTownCode, cityTownId);
        }
        public void InsertOrUpdate(CityTown entity,int appUserId)
        {
            if (entity.CityTownId.Equals(0))
            {
                entity.CreatedByAppUserId = appUserId;
                entity.CreatedOn = DateTime.Now;
                entity.IsActive = true;
            }
            else
            {
                entity.ModifiedByAppUserId = appUserId;
                entity.ModifiedOn = DateTime.Now;
            }
            _cityTownRepository.InsertOrUpdate(entity);
        }
        public CityTown GetById(int id)
        {
            return _cityTownRepository.GetById(id);
        }
        public void Insert(CityTown entity, int appUserId)
        {
            entity.CreatedByAppUserId = appUserId;
            entity.CreatedOn = DateTime.Now;

            //entity.ModifiedByAppUserId = appUserId;
            //entity.ModifiedOn = DateTime.Now;

            _cityTownRepository.InsertOrUpdate(entity);
        }
        public void Update(CityTown entity, int appUserId)
        {
            entity.ModifiedByAppUserId = appUserId;
            entity.ModifiedOn = DateTime.Now;

            _cityTownRepository.InsertOrUpdate(entity);
        }

        //public void Delete(int id)
        //{
        //    var entity = GetById(id);
        //    entity.IsDeleted = true;
        //    entity.IsActive = false;

        //    _cityTownRepository.InsertOrUpdate(entity);
        //}

        public bool CheckNameIfExists(string name, int provinceId, int id=0)
        {
            return _cityTownRepository.CheckNameIfExists(name,provinceId,id);
        }

        public IEnumerable<CityTown> GetAll()
        {
            return _cityTownRepository.GetAllCityTown();
        }
        public IEnumerable<CityTown> GetByProvinceId(int provinceId)
        {
            return _cityTownRepository.GetByProvinceId(provinceId);
        }
        public void ReactivateDeactivate(int cityTownId, int appUserId)
        {
            var entity = _cityTownRepository.GetById(cityTownId);
            if (entity.IsActive)
            {
                entity.IsActive = false;
            }
            else
            {
                entity.IsActive = true;
            }
            entity.ModifiedOn = DateTime.Now;
            entity.ModifiedByAppUserId = appUserId;
            _cityTownRepository.InsertOrUpdate(entity);        }

        public IPagedList<CityTownIndexDTO> SearchCityTown(CityTownFilter filter)
        {
            return _cityTownRepository.SearchCityTown(filter);
        }

        public string ExportDataToExcelFile(CityTownFilter filter, HttpServerUtilityBase server, int currentAppUserId, string currentOffice)
        {
            return ExportToExcelFileHelper.GenerateExcelFile(
               ExportToExcelFileHelper.CreateObjectBy(typeof(CityTownIndexDTO)),
               _cityTownRepository.GetDataForExportingToExcel(filter).ToList(),
               "CityTown_" + DateTime.Now.ToString("MM-dd-yyyy hh mm ss tt"),
               server,
                "CITY TOWN LIST",
               currentAppUserId,currentOffice);
        }
        public void AddOrUpdate(CityTownAddUpdateViewModel model,int appuserId)
        {
            var cityTown = _cityTownRepository.GetById(model.CityTownId);
            if (cityTown == null)
            {
                cityTown = new CityTown();
                cityTown.CreatedOn = DateTime.Now;
                cityTown.IsActive = true;
                cityTown.CreatedByAppUserId = appuserId;
            }
            else
            {
                cityTown.ModifiedOn = DateTime.Now;
                cityTown.ModifiedByAppUserId = appuserId;
            }
            cityTown.CityTownCode = model.CityTownCode;
            cityTown.Name = model.Name;
            cityTown.ProvinceId = model.ProvinceId;
            cityTown.Abbreviation = model.abbv;
            cityTown.Latitude = model.Latitude;
            cityTown.Longitude = model.Longitude;
            cityTown.ZipCode = model.ZipCode;
            _cityTownRepository.InsertOrUpdate(cityTown);
        }
    }
}