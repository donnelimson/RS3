//using Codebiz.Domain.Common.Model.Enums;
//using Infrastructure.Services;
//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.Drawing.Drawing2D;
//using System.Drawing.Imaging;
//using System.IO;
//using System.Linq;
//using System.Web;
//using System.Web.Helpers;
//using System.Web.Hosting;
//using System.Web.Mvc;
//using Web.Areas.CommercialServicesApplication.Models.ViewModels.FileUpload;
//using Web.Helpers;
//using static Web.Areas.CommercialServicesApplication.Controllers.FileUploadController;

//namespace Web.Controllers
//{
//    public class AppUserFileUploadController : Controller
//    {
//        private readonly IConfigSettingService _configSettingService;

//        public AppUserFileUploadController(IConfigSettingService configSettingService)
//        {
//            _configSettingService = configSettingService;
//        }

//        String tempPath = @"C:\AppUsersAttachmentFolder\Temp\";
//        String serverMapPath = @"C:\AppUsersAttachmentFolder";

//        private string StorageRoot
//        {
//            get
//            {
//                return serverMapPath;
//            }
//        }
//        private string UrlBase = @"C:\AppUsersAttachmentFolder\";
//        String DeleteURL = "/AppUserFileUpload/DeleteFile/?file=";
//        String DeleteType = "GET";

//        //public ActionResult Show()
//        //{
//        //    JsonFiles ListOfFiles = this.HelperGetFileList();
//        //    var model = new FilesViewModel()
//        //    {
//        //        Files = ListOfFiles.files
//        //    };

//        //    return View(model);
//        //}

//        [HttpPost]
//        public JsonResult Upload()
//        {
//            var resultList = new List<ViewDataUploadFilesResult>();

//            var CurrentContext = HttpContext;

//            this.UploadAndShowResults(CurrentContext, resultList);
//            JsonFiles files = new JsonFiles(resultList);

//            bool isEmpty = !resultList.Any();
//            if (isEmpty)
//            {
//                return Json("Error ");
//            }
//            else
//            {
//                return Json(files);
//            }
//        }

//        public JsonResult GetFileList()
//        {
//            var list = HelperGetFileList();
//            return Json(list, JsonRequestBehavior.AllowGet);
//        }

//        [HttpGet]
//        public JsonResult DeleteFile(string file)
//        {
//            this.HelperDeleteFile(file);
//            return Json("OK", JsonRequestBehavior.AllowGet);
//        }

//        #region FilesHelper
//        public void DeleteFiles(String pathToDelete)
//        {

//            string path = HostingEnvironment.MapPath(pathToDelete);

//            System.Diagnostics.Debug.WriteLine(path);
//            if (Directory.Exists(path))
//            {
//                DirectoryInfo di = new DirectoryInfo(path);
//                foreach (FileInfo fi in di.GetFiles())
//                {
//                    System.IO.File.Delete(fi.FullName);
//                    System.Diagnostics.Debug.WriteLine(fi.Name);
//                }

//                di.Delete(true);
//            }
//        }

//        public String HelperDeleteFile(String file)
//        {
//            System.Diagnostics.Debug.WriteLine("DeleteFile");
//            //    var req = HttpContext.Current;
//            System.Diagnostics.Debug.WriteLine(file);

//            String fullPath = Path.Combine(StorageRoot, file);
//            System.Diagnostics.Debug.WriteLine(fullPath);
//            System.Diagnostics.Debug.WriteLine(System.IO.File.Exists(fullPath));
//            String thumbPath = "/" + file + "80x80.jpg";
//            String partThumb1 = Path.Combine(StorageRoot, "thumbs");
//            String partThumb2 = Path.Combine(partThumb1, Path.GetFileNameWithoutExtension(file.Remove(0, 37)) + "80x80.jpg");

//            System.Diagnostics.Debug.WriteLine(partThumb2);
//            System.Diagnostics.Debug.WriteLine(System.IO.File.Exists(partThumb2));
//            if (System.IO.File.Exists(fullPath))
//            {
//                //delete thumb 
//                if (System.IO.File.Exists(partThumb2))
//                {
//                    System.IO.File.Delete(partThumb2);
//                }
//                System.IO.File.Delete(fullPath);
//                String succesMessage = "Ok";
//                return succesMessage;
//            }
//            String failMessage = "Error Delete";
//            return failMessage;
//        }

