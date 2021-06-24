using Codebiz.Domain.Common.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codebiz.Domain.Repository;
using Infrastructure.Utilities;
using Codebiz.Domain.Common.Model.DTOs;
using Codebiz.Domain.ERP.Model.Data.ERP;
using Codebiz.Domain.Common.Model.Enums;
using System.IO;
using Codebiz.Domain.Common.Model.DTOs.JobCategory;

namespace Infrastructure.Services
{
    public interface IContentFileService
    {
       
        void InsertOrUpdate(ContentFile contentFile, int appUserId);
        void MoveAttachment(string folder, string fileName);
        void DeleteFile(string folder, string fileName);

        ContentFile GetById(int Id);
        ContentFile GetByFileName(string fileName);



        ContentFileDTO PhotoUnavailable();
        ContentFileDTO ImageUnavailable();
        ContentFileDTO SignatureUnavailable();

        ContentFileDTO GetImageVideoToDisplay(int id);
    

        ContentFileDTO GetCurrentCroppedEmployeePhotoById(int? id);
        ContentFileDTO GetCurrentCroppedEmployeeSignatureById(int? id);
    }

    public class ContentFileService : IContentFileService
    {
        private readonly IContentFileRepository _appContentFileRepository;

        public ContentFileService(
            IContentFileRepository contentFileRepository
        )
        {
            _appContentFileRepository = contentFileRepository;
        }
        
        public void InsertOrUpdate(ContentFile entity, int id)
        {
            if (entity.ContentFileId.Equals(0))
            {
                var now = DateTime.Now;
                entity.CreatedByAppUserId = id;
                entity.CreatedOn = now;
            }
            else
            {
                entity.ModifiedByAppUserId = id;
                entity.ModifiedOn = DateTime.Now;
            }

            _appContentFileRepository.InsertOrUpdate(entity);
        }
        public void MoveAttachment(string folder, string fileName)
        {
            var tempFile = Path.Combine(folder, "temp", fileName);
            var destinationFile = Path.Combine(folder, fileName);
            if (File.Exists(tempFile) && !File.Exists(destinationFile))
            {
                File.Move(tempFile, destinationFile);
            }
        }
        public void DeleteFile(string folder, string fileName)
        {
            var file = Path.Combine(folder, fileName);
            if (File.Exists(file))
            {
                File.Delete(file);
            }
        }

        public ContentFile GetById(int Id)
        {
            return _appContentFileRepository.GetById(Id);
        }
        public ContentFile GetByFileName(string fileName)
        {
            return _appContentFileRepository.GetByFileName(fileName);
        }
        



        public ContentFileDTO GetImageVideoToDisplay(int id)
        {
            return _appContentFileRepository.GetImageVideoToDisplay(id);
        }

        public ContentFileDTO GetCurrentCroppedEmployeePhotoById(int? id)
        {
            return id != null
                ? _appContentFileRepository.GetCurrentCroppedEmployeePhotoById(id.Value) ?? PhotoUnavailable()
                : PhotoUnavailable();
        }

        public ContentFileDTO GetCurrentCroppedEmployeeSignatureById(int? id)
        {
            return id != null
                ? _appContentFileRepository.GetCurrentCroppedEmployeeSignatureById(id.Value) ?? SignatureUnavailable()
                : SignatureUnavailable();
        }


        public ContentFileDTO PhotoUnavailable()
        {
            var contentFile = new ContentFileDTO
            {
                FileName = "photo-unavailable.jpg",
                Folder = "~/assets/global/img/photo-unavailable.jpg",
                FileMimeType = "image/jpeg"
            };

            return contentFile;
        }
        public ContentFileDTO SignatureUnavailable()
        {
            var contentFile = new ContentFileDTO
            {
                FileName = "nosignature.jpg",
                Folder = "~/assets/global/img/nosignature.jpg",
                FileMimeType = "image/jpeg"
            };

            return contentFile;
        }
        public ContentFileDTO ImageUnavailable()
        {
            var contentFile = new ContentFileDTO
            {
                FileName = "image-unavailable.jpg",
                Folder = "~/assets/global/img/image-unavailable.png",
                FileMimeType = "image/png"
            };

            return contentFile;
        }


  
 
    }
}
