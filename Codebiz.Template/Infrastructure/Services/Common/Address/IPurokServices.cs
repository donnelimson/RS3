using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Common.Model.DTOs;
using Codebiz.Domain.Common.Model.DTOs.ExportToExcel;
using Codebiz.Domain.Common.Model.DTOs.Purok;
using Codebiz.Domain.Common.Model.Filter;
using Codebiz.Domain.Common.Model.ViewModel.Purok;
using Codebiz.Domain.Repository;
using Infrastructure.Utilities;
using PagedList;

namespace Infrastructure.Services
{
    public interface IPurokServices
    {
        Purok GetById(int id);
        void InsertOrUpdate(Purok entity, int appUserId);
        bool IsCodeExists(string code, int id);
        bool IsNameExists(string name,int? barangayId,int id);

        IEnumerable<Purok> GetAllByBarangayId(int BarangayId);
        IPagedList<PurokDTO> SearchPuroks(PurokFilter purokFilter);
        string ExportDataToExcelFile(PurokFilter filter, HttpServerUtilityBase server, int currentAppUserId, string currentOffice);


        void AddUpdatePurok(PurokViewModel model, int currentAppuserId);
        void DeletePurok(int id, int currentAppuserId);
        void TogglePurokActiveStatus(int id, int currentAppuserId);
        PurokDTO GetPurokDetails(int id);
    }

    public class PurokServices : IPurokServices
    {
        private readonly IPurokRepository _purokRepository;


        public PurokServices(
            IPurokRepository purokRepository)
        {
            _purokRepository = purokRepository;
        }

        #region Get Details / By Id

        public Purok GetById(int id)
        {
            return _purokRepository.GetById(id);
        }

        public PurokDTO GetPurokDetails(int id)
        {
            var data = _purokRepository.GetById(id);

            var dataDTO = new PurokDTO
            {
                PurokId = data.PurokId,
                Code = data.Code,
                Name = data.Name,
                ProvinceId = data.ProvinceId,
                CityTownId = data.CityTownId,
                BarangayId = data.BarangayId
            };

            return dataDTO;
        }

        #endregion

        #region Insert Or Update

        public void InsertOrUpdate(Purok entity, int appUserId)
        {
            var now = DateTime.Now;
            if (entity.PurokId.Equals(0))
            {
                entity.IsActive = true;
                entity.CreatedByAppUserId = appUserId;
                entity.CreatedOn = now;
                entity.IsDeleted = false;
            }
            else
            {
                entity.ModifiedByAppUserId = appUserId;
                entity.ModifiedOn = now;
            }
            _purokRepository.InsertOrUpdate(entity);
        }

        #endregion

        #region Search 

        public IPagedList<PurokDTO> SearchPuroks(PurokFilter purokFilter)
        {
            return _purokRepository.SearchPurok(purokFilter);
        }

        #endregion

        public IEnumerable<Purok> GetAllByBarangayId(int BarangayId)
        {
            return _purokRepository.GetAllByBarangayId(BarangayId).Where(x => !x.IsDeleted);
        }

        #region Validations

        public bool IsCodeExists(string code, int id)
        {
            return _purokRepository.IsCodeExists(code, id);
        }

        public bool IsNameExists(string name, int? barangayId, int id)
        {
            return _purokRepository.IsNameExists(name, barangayId, id);
        }



        #endregion

        #region Export data to excel file

        public string ExportDataToExcelFile(PurokFilter filter, HttpServerUtilityBase server, int currentAppUserId, string currentOffice)
        {
            var data = _purokRepository.GetDataForExportingToExcel(filter).ToList();
            var fileName = ExportToExcelFileHelper.GenerateExcelFile(
                ExportToExcelFileHelper.CreateObjectBy(typeof(PurokDTO)),
                data,
                "Purok_" + DateTime.Now.ToString("MM-dd-yyyy hh mm ss tt"),
                server,
                "Purok List",
                currentAppUserId, currentOffice);

            return fileName;
        }

        #endregion

        #region Add or Update

        public void AddUpdatePurok(PurokViewModel model, int currentAppuserId)
        {
            var purok = _purokRepository.GetById(model.PurokId) ?? new Purok();

            purok.Code = model.Code;
            purok.Name = model.Name;
            purok.ProvinceId = model.ProvinceId;
            purok.CityTownId = model.CityTownId;
            purok.BarangayId = model.BarangayId;

            InsertOrUpdate(purok, currentAppuserId);
        }

        #endregion

        #region Delete

        public void DeletePurok(int id, int currentAppuserId)
        {
            var purok = _purokRepository.GetById(id);

            purok.IsDeleted = !purok.IsDeleted;
            InsertOrUpdate(purok, currentAppuserId);
        }

        #endregion

        #region Reactivate / Deactivate

        public void TogglePurokActiveStatus(int id, int currentAppuserId)
        {
            var purok = _purokRepository.GetById(id);

            purok.IsActive = !purok.IsActive;
            InsertOrUpdate(purok, currentAppuserId);
        }

        #endregion
    }
}