//        public JsonFiles HelperGetFileList()
//        {

//            var r = new List<ViewDataUploadFilesResult>();

//            String fullPath = Path.Combine(StorageRoot);
//            if (Directory.Exists(fullPath))
//            {
//                DirectoryInfo dir = new DirectoryInfo(fullPath);
//                foreach (FileInfo file in dir.GetFiles())
//                {
//                    int SizeInt = unchecked((int)file.Length);
//                    r.Add(UploadResult(file.Name, SizeInt, file.FullName));
//                }

//            }
//            JsonFiles files = new JsonFiles(r);

//            return files;
//        }

//        public void UploadAndShowResults(HttpContextBase ContentBase, List<ViewDataUploadFilesResult> resultList)
//        {
//            var httpRequest = ContentBase.Request;
//            System.Diagnostics.Debug.WriteLine(Directory.Exists(tempPath));

//            String fullPath = Path.Combine(StorageRoot);
//            Directory.CreateDirectory(fullPath);
//            // Create new folder for thumbs
//            Directory.CreateDirectory(fullPath + "/thumbs/");

//            foreach (String inputTagName in httpRequest.Files)
//            {

//                var headers = httpRequest.Headers;

//                var file = httpRequest.Files[inputTagName];
//                System.Diagnostics.Debug.WriteLine(file.FileName);

//                if (string.IsNullOrEmpty(headers["X-File-Name"]))
//                {

//                    UploadWholeFile(ContentBase, resultList);
//                }
//                else
//                {

//                    UploadPartialFile(headers["X-File-Name"], ContentBase, resultList);
//                }
//            }
//        }

//        private void UploadWholeFile(HttpContextBase requestContext, List<ViewDataUploadFilesResult> statuses)
//        {

//            var request = requestContext.Request;
//            for (int i = 0; i < request.Files.Count; i++)
//            {
//                var file = request.Files[i];
//                String pathOnServer = Path.Combine(StorageRoot);

//                var guid = Guid.NewGuid().ToString();
//                var uniqueFilename = $"{guid}_{file.FileName}";

//                var fullPath = Path.Combine(pathOnServer, Path.GetFileName(uniqueFilename));
//                file.SaveAs(fullPath);

//                //Create thumb
//                string[] imageArray = file.FileName.Split('.');
//                if (imageArray.Length != 0)
//                {
//                    String extansion = imageArray[imageArray.Length - 1].ToLower();
//                    if (extansion != "jpg" && extansion != "png" && extansion != "jpeg") //Do not create thumb if file is not an image
//                    {
//                        //if (extansion == "docx" || extansion == "doc")
//                        //{
//                        //    var ThumbfullPath = Path.Combine(pathOnServer, "thumbs");
//                        //    var fileWord = File("/Content/Image/word.png", "image/png");
//                        //    String fileThumb = Path.GetFileNameWithoutExtension(fileWord.FileName) + "80x80.jpg";
//                        //    var ThumbfullPath2 = Path.Combine(ThumbfullPath, fileThumb);
//                        //    using (MemoryStream stream = new MemoryStream(System.IO.File.ReadAllBytes(fullPath)))
//                        //    {
//                        //        var thumbnail = new WebImage(stream).Resize(570, 570);
//                        //        thumbnail.Save(ThumbfullPath2, "jpg");
//                        //    }
//                        //}
//                    }
//                    else
//                    {
//                        var ThumbfullPath = Path.Combine(pathOnServer, "thumbs");
//                        //String fileThumb = file.FileName + ".80x80.jpg";
//                        String fileThumb = Path.GetFileNameWithoutExtension(file.FileName) + "80x80.jpg";
//                        var ThumbfullPath2 = Path.Combine(ThumbfullPath, fileThumb);
//                        using (MemoryStream stream = new MemoryStream(System.IO.File.ReadAllBytes(fullPath)))
//                        {
//                            var thumbnail = new WebImage(stream).Resize(570, 570);
//                            thumbnail.Save(ThumbfullPath2, "jpg");
//                        }

//                    }
//                }
//                statuses.Add(UploadResult(file.FileName, file.ContentLength, file.FileName, uniqueFilename));
//            }
//        }

