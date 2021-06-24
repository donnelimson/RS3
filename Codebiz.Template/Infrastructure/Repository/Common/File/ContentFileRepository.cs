using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Common.Model.DTOs;
using Codebiz.Domain.Common.Model.Enums.CommercialServicesApplication;
using Codebiz.Domain.Repository;
using Domain.Context;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Infrastructure.Repository
{
    public class ContentFileRepository : RepositoryBase<ContentFile>, IContentFileRepository
    {

        public ContentFileRepository(AppCommonContext context) : base(context)
        {
        }
        public ContentFile GetById(int Id)
        {
          return Context.Set<ContentFile>().FirstOrDefault(a => a.ContentFileId == Id && a.IsActive);
        }
        public ContentFile GetByFileName(string fileName)
        {
          return Context.Set<ContentFile>().FirstOrDefault(a => a.FileName == fileName && a.IsActive);
        }
        public override void InsertOrUpdate(ContentFile entity)
        {
            if (entity.ContentFileId.Equals(0))
            {
                this.Context.Entry(entity).State = EntityState.Added;
            }
            else
            {
                this.Context.Entry(entity).State = EntityState.Modified;
            }
        }


 



        public ContentFileDTO GetCurrentCroppedEmployeePhotoById(int id)
        {
            var currentCroppedPhoto = (from cf in Context.Set<ContentFile>()
                                       join emp in Context.Set<EmployeePhoto>()
                                       on cf.ContentFileId equals emp.CropImageContentFileId
                                       where emp.EmployeeId == id && emp.IsActive
                                       orderby emp.EmployeePhotoId descending
                                       select new ContentFileDTO
                                       {
                                           FileName = cf.FileName,
                                           Folder = cf.ContentFileType.ConfigSettingFolder.Value,
                                           FileMimeType = cf.FileType.MimeType
                                       }).FirstOrDefault();

            return currentCroppedPhoto;
        }

        public ContentFileDTO GetCurrentCroppedEmployeeSignatureById(int id)
        {
            var currentCroppedPhoto = (from cf in Context.Set<ContentFile>()
                                       join emp in Context.Set<EmployeeSignature>()
                                       on cf.ContentFileId equals emp.CropImageContentFileId
                                       where emp.EmployeeId == id && emp.IsActive
                                       orderby emp.EmployeeSignatureId descending
                                       select new ContentFileDTO
                                       {
                                           FileName = cf.FileName,
                                           Folder = cf.ContentFileType.ConfigSettingFolder.Value,
                                           FileMimeType = cf.FileType.MimeType
                                       }).FirstOrDefault();

            return currentCroppedPhoto;
        }

        public ContentFileDTO GetImageVideoToDisplay(int id)
        {
            return GetAll.Where(x => x.ContentFileId == id).Select(a => new ContentFileDTO
            {
                FileName = a.FileName,
                Folder = a.ContentFileType.ConfigSettingFolder.Value,
                FileMimeType = a.FileType.MimeType
            }).FirstOrDefault();
        }


   

    }
}
