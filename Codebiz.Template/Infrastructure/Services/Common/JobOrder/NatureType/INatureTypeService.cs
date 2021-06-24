using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Common.Model.Filter;
using Codebiz.Domain.Common.Model.ViewModel;
using Domain.Repository.NatureType;
using Infrastructure.Utilities;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Infrastructure.Services.Common
{
    public interface IJobOrderNatureTypeService
    {
        void InsertOrUpdate(JobOrderNatureType entity, int currentAppUserId);

        IPagedList<NatureTypeDTO> SearchNatureTypes(JobOrderManagementFilter filter);

        string ExportDataToExcelFile(JobOrderManagementFilter filter, HttpServerUtilityBase server, int currentAppUserId, string currentOffice);
        void ToggleNatureTypeActiveStatus(int id, int currentAppuserId);
        void AddOrUpdate(JobOrderSaveViewModel model, int currentAppUserId);

        IEnumerable<NatureTypeDTO> GetAllNatureTypes(bool permissionToEdit, bool permissionToModifyStatus);
        IEnumerable<NatureTypeDTO> GetAllNatureTypesByIdentification(bool forJOComplaint = false, bool forJORequest = false);

        bool CheckNatureTypeIfInUse(int id);

        IEnumerable<NatureTypeLookUpDTO> GetNatureTypes();
    }

    public class NatureTypeService : IJobOrderNatureTypeService
    {
        private readonly INatureTypeRepository _natureTypeRepository;

        public NatureTypeService
            (
             INatureTypeRepository natureTypeRepository
            )
        {
            _natureTypeRepository = natureTypeRepository;
        }

        public void InsertOrUpdate(JobOrderNatureType entity, int currentAppUserId)
        {
            var now = DateTime.Now;
            if (entity.JobOrderNatureTypeId.Equals(0))
            {
                entity.CreatedByAppUserId = currentAppUserId;
                entity.CreatedOn = now;
            }
            else
            {
                entity.ModifiedByAppUserId = currentAppUserId;
                entity.ModifiedOn = now;
            }
            _natureTypeRepository.InsertOrUpdate(entity);
        }

        public IPagedList<NatureTypeDTO> SearchNatureTypes(JobOrderManagementFilter filter)
        {
            return _natureTypeRepository.SearchNatureTypes(filter);
        }

        public string ExportDataToExcelFile(JobOrderManagementFilter filter, HttpServerUtilityBase server, int currentAppUserId, string currentOffice)
        {
            var data = _natureTypeRepository.GetDataForExportingToExcel(filter).ToList();
            var fileName = ExportToExcelFileHelper.GenerateExcelFile(
                ExportToExcelFileHelper.CreateObjectBy(typeof(NatureTypeDTO)),
                data,
                "Job Order - Nature type_" + DateTime.Now.ToString("MM-dd-yyyy hh mm ss tt"),
                server,
                "Job Order -  Nature types List",
                currentAppUserId, currentOffice);

            return fileName;
        }

        public void ToggleNatureTypeActiveStatus(int id, int currentAppuserId)
        {
            var natureType = _natureTypeRepository.GetById(id);

            natureType.IsActive = !natureType.IsActive;
            InsertOrUpdate(natureType, currentAppuserId);
        }

        public void AddOrUpdate(JobOrderSaveViewModel model, int currentAppUserId)
        {
            List<JobOrderNatureType> datas = _natureTypeRepository.GetAllNotSelected(model.DataResult.Select(a => a.JobOrderDataId).ToList());

            foreach (var item in model.DataResult)
            {
                var data = _natureTypeRepository.GetById(item.JobOrderDataId) ?? new JobOrderNatureType();

                data.Description = item.JobOrderData;
                data.ForJOComplaint = item.ForJOComplaint;
                data.ForJORequest = item.ForJORequest;
                data.IsActive = item.IsActive;
                InsertOrUpdate(data, currentAppUserId);
            }

            if (datas != null)
            {
                foreach (var items in datas)
                {
                    items.IsDeleted = true;
                    InsertOrUpdate(items, currentAppUserId);
                }
            }
        }

        public IEnumerable<NatureTypeDTO> GetAllNatureTypesByIdentification(bool forJOComplaint = false, bool forJORequest = false)
        {
            return _natureTypeRepository.GetAllNatureTypesByIdentification(forJOComplaint, forJORequest);
        }

        public IEnumerable<NatureTypeDTO> GetAllNatureTypes(bool permissionToEdit, bool permissionToModifyStatus)
        {
            return _natureTypeRepository.GetAllNatureTypes(permissionToEdit, permissionToModifyStatus);
        }

        public bool CheckNatureTypeIfInUse(int id)
        {
            return _natureTypeRepository.CheckNatureTypeIfInUse(id);
        }

        public IEnumerable<NatureTypeLookUpDTO> GetNatureTypes()
        {
            var data = _natureTypeRepository.GetAll
                .Where(p => p.IsActive && !p.IsDeleted);

            var dataDTO = data.Select(a => new NatureTypeLookUpDTO
            {
                Id = a.JobOrderNatureTypeId,
                Description = a.Description,
                ForJOComplaint = a.ForJOComplaint,
                ForJORequest = a.ForJORequest
            });

            return dataDTO.AsEnumerable();
        }
    }
}
