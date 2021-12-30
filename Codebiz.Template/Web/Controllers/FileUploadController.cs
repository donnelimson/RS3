using Codebiz.Domain.Common.Model.Enums;
using ERP.Common.Helpers;
using Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Hosting;
using System.Web.Mvc;
using Web.Models.ViewModels.FileUpload;

namespace Web.Controllers
{
    public class FileUploadController : Controller
    {
        private readonly IConfigSettingService _configSettingService;
        private string _folderPath;

        public FileUploadController(IConfigSettingService configSettingService)
        {
            _configSettingService = configSettingService;
        }

        public ActionResult Show()
        {
            JsonFiles ListOfFiles = HelperGetFileList();
            var model = new FilesViewModel()
            {
                Files = ListOfFiles.files
            };

            return View(model);
        }


        [HttpPost]
        public JsonResult UploadProductPhotos()
        {
            _folderPath = _configSettingService.GetByName(ConfigurationSettings.ProductPhotoFolder.ToString()).Value;

            return Upload();
        }

        public JsonResult GetFileList()
        {
            var list = HelperGetFileList();
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult DeleteFile(string file, string folderPath)
        {
            _folderPath = folderPath;
            HelperDeleteFile(file);
            return Json("OK", JsonRequestBehavior.AllowGet);
        }

        public JsonResult Upload()
        {
            var resultList = new List<ViewDataUploadFilesResult>();

            var CurrentContext = HttpContext;

            UploadAndShowResults(CurrentContext, resultList);
            JsonFiles files = new JsonFiles(resultList);

            bool isEmpty = !resultList.Any();
            if (isEmpty)
            {
                return Json("Error ");
            }
            else
            {
                return Json(files);
            }
        }

        #region FilesHelper
        public void DeleteFiles(String pathToDelete, string folderPath)
        {
            _folderPath = folderPath;

            string path = HostingEnvironment.MapPath(pathToDelete);

            if (Directory.Exists(path))
            {
                DirectoryInfo di = new DirectoryInfo(path);
                foreach (FileInfo fi in di.GetFiles())
                {
                    System.IO.File.Delete(fi.FullName);
                }

                di.Delete(true);
            }
        }

        public String HelperDeleteFile(String file)
        {
            var fullPath = Path.Combine(_folderPath, file);
            String thumbnail = Path.Combine(_folderPath, "thumbs", Path.GetFileNameWithoutExtension(file.Remove(0, 37)) + "80x80.jpg");

            if (System.IO.File.Exists(fullPath))
            {
                var succesMessage = "Ok";
                System.IO.File.Delete(fullPath);

                return succesMessage;
            }

            var failMessage = "Error Delete";

            return failMessage;
        }

        public JsonFiles HelperGetFileList()
        {

            var r = new List<ViewDataUploadFilesResult>();

            String fullPath = Path.Combine(_folderPath);

            if (Directory.Exists(fullPath))
            {
                DirectoryInfo dir = new DirectoryInfo(fullPath);
                foreach (FileInfo file in dir.GetFiles())
                {
                    int SizeInt = unchecked((int)file.Length);
                    r.Add(UploadResult(file.Name, SizeInt, file.FullName));
                }

            }
            JsonFiles files = new JsonFiles(r);

            return files;
        }

        public void UploadAndShowResults(HttpContextBase ContentBase, List<ViewDataUploadFilesResult> resultList)
        {
            var httpRequest = ContentBase.Request;

            var fullPath = Path.Combine(_folderPath);
            Directory.CreateDirectory(fullPath);
            // Create new folder for thumbs
            Directory.CreateDirectory(fullPath + "/thumbs/");

            foreach (String inputTagName in httpRequest.Files)
            {

                var headers = httpRequest.Headers;

                var file = httpRequest.Files[inputTagName];
                System.Diagnostics.Debug.WriteLine(file.FileName);

                if (string.IsNullOrEmpty(headers["X-File-Name"]))
                {

                    UploadWholeFile(ContentBase, resultList);
                }
                else
                {

                    UploadPartialFile(headers["X-File-Name"], ContentBase, resultList);
                }
            }
        }

        private void UploadWholeFile(HttpContextBase requestContext, List<ViewDataUploadFilesResult> statuses)
        {

            var request = requestContext.Request;
            for (int i = 0; i < request.Files.Count; i++)
            {
                var file = request.Files[i];
                var pathOnServer = Path.Combine(_folderPath);

                var guid = Guid.NewGuid().ToString();
                var uniqueFilename = $"{guid}_{file.FileName}";

                var fullPath = Path.Combine(pathOnServer, Path.GetFileName(uniqueFilename));
                file.SaveAs(fullPath);

                //Create thumb
                string[] imageArray = file.FileName.Split('.');
                if (imageArray.Length != 0)
                {
                    String extansion = imageArray[imageArray.Length - 1].ToLower();
                    if (extansion != "jpg" && extansion != "png" && extansion != "jpeg" && extansion!="gif")  //Do not create thumb if file is not an image
                    {
                        //if (extansion == "docx" || extansion == "doc")
                        //{
                        //    var ThumbfullPath = Path.Combine(pathOnServer, "thumbs");
                        //    var fileWord = File("/Content/Image/word.png", "image/png");
                        //    String fileThumb = Path.GetFileNameWithoutExtension(fileWord.FileName) + "80x80.jpg";
                        //    var ThumbfullPath2 = Path.Combine(ThumbfullPath, fileThumb);
                        //    using (MemoryStream stream = new MemoryStream(System.IO.File.ReadAllBytes(fullPath)))
                        //    {
                        //        var thumbnail = new WebImage(stream).Resize(570, 570);
                        //        thumbnail.Save(ThumbfullPath2, "jpg");
                        //    }
                        //}
                    }
                    else
                    {
                        var ThumbfullPath = Path.Combine(pathOnServer, "thumbs");
                        //String fileThumb = file.FileName + ".80x80.jpg";
                        String fileThumb = Path.GetFileNameWithoutExtension(file.FileName) + "80x80.jpg";
                        var ThumbfullPath2 = Path.Combine(ThumbfullPath, fileThumb);
                        using (MemoryStream stream = new MemoryStream(System.IO.File.ReadAllBytes(fullPath)))
                        {
                            var thumbnail = new WebImage(stream).Resize(570, 570);
                            thumbnail.Save(ThumbfullPath2, "jpg");
                        }
                    }
                }
                statuses.Add(UploadResult(file.FileName, file.ContentLength, file.FileName, uniqueFilename));
            }
        }

        private void UploadPartialFile(string fileName, HttpContextBase requestContext, List<ViewDataUploadFilesResult> statuses)
        {
            var request = requestContext.Request;

            if (request.Files.Count != 1)
            {
                throw new HttpRequestValidationException("Attempt to upload chunked file containing more than one fragment per request");
            }

            var file = request.Files[0];
            var inputStream = file.InputStream;
            var patchOnServer = Path.Combine(_folderPath);
            var fullName = Path.Combine(patchOnServer, Path.GetFileName(file.FileName));
            var ThumbfullPath = Path.Combine(fullName, Path.GetFileName(file.FileName + "80x80.jpg"));
            ImageHandler handler = new ImageHandler();

            var ImageBit = ImageHandler.LoadImage(fullName);
            handler.Save(ImageBit, 80, 80, 10, ThumbfullPath);

            using (var fs = new FileStream(fullName, FileMode.Append, FileAccess.Write))
            {
                var buffer = new byte[1024];

                var l = inputStream.Read(buffer, 0, 1024);
                while (l > 0)
                {
                    fs.Write(buffer, 0, l);
                    l = inputStream.Read(buffer, 0, 1024);
                }
                fs.Flush();
                fs.Close();
            }

            statuses.Add(UploadResult(file.FileName, file.ContentLength, file.FileName));
        }

        public ViewDataUploadFilesResult UploadResult(String FileName, int fileSize, String FileFullPath, string tempFileName = null)
        {
            String getType = MimeMapping.GetMimeMapping(FileFullPath);
            var result = new ViewDataUploadFilesResult()
            {
                name = FileName,
                size = fileSize,
                type = getType,
                url = Path.Combine(_folderPath, tempFileName),
                deleteUrl = Url.Action("DeleteFile", "FileUpload", new { file = tempFileName, folderPath = _folderPath }),
                thumbnailUrl = Url.Action("CheckThumbUpload", "FileUpload", new { type = getType, FileName = FileName, Folder = _folderPath}),
                deleteType = "GET",
                tempName = tempFileName
            };
            return result;
        }

        public FilePathResult CheckThumb(string type, string fileName, string folder)
        {
            var typeSplit = type.Split('/');
            var defaultPhoto = Path.Combine("~/assets/global/img/image-unavailable.png");
            var extension = typeSplit[1].ToLower(); 
            string thumbnailUrl;

            if (extension.Equals("jpeg") || extension.Equals("jpg") || extension.Equals("png") || extension.Equals("gif"))
            {
                thumbnailUrl = Path.Combine(folder, "thumbs", Path.GetFileNameWithoutExtension(fileName.Replace(extension, "")) + "80x80.jpg");

                if (System.IO.File.Exists(thumbnailUrl))
                {
                    return File(thumbnailUrl, "image/jpeg");
                }
                else
                {
                    return File(defaultPhoto, "image/png");
                }
            }
            else
            {
                if ((extension.Equals("msword") || extension.Equals("vnd.openxmlformats-officedocument.wordprocessingml.document")))
                {
                    return File("~/Content/Image/word.png", "image/png");
                }
                else if (extension.Equals("pdf"))
                {
                    return File("~/Content/Image/pdf.png", "image/png");
                }
                else
                {
                    return File(defaultPhoto, "image/png");
                }
            }
        }

        public FilePathResult CheckThumbUpload(string type, string fileName, string folder)
        {
            var typeSplit = type.Split('/');
            var defaultPhoto = Path.Combine("~/assets/global/img/image-unavailable.png");
            var extension = typeSplit[1].ToLower();
            string thumbnailUrl;
            string thumbnailUrl2;

            if (extension.Equals("jpeg") || extension.Equals("jpg") || extension.Equals("png")|| extension.Equals("gif"))
            {
                thumbnailUrl = Path.Combine(folder, "thumbs", Path.GetFileNameWithoutExtension(fileName) + "80x80.jpg");

                if (System.IO.File.Exists(thumbnailUrl))
                {
                    return File(thumbnailUrl, "image/jpeg");
                }
                else
                {
                    thumbnailUrl2 = Path.Combine(folder, "thumbs", Path.GetFileNameWithoutExtension(fileName.Substring(37)) + "80x80.jpg");

                    if (System.IO.File.Exists(thumbnailUrl2))
                    {
                        return File(thumbnailUrl2, "image/jpeg");
                    }
                    else
                    {
                        return File(defaultPhoto, "image/png");
                    }
                }
            }
            else
            {
                if ((extension.Equals("msword") || extension.Equals("vnd.openxmlformats-officedocument.wordprocessingml.document")))
                {
                    return File("~/Content/Image/word.png", "image/png");
                }
                else if (extension.Equals("pdf"))
                {
                    return File("~/Content/Image/pdf.png", "image/png");
                }
                else
                {
                    return File("~/Content/DefaultPhoto/" + extension + ".png", "image/png");
                }
            }
        }

        public FilePathResult CheckSource(string type, string fileName, string folder)
        {
            var defaultPhoto = Path.Combine("~/assets/global/img/photo-unavailable.jpg");

            if(!string.IsNullOrEmpty(type) && !string.IsNullOrEmpty(fileName))
            {
                if (folder.Contains("MemberSignatureFolder"))
                {
                    defaultPhoto = Path.Combine("~/assets/global/img/no-signature.jpg");
                }

                var fileNameSplit = fileName.Split('_');

                var sourceUrl = folder.Contains("CounterSetup") ? Path.Combine(folder, Path.GetFileNameWithoutExtension(fileName) + (Path.GetExtension(fileName) == ".mp4" ? ".mp4" : Path.GetExtension(fileName) == ".gif" ? ".gif": ".jpg")) : fileNameSplit[fileNameSplit.Length - 1] == "Cropped.jpg"
                    ? Path.Combine(folder, "Cropped", Path.GetFileNameWithoutExtension(fileName) + ".jpg")
                    : Path.Combine(folder, "Uncropped", Path.GetFileNameWithoutExtension(fileName) + ".jpg");

                if (System.IO.File.Exists(sourceUrl))
                {
                    if(Path.GetExtension(sourceUrl) == ".mp4")
                    {
                        return File(sourceUrl, "video/mp4");
                    }
                    if (Path.GetExtension(sourceUrl) == ".gif")
                    {
                        return File(sourceUrl, "image/gif");
                    }
                    return File(sourceUrl, "image/jpeg");
                }
                else
                {
                    return File(defaultPhoto, "image/jpeg");
                }
            }
            else
            {
                return File(defaultPhoto, "image/jpeg");
            }
        }

        public List<String> FilesList()
        {

            List<String> Filess = new List<String>();
            string path = HostingEnvironment.MapPath(_folderPath);
            System.Diagnostics.Debug.WriteLine(path);
            if (Directory.Exists(path))
            {
                DirectoryInfo di = new DirectoryInfo(path);
                foreach (FileInfo fi in di.GetFiles())
                {
                    Filess.Add(fi.Name);
                    System.Diagnostics.Debug.WriteLine(fi.Name);
                }

            }
            return Filess;
        }

        public void DownloadAttachment(string fileName, string folder)
        {
            try
            {
                var contentType = MimeMapping.GetMimeMapping(fileName);
                var fullPath = Path.Combine(folder, fileName);
                var origFileName = fileName;

                Response.Clear();
                Response.ClearHeaders();
                Response.ClearContent();
                Response.ContentType = contentType;
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + origFileName.Substring(37));
                Response.WriteFile(fullPath);
                Response.End();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void DownloadAttachmentConsumer(string fileName, string folder, string orignalname)
        {
            try
            {
                var contentType = MimeMapping.GetMimeMapping(fileName);
                var fullPath = Path.Combine(folder, fileName);
                var origFileName = orignalname;

                Response.Clear();
                Response.ClearHeaders();
                Response.ClearContent();
                Response.ContentType = contentType;
                Response.AddHeader("Content-Disposition",
                    string.Format("attachment; filename = \"{0}\"",
                    Path.GetFileName(origFileName)));
                Response.TransmitFile(fullPath);
                Response.End();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void PreviewAttachment(string fileName, string folder)
        {
            try
            {
                var contentType = MimeMapping.GetMimeMapping(fileName);
                var fullPath = Path.Combine(folder, fileName);
                var fileNameSplit = fileName.Split('_');
                var origFileName = fileName.Substring(37);

                Response.Clear();
                Response.ClearHeaders();
                Response.ClearContent();
                Response.ContentType = contentType;
                Response.AddHeader("Content-Disposition",
                    string.Format("inline; filename = \"{0}\"",
                    Path.GetFileName(origFileName)));
                Response.TransmitFile(fullPath);
                Response.End();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpGet]
        public void DownloadExcelFile(string fileName)
        {
            try
            {
                //Get the temp folder and file path in server
                string fullPath = Server.MapPath(Path.Combine("~/Download", fileName));

                Response.Clear();
                Response.ClearHeaders();
                Response.ClearContent();
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("Content-Disposition",
                    string.Format("attachment; filename = \"{0}\"",
                    Path.GetFileName(fileName)));
                Response.TransmitFile(fullPath);
                Response.End();

                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        JsonResult UploadPhoto(ImageCropCoordinatesViewModel model)
        {
            //Delete copy of images from Members attachment
            if (model.ImagesToDelete != null)
            {
                foreach (var imagePath in model.ImagesToDelete)
                {
                    HelperDeleteFile(imagePath);
                }
            }

            int widthToUse;
            int heightToUse;
            string imageType;

            if (model.IsPhoto)
            {
                widthToUse = 190;
                heightToUse = 190;
                imageType = "Photo";
            }
            else
            {
                widthToUse = 267;
                heightToUse = 100;
                imageType = "Signature";
            }

            var resultList = new List<ViewDataUploadFilesResult>();
            var base64String = model.Photo;
            var originalWidth = model.OriginalWidth;
            var originalHeight = model.OriginalHeight;
            var xCoord = model.XCoord;
            var yCoord = model.YCoord;
            var cropWidth = model.CropWidth;
            var cropHeight = model.CropHeight;
            var imageName = model.ImageName;
            var uncroppedPhotoFolderPath = Path.Combine(_folderPath, "Uncropped");
            var croppedPhotoFolderPath = Path.Combine(_folderPath, "Cropped");

            // Create directories
            Directory.CreateDirectory(uncroppedPhotoFolderPath);
            Directory.CreateDirectory(croppedPhotoFolderPath);

            // Save Uncropped
            byte[] bytes = Convert.FromBase64String(base64String);
            var guid = Guid.NewGuid().ToString();
            var uniqueFilenameUncropped = $"{guid}_{imageName}_{imageType}.jpg";
            var uncroppedPhotoFile = Path.Combine(uncroppedPhotoFolderPath, uniqueFilenameUncropped);
            Bitmap bmpUncropped;
            using (var ms = new MemoryStream(bytes))
            {
                bmpUncropped = new Bitmap(ms);
            }
            var resizePhotoUncropped = ResizeImage(bmpUncropped, originalWidth, originalHeight);
            resizePhotoUncropped.Save(uncroppedPhotoFile);

            // Save Cropped
            var sourceImage = new Bitmap(uncroppedPhotoFile);
            var crop = new Rectangle(xCoord, yCoord, cropWidth, cropHeight);
            var bmpCropped = new Bitmap(crop.Width, crop.Height);
            using (var gr = Graphics.FromImage(bmpCropped))
            {
                gr.DrawImage(sourceImage, new Rectangle(0, 0, bmpCropped.Width, bmpCropped.Height), crop, GraphicsUnit.Pixel);
            }
            var uniqueFilenameCropped = $"{guid}_{imageName}_{imageType}_Cropped.jpg";
            var croppedPhotoFile = Path.Combine(croppedPhotoFolderPath, uniqueFilenameCropped);
            var resizePhotoCropped = ResizeImage(bmpCropped, widthToUse, heightToUse);
            resizePhotoCropped.Save(croppedPhotoFile);

            // Return results
            var getTypeUncropped = MimeMapping.GetMimeMapping(uncroppedPhotoFile);
            FileInfo fileInfoUncropped = new FileInfo(uncroppedPhotoFile);
            var uncroppedResult = new ViewDataUploadFilesResult()
            {
                name = $"{imageName}_{imageType}.jpg",
                size = Convert.ToInt32(fileInfoUncropped.Length),
                type = getTypeUncropped,
                url = uncroppedPhotoFile,
                deleteUrl = Url.Action("DeleteFile", "FileUpload", new { file = uniqueFilenameUncropped, folderPath = _folderPath }),
                thumbnailUrl = Url.Action("CheckSource", "FileUpload", new { type = getTypeUncropped, FileName = uniqueFilenameUncropped, Folder = _folderPath }),
                deleteType = "GET",
                tempName = uniqueFilenameUncropped
            };
            resultList.Add(uncroppedResult);

            var getTypeCropped = MimeMapping.GetMimeMapping(croppedPhotoFile);
            var fileInfoCropped = new FileInfo(croppedPhotoFile);
            var croppedResult = new ViewDataUploadFilesResult()
            {
                name = $"{imageName}_{imageType}_Cropped.jpg",
                size = Convert.ToInt32(fileInfoCropped.Length),
                type = getTypeCropped,
                url = croppedPhotoFile,
                deleteUrl = Url.Action("DeleteFile", "FileUpload", new { file = uniqueFilenameCropped, folderPath = _folderPath }),
                thumbnailUrl = Url.Action("CheckSource", "FileUpload", new { type = getTypeCropped, FileName = uniqueFilenameCropped, Folder = _folderPath }),
                deleteType = "GET",
                tempName = uniqueFilenameCropped
            };
            resultList.Add(croppedResult);

            var files = new JsonFiles(resultList);

            bool isEmpty = !resultList.Any();
            if (isEmpty)
            {
                return Json("Error ");
            }
            else
            {
                return Json(new
                {
                    success = true,
                    data = files
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult UploadEmployeePhoto(ImageCropCoordinatesViewModel model)
        {
            _folderPath = _configSettingService.GetByName(ConfigurationSettings.EmployeePhotoFolder.ToString()).Value;

            return UploadPhoto(model);
        }
 
        private Bitmap ResizeImage(System.Drawing.Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        #endregion
    }
}