using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models.ViewModels.FileUpload
{
    public class ImageCropCoordinatesViewModel
    {
        public string Photo { get; set; }
        public int OriginalWidth { get; set; }
        public int OriginalHeight { get; set; }
        public int XCoord { get; set; }
        public int YCoord { get; set; }
        public int CropWidth { get; set; }
        public int CropHeight { get; set; }
        public string ImageName { get; set; }
        public List<string> ImagesToDelete { get; set; }
        public bool IsPhoto { get; set; }
    }
}