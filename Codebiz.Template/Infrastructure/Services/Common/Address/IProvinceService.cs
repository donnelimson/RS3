using System;
using System.Collections.Generic;
using System.Linq;
using PagedList;
using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Common.Model.DTOs;
using Codebiz.Domain.Common.Model.Filter;
using Codebiz.Domain.Repository;
using System.Web;
using Infrastructure.Utilities;
using Codebiz.Domain.Common.Model.DTOs.ExportToExcel;

namespace Infrastructure.Services
{
    public interface IProvinceService
    {
        Province GetById(int id);
        void InsertOrUpdate(Province entity, int appUserId);
        bool CheckAbbreviationIfExists(string abbreviation, int? regionId, int id);
        bool CheckNameIfExists(string name, int? regionId, int id);

        IEnumerable<Province> GetAll();
        IEnumerable<Province> GetAllActive();
        IPagedList<ProvinceDTO> SearchProvinces(ProvinceFilter provinceFilter);
        IEnumerable<Province> GetAllProvinceByRegionId(int regionId);
        string ExportDataToExcelFile(ProvinceFilter filter, HttpServerUtilityBase server, int currentAppUserId, string currentOffice);
        int GetTarlacProvinceId();
    }

    public class ProvinceService : IProvinceService
    {

        private readonly IProvinceRepository _provinceRepository;

        public ProvinceService(IProvinceRepository provinceRepository)
        {
            _provinceRepository = provinceRepository;
        }

        public Province GetById(int id)
        {
            return _provinceRepository.GetById(id);
        }
        public void InsertOrUpdate(Province entity, int appUserId)
        {
            var now = DateTime.Now;
            if (entity.ProvinceId.Equals(0))
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
            _provinceRepository.InsertOrUpdate(entity);
        }

        public bool CheckAbbreviationIfExists(string abbreviation, int? regionId, int id)
        {
            return _provinceRepository.CheckAbbreviationIfExists(abbreviation,regionId, id);
        }

        public bool CheckNameIfExists(string name, int? regionId, int id)
        {
            return _provinceRepository.CheckNameIfExists(name, regionId, id);
        }

        public IEnumerable<Province> GetAll()
        {
            return _provinceRepository.GetAll;
        }

        public IPagedList<ProvinceDTO> SearchProvinces(ProvinceFilter provinceFilter)
        {
            return _provinceRepository.SearchProvinces(provinceFilter);
        }

        public IEnumerable<Province> GetAllActive()
        {
            return _provinceRepository.GetAll.Where(a => a.IsActive).AsEnumerable();
        }
        public IEnumerable<Province>GetAllProvinceByRegionId(int regionId)
        {
            return _provinceRepository.GetAllProvinceByRegionId(regionId);
        }
        public string ExportDataToExcelFile(ProvinceFilter filter, HttpServerUtilityBase server, int currentAppUserId, string currentOffice)
        {
            var data = _provinceRepository.GetDataForExportingToExcel(filter).ToList();
            var fileName = ExportToExcelFileHelper.GenerateExcelFile(
                ExportToExcelFileHelper.CreateObjectBy(typeof(ProvinceToExcel)),
                data,
                "Province_" + DateTime.Now.ToString("MM-dd-yyyy hh mm ss tt"),
                server,
                "Province List",
                currentAppUserId, currentOffice);

            return fileName;
        }
        public int GetTarlacProvinceId()
        {
            return _provinceRepository.GetTarlacProvinceId();
        }
    }
}
