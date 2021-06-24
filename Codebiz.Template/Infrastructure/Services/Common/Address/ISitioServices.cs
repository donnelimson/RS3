using System;
using System.Collections.Generic;
using System.Linq;
using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Repository;
using PagedList;
using Codebiz.Domain.Common.Model.Filter;
using System.Web;
using Infrastructure.Utilities;
using Codebiz.Domain.Common.Model.ViewModel.Sitio;
using Codebiz.Domain.Common.Model.DTOs.Sitio;

namespace Infrastructure.Services
{
    public interface ISitioServices
    {

        Sitio GetById(int id);
        Sitio GetByisActive();
        Sitio GetAllById(int sitioId);
        void InsertOrUpdate(Sitio entity, int appUserId);
        List<string> GetAllSitioCode();
        bool IsSitioNameExists(string name, int? purokdId, int id);
        bool IsSitioCodeExists(string code, int id);
        IEnumerable<Sitio> GetAll();
        IEnumerable<Sitio> GetAllByBarangayId(int id);
        IPagedList<SitioDTO> SearchSitio(SitioFilter filter);
        string ExportDataToExcelFile(SitioFilter filter, HttpServerUtilityBase server, int currentAppUserId, string currentOffice);

        void AddUpdateSitio(SitioViewModel model, int currentAppuserId);
        void DeleteSitio(int id, int currentAppuserId);
        void ToggleSitioActiveStatus(int id, int currentAppuserId);
    }

    public class SitioServices : ISitioServices
    {
        private readonly ISitioRepository _sitioRepository;

        public SitioServices(
            ISitioRepository sitioRepository
        )
        {
            _sitioRepository = sitioRepository;
        }

        public IEnumerable<Sitio> GetAll()
        {
            return _sitioRepository.GetAllSitios().Where(a => !a.IsDeleted).AsEnumerable();
        }

        public Sitio GetAllById(int sitioId)
        {
            return _sitioRepository.GetById(sitioId);
        }

        public List<string> GetAllSitioCode()
        {
            return _sitioRepository.GetAll.Where(x => !x.IsDeleted).Select(a => a.Code).ToList();
        }

        public List<string> GetAllSitioName()
        {
            return _sitioRepository.GetAll.Where(x => !x.IsDeleted).Select(a => a.Name).ToList();
        }


        public Sitio GetById(int id)
        {
            return _sitioRepository.GetById(id);
        }

        public Sitio GetByisActive()
        {
            return _sitioRepository.GetAll.Where(a => a.IsActive).FirstOrDefault();
        }

        public void InsertOrUpdate(Sitio entity, int appUserId)
        {
            var now = DateTime.Now;
            if (entity.SitioId.Equals(0))
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
            _sitioRepository.InsertOrUpdate(entity);
        }

        public IPagedList<SitioDTO> SearchSitio(SitioFilter filter)
        {
            return _sitioRepository.SearchSitio(filter);
        }

     
        public IEnumerable<Sitio> GetAllByBarangayId(int id)
        {
            return _sitioRepository.GetAllByBarangayId(id).Where(x => !x.IsDeleted && x.IsActive);
        }

        #region Export Data to Excel

        public string ExportDataToExcelFile(SitioFilter filter, HttpServerUtilityBase server, int currentAppUserId, string currentOffice)
        {
            var data = _sitioRepository.GetDataForExportingToExcel(filter).ToList();
            var fileName = ExportToExcelFileHelper.GenerateExcelFile(
                ExportToExcelFileHelper.CreateObjectBy(typeof(SitioDTO)),
                data,
                "Sitio_" + DateTime.Now.ToString("MM-dd-yyyy hh mm ss tt"),
                server,
                "Sitio List",
                currentAppUserId, currentOffice);

            return fileName;
        }

        #endregion

        #region Validations

  
        public bool IsSitioNameExists(string name, int? purokdId, int id)
        {
            return _sitioRepository.IsSitioNameExists(name, purokdId, id);
        }

        public bool IsSitioCodeExists(string code, int id)
        {
            return _sitioRepository.IsSitioCodeExists(code, id);
        }


        #endregion

        #region Add or Update

        public void AddUpdateSitio(SitioViewModel model, int currentAppuserId)
        {
            var sitio = _sitioRepository.GetById(model.SitioId) ?? new Sitio();

            sitio.Code = model.Code;
            sitio.Name = model.Name;
            sitio.ProvinceId = model.ProvinceId;
            sitio.CityTownId = model.CityTownId;
            sitio.BarangayId = model.BarangayId;
            sitio.PurokId = model.PurokId;

            InsertOrUpdate(sitio, currentAppuserId);
        }

        #endregion

        #region Delete

        public void DeleteSitio(int id, int currentAppuserId)
        {
            var sitio = _sitioRepository.GetById(id);

            sitio.IsDeleted = true;
            InsertOrUpdate(sitio, currentAppuserId);
        }

        #endregion

        #region Reactivate / Deactivate

        public void ToggleSitioActiveStatus(int id, int currentAppuserId)
        {
            var sitio = _sitioRepository.GetById(id);

            sitio.IsActive = !sitio.IsActive;
            InsertOrUpdate(sitio, currentAppuserId);
        }

        #endregion
    }
}
