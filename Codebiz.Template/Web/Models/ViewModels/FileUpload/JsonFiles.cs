﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models.ViewModels.FileUpload
{
    public class JsonFiles
    {
        public ViewDataUploadFilesResult[] files;
        public string TempFolder { get; set; }
        public JsonFiles(List<ViewDataUploadFilesResult> filesList)
        {
            files = new ViewDataUploadFilesResult[filesList.Count];
            for (int i = 0; i < filesList.Count; i++)
            {
                files[i] = filesList.ElementAt(i);
            }
        }
    }
}