//        private void UploadPartialFile(string fileName, HttpContextBase requestContext, List<ViewDataUploadFilesResult> statuses)
//        {
//            var request = requestContext.Request;
//            if (request.Files.Count != 1) throw new HttpRequestValidationException("Attempt to upload chunked file containing more than one fragment per request");
//            var file = request.Files[0];
//            var inputStream = file.InputStream;
//            String patchOnServer = Path.Combine(StorageRoot);
//            var fullName = Path.Combine(patchOnServer, Path.GetFileName(file.FileName));
//            var ThumbfullPath = Path.Combine(fullName, Path.GetFileName(file.FileName + "80x80.jpg"));
//            ImageHandler handler = new ImageHandler();

//            var ImageBit = ImageHandler.LoadImage(fullName);
//            handler.Save(ImageBit, 80, 80, 10, ThumbfullPath);
//            using (var fs = new FileStream(fullName, FileMode.Append, FileAccess.Write))
//            {
//                var buffer = new byte[1024];

//                var l = inputStream.Read(buffer, 0, 1024);
//                while (l > 0)
//                {
//                    fs.Write(buffer, 0, l);
//                    l = inputStream.Read(buffer, 0, 1024);
//                }
//                fs.Flush();
//                fs.Close();
//            }
//            statuses.Add(UploadResult(file.FileName, file.ContentLength, file.FileName));
//        }

//        public ViewDataUploadFilesResult UploadResult(String FileName, int fileSize, String FileFullPath, string tempFileName = null)
//        {
//            String getType = MimeMapping.GetMimeMapping(FileFullPath);
//            var result = new ViewDataUploadFilesResult()
//            {
//                name = FileName,
//                size = fileSize,
//                type = getType,
//                url = UrlBase + tempFileName,
//                deleteUrl = DeleteURL + tempFileName,
//                thumbnailUrl = Url.Action("CheckThumb", "FileUpload", new { type = getType, FileName = FileName }),
//                deleteType = DeleteType,
//                tempName = tempFileName
//            };
//            return result;
//        }

//        public FilePathResult CheckThumb(String type, String FileName)
//        {
//            var splited = type.Split('/');
//            if (splited.Length == 2)
//            {
//                string extansion = splited[1].ToLower();
//                if (extansion.Equals("jpeg") || extansion.Equals("jpg") || extansion.Equals("png") || extansion.Equals("gif"))
//                {
//                    //String thumbnailUrl = UrlBase + "thumbs/" + Path.GetFileNameWithoutExtension(FileName) + "80x80.jpg";
//                    String thumbnailUrl = UrlBase + "thumbs/" + Path.GetFileNameWithoutExtension(FileName) + "80x80.jpg";
//                    var pathComb = Path.Combine(UrlBase + "thumbs/", Path.GetFileNameWithoutExtension(FileName) + "80x80.jpg");
//                    return File(pathComb, "image/jpeg");
//                    //return pathComb;
//                }
//                else
//                {
//                    if (extansion.Contains("docx") || extansion.Contains("doc")) //Fix for word files
//                    {
//                        return File("~/Content/Image/word.png", "image/png");
//                    }
//                    else if (extansion.Contains("pdf"))
//                    {
//                        return File("~/Content/Image/pdf.png", "image/png");
//                    }

//                    String thumbnailUrl = "~/Content/defaultThumbnails/" + extansion + ".png";
//                    return File(thumbnailUrl, "image/png");
//                }
//            }
//            else
//            {
//                var url = UrlBase + "/thumbs/" + Path.GetFileNameWithoutExtension(FileName) + "80x80.jpg";
//                return File(url, "image/jpeg");
//            }

//        }

//        public FilePathResult CheckSource(String type, String FileName)
//        {
//            var membersPhotoFolderPath = _configSettingService.GetByName(ConfigurationSettings.PhotoFolder.ToString()).Value;
//            var uncroppedPhotoFolderPath = Path.Combine(membersPhotoFolderPath, "Uncropped");
//            var croppedPhotoFolderPath = Path.Combine(membersPhotoFolderPath, "Cropped");

//            bool isCropped = FileName.Contains("Cropped");
//            var splited = type.Split('/');

