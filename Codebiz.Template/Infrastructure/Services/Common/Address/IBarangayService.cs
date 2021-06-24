using PagedList;
using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using Codebiz.Domain.Common.Model.Filter;
using System.Web;
using Infrastructure.Utilities;
using Codebiz.Domain.Common.Model.DTOs.ExportToExcel;
using Codebiz.Domain.Common.Model.DTOs.Route;
using Codebiz.Domain.Common.Model.ViewModel.Barangay;
using Codebiz.Domain.Common.Model.DTOs.Barangay;

namespace Infrastructure.Services
{
    public interface IBarangayService
    {
        Barangay GetById(int id);
        IEnumerable<Barangay> GetAll();
        IEnumerable<Barangay> GetAllByCityTownId(int CityTownId);
        //IPagedList<BarangayIndexDTO> Search(DataSearch filter, string barangayName);
        void ReactivateDeactivate(Barangay entity, int appUserId);
        bool CheckIfBarangayCodeExist(string barangayCode, int barangayId, int cityTownId);
        bool CheckIfBarangayNameExist(string barangayName, int barangayId, int cityTownId);
        void InsertOrUpdate(Barangay entity, int appUserId);
        GetDetailsBarangayDTO GetDetails(int barangayId);
        IPagedList<BarangayIndexDTO> SearchBarangay(BarangayFilter filter);
        string ExportDataToExcelFile(BarangayFilter filter, HttpServerUtilityBase server, int currentAppUserId, string currentOffice);
        void ReactivateDeactivate(int barangayId, int appuserId);
        void AddOrUpdate(BarangayAddOrUpdateViewModel model, int appuserId);
    }

    public class BarangayService : IBarangayService
    {
        private readonly IBarangayRepository _barangayRepository;

        public BarangayService(IBarangayRepository barangayRepository)
        {
            _barangayRepository = barangayRepository;
        }
        public Barangay GetById(int id)
        {
            return _barangayRepository.GetById(id);
        }
        public IEnumerable<Barangay> GetAll()
        {
            return _barangayRepository.GetAllBarangay();
        }
        public IEnumerable<Barangay> GetAllByCityTownId(int CityTownId)
        {
            return _barangayRepository.GetAllByCityTownId(CityTownId);
        }
        public bool CheckIfBarangayCodeExist(string barangayCode, int barangayId, int cityTownId)
        {
            return _barangayRepository.CheckIfBarangayCodeExist(barangayCode, barangayId, cityTownId);
        }

        public bool CheckIfBarangayNameExist(string barangayName, int barangayId, int cityTownId)
        {
            return _barangayRepository.CheckIfBarangayNameExist(barangayName, barangayId, cityTownId);
        }
        public void InsertOrUpdate(Barangay entity,int appUserId)
        {
            if (entity.BarangayId.Equals(0))
            {
                entity.CreatedByAppUserId = appUserId;
                entity.CreatedOn = DateTime.Now;
                entity.IsActive = true; 
            }
            else
            {
                entity.ModifiedOn = DateTime.Now;
                entity.ModifiedByAppUserId = appUserId;
            }
            _barangayRepository.InsertOrUpdate(entity);
        }
        public GetDetailsBarangayDTO GetDetails(int barangayId)
        {
            return _barangayRepository.GetDetails(barangayId);
        }
        public void ReactivateDeactivate (Barangay entity, int appUserId)
        {
            if (entity.IsActive)
            {
                entity.IsActive = false;
            }
            else
            {
                entity.IsActive=true;
            }
            entity.ModifiedOn = DateTime.Now;
            entity.ModifiedByAppUserId = appUserId;
            _barangayRepository.InsertOrUpdate(entity);
        }


        public IPagedList<BarangayIndexDTO> SearchBarangay(BarangayFilter filter)
        {
            return _barangayRepository.SearchBarangay(filter);
        }

        public string ExportDataToExcelFile(BarangayFilter filter, HttpServerUtilityBase server, int currentAppUserId, string currentOffice)
        {
            return ExportToExcelFileHelper.GenerateExcelFile(
              ExportToExcelFileHelper.CreateObjectBy(typeof(BarangayToExcel)),
              _barangayRepository.GetDataForExportingToExcel(filter).ToList(),
              "Barangays_" + DateTime.Now.ToString("MM-dd-yyyy hh mm ss tt"),
              server,
               "Barangay List",
              currentAppUserId,currentOffice);
        }
        public void ReactivateDeactivate(int barangayId,int appuserId)
        {
            var entity = _barangayRepository.GetById(barangayId);
            if (entity.IsActive)
            {
                entity.IsActive = false;
            }
            else
            {
                entity.IsActive = true;
            }
            entity.ModifiedOn = DateTime.Now;
            entity.ModifiedByAppUserId = appuserId;
            _barangayRepository.InsertOrUpdate(entity);
        }
        public void AddOrUpdate(BarangayAddOrUpdateViewModel model, int appuserId)
        {
            var barangay = _barangayRepository.GetById(model.BarangayId);
            if (barangay == null)
            {
                barangay = new Barangay();
                barangay.CreatedOn = DateTime.Now;
                barangay.CreatedByAppUserId = appuserId;
                barangay.IsActive = true;
            }
            else
            {
                barangay.ModifiedByAppUserId = appuserId;
                barangay.CreatedByAppUserId = appuserId;  
            }
            barangay.BarangayCode = model.BarangayCode;
            barangay.Name = model.Name;
            barangay.Longitude = model.Longitude;
            barangay.Latitude = model.Latitude;
            barangay.CityTownId = model.CityTownId;
            _barangayRepository.InsertOrUpdate(barangay);
        }
    }
}
