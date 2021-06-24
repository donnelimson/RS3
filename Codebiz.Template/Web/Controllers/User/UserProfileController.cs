using Codebiz.Domain.Common.Model;
using Codebiz.Domain.Common.Model.Enums;
using Infrastructure;
using Infrastructure.Services;
using System;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Web.Models.ViewModels.UserProfile;
using ConfigSetting = Codebiz.Domain.Common.Model.ConfigSetting;

namespace Web.Controllers
{
    public class UserProfileController : BaseController
    {
        private readonly IAppUserServices _appUserServices;
        private readonly IContentFileService _contentFileService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfigSettingService _configSettingService;
        private readonly IFileTypeService _fileTypeService;
        private readonly IFileServices _fileServices;

        public UserProfileController(IAppUserServices appUserServices
            , IContentFileService contentFileService
            , IUnitOfWork unitOfWork
            , IConfigSettingService configSettingService
            , IFileTypeService fileTypeService
            , IFileServices fileServices
        ) 
            : base(appUserServices)
        {
            _appUserServices = appUserServices;
            _contentFileService = contentFileService;
            _unitOfWork = unitOfWork;
            _configSettingService = configSettingService;
            _fileTypeService = fileTypeService;
            _fileServices = fileServices;
        }

        //[ClaimsAuthorize(ClaimCustomTypes.UserPermissions, PermissionData.UserCanViewConsumerAccountsData)]
        public ActionResult Index()
        {
            var appUser = _appUserServices.GetById(CurrentUser.AppUserId);
            var viewModel = new UserProfileIndexViewModel
            {
                Email = appUser.Employee.Email.Length > 0 ? appUser.Employee.Email : "",
                Username = appUser.Username.Length > 0 ? appUser.Username : "",
                Firstname = appUser.Employee.FirstName.Length > 0 ? appUser.Employee.FirstName : "",
                IsActive = appUser.IsActive,
                Lastname = appUser.Employee.LastName.Length > 0 ? appUser.Employee.LastName : "",
                MiddleName = appUser.Employee.MiddleName.Length > 0 ? appUser.Employee.MiddleName : "",
                UserGroup = appUser.UserGroups.Count > 0
                    ? (from a in appUser.UserGroups select new { a.Description }).FirstOrDefault().Description
                    : "",
                AccessLevel = appUser.AccessLevel != null ? appUser.AccessLevel.Description : "",
                Region = appUser.Employee.Region != null ? appUser.Employee.Region.Name : "",
            };
            return View(viewModel);
        }
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[ClaimsAuthorize(ClaimCustomTypes.UserPermissions, PermissionData.UserCanEditAccountImage)]
        public ActionResult Index(UserProfileIndexViewModel model)
        {
            try
            {
                string path = GetUploadedPhotosDir();
                var path2 = Path.Combine(Path.GetFullPath(path));

                foreach (string requestFile in Request.Files)
                {
                    HttpPostedFileBase file = Request.Files[requestFile];
                    if (file.ContentLength > 0)
                    {
                        string fileName = Path.GetFileName(file.FileName);
                        string directory = path2;
                        FileInfo fileInfo = new FileInfo(fileName);
                        fileName = Guid.NewGuid().ToString() + fileInfo.Extension;
                        if (!Directory.Exists(directory))
                        {
                            Directory.CreateDirectory(directory);
                        }

                        var fileType = GetIdByMimeType(file.ContentType.Contains("image/jpeg")
                            ? file.ContentType.Replace("image/jpeg", "image/jpg")
                            : file.ContentType);
                        var contentFile = new ContentFile
                        {
                            FileName = fileName,
                            ContentFileTypeId = (int)(ContentFileTypes.EmployeePhoto),
                            FileTypeId = fileType.FileTypeId,
                            IsActive = true,
                        };
                        _contentFileService.InsertOrUpdate(contentFile, CurrentUser.AppUserId);
                        _unitOfWork.SaveChanges();

                        _fileServices.CreateFile(file.InputStream,Path.Combine(path2,fileName));

                        //file.SaveAs(path2 + fileName);

                        //int personnelID = (int) CurrentUser.PersonnelId;
                        //var personnel = _personnelService.GetById(personnelID);
                        //personnel.ContentFileId = contentFile.ContentFileId;
                        //;
                        //_personnelService.InsertOrUpdate(personnel, CurrentUser.AppUserId);
                        //_unitOfWork.SaveChanges();
                    }
                }
                CreateSuccessMessage("Profile picture has been updated successfully.");
            }
            catch (UnauthorizedAccessException ex)
            {
                CreateSuccessMessage("Unable to save profile picture, error encountered upon accessing the directory.");
            }

            return RedirectToAction("Index");
        }

        //[OutputCache(Duration = 120, VaryByParam = "none", Location = System.Web.UI.OutputCacheLocation.Client)]
        //public FileContentResult UserPhotos()
        //{
        //    string fullPath = Path.Combine(GetUploadedPhotosDir(),(contentFile != null ? contentFile.FileName : string.Empty));
        //    byte[] imageData = null;
        //    FileInfo fileInfo = new FileInfo(fullPath);
        //    if (fileInfo.Exists)
        //    {
               
        //        long imageFileLength = fileInfo.Length;
        //        FileStream fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read);
        //        BinaryReader br = new BinaryReader(fs);
        //        imageData = br.ReadBytes((int) imageFileLength);
        //        return File(imageData, fileInfo.Extension.Contains("png") ? "image/png" : "image/jpg");
        //    }
        //    else
        //    {
        //        string defaultFileName = HttpContext.Server.MapPath(@"~/App_Data/DefaultPhoto/default.png");
        //        byte[] defaultImageData = null;
        //        FileInfo defaultFileInfo = new FileInfo(defaultFileName);
        //        long imageFileLength = defaultFileInfo.Length;
        //        FileStream fs = new FileStream(defaultFileName, FileMode.Open, FileAccess.Read);
        //        BinaryReader br = new BinaryReader(fs);
        //        defaultImageData = br.ReadBytes((int)imageFileLength);
        //        return File(defaultImageData, "image/png");
        //    }
            
        //}

        public string GetUploadedPhotosDir()
        {            
          ConfigSetting configSetting =  _configSettingService.GetById(Convert.ToInt32(ConfigurationSettings.DisplayPhoto));
          return configSetting.Value.ToString();
        }

        public FileType GetIdByMimeType(string type)
        {
            return _fileTypeService.GetByType(type);
        }

    

    }
}