//            string extension = splited[1].ToLower();
//            if (extension.Equals("jpeg") || extension.Equals("jpg") || extension.Equals("png") || extension.Equals("gif"))
//            {
//                if (isCropped)
//                {
//                    var sourceUrl = Path.Combine(croppedPhotoFolderPath, Path.GetFileNameWithoutExtension(FileName) + ".jpg");
//                    return File(sourceUrl, "image/jpeg");
//                }
//                else
//                {
//                    var sourceUrl = Path.Combine(uncroppedPhotoFolderPath, Path.GetFileNameWithoutExtension(FileName) + ".jpg");
//                    return File(sourceUrl, "image/jpeg");
//                }
//            }
//            else
//            {
//                if (extension.Equals("octet-stream")) //Fix for exe files
//                {
//                    return File("/Content/defaultThumbnails/exe.png", "image/png");
//                }
//                if (extension.Contains("zip")) //Fix for exe files
//                {
//                    return File("/Content/defaultThumbnails/zip.png", "image/png");
//                }
//                String thumbnailUrl = "/Content/defaultThumbnails/" + extension + ".png";
//                return File(thumbnailUrl, "image/png");
//            }
//        }

//        public List<String> FilesList()
//        {

//            List<String> Filess = new List<String>();
//            string path = HostingEnvironment.MapPath(serverMapPath);
//            System.Diagnostics.Debug.WriteLine(path);
//            if (Directory.Exists(path))
//            {
//                DirectoryInfo di = new DirectoryInfo(path);
//                foreach (FileInfo fi in di.GetFiles())
//                {
//                    Filess.Add(fi.Name);
//                    System.Diagnostics.Debug.WriteLine(fi.Name);
//                }

//            }
//            return Filess;
//        }

//        public void DownloadAttachment(string fileName)
//        {
//            try
//            {
//                var contentType = MimeMapping.GetMimeMapping(fileName);
//                var fullPath = Path.Combine(UrlBase + fileName);
//                var origFileName = fileName.Substring(37);

//                Response.Clear();
//                Response.ClearHeaders();
//                Response.ClearContent();
//                Response.ContentType = contentType;
//                Response.AddHeader("Content-Disposition", "attachment; filename=" + origFileName);
//                Response.TransmitFile(fullPath);
//                Response.End();
//            }
//            catch (Exception ex)
//            {
//                throw;
//            }
//        }

//        public void PreviewAttachment(string fileName)
//        {
//            try
//            {
//                var contentType = MimeMapping.GetMimeMapping(fileName);
//                var fullPath = Path.Combine(UrlBase + fileName);
//                var fileNameSplit = fileName.Split('_');
//                var origFileName = fileName.Substring(37);

//                Response.Clear();
//                Response.ClearHeaders();
//                Response.ClearContent();
//                Response.ContentType = contentType;
//                Response.AddHeader("Content-Disposition", "inline; filename=" + origFileName);
//                Response.TransmitFile(fullPath);
//                Response.End();
//            }
//            catch (Exception ex)
//            {
//                throw;
//            }
//        }

//        public JsonResult UploadAppUserPhoto(AppUserPhotoCropCoordinatesViewModel model)
//        {
//            //Delete copy of images from Members attachment
//            if (model.ImagesToDelete != null)
//            {
//                foreach (var imagePath in model.ImagesToDelete)
//                {
//                    HelperDeleteFile(imagePath);
//                }
//            }

//            var widthToUse = 0;
//            var heightToUse = 0;
//            var imageType = "";

//            if (model.IsPhoto)
//            {
//                widthToUse = 190;
//                heightToUse = 190;
//                imageType = "Photo";
//            }
//            else
//            {
//                widthToUse = 267;
//                heightToUse = 100;
//                imageType = "Signature";
//            }

//            var resultList = new List<ViewDataUploadFilesResult>();
//            var base64String = model.AppUserPhoto;
//            var originalWidth = model.OriginalWidth;
//            var originalHeight = model.OriginalHeight;
//            var xCoord = model.XCoord;
//            var yCoord = model.YCoord;
//            var cropWidth = model.CropWidth;
//            var cropHeight = model.CropHeight;
//            var imageName = model.ImageName;
//            var membersPhotoFolderPath = _configSettingService.GetByName(ConfigurationSettings.PhotoFolder.ToString()).Value;
//            var uncroppedPhotoFolderPath = Path.Combine(membersPhotoFolderPath, "Uncropped");
//            var croppedPhotoFolderPath = Path.Combine(membersPhotoFolderPath, "Cropped");

//            // Create directories
//            Directory.CreateDirectory(uncroppedPhotoFolderPath);
//            Directory.CreateDirectory(croppedPhotoFolderPath);

//            // Save Uncropped
//            byte[] bytes = Convert.FromBase64String(base64String);
//            var guid = Guid.NewGuid().ToString();
//            var uniqueFilenameUncropped = $"{guid}_{imageName}_{imageType}.jpg";
//            var uncroppedPhotoFile = Path.Combine(uncroppedPhotoFolderPath, uniqueFilenameUncropped);
//            Bitmap bmpUncropped;
//            using (var ms = new MemoryStream(bytes))
//            {
//                bmpUncropped = new Bitmap(ms);
//            }
//            var resizePhotoUncropped = ResizeImage(bmpUncropped, originalWidth, originalHeight);
//            resizePhotoUncropped.Save(uncroppedPhotoFile);

//            // Save Cropped
//            var sourceImage = new Bitmap(uncroppedPhotoFile);
//            var crop = new Rectangle(xCoord, yCoord, cropWidth, cropHeight);
//            var bmpCropped = new Bitmap(crop.Width, crop.Height);
//            using (var gr = Graphics.FromImage(bmpCropped))
//            {
//                gr.DrawImage(sourceImage, new Rectangle(0, 0, bmpCropped.Width, bmpCropped.Height), crop, GraphicsUnit.Pixel);
//            }
//            var uniqueFilenameCropped = $"{guid}_{imageName}_{imageType}_Cropped.jpg";
//            var croppedPhotoFile = Path.Combine(croppedPhotoFolderPath, uniqueFilenameCropped);
//            var resizePhotoCropped = ResizeImage(bmpCropped, widthToUse, heightToUse);
//            resizePhotoCropped.Save(croppedPhotoFile);

//            // Return results
//            var getTypeUncropped = MimeMapping.GetMimeMapping(uncroppedPhotoFile);
//            FileInfo fileInfoUncropped = new FileInfo(uncroppedPhotoFile);
//            var uncroppedResult = new ViewDataUploadFilesResult()
//            {
//                name = $"{imageName}_{imageType}.jpg",
//                size = Convert.ToInt32(fileInfoUncropped.Length),
//                type = getTypeUncropped,
//                url = uncroppedPhotoFile,
//                deleteUrl = DeleteURL + uniqueFilenameUncropped,
//                thumbnailUrl = Url.Action("CheckSource", "FileUpload", new { type = getTypeUncropped, FileName = uniqueFilenameUncropped }),
//                deleteType = DeleteType,
//                tempName = uniqueFilenameUncropped
//            };
//            resultList.Add(uncroppedResult);

//            var getTypeCropped = MimeMapping.GetMimeMapping(croppedPhotoFile);
//            var fileInfoCropped = new FileInfo(croppedPhotoFile);
//            var croppedResult = new ViewDataUploadFilesResult()
//            {
//                name = $"{imageName}_{imageType}_Cropped.jpg",
//                size = Convert.ToInt32(fileInfoCropped.Length),
//                type = getTypeCropped,
//                url = croppedPhotoFile,
//                deleteUrl = DeleteURL + uniqueFilenameCropped,
//                thumbnailUrl = Url.Action("CheckSource", "FileUpload", new { type = getTypeCropped, FileName = uniqueFilenameCropped }),
//                deleteType = DeleteType,
//                tempName = uniqueFilenameCropped
//            };
//            resultList.Add(croppedResult);

//            var files = new JsonFiles(resultList);

//            bool isEmpty = !resultList.Any();
//            if (isEmpty)
//            {
//                return Json("Error ");
//            }
//            else
//            {
//                return Json(new
//                {
//                    success = true,
//                    data = files
//                }, JsonRequestBehavior.AllowGet);
//            }
//        }

//        private Bitmap ResizeImage(System.Drawing.Image image, int width, int height)
//        {
//            var destRect = new Rectangle(0, 0, width, height);
//            var destImage = new Bitmap(width, height);

//            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

//            using (var graphics = Graphics.FromImage(destImage))
//            {
//                graphics.CompositingMode = CompositingMode.SourceCopy;
//                graphics.CompositingQuality = CompositingQuality.HighQuality;
//                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
//                graphics.SmoothingMode = SmoothingMode.HighQuality;
//                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

//                using (var wrapMode = new ImageAttributes())
//                {
//                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
//                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
//                }
//            }

//            return destImage;
//        }

//        #endregion
//    }
//}